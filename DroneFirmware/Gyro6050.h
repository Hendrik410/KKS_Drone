#ifndef _GYRO6050_h
#define _GYRO6050_h
#include "Build.h"
#include "Gyro.h"

#include <Wire/Wire.h>
#include <MPU6050/MPU6050_6Axis_MotionApps20.h>
#include <I2Cdev/I2Cdev.h>

#define USE_DMP true

class Gyro6050 : public Gyro
{
protected:
	bool mpuOK;

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

	bool init();
	void update();
	void reset();

	float getTemperature();

	bool hasMagnetometer();
	bool hasCompass();
};

#endif