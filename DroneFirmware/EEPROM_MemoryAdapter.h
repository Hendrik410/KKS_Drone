// EEPROM_MemoryAdapter.h

#ifndef _EEPROM_MEMORYADAPTER_h
#define _EEPROM_MEMORYADAPTER_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

#ifdef _VSARDUINO_H_ //Kompatibilität mit visual micro
#include <EEPROM/EEPROM.h>

#define byte unsigned char
void * memcpy(void * destination, const void * source, int num);
#else
#include <EEPROM.h>
#endif

#include "MemoryAdapter.h"

class EEPROM_MemoryAdapter : public MemoryAdaptor {
protected:
	uint16_t size;

public:
	EEPROM_MemoryAdapter(uint16_t size);

	bool begin() override;
	bool end() override;
	void writeByte(uint32_t address, byte val) override;
	void write(uint32_t address, byte* data, uint32_t length) override;
	byte readByte(uint32_t address) override;
	void read(uint32_t address, byte* data, uint32_t length) override;

};

#endif

