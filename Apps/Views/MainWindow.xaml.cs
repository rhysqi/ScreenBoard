using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

using ScreenBoard.Models;
using ScreenBoard.ViewModels;

namespace ScreenBoard.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }

}