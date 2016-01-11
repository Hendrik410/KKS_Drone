// BinaryHelper.h

#ifndef _BINARYHELPER_h
#define _BINARYHELPER_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

#ifdef _VSARDUINO_H_ //Kompatibilität mit visual micro
#define byte unsigned char
#endif

class BinaryHelper
{
 protected:


 public:
	 static void writeShort(byte* buf, int offset, short val);
	 static short readShort(const byte* buf, int offset);

	 static void writeInt(byte* buf, int offset, int value);
	 static int readInt(const byte* buf, int offset);
};

#endif

