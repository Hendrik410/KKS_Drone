// 
// 
// 

#include "DroneEngine.h"

DroneEngine::DroneEngine(Gyro* gyro, ServoManager* servos, Config* config) {
	this->gyro = gyro;
	this->servos = servos;
	this->config = config;

	this->lastHeartbeat = 0;
	this->lastMovementUpdate = 0;
	this->lastPhysicsCalc = 0;

	this->frontLeftRatio = 0;
	this->frontRightRatio = 0;
	this->backLeftRatio = 0;
	this->backRightRatio = 0;

	this->frontLeftCorrection = 0;
	this->frontRightCorrection = 0;
	this->backLeftCorrection = 0;
	this->backRightCorrection = 0;

	setMaxTilt(30);
	setMaxRotationSpeed(60);

	_state = StateReset;
	_stopReason = None;
	servos->setAllServos(config->ServoMin);
}


void DroneEngine::arm() {
	if (_state == StateIdle) {
		gyro->setAsZero();
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
	Log::info("Engine", "Stopped! reason: %d", reason);
}

void DroneEngine::clearStatus() {
	if (_state == StateStopped) {
		Log::info("Engine", "Reseting gyro, because clearStatus()");
		gyro->init();
	}

	if (_state == StateReset || _state == StateStopped) {
		_state = StateIdle;
		Log::info("Engine", "Status cleared");
	}
}

void DroneEngine::handle() {
	if (_state == StateArmed || _state == StateFlying) {
		blinkLED();


		if (millis() - lastHeartbeat >= config->MaximumNetworkTimeout) {
			stop(NoPing);
			return;
		}

		if (abs(gyro->getRoll()) > config->SafeRoll || abs(gyro->getPitch()) > config->SafePitch) {
			stop(InvalidGyro);
			return;
		}

		if (_state == StateFlying) {
			if (millis() - lastMovementUpdate >= config->MaximumNetworkTimeout) {
				stop(NoData);
				return;
			}

			if (millis() - lastPhysicsCalc >= config->PhysicsCalculationInterval) {
				handleInternal();
				lastPhysicsCalc = millis();
			}
		}
	}
}

void DroneEngine::setRawServoValues(int fl, int fr, int bl, int br, bool forceWrite) const {
	if(_state == StateArmed)
		servos->setServos(fl, fr, bl, br, forceWrite);
}

void DroneEngine::setRawServoValues(int all, bool forceWrite) const {
	setRawServoValues(all, all, all, all, forceWrite);
}

void DroneEngine::heartbeat() {
	lastHeartbeat = millis();
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


void DroneEngine::setTargetMovement(float pitch, float roll, float rotationalSpeed, float verticalSpeed) {
	if (_state != StateArmed && _state != StateFlying)
		return;

	// Werte in richtigen Bereich bringen und setzen
	targetPitch = MathHelper::fixValue(MathHelper::clampValue(pitch, -maxTilt, maxTilt), -M_PI_2, M_PI_2);
	targetRoll = MathHelper::fixValue(MathHelper::clampValue(roll, -maxTilt, maxTilt), -M_PI_2, M_PI_2);
	targetRotationalSpeed = MathHelper::clampValue(rotationalSpeed, -maxRotationSpeed, maxRotationSpeed);
	targetVerticalSpeed = MathHelper::clampValue(verticalSpeed, -1, 1);

	// in den Fliegen Modus gehen
	_state = StateFlying;
	lastMovementUpdate = millis();
}

float DroneEngine::getTargetPitch() const {
	return targetPitch;
}

float DroneEngine::getTargetRoll() const {
	return targetRoll;
}

float DroneEngine::getTargetRotationalSpeed() const {
	return targetRotationalSpeed;
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

float DroneEngine::getFrontLeftCorrection() const {
	return frontLeftCorrection;
}

float DroneEngine::getFrontRightCorrection() const {
	return frontRightCorrection;
}

float DroneEngine::getBackLeftCorrection() const {
	return backLeftCorrection;
}

float DroneEngine::getBackRightCorrection() const {
	return backRightCorrection;
}