// 
// 
// 

#include "Gyro.h"

Gyro::Gyro(Config* config) {
	this->config = config;

	this->pitchOffset = 0;
	this->rollOffset = 0;
	this->yawOffset = 0;

	this->accelerationXOffset = 0;
	this->accelerationYOffset = 0;
	this->accelerationZOffset = 0;

	this->ypr[0] = 0;
	this->ypr[1] = 0;
	this->ypr[2] = 0;

	this->_dirty = false;
}

void Gyro::init() {
	Log::info("Gyro", "init()");

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

	packetSize = mpu.dmpPacketSize;

	mpu.setDMPEnabled(true);
	mpu.resetFIFO();
}

void Gyro::update() {
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

	_dirty = true;
	Profiler::end();
}

float Gyro::getTemperature() {
	return mpu.getTemperature() / 340.00 + 36.53;
}


void Gyro::setAsZero() {
	pitchOffset = ypr[2];
	rollOffset = ypr[1];
	yawOffset = ypr[0];

	accelerationXOffset = aaReal.x;
	accelerationYOffset = aaReal.y;
	accelerationZOffset = aaReal.z;
}


float Gyro::getPitch() const {
	return getPitchRad() * 180 / M_PI;
}

float Gyro::getYaw() const {
	return getYawRad() * 180 / M_PI;
}


float Gyro::getRoll() const {
	return getRollRad() * 180 / M_PI;
}

float Gyro::getPitchRad() const {
	float pitch = ypr[2] - pitchOffset;
	return MathHelper::fixValue(pitch, -M_PI_2, M_PI_2);
}

float Gyro::getYawRad() const {
	float yaw = ypr[0] - yawOffset;
	return MathHelper::fixValue(yaw, 0, M_TWOPI);
}


float Gyro::getRollRad() const {
	float roll = ypr[1] - rollOffset;
	return MathHelper::fixValue(roll, -M_PI_2, M_PI_2);
}

float Gyro::getAccelerationX() const {
	return aaReal.x - accelerationXOffset;
}

float Gyro::getAccelerationY() const {
	return aaReal.y - accelerationYOffset;
}

float Gyro::getAccelerationZ() const {
	return aaReal.z - accelerationZOffset;
}