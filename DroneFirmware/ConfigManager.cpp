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
	EEPROM_MemoryAdapter* adapter = new EEPROM_MemoryAdapter(1024, 64);

	adapter->begin();
	saveConfig(adapter, config);
	adapter->end();

	delete adapter;
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

	strcpy(config.DroneName, "koalaDrone");

	// leere String, siehe NetworkSSID
	strcpy(config.NetworkSSID, ""); 
	strcpy(config.NetworkPassword, "12345678");

	config.NetworkHelloPort = 4710;
	config.NetworkControlPort = 4711;
	config.NetworkDataPort = 4712;
	config.NetworkPacketBufferSize = 512;
	config.MaximumNetworkTimeout = 500;

	config.VerboseSerialLog = true;
	config.MaxTemperature = 60;

	config.TrimPitch = 0;
	config.TrimRoll = 0;
	config.TrimYaw = 0;
	config.TrimThrottle = 0;

	config.ServoMin = 900;
	config.ServoMax = 2000;
	config.ServoIdle = 975;
	config.ServoHover = 1200;

	config.DMPOffsetX = 220;
	config.DMPOffsetY = 76;
	config.DMPOffsetZ = -85;
	config.DMPOffsetAccel = 1788;

	config.PinFrontLeft = 12;
	config.PinFrontRight = 13;
	config.PinBackLeft = 16;
	config.PinBackRight = 14;
	config.PinLed = 0;

	config.Degree2Ratio = 0.05f;
	config.RotaryDegree2Ratio = 0.05f;
	config.PhysicsCalcDelay = 20;

	config.EngineType = EngineLinear;

	config.PitchPidSettings.Kp = 1;
	config.PitchPidSettings.Ki = 0.05;
	config.PitchPidSettings.Kd = 0.25;

	config.RollPidSettings.Kp = 1;
	config.RollPidSettings.Ki = 0.05;
	config.RollPidSettings.Kd = 0.25;

	config.YawPidSettings.Kp = 1;
	config.YawPidSettings.Ki = 0.05;
	config.YawPidSettings.Kd = 0.25;

	config.InterpolationFactor = 0.5f;
	config.CorrectionFactor = 0;
	

	Log::info("Config", "Using default config");
	return config;
}
