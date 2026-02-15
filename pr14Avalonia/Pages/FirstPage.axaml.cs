using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using pr14Avalonia.Models;
using WpfLikeAvaloniaNavigation;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using MessageBox;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ButtonEnum = MessageBox.Avalonia.Enums.ButtonEnum;
using MessageBoxManager = MessageBox.Avalonia.MessageBoxManager;

namespace pr14Avalonia.Pages;

public partial class FirstPage : Page
{
    public static List<Movie> MoviesBd { get; } = Core.Context.Movies.ToList();

    public FirstPage()
    {
        InitializeComponent();
        foreach (var movie in MoviesBd)
        {
            var relativePath = movie.Url.Replace('/', Path.DirectorySeparatorChar);
            var fullPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, relativePath));

            if (File.Exists(fullPath))
                movie.Image = new Bitmap(fullPath);
            else
                Console.WriteLine($"Файл не найден: {fullPath}");
        }

        prod.ItemsSource = MoviesBd;
    }

    private void TextBox_TextChanged(object? sender, TextChangedEventArgs e)
    {
        List<Movie> moviesSearch = MoviesBd.Where(p => p.MovieName.ToLower().Contains(Search.Text.ToLower())).ToList();
        prod.ItemsSource = moviesSearch;
    }

    private void Button_Click(object? sender, RoutedEventArgs e)
    {
        NavigationService?.Navigate(new RegistrationPage());
    }

    private void Button_Click_2(object? sender, RoutedEventArgs e)
    {
        List<Movie> MoviesRating = (List<Movie>)MoviesBd.OrderByDescending(u => u.Rating).ToList();
        prod.ItemsSource = MoviesRating;
    }

    private void Button_Click_3(object? sender, RoutedEventArgs e)
    {
        List<Movie> MoviesName = (List<Movie>)MoviesBd.OrderBy(u => u.MovieName).ToList();
        prod.ItemsSource = MoviesName;
    }

    private void Button_Click_1(object? sender, RoutedEventArgs e)
    {
        if (AppState.CurentUser == null)
        {var mainWindow = (Application.Current.ApplicationLifetime
                as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
            var dialogg= new SimpleWindow("Не войдено(");
            dialogg.ShowDialog(mainWindow);
            return;
        }

        NavigationService?.Navigate(new ProfilePage());
    }
    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        if (NavigationService.CanGoBack)
        {
            NavigationService.GoBack();
        }
        else
        {var mainWindow = (Application.Current.ApplicationLifetime
            as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
        
            var dialog = new SimpleWindow("Некуда назад(");
            dialog.ShowDialog(mainWindow);
        }
    }


    private void Button_Click_4(object? sender, RoutedEventArgs e)
    {
        NavigationService?.Navigate(new EntrancePage());
    }

    private void Movie_Click(object? sender, RoutedEventArgs e)
    {
        Button btn = sender as Button;

        var Movie = btn.DataContext as Movie;
        AppState.CurrentMovie = btn.DataContext as Movie;
        AppState.CurentSession.Clear();
        List<Session> SessionsList =Core.Context.Sessions.ToList();
        foreach (Session session in SessionsList)
        {
            if (AppState.CurrentMovie.Id == session.MoviesId)
            {
                AppState.CurentSession.Add(session);
            }
        }  
        NavigationService?.Navigate(new CurentFilmPage());
       
    }
}
