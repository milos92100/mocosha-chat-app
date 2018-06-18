using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Terminal.Gui;

namespace Mocosha.ChatApp.ConsoleApp.View
{
    public class MocoshaListView : ListView
    {
        public MocoshaListView(Rect rect, IList source) : base(rect, source)
        {
        }

        public Action<int> ItemSelected;

        public override bool MouseEvent(MouseEvent me)
        {
            base.MouseEvent(me);


            ItemSelected?.Invoke(SelectedItem);

            return true;
        }
    }
}
