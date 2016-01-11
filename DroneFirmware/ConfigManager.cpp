// 
// 
// 

#include "ConfigManager.h"

bool ConfigManager::loadConfig(MemoryAdaptor* memory) {
	memory->begin();

	//ToDo load config

	memory->end();

	return true; //successful
}

bool ConfigManager::saveConfig(MemoryAdaptor* memory) {
	memory->begin();

	//ToDo save Config

	memory->end();

	return true; //successful
}
