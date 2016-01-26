// 
// 
// 

#include "LinearDroneEngine.h"

LinearDroneEngine::LinearDroneEngine(Gyro* gyro, ServoManager* servos, Config* config) 
	: DroneEngine(gyro, servos, config)
{
	
}


void LinearDroneEngine::handleInternal() {
	if (millis() - lastYawTargetCalc >= 100) { // recalculate the yaw target 10 times a second to match rotary speed
		targetYaw += targetRotationSpeed / 10;
		lastYawTargetCalc = millis();
	}

	if (millis() - lastPhysicsCalc >= config->PhysicsCalcDelay) {
		float currentPitch = gyro->getPitch();
		float currentRoll = gyro->getRoll();
		float currentYaw = gyro->getYaw();

		float correctionPitch = targetPitch - currentPitch;
		float correctionRoll = targetRoll - currentRoll;
		float correctionYaw = 0; // MathHelper::angleDifference(targetYaw, currentYaw);

		frontLeftRatio = MathHelper::mixMotor(config, correctionPitch, correctionRoll, correctionYaw, targetVerticalSpeed, Position_Front | Position_Left, Counterclockwise);
		frontRightRatio = MathHelper::mixMotor(config, correctionPitch, correctionRoll, correctionYaw, targetVerticalSpeed, Position_Front | Position_Right, Clockwise);
		backLeftRatio = MathHelper::mixMotor(config, correctionPitch, correctionRoll, correctionYaw, targetVerticalSpeed, Position_Back | Position_Left, Clockwise);
		backRightRatio = MathHelper::mixMotor(config, correctionPitch, correctionRoll, correctionYaw, targetVerticalSpeed, Position_Back | Position_Right, Counterclockwise);

		servos->setRatio(frontLeftRatio, frontRightRatio, backLeftRatio, backRightRatio);

		lastPhysicsCalc = millis();
	}
}
