// Packet.h

#ifndef _PACKETBUFFER_h
#define _PACKETBUFFER_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

#include "BinaryHelper.h"
#include "Log.h"

class PacketBuffer {
private:
	uint8_t* data; // Zeiger auf den Buffer
	uint32_t bufferSize;

	uint32_t position; // aktuelle Position
	uint32_t size; // L�nge des Pakets

	bool assertPosition(uint32_t length); // �berpr�ft die Position
	uint32_t addAndAssertPosition(uint32_t length); // addiert einen Wert zur Position, �berpr�ft die Position und gibt die alte Position zur�ck

public:
	explicit PacketBuffer(uint32_t size);

	uint8_t* getBuffer() const;
	uint32_t getBufferSize() const;

	uint32_t getPosition() const;
	void seek(uint32_t offset); // �berspringt offset Bytes
	void resetPosition();

	// setzt die Position und die L�nge des Pakets auf 0
	void reset();

	// gibt die L�nge des Pakets in Bytes zur�ck
	uint32_t getSize() const;

	// setzt die L�nge des Pakets in Bytes
	void setSize(uint32_t size);

	uint8_t readUint8();
	uint16_t readUint16();
	uint32_t readUint32();
	uint64_t readUint64();

	float readFloat();
	double readDouble();

	void read(uint8_t* buffer, int length);
	void read(uint8_t* buffer, int offset, int length);

	void write(char value);
	void write(uint8_t value);
	void write(uint16_t value);
	void write(uint32_t value);
	void write(uint64_t value);
	void write(float value);
	void write(double value);

	void write(uint8_t* buffer, int length);
	void write(uint8_t* buffer, int offset, int length);
};
#endif

