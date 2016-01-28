#include "PID_Settings.h"

bool PID_Settings::read(PacketBuffer* buffer, PID_Settings* out) {
	float kp = buffer->readFloat();
	if (kp < 0 || kp > 1) {
		Log::error("PIDSettings", "kp is out of range");
		return false;
	}

	float ki = buffer->readFloat();
	if (ki < 0 || ki > 1) {
		Log::error("PIDSettings", "ki is out of range");
		return false;
	}

	float kd = buffer->readFloat();
	if (kd < 0 || kd > 1) {
		Log::error("PIDSettings", "kd is out of range");
		return false;
	}

	out->Kp = kp;
	out->Ki = ki;
	out->Kd = kd;
	return true;
}