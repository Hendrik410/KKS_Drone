#include "Gyro6050.h"

Gyro6050::Gyro6050(Config* config) : Gyro(config) {
}

char* Gyro6050::name() {
	if (mpuOK)
		return "InvenSense MPU-6050";
	return "";
}

char* Gyro6050::magnetometerName() {
	return "";
}

bool Gyro6050::init() {
	Log::info("Gyro6050", "init()");

	Wire.begin(SDA, SCL);
	Wire.setClock(400000L); // gotta go fast

	if (!mpu.testConnection()) {
		Log::error("Gyro6050", "Connection failed");
		mpuOK = false;
		return false;
	}

	Log::debug("Gyro6050", "mpu.reset()");
	mpu.reset();

	Log::debug("Gyro6050", "mpu.initialize()");
	mpu.initialize();

	useDMP = config->GyroUseDMP;
	if (useDMP) {
		Log::info("Gyro6050", "dmpInitialize()");
		int result = mpu.dmpInitialize();
		if (result != 0) {
			Log::error("Gyro6050", "failure: %d", result);
			mpuOK = false;
			return false;
		}

		fifoBuffer = (byte*)malloc(sizeof(byte) * mpu.dmpGetFIFOPacketSize());

		mpu.setDMPEnabled(true);
		mpu.resetFIFO();
	}

	mpu.setClockSource(MPU6050_CLOCK_PLL_ZGYRO);
	mpu.setFullScaleAccelRange(MPU6050_ACCEL_FS_8);
	mpu.setFullScaleGyroRange(MPU6050_GYRO_FS_2000);
	mpu.setDLPFMode(0);

	float accRange[4] = { 2, 4, 8, 16 }; // g
	float gyroRange[4] = { 250, 500, 1000, 2000 }; // degress/s

	accRes = accRange[mpu.getFullScaleAccelRange()] / 32768.0f;
	gyroRes = gyroRange[mpu.getFullScaleGyroRange()] / 32768.0f;

	Log::info("Gyro6050", "done with init");

	mpuOK = true;
	firstUpdate = true;
	return mpuOK;
}

float Gyro6050::filter(float value) {
	if (abs(value) < 0.1f)
		return 0;
	return value;
}

void Gyro6050::update() {
	if (!mpuOK)
		return;

	// erstes Aufruf ¸berspringen
	if (firstUpdate) {
		mpu.resetFIFO();
		firstUpdate = false;

		lastSample = micros();
		memset(lastGyroValues, 0, sizeof(lastGyroValues));
		return;
	}

	// nur alle 10 Millisekunden Daten einlesen
	uint32_t interval = micros() - lastSample;
	if (interval < CYCLE_GYRO * 1000)
		return;
	Profiler::pushData("Gyro6050::jitter()", interval - CYCLE_GYRO * 1000);
	lastSample = micros();

	Profiler::begin("Gyro6050::update()");

	float gyroValues[9];

	float accRes = config->GyroUseRaw ? 1 : this->accRes;
	float gyroRes = config->GyroUseRaw ? 1 : this->gyroRes;
	int16_t shift = config->GyroUseRaw ? 2 : 0;

	if (useDMP) {
		int fifoCount = mpu.getFIFOCount();
		if (fifoCount == 1024) { // 1024 Bytes ist der FIFO Buffer groﬂ auf dem MPU6050
			mpu.resetFIFO();

			Log::error("Gyro6050", "FIFO overflow!");
			Profiler::end();
			return;
		}

		Profiler::begin("Gyro6050::getFIFOBytes()");
		int size = mpu.dmpGetFIFOPacketSize();
		if (fifoCount < size) { // nicht genug Daten
			Profiler::end();
			Profiler::end();
			return;
		}

		// FIFO vollst‰ndig einlesen
		while (fifoCount >= size) {
			mpu.getFIFOBytes(fifoBuffer, size);
			fifoCount -= size;
			yield();
		}
		Profiler::end();

		// Yaw Pitch Roll
		mpu.dmpGetQuaternion(&q, fifoBuffer);
		mpu.dmpGetGravity(&gravity, &q);
		mpu.dmpGetYawPitchRoll(ypr, &q, &gravity);

		gyroValues[0] = ypr[2];
		gyroValues[1] = ypr[1];
		gyroValues[2] = ypr[0];

		// Beschleunigung
		mpu.dmpGetAccel(&aa, fifoBuffer);
		mpu.dmpGetLinearAccel(&aaReal, &aa, &gravity);

		gyroValues[3] = (aaReal.x >> shift) * accRes;
		gyroValues[4] = (aaReal.y >> shift) * accRes;
		gyroValues[5] = (aaReal.z >> shift) * accRes;

		// Gyro Werte
		int16_t values[3];
		mpu.dmpGetGyro(values, fifoBuffer);

		gyroValues[6] = (values[0] >> shift) * gyroRes;
		gyroValues[7] = (values[1] >> shift) * gyroRes;
		gyroValues[8] = (values[2] >> shift) * gyroRes;

	}
	else {
		gyroValues[0] = 0;
		gyroValues[1] = 0;
		gyroValues[2] = 0;

		int16_t x, y, z;
		mpu.getAcceleration(&x, &y, &z);

		gyroValues[3] = (x >> shift) * accRes;
		gyroValues[4] = (y >> shift) * accRes;
		gyroValues[5] = (z >> shift) * accRes;

		mpu.getRotation(&x, &y, &z);

		gyroValues[6] = (x >> shift) * gyroRes;
		gyroValues[7] = (y >> shift) * gyroRes;
		gyroValues[8] = (z >> shift) * gyroRes;
	}

	if (memcmp(gyroValues, lastGyroValues, sizeof(lastGyroValues)) != 0) {
		Profiler::restart("Gyro6050::data()");

		_dirty = true;
		memcpy(lastGyroValues, gyroValues, sizeof(lastGyroValues));
	}


	for (int i = 3; i < 9; i++)
		gyroValues[i] = filter(gyroValues[i]);

	roll = gyroValues[1];
	pitch = -gyroValues[0];
	yaw = gyroValues[2];

	accX = -gyroValues[3];
	accY = -gyroValues[4];
	accZ = -gyroValues[5];

	gyroX = -gyroValues[7];
	gyroY = -gyroValues[6];
	gyroZ = -gyroValues[8];

	Profiler::end();
}

void Gyro6050::reset() {
	if (mpuOK)
		mpu.resetSensors();
}

float Gyro6050::getTemperature() {
	if (mpuOK)
		return mpu.getTemperature() / 340.00 + 36.53;
	return 0;
}

bool Gyro6050::hasMagnetometer() {
	return false;
}

bool Gyro6050::hasCompass() {
	return false;
}
