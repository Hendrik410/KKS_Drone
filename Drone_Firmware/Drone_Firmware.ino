/*
 Name:        DroneFirmware.ino
 Created:    19.12.2015 17:33:55
 Author:    Hendrik
*/

#include <Wire.h>
#include <Servo.h>
#include <WiFiUdp.h>
#include <ESP8266WiFi.h>
#include <FS.h>

#ifdef _VSARDUINO_H_
#define byte unsigned char
#endif

#define BUILD_VERSION 1
#define VERBOS_SERIAL_PRINT true

#define CONTROL_PORT 4711
#define DATA_PORT 4712

#define PACKET_BUFFER_SIZE 512

#define SERVO_FR_PIN 16
#define SERVO_FL_PIN 14
#define SERVO_BR_PIN 12
#define SERVO_BL_PIN 13

#define LED_PIN 0

/* Set these to your desired credentials. */
char *ssid = "drone1";
char *password = "12345678";

short delayTime = 20;

int servoOffValue = 900;
int servoIdleValue = 1100;
int servoMaxValue = 1900;

WiFiUDP udpControl;
byte controlPacketBuffer[PACKET_BUFFER_SIZE];

WiFiUDP udpData;
byte dataPacketBuffer[PACKET_BUFFER_SIZE];

// The values for the Servos
int servoFLValue;
int servoFRValue;
int servoBLValue;
int servoBRValue;

// The objects to control the Servos (ESCs)
Servo frontLeft;
Servo frontRight;
Servo backLeft;
Servo backRight;

int lastRevision = 0;
bool isArmed = false;

bool blinkRequested = false;
bool blinkExecuting = false;
int blinkStart = 0;

int lastLoopTime = 0;

enum ControlPacketType : byte {
    MovementPacket = 1,
    StopPacket = 2,
    ArmPacket = 3,
    BlinkPacket = 4,
    RawSetPacket = 5,

    AckPacket = 6,
    PingPacket = 7,
    ResetRevisionPacket = 8,

    GetInfoPacket = 9
};

void attachServos() {
    frontLeft.attach(SERVO_FL_PIN);
    frontRight.attach(SERVO_FR_PIN);
    backLeft.attach(SERVO_BL_PIN);
    backRight.attach(SERVO_BR_PIN);
}

void setServos(int fl, int fr, int bl, int br) {
    servoFLValue = fl;
    servoFRValue = fr;
    servoBLValue = bl;
    servoBRValue = br;

    frontLeft.writeMicroseconds(fl);
    frontRight.writeMicroseconds(fr);
    backLeft.writeMicroseconds(bl);
    backRight.writeMicroseconds(br);

	if(VERBOS_SERIAL_PRINT) {
		Serial.print("Set Servos to: ");
		Serial.print(fl);
		Serial.print(", ");
		Serial.print(fr);
		Serial.print(", ");
		Serial.print(bl);
		Serial.print(", ");
		Serial.println(br);
	}
}

void setAllServos(int val) {
    setServos(val, val, val, val);
}

void armMotors() {
    if(!isArmed) {
        setAllServos(servoIdleValue);
        isArmed = true;
    }
}

void disarmMotors() {
    if(isArmed) {
        setAllServos(servoOffValue);
        isArmed = false;
    }
}

short readShort(const byte* buffer, int offset) {
    return (buffer[offset] << 8) | buffer[offset + 1];
}

void writeShort(byte buf[], short val, int offset) {
    buf[offset] = (val >> 8) & 0xFF;
    buf[offset + 1] = (val)& 0xFF;
}

int readInt(const byte* buffer, int offset) {
    return (buffer[offset] << 24) | (buffer[offset + 1] << 16) | (buffer[offset + 2] << 8) | buffer[offset + 3];
}

