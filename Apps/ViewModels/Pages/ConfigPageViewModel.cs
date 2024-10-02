using System.Windows.Input;

using static ScreenBoard.ViewModels.BaseViewModel;

namespace ScreenBoard.ViewModels.Pages;

public class ConfigPageViewModel
{
    public ICommand? ScreenColor {  get; set; }
    public ICommand? Opacity {  get; set; }
    public ICommand? PenColor {  get; set; }
    public ICommand? RegisterHotkeys {  get; set; }

    public ICommand? Apply {  get; set; }
    public ICommand? Reset {  get; set; }

    public ConfigPageViewModel()
    {
        Apply = new RelayCommand(OnApply);
        Reset = new RelayCommand(OnReset);
    }

    private void OnApply(object? parameter)
    {

    }

    private void OnReset(object? parameter)
    {
        
    }
}
