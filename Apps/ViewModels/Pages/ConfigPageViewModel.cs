using System.Windows;
using System.Windows.Input;

using static ScreenBoard.ViewModels.BaseViewModel;

namespace ScreenBoard.ViewModels.Pages;

public class ConfigPageViewModel
{
    public ICommand? QuickGuide {  get; set; }

    public ICommand? ScreenColor {  get; set; }
    public ICommand? Opacity {  get; set; }
    public ICommand? PenColor {  get; set; }
    public ICommand? RegisterHotkeys {  get; set; }

    public ICommand? Apply {  get; set; }
    public ICommand? Default {  get; set; }
    public ICommand? Verify {  get; set; }

    public ConfigPageViewModel()
    {
        QuickGuide = new RelayCommand(onQuickGuide);

        Apply = new RelayCommand(OnApply);
        Default = new RelayCommand(OnDefault);
        Verify = new RelayCommand(onVerify);
    }

    private void OnApply(object? parameter)
    {
        
    }

    private void OnDefault(object? parameter)
    {
        
    }

    private void onVerify(object? parameter)
    {

    }

    private void onQuickGuide(object? parameter)
    {
        const string Msg =
            "ESC: for quit from ScreenBoard\n"
            + "LMB: for use the tools\n"
            + "RMB: for swapping between pen and eraser\n"
            + "Scroll: for change size\n"
            + "C: for clear board\n\0";
        
        const string Cap = "ScreenBoard Quick Guide";

        MessageBox.Show(Msg, Cap, MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
