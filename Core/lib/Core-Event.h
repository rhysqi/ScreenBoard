#ifndef UNICODE
#define UNICODE
#endif /* UNICODE */

#include <Windows.h>

inline int ActionKeyDown(HWND hwnd, WPARAM wParam, HDC memoryHDC)
{
    if(wParam == VK_ESCAPE)
    {
        PostQuitMessage(0);
    }

    if(wParam == 0x43)
    {
        // Clear the drawing buffer
        PatBlt(memoryHDC, 0, 0, GetSystemMetrics(SM_CXFULLSCREEN), GetSystemMetrics(SM_CYFULLSCREEN), WHITENESS);

        // Invalidate the window to force a repaint
        InvalidateRect(hwnd, NULL, TRUE); // hWnd should be a global or passed in some way
        UpdateWindow(hwnd);
    }
    return 0;
}