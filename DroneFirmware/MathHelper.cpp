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

const float MathHelper::Degree2Ratio = 0.1;
const float MathHelper::RotaryDegree2Ratio = 0.1;

float MathHelper::mixMotor(float pitchDelta, float rollDelta, float yawDelta, float verticalRatio, MotorPosition position, MotorRotation rotation) {
	float targetMotorRatio = verticalRatio;

	if(position & Position_Front) {
		targetMotorRatio -= pitchDelta * Degree2Ratio;
	} else {
		targetMotorRatio += pitchDelta * Degree2Ratio;
	}

	if(position & Position_Left) {
		targetMotorRatio += rollDelta * Degree2Ratio;
	} else {
		targetMotorRatio -= rollDelta * Degree2Ratio;
	}

	if(rotation == Clockwise) {
		targetMotorRatio += yawDelta * RotaryDegree2Ratio;
	} else {
		targetMotorRatio -= yawDelta * RotaryDegree2Ratio;
	}

	return targetMotorRatio;
}
