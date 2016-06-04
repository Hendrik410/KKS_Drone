// 
// 
// 

#include "VoltageInputReader.h"


VoltageInputReader::VoltageInputReader(int pin, float maxVoltage, float maxInputVoltage) {
	_inputPin = pin;
	_maxVoltage = maxVoltage;
	_maxInputVoltage = maxInputVoltage;
	_voltage = _maxVoltage;
}

float VoltageInputReader::readVoltage() {
	_voltage = _voltage * 0.92 + readRawVoltage() * 0.08;
	return _voltage;
}

float VoltageInputReader::readRawVoltage() {
	return ((analogRead(_inputPin) / 1024.0f) / _maxInputVoltage) * _maxVoltage;
}
