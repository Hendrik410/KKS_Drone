// NetworkManager.h

#ifndef _NETWORKMANAGER_h
#define _NETWORKMANAGER_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

#include "Config.h"
#include "Gyro.h"
#include "ServoManager.h"
#include "DroneEngine.h"
#include "PacketBuffer.h"
#include "PacketType.h"
#include <ESP8266WiFi/src/WiFiUdp.h>

class NetworkManager
{
protected:
	Gyro* gyro;
	ServoManager* servos;
	DroneEngine* engine;
	Config* config;

	WiFiUDP helloUDP;
	WiFiUDP controlUDP;
	WiFiUDP dataUDP;

	PacketBuffer* readBuffer;
	PacketBuffer* writeBuffer;

	bool beginParse(WiFiUDP udp);
	void handleHello(WiFiUDP udp);
	void handleControl(WiFiUDP udp);

	void writeHeader(WiFiUDP udp, uint32_t revision, ControlPacketType packetType);

	void sendPacket(WiFiUDP udp);
	void sendAck(WiFiUDP udp, uint32_t revision);
	void echoPacket(WiFiUDP udp);
public:
	explicit NetworkManager(Gyro* gyro, ServoManager* servos, DroneEngine* engine, Config* config);

	void handlePackets();
};

#endif

