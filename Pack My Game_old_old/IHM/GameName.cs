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
    public partial class GameName : Form
    {
        /// <summary>
        /// Nom du jeu suggéré
        /// </summary>
        public string SuggestedGameName { get; set; }


        /// <summary>
        /// Nom du jeu choisi
        /// </summary>
        public string ChoosenGameName { get; private set; }

        public GameName()
        {
            InitializeComponent();

        }

        private void GameName_Load(object sender, EventArgs e)
        {
            lGameName.Text = Lang._GameName;
            lChoosenName.Text = Lang.CustomGameName;
            lDescription.Text = Lang.IhmGameName;
            tbChosenName.Text = tbCurrentName.Text = SuggestedGameName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbChosenName_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '?':
                case '/':
                case '\\':
                case ':':
                case '*':
                case '>':
                case '<':
                    e.Handled = true;
                    break;
            }

        }

        /// <summary>
        /// Validation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btChange_Click(object sender, EventArgs e)
        {
            ChoosenGameName = tbChosenName.Text;
        }

        /// <summary>
        /// Annulation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCancel_Click(object sender, EventArgs e)
        {
            ChoosenGameName = SuggestedGameName;
        }
    }
}
