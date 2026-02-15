using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using pr14Avalonia.Models;
using WpfLikeAvaloniaNavigation;

namespace pr14Avalonia.Pages;

public partial class ProfilePage : Page
{
    private static List<Ticket> Tickets = Core.Context.Tickets.ToList();
    private static List<Ticket> UserTickets = new List<Ticket>();
    
    public ProfilePage()
    {
        foreach (Ticket tk in Tickets)
        {
             
                UserTickets.Add(tk);
            
        }

        foreach (Ticket tk in UserTickets)
        {
            
        }
        InitializeComponent();
        IdBlock.Text=$"Id-{AppState.CurentUser.Id}";
        LoginBlock.Text=$"Login-{AppState.CurentUser.Login}";
        Console.WriteLine(AppState.CurentUser.Id);
        
        SessionSP.ItemsSource = UserTickets;
    }

    private void Button_OnClick1(object? sender, RoutedEventArgs e)
    {
        NavigationService?.Navigate(new FirstPage());
    }

    private void Button_OnClick2(object? sender, RoutedEventArgs e)
    {
        if (NavigationService.CanGoBack)
        {
            NavigationService?.GoBack();
        }
    }
}