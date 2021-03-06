// 
// 
// 

#include "DroneEngine.h"

DroneEngine::DroneEngine(Gyro* gyro, ServoManager* servos, Config* config) {
	this->gyro = gyro;
	this->servos = servos;
	this->config = config;

	this->armTime = 0;
	this->lastHeartbeat = 0;
	this->lastMovementUpdate = 0;

	this->pitchPID = NULL;
	this->rollPID = NULL;
	this->yawPID = NULL;

	createPID();

	setMaxTilt(30);
	setMaxRotationSpeed(60);

	_state = StateReset;
	_stopReason = None;
	servos->setAllServos(config->ServoMin);
}

void DroneEngine::createPID() {
	if (pitchPID)
		delete pitchPID;
	if (rollPID)
		delete rollPID;
	if (yawPID)
		delete yawPID;

	pitchPID = createPID(config->PitchPid, &pitchOutput);
	rollPID = createPID(config->RollPid, &rollOutput);
	yawPID = createPID(config->YawPid, &yawOutput);
}

PID* DroneEngine::createPID(PIDSettings settings, double* output) {
	PID* pid = new PID(&pidInput, output, &pidSetpoint, settings.Kp, settings.Ki, settings.Kd, DIRECT);
	pid->SetMode(AUTOMATIC);
	pid->SetSampleTime(CYCLE_PID);
	pid->SetOutputLimits(-300, 300);
	return pid;
}

bool DroneEngine::isGyroSafe() {
	return abs(gyro->getRoll()) <= config->SafeRoll && abs(gyro->getPitch()) <= config->SafePitch;
}

void DroneEngine::arm() {
	if (_state == StateIdle) {
		if (!isGyroSafe()) {
			blinkLED(5, 300);
			Log::info("Engine", "Can not arm motors, gyro is not safe");
			return;
		}
		if (config->OnlyArmWhenStill && (gyro->isMoving() || !gyro->isFlat())) {
			blinkLED(5, 300);
			Log::info("Engine", "Can not arm motors, not still");
			return;
		}

		armTime = millis();
		_state = StateArmed;
		blinkLED(3, 600);

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

	if (thrust > config->MaxThrustForFlying)
		return;

	pitchOutput = 0;
	rollOutput = 0;
	yawOutput = 0;

	createPID();

	_state = StateFlying;
	blinkLED(3, 550);
	Log::info("Engine", "Flying");
}

void DroneEngine::stop(StopReason reason) {
	if (_state == StateOTA)
		return;

	disarm();
	blinkLED(5, 250);

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

	if (_state == StateArmed && armTime > 0 && millis() - armTime > 250) {
		setRawServoValues(config->ServoIdle);
		armTime = 0;
	}

	if (_state == StateArmed || _state == StateFlying) {
		blinkLED(1, 800);

		if (millis() - lastHeartbeat >= config->MaximumNetworkTimeout) {
			stop(NoPing);
			Profiler::end();
			return;
		}

		if (!isGyroSafe()) {
			stop(InvalidGyro);
			Profiler::end();
			return;
		}

		if (_state == StateFlying) {
			if (millis() - lastMovementUpdate >= config->MaximumNetworkTimeout) {
				stop(NoData);
				Profiler::end();
				return;
			}

			handleInternal();
		}
	}
	Profiler::end();
}

void DroneEngine::handleInternal() {
	int values[4];

	if (config->EnableStabilization) {
		if (config->EngineUseGyro) {
			calculatePID(rollPID, gyro->getGyroX(), targetRoll);
			calculatePID(pitchPID, gyro->getGyroY(), targetPitch);
		}
		else {
			calculatePID(pitchPID, gyro->getPitch(), targetPitch);
			calculatePID(rollPID, gyro->getRoll(), targetRoll);
		}
		calculatePID(yawPID, gyro->getGyroZ(), targetRotationalSpeed);
	}
	else {
		calculatePID(pitchPID, -targetPitch, 0);
		calculatePID(rollPID, -targetRoll, 0);
		calculatePID(yawPID, -targetRotationalSpeed, 0);
	}

	uint16_t minServo = config->ServoMin;
	if (config->KeepMotorsOn)
		minServo = config->ServoIdle;

	for (int i = 0; i < 4; i++)
		values[i] = (int)MathHelper::clampValue(minServo + thrust + MathHelper::mixMotor(config, i, pitchOutput, rollOutput, yawOutput), config->ServoMin, config->ServoMax);

	servos->setServos(values[0], values[1], values[2], values[3]);

	if (memcmp(values, lastValues, sizeof(lastValues)) != 0)
	{
		Profiler::restart("DroneEngine::data()");
		memcpy(lastValues, values, sizeof(lastValues));
	}
}

void DroneEngine::updateTunings() {
	pitchPID->SetTunings(config->PitchPid.Kp, config->PitchPid.Ki, config->PitchPid.Kd);
	rollPID->SetTunings(config->RollPid.Kp, config->RollPid.Ki, config->RollPid.Kd);
	yawPID->SetTunings(config->YawPid.Kp, config->YawPid.Ki, config->YawPid.Kd);
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


void DroneEngine::setTargetMovement(float pitch, float roll, float rotationalSpeed, int thrust) {
	if (_state != StateArmed && _state != StateFlying)
		return;

	// Werte in richtigen Bereich bringen und setzen
	this->targetPitch = MathHelper::clampValue(pitch, -maxTilt, maxTilt);
	this->targetRoll = MathHelper::clampValue(roll, -maxTilt, maxTilt);
	this->targetRotationalSpeed = MathHelper::clampValue(rotationalSpeed, -maxRotationSpeed, maxRotationSpeed);
	this->thrust = MathHelper::clampValue(thrust, 0, config->ServoMax - config->ServoMin);

	// in den Fliegen Modus gehen
	fly();
	lastMovementUpdate = millis();

	Profiler::restart("DroneEngine::input()");
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

int DroneEngine::getThrust() const {
	return thrust;
}

float DroneEngine::getPitchOutput() const {
	return pitchOutput;
}

float DroneEngine::getRollOutput() const {
	return rollOutput;
}

float DroneEngine::getYawOutput() const {
	return yawOutput;
}