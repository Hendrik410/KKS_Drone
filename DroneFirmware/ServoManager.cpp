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

void ServoManager::internalAttach() {
	frontLeft.attach(config->PinFrontLeft);
	frontRight.attach(config->PinFrontRight);
	backLeft.attach(config->PinBackLeft);
	backRight.attach(config->PinBackRight);
	attached = true;
	yield();
}

void ServoManager::attach() {
	if (attached)
		return;

	Log::info("Servo", "attach()");
	setAllServos(config->ServoMin);
	internalAttach();
}

void ServoManager::detach() {
	if (!attached)
		return;

	Log::info("Servo", "detach()");

	setAllServos(config->ServoMin);

	frontLeft.detach();
	frontRight.detach();
	backLeft.detach();
	backRight.detach();

	attached = false;
	yield();
}

void ServoManager::waitForDetach() {
	Profiler::begin("ServoManager::waitForDetach()");
	if (attached)
		detach();

	delayMicroseconds(REFRESH_INTERVAL);
	waitForDetach(frontLeft);
	waitForDetach(frontRight);
	waitForDetach(backLeft);
	waitForDetach(backRight);
	Profiler::end();
}

void ServoManager::waitForDetach(Servo servo) {
	int start = millis();
	while (millis() - start < 1000 && servo.attached()) 
		yield();
}

void ServoManager::handleTick() {
	if (!attached)
		return;

	int value = config->ServoMin;
	
	// alle 1000 Millisekunden f�r 150 Millisekunden kurz Motor drehen
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

void ServoManager::calibrate() {
	if (attached)
		return;

	frontLeft.writeMicroseconds(config->ServoMax);
	frontRight.writeMicroseconds(config->ServoMax);
	backLeft.writeMicroseconds(config->ServoMax);
	backRight.writeMicroseconds(config->ServoMax);
	internalAttach();

	delay(6000);

	setAllServos(config->ServoMin);
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