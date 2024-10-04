using System.Windows.Input;

using static ScreenBoard.ViewModels.BaseViewModel;

using ScreenBoard.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ScreenBoard.ViewModels;

internal class MainViewModel : INotifyPropertyChanged
{
    private readonly SystemMetricsModel _systemMetrics;
    private int _captionHeight;

    public int CaptionHeight
    {
        get => _captionHeight;
        set
        {
            if (_captionHeight != value)
            {
                _captionHeight = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand? GetCaptionHeightCommand { get; }

    internal MainViewModel()
    {
        _systemMetrics = new SystemMetricsModel();
        GetCaptionHeightCommand = new RelayCommand(GetCaptionHeight);
        
        GetCaptionHeight(null);
    }
    
    private void GetCaptionHeight(object? parameter)
    {
        CaptionHeight = _systemMetrics.GetCaptionHeight();
    }


    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
