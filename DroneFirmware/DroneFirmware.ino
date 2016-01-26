
#ifdef _VSARDUINO_H_ //Kompatibilität mit visual micro
#include <Wire/Wire.h>
#include <I2Cdev/I2Cdev.h>
#include <MPU6050/MPU6050_6Axis_MotionApps20.h>
#include <Servo/src/Servo.h>
#include <ESP8266WiFi/src/WiFiUdp.h>
#include <ESP8266WiFi/src/ESP8266WiFi.h>
#include <EEPROM/EEPROM.h>
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

#define byte unsigned char

#else
#include <Wire.h>
#include <MPU6050/MPU6050_6Axis_MotionApps20.h>
#include <I2Cdev/I2Cdev.h>
#include <WiFiUdp.h>
#include <ESP8266WiFi.h>
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
#endif


// #################### Global Variables #####################

//The configuration of the drone
Config config;

VoltageInputReader* voltageReader;
Gyro* gyro;
ServoManager* servos;
DroneEngine* engine;
NetworkManager* network;


int lastLoopTime = 0;
short delayTime = 10;

//######################### Methods

void hang() {
	while(true) wdt_reset();
}



void setup() {
	Serial.begin(74880);
	Log::info("Boot", "Drone v%d booting...", BUILD_VERSION);
	Log::info("Boot", "Model: %s, Build: %s", MODEL_NAME, BUILD_NAME);

	char serialCode[32];
	getBuildSerialCode(serialCode, sizeof(serialCode));
	Log::info("Boot", "Serial code: %s", serialCode);

	config = ConfigManager::loadConfig();

	//setup servos
	servos = new ServoManager(&config);
	servos->init(config.PinFrontLeft, config.PinFrontRight, config.PinBackLeft, config.PinBackRight);

	Log::info("Boot", "Init network...");

	setupLED(&config);

	WiFi.mode(WIFI_STA);
	WiFi.begin(config.NetworkSSID, config.NetworkPassword);
	int connectStartTime = millis();
	while(WiFi.waitForConnectResult() != WL_CONNECTED && millis() - connectStartTime >= 5000) {
		delay(20);
	}

	if(WiFi.waitForConnectResult() != WL_CONNECTED) {
		Log::info("Boot", "Access point not found, creating own ...");

		WiFi.mode(WIFI_AP);
		WiFi.softAP(config.NetworkSSID, config.NetworkPassword);

		Log::info("Boot", "AP IP address: %s", WiFi.softAPIP().toString().c_str());
	} 
	else {
		Log::info("Boot", "Successfully connected to access point");
		Log::info("Boot", "IP address: %s", WiFi.localIP().toString().c_str());
	}


	//setup MPU6050
	gyro = new Gyro(&config);
	Wire.begin(SDA, SCL);
	gyro->init();

	//setup battery input reader
	voltageReader = new VoltageInputReader(A0, 17, 1);

	//setup calculation engine
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

	//start network
	network = new NetworkManager(gyro, servos, engine, &config, voltageReader);

	Log::info("Boot", "done booting. ready.");
}

void loop() {
	//keep gyro data updated
	gyro->update();

	//handle drone physics
	engine->handle();

	// handle LED
	handleBlink();

	if(millis() - lastLoopTime >= delayTime) {
		network->handlePackets();
		
		lastLoopTime = millis();
	}
}

