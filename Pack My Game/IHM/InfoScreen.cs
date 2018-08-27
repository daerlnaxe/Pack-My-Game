using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pack_My_Game.IHM
{
    public partial class InfoScreen : Form
    {
        private bool _Block;

        public InfoScreen()
        {
            InitializeComponent();

        }

        internal void WriteLine(string text)
        {
            Log.Text += text + "\n";

            Log.SelectionStart = Log.Text.Length;
            Log.ScrollToCaret();
        }

        internal void Write(string text)
        {
            Log.Text += text;
            /*
            Log.SelectionStart = Log.Text.Length;
            Log.ScrollToCaret();*/
        }

        public string GetLog()
        {
            return Log.Text;
        }


        private void btLeave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        internal async void KillAfter(int time)
        {
            btLeave.Visible = true;
            btAlive.Visible = true;
            //Cursor.Current = Cursors.Default;
            UseWaitCursor = false;

            for (int i = time; i > 0; i--)
            {
                if (_Block)
                {
                    btLeave.Text = Lang.Leave;
                    break;
                }
                btLeave.Text = $"{Lang.Leave} ({i})";
                await Task.Delay(1000);
            }

            if (!_Block) this.Close();
        }

        private void btAlive_Click(object sender, EventArgs e)
        {
            _Block = true;

        }
    }
}
