#ifndef UNICODE
#define UNICODE
#endif /* UNICODE */

#include <Windows.h>

inline int ActionKeyDown(HWND hwnd, WPARAM wParam, HDC memoryHDC)
{
    int DrawRegionLimitX = GetSystemMetrics(SM_CXMAXIMIZED);
    int DrawRegionLimitY = GetSystemMetrics(SM_CYMAXIMIZED);

    if(wParam == VK_ESCAPE)
    {
        PostQuitMessage(0);
    }

    if(wParam == 0x43)
    {
        // Clear the drawing buffer
        PatBlt(memoryHDC, 0, 0, DrawRegionLimitX, DrawRegionLimitY, WHITENESS);

        // Invalidate the window to force a repaint
        InvalidateRect(hwnd, NULL, TRUE); // hWnd should be a global or passed in some way
        UpdateWindow(hwnd);
    }
    return 0;
}