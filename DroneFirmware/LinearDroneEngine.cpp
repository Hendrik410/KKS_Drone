// 
// 
// 

#include "LinearDroneEngine.h"

LinearDroneEngine::LinearDroneEngine(Gyro* gyro, ServoManager* servos, Config* config) 
	: DroneEngine(gyro, servos, config)
{
	
}


void LinearDroneEngine::handle() {
	if(_state == State_Flying) {
		if(millis() - lastMovementUpdate >= maxMovementUpdateInterval) {
			stop();
			return;
		}

		if(abs(gyro->getRoll()) > 35 || abs(gyro->getPitch()) > 35) {
			stop();
			return;
		}
	}

	if(millis() - lastYawTargetCalc >= 100) { // recalculate the yaw target 10 times a second to match rotary speed
		targetYaw += targetRotationSpeed / 10;
		lastYawTargetCalc = millis();
	}

	if(millis() - lastPhysicsCalc >= config->PhysicsCalcDelay && _state == State_Flying) {
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
