#ifndef _MATHHELPER_h
#define _MATHHELPER_h

#include "arduino.h"
#include "Config.h"

class MathHelper
{
 public:
	 static float clampValue(float value, float min, float max);
	 static float fixValue(float value, float begin, float end);

	 static float angleDifference(float a, float b);
	 static float mixMotor(Config* config, int motorIndex, float pitch, float roll, float yaw, float thrust);
};

#endif

