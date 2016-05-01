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
	return mpuOK;
}

void Gyro6050::update() {
	if (!mpuOK)
		return;

	Profiler::begin("Gyro6050::update()");
#if USE_DMP
	if (mpu.getIntFIFOBufferOverflowStatus() || mpu.getFIFOCount() == 1024) { // 1024 Bytes ist der FIFO Buffer groß auf dem MPU6050
		mpu.resetFIFO();

		Log::error("Gyro6050", "FIFO overflow!");
		Profiler::end();
		return;
	}

	if (!mpu.dmpPacketAvailable()) {
		Profiler::end();
		return;
	}

	Profiler::begin("Gyro6050::getFIFOBytes()");
	int fifoCount = mpu.getFIFOCount();
	int size = mpu.dmpGetFIFOPacketSize();
	while (fifoCount >= size) {
		mpu.getFIFOBytes(fifoBuffer, size);
		fifoCount -= size;
	}
	Profiler::end();

	// Yaw Pitch Roll
	mpu.dmpGetQuaternion(&q, fifoBuffer);
	mpu.dmpGetGravity(&gravity, &q);
	mpu.dmpGetYawPitchRoll(ypr, &q, &gravity);

	yaw = ypr[0];
	roll = ypr[1];
	pitch = ypr[2];

	// Beschleunigung
	mpu.dmpGetAccel(&aa, fifoBuffer);
	mpu.dmpGetLinearAccel(&aaReal, &aa, &gravity);

	accX = aaReal.x * accRes;
	accY = aaReal.y * accRes;
	accZ = aaReal.z * accRes;

	// Gyro Werte
	int16_t values[3];
	mpu.dmpGetGyro(values, fifoBuffer);

	gyroX = values[0] * gyroRes;
	gyroY = values[1] * gyroRes;
	gyroZ = values[2] * gyroRes;

	// Richtung anpassen
	pitch *= -1;
	gyroZ *= -1;
#else

	int16_t values[6];
	mpu.getMotion6(values, values + 1, values + 2, values + 3, values + 4, values + 5);

	accX = values[0] * accRes;
	accY = values[1] * accRes;
	accZ = values[2] * accRes;

	gyroX = values[3] * gyroRes;
	gyroY = values[4] * gyroRes;
	gyroZ = values[5] * gyroRes;
#endif

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
