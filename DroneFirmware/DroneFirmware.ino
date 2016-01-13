

#ifdef _VSARDUINO_H_ //Kompatibilität mit visual micro
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
#define VERBOSE_SERIAL_PRINT true

#define CONTROL_PORT 4711
#define DATA_PORT 4712

#define PACKET_BUFFER_SIZE 128

#define SERVO_FR_PIN 16
#define SERVO_FL_PIN 14
#define SERVO_BR_PIN 12
#define SERVO_BL_PIN 13

#define LED_PIN 0

// #################### Global Variables #####################

/* Set these to your desired credentials. */
const char *ssid = "drone1";
const char *password = "12345678";

uint32_t lastRevision = 0;

WiFiUDP udpControl;
byte controlPacketBuffer[PACKET_BUFFER_SIZE];

WiFiUDP udpData;
byte dataPacketBuffer[PACKET_BUFFER_SIZE];

bool dataFeedSubscribed = false;
IPAddress dataFeedSubscriptor;
//int dataFeedSubscriptorPort = 0;

Gyro gyro(VERBOSE_SERIAL_PRINT);
ServoManager servos(900, 1100, 1900, 1500, VERBOSE_SERIAL_PRINT);
DroneEngine engine(&gyro, &servos, VERBOSE_SERIAL_PRINT);


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

	Serial.println("$ Got Packet");


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

	if(VERBOSE_SERIAL_PRINT) {
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

			if(servos.isArmed() || ignoreNotArmed) {
				servos.setServos(fl, fr, bl, br);
			}
			packetHandled = true;
		}
		break;
		case StopPacket:
			engine.stop();
			packetHandled = true;
			break;
		case ArmPacket:
			if(packetSize == 13) {
				if(controlPacketBuffer[9] == 'A' && controlPacketBuffer[10] == 'R' && controlPacketBuffer[11] == 'M') {
					if(controlPacketBuffer[12] > 0)
						engine.arm();
					else
						engine.disarm();

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
			
			buf[14] = engine.isArmed() ? 1 : 0;

			BinaryHelper::writeUint16(buf, 15, servos.FL());
			BinaryHelper::writeUint16(buf, 19, servos.FR());
			BinaryHelper::writeUint16(buf, 23, servos.BL());
			BinaryHelper::writeUint16(buf, 27, servos.BR());

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
			gyro.setAsZero();
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
			digitalWrite(LED_PIN, LOW);
			blinkExecuting = false;
		}
	} else if(blinkRequested) {
		blinkStart = millis();
		digitalWrite(LED_PIN, HIGH);
		blinkRequested = false;
		blinkExecuting = true;
	}
}


void setup() {
	pinMode(LED_PIN, OUTPUT);
	digitalWrite(LED_PIN, HIGH);

	Serial.begin(74880);
	if(VERBOSE_SERIAL_PRINT)
		Serial.println("$ Drone V1 booting");


	if(VERBOSE_SERIAL_PRINT)
		Serial.println("$ Configuring access point");
	//setup ap
	WiFi.softAP(ssid, password);
	IPAddress myIP = WiFi.softAPIP();
	if(VERBOSE_SERIAL_PRINT) {
		Serial.print("AP IP address: ");
		Serial.println(myIP);
	}

	//start udp servers
	udpControl.begin(CONTROL_PORT);
	if(VERBOSE_SERIAL_PRINT) {
		Serial.print("$ Started UDP on port ");
		Serial.print(CONTROL_PORT);
		Serial.println(" for control");
	}

	udpData.begin(DATA_PORT);
	if(VERBOSE_SERIAL_PRINT) {
		Serial.print("$ Started UDP on port ");
		Serial.print(DATA_PORT);
		Serial.println(" for data");
	}

	//setup servos
	servos.init(SERVO_FL_PIN, SERVO_FR_PIN, SERVO_BL_PIN, SERVO_BR_PIN);


	//setup MPU6050
	Wire.begin(SDA, SCL);
	gyro.init();

	
	

	digitalWrite(LED_PIN, LOW);
	if(VERBOSE_SERIAL_PRINT)
		Serial.println("$ Drone boot successfully");
}

void loop() {
	//keep gyro data updated
	gyro.update();

	//handle drone physics
	engine.handle();

	if(millis() - lastLoopTime >= delayTime) {
		handleUdpControl();
		handleUdpData();
		handleBlink();
		
		lastLoopTime = millis();
	}
}

