
#include "Arduino.h"

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

Gyro* gyro;
ServoManager* servos;


int lastLoopTime = 0;
short delayTime = 5;

//######################### Methods


void setup() {
	Serial.begin(115200);
	config = ConfigManager::getDefault();

	//setup servos
	servos = new ServoManager(&config);
	servos->init(config.PinFrontLeft, config.PinFrontRight, config.PinBackLeft, config.PinBackRight);



	//setup MPU6050
	gyro = new Gyro(&config);
	Wire.begin(SDA, SCL);
	gyro->init();
	gyro->setAsZero();
	
}

byte buffer[128];

void loop() {
	//keep gyro data updated
	gyro->update();

	Serial.print("G");
	Serial.print(int32_t(gyro->getPitch() * 1000));
	Serial.print(";");
	Serial.print(0);// int32_t(gyro->getRoll() * 1000));
	Serial.print(";");
	Serial.println(0);// uint32_t(gyro->getYaw() * 1000.0));
	
	while(Serial.available() >= 9) {

		byte incomeBuffer[9];
		Serial.readBytes(incomeBuffer, 9);

		switch(incomeBuffer[0]) {
			case 'S': {

				uint16_t fl = BinaryHelper::readUint16(incomeBuffer, 1);
				uint16_t fr = BinaryHelper::readUint16(incomeBuffer, 3);
				uint16_t bl = BinaryHelper::readUint16(incomeBuffer, 5);
				uint16_t br = BinaryHelper::readUint16(incomeBuffer, 7);

				servos->setServos(fl, fr, bl, br);
				}
				break;
			case 'R':
				gyro->setAsZero();
				break;
		}
	}

	while(millis() - lastLoopTime < delayTime) {
		gyro->update();
	}
	lastLoopTime = millis();
}

