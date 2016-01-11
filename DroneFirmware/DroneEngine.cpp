// 
// 
// 

#include "DroneEngine.h"

DroneEngine::DroneEngine(Gyro* gyro, ServoManager* servos, bool debug_output) {
	this->gyro = gyro;
	this->servos = servos;
	this->debug_output = debug_output;
	this->lastPhysicsCalc = 0;

	setMaxTilt(30);
	setMaxRotationSpeed(60);
}


void DroneEngine::arm(){
	gyro->setAsZero();
	setTargetMovement(0, 0, 0);
	servos->armMotors();
}

void DroneEngine::disarm() {
	servos->disarmMotors();
}

void DroneEngine::stop() {
	servos->disarmMotors();
}

void DroneEngine::handle() {
	if(millis() - lastYawTargetCalc >= 100) { // recalculate the yaw target 10 times a second to match rotary speed
		targetYaw += targetRotationSpeed / 10;
	}

	if(millis() - lastPhysicsCalc >= PHYSICS_CALC_DELAY_MS && _isArmed) {
		float currentPitch = gyro->getPitch();
		float currentRoll = gyro->getRoll();
		float currentYaw = gyro->getYaw();

		float correctionPitch = currentPitch - targetPitch;
		float correctionRoll = currentRoll - targetRoll;
		float correctionYaw = currentYaw - targetYaw;
		
		float ratioFL = MathHelper::mixMotor(correctionPitch, correctionRoll, correctionYaw, targetVerticalSpeed, Position_Front | Position_Left, Counterclockwise);
		float ratioFR = MathHelper::mixMotor(correctionPitch, correctionRoll, correctionYaw, targetVerticalSpeed, Position_Front | Position_Right, Clockwise);
		float ratioBL = MathHelper::mixMotor(correctionPitch, correctionRoll, correctionYaw, targetVerticalSpeed, Position_Back | Position_Left, Counterclockwise);
		float ratioBR = MathHelper::mixMotor(correctionPitch, correctionRoll, correctionYaw, targetVerticalSpeed, Position_Back | Position_Right, Clockwise);

		servos->setRatio(ratioFL, ratioFR, ratioBL, ratioBR);

		lastPhysicsCalc = millis();
	}
}

/*################## Getter and Setter ####################*/

bool DroneEngine::isArmed() const {
	return _isArmed;
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
	setTargetPitch(pitch);
	setTargetRoll(roll);
	setTargetRotarySpeed(yaw);
}

void DroneEngine::setTargetPitch(float pitch) {
	targetPitch = MathHelper::clampValue(pitch, -maxTilt, maxTilt);
}

void DroneEngine::setTargetRoll(float roll) {
	targetRoll = MathHelper::clampValue(roll, -maxTilt, maxTilt);
}

void DroneEngine::setTargetRotarySpeed(float yaw) {
	targetRotationSpeed = MathHelper::clampValue(yaw, -maxRotationSpeed, maxRotationSpeed);
}

void DroneEngine::setTargetVerticalSpeed(float vertical) {
	targetVerticalSpeed = MathHelper::clampValue(vertical, -1, 1);
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

