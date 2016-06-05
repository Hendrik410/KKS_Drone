#ifndef _PROFILER_h
#define _PROFILER_h

#include "arduino.h"
#include "PacketBuffer.h"
#include "CycleTimes.h"

#define PROFILE_SIZE 16

struct ProfilerFunction {
	uint32_t index;
	char* name;
	uint32_t lastTime;
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

	static long lastMaxReset;

	static ProfilerFunction* findFunction(const char* name);

public:
	static void init();
	static void begin(const char* name);
	static void end();
	static void finishFrame();
	static void pushData(const char* name, uint32_t value);
	static void write(PacketBuffer* buffer);
};
#endif