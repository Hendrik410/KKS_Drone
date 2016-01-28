// MotorEnums.h

#ifndef _MOTORENUMS_h
#define _MOTORENUMS_h

template<class MotorPosition> inline MotorPosition operator~ (MotorPosition a) {
	return static_cast<MotorPosition>(~static_cast<int>(a));
}
template<class MotorPosition> inline MotorPosition operator| (MotorPosition a, MotorPosition b) {
	return static_cast<MotorPosition>(static_cast<int>(a) | static_cast<int>(b));
}
template<class MotorPosition> inline MotorPosition operator& (MotorPosition a, MotorPosition b) {
	return static_cast<MotorPosition>(static_cast<int>(a) & static_cast<int>(b));
}
template<class MotorPosition> inline MotorPosition operator^ (MotorPosition a, MotorPosition b) {
	return static_cast<MotorPosition>(static_cast<int>(a) ^ static_cast<int>(b));
}
template<class MotorPosition> inline MotorPosition& operator|= (MotorPosition& a, MotorPosition b) {
	return static_cast<MotorPosition&>(static_cast<int&>(a) |= static_cast<int>(b));
}
template<class MotorPosition> inline MotorPosition& operator&= (MotorPosition& a, MotorPosition b) {
	return static_cast<MotorPosition&>(static_cast<int&>(a) &= static_cast<int>(b));
}
template<class MotorPosition> inline MotorPosition& operator^= (MotorPosition& a, MotorPosition b) {
	return static_cast<MotorPosition&>(static_cast<int&>(a) ^= static_cast<int>(b));
}
enum MotorPosition {
	Position_Front = 1,
	Position_Back = 2,
	Position_Left = 4,
	Position_Right = 8
};

enum MotorRotation {
	Clockwise = 1,
	Counterclockwise = 2
};

#endif

