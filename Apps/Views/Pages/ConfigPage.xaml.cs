using System.Windows.Controls;

using ScreenBoard.ViewModels;
using ScreenBoard.ViewModels.Pages;

namespace ScreenBoard.Views.Pages
{
    /// <summary>
    /// Interaction logic for ConfigPage.xaml
    /// </summary>
    public partial class ConfigPage : Page
    {
        public ConfigPage()
        {
            InitializeComponent();
            DataContext = new ConfigPageViewModel();
        }
    }
}
