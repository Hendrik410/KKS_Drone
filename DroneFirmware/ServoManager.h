// ServoManager.h

#ifndef _SERVOMANAGER_h
#define _SERVOMANAGER_h

#include "arduino.h"
#include "Config.h"

#ifdef _VSARDUINO_H_ //Kompatibilität mit visual micro
#include <Servo/src/Servo.h>
#include "MathHelper.h"

#define byte unsigned char
#else
#include <Servo.h>
#include "MathHelper.h"
#endif

class ServoManager
{
 protected:
	 Config* config;

	 // The values for the Servos
	 int servoFLValue;
	 int servoFRValue;
	 int servoBLValue;
	 int servoBRValue;

	 // The objects to control the Servos (ESCs)
	 Servo frontLeft;
	 Servo frontRight;
	 Servo backLeft;
	 Servo backRight;

	 bool _isArmed = false;

 public:
	 explicit ServoManager(Config* config);

	void init(int pinFL, int pinFR, int pinBL, int pinBR);
	void setServos(int fl, int fr, int bl, int br, bool forceWrite = false);
	void setAllServos(int val, bool forceWrite = false);
	void setRatio(float fl, float fr, float bl, float br);
	void setRationAll(float ratio);
	void armMotors();
	void disarmMotors();

	bool isArmed() const {
		return _isArmed;
	}
	int FL() const {
		return servoFLValue;
	}
	int FR() const {
		return servoFRValue;
	}
	int BL() const {
		return servoBLValue;
	}
	int BR() const {
		return servoBRValue;
	}
};


#endif

