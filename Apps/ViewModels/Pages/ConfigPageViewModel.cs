﻿using System.ComponentModel;
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

    private string _boardColor = "";
    private string _boardOpacity = "";
    private string _penColor = "";

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
        // Display the values in a MessageBox
        MessageBox.Show($"Board Color: {BoardColor}\nBoard Opacity: {BoardOpacity}\nPen Color: {PenColor}",
                        "Configuration Applied", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void onVerify(object? parameter)
    {

    }

    private void onQuickGuide(object? parameter)
    {
        const string Msg1 =
            "ESC: for quit from ScreenBoard\n"
            + "LMB: for use the tools\n"
            + "RMB: for swapping between pen and eraser\n"
            + "Scroll: for change size\n"
            + "C: for clear board\n\0";
        
        const string Cap = "ScreenBoard Quick Guide";

        MessageBox.Show(Msg1, Cap, MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
