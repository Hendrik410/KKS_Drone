// Gyro.h

#ifndef _GYRO_h
#define _GYRO_h

#include "arduino.h"
#include "Config.h"
#include "Log.h"

#ifdef _VSARDUINO_H_ //Kompatibilität mit visual micro
#include <Wire/Wire.h>
#include <I2Cdev/I2Cdev.h>
#include <MPU6050/MPU6050_6Axis_MotionApps20.h>

#define byte unsigned char
#else
#include <Wire/Wire.h>
#include <MPU6050/MPU6050_6Axis_MotionApps20.h>
#include <I2Cdev/I2Cdev.h>
#endif
class Gyro
{
 protected:
	 Config* config;

	 MPU6050 _MPU6050;
	 int MPU6050_Packet_Size = 0;
	 byte MPU6050_FIFO_Buffer[64];

	 // orientation/motion vars
	 Quaternion q;           // [w, x, y, z]         quaternion container
	 VectorInt16 aa;         // [x, y, z]            accel sensor measurements
	 VectorInt16 aaReal;     // [x, y, z]            gravity-free accel sensor measurements
	 VectorInt16 aaWorld;    // [x, y, z]            world-frame accel sensor measurements
	 VectorFloat gravity;    // [x, y, z]            gravity vector
	 float euler[3];         // [psi, theta, phi]    Euler angle container
	 float ypr[3];

	 float pitchOffset;
	 float rollOffset;
	 float yawOffset;

 public:
	explicit Gyro(Config* config);

	void init();
	void update();

	float getTemperature();

	void setAsZero();

	float getPitch() const;
	float getRoll() const;
	float getYaw() const;

	float getPitchRad() const;
	float getRollRad() const;
	float getYawRad() const;
};

#endif

