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
        entrance(UserPassword,UserName);
    }

    void entrance(TextBox pasw,TextBox login)
    {
        var mainWindow = (Application.Current.ApplicationLifetime
            as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
        if (string.IsNullOrWhiteSpace(pasw.Text) | string.IsNullOrWhiteSpace(login.Text))
        {
            var dialog = new SimpleWindow("не все поля заполнены");
            dialog.ShowDialog(mainWindow);
            return;
        }
        if (Users_bd.FirstOrDefault(u => u.Login == login.Text) == null)
        {
           
            var dialoga = new SimpleWindow("логин занят");
            dialoga.ShowDialog(mainWindow);
            return;

        }
        AppState.CurentUser=Users_bd.FirstOrDefault(u=>u.Login==UserName.Text);    
        NavigationService?.Navigate(new ProfilePage());
    }
}