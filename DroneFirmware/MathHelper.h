// MathHelper.h

#ifndef _MATHHELPER_h
#define _MATHHELPER_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

#include "MotorEnums.h"
#include "Config.h"

class MathHelper
{
 public:
	 static float clampValue(float value, float min, float max);
	 static float mapRatio(float ratio, float min, float max, float center);
	 static float fixValue(float value, float begin, float end);

	 static float angleDifference(float a, float b);
	 static float mixMotor(Config* config,float pitchDelta, float rollDelta, float yawDelta, float verticalRatio, MotorPosition position, MotorRotation rotation);
};

#endif

