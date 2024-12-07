#ifndef UNICODE
#define UNICODE
#endif /* UNICODE */

#include "Core-Event.h"
#include "../include/App-Core.h"

#define MARGIN 3

#include <Windows.h>
#include <winuser.h>
#include <minwindef.h>

unsigned int BoardColor;
unsigned int Opacity;
unsigned int PenColor;

int DrawRegionLimitX;
int DrawRegionLimitY;

LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

BOOL drawing = FALSE;  // Indicates if we are in the process of drawing
BOOL erasing = FALSE;  // Indicates if we are in the process of erasing
POINT lastPoint;       // Last mouse position

int brushSize = 5;
HDC memoryHDC;
HBITMAP hBitmap;

unsigned int brushValue = 0xffffffff;
int alphaValue = 125;

int __fastcall AppCore_Window()
{
    DrawRegionLimitX = GetSystemMetrics(SM_CXMAXIMIZED);
    DrawRegionLimitY = GetSystemMetrics(SM_CYMAXIMIZED);

    // Get the instance handle
    HINSTANCE hInstance = GetModuleHandle(NULL);

    // Define window class for the splash screen
    WNDCLASS wc = {0};
    wc.lpfnWndProc = WindowProc;
    wc.hInstance = hInstance;
    wc.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
    wc.lpszClassName = L"InitWindowClass";
    wc.hCursor = LoadCursor(NULL, IDC_ARROW);

    // Register the window class
    if (!RegisterClass(&wc)) {
        MessageBoxW(NULL, L"Window Registration Failed!", L"Error!", MB_ICONEXCLAMATION | MB_OK);
        return 0;
    }

    int Height = GetSystemMetrics(SM_CYMAXIMIZED);
    int Width = GetSystemMetrics(SM_CXMAXIMIZED);

    // Create the init screen window
    HWND hInitWindow = CreateWindowExW(
        WS_EX_LAYERED | WS_EX_APPWINDOW, // No taskbar icon and layered for transparency
        L"InitWindowClass",             // Window class
        NULL,                             // No title text for splash screen
        WS_POPUPWINDOW | WS_OVERLAPPED, // Popup window, no border, no title bar
        CW_USEDEFAULT, CW_USEDEFAULT,    // Position on screen
        Width, Height,                   // Size of the splash screen window
        NULL, NULL, hInstance, NULL);    // No parent or menu

    if (hInitWindow == NULL) {
        MessageBoxW(NULL, L"Window Creation Failed!", L"Error!", MB_ICONEXCLAMATION | MB_OK);
        return 0;
    }

    // Make the window transparent
    SetLayeredWindowAttributes(hInitWindow, 0, alphaValue, LWA_ALPHA);

    // Create a memory HDC for off-screen drawing
    HDC hdc = GetDC(hInitWindow);
    memoryHDC = CreateCompatibleDC(hdc);
    hBitmap = CreateCompatibleBitmap(hdc, Width, Height);
    SelectObject(memoryHDC, hBitmap);
    PatBlt(memoryHDC, 0, 0, Width, Height, WHITENESS);  // Clear the bitmap with white

    // Show the window
    ShowWindow(hInitWindow, SW_SHOW);

    // Message loop
    MSG msg;
    while (GetMessage(&msg, NULL, 0, 0)) {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    // Cleanup
    DeleteObject(hBitmap);
    DeleteDC(memoryHDC);
    ReleaseDC(hInitWindow, hdc);

    return (int) msg.wParam;
}

// Window Procedure (Handles messages for the window)
LRESULT CALLBACK WindowProc(HWND hwnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
    switch (uMsg)
    {
        case WM_LBUTTONDOWN:
        {
            // Capture mouse input so we keep drawing even if cursor leaves window
            SetCapture(hwnd);
            drawing = TRUE;  // Start drawing
            lastPoint.x = LOWORD(lParam);  // Get starting x-coordinate
            lastPoint.y = HIWORD(lParam);  // Get starting y-coordinate
            return 0;
        }

        case WM_RBUTTONDOWN:
        {
            // Capture mouse input so we keep drawing even if cursor leaves window
            SetCapture(hwnd);
            erasing = TRUE;  // Start drawing
            lastPoint.x = LOWORD(lParam);  // Get starting x-coordinate
            lastPoint.y = HIWORD(lParam);  // Get starting y-coordinate
            return 0;
        }

        case WM_MOUSEMOVE:
        {
            if (drawing) {
                // Get the current mouse position
                POINT currentPoint;
                currentPoint.x = LOWORD(lParam);
                currentPoint.y = HIWORD(lParam);

                // Check if the current point is within the margin
                if (currentPoint.x < MARGIN || currentPoint.x > DrawRegionLimitX + MARGIN ||
                    currentPoint.y < MARGIN || currentPoint.y > DrawRegionLimitY + MARGIN) {
                    // If it reaches the limit, don't draw
                    return 0;
                }

                // Draw a line from the last point to the current point in memory HDC
                MoveToEx(memoryHDC, lastPoint.x, lastPoint.y, NULL);
                HPEN hPen = CreatePen(PS_SOLID,  brushSize, RGB(0, 0, 0)); // Create a pen with the current brush size
                SelectObject(memoryHDC, hPen);
                LineTo(memoryHDC, currentPoint.x, currentPoint.y);

                // Update the last point to the current point
                lastPoint = currentPoint;

                // Force the window to repaint itself
                InvalidateRect(hwnd, NULL, FALSE);
            }
            if (erasing)
            {
                // Get the current mouse position
                POINT currentPoint;
                currentPoint.x = LOWORD(lParam);
                currentPoint.y = HIWORD(lParam);

                // Check if the current point is within the margin
                if (currentPoint.x < MARGIN || currentPoint.x > DrawRegionLimitX - MARGIN ||
                    currentPoint.y < MARGIN || currentPoint.y > DrawRegionLimitY - MARGIN) {
                    // If it reaches the limit, don't draw
                    return 0;
                }

                // Draw a line from the last point to the current point in memory HDC
                MoveToEx(memoryHDC, lastPoint.x, lastPoint.y, NULL);
                HPEN hPen = CreatePen(PS_SOLID, brushSize, RGB(255, 255, 255)); // Create a pen with the current brush size
                SelectObject(memoryHDC, hPen);
                LineTo(memoryHDC, currentPoint.x, currentPoint.y);

                // Update the last point to the current point
                lastPoint = currentPoint;

                // Force the window to repaint itself
                InvalidateRect(hwnd, NULL, FALSE);
            }
            return 0;
        }

        case WM_LBUTTONUP:
        {
            // Release the mouse capture and stop drawing
            ReleaseCapture();
            drawing = FALSE;
            return 0;
        }
        case WM_RBUTTONUP:
        {
            ReleaseCapture();
            erasing = FALSE;
            return 0;
        }

        case WM_PAINT:
        {
            // Standard WM_PAINT handling, redraw the current bitmap
            PAINTSTRUCT ps;
            HDC hdc = BeginPaint(hwnd, &ps);

            // Copy the memory HDC (off-screen drawing) to the window's HDC
            BitBlt(hdc, 0, 0, DrawRegionLimitX, DrawRegionLimitY, memoryHDC, 0, 0, SRCCOPY);

            EndPaint(hwnd, &ps);
            return 0;
        }
        
        case WM_KEYDOWN:
        {
            ActionKeyDown(hwnd, wParam, memoryHDC);
            return 0;
        }

        case WM_MOUSEWHEEL:
        {
            // Mouse wheel event for scaling brush size
            int delta = GET_WHEEL_DELTA_WPARAM(wParam); // Get the scroll delta
            if (delta > 0) // Scrolled up
            {
                brushSize++; // Increase brush size
            }
            else if (delta < 0) // Scrolled down
            {
                if (brushSize > 1) // Ensure brush size does not go below 1
                    brushSize--; // Decrease brush size
            }
            return 0;
        }

        case WM_DESTROY:
            PostQuitMessage(0);
            return 0;
    }
    return DefWindowProc(hwnd, uMsg, wParam, lParam);
}