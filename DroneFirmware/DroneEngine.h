// DroneEngine.h

#ifndef _DRONEENGINE_h
#define _DRONEENGINE_h


#ifdef _VSARDUINO_H_ //Kompatibilität mit visual micro
#include "arduino.h"
#include "ServoManager.h"
#include "Gyro.h"
#include "MathHelper.h"

#define byte unsigned char
#else
#include "arduino.h"
#include "ServoManager.h"
#include "Gyro.h"
#include "MathHelper.h"
#endif

#define PHYSICS_CALC_DELAY_MS 20

class DroneEngine
{
 protected:
	 Config* config;
	 long lastPhysicsCalc;
	 long lastYawTargetCalc;
	 long lastMovementUpdate;
	 long maxMovementUpdate = 200;

	 bool _isArmed;

	 Gyro* gyro;
	 ServoManager* servos;

	 float maxTilt;
	 float maxRotationSpeed;

	 float targetVerticalSpeed;
	 float targetPitch;
	 float targetRoll;
	 float targetYaw;
	 float targetRotationSpeed;

 public:
	explicit DroneEngine(Gyro* gyro, ServoManager* servos, Config* config);

	void arm();
	void disarm();
	void stop();

	bool isArmed() const;
	
	void handle();

	void setMaxTilt(float tilt);
	void setMaxRotationSpeed(float rotaionSpeed);

	float getMaxTilt() const;
	float getMaxRotationSpeed() const;

	void setTargetMovement(float pitch, float roll, float yaw);
	void setTargetPitch(float pitch);
	void setTargetRoll(float roll);
	void setTargetRotarySpeed(float yaw);
	void setTargetVerticalSpeed(float vertical);

	float getTargetPitch() const;
	float getTargetRoll() const;
	float getTargetYaw() const;
	float getTargetRotarySpeed() const;
	float getTargetVerticalSpeed() const;
};

#endif

