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
    public partial class ProgressCompFolder : Form
    {
        delegate void nothin();


        public ProgressCompFolder()
        {
            InitializeComponent();
        }

        public void StopIt()
        {
            if (this.InvokeRequired)
            {
                nothin d = new nothin(StopIt);
                this.Invoke(d, new object []{ });
            }
            else
            {
                this.Close();
            }
        }


    }
}
