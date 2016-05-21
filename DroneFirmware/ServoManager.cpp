// 
// 
// 

#include "ServoManager.h"

ServoManager::ServoManager(Config* config) {
	this->config = config;

	this->attached = false;
	servoFLValue = config->ServoMin;
	servoFRValue = config->ServoMin;
	servoBLValue = config->ServoMin;
	servoBRValue = config->ServoMin;
	this->_dirty = true;
}


void ServoManager::attach() {
	if (attached)
		return;

	setAllServos(config->ServoMin);

	frontLeft.attach(config->PinFrontLeft);
	frontRight.attach(config->PinFrontRight);
	backLeft.attach(config->PinBackLeft);
	backRight.attach(config->PinBackRight);

	attached = true;
}

void ServoManager::detach() {
	if (!attached)
		return;

	frontLeft.detach();
	frontRight.detach();
	backLeft.detach();
	backRight.detach();

	attached = false;
}

void ServoManager::handleTick() {
	if (!attached)
		return;

	int value = config->ServoMin;
	
	// alle 1000 Millisekunden für 150 Millisekunden kurz Motor drehen
	if (millis() % 1000 < 150)
		value = config->ServoIdle;

	if (servoFLValue == 1)
		frontLeft.writeMicroseconds(value);
	if (servoFRValue == 1)
		frontRight.writeMicroseconds(value);
	if (servoBLValue == 1)
		backLeft.writeMicroseconds(value);
	if (servoBRValue == 1)
		backRight.writeMicroseconds(value);
}

int ServoManager::getValue(int value) {
	if (value == 1)
		return value;
	value = MathHelper::clampValue(value, config->ServoMin, config->ServoMax);
	if (value > config->SafeServoValue)
		return config->SafeServoValue;
	return value;
}

void ServoManager::setServos(int fl, int fr, int bl, int br) {
	servoFLValue = getValue(fl);
	servoFRValue = getValue(fr);
	servoBLValue = getValue(bl);
	servoBRValue = getValue(br);

	_dirty = true;

	if (!attached)
		return;

	if (servoFLValue != 1)
		frontLeft.writeMicroseconds(servoFLValue);
	if (servoFRValue != 1)
		frontRight.writeMicroseconds(servoFRValue);
	if (servoBLValue != 1)
		backLeft.writeMicroseconds(servoBLValue);
	if (servoBRValue != 1)
		backRight.writeMicroseconds(servoBRValue);
}


void ServoManager::setAllServos(int val) {
	setServos(val, val, val, val);
}