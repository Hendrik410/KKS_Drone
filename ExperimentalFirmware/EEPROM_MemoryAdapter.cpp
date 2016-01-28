// 
// 
// 

#include "EEPROM_MemoryAdapter.h"

EEPROM_MemoryAdapter::EEPROM_MemoryAdapter(uint16_t size, uint16_t offset) {
	this->size = size;
	this->offset = offset;
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

void EEPROM_MemoryAdapter::writeByte(uint32_t address, uint8_t val) {
	EEPROM.write(address + offset, val);
}

byte EEPROM_MemoryAdapter::readByte(uint32_t address) {
	return EEPROM.read(address + offset);
}

void EEPROM_MemoryAdapter::read(uint32_t address, uint8_t* data, uint32_t length) {
	memcpy(data, EEPROM.getDataPtr() + address + offset, length);
}

void EEPROM_MemoryAdapter::write(uint32_t address, uint8_t* data, uint32_t length) {
	memcpy(EEPROM.getDataPtr() + address + offset, data, length);
	EEPROM.setDirty();
}

