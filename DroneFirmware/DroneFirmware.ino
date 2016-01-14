

#ifdef _VSARDUINO_H_ //Kompatibilität mit visual micro
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

/* Set these to your desired credentials. */
const char *ssid = "Kugelmatik";
const char *password = "123456abc";

uint32_t lastRevision = 0;

WiFiUDP udpControl;
byte* controlPacketBuffer;

WiFiUDP udpData;
byte* dataPacketBuffer;

bool dataFeedSubscribed = false;
IPAddress dataFeedSubscriptor;
//int dataFeedSubscriptorPort = 0;

Gyro* gyro;
ServoManager* servos;
DroneEngine* engine;


bool blinkRequested = false;
bool blinkExecuting = false;
int blinkStart = 0;

int lastLoopTime = 0;
short delayTime = 15;

//######################### Methods

enum ControlPacketType : byte {
	MovementPacket = 1,
	StopPacket = 2,
	ArmPacket = 3,
	BlinkPacket = 4,
	RawSetPacket = 5,

	AckPacket = 6,
	PingPacket = 7,
	ResetRevisionPacket = 8,

	GetInfoPacket = 9,
	SubscribeDataFeed = 10,
	UnsubscribeDataFeed = 11,

	CalibrateGyro = 12,
};

void hang() {
	while(true) wdt_reset();
}

byte* generatePacket(byte buffer[], int rev) {
	buffer[0] = 'F';
	buffer[1] = 'L';
	buffer[2] = 'Y';
	BinaryHelper::writeUint32(buffer, 3, rev);
	buffer[7] = 0;
	return buffer;
}

void handleUdpControl() {
	int packetSize = udpControl.parsePacket();


	//return wenn mindestgrösse nicht erreicht ist und leere buffer
	if(packetSize < 9) {
		for(byte i = 0; i < packetSize; i++)
			udpControl.read();
		return;
	}


	udpControl.read(controlPacketBuffer, packetSize);

	//return wenn magic value falsch ist
	if(controlPacketBuffer[0] != 'F' || controlPacketBuffer[1] != 'L' || controlPacketBuffer[2] != 'Y')
		return;

	uint32_t revision = BinaryHelper::readUint32(controlPacketBuffer, 3);
	bool ackRequested = controlPacketBuffer[7] > 0;

	ControlPacketType type = static_cast<ControlPacketType>(controlPacketBuffer[8]);

	//return wenn revision unzulässig
	if(lastRevision >= revision && type != ResetRevisionPacket && type != PingPacket)
		return;

	if(config.VerboseSerialLog) {
		Serial.print("$ Got Packet with rev ");
		Serial.print(revision);
		Serial.print(" and type ");
		Serial.println(controlPacketBuffer[8]);
	}

	bool packetHandled = false;

	switch(type) {
		case MovementPacket:


			break;
		case RawSetPacket: {
			//set the 4 motor values raw
			if(packetSize < 18)
				return;

			Serial.println(controlPacketBuffer[25]);
			uint16_t fl = BinaryHelper::readUint16(controlPacketBuffer, 9);
			uint16_t fr = BinaryHelper::readUint16(controlPacketBuffer, 11);
			uint16_t bl = BinaryHelper::readUint16(controlPacketBuffer, 13);
			uint16_t br = BinaryHelper::readUint16(controlPacketBuffer, 15);

			bool ignoreNotArmed = controlPacketBuffer[17] > 0;

			if(servos->isArmed() || ignoreNotArmed) {
				servos->setServos(fl, fr, bl, br);
			}
			packetHandled = true;
		}
		break;
		case StopPacket:
			engine->stop();
			packetHandled = true;
			break;
		case ArmPacket:
			if(packetSize == 13) {
				if(controlPacketBuffer[9] == 'A' && controlPacketBuffer[10] == 'R' && controlPacketBuffer[11] == 'M') {
					if(controlPacketBuffer[12] > 0)
						engine->arm();
					else
						engine->disarm();

					packetHandled = true;
				}
			}
			break;
		case PingPacket:
			//respond whole packet
			udpControl.beginPacket(udpControl.remoteIP(), udpControl.remotePort());
			udpControl.write(controlPacketBuffer, packetSize);
			udpControl.endPacket();
			packetHandled = true;
			break;
		case BlinkPacket:
			blinkRequested = true;
			packetHandled = true;
			break;
		case ResetRevisionPacket:
			lastRevision = 0;
			packetHandled = true;
			break;

		case GetInfoPacket: {
			byte buf[30];
			generatePacket(buf, revision);
			buf[8] = GetInfoPacket;

			buf[9] = BUILD_VERSION;
			BinaryHelper::writeUint32(buf, 10, lastRevision);
			
			buf[14] = engine->isArmed() ? 1 : 0;

			BinaryHelper::writeUint16(buf, 15, servos->FL());
			BinaryHelper::writeUint16(buf, 19, servos->FR());
			BinaryHelper::writeUint16(buf, 23, servos->BL());
			BinaryHelper::writeUint16(buf, 27, servos->BR());

			udpControl.beginPacket(udpControl.remoteIP(), udpControl.remotePort());
			udpControl.write(buf, 30);
			udpControl.endPacket();
			packetHandled = true;

			
			}
			break;
		case SubscribeDataFeed:
			dataFeedSubscriptor = udpControl.remoteIP();
			dataFeedSubscribed = true;
			packetHandled = true;
			break;

		case UnsubscribeDataFeed:
			dataFeedSubscribed = false;
			packetHandled = true;
			break;

		case CalibrateGyro:
			gyro->setAsZero();
			packetHandled = true;
			break;

		default:
			break;
	}

	if(packetHandled) {
		if(type != ResetRevisionPacket)
			lastRevision = revision;

		if(ackRequested && type != PingPacket && type != GetInfoPacket) {
			byte buf[9];
			generatePacket(buf, revision);
			buf[8] = AckPacket;

			udpControl.beginPacket(udpControl.remoteIP(), udpControl.remotePort());
			udpControl.write(buf, 9);
			udpControl.endPacket();
		}
	}
}

void handleUdpData() {
	if(!dataFeedSubscribed) return;


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

	//allocate buffer memory
	controlPacketBuffer = new byte[config.NetworkPacketBufferSize];
	dataPacketBuffer = new byte[config.NetworkPacketBufferSize];


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
	udpControl.begin(config.NetworkControlPort);
	if(config.VerboseSerialLog) {
		Serial.print("$ Started UDP on port ");
		Serial.print(config.NetworkControlPort);
		Serial.println(" for control");
	}

	udpData.begin(config.NetworkDataPort);
	if(config.VerboseSerialLog) {
		Serial.print("$ Started UDP on port ");
		Serial.print(config.NetworkDataPort);
		Serial.println(" for data");
	}

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
		handleUdpControl();
		handleUdpData();
		handleBlink();
		
		lastLoopTime = millis();
	}
}

