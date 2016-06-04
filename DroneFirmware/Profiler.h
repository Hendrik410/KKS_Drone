#ifndef _PROFILER_h
#define _PROFILER_h

#include "arduino.h"
#include "PacketBuffer.h"

#define PROFILE_SIZE 16

struct ProfilerFunction {
	char* name;
	uint32_t time;
	uint32_t maxTime;

	uint32_t currentTime;
};

class Profiler 
{
private:
	static ProfilerFunction* functions;
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