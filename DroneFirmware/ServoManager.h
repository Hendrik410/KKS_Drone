// ServoManager.h

#ifndef _SERVOMANAGER_h
#define _SERVOMANAGER_h

#include "arduino.h"
#include <Servo.h>
#include "Config.h"
#include "Log.h"
#include "MathHelper.h"


class ServoManager
{
protected:
	Config* config;

	bool attached;

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

	 int getValue(int servoValue);

public:
	explicit ServoManager(Config* config);

	void attach();
	void detach();

	void setServos(int fl, int fr, int bl, int br);
	void setAllServos(int val);

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

