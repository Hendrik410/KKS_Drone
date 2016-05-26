// NetworkManager.h

#ifndef _NETWORKMANAGER_h
#define _NETWORKMANAGER_h

#include "arduino.h"
#include "Config.h"
#include "Gyro.h"
#include "ServoManager.h"
#include "DroneEngine.h"
#include "PacketBuffer.h"
#include "PacketType.h"
#include "Log.h"
#include "Build.h"
#include "ConfigManager.h"
#include "LED.h"

#include <ESP8266WiFi/src/ESP8266WiFi.h>
#include <ESP8266WiFi/src/WiFiUdp.h>
#include "VoltageInputReader.h"
#include <user_interface.h>

#define VERBOSE_PACKET_LOG false

class NetworkManager
{
protected:
	Gyro* gyro;
	ServoManager* servos;
	DroneEngine* engine;
	Config* config;
	VoltageInputReader* voltageReader;

	IPAddress _dataFeedSubscriptor;
	bool _dataFeedSubscribed;
	long _lastDataSend;

	bool saveConfig;

	uint64_t tickCount;

	DroneState lastState;
	int dataRevision;

	int lastMovementRevision;
	int lastOtaRevision;

	WiFiUDP helloUDP;
	WiFiUDP controlUDP;
	WiFiUDP dataUDP;

	PacketBuffer* readBuffer;
	PacketBuffer* writeBuffer;

	bool checkRevision(int a, int b);

	bool beginParse(WiFiUDP udp);
	void handleHello(WiFiUDP udp);
	void handleControl(WiFiUDP udp);
	void handleData(WiFiUDP upd);

	void writeHeader(WiFiUDP udp, int32_t revision, ControlPacketType packetType);
	void writeDataHeader(WiFiUDP udp, int32_t revision, DataPacketType packetType);

	void sendPacket(WiFiUDP udp);
	void sendAck(WiFiUDP udp, int32_t revision);
	void sendData(WiFiUDP udp);
	void echoPacket(WiFiUDP udp);

	void sendDroneData(WiFiUDP udp);
	void sendLog(WiFiUDP udp);
	void sendDebugData(WiFiUDP udp);
public:
	explicit NetworkManager(Gyro* gyro, ServoManager* servos, DroneEngine* engine, Config* config, VoltageInputReader* voltageReader);

	void handlePackets();
	void handleData();
};

#endif

