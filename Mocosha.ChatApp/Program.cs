using Mocosha.ChatApp.Core.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Mocosha.ChatApp
{
    public class Chat : WebSocketBehavior
    {
        private string _suffix;

        public Chat()
          : this(null)
        {

        }

        public Chat(string suffix)
        {
            _suffix = suffix ?? String.Empty;
        }

        private void Answer()
        {
            string json = JsonConvert.SerializeObject(new Message
            {
                Id = Guid.NewGuid().ToString(),
                Type = Message.Types.STATUS,
                Content = "Message sent"

            });
            Send(json);
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            var name = Context.CookieCollection["username"];
            if (name != null)
            {
                Console.WriteLine($"User: {name.Value} connected; {ID}");
                Program.serverClients.Add(name.Value, ID);

            }
        }

        protected override void OnClose(CloseEventArgs e)
        {
            base.OnClose(e);
            var name = Context.CookieCollection["username"];
            if (name != null)
            {
                Console.WriteLine($"User: {name} disconnected; {ID}");
                Program.serverClients.Remove(name.Value);

            }
        }

        protected override void OnMessage(MessageEventArgs e)
        {

            Console.WriteLine($"Chat received: {e.Data}");
            var message = JsonConvert.DeserializeObject<Message>(e.Data);

            var to = Program.serverClients.GetValueOrDefault(message.To, null);
            if (to != null)
            {
                //Sessions.Broadcast(e.Data);
                Sessions.SendTo(e.Data, to);
                Answer();
            }
            else
            {

                string json = JsonConvert.SerializeObject(new Message
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = Message.Types.STATUS,
                    Content = $"user: {message.To} not found"

                });
                Send(json);

                // appent to meessages that will be sent when client is connected
            }
        }
    }

    public class Program
    {
        public static Dictionary<string, string> serverClients = new Dictionary<string, string>();


        static void Main(string[] args)
        {
            var wssv = new WebSocketServer("ws://127.0.0.1:1992");
            wssv.AddWebSocketService<Chat>("/Chat");
            wssv.Start();

            Console.Title = "Chat Server";

            Console.ReadKey(true);
            wssv.Stop();
        }
    }
}
