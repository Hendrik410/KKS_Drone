

#ifdef _VSARDUINO_H_ 

#include <WiFiUdp.h>
#include <WiFiServer.h>
#include <WiFiClientSecure.h>
#include <WiFiClient.h>
#include <ESP8266WiFiMulti.h>
#include <ESP8266WiFi.h>
#define byte unsigned char

#else
#include <WiFiUdp.h>
#include <WiFiServer.h>
#include <WiFiClientSecure.h>
#include <WiFiClient.h>
#include <ESP8266WiFiMulti.h>
#include <ESP8266WiFi.h>
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

int lastRevision = 0;

WiFiUDP udpControl;
byte controlPacketBuffer[PACKET_BUFFER_SIZE];

WiFiUDP udpData;
byte dataPacketBuffer[PACKET_BUFFER_SIZE];

bool dataFeedSubscribed = false;
//IPAddress dataFeedSubscriptor = nullptr;
//int dataFeedSubscriptorPort = 0;

//ServoManager servos(900, 1100, 1900, VERBOS_SERIAL_PRINT);


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
	buffer[3] = byte(rev) >> 24;
	buffer[4] = byte(rev) >> 16;
	buffer[5] = byte(rev) >> 8;
	buffer[6] = byte(rev);
	buffer[7] = 0;
	return buffer;
}

void handleUdpControl() {
	/*int packetSize = udpControl.parsePacket();

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

	int revision = 0;
	bool ackRequested = controlPacketBuffer[7] > 0;

	ControlPacketType type = static_cast<ControlPacketType>(controlPacketBuffer[8]);

	//return wenn revision unzulässig
	if(lastRevision >= revision && type != ResetRevisionPacket)
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
			if(packetSize < 26)
				return;

			Serial.println(controlPacketBuffer[25]);

			bool ignoreNotArmed = controlPacketBuffer[25] > 0;

			/*if(servos.isArmed() || ignoreNotArmed) {
			servos.setServos(fl, fr, bl, br);
			}
			packetHandled = true;
		}
						   break;
		case StopPacket:
			if(packetSize >= 13) {
				if(controlPacketBuffer[9] == 'S' && controlPacketBuffer[10] == 'T' && controlPacketBuffer[11] == 'O' && controlPacketBuffer[12] == 'P') {
					//servos.disarmMotors();
					packetHandled = true;
				}
			}
			break;
		case ArmPacket:
			if(packetSize == 13) {
				if(controlPacketBuffer[9] == 'A' && controlPacketBuffer[10] == 'R' && controlPacketBuffer[11] == 'M') {
					/*if(controlPacketBuffer[12] > 0)
					servos.armMotors();
					else
					servos.disarmMotors();*

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
			/*
			buf[14] = servos.isArmed() ? 1 : 0;

			BinaryHelper::writeInt(buf, 15, servos.FL());
			BinaryHelper::writeInt(buf, 19, servos.FR());
			BinaryHelper::writeInt(buf, 23, servos.BL());
			BinaryHelper::writeInt(buf, 27, servos.BR());

			udpControl.beginPacket(udpControl.remoteIP(), udpControl.remotePort());
			udpControl.write(buf, 30);
			udpControl.endPacket();
			packetHandled = true;


		}

		case SubscribeDataFeed:
			dataFeedSubscriptor = udpControl.remoteIP();
			dataFeedSubscribed = true;
			packetHandled = true;
			break;

		case UnsubscribeDataFeed:
			dataFeedSubscriptor = nullptr;
			dataFeedSubscribed = false;
			packetHandled = true;
			break;

		case CalibrateGyro:
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
	}*/
}

void handleUdpData() {
	//if(!dataFeedSubscribed || dataFeedSubscriptor == nullptr) return;


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
	/*if(VERBOSE_SERIAL_PRINT)
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

	
*/
#ifdef __IEEE_LITTLE_ENDIAN
	Serial.println("$ Little Endian");
#endif
#ifdef __IEEE_BIG_ENDIAN
	Serial.println("$ Big Endian");
#endif


	digitalWrite(LED_PIN, LOW);
	if(VERBOSE_SERIAL_PRINT)
		Serial.println("$ Drone boot successfully");
}

void loop() {
	

	if(millis() - lastLoopTime >= delayTime) {
		//handleUdpControl();
		//handleUdpData();
		handleBlink();

		lastLoopTime = millis();
	}

	blinkRequested = true;
}

