namespace Mocosha.ChatApp.Core.Messages
{
    public class Message
    {
        public class Types
        {
            public const string PERSONAL = "Personal";
            public const string STATUS = "Status";
        }

        public string Id { get; set; }
        public string Type { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
    }
}
