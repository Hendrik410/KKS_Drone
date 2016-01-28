// VoltageInputReader.h

#ifndef _VOLTAGEINPUTREADER_h
#define _VOLTAGEINPUTREADER_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

class VoltageInputReader {
protected:
	int _inputPin;
	float _maxVoltage;
	float _maxInputVoltage;
	float _lastVoltage;
public:
	VoltageInputReader(int pin, float maxVoltage, float maxInputVoltage);

	float readVoltage();
	float readRawVoltage();
};

#endif

