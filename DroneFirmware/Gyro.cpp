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
}

void Gyro::init(){
	Log::info("Gyro", "init()");

	mpu.reset();
	mpu.initialize();
	if(mpu.testConnection()) {
		Log::error("Gyro", "testConnection() failed!");
	}
	else 
		Log::info("Gyro", "testConnection() OK");

	Log::info("Gyro", "dmpInitialize()");
	
	int result = mpu.dmpInitialize();
	if(result) 
		Log::error("Gyro", "result: %d", result);

	Log::info("Gyro", "done with init");

	packetSize = mpu.dmpPacketSize;

	mpu.setDMPEnabled(true);
	mpu.resetFIFO();
}

void Gyro::update() {
	uint16_t fifoCount = mpu.getFIFOCount();
	if (!mpu.getIntDataReadyStatus() || fifoCount < packetSize)
		return;


	if (mpu.getIntFIFOBufferOverflowStatus() || fifoCount == 1024) {
		mpu.resetFIFO();

		Log::error("Gyro", "FIFO overflow!");
		return;
	}

	while (fifoCount >= packetSize) {
		mpu.getFIFOBytes(fifoBuffer, packetSize);
		fifoCount -= packetSize;
	}

	// Gyro Werte
	mpu.dmpGetQuaternion(&q, fifoBuffer);
	mpu.dmpGetGravity(&gravity, &q);
	mpu.dmpGetYawPitchRoll(ypr, &q, &gravity);

	// Beschleunigung
	mpu.dmpGetAccel(&aa, fifoBuffer);
	mpu.dmpGetLinearAccel(&aaReal, &aa, &gravity);

	_dirty = true;
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
	while (pitch < -M_PI_2)
		pitch += M_PI;
	while (pitch >= M_PI_2)
		pitch -= M_PI;
	return pitch;
}

float Gyro::getYawRad() const {
	float yaw = ypr[0] - yawOffset;
	while (yaw < 0)
		yaw += M_TWOPI;
	while (yaw >= M_TWOPI)
		yaw -= M_TWOPI;
	return yaw;
}


float Gyro::getRollRad() const {
	float roll = ypr[1] - rollOffset;
	while (roll < -M_PI_2)
		roll += M_PI;
	while (roll >= M_PI_2)
		roll -= M_PI;
	return roll;
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