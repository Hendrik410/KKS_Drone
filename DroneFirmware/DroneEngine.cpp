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

	_state = StateReset;
	_stopReason = None;
	servos->setAllServos(config->ServoMin);
}


void DroneEngine::arm(){
	if(_state == StateIdle) {
		gyro->setAsZero();
		setTargetMovement(0, 0, 0);
		servos->setAllServos(config->ServoIdle);

		_state = StateArmed;

		Log::info("Engine", "Armed motors");
	}
}

void DroneEngine::disarm() {
	if (_state == StateArmed || _state == StateFlying) {
		servos->setAllServos(config->ServoMin);

		_state = StateIdle;
		Log::info("Engine", "Disarmed motors");
	}
}

void DroneEngine::stop(StopReason reason) {
	disarm();

	_stopReason = reason;
	_state = StateStopped;
	Log::info("Engine", "Stopped!");
}

void DroneEngine::clearStatus() {
	if (_state == StateReset || _state == StateStopped) {
		_state = StateIdle;
		Log::info("Engine", "Status cleared");
	}
}

void DroneEngine::handle() {
	if (_state == StateArmed || _state == StateFlying)
		blinkLED();

	if (_state == StateFlying) {
		if (millis() - lastMovementUpdate >= maxMovementUpdateInterval) {
			stop(NoData);
			return;
		}

		if (abs(gyro->getRoll()) > 35 || abs(gyro->getPitch()) > 35) {
			stop(InvalidGyro);
			return;
		}

		handleInternal();
	}
}

void DroneEngine::setRawServoValues(int fl, int fr, int bl, int br, bool forceWrite) const {
	if(_state == StateArmed)
		servos->setServos(fl, fr, bl, br, forceWrite);
}

void DroneEngine::setRawServoValues(int all, bool forceWrite) const {
	setRawServoValues(all, all, all, all, forceWrite);
}



/*################## Getter and Setter ####################*/

DroneState DroneEngine::state() const {
	return _state;
}

StopReason DroneEngine::getStopReason() const {
	return _stopReason;
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
	if (_state != StateArmed && _state != StateFlying)
		return;

	setTargetPitch(pitch);
	setTargetRoll(roll);
	setTargetRotarySpeed(yaw);
	_state = StateFlying;
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