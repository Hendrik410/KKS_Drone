// 
// 
// 

#include "Gyro.h"

Gyro::Gyro(Config* config) {
	this->config = config;

	this->_dirty = false;
}

void Gyro::setAsZero() {
	if (!hasMagnetometer()) {
		pitchOffset = pitch;
		rollOffset = roll;
	}
	if (!hasCompass()) {
		yawOffset = yaw;
	}

	accelerationXOffset = accX;
	accelerationYOffset = accY;
	accelerationZOffset = accZ;
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
	float pitch = this->pitch - pitchOffset;
	return MathHelper::fixValue(pitch, -M_PI_2, M_PI_2);
}

float Gyro::getYawRad() const {
	float yaw = this->yaw - yawOffset;
	return MathHelper::fixValue(yaw, 0, M_TWOPI);
}


float Gyro::getRollRad() const {
	float roll = this->roll - rollOffset;
	return MathHelper::fixValue(roll, -M_PI_2, M_PI_2);
}

float Gyro::getGyroX() const {
	return gyroX;
}

float Gyro::getGyroY() const {
	return gyroY;
}

float Gyro::getGyroZ() const {
	return gyroZ;
}

float Gyro::getAccelerationX() const {
	return accX - accelerationXOffset;
}

float Gyro::getAccelerationY() const {
	return accY - accelerationYOffset;
}

float Gyro::getAccelerationZ() const {
	return accZ - accelerationZOffset;
}