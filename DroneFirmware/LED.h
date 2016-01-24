#ifndef _LED_h
#define _LED_h

#include "arduino.h"
#include "Config.h"

extern bool shouldBlink;
extern bool blinkCooldown;
extern int blinkTimer;
extern int ledPin;

void setupLED(Config* config);
void handleBlink();
void blinkLED();
void turnLedOn();
void turnLedOff();
#endif