#include "PacketBuffer.h"

PacketBuffer::PacketBuffer(uint32_t size) {
	this->data = new uint8_t[size];
	this->bufferSize = size;

	this->position = 0;
	this->size = size;

	this->error = false;
}

PacketBuffer::PacketBuffer(uint8_t* data, uint32_t size) {
	setBuffer(data, size);
}

void PacketBuffer::setBuffer(uint8_t* data, uint32_t size) {
	this->data = data;
	this->bufferSize = size;

	this->position = 0;
	this->size = size;
	this->allowRead = true;
}


PacketBuffer::~PacketBuffer()
{
	if (data)
		free(data);
}

bool PacketBuffer::getError() {
	bool err = error;
	this->error = false;
	return err;
}

uint8_t* PacketBuffer::getBuffer() const {
	return data;
}

uint32_t PacketBuffer::getBufferSize() const {
	return bufferSize;
}

bool PacketBuffer::assertRead() {
	if (!allowRead) {
		error = true;
		Log::error("PacketBuffer", "assertRead() not allowed to read");
	}
	return allowRead;
}

bool PacketBuffer::assertPosition(uint32_t length) {
	if (position + length > size) {
		error = true;
		Log::error("PacketBuffer", "assertPosition() operation not in range of packet");
		Log::error("PacketBuffer", "%d + %d > %d", position, length, size);
		return false;
	}
	if (position + length > bufferSize) {
		error = true;
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
	allowRead = true;
}

void PacketBuffer::reset() {
	resetPosition();
	setSize(0);
}

uint32_t PacketBuffer::getSize() const {
	return size;
}

void PacketBuffer::setSize(uint32_t size) {
	if (size > bufferSize) {
		Log::error("PacketBuffer", "setSize() operation not in range of packet");
		Log::error("PacketBuffer", "%d > %d", size, bufferSize);
		error = true;
		return;
	}

	this->size = size;
}

bool PacketBuffer::readBoolean() {
	return readUint8() > 0;
}

int8_t PacketBuffer::readInt8() {
	assertRead();
	return data[addAndAssertPosition(sizeof(int8_t))];
}

uint8_t PacketBuffer::readUint8() {
	assertRead();
	return data[addAndAssertPosition(sizeof(uint8_t))];
}

int16_t PacketBuffer::readInt16() {
	assertRead();
	return BinaryHelper::readInt16(data, addAndAssertPosition(sizeof(int16_t)));
}

uint16_t PacketBuffer::readUint16() {
	assertRead();
	return BinaryHelper::readUint16(data, addAndAssertPosition(sizeof(uint16_t)));
}

int32_t PacketBuffer::readInt32() {
	assertRead();
	return BinaryHelper::readInt32(data, addAndAssertPosition(sizeof(int32_t)));
}

uint32_t PacketBuffer::readUint32() {
	assertRead();
	return BinaryHelper::readUint32(data, addAndAssertPosition(sizeof(uint32_t)));
}

int64_t PacketBuffer::readInt64() {
	assertRead();
	return BinaryHelper::readInt64(data, addAndAssertPosition(sizeof(int64_t)));
}

uint64_t PacketBuffer::readUint64() {
	assertRead();
	return BinaryHelper::readUint64(data, addAndAssertPosition(sizeof(uint64_t)));
}

float PacketBuffer::readFloat() {
	assertRead();
	return BinaryHelper::readFloat(data, addAndAssertPosition(sizeof(float)));
}

double PacketBuffer::readDouble() {
	assertRead();
	return BinaryHelper::readDouble(data, addAndAssertPosition(sizeof(double)));
}

void PacketBuffer::read(uint8_t* buffer, int length) {
	read(buffer, length, 0);
}

void PacketBuffer::read(uint8_t* buffer, int length, int offset) {
	assertRead();
	uint8_t* source = this->data + addAndAssertPosition(sizeof(uint8_t) * length);
	uint8_t* dest = buffer + offset;

	memcpy(dest, source, length);
}

void PacketBuffer::read(char* buffer, int length, int offset) {
	assertRead();
	uint8_t* source = this->data + addAndAssertPosition(sizeof(char) * length);
	char* dest = buffer + offset;

	memcpy(dest, source, length);
}

uint8_t* PacketBuffer::getBufferRegion(int size) {
	return this->data + addAndAssertPosition(size);
}

char* PacketBuffer::readString() {
	uint16_t length = readUint16();

	int size = length * sizeof(char);

	char* str = (char*)malloc(size + 1);
	read(str, size, 0);
	str[size] = '\0';
	return str;
}

void PacketBuffer::write(bool value) {
	if (value)
		write(uint8_t(1));
	else
		write(uint8_t(0));
}

void PacketBuffer::write(char value) {
	allowRead = false;
	data[addAndAssertPosition(sizeof(value))] = value;
}

void PacketBuffer::write(uint8_t value) {
	allowRead = false;
	data[addAndAssertPosition(sizeof(value))] = value;
}

void PacketBuffer::write(uint16_t value) {
	allowRead = false;
	BinaryHelper::writeUint16(data, addAndAssertPosition(sizeof(value)), value);
}

void PacketBuffer::write(uint32_t value) {
	allowRead = false;
	BinaryHelper::writeUint32(data, addAndAssertPosition(sizeof(value)), value);
}

void PacketBuffer::write(uint64_t value) {
	allowRead = false;
	BinaryHelper::writeUint64(data, addAndAssertPosition(sizeof(value)), value);
}

void PacketBuffer::write(int8_t value) {
	allowRead = false;
	data[addAndAssertPosition(sizeof(value))] = value;
}

void PacketBuffer::write(int16_t value) {
	allowRead = false;
	BinaryHelper::writeInt16(data, addAndAssertPosition(sizeof(value)), value);
}

void PacketBuffer::write(int32_t value) {
	allowRead = false;
	BinaryHelper::writeInt32(data, addAndAssertPosition(sizeof(value)), value);
}

void PacketBuffer::write(int64_t value) {
	allowRead = false;
	BinaryHelper::writeInt64(data, addAndAssertPosition(sizeof(value)), value);
}

void PacketBuffer::write(float value) {
	allowRead = false;
	BinaryHelper::writeFloat(data, addAndAssertPosition(sizeof(value)), value);
}

void PacketBuffer::write(double value) {
	allowRead = false;
	BinaryHelper::writeDouble(data, addAndAssertPosition(sizeof(value)), value);
}

void PacketBuffer::write(uint8_t* buffer, int length) {
	write(buffer, length, 0);
}

void PacketBuffer::write(uint8_t* buffer, int length, int offset) {
	allowRead = false;
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

void PacketBuffer::writeString(const char* str) {
	writeString((char*)str);
}