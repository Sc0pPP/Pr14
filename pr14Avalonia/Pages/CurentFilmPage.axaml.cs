using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using pr14Avalonia.Models;
using WpfLikeAvaloniaNavigation;
namespace pr14Avalonia.Pages;
public partial class CurentFilmPage : Page
{
    public static List<Session> SessionsList = Core.Context.Sessions.ToList();
    public CurentFilmPage()
    {
        InitializeComponent();
        Produc.ItemsSource = new[] {AppState.CurrentMovie} ; 
        SessionSP.ItemsSource=AppState.CurentSession;
        
    }
    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        if (NavigationService.CanGoBack)
        {
            NavigationService?.GoBack();
        }
    }

    private void ChooseSeans(object? sender, RoutedEventArgs e)
    {
        AppState.ChoiceSession = null;
        if (AppState.CurentUser== null)
        {var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;

            var dialog = new SimpleWindow("Для покупки билета необходимо войти в аккаунт или зарегестрироваться(");
            dialog.ShowDialog(mainWindow);
            return;
        }
        Button btn = sender as Button;

        var Session = btn.DataContext as Session;
        AppState.ChoiceSession = btn.DataContext as Session;

        AppState.ChoiceSession = Session;
        NavigationService?.Navigate(new ChoiceSessionPage());
    }
}