// 
// 
// 

#include "ServoManager.h"

ServoManager::ServoManager(Config* config) {
	this->config = config;

	servoFLValue = config->ServoMin;
	servoFRValue = config->ServoMin;
	servoBLValue = config->ServoMin;
	servoBRValue = config->ServoMin;
	this->_dirty = true;
}


void ServoManager::init(int pinFL, int pinFR, int pinBL, int pinBR) {
	frontLeft.attach(pinFL);
	frontRight.attach(pinFR);
	backLeft.attach(pinBL);
	backRight.attach(pinBR);

	setAllServos(config->ServoMin);
}

void ServoManager::handleTick() {
	int value = config->ServoMin;
	
	// für 50 Millisekunden alle 500 Millisekunden ticken
	if (millis() % 500 < 50)
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

	if (servoFLValue != 1)
		frontLeft.writeMicroseconds(servoFLValue);
	if (servoFRValue != 1)
		frontRight.writeMicroseconds(servoFRValue);
	if (servoBLValue != 1)
		backLeft.writeMicroseconds(servoBLValue);
	if (servoBRValue != 1)
		backRight.writeMicroseconds(servoBRValue);

	_dirty = true;
}


void ServoManager::setAllServos(int val) {
	setServos(val, val, val, val);
}

void ServoManager::setRatio(float fl, float fr, float bl, float br) {
	int targetFL = MathHelper::mapRatio(fl, config->ServoMin, config->ServoMax, config->ServoHover);
	int targetFR = MathHelper::mapRatio(fr, config->ServoMin, config->ServoMax, config->ServoHover);
	int targetBL = MathHelper::mapRatio(bl, config->ServoMin, config->ServoMax, config->ServoHover);
	int targetBR = MathHelper::mapRatio(br, config->ServoMin, config->ServoMax, config->ServoHover);

	setServos(targetFL, targetFR, targetBL, targetBR);
}

void ServoManager::setRationAll(float ratio) {
	setRatio(ratio, ratio, ratio, ratio);
}
