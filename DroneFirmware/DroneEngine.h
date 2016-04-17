// DroneEngine.h

#ifndef _DRONEENGINE_h
#define _DRONEENGINE_h

#include "arduino.h"
#include "ServoManager.h"
#include "Gyro.h"
#include "MathHelper.h"
#include "Log.h"
#include "LED.h"
#include "StopReason.h"
#include "Profiler.h"
#include "PID.h"

enum DroneState {
	StateUnkown,
	StateReset,
	StateOTA,
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

	 double pidInput;
	 double pidSetpoint;
	 
	 double pitchOutput;
	 double rollOutput;
	 double yawOutput;
	 
	 PID* pitchPID;
	 PID* rollPID;
	 PID* yawPID;

	 void createPID();
	 PID* createPID(PIDSettings settings, double* output);

	 void calculatePID(PID* pid, float input, float setpoint);

 public:
	explicit DroneEngine(Gyro* gyro, ServoManager* servos, Config* config);

	void arm();
	void disarm();
	void fly();
	void stop(StopReason reason);
	void clearStatus();
	
	bool beginOTA();
	void endOTA();

	DroneState state() const;
	StopReason getStopReason() const;
	
	void handle();
	void handleInternal();

	void updateTunings();

	void heartbeat();

	void setRawServoValues(int fl, int fr, int bl, int br) const;
	void setRawServoValues(int all) const;

	void setMaxTilt(float tilt);
	void setMaxRotationSpeed(float rotaionSpeed);

	float getMaxTilt() const;
	float getMaxRotationSpeed() const;

	void setTargetMovement(float pitch, float roll, float rotationalSpeed, float verticalSpeed);

	float getTargetPitch() const;
	float getTargetRoll() const;
	float getTargetRotationalSpeed() const;
	float getTargetVerticalSpeed() const;

	float getPitchOutput() const;
	float getRollOutput() const;
	float getYawOutput() const;
};

#endif

