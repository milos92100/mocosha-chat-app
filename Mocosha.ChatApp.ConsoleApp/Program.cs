using Terminal.Gui;
using Mocosha.ChatApp.ConsoleApp.View;
using System.Collections.Generic;
using System;

namespace Mocosha.ChatApp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Init();
            var top = Application.Top;

            // Creates the top-level window to show
            var win = new MainWindow(new Rect(0, 1, top.Frame.Width, 35), "Mocosha Chat App");
            top.Add(win);

            win.OnLogin += new Action<string>((name) =>
            {

                win.ConctactsPanel.SetContactsList(new List<String>(new string[] {
                "Milos Stojanovic (milos)",
                "Pera Peric (pera)",
                "Johb Dow (johny)",
                "Will Smith (will)"
            }));

                win.ChatPanel.SendClicked += new Action<ChatPanel.MessageToSend>((msg) =>
                {
                    Console.WriteLine(msg.Content);

                });

            });

            Application.Run();

        }
    }
}
