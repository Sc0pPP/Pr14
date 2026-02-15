using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace pr14Avalonia.Pages;

public partial class SimpleWindow : Window
{
    public SimpleWindow(string message)
    {
        InitializeComponent();
        MessageText.Text = message;
    }

    private void Ok_Click (object? sender, RoutedEventArgs e)
    {
        Close();
    }
}