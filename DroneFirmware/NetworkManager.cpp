// 
// 
// 

#include "NetworkManager.h"

NetworkManager::NetworkManager(Gyro* gyro, ServoManager* servos, DroneEngine* engine, Config* config, VoltageInputReader* voltageReader) {
	this->gyro = gyro;
	this->servos = servos;
	this->engine = engine;
	this->config = config;
	this->voltageReader = voltageReader;

	_dataFeedSubscribed = false;
	_lastDataSend = 0;
	dataRevision = 1;

	tickCount = 0;

	lastState = StateUnkown;

	Log::info("Network", "Starting network manager...");
	Log::debug("Network", "[Ports] hello: %d, control: %d, data: %d", config->NetworkHelloPort, config->NetworkControlPort, config->NetworkDataPort);

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

	handleData(dataUDP);
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

void NetworkManager::writeHeader(WiFiUDP udp, int32_t revision, ControlPacketType packetType) {
	writeBuffer->write('F');
	writeBuffer->write('L');
	writeBuffer->write('Y');
	writeBuffer->write(revision);
	writeBuffer->write(byte(0)); // kein Ack anfordern
	writeBuffer->write(static_cast<uint8_t>(packetType));
}

void NetworkManager::writeDataHeader(WiFiUDP udp, int32_t revision, DataPacketType packetType) {
	writeBuffer->write('F');
	writeBuffer->write('L');
	writeBuffer->write('Y');
	writeBuffer->write(revision);
	writeBuffer->write(static_cast<uint8_t>(packetType));
}


void NetworkManager::sendAck(WiFiUDP udp, int32_t revision) {
	writeHeader(udp, revision, AckPacket);
	sendPacket(udp);
}

void NetworkManager::sendData(WiFiUDP udp) {
	udp.beginPacket(_dataFeedSubscriptor, config->NetworkDataPort);
	udp.write(writeBuffer->getBuffer(), writeBuffer->getPosition());
	udp.endPacket();

	writeBuffer->resetPosition();
}

void NetworkManager::echoPacket(WiFiUDP udp) {
	udp.beginPacket(udp.remoteIP(), udp.remotePort());
	udp.write(readBuffer->getBuffer(), readBuffer->getSize());
	udp.endPacket();
}

void NetworkManager::handleHello(WiFiUDP udp) {
	if (readBuffer->getSize() < 4 || readBuffer->readUint8() != HelloQuestion)
		return;

	writeBuffer->write('F');
	writeBuffer->write('L');
	writeBuffer->write('Y');
	writeBuffer->write(byte(HelloAnswer));

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

	int32_t revision = readBuffer->readInt32();
	bool ackRequested = readBuffer->readBoolean();

	ControlPacketType type = static_cast<ControlPacketType>(readBuffer->readUint8());

	Log::debug("Network", "[Packet] %s, size %d, rev %d", getControlPacketName(type), readBuffer->getSize(), revision);

	
	if (ackRequested)
		sendAck(udp, revision);


	switch (type) {
	case MovementPacket: {
		bool hover = readBuffer->readBoolean();

		float pitch = readBuffer->readFloat();
		float roll = readBuffer->readFloat();
		float yaw = readBuffer->readFloat();
		float thrust = readBuffer->readFloat();

		engine->setTargetMovement(pitch, roll, yaw);
		engine->setTargetVerticalSpeed(thrust);

	}
	break;
	case RawSetPacket: {
		uint16_t fl = readBuffer->readUint16();
		uint16_t fr = readBuffer->readUint16();
		uint16_t bl = readBuffer->readUint16();
		uint16_t br = readBuffer->readUint16();

		if (fl > config->ServoMax) {
			Log::error("Network", "[RawSetPacket] Invalid value for fl");
			return;
		}

		if (fr > config->ServoMax) {
			Log::error("Network", "[RawSetPacket] Invalid value for fr");
			return;
		}

		if (bl > config->ServoMax) {
			Log::error("Network", "[RawSetPacket] Invalid value for bl");
			return;
		}

		if (br > config->ServoMax) {
			Log::error("Network", "[RawSetPacket] Invalid value for br");
			return;
		}

		if (engine->state() == StateArmed)
			engine->setRawServoValues(fl, fr, bl, br);

		break;
	}
	case StopPacket:
		engine->stop(User);
		break;
	case ArmPacket:
		if (readBuffer->getSize() == 13) {
			if (readBuffer->readUint8() == 'A' && readBuffer->readUint8() == 'R' && readBuffer->readUint8() == 'M') {
				if (readBuffer->readBoolean())
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
		blinkLED();
		break;
	case ResetRevisionPacket:
		//lastRevision = 0;
		break;

	case GetInfoPacket: {
		writeHeader(udp, revision, GetInfoPacket);

		writeBuffer->writeString(config->DroneName);
		writeBuffer->writeString(MODEL_NAME);

		char serialCode[32];
		getBuildSerialCode(serialCode, sizeof(serialCode));
		writeBuffer->writeString(serialCode);

		writeBuffer->writeString(BUILD_NAME);

		writeBuffer->write(uint8_t(BUILD_VERSION));
		writeBuffer->write(uint32_t(0)); // lastRevision);

		rst_info* resetInfo = ESP.getResetInfoPtr();

		writeBuffer->write(uint8_t(resetInfo->reason));
		writeBuffer->write(uint8_t(resetInfo->exccause));

		writeBuffer->write(uint8_t(engine->getStopReason()));

		writeBuffer->write((uint8_t*)config, sizeof(Config));

		sendPacket(udp);
		break;
	}
						
	case SubscribeDataFeed:
		_dataFeedSubscriptor = udp.remoteIP();
		_dataFeedSubscribed = true;

		Log::debug("Network", "Client %s subscribed data", udp.remoteIP().toString().c_str());
		break;

	case UnsubscribeDataFeed:
		_dataFeedSubscribed = false;

		Log::debug("Network", "Client %s unsubscribed data", udp.remoteIP().toString().c_str());
		break;
	case CalibrateGyro:
		servos->setAllServos(config->ServoMin);

		if (engine->state() == StateReset || engine->state() == StateStopped || engine->state() == StateIdle)
			gyro->setAsZero();
		break;

	case Reset:
		if (engine->state() == StateReset || engine->state() == StateStopped || engine->state() == StateIdle)
			ESP.restart();
		break;
	case SetConfig: {
		if (readBuffer->getSize() - readBuffer->getPosition() != sizeof(Config)) {
			Log::error("Network", "[SetConfig] Packet size does not match config structure size");
			return;
		}
		readBuffer->read((byte*)config, sizeof(Config));

		Log::info("Network", "Config set.");
		Log::setPrintToSerial(config->VerboseSerialLog);

		ConfigManager::saveConfig(*config);
		}
		break;
	case ClearStatus:
		engine->clearStatus();
		break;
	}
}

void NetworkManager::handleData(WiFiUDP udp) {
	if (!_dataFeedSubscribed)
		return;

	sendDroneData(udp);

	if (tickCount % 4 == 0)
		sendLog(udp);

	if (tickCount % 8 == 0)
		sendDebugData(udp);
}

void NetworkManager::sendDroneData(WiFiUDP udp) {
	// binary OR wird verwendet, damit alle dirty Methoden aufgerufen werden
	bool droneDataDirty = lastState != engine->state() | servos->dirty() | gyro->dirty() | voltageReader->dirty();

	if (droneDataDirty || millis() - _lastDataSend >= 2000) { // 2 Sekunden
		writeDataHeader(dataUDP, dataRevision++, DataDrone);

		writeBuffer->write(uint8_t(engine->state()));

		writeBuffer->write(uint16_t(servos->FL()));
		writeBuffer->write(uint16_t(servos->FR()));
		writeBuffer->write(uint16_t(servos->BL()));
		writeBuffer->write(uint16_t(servos->BR()));

		writeBuffer->write(gyro->getPitch());
		writeBuffer->write(gyro->getRoll());
		writeBuffer->write(gyro->getYaw());

		writeBuffer->write(gyro->getAccelerationX());
		writeBuffer->write(gyro->getAccelerationY());
		writeBuffer->write(gyro->getAccelerationZ());

		writeBuffer->write(gyro->getTemperature());
		writeBuffer->write(voltageReader->readVoltage());
		writeBuffer->write(WiFi.RSSI());

		sendData(udp);
		_lastDataSend = millis();

		lastState = engine->state();
	}
}

void NetworkManager::sendLog(WiFiUDP udp) {
	while (Log::getBufferLines() > 0) {
		writeDataHeader(dataUDP, dataRevision++, DataLog);

		int messagesToSend = Log::getBufferLines();
		if (messagesToSend > 5)
			messagesToSend = 5;

		writeBuffer->write(messagesToSend);

		for (int i = 0; i < messagesToSend; i++) {
			char* msg = Log::popMessage();
			writeBuffer->writeString(msg);
			free(msg);
		}

		sendData(udp);
	}
}

void NetworkManager::sendDebugData(WiFiUDP udp) {
	writeDataHeader(dataUDP, dataRevision++, DataDebug);

	writeBuffer->write(engine->getFrontLeftRatio());
	writeBuffer->write(engine->getFrontRightRatio());
	writeBuffer->write(engine->getBackLeftRatio());
	writeBuffer->write(engine->getBackRightRatio());

	sendData(udp);
}