using System;
using System.Collections.Generic;
using System.Text;
using NStack;
using Terminal.Gui;

namespace Mocosha.ChatApp.ConsoleApp.View
{
    public class LoginPanel : FrameView
    {
        public TextField UsernameInput;
        public Button OkButton;
        public Button CancelButton;

        public Action<string> OnLogin { set; get; }
        public Action CancelClicked { set; get; }

        public LoginPanel(ustring title) : base(title)
        {
            InitView();
        }

        private void InitView()
        {
            UsernameInput = new TextField(14, 2, 40, "");
            OkButton = new Button(3, 14, "Ok");
            CancelButton = new Button(10, 14, "Cancel");

            OkButton.Clicked += new Action(() =>
            {
                var name = UsernameInput.Text.TrimSpace().ToString();
                if (!string.IsNullOrEmpty(name))
                {
                    OnLogin?.Invoke(name);


                }
            });

            CancelButton.Clicked += new Action(() =>
            {
                CancelClicked?.Invoke();
            });


            Add(
            new Label(3, 2, "Login: "),
            UsernameInput,
            new Label(3, 4, "Password: "),
            new TextField(14, 4, 40, "") { Secret = true },
            OkButton,
            CancelButton);
        }
    }
}
