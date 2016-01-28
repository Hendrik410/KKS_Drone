// PidDroneEngine.h

#ifndef _PIDDRONEENGINE_h
#define _PIDDRONEENGINE_h

#include "arduino.h"
#include "DroneEngine.h"
#include "PID.h"

class PidDroneEngine : public DroneEngine
{
 protected:
	 PID* pidPitch;
	 PID* pidRoll;
	 PID* pidYaw;

	 double targetYaw;


	 double inputPitch, inputRoll, inputYaw;
	 double outputPitch, outputRoll, outputYaw;
 public:
	 PidDroneEngine(Gyro* gyro, ServoManager* servos, Config* config);
	 void handleInternal() override;
};

#endif

