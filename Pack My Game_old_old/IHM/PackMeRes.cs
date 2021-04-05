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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vGame">Résultat de la copy du jeu</param>
        /// <param name="vManual">Résultat de la copy du manuel</param>
        /// <param name="vMusic">Résultat de la copy de la musique</param>
        /// <param name="vVideo">Résultat de la copy de la video</param>
        /// <param name="vApp">Résultat de la copie des apps</param>
        internal static void ShowDialog(bool vGame, bool vManual, bool vMusic, bool vVideo, bool vApp)
        {
            PackMeRes window = new PackMeRes();
            if (vGame)
                window.clb1.SetItemChecked(0, true);
            if (vManual)
                window.clb1.SetItemChecked(1, true);
            if (vMusic)
                window.clb1.SetItemChecked(2, true);
            if (vVideo)
                window.clb1.SetItemChecked(3, true);
            if (vApp)
                window.clb1.SetItemChecked(4, true);

            window.ShowDialog();
        }
    }
}
