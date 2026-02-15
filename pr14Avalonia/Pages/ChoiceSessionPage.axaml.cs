using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Microsoft.EntityFrameworkCore;
using pr14Avalonia.Models;
using WpfLikeAvaloniaNavigation;

namespace pr14Avalonia.Pages;

public partial class ChoiceSessionPage : Page
{
    public List<Ticket> ChoosedTickets= new List <Ticket>();
    
    public List<Ticket> BoughtTickets = Core.Context.Tickets
        .Include(t => t.Seat) // <-- обязательно Include!
        .ToList();
    public ChoiceSessionPage()
    {
        
        List<Hall> ListHall = Core.Context.Halls.ToList();
        InitializeComponent();
       
        int CurentHallId=AppState.ChoiceSession.HallId;
        Hall CurentHall=ListHall.First(u=>u.Id==CurentHallId);
        GenerateSeats(CurentHall.RowsCount,CurentHall.SeatPerRow/CurentHall.RowsCount);
        DisableBoughtSeats(BoughtTickets);
    }

    private void DisableBoughtSeats(List<Ticket> boughtTickets)
    {
        // Фильтруем только билеты для текущего сеанса
        var currentSessionTickets = BoughtTickets
            .Where(t => t.Seat != null && t.Seat.SessionId == AppState.ChoiceSession.Id)
            .ToList();

        foreach (var ticket in currentSessionTickets)
        {
            var btn = SeatsPanel.Children
                .OfType<CheckBox>()
                .FirstOrDefault(b => b.Tag is int id && id == ticket.Place);

            if (btn != null)
            {
                btn.IsEnabled = false;
                btn.Background = Brushes.Gray;
            }
        
}
    }


    private void GenerateSeats(int rows, int seatsPerRow)
    {
        SeatsPanel.Children.Clear();
        SeatsPanel.Columns = seatsPerRow;

        for (int r = 1; r <= rows; r++)
        {
            for (int s = 1; s <= seatsPerRow; s++)
            {
                int seatId = r * 1000 + s; // row*1000 + seat → совпадение с Ticket.Place

                var btn = new CheckBox()
                {
                    Width = 30,
                    Height = 10,
                    Margin = new Thickness(3),
                    Tag = seatId, // Tag = seatId
                    CornerRadius = new CornerRadius(20),
                    Background = Brushes.BlueViolet
                };

                btn.Checked += SeatClicked;
                btn.Unchecked += SeatClicked;

                SeatsPanel.Children.Add(btn);
            }
        }
    }

    private void SeatClicked(object? sender, RoutedEventArgs e)
    {
        var btn = sender as CheckBox;
        if (btn == null) return;

        int seatId = (int)btn.Tag; // Tag = int, напрямую

        string status = AppState.ChoiceSession.StartDateTime < DateTime.Now
            ? "прошел"
            : "ожидается";

        if (btn.IsChecked == true)
        {
            // Создаём Seat
            var seat = new Seat()
            {
                SessionId = AppState.ChoiceSession.Id,
                Place = seatId
            };
            Core.Context.Seats.Add(seat);
            Core.Context.SaveChanges(); // EF присвоит seat.Id

            // Создаём Ticket
            var ticket = new Ticket()
            {
                SeatId = seat.Id,
                UserId = AppState.CurentUser.Id,
                Place = seatId, // совпадает с Tag кнопки
                PerchaseDateTime = DateTime.Now,
                FinalPrice = AppState.ChoiceSession.BaseTicketPrice,
                Status = status
            };

            ChoosedTickets.Add(ticket);
        }
        else
        {
            // Удаляем выбранный билет по Place
            var ticketToRemove = ChoosedTickets.FirstOrDefault(t => t.Place == seatId);
            if (ticketToRemove != null)
                ChoosedTickets.Remove(ticketToRemove);
        }

        // Для отладки: показываем выбранные места
        foreach (Ticket tk in ChoosedTickets)
            Console.WriteLine($"Selected SeatID: {tk.Place}");

        UpdatePrice();
    }

    private void UpdatePrice()
    {
        decimal price = ChoosedTickets.Sum(t => t.FinalPrice);
        PriceBox.Text = $"Цена: {price}";
    }

    
    private string GenerateSeatId(int row, int seat)
    {
        return $"{row:D2}0{seat:D3}";
    }


    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        if (NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
    }

    private void Button_OnClick1(object? sender, RoutedEventArgs e)
    {
        foreach (Ticket tk in ChoosedTickets )
        {
            Core.Context.Tickets.Add(tk);
            Core.Context.SaveChanges();
            
        }
        var mainWindow = (Application.Current.ApplicationLifetime
            as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
        var dialogg= new SimpleWindow("Билеты приобретены,можете просмотреть их в личном кабинете");
        dialogg.ShowDialog(mainWindow);
    }
   
    
}




