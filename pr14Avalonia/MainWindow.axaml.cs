using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Interactivity;
using pr14Avalonia.Pages;
using WpfLikeAvaloniaNavigation;
using Avalonia.Controls;
namespace pr14Avalonia;
        public partial class MainWindow : Window
        {
            public MainWindow()
            {
                InitializeComponent();
                MainContent.Navigate(new FirstPage());
            }

            private void BackButton_OnClick(object? sender, RoutedEventArgs e)
            { 
                
            }
        }
    

