// ServoManager.h

#ifndef _SERVOMANAGER_h
#define _SERVOMANAGER_h

#include "arduino.h"
#include "Config.h"
#include "Log.h"

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

	 bool _dirty;


 public:
	 explicit ServoManager(Config* config);

	void init(int pinFL, int pinFR, int pinBL, int pinBR);
	void setServos(int fl, int fr, int bl, int br);
	void setAllServos(int val);
	void setRatio(float fl, float fr, float bl, float br);
	void setRationAll(float ratio);

	void handleTick();

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

	// Gibt zurück ob die Daten sich geändert haben und setzt dann dirty wieder zurück
	bool dirty() {
		bool d = _dirty;
		_dirty = false;
		return d;
	}
};


#endif

