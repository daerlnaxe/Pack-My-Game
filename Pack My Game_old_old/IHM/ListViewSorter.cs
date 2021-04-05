using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pack_My_Game.IHM
{
    class LvItemComparer: IComparer
    {
        private int _Col;
        private SortOrder _Order;

        public LvItemComparer()
        {
            _Col = 0;
            _Order = SortOrder.Ascending;
        }
        public LvItemComparer(int column, SortOrder order)
        {
            _Col = column;
            this._Order = order;
        }

        public int Compare(object x, object y)
        {
            int returnVal = -1;
            returnVal = String.Compare(((ListViewItem)x).SubItems[_Col].Text,
                            ((ListViewItem)y).SubItems[_Col].Text);
            
            if (_Order == SortOrder.Descending) returnVal *= -1;

            return returnVal;
        }
    }
}
