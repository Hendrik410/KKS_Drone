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
#include "Profiler.h"

#define byte unsigned char
#else
#include "arduino.h"
#include "ServoManager.h"
#include "Gyro.h"
#include "MathHelper.h"
#include "Log.h"
#include "LED.h"
#include "StopReason.h"
#include "Profiler.h"
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
 private:
	long lastPhysicsCalc;
	long lastMovementUpdate;
	long lastHeartbeat;

	float maxTilt;
	float maxRotationSpeed;

 protected:
	 Config* config;
	 

	 DroneState _state;
	 StopReason _stopReason;

	 Gyro* gyro;
	 ServoManager* servos;

	 double targetPitch;
	 double targetRoll;
	 float targetRotationalSpeed;
	 float targetVerticalSpeed;

	 float frontLeftRatio;
	 float frontRightRatio;
	 float backLeftRatio;
	 float backRightRatio;

	 float frontLeftCorrection;
	 float frontRightCorrection;
	 float backLeftCorrection;
	 float backRightCorrection;

 public:
	explicit DroneEngine(Gyro* gyro, ServoManager* servos, Config* config);

	void arm();
	void disarm();
	void fly();
	void stop(StopReason reason);
	void clearStatus();

	DroneState state() const;
	StopReason getStopReason() const;
	
	void handle();
	virtual void handleInternal() = 0;

	void heartbeat();

	void setRawServoValues(int fl, int fr, int bl, int br, bool forceWrite = false) const;
	void setRawServoValues(int all, bool forceWrite = false) const;

	void setMaxTilt(float tilt);
	void setMaxRotationSpeed(float rotaionSpeed);

	float getMaxTilt() const;
	float getMaxRotationSpeed() const;

	void setTargetMovement(float pitch, float roll, float rotationalSpeed, float verticalSpeed);

	float getTargetPitch() const;
	float getTargetRoll() const;
	float getTargetRotationalSpeed() const;
	float getTargetVerticalSpeed() const;

	float getFrontLeftRatio() const;
	float getFrontRightRatio() const;
	float getBackLeftRatio() const;
	float getBackRightRatio() const;

	float getFrontLeftCorrection() const;
	float getFrontRightCorrection() const;
	float getBackLeftCorrection() const;
	float getBackRightCorrection() const;
};

#endif

