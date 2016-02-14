// LinearDroneEngine.h

#ifndef _LINEARDRONEENGINE_h
#define _LINEARDRONEENGINE_h

#include "arduino.h"
#include "DroneEngine.h"

class LinearDroneEngine : public DroneEngine
{
protected:
	float oldValues[4];
	float newValues[4];
	float correctionValues[4];

	float getTargetRatio(MotorPosition position, MotorRotation rotation, float* values);

public:
	LinearDroneEngine(Gyro* gyro, ServoManager* servos, Config* config);
	void handleInternal() override;
};

#endif

