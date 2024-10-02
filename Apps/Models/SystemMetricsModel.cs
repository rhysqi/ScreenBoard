using System.Runtime.InteropServices;

namespace ScreenBoard.Models;

public class SystemMetricsModel
{
    [DllImport("user32.dll")]
    private static extern int GetSystemMetrics(IntPtr hWnd);

    private const int SM_CXSCREEN = 0;
    private const int SM_CYSCREEN = 1;

    int GetScreenWidht()
    {
        return GetSystemMetrics(SM_CXSCREEN);
    }

    int GetScreenHeight()
    {
        return GetSystemMetrics(SM_CYSCREEN);
    }
}
