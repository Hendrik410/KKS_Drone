#include "Profiler.h"

char** Profiler::names = NULL;
uint32_t* Profiler::times = NULL;
uint32_t Profiler::length = 0;

uint32_t* Profiler::stack = NULL;
uint32_t Profiler::stackCurrent = 0;

void Profiler::init() {
	names = (char**)malloc(sizeof(char*) * PROFILE_SIZE);
	times = (uint32_t*)malloc(sizeof(uint32_t) * PROFILE_SIZE);
	length = 0;

	stack = (uint32_t*)malloc(sizeof(uint32_t) * PROFILE_SIZE);
	stackCurrent = 0;
}

void Profiler::begin(const char* name) {
	if (names == NULL)
		init();

	uint32_t index = UINT32_MAX;

	// Eintrag suchen
	for (int i = 0; i < length; i++) {
		if (strcmp(names[i], name) == 0) {
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
		names[index] = (char*)name;
		length++;
	}

	times[index] = micros();
	stack[stackCurrent++] = index;
}

void Profiler::end() {
	if (names == NULL) {
		Log::error("Profiler", "init() not called");
		return;
	}
	if (stackCurrent == 0) {
		Log::error("Profiler", "Invalid end(), no parts left");
		return;
	}

	uint32_t index = stack[--stackCurrent];
	times[index] = micros() - times[index];
}

void Profiler::write(PacketBuffer* buffer) {
	if (names == NULL)
		init();

	while (stackCurrent > 0)
		end();

	buffer->write(length);
	for (int i = 0; i < length; i++) {
		buffer->writeString(names[i]);
		buffer->write((uint32_t)times[i]);
	}
}