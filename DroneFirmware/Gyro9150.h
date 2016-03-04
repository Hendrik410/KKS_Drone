#ifndef _GYRO9150_h
#define _GYRO9150_h

#include "Gyro.h"

#define MPU9150_SWITCH_XY 1
#include "mpu9150.h"

class Gyro9150 : public Gyro
{
protected:
	MPU9150 mpu;
	bool mpuOK;

public:
	explicit Gyro9150(Config* config);

	bool init();
	void update();
	void reset();

	float getTemperature();
	bool hasMagnetometer();
	bool hasCompass();
};

#endif