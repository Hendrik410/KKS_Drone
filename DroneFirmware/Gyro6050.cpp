#include "Gyro6050.h"

Gyro6050::Gyro6050(Config* config) : Gyro(config) {
}

void Gyro6050::init() {
	Log::info("Gyro", "init()");

	Wire.begin(SDA, SCL);

	mpu.reset();
	mpu.initialize();
	if (mpu.testConnection())
		Log::error("Gyro", "testConnection() failed!");
	else
		Log::info("Gyro", "testConnection() OK");

	Log::info("Gyro", "dmpInitialize()");

	int result = mpu.dmpInitialize();
	if (result)
		Log::error("Gyro", "result: %d", result);

	Log::info("Gyro", "done with init");

	fifoBuffer = (byte*)malloc(sizeof(byte) * mpu.dmpGetFIFOPacketSize());

	mpu.setDMPEnabled(true);
	mpu.resetFIFO();
}

void Gyro6050::update() {
	if (mpu.getIntFIFOBufferOverflowStatus()) {
		mpu.resetFIFO();

		Log::error("Gyro", "FIFO overflow!");
		return;
	}

	if (!mpu.dmpPacketAvailable())
		return;

	Profiler::begin("Gyro::update()");

	Profiler::begin("getFIFOBytes()");
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

	_dirty = true;
	Profiler::end();
}

void Gyro6050::reset() {
	mpu.resetSensors();
}

float Gyro6050::getTemperature() {
	return mpu.getTemperature() / 340.00 + 36.53;
}