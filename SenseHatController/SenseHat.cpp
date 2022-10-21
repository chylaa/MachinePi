#include "pch.h"
#define SENSE_DLL __declspec(dllexport)

extern "C" {
	SENSE_DLL int GetJoystickState() {
		return 12;
	}
}