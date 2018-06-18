using System;
using System.Collections.Generic;
using System.Text;
using NStack;
using Terminal.Gui;

namespace Mocosha.ChatApp.ConsoleApp.View
{
    public class ChatPanel : FrameView
    {
        public class MessageToSend
        {
            public string To { set; get; }
            public string Content { set; get; }
        }

        public class HostoryMessage
        {
            public string From { set; get; }
            public string Content { set; get; }
        }

        private Label ChatHistory;
        private TextField MessageInput;
        private Button SendButton;

        private string Contact;
        private string Me;

        public Action<MessageToSend> SendClicked { set; get; }

        public ChatPanel(Rect frame, ustring title, string me) : base(frame, title)
        {
            Me = me;
            InitView();
        }

        public void SetChatContact(string contact)
        {
            Contact = contact;
            Title = "Chat: " + contact;


            ChatHistory.Text = "";
            MessageInput.Text = "";

            SetNeedsDisplay();
        }

        private void InitView()
        {
            var ChatHistoryFrame = new FrameView(new Rect(1, 1, 45, 22), "History: ");
            ChatHistory = new Label(new Rect(1, 1, 40, 18), "");

            ChatHistoryFrame.Add(ChatHistory);

            var MessageInputFrame = new FrameView(new Rect(1, 23, 30, 5), "Message: ");
            MessageInput = new TextField(1, 1, 25, "");
            MessageInputFrame.Add(MessageInput);

            SendButton = new Button(32, 23, "Send");
            SendButton.Clicked += new Action(() =>
            {

                var message = MessageInput.Text.ToString();

                if (SendClicked != null && !String.IsNullOrEmpty(message))
                {
                    SendClicked.Invoke(new MessageToSend { To = Contact, Content = message });
                    ChatHistory.Text += message + "\n";

                    MessageInput.Text = "";
                }
            });

            Add(ChatHistoryFrame);
            Add(MessageInputFrame);
            Add(SendButton);
        }
    }
}
