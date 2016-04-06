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
	// wir nutzen erstes Byte um zu Erkennen ob schon Daten geschrieben wurden
	if (memory->readByte(0) != CONFIG_MAGIC_VALUE) {
		Log::info("Config", "Saved magic value does not match excepted magic value");
		return getDefault();
	}

	if (memory->readByte(1) != CONFIG_VERSION) {
		Log::info("Config", "Saved config version does not match excepted version");
		return getDefault();
	}

	// nach Magic Value folgt ein uint16_t für die Größe der Config
	uint8_t buffer[sizeof(uint16_t)];
	memory->read(2, buffer, sizeof(buffer));

	uint16_t size = BinaryHelper::readUint16(buffer, 0);

	// über die Größe erkennen wir ob sich die Structure geändert hat
	if (size != sizeof(Config)) {
		Log::info("Config", "Config size does not match saved size");
		return getDefault();
	}

	// nach der Größe folgen unsere eigentliche Daten
	Config* config = (Config*)malloc(sizeof(Config));
	memory->read(4, (byte*)config, sizeof(Config));

	Log::info("Config", "Config loaded");
	return *config;
}

void ConfigManager::saveConfig(const Config config) {
	Profiler::begin("saveConfig()");
	EEPROM_MemoryAdapter* adapter = new EEPROM_MemoryAdapter(1024, 64);

	adapter->begin();
	saveConfig(adapter, config);
	adapter->end();

	delete adapter;
	Profiler::end();
}

void ConfigManager::saveConfig(MemoryAdaptor* memory, const Config config) {
	// Magic Value speichern
	memory->writeByte(0, CONFIG_MAGIC_VALUE);

	memory->writeByte(1, CONFIG_VERSION);

	// Größe der Config Structure speichern
	uint8_t buffer[sizeof(uint16_t)];
	BinaryHelper::writeUint16(buffer, 0, sizeof(Config));
	memory->write(2, buffer, sizeof(buffer));

	// eigentliche Daten speichern
	memory->write(4, (byte*)(&config), sizeof(Config));

	Log::info("Config", "Config saved");
}

Config ConfigManager::getDefault() {
	Config config;

	strncpy(config.DroneName, "koalaDrone", sizeof(config.DroneName));

	strncpy(config.NetworkSSID, "", sizeof(config.NetworkSSID));
	strncpy(config.NetworkPassword, "", sizeof(config.NetworkPassword));

	strncpy(config.AccessPointPassword, "12345678", sizeof(config.AccessPointPassword));

	config.NetworkHelloPort = 4710;
	config.NetworkControlPort = 4711;
	config.NetworkDataPort = 4712;
	config.NetworkPacketBufferSize = 512;
	config.MaximumNetworkTimeout = 1500;

	config.VerboseSerialLog = true;
	config.MaxTemperature = 60;

	config.ServoMin = 900;
	config.ServoMax = 2000;
	config.ServoIdle = 975;
	config.ServoHover = 1100;

	config.PinFrontLeft = 12;
	config.PinFrontRight = 13;
	config.PinBackLeft = 16;
	config.PinBackRight = 14;
	config.PinLed = 0;

	config.PitchPid.Kp = 15;
	config.PitchPid.Ki = 10;
	config.PitchPid.Kd = 3;

	config.RollPid.Kp = 15;
	config.RollPid.Ki = 10;
	config.RollPid.Kd = 3;

	config.YawPid.Kp = 10;
	config.YawPid.Ki = 0;
	config.YawPid.Kd = 0;

	config.SafePitch = 60;
	config.SafeRoll = 60;
	config.SafeServoValue = 1500;

	Log::info("Config", "Using default config");
	return config;
}
