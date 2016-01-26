// DroneEngine.h

#ifndef _DRONEENGINE_h
#define _DRONEENGINE_h


#ifdef _VSARDUINO_H_ //Kompatibilität mit visual micro
#include "arduino.h"
#include "ServoManager.h"
#include "Gyro.h"
#include "MathHelper.h"
#include "Log.h"
#include "LED.h"
#include "StopReason.h"

#define byte unsigned char
#else
#include "arduino.h"
#include "ServoManager.h"
#include "Gyro.h"
#include "MathHelper.h"
#include "Log.h"
#include "LED.h"
#include "StopReason.h"
#endif

enum DroneState {
	StateUnkown,
	StateReset,
	StateStopped,
	StateIdle,
	StateArmed,
	StateFlying
};

class DroneEngine
{
 protected:
	 Config* config;
	 long lastPhysicsCalc;
	 long lastYawTargetCalc;
	 long lastMovementUpdate;
	 long maxMovementUpdateInterval = 500;

	 DroneState _state;
	 StopReason _stopReason;

	 Gyro* gyro;
	 ServoManager* servos;

	 float maxTilt;
	 float maxRotationSpeed;

	 float targetVerticalSpeed;
	 double targetPitch;
	 double targetRoll;
	 double targetYaw;
	 double targetRotationSpeed;

	 float frontLeftRatio;
	 float frontRightRatio;
	 float backLeftRatio;
	 float backRightRatio;

 public:
	explicit DroneEngine(Gyro* gyro, ServoManager* servos, Config* config);

	void arm();
	void disarm();
	void stop(StopReason reason);
	void clearStatus();

	DroneState state() const;
	StopReason getStopReason() const;
	
	void handle();
	virtual void handleInternal() = 0;

	void setRawServoValues(int fl, int fr, int bl, int br, bool forceWrite = false) const;
	void setRawServoValues(int all, bool forceWrite = false) const;

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

	float getFrontLeftRatio() const;
	float getFrontRightRatio() const;
	float getBackLeftRatio() const;
	float getBackRightRatio() const;
};

#endif

