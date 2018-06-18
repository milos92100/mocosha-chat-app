using System;
using System.Collections.Generic;
using System.Text;
using NStack;
using Terminal.Gui;

namespace Mocosha.ChatApp.ConsoleApp.View
{
    public class ContactsPanel : FrameView
    {
        private MocoshaListView List;
        private List<string> Contacts;

        public Action<string> ContactSelected { set; get; }

        public ContactsPanel(Rect frame, ustring title) : base(frame, title)
        {
            InitView();
        }

        private void InitView()
        {
            List = new MocoshaListView(new Rect(1, 1, 26, 15), new List<string>())
            {
                AllowsMarking = true,
            };

            List.ItemSelected = new Action<int>((index) =>
            {
                var contact = Contacts[index];

                ContactSelected?.Invoke(contact);
            });

            Add(List);
        }

        public void SetContactsList(List<String> contacts)
        {
            Contacts = contacts;
            List.SetSource(contacts);
            List.SetNeedsDisplay();
        }

        public void RemoveContact(string name)
        {
            Contacts.Remove(name);
            List.SetSource(Contacts);
            List.SetNeedsDisplay();
        }

        public void AddContact(string name)
        {
            Contacts.Add(name);
            List.SetSource(Contacts);
            List.SetNeedsDisplay();
        }
    }
}
