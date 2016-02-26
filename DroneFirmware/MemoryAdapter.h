// MemoryAdapter.h

#ifndef _MEMORYADAPTER_h
#define _MEMORYADAPTER_h

#include "arduino.h"

class MemoryAdaptor {
protected:
	~MemoryAdaptor() {
	}

public:
	virtual bool begin() = 0;
	virtual void writeByte(uint32_t address, uint8_t val) = 0;
	virtual void write(uint32_t address, uint8_t* data, uint32_t length) = 0;
	virtual byte readByte(uint32_t address) = 0;
	virtual void read(uint32_t address, uint8_t* data, uint32_t length) = 0;
	virtual bool end() = 0;
};

#endif

