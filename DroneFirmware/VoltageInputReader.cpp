// 
// 
// 

#include "VoltageInputReader.h"


VoltageInputReader::VoltageInputReader(int pin, float maxVoltage, float maxInputVoltage) {
	_inputPin = pin;
	_maxVoltage = maxVoltage;
	_maxInputVoltage = maxInputVoltage;
	_lastVoltage = _maxVoltage;
}

float VoltageInputReader::readVoltage() {
	float newVoltage = _lastVoltage * 0.92 + readRawVoltage() * 0.08;
	_lastVoltage = newVoltage;
	return newVoltage;
}

float VoltageInputReader::readRawVoltage() {
	return ((analogRead(_inputPin) / 1024.0f) / _maxInputVoltage) * _maxVoltage;
}

