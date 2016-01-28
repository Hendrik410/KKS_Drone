#ifndef _PID_SETTINGS_H_
#define _PID_SETTINGS_H_

#include "PacketBuffer.h"

struct PID_Settings {
public:
	float Kp, Ki, Kd;

	static bool read(PacketBuffer* buffer, PID_Settings* out);
};

#endif