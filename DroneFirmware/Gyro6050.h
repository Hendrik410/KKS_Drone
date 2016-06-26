#ifndef _GYRO6050_h
#define _GYRO6050_h
#include "Build.h"
#include "Gyro.h"
#include "CycleTimes.h"

#include <Wire.h>
#include <MPU6050_6Axis_MotionApps20.h>
#include <I2Cdev.h>

class Gyro6050 : public Gyro
{
protected:
	bool mpuOK;

	MPU6050 mpu;
	byte* fifoBuffer;
	int fifoOffset;

	uint32_t lastSample;
	bool firstUpdate;

	float accRes;
	float gyroRes;

	bool useDMP;

	float lastGyroValues[9];

	// orientation/motion vars
	Quaternion q;           // [w, x, y, z]         quaternion container
	VectorInt16 aa;         // [x, y, z]            accel sensor measurements
	VectorInt16 aaReal;     // [x, y, z]            gravity-free accel sensor measurements
	VectorInt16 aaWorld;    // [x, y, z]            world-frame accel sensor measurements
	VectorFloat gravity;    // [x, y, z]            gravity vector
	float ypr[3];

	float filter(float value);

public:
	explicit Gyro6050(Config* config);

	char* name();
	char* magnetometerName();

	bool init();
	void update();
	void reset();

	float getTemperature();

	bool hasMagnetometer();
	bool hasCompass();
};

#endif