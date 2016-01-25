// 
// 
// 

#include "ConfigManager.h"

Config ConfigManager::loadConfig() {
	EEPROM_MemoryAdapter* adapter = new EEPROM_MemoryAdapter(1024, 64);

	adapter->begin();
	Config config = loadConfig(adapter);
	adapter->end();

	delete adapter;
	return config;
}

Config ConfigManager::loadConfig(MemoryAdaptor* memory) {
	if (memory->readByte(0) != 123) {
		return getDefault();
	}

	Config* config = (Config*)malloc(sizeof(Config));
	memory->read(1, (byte*)config, sizeof(Config)); // read main structure

	// read strings
	PacketBuffer* buffer = new PacketBuffer(128);
	memory->read(128, buffer->getBuffer(), buffer->getBufferSize());

	config->DroneName = buffer->readString();
	config->NetworkSSID = buffer->readString();
	config->NetworkPassword = buffer->readString();

	delete buffer;


	Log::info("Config", "Config loaded");
	return *config; 
}

void ConfigManager::saveConfig(const Config config) {
	EEPROM_MemoryAdapter* adapter = new EEPROM_MemoryAdapter(1024, 64);

	adapter->begin();
	saveConfig(adapter, config);
	adapter->end();

	delete adapter;
}

void ConfigManager::saveConfig(MemoryAdaptor* memory, const Config config) {
	memory->writeByte(0, 123);

	memory->write(1, (byte*)(&config), sizeof(Config)); // write main structure

	// write strings
	PacketBuffer* buffer = new PacketBuffer(128);
	buffer->writeString(config.DroneName);
	buffer->writeString(config.NetworkSSID);
	buffer->writeString(config.NetworkPassword);

	memory->write(128, buffer->getBuffer(), buffer->getBufferSize());
	
	delete buffer;

	Log::info("Config", "Config saved");
}

Config ConfigManager::getDefault() {
	Config config;

	config.DroneName = "koalaDrone";

	config.NetworkSSID = "Drone";
	config.NetworkPassword = "12345678"; 

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
	config.ServoHover = 1280;

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
	config.PhysicsCalcDelay = 20;

	config.EngineType = PID;

	config.PitchPidSettings.Kp = 1;
	config.PitchPidSettings.Ki = 0.05;
	config.PitchPidSettings.Kd = 0.25;

	config.RollPidSettings.Kp = 1;
	config.RollPidSettings.Ki = 0.05;
	config.RollPidSettings.Kd = 0.25;

	config.YawPidSettings.Kp = 1;
	config.YawPidSettings.Ki = 0.05;
	config.YawPidSettings.Kd = 0.25;
	

	Log::info("Config", "Using default config");
	return config;
}
