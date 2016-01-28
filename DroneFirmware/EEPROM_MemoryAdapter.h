// EEPROM_MemoryAdapter.h

#ifndef _EEPROM_MEMORYADAPTER_h
#define _EEPROM_MEMORYADAPTER_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

#ifdef _VSARDUINO_H_ //Kompatibilitšt mit visual micro
#include <EEPROM/EEPROM.h>

#define byte unsigned char
void * memcpy(void * destination, const void * source, int num);
#else
#include <EEPROM.h>
#endif

#include "MemoryAdapter.h"
#include "Log.h"

class EEPROM_MemoryAdapter : public MemoryAdaptor {
protected:
	uint16_t size;
	uint16_t offset;

	bool assertAddress(uint32_t address, uint32_t length);

public:
	EEPROM_MemoryAdapter(uint16_t size, uint16_t offset);

	bool begin() override;
	bool end() override;
	void writeByte(uint32_t address, uint8_t val) override;
	void write(uint32_t address, uint8_t* data, uint32_t length) override;
	byte readByte(uint32_t address) override;
	void read(uint32_t address, uint8_t* data, uint32_t length) override;

};

#endif

