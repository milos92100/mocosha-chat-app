using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Mocosha.ChatApp.ClientGui
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
            this.AttachDevTools();

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

        }

        public static void Btn_Clicked(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
