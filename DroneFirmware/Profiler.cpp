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

ProfilerFunction* Profiler::findFunction(const char* name) {
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
			return NULL;
		}

		index = length;

		ProfilerFunction* function = &functions[index];
		memset(function, 0, sizeof(ProfilerFunction));
		function->index = index;
		function->name = (char*)name;
		length++;
		return function;
	}
	return &functions[index];
}

uint32_t Profiler::start(const char* name) {
	return start(name, true);
}

uint32_t Profiler::start(const char* name, boolean reset) {
	ProfilerFunction* function = findFunction(name);
	if (function == NULL)
		return UINT32_MAX;

	if (function->shouldReset && reset)
		function->time = 0;  // Zeit zurücksetzen

	function->currentTime = micros();
	return function->index;
}

void Profiler::stop(ProfilerFunction* function, boolean add) {
	if (function->currentTime != 0) {
		if (add)
			function->time += micros() - function->currentTime;
		else
			function->time = micros() - function->currentTime;
	}
}
void Profiler::stop(const char* name) {
	ProfilerFunction* function = findFunction(name);
	if (function == NULL)
		return;

	stop(function, false);
}


void Profiler::begin(const char* name) {
	uint32_t func = start(name);
	if (func != UINT32_MAX)
		stack[stackCurrent++] = func;
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
	stop(&functions[index], true);
}

void Profiler::restart(const char* name) {
	stop(name);
	start(name, false);
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
		function->shouldReset = true;	// anfangen mit Zeit zurücksetzen

		if (resetMax || function->time > function->maxTime)
			function->maxTime = function->time;
	}
}

void Profiler::pushData(const char* name, uint32_t value) {
	ProfilerFunction* function = findFunction(name);
	if (function == NULL)
		return;

	function->time = value;
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