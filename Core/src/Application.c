#ifndef UNICODE
#define UNICODE
#endif /* UNICODE */

#include "../include/App-Core.h"

#include <Windows.h>

int WINAPI wWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPWSTR lpCmdLine, int nShowCmd)
{
	AppCore_ReadFile("Core.txt");
	AppCore_Window();

	return 0;
}