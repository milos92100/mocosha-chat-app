using System;
using System.Threading;
using Mocosha.ChatApp.Core.Messages;
using Newtonsoft.Json;
using WebSocketSharp;

namespace Mocosha.ChatApp.Client
{
    class Program
    {
        public static bool _run = true;
        public static bool _sereverResponded = false;

        protected static void SendMessage(Message msg, WebSocket ws)
        {
            string json = JsonConvert.SerializeObject(msg);
            ws.Send(json);
        }

        protected static void MessageReceived(object sender, MessageEventArgs e, string myName)
        {
            // Console.WriteLine("Received: " + e.Data);
            var msg = JsonConvert.DeserializeObject<Message>(e.Data);
            switch (msg.Type)
            {
                case Message.Types.STATUS:
                    Console.WriteLine("Server: " + msg.Content);
                    break;

                case Message.Types.PERSONAL:
                    Console.WriteLine($"{msg.From}: " + msg.Content);
                    Console.Write("Enter message: ");

                    break;
            }
            _sereverResponded = true;
        }

        static void Main(string[] args)
        {

            Console.WriteLine("To quit type 'quit'");
            Console.Write("Enter your name: ");
            string myName = Console.ReadLine();
            Console.Title = myName;

            using (var ws = new WebSocket("ws://127.0.0.1:1992/Chat"))
            {
                ws.SetCookie(new WebSocketSharp.Net.Cookie { Name = "username", Value = myName });
                ws.OnMessage += ((sender, e) => { MessageReceived(sender, e, myName); });

                ws.OnOpen += ((sender, e) =>
                {
                    Console.WriteLine("Connection opened");
                });


                ws.OnError += ((sender, e) =>
                {
                    Console.WriteLine($"Error: {e.Message}");
                    _sereverResponded = true;
                });


                ws.Connect();

                while (_run && ws.IsAlive)
                {
                    Console.Write("Enter message: ");
                    string message = Console.ReadLine();
                    Console.Write("To: ");
                    string to = Console.ReadLine();

                    var msg = new Message
                    {
                        Id = Guid.NewGuid().ToString(),
                        From = myName,
                        Type = Message.Types.PERSONAL,
                        To = to,
                        Content = message
                    };


                    if (message.Equals("quit"))
                    {
                        _run = false;
                    }

                    _sereverResponded = false;

                    SendMessage(msg, ws);

                    while (!_sereverResponded) { Thread.Sleep(10); }
                }
            }
        }
    }
}
