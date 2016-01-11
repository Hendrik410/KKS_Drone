// 
// 
// 

#include "BinaryHelper.h"

void BinaryHelper::writeShort(byte * buf, int offset, short val) {
	buf[offset] = (val >> 8) & 0xFF;
	buf[offset + 1] = (val)& 0xFF;
}

short BinaryHelper::readShort(const byte* buffer, int offset) {
	return (buffer[offset] << 8) | buffer[offset + 1];
}

void BinaryHelper::writeInt(byte buf[], int offset, int val) {
	buf[offset] = (val >> 24) & 0xFF;
	buf[offset + 1] = (val >> 16) & 0xFF;
	buf[offset + 2] = (val >> 8) & 0xFF;
	buf[offset + 3] = (val)& 0xFF;
}


int BinaryHelper::readInt(const byte* buffer, int offset) {
	return (buffer[offset] << 24) | (buffer[offset + 1] << 16) | (buffer[offset + 2] << 8) | buffer[offset + 3];
}


