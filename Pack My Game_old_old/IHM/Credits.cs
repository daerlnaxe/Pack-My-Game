using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pack_My_Game.IHM
{
    public partial class Credits : Form
    {
        public Credits()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Credits_Load(object sender, EventArgs e)
        {
            var lang = Thread.CurrentThread.CurrentUICulture.TextInfo.CultureName;

            rtbCredits.Text = Lang.Credits_Content;
            /*
            switch (lang)
            {
                case "fr-FR":
                    rtbCredits.Text = File.ReadAllText(@".\Credits\Credits-Fr.txt");
                    break;
                default:
                    rtbCredits.Text = File.ReadAllText(@".\Credits\Credits-En.txt");
                    break;
            }*/
        }

        private void rtbCredits_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            ((RichTextBox)sender).Height = e.NewRectangle.Width + 5;
        }
    }
}
