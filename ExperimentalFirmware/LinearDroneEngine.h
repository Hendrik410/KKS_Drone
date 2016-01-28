// LinearDroneEngine.h

#ifndef _LINEARDRONEENGINE_h
#define _LINEARDRONEENGINE_h

#include "arduino.h"
#include "DroneEngine.h"

class LinearDroneEngine : public DroneEngine
{
 protected:


 public:
	 LinearDroneEngine(Gyro* gyro, ServoManager* servos, Config* config);
	 void handle() override;
};

#endif

