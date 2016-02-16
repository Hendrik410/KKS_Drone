#ifndef _PROFILER_h
#define _PROFILER_h

#include "arduino.h"
#include "PacketBuffer.h"

#define PROFILE_SIZE 16

class Profiler 
{
private:
	static char** names;
	static uint32_t* times;
	static uint32_t* currentTimes;
	static uint32_t length;

	static uint32_t* stack;
	static uint32_t stackCurrent;

public:
	static void init();
	static void begin(const char* name);
	static void end();
	static void write(PacketBuffer* buffer);
};
#endif