using System;
using System.Collections.Generic;
using System.Text;

namespace Mocosha.ChatApp.Core.Messages
{
    public class Notification
    {
        public class ActionType
        {
            public const string CONNECTED = "Connected";
            public const string DISCONECTED = "Disconected";
        }

        public string Id { get; set; }
        public string User { get; set; }
        public string Action { get; set; }
    }
}
