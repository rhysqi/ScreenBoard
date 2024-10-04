#include "../include/App-Core.h"

#include <Windows.h>

int __fastcall AppCore_Window()
{
	// Register the window class.
    const wchar_t CLASS_NAME[]  = L"Sample Window Class";
    WNDCLASS wc = { };

	MSG msg = { };
    while (GetMessage(&msg, NULL, 0, 0) > 0)
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

	return 0;
}