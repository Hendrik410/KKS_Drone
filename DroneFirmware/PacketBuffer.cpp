// 
// 
// 

#include "PacketBuffer.h"



PacketBuffer::PacketBuffer(uint32_t size) {
	this->data = new uint8_t[size];
	this->bufferSize = size;

	this->position = 0;
	this->size = size;
}

uint8_t* PacketBuffer::getBuffer() const {
	return data;
}

uint32_t PacketBuffer::getBufferSize() const {
	return bufferSize;
}

bool PacketBuffer::assertPosition(uint32_t length) {
	if (position + length > size) {
		Log::error("PacketBuffer", "assertPosition() operation not in range of packet");
		Log::error("PacketBuffer", "%d + %d > %d", position, length, size);
		return false;
	}
	if (position + length > bufferSize) {
		Log::error("PacketBuffer", "assertPosition() operation not in range of internal buffer");
		Log::error("PacketBuffer", "%d + %d > %d", position, length, bufferSize);
		return false;
	}
	return true;
}

uint32_t PacketBuffer::addAndAssertPosition(uint32_t length) {
	uint32_t oldPosition = position;
	if (!assertPosition(length))
		return oldPosition;

	position += length;

	return oldPosition;
}

uint32_t PacketBuffer::getPosition() const {
	return position;
}

void PacketBuffer::seek(uint32_t offset) {
	addAndAssertPosition(offset);
}

void PacketBuffer::resetPosition() {
	position = 0;
}

void PacketBuffer::reset() {
	position = 0;
	setSize(0);
}

uint32_t PacketBuffer::getSize() const {
	return size;
}

void PacketBuffer::setSize(uint32_t size) {
	if (size > bufferSize) {
		Log::error("PacketBuffer", "setSize() operation not in range of packet");
		Log::error("PacketBuffer", "%d > %d", size, bufferSize);
		return;
	}

	this->size = size;
}

int8_t PacketBuffer::readInt8() {
	return data[addAndAssertPosition(sizeof(int8_t))];
}

uint8_t PacketBuffer::readUint8() {
	return data[addAndAssertPosition(sizeof(uint8_t))];
}

int16_t PacketBuffer::readInt16() {
	return BinaryHelper::readInt16(data, addAndAssertPosition(sizeof(int16_t)));
}

uint16_t PacketBuffer::readUint16() {
	return BinaryHelper::readUint16(data, addAndAssertPosition(sizeof(uint16_t)));
}

int32_t PacketBuffer::readInt32() {
	return BinaryHelper::readInt32(data, addAndAssertPosition(sizeof(int32_t)));
}

uint32_t PacketBuffer::readUint32() {
	return BinaryHelper::readUint32(data, addAndAssertPosition(sizeof(uint32_t)));
}

int64_t PacketBuffer::readInt64() {
	return BinaryHelper::readInt64(data, addAndAssertPosition(sizeof(int64_t)));
}

uint64_t PacketBuffer::readUint64() {
	return BinaryHelper::readUint64(data, addAndAssertPosition(sizeof(uint64_t)));
}

float PacketBuffer::readFloat() {
	return BinaryHelper::readFloat(data, addAndAssertPosition(sizeof(float)));
}

double PacketBuffer::readDouble() {
	return BinaryHelper::readDouble(data, addAndAssertPosition(sizeof(double)));
}

void PacketBuffer::read(uint8_t* buffer, int length) {
	read(buffer, length, 0);
}

void PacketBuffer::read(uint8_t* buffer, int length, int offset) {
	uint8_t* source = this->data + addAndAssertPosition(sizeof(uint8_t) * length);
	uint8_t* dest = buffer + offset;

	memcpy(dest, source, length);
}

void PacketBuffer::read(char* buffer, int length, int offset) {
	uint8_t* source = this->data + addAndAssertPosition(sizeof(uint8_t) * length);
	char* dest = buffer + offset;

	memcpy(dest, source, length);
}

char* PacketBuffer::readString() {
	uint16_t length = readUint16();

	char* str = (char*)malloc(length * sizeof(length));
	read(str, length, 0);
	return str;
}

void PacketBuffer::write(char value) {
	data[addAndAssertPosition(sizeof(value))] = value;
}

void PacketBuffer::write(uint8_t value) {
	data[addAndAssertPosition(sizeof(value))] = value;
}

void PacketBuffer::write(uint16_t value) {
	BinaryHelper::writeUint16(data, addAndAssertPosition(sizeof(value)), value);
}

void PacketBuffer::write(uint32_t value) {
	BinaryHelper::writeUint32(data, addAndAssertPosition(sizeof(value)), value);
}

void PacketBuffer::write(uint64_t value) {
	BinaryHelper::writeUint64(data, addAndAssertPosition(sizeof(value)), value);
}

void PacketBuffer::write(int8_t value) {
	data[addAndAssertPosition(sizeof(value))] = value;
}

void PacketBuffer::write(int16_t value) {
	BinaryHelper::writeInt16(data, addAndAssertPosition(sizeof(value)), value);
}

void PacketBuffer::write(int32_t value) {
	BinaryHelper::writeInt32(data, addAndAssertPosition(sizeof(value)), value);
}

void PacketBuffer::write(int64_t value) {
	BinaryHelper::writeInt64(data, addAndAssertPosition(sizeof(value)), value);
}

void PacketBuffer::write(float value) {
	BinaryHelper::writeFloat(data, addAndAssertPosition(sizeof(value)), value);
}

void PacketBuffer::write(double value) {
	BinaryHelper::writeDouble(data, addAndAssertPosition(sizeof(value)), value);
}

void PacketBuffer::write(uint8_t* buffer, int length) {
	write(buffer, length, 0);
}

void PacketBuffer::write(uint8_t* buffer, int length, int offset) {
	int size = sizeof(uint8_t) * length;
	uint8_t* dest = this->data + addAndAssertPosition(size);
	uint8_t* source = buffer + offset;

	memcpy(dest, source, size);
}

void PacketBuffer::writeString(char* str) {
	int size = sizeof(char) * strlen(str); 

	write((uint16_t)strlen(str));

	uint8_t* dest = this->data + addAndAssertPosition(size);
	memcpy(dest, str, size);
}