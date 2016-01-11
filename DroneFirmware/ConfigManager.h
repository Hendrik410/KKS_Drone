// ConfigManager.h

#ifndef _CONFIGMANAGER_h
#define _CONFIGMANAGER_h

#include "arduino.h"

#ifdef _VSARDUINO_H_ //Kompatibilität mit visual micro
#include <EEPROM/EEPROM.h>

#define byte unsigned char
void * memcpy(void * destination, const void * source, int num);
#else
#include <EEPROM.h>
#endif

#include "MemoryAdapter.h"

class ConfigManager
{
 protected:


 public:
	 bool loadConfig(MemoryAdaptor* memory);
	 bool saveConfig(MemoryAdaptor* memory);
};

#endif

