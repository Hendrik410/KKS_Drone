// Log.h

#ifndef _LOG_h
#define _LOG_h

#include "arduino.h"
#include <stdarg.h>

enum LogLevel {
	Error, // Nachrichten die als Fehler eingestuft werden, k�nnen Sicherheit von Drone gef�hrden
	Info, // Nachrichten die nicht als Error oder Debug eingestuft werden
	Debug // Nachrichten die f�r Programmierer interessant sein k�nnen
};

#define LOG_BUFFER_LINES 32

class Log {
private:
	static bool printToSerial;
	static uint32_t bufferLines;
	static char** _buffer;

	static void addMessage(char* str);

	static const char* getLevelString(LogLevel level);
	static void print(LogLevel level, const char* tag, const char* format, va_list args);
public:
	static void error(const char* tag, const char* format, ...);
	static void info(const char* tag, const char* format, ...);
	static void debug(const char* tag, const char* format, ...);

	static uint32_t getBufferLines();
	static char** getBuffer();
	static void clearBuffer();
	static char* popMessage();

	static void setPrintToSerial(bool value);
};


#endif

