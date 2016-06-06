#ifndef _LED_h
#define _LED_h

#include "arduino.h"
#include "Config.h"

extern int ledPin;

extern bool shouldBlink;
extern bool blinkCooldown;

extern int blinkCount;
extern int blinkCooldownTime;
extern int blinkTimer;

void setupLED(Config* config);
void handleBlink();
void blinkLED(int count, int time);
void turnLedOn();
void turnLedOff();
#endif