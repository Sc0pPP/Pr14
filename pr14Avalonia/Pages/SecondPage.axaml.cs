using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using WpfLikeAvaloniaNavigation;

namespace pr14Avalonia.Pages;

public partial class SecondPage:Page
{
    public SecondPage()
    {
        InitializeComponent();
    }

   

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        NavigationService?.GoBack();
    }
}