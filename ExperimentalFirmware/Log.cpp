// 
// 
// 

#include "Log.h"

uint32_t Log::bufferLines = 0;
char** Log::_buffer;

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

uint32_t Log::getBufferLines() {
	return bufferLines;
}

char** Log::getBuffer() {
	if (_buffer == NULL)
		_buffer = (char**)malloc(LOG_BUFFER_LINES * sizeof(char*));
	return _buffer;
}

void Log::clearBuffer() {
	char** buffer = getBuffer();
	for (int i = 0; i < bufferLines; i++)
	{
		free(buffer[i]);
		buffer[i] = NULL;
	}
	bufferLines = 0;
}

char* Log::popMessage() {
	if (bufferLines == 0)
		return NULL;

	char** buffer = getBuffer();
	char* msg = buffer[0];

	for (int i = 1; i < bufferLines; i++)
		buffer[i - 1] = buffer[i];

	bufferLines--;

	return msg;
}

void Log::addMessage(char* message) {
	char** buffer = getBuffer();

	// alte Log Einträge verschieben
	if (bufferLines >= LOG_BUFFER_LINES) {
		for (int i = 1; i < LOG_BUFFER_LINES; i++)
			buffer[i - 1] = buffer[i];
		bufferLines = LOG_BUFFER_LINES - 1;
	}

	buffer[bufferLines++] = message;
}

void Log::print(LogLevel level, const char* tag, const char* format, va_list args) {
	
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