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
#include "Config.h"
#include "Log.h"
#include "EEPROM_MemoryAdapter.h"
#include "PacketBuffer.h"

#define CONFIG_MAGIC_VALUE 124
#define CONFIG_VERSION 2

class ConfigManager
{
 public:
	 static Config loadConfig();
	 static Config loadConfig(MemoryAdaptor* memory);

	 static void saveConfig(const Config config);
	 static void saveConfig(MemoryAdaptor* memory, const Config config);

	 static Config getDefault();
};

#endif

