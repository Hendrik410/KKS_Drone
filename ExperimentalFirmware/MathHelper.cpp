// 
// 
// 

#include "MathHelper.h"

float MathHelper::clampValue(float value, float min, float max) {
	if(value <= min)
		return min;
	if(value >= max)
		return max;
	return value;
}

float MathHelper::mapRatio(float ratio, float min, float max, float center) {
	clampValue(ratio, -1, 1);

	if(ratio == 0)
		return center;

	if(ratio > 0) {
		return (ratio) * (max - center) + center;
	} else {
		return center + (center - min) * ratio;
	}
}

float MathHelper::angleDifference(float a, float b) {
	return fmodf(fmodf(a - b, 360) + 540, 360) - 180;
}

float MathHelper::mixMotor(Config* config, float pitchDelta, float rollDelta, float yawDelta, float verticalRatio, MotorPosition position, MotorRotation rotation) {
	float targetMotorRatio = verticalRatio;

	if (abs(pitchDelta) >= 0.02) {
		if (position & Position_Front) {
			targetMotorRatio += pitchDelta * config->Degree2Ratio;
		}
		else {
			targetMotorRatio -= pitchDelta * config->Degree2Ratio;
		}
	}

	if (abs(rollDelta) >= 0.02) {
		if (position & Position_Left) {
			targetMotorRatio -= rollDelta * config->Degree2Ratio;
		}
		else {
			targetMotorRatio += rollDelta * config->Degree2Ratio;
		}
	}

	if (abs(yawDelta) >= 0.02) {
		if (rotation == Clockwise) {
			targetMotorRatio -= yawDelta * config->RotaryDegree2Ratio;
		}
		else {
			targetMotorRatio += yawDelta * config->RotaryDegree2Ratio;
		}
	}

	return targetMotorRatio;
}