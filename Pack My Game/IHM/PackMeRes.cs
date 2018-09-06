using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pack_My_Game.IHM
{
    public partial class PackMeRes : Form
    {
        public PackMeRes()
        {
            InitializeComponent();
        }

        internal static void ShowDialog(bool vGame, bool vManual, bool vMusic, bool vVideo)
        {
            PackMeRes window = new PackMeRes();
            if (vGame) window.clb1.SetItemChecked(0, true);
            if (vManual) window.clb1.SetItemChecked(1, true);
            if (vMusic) window.clb1.SetItemChecked(2, true);
            if (vVideo) window.clb1.SetItemChecked(3, true);
        }
    }
}
