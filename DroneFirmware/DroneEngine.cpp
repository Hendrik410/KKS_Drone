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

	this->pitchPID = createPID(config->PitchPid, &pitchOutput);
	this->rollPID = createPID(config->RollPid, &rollOutput);
	this->yawPID = createPID(config->YawPid, &yawOutput);

	setMaxTilt(30);
	setMaxRotationSpeed(60);

	_state = StateReset;
	_stopReason = None;
	servos->setAllServos(config->ServoMin);
}

PID* DroneEngine::createPID(PIDSettings settings, double* output) {
	PID* pid = new PID(&pidInput, output, &pidSetpoint, settings.Kp, settings.Ki, settings.Kd, DIRECT);
	pid->SetMode(AUTOMATIC);
	pid->SetOutputLimits(-300, 300);
	return pid;
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

void DroneEngine::fly() {
	// entweder schon im Flying Zustand
	// oder nicht Armed
	if (_state != StateArmed)
		return;

	_state = StateFlying;
	Log::info("Engine", "Flying");
}

void DroneEngine::stop(StopReason reason) {
	if (_state == StateOTA)
		return;

	disarm();

	_stopReason = reason;
	_state = StateStopped;
	Log::info("Engine", "Stopped! reason: %d", reason);
}

void DroneEngine::clearStatus() {
	if (_state == StateStopped) {
		Log::info("Engine", "Resetting gyro, because clearStatus()");
		gyro->reset();
	}

	if (_state == StateReset || _state == StateStopped) {
		_state = StateIdle;
		Log::info("Engine", "Status cleared");
	}
}

bool DroneEngine::beginOTA() {
	if (_state != StateFlying) {
		disarm();

		_state = StateOTA;
		Log::info("Engine", "Now in OTA state");
		return true;
	}
	return false;
}

void DroneEngine::endOTA() {
	if (_state == StateOTA) {
		_state = StateIdle;
		Log::info("Engine", "Stopped OTA state");
	}
}

void DroneEngine::handle() {
	Profiler::begin("DroneEngine::handle()");
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

			handleInternal();
		}
	}
	Profiler::end();
}

void DroneEngine::handleInternal() {
	float values[4];

	if (config->EnableGyro) {
		calculatePID(pitchPID, -gyro->getPitch(), targetPitch);
		calculatePID(rollPID, gyro->getRoll(), targetRoll);
		calculatePID(yawPID, gyro->getGyroZ(), targetRotationalSpeed);
	}
	else {
		calculatePID(pitchPID, 0, targetPitch);
		calculatePID(rollPID, 0, targetRoll);
		calculatePID(yawPID, 0, targetRotationalSpeed);
	}

	float thrust = targetVerticalSpeed * config->ServoThrust;

	for (int i = 0; i < 4; i++)
		values[i] = MathHelper::clampValue(config->ServoHover + MathHelper::mixMotor(i, pitchOutput, rollOutput, yawOutput, thrust), config->ServoMin, config->ServoMax);

	servos->setServos(values[0], values[1], values[2], values[3]);
}

void DroneEngine::calculatePID(PID* pid, float input, float setpoint) {
	this->pidInput = input;
	this->pidSetpoint = setpoint;
	pid->Compute();
}


void DroneEngine::setRawServoValues(int fl, int fr, int bl, int br) const {
	if(_state == StateArmed)
		servos->setServos(fl, fr, bl, br);
}

void DroneEngine::setRawServoValues(int all) const {
	setRawServoValues(all, all, all, all);
}

void DroneEngine::heartbeat() {
	lastHeartbeat = millis();
}

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
	targetPitch = MathHelper::clampValue(pitch, -maxTilt, maxTilt);
	targetRoll = MathHelper::clampValue(roll, -maxTilt, maxTilt);
	targetRotationalSpeed = MathHelper::clampValue(rotationalSpeed, -maxRotationSpeed, maxRotationSpeed);
	targetVerticalSpeed = MathHelper::clampValue(verticalSpeed, -1, 20);

	// in den Fliegen Modus gehen
	fly();
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
