#include "Profiler.h"

ProfilerFunction* Profiler::functions = NULL;
uint32_t Profiler::length = 0;

uint32_t* Profiler::stack = NULL;
uint32_t Profiler::stackCurrent = 0;

long Profiler::lastMaxReset = 0;

void Profiler::init() {
	functions = new ProfilerFunction[PROFILE_SIZE];
	length = 0;

	stack = new uint32_t[PROFILE_SIZE];
	stackCurrent = 0;
}

void Profiler::begin(const char* name) {
	if (functions == NULL)
		init();

	uint32_t index = UINT32_MAX;

	// Eintrag suchen
	for (int i = 0; i < length; i++) {
		if (strcmp(functions[i].name, name) == 0) {
			index = i;
			break;
		}
	}

	// Eintrag nicht gefunden
	if (index == UINT32_MAX) {
		if (length >= PROFILE_SIZE) {
			Log::error("Profiler", "Profiler buffer full");
			return;
		}

		index = length;

		ProfilerFunction* function = &functions[index];
		function->name = (char*)name;
		function->lastTime = 0;
		function->time = 0;
		function->maxTime = 0;
		length++;
	}
	else if (functions[index].currentTime == 0) 
		functions[index].time = 0;  // Zeit zurücksetzen

	functions[index].currentTime = micros();
	stack[stackCurrent++] = index;
}

void Profiler::end() {
	if (functions == NULL) {
		Log::error("Profiler", "init() not called");
		return;
	}
	if (stackCurrent == 0) {
		Log::error("Profiler", "Invalid end(), no parts left");
		return;
	}

	uint32_t index = stack[--stackCurrent];

	ProfilerFunction* function = &functions[index];
	function->time += micros() - function->currentTime;
}

void Profiler::finishFrame() {
	if (functions == NULL)
		init();

	if (stackCurrent > 0) {
		Log::error("Profiler", "Frame not correctly ended");
		while (stackCurrent > 0)
			end();
	}

	boolean resetMax = millis() - lastMaxReset > CYCLE_PROFILER_MAX;
	if (resetMax)
		lastMaxReset = millis();

	for (int i = 0; i < length; i++) {
		ProfilerFunction* function = &functions[i];
		function->lastTime = function->time;
		function->currentTime = 0;	// anfangen mit Zeit zurücksetzen

		if (resetMax || function->time > function->maxTime)
			function->maxTime = function->time;
	}
}

void Profiler::write(PacketBuffer* buffer) {
	if (functions == NULL)
		init();

	buffer->write(length);
	for (int i = 0; i < length; i++) {
		ProfilerFunction* function = &functions[i];

		buffer->writeString(function->name);
		buffer->write(function->lastTime);
		buffer->write(function->maxTime);	
	}
}