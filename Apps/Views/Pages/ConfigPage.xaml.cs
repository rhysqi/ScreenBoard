using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using ScreenBoard.ViewModels.Pages;

namespace ScreenBoard.Views.Pages;

/// <summary>
/// Interaction logic for ConfigPage.xaml
/// </summary>
public partial class ConfigPage : Page
{
    public ConfigPage()
    {
        InitializeComponent();
        Loaded += ConfigPage_Loaded;
        DataContext = new ConfigPageViewModel();
        InterComponent();
    }

    private void InterComponent()
    {
        TB_BoardColor.Text = "000000";
        TB_BoardOpacity.Text = "255";
        TB_PenColor.Text = "ffffff";
    }

    private void ConfigPage_Loaded(object sender, RoutedEventArgs e)
    {
        // Simulate LostFocus for all TextBoxes to apply the placeholder on startup
        foreach (var control in FindVisualChildren<TextBox>(this))
        {
            TextBox_LostFocus(control, null);
        }
    }

    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        if (textBox.Foreground == Brushes.Gray)
        {
            textBox.Text = string.Empty;
            textBox.Foreground = Brushes.Black;
        }
    }

    private void TextBox_LostFocus(object sender, RoutedEventArgs? e)
    {
        TextBox textBox = (TextBox)sender;
        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            textBox.Foreground = Brushes.Gray;
            textBox.Text = (string)textBox.Tag;  // Use the Tag property to store the placeholder
        }
    }

    // Helper method to find all children of a specific type in the visual tree
    private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
    {
        if (depObj != null)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child != null && child is T)
                {
                    yield return (T)child;
                }

                foreach (T childOfChild in FindVisualChildren<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }
    }
}
