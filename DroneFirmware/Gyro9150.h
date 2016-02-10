#ifndef _GYRO9150_h
#define _GYRO9150_h

#include "Gyro.h"

#include <Wire/Wire.h>
#include <MPU9150/MPU9150_9Axis_MotionApps41.h>
#include <I2Cdev/I2Cdev.h>

class Gyro9150 : public Gyro
{
protected:
	MPU9150 mpu;
	byte* fifoBuffer;
	int fifoOffset;

	// orientation/motion vars
	Quaternion q;           // [w, x, y, z]         quaternion container
	VectorInt16 aa;         // [x, y, z]            accel sensor measurements
	VectorInt16 aaReal;     // [x, y, z]            gravity-free accel sensor measurements
	VectorInt16 aaWorld;    // [x, y, z]            world-frame accel sensor measurements
	VectorFloat gravity;    // [x, y, z]            gravity vector
	float ypr[3];
public:
	explicit Gyro9150(Config* config);

	void init();
	void update();
	void reset();

	float getTemperature();
};

#endif