void writeInt(byte buf[], int val, int offset) {
    buf[offset] = (val >> 24) & 0xFF;
    buf[offset + 1] = (val >> 16) & 0xFF;
    buf[offset + 2] = (val >> 8) & 0xFF;
    buf[offset + 3] = (val)& 0xFF;
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

void handleUdp() {
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

    int revision = readInt(controlPacketBuffer, 3);
    bool ackRequested = controlPacketBuffer[7] > 0;

    ControlPacketType type = static_cast<ControlPacketType>(controlPacketBuffer[8]);

    //return wenn revision unzulässig
    if(lastRevision >= revision && type != ResetRevisionPacket)
        return;

	if(VERBOS_SERIAL_PRINT) {
		Serial.print("Got Packet with rev ");
		Serial.print(revision);
		Serial.print(" and type ");
		Serial.println(controlPacketBuffer[8]);
	}

    bool packetHandled = false;

    switch(type) {
        case MovementPacket:

            break;
        case RawSetPacket:{
            //set the 4 motor values raw
            if(packetSize < 26)
                return;

			Serial.println(controlPacketBuffer[25]);
            int fl = readInt(controlPacketBuffer, 9);
            int fr = readInt(controlPacketBuffer, 13);
            int bl = readInt(controlPacketBuffer, 17);
            int br = readInt(controlPacketBuffer, 21);

            bool ignoreNotArmed = controlPacketBuffer[25] > 0;

			if(isArmed || ignoreNotArmed) {
				setServos(fl, fr, bl, br);
			}
            packetHandled = true;
        }
            break;
        case StopPacket:
            if(packetSize >= 13) {
                if(controlPacketBuffer[9] == 'S' && controlPacketBuffer[10] == 'T' && controlPacketBuffer[11] == 'O' && controlPacketBuffer[12] == 'P') {
                    setAllServos(servoOffValue);
                    isArmed = false;
                    packetHandled = true;
                }
            }
            break;
        case ArmPacket:
            if(packetSize == 13) {
                if(controlPacketBuffer[9] == 'A' && controlPacketBuffer[10] == 'R' && controlPacketBuffer[11] == 'M') {
                    if(controlPacketBuffer[12] > 0)
                        armMotors();
                    else
                        disarmMotors();

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

        case GetInfoPacket:{
            byte buf[30];
            generatePacket(buf, revision);
            buf[8] = GetInfoPacket;

            buf[9] = BUILD_VERSION;
            writeInt(buf, lastRevision, 10);

			buf[4] = isArmed ? 1 : 0;

			writeInt(buf, servoFLValue, 15);
			writeInt(buf, servoFRValue, 19);
			writeInt(buf, servoBLValue, 23);
			writeInt(buf, servoBRValue, 27);

            udpControl.beginPacket(udpControl.remoteIP(), udpControl.remotePort());
            udpControl.write(buf, 22);
            udpControl.endPacket();
            packetHandled = true;
        }
        default:
            break;
    }

    if(packetHandled) {
		if(type != ResetRevisionPacket)
			lastRevision = revision;

        if(ackRequested && type != PingPacket) {
            byte buf[9];
            generatePacket(buf, revision);
            buf[8] = AckPacket;

            udpControl.beginPacket(udpControl.remoteIP(), udpControl.remotePort());
            udpControl.write(buf, 9);
            udpControl.endPacket();
        }
    }
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
    Serial.println("Drone V1 booting");

	// SPIFFS Filesystem
	Serial.print("Mounting Filesystem ... ");
	bool fs_status = SPIFFS.begin();
	if(!fs_status) {
		Serial.println("failed!");
		exit(); //don't boot without filesystem
	}
	Serial.println("done!");
	FSInfo fsInfo;
	SPIFFS.info(fsInfo);
	Serial.print("Usage: ");
	Serial.print(fsInfo.totalBytes / fsInfo.usedBytes);
	Serial.print("% of ");
	Serial.print(fsInfo.totalBytes);
	Serial.println(" bytes used");



    Serial.println("Configuring access point");
    //setup ap
    WiFi.softAP(ssid, password);
    IPAddress myIP = WiFi.softAPIP();
    Serial.print("AP IP address: ");
    Serial.println(myIP);

    //start udp servers
    udpControl.begin(CONTROL_PORT);
    Serial.print("Started UDP on port ");
    Serial.print(CONTROL_PORT);
    Serial.println(" for control");

    udpData.begin(DATA_PORT);
    Serial.print("Started UDP on port ");
    Serial.print(DATA_PORT);
    Serial.print(" for data");

    //attach Servo outputs
    attachServos();
    setAllServos(servoOffValue);


    digitalWrite(LED_PIN, LOW);
    Serial.println("Drone boot successfully");
}

void loop() {
    handleUdp();
    handleBlink();

	lastLoopTime = millis();
	while(millis() - lastLoopTime < delayTime);
}