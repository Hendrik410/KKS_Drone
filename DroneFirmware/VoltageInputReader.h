// VoltageInputReader.h

#ifndef _VOLTAGEINPUTREADER_h
#define _VOLTAGEINPUTREADER_h

#include "arduino.h"

class VoltageInputReader {
protected:
	int _inputPin;
	float _maxVoltage;
	float _maxInputVoltage;
	float _voltage;

public:
	VoltageInputReader(int pin, float maxVoltage, float maxInputVoltage);

	float readVoltage();
	float readRawVoltage();
};

#endif

