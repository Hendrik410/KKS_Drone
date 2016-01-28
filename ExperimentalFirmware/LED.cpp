#include "LED.h"

bool shouldBlink = false;
bool blinkCooldown = false;
int blinkTimer = 0;
int ledPin = 0;

void setupLED(Config* config) {
	ledPin = config->PinLed;
	pinMode(ledPin, OUTPUT);
}

void handleBlink() {
	if (millis() - blinkTimer > 500) {
		if (shouldBlink) {
			turnLedOff();
			
			shouldBlink = false;
			blinkCooldown = true;
			blinkTimer = millis();
		}
		else
			blinkCooldown = false;
	}
}

void blinkLED() {
	if (blinkCooldown || shouldBlink)
		return;

	turnLedOn();

	shouldBlink = true;
	blinkTimer = millis();
}

void turnLedOn() {
	shouldBlink = false;
	blinkCooldown = false;

	digitalWrite(ledPin, HIGH);
}

void turnLedOff() {
	shouldBlink = false;
	blinkCooldown = false;

	digitalWrite(ledPin, LOW);
}


