#include <Wire.h>
#include <I2Cdev.h>
#include <WiFiUdp.h>
#include <ESP8266WiFi.h>
#include <MPU6050_6Axis_MotionApps20.h>
#include <Servo.h>
#include <EEPROM.h>
#include "Build.h"
#include "Log.h"
#include "NetworkManager.h"
#include "Config.h"
#include "ConfigManager.h"
#include "EEPROM_MemoryAdapter.h"
#include "MemoryAdapter.h"
#include "MathHelper.h"
#include "DroneEngine.h"
#include "ServoManager.h"
#include "BinaryHelper.h"
#include "LED.h"
#include "VoltageInputReader.h"
#include "Profiler.h"
#include "Gyro6050.h"
#include "Gyro9150.h"
#include "PID.h"

Config config;

VoltageInputReader* voltageReader;
Gyro* gyro;
ServoManager* servos;
DroneEngine* engine;
NetworkManager* network;

int lastLoopTime = 0;
short delayTime = 10;

void setup() {
	Serial.begin(115200);
	Serial.println();

	Log::info("Boot", "=====================");
	Log::info("Boot", "Drone v%d booting...", BUILD_VERSION);
	Log::info("Boot", "Model: %s", MODEL_NAME);
	Log::info("Boot", "Build: %s", BUILD_NAME);

	ESP.eraseConfig();

	// Serialnummer schreiben
	char serialCode[32];
	getBuildSerialCode(serialCode, sizeof(serialCode));
	Log::info("Boot", "Serial code: %s", serialCode);

	config = ConfigManager::getDefault(); // ConfigManager::loadConfig();

	// Log setzen
	Log::setPrintToSerial(config.VerboseSerialLog);

	// ServoManager initialisieren
	servos = new ServoManager(&config);
	servos->attach();

	setupLED(&config);

	Log::info("Boot", "Init network...");
	bool openOwnNetwork = true;

	// Hostname generieren
	char name[30];
	strncpy(name, config.DroneName, sizeof(name));
	strncat(name, "-", sizeof(name));
	strncat(name, serialCode, sizeof(name));

	// WiFi Einstellungen setzen
	WiFi.hostname(name); 
	WiFi.setOutputPower(20.5f); 
	WiFi.setPhyMode(WIFI_PHY_MODE_11N);

	// versuchen mit dem in der Config gespeicherten AP zu verbinden
	if (strlen(config.NetworkSSID) > 0) {
		Log::info("Boot", "Trying to connect to %s", config.NetworkSSID);

		WiFi.mode(WIFI_STA);
		if (WiFi.begin(config.NetworkSSID, config.NetworkPassword) == WL_CONNECT_FAILED)
			Log::error("Boot", "WiFi.begin() failed");
		if (WiFi.status() == WL_IDLE_STATUS) // begin() verbindet nicht wenn die Einstellungen sich nicht geändert haben
			WiFi.begin(); // daher erneut probieren

		// auf Verbindung warten
		openOwnNetwork = WiFi.waitForConnectResult() != WL_CONNECTED; 

		if (openOwnNetwork) {
			Log::error("Boot", "Could not connect to the access point!");
			Log::error("Boot", "Status: %d", WiFi.status());
		}
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
		WiFi.softAP(name, config.AccessPointPassword);

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

	Log::info("Boot", "Gyro sensor: \"%s\"", gyro->name());
	Log::info("Boot", "Magnetometer: \"%s\"", gyro->magnetometerName());

	// Batterie Voltage Reader laden
	voltageReader = new VoltageInputReader(A0, 17, 1);

	engine = new DroneEngine(gyro, servos, &config);

	// Netzwerkmanager starten
	network = new NetworkManager(gyro, servos, engine, &config, voltageReader);

	// Profiler laden
	Profiler::init();

	Log::info("Boot", "done booting. ready.");
}

void loop() {
	Profiler::begin("loop()");

	network->handlePackets();

	if (engine->state() != StateOTA)
	{
		if (config.EnableGyro)
			gyro->update();
		engine->handle();
	}
	handleBlink();

	if (engine->state() == StateArmed)
		servos->handleTick();

	Profiler::end();

	if(millis() - lastLoopTime >= delayTime) {
		network->handleData();
		
		lastLoopTime = millis();
	}
}

