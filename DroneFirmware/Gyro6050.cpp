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

#if USE_DMP
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
#endif

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

	// erstes Aufruf überspringen
	if (firstUpdate) {
		mpu.resetFIFO();
		firstUpdate = false;
		lastSample = millis();
		return;
	}

	// nur alle 10 Millisekunden Daten einlesen
	uint32_t interval = millis() - lastSample;
	if (interval < CYCLE_GYRO)
		return;
	Profiler::pushData("Jitter::Gyro6050()", interval - CYCLE_GYRO);
	lastSample = millis();

	Profiler::begin("Gyro6050::update()");

	float gyroValues[9];
#if USE_DMP
	int fifoCount = mpu.getFIFOCount();
	if (fifoCount == 1024) { // 1024 Bytes ist der FIFO Buffer groß auf dem MPU6050
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

	// FIFO vollständig einlesen
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

	gyroValues[3] = aaReal.x * accRes;
	gyroValues[4] = aaReal.y * accRes;
	gyroValues[5] = aaReal.z * accRes;

	// Gyro Werte
	int16_t values[3];
	mpu.dmpGetGyro(values, fifoBuffer);

	gyroValues[6] = values[0] * gyroRes;
	gyroValues[7] = values[1] * gyroRes;
	gyroValues[8] = values[2] * gyroRes;

#else

	int16_t values[6];
	mpu.getMotion6(values, values + 1, values + 2, values + 3, values + 4, values + 5);

	gyroValues[0] = 0;
	gyroValues[1] = 0;
	gyroValues[2] = 0;

	gyroValues[3] = values[0] * accRes;
	gyroValues[4] = values[1] * accRes;
	gyroValues[5] = values[2] * accRes;

	gyroValues[6] = values[3] * gyroRes;
	gyroValues[7] = values[4] * gyroRes;
	gyroValues[8] = values[5] * gyroRes;
#endif

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
	

	_dirty = true;
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
