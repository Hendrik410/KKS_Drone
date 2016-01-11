// 
// 
// 

#include "EEPROM_MemoryAdapter.h"

EEPROM_MemoryAdapter::EEPROM_MemoryAdapter(uint16_t size) {
	this->size = size;
}

bool EEPROM_MemoryAdapter::begin() {
	EEPROM.begin(size);
	return true;
}

bool EEPROM_MemoryAdapter::end() {
	EEPROM.commit();
	EEPROM.end();
	return true;
}

void EEPROM_MemoryAdapter::writeByte(uint32_t address, unsigned char val) {
	EEPROM.write(address, val);
}

byte EEPROM_MemoryAdapter::readByte(uint32_t address) {
	return EEPROM.read(address);
}

void EEPROM_MemoryAdapter::read(uint32_t address, unsigned char* data, uint32_t length) {
	memcpy(data, EEPROM.getDataPtr(), length);
}

void EEPROM_MemoryAdapter::write(uint32_t address, unsigned char* data, uint32_t length) {
	memcpy(EEPROM.getDataPtr(), data, length);
	EEPROM.setDirty();
}

