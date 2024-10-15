using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

using ScreenBoard.Views.Pages;
using static ScreenBoard.ViewModels.BaseViewModel;

namespace ScreenBoard.ViewModels.Pages;

public class ConfigPageViewModel
{
    public ICommand? QuickGuide {  get; set; }

    public ICommand? Apply {  get; set; }
    public ICommand? Verify {  get; set; }

    private string _boardColor = "000000";
    private string _boardOpacity = "255";
    private string _penColor = "ffffff";

    public string BoardColor
    {
        get => _boardColor;
        set
        {
            _boardColor = value;
            OnPropertyChanged(nameof(BoardColor));
        }
    }

    public string BoardOpacity
    {
        get => _boardOpacity;
        set
        {
            _boardOpacity = value;
            OnPropertyChanged(nameof(BoardOpacity));
        }
    }

    public string PenColor
    {
        get => _penColor;
        set
        {
            _penColor = value;
            OnPropertyChanged(nameof(PenColor));
        }
    }

    public ConfigPageViewModel()
    {
        QuickGuide = new RelayCommand(onQuickGuide);

        Apply = new RelayCommand(OnApply);
        Verify = new RelayCommand(onVerify);
    }

    private void OnApply(object? parameter)
    {
        string ScreenBoardData = "ScreenBoardData#" + BoardColor +  "#" + BoardOpacity + "#" + PenColor;
        File.WriteAllTextAsync("ScreenBoardData.txt", ScreenBoardData);

        // Display the values in a MessageBox
        MessageBox.Show($"Board Color: {BoardColor}\nBoard Opacity: {BoardOpacity}\nPen Color: {PenColor}",
                        "Configuration Applied", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void onVerify(object? parameter)
    {
        if (!Directory.Exists("./Core.exe"))
        {
            MessageBox.Show("Your core app are not here!\0", "App Verify", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void onQuickGuide(object? parameter)
    {
        const string Msg1 =
            "ESC: for quit from ScreenBoard\n"
            + "LMB: for use the tools\n"
            + "RMB: for swapping between pen and eraser\n"
            + "Scroll: for change size\n"
            + "C: for clear board\n\0";

        const string Msg2 = 
            "Apply: export your config\n" 
            + "Verify: Check your core application\0";

        const string Cap = "ScreenBoard Quick Guide";

        MessageBox.Show(Msg1, Cap, MessageBoxButton.OK, MessageBoxImage.Information);
        MessageBox.Show(Msg2, Cap, MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
