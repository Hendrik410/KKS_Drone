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

float MathHelper::angleDifference(float a, float b) {
	return (float)fmod(fmod(a - b, 360) + 540, 360) - 180;
}

float MathHelper::fixValue(float value, float begin, float end) {
	float range = end - begin;
	while (value < begin)
		value += range;
	while (value > end)
		value -= range;
	return value;
}

float motorsPitch[] = { 1, 1, -1, -1 };
float motorsRoll[] = { 1, -1, 1, -1 };
float motorsYaw[] = { 1, -1, -1, 1 };

float MathHelper::mixMotor(int motorIndex, float pitch, float roll, float yaw, float thrust) {
	float value = thrust;
	value += max(0, motorsPitch[motorIndex] * pitch);
	value += max(0, motorsRoll[motorIndex] * roll);
	value += max(0, motorsYaw[motorIndex] * yaw);
	return value;
}
