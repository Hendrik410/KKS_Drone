// Gyro.h

#ifndef _GYRO_h
#define _GYRO_h

#include "arduino.h"
#include "MathHelper.h"
#include "Config.h"
#include "Log.h"
#include "Profiler.h"

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

	 MPU6050 mpu;
	 int packetSize = 0;
	 byte fifoBuffer[64];

	 // orientation/motion vars
	 Quaternion q;           // [w, x, y, z]         quaternion container
	 VectorInt16 aa;         // [x, y, z]            accel sensor measurements
	 VectorInt16 aaReal;     // [x, y, z]            gravity-free accel sensor measurements
	 VectorInt16 aaWorld;    // [x, y, z]            world-frame accel sensor measurements
	 VectorFloat gravity;    // [x, y, z]            gravity vector
	 float ypr[3];

	 float pitchOffset;
	 float rollOffset;
	 float yawOffset;

	 float accelerationXOffset;
	 float accelerationYOffset;
	 float accelerationZOffset;

	 bool _dirty;

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

	float getAccelerationX() const;
	float getAccelerationY() const;
	float getAccelerationZ() const;

	// Gibt zurück ob die Daten sich geändert haben und setzt dann dirty wieder zurück
	bool dirty() {
		bool d = _dirty;
		_dirty = false;
		return d;
	}
};

#endif

