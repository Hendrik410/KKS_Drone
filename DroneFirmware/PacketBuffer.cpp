// 
// 
// 

#include "PacketBuffer.h"
#include <stdexcept>


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
		Serial.println("[Error] [PacketBuffer] assertPosition() operation not in range of packet");
		return false;
	}
	if (position + length > bufferSize) {
		Serial.println("[Error] [PacketBuffer] assertPosition() operation not in range of internal buffer");
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
		Serial.println("[Error] [PacketBuffer] setSize() operation not in range of packet");
		return;
	}

	this->size = size;
}

uint8_t PacketBuffer::readUint8() {
	return data[addAndAssertPosition(sizeof(uint8_t))];
}

uint16_t PacketBuffer::readUint16() {
	return BinaryHelper::readUint16(data, addAndAssertPosition(sizeof(uint16_t)));
}

uint32_t PacketBuffer::readUint32() {
	return BinaryHelper::readUint32(data, addAndAssertPosition(sizeof(uint32_t)));
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
	uint8_t* dest = this->data + addAndAssertPosition(sizeof(uint8_t) * length);
	uint8_t* source = buffer + offset;

	memcpy(dest, source, length);
}