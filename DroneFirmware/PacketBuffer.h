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
	uint32_t size; // Länge des Pakets

	bool error;
	bool allowRead;

	bool assertRead(); // überprüft ob das Lesen zulässig ist
	bool assertPosition(uint32_t length); // überprüft die Position
	uint32_t addAndAssertPosition(uint32_t length); // addiert einen Wert zur Position, überprüft die Position und gibt die alte Position zurück

public:
	explicit PacketBuffer(uint32_t size);
	explicit PacketBuffer(uint8_t* data, uint32_t size);
	~PacketBuffer();

	bool getError();

	void setBuffer(uint8_t* data, uint32_t size);

	uint8_t* getBuffer() const;
	uint32_t getBufferSize() const;

	uint32_t getPosition() const;
	void seek(uint32_t offset); // überspringt offset Bytes
	void resetPosition();

	// setzt die Position und die Länge des Pakets auf 0
	void reset();

	// gibt die Länge des Pakets in Bytes zurück
	uint32_t getSize() const;

	// setzt die Länge des Pakets in Bytes
	void setSize(uint32_t size);

	bool readBoolean();
	int8_t readInt8();
	uint8_t readUint8();
	int16_t readInt16();
	uint16_t readUint16();
	int32_t readInt32();
	uint32_t readUint32();
	int64_t readInt64();
	uint64_t readUint64();

	float readFloat();
	double readDouble();

	void read(uint8_t* buffer, int length);
	void read(uint8_t* buffer, int length, int offset);
	void read(char* buffer, int length, int offset);

	uint8_t* getBufferRegion(int size);

	char* readString();

	void write(bool value);
	void write(char value);
	void write(int8_t value);
	void write(uint8_t value);
	void write(int16_t value);
	void write(uint16_t value);
	void write(int32_t value);
	void write(uint32_t value);
	void write(int64_t value);
	void write(uint64_t value);

	void write(float value);
	void write(double value);

	void write(uint8_t* buffer, int length);
	void write(uint8_t* buffer, int offset, int length);

	void writeString(char* str);
	void writeString(const char* str);
};
#endif

