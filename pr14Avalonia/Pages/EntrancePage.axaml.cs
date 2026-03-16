using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using WpfLikeAvaloniaNavigation;
using Avalonia.Interactivity;
using pr14Avalonia.Models;

namespace pr14Avalonia.Pages;

public partial class EntrancePage : Page
{
    public List<User> Users_bd = Core.Context.Users.ToList();

    public EntrancePage()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        NavigationService?.GoBack();
    }

    private void Button_Click(object? sender, RoutedEventArgs e)
    {
        entrance(UserPassword.Text,UserName.Text);
    }

    bool entrance(string pasw,string login)
    {
        var mainWindow = (Application.Current.ApplicationLifetime
            as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
        if (string.IsNullOrWhiteSpace(pasw) | string.IsNullOrWhiteSpace(login))
        {
            var dialog = new SimpleWindow("не все поля заполнены");
            dialog.ShowDialog(mainWindow);
            return false;
        }
        if (Users_bd.FirstOrDefault(u => u.Login == login) == null)
        {
           
            var dialoga = new SimpleWindow("логин занят");
            dialoga.ShowDialog(mainWindow);
            return false;

        }
        AppState.CurentUser=Users_bd.FirstOrDefault(u=>u.Login==UserName.Text);    
        NavigationService?.Navigate(new ProfilePage());
        return true;
    }
}