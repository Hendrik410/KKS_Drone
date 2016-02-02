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

	setAllServos(config->ServoMin, true);
}

void ServoManager::setServos(int fl, int fr, int bl, int br, bool forceWrite) {
	servoFLValue = forceWrite ? fl : MathHelper::clampValue(fl, config->ServoMin, config->ServoMax);
	servoFRValue = forceWrite ? fr : MathHelper::clampValue(fr, config->ServoMin, config->ServoMax);
	servoBLValue = forceWrite ? bl : MathHelper::clampValue(bl, config->ServoMin, config->ServoMax);
	servoBRValue = forceWrite ? br : MathHelper::clampValue(br, config->ServoMin, config->ServoMax);

	if (servoFLValue > config->SafeServoValue)
		servoFLValue = config->SafeServoValue;

	if (servoFRValue > config->SafeServoValue)
		servoFRValue = config->SafeServoValue;

	if (servoBLValue > config->SafeServoValue)
		servoBLValue = config->SafeServoValue;

	if (servoBRValue > config->SafeServoValue)
		servoBRValue = config->SafeServoValue;

	frontLeft.writeMicroseconds(servoFLValue);
	frontRight.writeMicroseconds(servoFRValue);
	backLeft.writeMicroseconds(servoBLValue);
	backRight.writeMicroseconds(servoBRValue);

	_dirty = true;
}


void ServoManager::setAllServos(int val, bool forceWrite) {
	setServos(val, val, val, val, forceWrite);
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
