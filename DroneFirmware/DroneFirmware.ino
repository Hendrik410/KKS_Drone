

#ifdef _VSARDUINO_H_ //Kompatibilität mit visual micro
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
#include <Wire/Wire.h>
#include <I2Cdev/I2Cdev.h>
#include <MPU6050/MPU6050_6Axis_MotionApps20.h>
#include "Gyro.h"
#include <Servo/src/Servo.h>
#include <ESP8266WiFi/src/WiFiUdp.h>
#include <ESP8266WiFi/src/ESP8266WiFi.h>
#include <EEPROM/EEPROM.h>

#define byte unsigned char

#else
#include "NetworkManager.h"
#include "Config.h"
#include "ConfigManager.h"
#include "EEPROM_MemoryAdapter.h"
#include "MemoryAdapter.h"
#include "MotorEnums.h"
#include "MathHelper.h"
#include "DroneEngine.h"
#include <Wire.h>
#include <MPU6050/MPU6050_6Axis_MotionApps20.h>
#include <I2Cdev/I2Cdev.h>
#include "ServoManager.h"
#include "BinaryHelper.h"
#include "Gyro.h"
#include <WiFiUdp.h>
#include <ESP8266WiFi.h>
#include <Servo.h>
#include <EEPROM.h>
#endif

#define BUILD_VERSION 1

// #################### Global Variables #####################

//The configuration of the drone
Config config;

bool dataFeedSubscribed = false;
IPAddress dataFeedSubscriptor;
//int dataFeedSubscriptorPort = 0;

Gyro* gyro;
ServoManager* servos;
DroneEngine* engine;
NetworkManager* network;


bool blinkRequested = false;
bool blinkExecuting = false;
int blinkStart = 0;

int lastLoopTime = 0;
short delayTime = 15;

//######################### Methods

void hang() {
	while(true) wdt_reset();
}


void handleBlink() {
	if(blinkExecuting) {
		if(millis() - blinkStart > 250) {
			digitalWrite(config.PinLed, LOW);
			blinkExecuting = false;
		}
	} else if(blinkRequested) {
		blinkStart = millis();
		digitalWrite(config.PinLed, HIGH);
		blinkRequested = false;
		blinkExecuting = true;
	}
}


void setup() {

	Serial.begin(74880);

	config = ConfigManager::getDefault();
	if(config.VerboseSerialLog)
		Serial.println("$ Drone V1 booting\n$ Configuration loaded");

	//setup status LED
	pinMode(config.PinLed, OUTPUT);
	digitalWrite(config.PinLed, HIGH);


	if(config.VerboseSerialLog)
		Serial.println("$ Configuring network");


	WiFi.mode(WIFI_STA);
	WiFi.begin(config.NetworkSSID, config.NetworkPassword);
	int connectStartTime = millis();
	while(WiFi.waitForConnectResult() != WL_CONNECTED && millis() - connectStartTime >= 5000) {
		delay(20);
	}

	if(WiFi.waitForConnectResult() != WL_CONNECTED) {
		if(config.VerboseSerialLog)
			Serial.println("$ Access Point not found, creating own ...");

		WiFi.mode(WIFI_AP);
		//setup ap
		WiFi.softAP(config.NetworkSSID, config.NetworkPassword);
		IPAddress myIP = WiFi.softAPIP();
		if(config.VerboseSerialLog) {
			Serial.print("$ AP IP address: ");
			Serial.println(myIP);
		}
	} else {
		if(config.VerboseSerialLog) {
			Serial.println("$ Successfully connected to acces point");
			IPAddress myIP = WiFi.localIP();
			Serial.print("$ IP Adress: ");
			Serial.println(myIP);
		}

	}


	//start udp servers
	network = new NetworkManager(gyro, servos, engine, &config);

	//setup servos
	servos = new ServoManager(&config);
	servos->init(config.PinFrontLeft, config.PinFrontRight, config.PinBackLeft, config.PinBackRight);


	//setup MPU6050
	gyro = new Gyro(&config);
	Wire.begin(SDA, SCL);
	gyro->init();

	
	engine = new DroneEngine(gyro, servos, &config);

	digitalWrite(config.PinLed, LOW);
	if(config.VerboseSerialLog)
		Serial.println("$ Drone boot successfully");
}

void loop() {
	//keep gyro data updated
	gyro->update();

	//handle drone physics
	engine->handle();

	if(millis() - lastLoopTime >= delayTime) {
		network->handlePackets();

		handleBlink();
		
		lastLoopTime = millis();
	}
}

