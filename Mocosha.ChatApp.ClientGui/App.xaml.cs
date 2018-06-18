using Avalonia;
using Avalonia.Markup.Xaml;

namespace Mocosha.ChatApp.ClientGui
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
