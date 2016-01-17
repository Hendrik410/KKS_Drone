// Log.h

#ifndef _LOG_h
#define _LOG_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

#include <stdarg.h>

enum LogLevel {
	Error, // Nachrichten die als Fehler eingestuft werden, k�nnen Sicherheit von Drone gef�hrden
	Info, // Nachrichten die nicht als Error oder Debug eingestuft werden
	Debug // Nachrichten die f�r Programmierer interessant sein k�nnen
};

class Log {
private:
	static const char* getLevelString(LogLevel level);
	static void print(LogLevel level, const char* tag, const char* format, va_list args);
public:
	static void error(const char* tag, const char* format, ...);
	static void info(const char* tag, const char* format, ...);
	static void debug(const char* tag, const char* format, ...);
};


#endif

