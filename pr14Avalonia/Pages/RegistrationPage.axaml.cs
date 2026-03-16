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

public partial class RegistrationPage : Page
{ 
    public List<User> Users_bd = Core.Context.Users.ToList();
    public RegistrationPage()
    {
        InitializeComponent();
    }

    private void Button_Click(object? sender, RoutedEventArgs e)
    {
        registration(UserPassword.Text,PasswordProv.Text,UserName.Text);
    }

    bool registration(string pasw,string paswsec, string login)
    {
        var mainWindow = (Application.Current.ApplicationLifetime
            as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
        if (string.IsNullOrWhiteSpace(UserPassword.Text) | string.IsNullOrWhiteSpace(pasw) | string.IsNullOrWhiteSpace(login))
        {
            
        
            var dialog = new SimpleWindow("не все поля заполнены");
            dialog.ShowDialog(mainWindow);
            return false;
        }
        if (Users_bd.FirstOrDefault(u => u.Login == login) != null)
        {
           
            var dialoga = new SimpleWindow("логин занят");
            dialoga.ShowDialog(mainWindow);
            return false;

        }
        if (pasw != paswsec)
        {
           
            var dialogaa = new SimpleWindow("пароли не совпадают");
            dialogaa.ShowDialog(mainWindow);

            return false;
        }               
        User newUser = new User // создание нового пользователя
        {
            Login = login,
            Password = pasw,
        };
        AppState.CurentUser=newUser;
        Core.Context.Users.Add(newUser); // добавление пользователя в таблицу в БД
        Core.Context.SaveChanges(); // сохранение изменений в БД
        var dialogg = new SimpleWindow("аккаунт создан");
        dialogg.ShowDialog(mainWindow);
        return true;
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {if(NavigationService.CanGoBack)NavigationService.GoBack();
    }
}