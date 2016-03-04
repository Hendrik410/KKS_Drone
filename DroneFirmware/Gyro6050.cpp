#include "Gyro6050.h"

Gyro6050::Gyro6050(Config* config) : Gyro(config) {
}

bool Gyro6050::init() {
	Log::info("Gyro6050", "init()");

	Wire.begin(SDA, SCL);

	Log::debug("Gyro6050", "mpu.reset()");
	mpu.reset();

	Log::debug("Gyro6050", "mpu.initialize()");
	mpu.initialize();

	Log::debug("Gyro6050", "mpu.testConnection()");
	if (!mpu.testConnection()) {
		Log::error("Gyro6050", "testConnection() failed!");
		mpuOK = false;
		return false;
	}

	Log::info("Gyro6050", "dmpInitialize()");

#if USE_DMP
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
	Log::info("Gyro6050", "done with init");

	mpuOK = true;
	return mpuOK;
}

void Gyro6050::update() {
	if (!mpuOK)
		return;

	Profiler::begin("Gyro6050::update()");
#if USE_DMP
	if (mpu.getIntFIFOBufferOverflowStatus() || mpu.getFIFOCount() == 1024) { // 1024 Bytes ist der FIFO Buffer groﬂ auf dem MPU6050
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
	mpu.getFIFOBytes(fifoBuffer, mpu.dmpGetFIFOPacketSize());
	Profiler::end();

	// Gyro Werte
	mpu.dmpGetQuaternion(&q, fifoBuffer);
	mpu.dmpGetGravity(&gravity, &q);
	mpu.dmpGetYawPitchRoll(ypr, &q, &gravity);

	// Beschleunigung
	mpu.dmpGetAccel(&aa, fifoBuffer);
	mpu.dmpGetLinearAccel(&aaReal, &aa, &gravity);

	yaw = ypr[0];
	roll = ypr[1];
	pitch = ypr[2];

	accX = aaReal.x;
	accY = aaReal.y;
	accZ = aaReal.z;
#else
	int16_t values[6];
	mpu.getMotion6(values, values + 1, values + 2, values + 3, values + 4, values + 5);

	accX = (float)values[0];
	accY = (float)values[1];
	accZ = (float)values[2];

	gyroX = (float)values[3];
	gyroY = (float)values[4];
	gyroZ = (float)values[5];
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
