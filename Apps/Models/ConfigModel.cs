using System.Runtime.InteropServices;

namespace ScreenBoard.Models;

internal class ConfigModel
{
    // Declare the config model
    private string? ScreenColor;
    private int Opacity;
    private string? PenColor;

    // Default value if the value are not correct
    private string? dftScreenColor = "000000";
    private uint dftOpacity = 90;
    private string? dftPenColor = "ffffff";
}
