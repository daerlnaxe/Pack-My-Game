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
    public partial class Help : Form
    {


        public Help()
        {
            InitializeComponent();
        }


        private void Help_Load(object sender, EventArgs e)
        {
            var lang = Thread.CurrentThread.CurrentUICulture.TextInfo.CultureName;

            switch (lang)
            {
                case "fr-FR":
                    rtbHelp.Text = File.ReadAllText(@".\Credits\Credits-Fr.txt");
                    break;
                default:
                    rtbHelp.Text = File.ReadAllText(@".\Credits\Credits-En.txt");
                    break;
            }
        }
    }
}
