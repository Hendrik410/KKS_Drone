#include <Wire.h>
#include <I2Cdev.h>
#include <WiFiUdp.h>
#include <ESP8266WiFi.h>
#include <MPU6050/MPU6050_6Axis_MotionApps20.h>
#include <Servo.h>
#include <EEPROM.h>
#include "Build.h"
#include "Log.h"
#include "NetworkManager.h"
#include "Config.h"
#include "ConfigManager.h"
#include "EEPROM_MemoryAdapter.h"
#include "MemoryAdapter.h"
#include "MotorEnums.h"
#include "MathHelper.h"
#include "DroneEngine.h"
#include "ServoManager.h"
#include "BinaryHelper.h"
#include "LED.h"
#include "PidDroneEngine.h"
#include "LinearDroneEngine.h"
#include "VoltageInputReader.h"
#include "Profiler.h"
#include "Gyro6050.h"
#include "Gyro9150.h"

Config config;

VoltageInputReader* voltageReader;
Gyro* gyro;
ServoManager* servos;
DroneEngine* engine;
NetworkManager* network;

int lastLoopTime = 0;
short delayTime = 10;

void setup() {
	Serial.begin(74880);
	Serial.println();

	Log::info("Boot", "=====================");
	Log::info("Boot", "Drone v%d booting...", BUILD_VERSION);
	Log::info("Boot", "Model: %s", MODEL_NAME);
	Log::info("Boot", "Build: %s", BUILD_NAME);

	// Serialnummer schreiben
	char serialCode[32];
	getBuildSerialCode(serialCode, sizeof(serialCode));
	Log::info("Boot", "Serial code: %s", serialCode);

	config = ConfigManager::getDefault(); // ConfigManager::loadConfig();

	// Log setzen
	Log::setPrintToSerial(config.VerboseSerialLog);

	// ServoManager initialisieren
	servos = new ServoManager(&config);
	servos->init(config.PinFrontLeft, config.PinFrontRight, config.PinBackLeft, config.PinBackRight);

	Log::info("Boot", "Init network...");

	setupLED(&config);

	bool openOwnNetwork = true;

	// Hostname setzen
	char name[30];
	strncpy(name, config.DroneName, sizeof(name));
	strncat(name, "-", sizeof(name));
	strncat(name, serialCode, sizeof(name));

	WiFi.persistent(false);
	WiFi.hostname(name);
	WiFi.setOutputPower(20.5f);
	WiFi.setPhyMode(WIFI_PHY_MODE_11N);

	// versuchen mit dem eingestellen AP zu verbinden
	if (strlen(config.NetworkSSID) > 0) {
		Log::info("Boot", "Trying to connect to %s", config.NetworkSSID);

		WiFi.mode(WIFI_STA);
		WiFi.begin(config.NetworkSSID, config.NetworkPassword);

		int connectStartTime = millis();
		while (WiFi.status() != WL_CONNECTED && millis() - connectStartTime >= 5000) {
			delay(20);
		}
		openOwnNetwork = WiFi.status() != WL_CONNECTED;

		if (openOwnNetwork)
			Log::info("Boot", "Access point not found!");
		else {
			Log::info("Boot", "Successfully connected to access point");
			Log::info("Boot", "IP address: %s", WiFi.localIP().toString().c_str());
		}
	}

	// eigenen AP erstellen
	if (openOwnNetwork) {
		Log::info("Boot", "Creating own network...");

		Log::info("Boot", "Network SSID: %s", name);

		WiFi.mode(WIFI_AP);
		WiFi.softAP(name, config.NetworkPassword);

		Log::info("Boot", "AP IP address: %s", WiFi.softAPIP().toString().c_str());
	}


	// Gyro Sensor initialisieren
	gyro = new Gyro9150(&config);
	if (!gyro->init()) { // Gyro9150 Init fehlgeschlagen
		delete gyro; 

		// versuchen Gyro6050 zu initalisieren
		gyro = new Gyro6050(&config);
		gyro->init();
	}

	// Batterie Voltage Reader laden
	voltageReader = new VoltageInputReader(A0, 17, 1);

	// DroneEngine laden
	switch(config.EngineType) {
		case EnginePID:
			engine = new PidDroneEngine(gyro, servos, &config);
			break;
		case EngineLinear:
			engine = new LinearDroneEngine(gyro, servos, &config);
			break;
		default:
			return;
	}

	// Netzwerkmanager starten
	network = new NetworkManager(gyro, servos, engine, &config, voltageReader);

	// Profiler laden
	Profiler::init();

	Log::info("Boot", "done booting. ready.");
}

void loop() {
	Profiler::begin("loop()");
	
	gyro->update();
	engine->handle();
	handleBlink();

	if (engine->state() == StateArmed)
		servos->handleTick();

	network->handlePackets();

	Profiler::end();

	if(millis() - lastLoopTime >= delayTime) {
		network->handleData();
		
		lastLoopTime = millis();
	}
}

