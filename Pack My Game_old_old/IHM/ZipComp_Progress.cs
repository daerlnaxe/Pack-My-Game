using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace Pack_My_Game.IHM
{

    public partial class ZipComp_Progress : Form
    {
        delegate void UpdateProgress(int value);
        public ZipComp_Progress()
        {
            InitializeComponent();


        }

        /// <summary>
        ///  Update the entry progress bar without calculation
        /// </summary>
        /// <remarks>Work async</remarks>
        /// <param name="percent"></param>
        public void UpdateEntryBar(int percent)
        {
            if (this.entryProgress.InvokeRequired)
            {
                UpdateProgress d = new UpdateProgress(AsyncUpdateEntryBar);
                this.Invoke(d, new object[] { percent });
            }
            else
            {
                this.entryProgress.Value = percent;
                
                //.Value = percent;

             //   lbEntryP.Refresh();
            }
        }

        public void AsyncUpdateEntryTxt(int percent)
        {
            if (this.lbEntryP.InvokeRequired)
            {
                UpdateProgress d = new UpdateProgress(AsyncUpdateEntryBar);
                this.Invoke(d, new object[] { percent });
            }
            else
            {
                // this.entryProgress.Value = percent;
                this.lbEntryP.Text = $"{percent} %";
                
                //   lbEntryP.Refresh();
            }
        }

        #region EntryBar: Concerne la barre de progression de fichier

        /// <summary>
        /// Evite la division par 0
        /// </summary>
        public void EntryInit()
        {
            lbEntryP.Text = "0%";
            entryProgress.Value = 0;
            lbEntryP.Refresh();

        }

        /// <summary>
        /// Update the entry progress bar by Calc
        /// </summary>
        /// <param name="part">Part bytes</param>
        /// <param name="total">Total bytes</param>
        public void EntryCalcUpdate(long part, long total)
        {
            int val = (int)(part * 100 / total);
            this.entryProgress.Value = val;
            lbEntryP.Text = $"{ val} %";

            lbEntryP.Refresh();
        }


        /*
        public void EntryFinalize()
        {
            lbEntryP.Text = "100%";
            entryProgress.Value = 100;
        }*/
        #endregion


        /// <summary>
        /// Concerne la barre de progression totale
        /// txt 0/0
        /// Globalprogress
        /// Global %
        /// </summary>
        #region EntryBar
        /*
    public void GlobalInit(int total)
    {
        lbTotalP.Text = "0%";
        totalProgress.Value = 0;
    }*/

        public void GlobalProgress(int current, int total)
        {
            int val = (int)(current * 100 / total);
            lbTotalP.Text = $"{ val}";

            totalProgress.Value = current;
            totalProgress.Maximum = total;
            lbFiles.Text = $"{current}/{total}";

            lbFiles.Refresh();
            lbTotalP.Refresh();
        }

        public void GlobalFinalize(int total)
        {
            lbEntryP.Text = "100%";
            totalProgress.Value = 100;
            lbFiles.Text = $"{total}/{total}";

            lbFiles.Refresh();
            lbTotalP.Refresh();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        public void SetCurrentTask(string info)
        {
            lbInfos.Text = info;
            lbInfos.Refresh();
            labelTask.Refresh();

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
