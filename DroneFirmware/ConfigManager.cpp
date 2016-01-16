// 
// 
// 

#include "ConfigManager.h"

Config ConfigManager::loadConfig(MemoryAdaptor* memory) {
	Config config;
	byte buf[sizeof(config)];
	
	unsigned char name[20];
	unsigned char ssid[20];
	unsigned char password[20];

	int stringPos = 128; // all strings start after this address
	memory->begin();
	memory->read(0, buf, sizeof(config)); // read main structure

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

	return config; //successful
}

void ConfigManager::saveConfig(MemoryAdaptor* memory, const Config config) {
	byte buf[sizeof(config)];
	memcpy(buf, &config, sizeof(config));

	byte* name = reinterpret_cast<byte*>(config.DroneName);
	byte* ssid = reinterpret_cast<byte*>(config.NetworkSSID);
	byte* password = reinterpret_cast<byte*>(config.NetworkPassword);

	int stringPos = 128; // all strings start after this address
	memory->begin();

	memory->write(0, buf, sizeof(config)); // write main structure

	// write strings
	memory->write(stringPos, name, 20);
	stringPos += 20;

	memory->read(stringPos, ssid, 20);
	stringPos += 20;

	memory->read(stringPos, password, 20);
	stringPos += 20;

	memory->end();

}

Config ConfigManager::getDefault() {
	Config config;

	config.DroneName = "Drone";

	config.NetworkSSID = "Drone";
	config.NetworkPassword = "12345678";

	config.NetworkHelloPort = 4710;
	config.NetworkControlPort = 4711;
	config.NetworkDataPort = 4712;
	config.NetworkPacketBufferSize = 128;

	config.VerboseSerialLog = true;
	config.MaxTemperature = 60;

	config.TrimPitch = 0;
	config.TrimRoll = 0;
	config.TrimYaw = 0;
	config.TrimThrottle = 0;

	config.ServoMin = 1100;
	config.ServoMax = 1900;
	config.ServoIdle = 1200;
	config.ServoHover = 1500;

	config.DMPOffsetX = 220;
	config.DMPOffsetY = 76;
	config.DMPOffsetZ = -85;
	config.DMPOffsetAccel = 1788;

	config.PinFrontLeft = 12;
	config.PinFrontRight = 13;
	config.PinBackLeft = 16;
	config.PinBackRight = 14;
	config.PinLed = 0;

	return config;
}
