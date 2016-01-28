// 
// 
// 

#include "PidDroneEngine.h"

PidDroneEngine::PidDroneEngine(Gyro* gyro, ServoManager* servos, Config* config)
	: DroneEngine(gyro, servos, config) {
	
	inputPitch = 0;
	inputRoll = 0;
	inputYaw = 0;

	lastYaw = gyro->getYaw();

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

	pidPitch->SetMode(AUTOMATIC);
	pidPitch->SetOutputLimits(-40, 40);
	pidRoll->SetMode(AUTOMATIC);
	pidRoll->SetOutputLimits(-40, 40);
	pidYaw->SetMode(AUTOMATIC);
	pidYaw->SetOutputLimits(-40, 40);
}


void PidDroneEngine::handleInternal() {
	inputPitch = gyro->getPitch();
	inputRoll = gyro->getRoll();
	inputYaw = gyro->getYaw();

	pidRoll->SetTunings(config->RollPidSettings.Kp,
		config->RollPidSettings.Ki,
		config->RollPidSettings.Kd);
	pidPitch->SetTunings(config->PitchPidSettings.Kp,
		config->PitchPidSettings.Ki,
		config->PitchPidSettings.Kd);

	pidYaw->SetTunings(config->YawPidSettings.Kp,
		config->YawPidSettings.Ki,
		config->YawPidSettings.Kd);

	pidPitch->Compute();
	pidRoll->Compute();
	pidYaw->Compute();

	frontLeftRatio = MathHelper::mixMotor(config, outputPitch, outputRoll, outputYaw, targetVerticalSpeed, Position_Front | Position_Left, Counterclockwise);
	frontRightRatio = MathHelper::mixMotor(config, outputPitch, outputRoll, outputYaw, targetVerticalSpeed, Position_Front | Position_Right, Clockwise);
	backLeftRatio = MathHelper::mixMotor(config, outputPitch, outputRoll, outputYaw, targetVerticalSpeed, Position_Back | Position_Left, Clockwise);
	backRightRatio = MathHelper::mixMotor(config, outputPitch, outputRoll, outputYaw, targetVerticalSpeed, Position_Back | Position_Right, Counterclockwise);

	servos->setRatio(frontLeftRatio, frontRightRatio, backLeftRatio, backRightRatio);
}


