using System;
using System.Collections.Generic;
using NStack;
using Terminal.Gui;

namespace Mocosha.ChatApp.ConsoleApp.View
{
    public class MainWindow : Window
    {
        private const string CHAT_PANEL_TITLE = "Chat";
        private const string CONCTACTS_PANEL_TITLE = "Online Contacts: ";

        
        public ContactsPanel ConctactsPanel { set; get; }
        public ChatPanel ChatPanel { set; get; }
        public LoginPanel LoginPanel { set; get; }

      
        public Action<string> OnLogin { set; get; }

        public MainWindow(Rect frame, ustring title = null) : base(frame, title)
        {
            InitLoginView();
        }

        private void InitLoginView()
        {

            LoginPanel = new LoginPanel( "Login");

            LoginPanel.OnLogin += new Action<string>((name) =>
            {
                Remove(LoginPanel);
                InitChatView(name);
                OnLogin?.Invoke(name);
            });

            LoginPanel.CancelClicked += new Action(() =>
            {
                Environment.Exit(0);
            });


            Add(LoginPanel);
        }


        private void InitChatView(string me)
        {
            ConctactsPanel = new ContactsPanel(new Rect(1, 1, 30, 30), CONCTACTS_PANEL_TITLE);
            ChatPanel = new ChatPanel(new Rect(33, 1, 50, 30), CHAT_PANEL_TITLE, me);

            Add(ConctactsPanel, ChatPanel);

            ConctactsPanel.ContactSelected += new Action<string>((name) =>
            {
                ChatPanel.SetChatContact(name);
            });


            SetNeedsDisplay();

        }
    }
}
