// Gyro.h

#ifndef _GYRO_h
#define _GYRO_h

#include "arduino.h"
#include "MathHelper.h"
#include "Config.h"
#include "Log.h"
#include "Profiler.h"

class Gyro
{
 protected:
	 Config* config;
	 
	 float roll = 0;
	 float pitch = 0;
	 float yaw = 0;

	 float rollOffset = 0;
	 float pitchOffset = 0;
	 float yawOffset = 0;

	 float gyroX = 0;
	 float gyroY = 0;
	 float gyroZ = 0;

	 float accX = 0;
	 float accY = 0;
	 float accZ = 0;

	 float accelerationXOffset = 0;
	 float accelerationYOffset = 0;
	 float accelerationZOffset = 0;

	 float magnetX = 0;
	 float magnetY = 0;
	 float magnetZ = 0;

	 bool _dirty;

	 float getPitchRad() const;
	 float getRollRad() const;
	 float getYawRad() const;

 public:
	explicit Gyro(Config* config);

	virtual char* name() = 0;
	virtual char* magnetometerName() = 0;

	virtual bool init() = 0;
	virtual void update() = 0;
	virtual void reset() = 0;
	virtual float getTemperature() = 0;
	virtual bool hasMagnetometer() = 0;
	virtual bool hasCompass() = 0;

	void setAsZero();

	float getRoll() const;
	float getPitch() const;
	float getYaw() const;

	float getGyroX() const;
	float getGyroY() const;
	float getGyroZ() const;

	float getAccelerationX() const;
	float getAccelerationY() const;
	float getAccelerationZ() const;

	float getMagnetX() const;
	float getMagnetY() const;
	float getMagnetZ() const;

	boolean isMoving() const;
	boolean isFlat() const;

	// Gibt zurück ob die Daten sich geändert haben und setzt dann dirty wieder zurück
	bool dirty() {
		bool d = _dirty;
		_dirty = false;
		return d;
	}
};

#endif

