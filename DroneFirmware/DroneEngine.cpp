// 
// 
// 

#include "DroneEngine.h"

DroneEngine::DroneEngine(Gyro* gyro, ServoManager* servos, Config* config) {
	this->gyro = gyro;
	this->servos = servos;
	this->config = config;
	this->lastPhysicsCalc = 0;

	this->frontLeftRatio = 0;
	this->frontRightRatio = 0;
	this->backLeftRatio = 0;
	this->backRightRatio = 0;

	setMaxTilt(30);
	setMaxRotationSpeed(60);
	_state = State_Idle;
	servos->setAllServos(config->ServoMin);
}


void DroneEngine::arm(){
	if(_state == State_Idle) {
		gyro->setAsZero();
		setTargetMovement(0, 0, 0);
		servos->setAllServos(config->ServoIdle);

		_state = State_Armed;

		Log::debug("Engine", "Armed Motors");
	}
}

void DroneEngine::disarm() {
	if(_state == State_Armed) {
		servos->setAllServos(config->ServoMin);

		_state = State_Idle;

		Log::debug("Engine", "Disarmed Motors");
	}
}

void DroneEngine::stop() {
	if(_state == State_Flying) {
		servos->setAllServos(config->ServoMin);

		_state = State_Idle;
	} else if(_state == State_Armed) {
		disarm();
	}
}

void DroneEngine::setRawServoValues(int fl, int fr, int bl, int br, bool forceWrite) const {
	if(_state == State_Armed)
		servos->setServos(fl, fr, bl, br, forceWrite);
}

void DroneEngine::setRawServoValues(int all, bool forceWrite) const {
	setRawServoValues(all, all, all, all, forceWrite);
}



/*################## Getter and Setter ####################*/

DroneState DroneEngine::state() const {
	return _state;
}


void DroneEngine::setMaxTilt(float tilt) {
	maxTilt = abs(tilt);
}

void DroneEngine::setMaxRotationSpeed(float rotaionSpeed) {
	maxRotationSpeed = abs(rotaionSpeed);
}

float DroneEngine::getMaxTilt() const {
	return maxTilt;
}

float DroneEngine::getMaxRotationSpeed() const {
	return maxRotationSpeed;
}


void DroneEngine::setTargetMovement(float pitch, float roll, float yaw) {
	if (_state == State_Idle)
		return;

	setTargetPitch(pitch);
	setTargetRoll(roll);
	setTargetRotarySpeed(yaw);
	_state = State_Flying;
	lastMovementUpdate = millis();
}

void DroneEngine::setTargetPitch(float pitch) {
	targetPitch = MathHelper::clampValue(pitch, -maxTilt, maxTilt);
	lastMovementUpdate = millis();
}

void DroneEngine::setTargetRoll(float roll) {
	targetRoll = MathHelper::clampValue(roll, -maxTilt, maxTilt);
	lastMovementUpdate = millis();
}

void DroneEngine::setTargetRotarySpeed(float yaw) {
	targetRotationSpeed = MathHelper::clampValue(yaw, -maxRotationSpeed, maxRotationSpeed);
	lastMovementUpdate = millis();
}

void DroneEngine::setTargetVerticalSpeed(float vertical) {
	targetVerticalSpeed = MathHelper::clampValue(vertical, -1, 1);
	lastMovementUpdate = millis();
}

float DroneEngine::getTargetPitch() const {
	return targetPitch;
}

float DroneEngine::getTargetRoll() const {
	return targetRoll;
}

float DroneEngine::getTargetYaw() const {
	return targetYaw;
}

float DroneEngine::getTargetRotarySpeed() const {
	return targetRotationSpeed;
}

float DroneEngine::getTargetVerticalSpeed() const {
	return targetVerticalSpeed;
}

float DroneEngine::getFrontLeftRatio() const {
	return frontLeftRatio;
}

float DroneEngine::getFrontRightRatio() const {
	return frontRightRatio;
}

float DroneEngine::getBackLeftRatio() const {
	return backLeftRatio;
}

float DroneEngine::getBackRightRatio() const {
	return backRightRatio;
}