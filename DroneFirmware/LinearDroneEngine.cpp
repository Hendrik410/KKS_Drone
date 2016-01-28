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
	data[0] = gyro->getPitch();
	data[1] = gyro->getRoll();
	data[2] = 0; // MathHelper::angleDifference(gyro->getYaw(), targetYaw);

	newValues[0] = getTargetRatio(Position_Front | Position_Left, Counterclockwise, target, data);
	newValues[1] = getTargetRatio(Position_Front | Position_Right, Clockwise, target, data);
	newValues[2] = getTargetRatio(Position_Back | Position_Left, Clockwise, target, data);
	newValues[3] = getTargetRatio(Position_Back | Position_Right, Counterclockwise, target, data);

	for (int i = 0; i < 4; i++)
		oldValues[i] += (newValues[i] - oldValues[i]) * config->InterpolationFactor;

	servos->setRatio(oldValues[0], oldValues[1], oldValues[2], oldValues[3]);

	frontLeftRatio = oldValues[0];
	frontRightRatio = oldValues[1];
	backLeftRatio = oldValues[2];
	backRightRatio = oldValues[3];
}

float LinearDroneEngine::getTargetRatio(MotorPosition position, MotorRotation rotation, float* target, float* data)
{
	float ratio = targetVerticalSpeed;

	ratio += MathHelper::mixMotor(config, target[0], target[1], target[2], 0, position, rotation);
	ratio -= config->CorrectionFactor * MathHelper::mixMotor(config, data[0], data[1], data[2], 0, position, rotation);
	return ratio;
}
