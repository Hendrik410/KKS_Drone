// 
// 
// 

#include "NetworkManager.h"

NetworkManager::NetworkManager(Gyro* gyro, ServoManager* servos, DroneEngine* engine, Config* config) {
	this->gyro = gyro;
	this->servos = servos;
	this->engine = engine;
	this->config = config;

	Log::info("Network", "Starting network manager...");
	Log::debug("Network", "[Ports] hello: %d, control: %d, data: %d\n", config->NetworkHelloPort, config->NetworkControlPort, config->NetworkDataPort);

	Log::info("Network", "Creating UDP sockets...");

	helloUDP.begin(config->NetworkHelloPort);
	controlUDP.begin(config->NetworkControlPort);

	Log::info("Network", "Creating buffers...");

	readBuffer = new PacketBuffer(config->NetworkPacketBufferSize);
	writeBuffer = new PacketBuffer(config->NetworkPacketBufferSize);
}

void NetworkManager::handlePackets() {
	if (beginParse(helloUDP))
		handleHello(helloUDP);

	if (beginParse(controlUDP))
		handleControl(controlUDP);
}

bool NetworkManager::beginParse(WiFiUDP udp) {
	int size = udp.parsePacket();

	if (size == 0)
		return false;

	// setSize vor readBytes sorgt dafür, dass wenn Paket länger als interner Buffer ist eine Exception geworfen wird
	readBuffer->resetPosition();
	readBuffer->setSize(size);
	udp.readBytes(readBuffer->getBuffer(), size);

	// Pakete ohne richtige Magic Value am Anfang werden ignoriert
	if (size < 3)
		return false;
	if (readBuffer->readUint8() != 'F' || readBuffer->readUint8() != 'L' || readBuffer->readUint8() != 'Y')
		return false;

	return true;
}

void NetworkManager::sendPacket(WiFiUDP udp) {
	udp.beginPacket(udp.remoteIP(), udp.remotePort());
	udp.write(writeBuffer->getBuffer(), writeBuffer->getPosition());
	udp.endPacket();

	writeBuffer->resetPosition();
}

void NetworkManager::writeHeader(WiFiUDP udp, uint32_t revision, ControlPacketType packetType) {
	writeBuffer->write('F');
	writeBuffer->write('L');
	writeBuffer->write('Y');
	writeBuffer->write(revision);
	writeBuffer->write(byte(0)); // Ack
	writeBuffer->write(static_cast<uint8_t>(packetType));
}

void NetworkManager::sendAck(WiFiUDP udp, uint32_t revision) {
	writeHeader(udp, revision, AckPacket);
	sendPacket(udp);
}

void NetworkManager::echoPacket(WiFiUDP udp) {
	udp.beginPacket(udp.remoteIP(), udp.remotePort());
	udp.write(readBuffer->getBuffer(), readBuffer->getSize());
	udp.endPacket();
}

void NetworkManager::handleHello(WiFiUDP udp) {
	if (readBuffer->getSize() < 4 || readBuffer->readUint8() != 1)
		return;

	writeBuffer->write('F');
	writeBuffer->write('L');
	writeBuffer->write('Y');
	writeBuffer->write(byte(2));

	writeBuffer->writeString(config->DroneName);
	writeBuffer->writeString(MODEL_NAME);

	char serialCode[32];
	getBuildSerialCode(serialCode, sizeof(serialCode));
	writeBuffer->writeString(serialCode);


	writeBuffer->write(uint8_t(BUILD_VERSION));

	sendPacket(udp);
}

void NetworkManager::handleControl(WiFiUDP udp) {
	if (readBuffer->getSize() < 9)
		return;

	uint32_t revision = readBuffer->readUint32();
	bool ackRequested = readBuffer->readUint8() > 0;

	ControlPacketType type = static_cast<ControlPacketType>(readBuffer->readUint8());

	Log::debug("Network", "Got Packet with size %d rev %d and type: %d", readBuffer->getSize(), revision, type);

	
	if (ackRequested)
		sendAck(udp, revision);


	switch (type) {
	case MovementPacket:
		break;
	case RawSetPacket: {
		//set the 4 motor values raw
		if (readBuffer->getSize() < 17)
			return;

		uint16_t fl = readBuffer->readUint16();
		uint16_t fr = readBuffer->readUint16();
		uint16_t bl = readBuffer->readUint16();
		uint16_t br = readBuffer->readUint16();

		engine->setRawServoValues(fl, fr, bl, br);

		break;
	}
	case StopPacket:
		engine->stop();
		break;
	case ArmPacket:
		if (readBuffer->getSize() == 13) {
			if (readBuffer->readUint8() == 'A' && readBuffer->readUint8() == 'R' && readBuffer->readUint8() == 'M') {
				if (readBuffer->readUint8() > 0)
					engine->arm();
				else
					engine->disarm();
			}
		}
		break;
	case PingPacket:
		echoPacket(udp);
		break;
	case BlinkPacket:
		//blinkRequested = true;
		break;
	case ResetRevisionPacket:
		//lastRevision = 0;
		break;

	case GetInfoPacket: {
		writeHeader(udp, revision, GetInfoPacket);

		writeBuffer->write(uint8_t(BUILD_VERSION));
		writeBuffer->write(uint32_t(0)); // lastRevision);

		writeBuffer->write(byte(engine->state() == State_Armed ? 1 : 0));

		writeBuffer->write(uint16_t(servos->FL()));
		writeBuffer->write(uint16_t(servos->FR()));
		writeBuffer->write(uint16_t(servos->BL()));
		writeBuffer->write(uint16_t(servos->BR()));

		sendPacket(udp);
		break;
	}
						
	case SubscribeDataFeed:
		/*dataFeedSubscriptor = udp.remoteIP();
		dataFeedSubscribed = true;*/
		break;

	case UnsubscribeDataFeed:
		//dataFeedSubscribed = false;
		break;
	case CalibrateGyro:
		gyro->setAsZero();
		break;
	}
}