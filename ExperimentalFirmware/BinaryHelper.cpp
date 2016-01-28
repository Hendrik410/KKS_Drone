// 
// 
// 

#include "BinaryHelper.h"
#include "string.h"

bool BinaryHelper::BigEndian = true;

bool BinaryHelper::swappingNeeded() {
#ifdef __IEEE_LITTLE_ENDIAN
	return BigEndian;
#else
	return !BigEndian;
#endif
}

void BinaryHelper::changeByteOrder(unsigned char* buffer, int start, int length) {
	int end = start + length;
	int swapsLeft = length / 2;

	byte temp;
	while(swapsLeft > 0) {
		temp = buffer[start + swapsLeft - 1];
		buffer[start + swapsLeft - 1] = buffer[end - swapsLeft];
		buffer[end - swapsLeft] = temp;
		swapsLeft--;
	}
}

void BinaryHelper::writeInt16(unsigned char* buf, int offset, int16_t val) {
	memcpy(buf + offset, &val, sizeof(int16_t));
	if (swappingNeeded())
		changeByteOrder(buf, offset, sizeof(int16_t));
}

int16_t BinaryHelper::readInt16(byte* buf, int offset) {
	byte valArr[sizeof(int16_t)];
	memcpy(valArr, buf + offset, sizeof(int16_t));
	if (swappingNeeded())
		changeByteOrder(valArr, 0,sizeof(int16_t));
	return *reinterpret_cast<int16_t*>(valArr);
}

void BinaryHelper::writeUint16(unsigned char* buf, int offset, uint16_t val) {
	memcpy(buf + offset, &val, sizeof(uint16_t));
	if(swappingNeeded())
		changeByteOrder(buf, offset, sizeof(uint16_t));
}

uint16_t BinaryHelper::readUint16(byte* buf, int offset) {
	byte valArr[sizeof(uint16_t)];
	memcpy(valArr, buf + offset, sizeof(uint16_t));
	if(swappingNeeded())
		changeByteOrder(valArr, 0, sizeof(uint16_t));
	return *reinterpret_cast<uint16_t*>(valArr);
}

void BinaryHelper::writeInt32(byte* buf, int offset, int32_t val) {
	memcpy(buf + offset, &val, sizeof(int32_t));
	if (swappingNeeded())
		changeByteOrder(buf, offset, sizeof(int32_t));
}

int32_t BinaryHelper::readInt32(unsigned char* buf, int offset) {
	byte valArr[sizeof(int32_t)];
	memcpy(valArr, buf + offset, sizeof(int32_t));
	if (swappingNeeded())
		changeByteOrder(valArr, 0, sizeof(int32_t));
	return *reinterpret_cast<int32_t*>(valArr);
}

void BinaryHelper::writeUint32(byte* buf, int offset, uint32_t val) {
	memcpy(buf + offset, &val, sizeof(uint32_t));
	if(swappingNeeded())
		changeByteOrder(buf, offset, sizeof(uint32_t));
}

uint32_t BinaryHelper::readUint32(unsigned char* buf, int offset) {
	byte valArr[sizeof(uint32_t)];
	memcpy(valArr, buf + offset, sizeof(uint32_t));
	if(swappingNeeded())
		changeByteOrder(valArr, 0, sizeof(uint32_t));
	return *reinterpret_cast<uint32_t*>(valArr);
}

void BinaryHelper::writeInt64(unsigned char* buf, int offset, int64_t val) {
	memcpy(buf + offset, &val, sizeof(int64_t));
	if (swappingNeeded())
		changeByteOrder(buf, offset, sizeof(int64_t));
}


int64_t BinaryHelper::readInt64(unsigned char* buf, int offset) {
	byte valArr[sizeof(int64_t)];
	memcpy(valArr, buf + offset, sizeof(int64_t));
	if (swappingNeeded())
		changeByteOrder(valArr, 0, sizeof(int64_t));
	return *reinterpret_cast<int64_t*>(valArr);
}


void BinaryHelper::writeUint64(unsigned char* buf, int offset, uint64_t val) {
	memcpy(buf + offset, &val, sizeof(uint64_t));
	if(swappingNeeded())
		changeByteOrder(buf, offset, sizeof(uint64_t));
}


uint64_t BinaryHelper::readUint64(unsigned char* buf, int offset) {
	byte valArr[sizeof(uint64_t)];
	memcpy(valArr, buf + offset, sizeof(uint64_t));
	if(swappingNeeded())
		changeByteOrder(valArr, 0, sizeof(uint64_t));
	return *reinterpret_cast<uint64_t*>(valArr);
}

void BinaryHelper::writeFloat(unsigned char* buf, int offset, float val) {
	memcpy(&buf[offset], &val, sizeof(float));
}

float BinaryHelper::readFloat(unsigned char* buf, int offset) {
	byte valArr[sizeof(float)];
	memcpy(valArr, buf + offset, sizeof(float));
	return *reinterpret_cast<float*>(valArr);
}

void BinaryHelper::writeDouble(unsigned char* buf, int offset, double val) {
	memcpy(buf + offset, &val, sizeof(double));
}

double BinaryHelper::readDouble(unsigned char* buf, int offset) {
	byte valArr[sizeof(double)];
	memcpy(valArr, buf + offset, sizeof(double));
	return *reinterpret_cast<double*>(valArr);
}
