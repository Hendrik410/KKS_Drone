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

	_MPU6050.reset();
	_MPU6050.initialize();
	if(!_MPU6050.testConnection()) {
		Log::error("Gyro", "testConnection() failed!");
	}
	else 
		Log::info("Gyro", "testConnection() OK");

	Log::info("Gyro", "dmpInitialize()");
	
	int result = _MPU6050.dmpInitialize();
	if(result) 
		Log::error("Gyro", "result: %d", result);

	Log::info("Gyro", "done with init");

	MPU6050_Packet_Size = _MPU6050.dmpPacketSize;

	// supply your own gyro offsets here, scaled for min sensitivity
	_MPU6050.setXGyroOffset(220);
	_MPU6050.setYGyroOffset(76);
	_MPU6050.setZGyroOffset(-85);
	_MPU6050.setZAccelOffset(1788); // 1688 factory default for my test chip

	_MPU6050.setDMPEnabled(true);
	_MPU6050.resetFIFO();
}

void Gyro::update() {

	int fifoCount = _MPU6050.getFIFOCount();

	if(_MPU6050.getIntFIFOBufferOverflowStatus() || fifoCount == 1024) {
		_MPU6050.resetFIFO();
		
		Serial.println("FIFO overflow!");
	}

	if(!_MPU6050.getIntDataReadyStatus())
		return;

	while(fifoCount < MPU6050_Packet_Size) fifoCount = _MPU6050.getFIFOCount();

	while(fifoCount > MPU6050_Packet_Size) {
		_MPU6050.getFIFOBytes(MPU6050_FIFO_Buffer, MPU6050_Packet_Size);

		// get Euler angles in radiant
		_MPU6050.dmpGetQuaternion(&q, MPU6050_FIFO_Buffer);
		_MPU6050.dmpGetGravity(&gravity, &q);
		_MPU6050.dmpGetYawPitchRoll(ypr, &q, &gravity);

		// Beschleunigung
		_MPU6050.dmpGetAccel(&aa, MPU6050_FIFO_Buffer);
		_MPU6050.dmpGetLinearAccel(&aaReal, &aa, &gravity);

		_dirty = true;

		fifoCount = _MPU6050.getFIFOCount();
	}
}

float Gyro::getTemperature() {
	return _MPU6050.getTemperature() / 340.00 + 36.53;
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