// ConfigManager.h

#ifndef _CONFIGMANAGER_h
#define _CONFIGMANAGER_h

#include "arduino.h"
#include <EEPROM.h>

#include "MemoryAdapter.h"
#include "Config.h"
#include "Log.h"
#include "EEPROM_MemoryAdapter.h"
#include "PacketBuffer.h"
#include "Profiler.h"

#define CONFIG_MAGIC_VALUE 124
#define CONFIG_VERSION 3

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

