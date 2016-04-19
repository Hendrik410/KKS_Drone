#ifndef _STOPREASON_h
#define _STOPREASON_h

enum StopReason : byte {
	Unknown,
	None, 
	User,
	NoData,
	NoPing,
	InvalidGyro
};

#endif