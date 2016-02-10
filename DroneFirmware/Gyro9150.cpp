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
	if (mpuOK)
		mpu.update(&roll, &pitch, &yaw); // Pitch und Roll umgedreht
	Profiler::end();
}

void Gyro9150::reset() {

}

float Gyro9150::getTemperature() {
	if (mpuOK)
		return mpu.readTempData() / 340.00 + 36.53;
	return 0;
}
