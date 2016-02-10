#ifndef _GYRO6050_h
#define _GYRO6050_h
#include "Build.h"
#if !USE_MPU9150


#include "Gyro.h"

#include <Wire/Wire.h>
#include <MPU6050/MPU6050_6Axis_MotionApps20.h>
#include <I2Cdev/I2Cdev.h>

class Gyro6050 : public Gyro
{
protected:
	MPU6050 mpu;
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
	explicit Gyro6050(Config* config);

	void init();
	void update();
	void reset();

	float getTemperature();
};

#endif
#endif