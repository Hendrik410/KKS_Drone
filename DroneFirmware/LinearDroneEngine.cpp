// 
// 
// 

#include "LinearDroneEngine.h"

LinearDroneEngine::LinearDroneEngine(Gyro* gyro, ServoManager* servos, Config* config)
	: DroneEngine(gyro, servos, config)
{
	for (int i = 0; i < 4; i++)
		oldValues[i] = 0;
}


void LinearDroneEngine::handleInternal() {

	float target[3];
	target[0] = targetPitch;
	target[1] = targetRoll;
	target[2] = targetRotationalSpeed;


	float data[3];
	data[0] = 0; // gyro->getPitch();
	data[1] = 0; // gyro->getRoll();
	data[2] = gyro->getGyroZ();

	if (data[2] > config->RotationalCorrectionMax)
		data[2] = config->RotationalCorrectionMax;
	else if (data[2] < -config->RotationalCorrectionMax)
		data[2] = -config->RotationalCorrectionMax;

	newValues[0] = getTargetRatio(Position_Front | Position_Left, Counterclockwise, target);
	newValues[1] = getTargetRatio(Position_Front | Position_Right, Clockwise, target);
	newValues[2] = getTargetRatio(Position_Back | Position_Left, Clockwise, target);
	newValues[3] = getTargetRatio(Position_Back | Position_Right, Counterclockwise, target);

	correctionValues[0] = getTargetRatio(Position_Front | Position_Left, Counterclockwise, data);
	correctionValues[1] = getTargetRatio(Position_Front | Position_Right, Clockwise, data);
	correctionValues[2] = getTargetRatio(Position_Back | Position_Left, Clockwise, data);
	correctionValues[3] = getTargetRatio(Position_Back | Position_Right, Counterclockwise, data);

	float interpolationFactor = MathHelper::clampValue(config->InterpolationFactor, 0.01f, 1);
	for (int i = 0; i < 4; i++) {
		correctionValues[i] *= config->CorrectionFactor;

		newValues[i] += targetVerticalSpeed;
		newValues[i] -= correctionValues[i];

		float delta = newValues[i] - oldValues[i];
		if (abs(delta) < 0.05f || config->InterpolationFactor >= 1)
			oldValues[i] = newValues[i];
		else
			oldValues[i] += delta * interpolationFactor;
	}

	servos->setRatio(oldValues[0], oldValues[1], oldValues[2], oldValues[3]);

	frontLeftRatio = oldValues[0];
	frontRightRatio = oldValues[1];
	backLeftRatio = oldValues[2];
	backRightRatio = oldValues[3];

	frontLeftCorrection = correctionValues[0];
	frontRightCorrection = correctionValues[1];
	backLeftCorrection = correctionValues[2];
	backRightCorrection = correctionValues[3];
}

float LinearDroneEngine::getTargetRatio(MotorPosition position, MotorRotation rotation, float* values)
{
	return MathHelper::mixMotor(config, values[0], values[1], values[2], 0, position, rotation, config->EnableRatioSubtracting);
}
