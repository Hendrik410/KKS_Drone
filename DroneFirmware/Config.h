// Config.h

#ifndef _CONFIG_h
#define _CONFIG_h

#include "arduino.h"
#include "PID_Settings.h"

#ifdef _VSARDUINO_H_ //Kompatibilität mit visual micro

#define byte unsigned char
#else
#endif

enum DroneEngineType : byte {
	EngineLinear,
	EnginePID
};

struct Config {
	//A user-friendly name for the drone
	char DroneName[20];

	// Name des WiFi Netzwerks
	// leerer String bedeutet es wird keine Verbindung zu einem Netzwerk aufgebaut
	char NetworkSSID[20];

	//The password of the WiFi network
	char NetworkPassword[30];

	//The UDP-Port for the hello packets
	uint16_t NetworkHelloPort;

	//The UDP-Port for the control packets
	uint16_t NetworkControlPort;
	//The UDP-Port for the data packets
	uint16_t NetworkDataPort;
	//The size of the buffer for incoming packets
	uint16_t NetworkPacketBufferSize;

	//The time, after when the drone stopps without any action sent
	uint32_t MaximumNetworkTimeout;

	//Toogles the debug output on Serial
	bool VerboseSerialLog;
	//The temperature, at which the drone starts to decent on turn off
	float MaxTemperature;

	//A offset value for the pitch
	uint16_t TrimPitch;
	//A offset value for the roll
	uint16_t TrimRoll;
	//A offset value for the yaw
	uint16_t TrimYaw;
	//A offset value for the throttle
	uint16_t TrimThrottle;

	//The minumum output value for the ESC's
	uint16_t ServoMin;
	//The maximum output value for the ESC's
	uint16_t ServoMax;
	//The output value for the ESC's, at which they start to turn
	uint16_t ServoIdle;
	//The output value for the ESC's, at which the drone hovers
	uint16_t ServoHover;

	//The X gyro offset value for the DMP
	int16_t DMPOffsetX;
	//The Y gyro offset value for the DMP
	int16_t DMPOffsetY;
	//The Z gyro offset value for the DMP
	int16_t DMPOffsetZ;
	//The acceleration offset value for the DMP
	uint16_t DMPOffsetAccel;

	//The pin of the front-left motor
	byte PinFrontLeft;
	//The pin of the front-right motor
	byte PinFrontRight;
	//The pin of the back-left motor
	byte PinBackLeft;
	//The pin of the back-right motor
	byte PinBackRight;
	//The pin of the LED
	byte PinLed;

	float Degree2Ratio;
	float RotaryDegree2Ratio;

	uint16_t PhysicsCalcDelay;

	DroneEngineType EngineType;

	PID_Settings PitchPidSettings;
	PID_Settings RollPidSettings;
	PID_Settings YawPidSettings;

	float InterpolationFactor;
	float CorrectionFactor;
};

#endif

