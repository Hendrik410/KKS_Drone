// 
// 
// 

#include "ServoManager.h"

ServoManager::ServoManager(int offValue, int idleValue,int hoverValue, int maxValue, bool debug_output) {
	this->servoOffValue = offValue;
	this->servoIdleValue = idleValue;
	this->servoHoverValue = hoverValue;
	this->servoMaxValue = maxValue;
	this->debug_output = debug_output;

	servoFLValue = offValue;
	servoFRValue = offValue;
	servoBLValue = offValue;
	servoBRValue = offValue;
}


void ServoManager::init(int pinFL, int pinFR, int pinBL, int pinBR) {
	frontLeft.attach(pinFL);
	frontRight.attach(pinFR);
	backLeft.attach(pinBL);
	backRight.attach(pinBR);

	setAllServos(servoOffValue);
}

void ServoManager::setServos(int fl, int fr, int bl, int br, bool forceWrite) {
	servoFLValue = forceWrite ? fl : MathHelper::clampValue(fl, servoOffValue, servoMaxValue);
	servoFRValue = forceWrite ? fr : MathHelper::clampValue(fr, servoOffValue, servoMaxValue);
	servoBLValue = forceWrite ? bl : MathHelper::clampValue(bl, servoOffValue, servoMaxValue);
	servoBRValue = forceWrite ? br : MathHelper::clampValue(br, servoOffValue, servoMaxValue);

	frontLeft.writeMicroseconds(servoFLValue);
	frontRight.writeMicroseconds(servoFRValue);
	backLeft.writeMicroseconds(servoBLValue);
	backRight.writeMicroseconds(servoBRValue);

	if(debug_output) {
		Serial.print("$ Set Servos to: ");
		Serial.print(servoFLValue);
		Serial.print(", ");
		Serial.print(servoFRValue);
		Serial.print(", ");
		Serial.print(servoBLValue);
		Serial.print(", ");
		Serial.println(servoBRValue);
	}
}


void ServoManager::setAllServos(int val, bool forceWrite) {
	setServos(val, val, val, val, forceWrite);
}

void ServoManager::setRatio(float fl, float fr, float bl, float br) {
	int targetFL = MathHelper::mapRatio(fl, servoOffValue, servoMaxValue, servoIdleValue);
	int targetFR = MathHelper::mapRatio(fr, servoOffValue, servoMaxValue, servoIdleValue);
	int targetBL = MathHelper::mapRatio(bl, servoOffValue, servoMaxValue, servoIdleValue);
	int targetBR = MathHelper::mapRatio(br, servoOffValue, servoMaxValue, servoIdleValue);

	setServos(targetFL, targetFR, targetBL, targetBR);
}

void ServoManager::setRationAll(float ratio) {
	setRatio(ratio, ratio, ratio, ratio);
}


void ServoManager::armMotors() {
	if(!isArmed()) {
		setAllServos(servoIdleValue);
		_isArmed = true;
	}
}

void ServoManager::disarmMotors() {
	if(isArmed()) {
		setAllServos(servoOffValue);
		_isArmed = false;
	}
}