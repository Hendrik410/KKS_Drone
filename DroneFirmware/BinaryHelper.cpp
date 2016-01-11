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


void BinaryHelper::writeUint16(unsigned char* buf, int offset, uint16_t val) {
	memcpy(&buf[offset], &val, 2);
	if(swappingNeeded())
		changeByteOrder(buf, offset, 2);
}

uint16_t BinaryHelper::readUint16(byte* buf, int offset) {
	byte valArr[2];
	memcpy(valArr, &buf[offset], 2);
	if(swappingNeeded())
		changeByteOrder(buf, 0, 2);
	return *reinterpret_cast<uint16_t*>(valArr);
}

void BinaryHelper::writeUint32(byte* buf, int offset, uint32_t val) {
	memcpy(&buf[offset], &val, 4);
	if(swappingNeeded())
		changeByteOrder(buf, offset, 4);
}

uint32_t BinaryHelper::readUint32(unsigned char* buf, int offset) {
	byte valArr[4];
	memcpy(valArr, &buf[offset], 4);
	if(swappingNeeded())
		changeByteOrder(buf, 0, 4);
	return *reinterpret_cast<uint32_t*>(valArr);
}


void BinaryHelper::writeUint64(unsigned char* buf, int offset, uint64_t val) {
	memcpy(&buf[offset], &val, 8);
	if(swappingNeeded())
		changeByteOrder(buf, offset, 8);
}


uint64_t BinaryHelper::readUint64(unsigned char* buf, int offset) {
	byte valArr[8];
	memcpy(valArr, &buf[offset], 8);
	if(swappingNeeded())
		changeByteOrder(buf, 0, 8);
	return *reinterpret_cast<uint64_t*>(valArr);
}

void BinaryHelper::writeFloat(unsigned char* buf, int offset, float val) {
	memcpy(&buf[offset], &val, 4);
	if(swappingNeeded())
		changeByteOrder(buf, offset, 4);
}

float BinaryHelper::readFloat(unsigned char* buf, int offset) {
	byte valArr[4];
	memcpy(valArr, &buf[offset], 4);
	if(swappingNeeded())
		changeByteOrder(buf, 0, 4);
	return *reinterpret_cast<float*>(valArr);
}

void BinaryHelper::writeDouble(unsigned char* buf, int offset, double val) {
	memcpy(&buf[offset], &val, 8);
	if(swappingNeeded())
		changeByteOrder(buf, offset, 8);
}

double BinaryHelper::readDouble(unsigned char* buf, int offset) {
	byte valArr[8];
	memcpy(valArr, &buf[offset], 8);
	if(swappingNeeded())
		changeByteOrder(buf, 0, 8);
	return *reinterpret_cast<double*>(valArr);
}
