// BinaryHelper.h

#ifndef _BINARYHELPER_h
#define _BINARYHELPER_h

#include "arduino.h"

class BinaryHelper
{
 public:
	 static void changeByteOrder(byte* buffer, int start, int length);

	 static void writeInt16(byte* buf, int offset, int16_t val);
	 static int16_t readInt16(byte* buf, int offset);

	 static void writeInt32(byte* buf, int offset, int32_t value);
	 static int32_t readInt32(byte* buf, int offset);

	 static void writeInt64(byte* buf, int offset, int64_t val);
	 static int64_t readInt64(byte* buf, int offset);

	 
	 static void writeUint16(byte* buf, int offset, uint16_t val);
	 static uint16_t readUint16(byte* buf, int offset);

	 static void writeUint32(byte* buf, int offset, uint32_t value);
	 static uint32_t readUint32(byte* buf, int offset);

	 static void writeUint64(byte* buf, int offset, uint64_t val);
	 static uint64_t readUint64(byte* buf, int offset);

	 static void writeFloat(byte* buf, int offset, float val);
	 static float readFloat(byte* buf, int offset);

	 static void writeDouble(byte* buf, int offset, double val);
	 static double readDouble(byte* buf, int offset);
};

#endif

