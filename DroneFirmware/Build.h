#ifndef _BUILD_h
#define _BUILD_h

#define BUILD_VERSION 1
#define MODEL_NAME "Drone r1"
#define BUILD_NAME "build_" __DATE__ "_" __TIME__

#include <stdio.h>
#include <Esp.h>

inline void getBuildSerialCode(char* buffer, int length) {
	snprintf(buffer, length, "%X%X", ESP.getChipId(), ESP.getFlashChipId());
}

#endif