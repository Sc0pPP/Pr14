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
    public ProfilePage()
    {
        InitializeComponent();
        IdBlock.Text = $"Id-{AppState.CurentUser.Id}";
        LoginBlock.Text = $"Login-{AppState.CurentUser.Login}";
        LoadTickets();
    }

    public class TicketView
    {
        public int? Place { get; set; }  // <-- сделай nullable
        public DateTime PerchaseDateTime { get; set; }
        public decimal FinalPrice { get; set; }
        public string Status { get; set; }
        public string MovieName { get; set; }
    }

    private void LoadTickets()
    {
        var userId = AppState.CurentUser.Id;
    
        var tickets = (from t in Core.Context.Tickets
            join seat in Core.Context.Seats on t.SeatId equals seat.Id
            join ses in Core.Context.Sessions on seat.SessionId equals ses.Id
            join m in Core.Context.Movies on ses.MoviesId equals m.Id
            where t.UserId == userId
            select new Ticket
            {
                Id = t.Id,
                Place = t.Place,
                PerchaseDateTime = t.PerchaseDateTime,
                FinalPrice = t.FinalPrice,
                Status = t.Status,
                MoviesName = m.MovieName
            }).ToList();
        SessionSP.ItemsSource = tickets;
    }

    private void Button_OnClick1(object? sender, RoutedEventArgs e)
    {
        NavigationService?.Navigate(new FirstPage());
    }

    private void Button_OnClick2(object? sender, RoutedEventArgs e)
    {
        if (NavigationService.CanGoBack)
            NavigationService?.GoBack();
    }
}