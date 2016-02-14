#include "Gyro9150.h"

Gyro9150::Gyro9150(Config* config) : Gyro(config) {
}

void Gyro9150::init() {
	Log::info("Gyro", "init()");

	Wire.begin(SCL, SDA);
	mpuOK = mpu.init() == IError_None;

	if (mpuOK)
		Log::info("Gyro", "Success");
	else
		Log::error("Gyro", "Failure");
}

void Gyro9150::update() {
	Profiler::begin("Gyro::update()");
	if (mpuOK) {
		mpu.update(	&gyroX, &gyroY, &gyroZ, 
					&accX, &accY, &accY, 
					&magnetX, &magnetY, &magnetZ, 
					&pitch, &roll, &yaw);
		_dirty = true;
	}
	Profiler::end();
}

void Gyro9150::reset() {

}

float Gyro9150::getTemperature() {
	if (mpuOK)
		return mpu.readTempData() / 340.00 + 36.53;
	return 0;
}

bool Gyro9150::hasMagnetometer() {
	return true;
}

bool Gyro9150::hasCompass() {
	return true;
}
