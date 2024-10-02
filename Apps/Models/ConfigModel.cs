using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ScreenBoard.Models;

internal class ConfigModel
{
    [DllImport("user32.lib")]
    public static extern bool RegisterHotkeys(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    private string? ScreenColor;
    private int Opacity;
    private string? PenColor;
    
    
}
