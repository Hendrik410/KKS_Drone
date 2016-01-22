// 
// 
// 

#include "ConfigManager.h"

Config ConfigManager::loadConfig() {
	EEPROM_MemoryAdapter* adapter = new EEPROM_MemoryAdapter(1024, 64);
	Config config = loadConfig(adapter);
	delete adapter;
	return config;
}

Config ConfigManager::loadConfig(MemoryAdaptor* memory) {
	if (memory->readByte(0) != 123)
		return getDefault();

	Config config;
	byte buf[sizeof(config)];
	
	unsigned char name[20];
	unsigned char ssid[20];
	unsigned char password[20];

	int stringPos = 128; // all strings start after this address
	memory->begin();
	memory->read(1, buf, sizeof(config)); // read main structure

	// read strings
	memory->read(stringPos, name, sizeof(name)); 
	stringPos += sizeof(name);

	memory->read(stringPos, ssid, sizeof(ssid)); 
	stringPos += sizeof(ssid);

	memory->read(stringPos, password, sizeof(password));
	stringPos += sizeof(password);
	memory->end();

	memcpy(&config, buf, sizeof(config));

	config.DroneName = reinterpret_cast<char*>(name);
	config.NetworkSSID = reinterpret_cast<char*>(ssid);
	config.NetworkPassword = reinterpret_cast<char*>(password);

	Log::info("Config", "Config loaded");
	return config; 
}

void ConfigManager::saveConfig(const Config config) {
	EEPROM_MemoryAdapter* adapter = new EEPROM_MemoryAdapter(1024, 64);
	saveConfig(adapter, config);
	delete adapter;
}

void ConfigManager::saveConfig(MemoryAdaptor* memory, const Config config) {
	byte buf[sizeof(config)];
	memcpy(buf, &config, sizeof(config));

	byte* name = reinterpret_cast<byte*>(config.DroneName);
	byte* ssid = reinterpret_cast<byte*>(config.NetworkSSID);
	byte* password = reinterpret_cast<byte*>(config.NetworkPassword);

	int stringPos = 128; // all strings start after this address
	memory->begin();

	memory->writeByte(0, 123);
	memory->write(1, buf, sizeof(config)); // write main structure

	// write strings
	memory->write(stringPos, name, 20);
	stringPos += 20;

	memory->read(stringPos, ssid, 20);
	stringPos += 20;

	memory->read(stringPos, password, 20);
	stringPos += 20;

	memory->end();

	Log::info("Config", "Config saved");
}

Config ConfigManager::getDefault() {
	Config config;

	config.DroneName = "koalaDrone";

	config.NetworkSSID = "Kugelmatik";
	config.NetworkPassword = "123456abc";

	config.NetworkHelloPort = 4710;
	config.NetworkControlPort = 4711;
	config.NetworkDataPort = 4712;
	config.NetworkPacketBufferSize = 512;

	config.VerboseSerialLog = true;
	config.MaxTemperature = 60;

	config.TrimPitch = 0;
	config.TrimRoll = 0;
	config.TrimYaw = 0;
	config.TrimThrottle = 0;

	config.ServoMin = 900;
	config.ServoMax = 1400;
	config.ServoIdle = 1200;
	config.ServoHover = 1300;

	config.DMPOffsetX = 220;
	config.DMPOffsetY = 76;
	config.DMPOffsetZ = -85;
	config.DMPOffsetAccel = 1788;

	config.PinFrontLeft = 12;
	config.PinFrontRight = 13;
	config.PinBackLeft = 16;
	config.PinBackRight = 14;
	config.PinLed = 0;

	config.Degree2Ratio = 0.03f;
	config.RotaryDegree2Ratio = 0.03f;

	Log::info("Config", "Using default config");
	return config;
}
