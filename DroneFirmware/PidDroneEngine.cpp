// 
// 
// 

#include "PidDroneEngine.h"

PidDroneEngine::PidDroneEngine(Gyro* gyro, ServoManager* servos, Config* config)
	: DroneEngine(gyro, servos, config) {
	
	inputPitch = 0;
	inputRoll = 0;
	inputYaw = 0;

	pidPitch = new PID(&inputPitch, &outputPitch, &targetPitch, 
					   config->PitchPidSettings.Kp, 
					   config->PitchPidSettings.Ki, 
					   config->PitchPidSettings.Kd, DIRECT);

	pidRoll = new PID(&inputRoll, &outputRoll, &targetRoll,
					  config->RollPidSettings.Kp,
					  config->RollPidSettings.Ki,
					  config->RollPidSettings.Kd, DIRECT);

	pidYaw = new PID(&inputYaw, &outputYaw, &targetYaw,
					   config->YawPidSettings.Kp,
					   config->YawPidSettings.Ki,
					   config->YawPidSettings.Kd, DIRECT);

}


void PidDroneEngine::handle() {
	inputPitch = gyro->getPitch();
	inputRoll = gyro->getRoll();
	inputYaw = gyro->getYaw();

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
		pidPitch->Compute();
		pidRoll->Compute();
		pidYaw->Compute();

		frontLeftRatio = MathHelper::mixMotor(config, outputPitch, outputRoll, outputYaw, targetVerticalSpeed, Position_Front | Position_Left, Counterclockwise);
		frontRightRatio = MathHelper::mixMotor(config, outputPitch, outputRoll, outputYaw, targetVerticalSpeed, Position_Front | Position_Right, Clockwise);
		backLeftRatio = MathHelper::mixMotor(config, outputPitch, outputRoll, outputYaw, targetVerticalSpeed, Position_Back | Position_Left, Clockwise);
		backRightRatio = MathHelper::mixMotor(config, outputPitch, outputRoll, outputYaw, targetVerticalSpeed, Position_Back | Position_Right, Counterclockwise);

		servos->setRatio(frontLeftRatio, frontRightRatio, backLeftRatio, backRightRatio);

		lastPhysicsCalc = millis();
	}
}


