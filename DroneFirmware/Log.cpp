// 
// 
// 

#include "Log.h"

const char* Log::getLevelString(LogLevel level) {
	switch (level) {
	case Error:
		return "[Error]";
	case Info:
		return " [Info]"; // extra Leerzeichen damit alle Strings gleich lang sind
	case Debug:
		return "[Debug]";
	}
}

void Log::print(LogLevel level, const char* tag, const char* format, va_list args) {
	Serial.printf("$ [%8ds] %s [%s]", millis() / 1000, getLevelString(level), tag);

	int padding = 12 - strlen(tag);
	for (int i = 0; i < padding; i++)
		Serial.print(' ');

	char buffer[128];
	int size = vsnprintf(buffer, sizeof(buffer), format, args);
	if (size > 0 && size < sizeof(buffer))
		Serial.print(buffer);

	Serial.println();
}

void Log::error(const char* tag, const char* format, ...) {
	va_list args;
	va_start(args, format);
	print(Error, tag, format, args);
	va_end(args);
}

void Log::info(const char* tag, const char* format, ...) {
	va_list args;
	va_start(args, format);
	print(Info, tag, format, args);
	va_end(args);
}

void Log::debug(const char* tag, const char* format, ...) {
	va_list args;
	va_start(args, format);
	print(Debug, tag, format, args);
	va_end(args);
}