using Pack_My_Game.IHM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pack_My_Game
{


    class InfoHandler
    {

        public string Prefix { get; set; }
        // public bool ActiveConsole { get; set; }
        public bool ActiveWindow { get; set; }

        public int Col1 { get; set; }
        public int Time { get; set; }

        public bool ActiveLog { get; set; }
        public string OutPutLog { get; set; }

        private InfoScreen IScreen { get; set; }

        /// <summary>
        /// Redirige le flux de sortie selon les choix
        /// </summary>
        /// <param name="prefix">Ajoute un préfix</param>
        /// <param name="console">Active la sortie sur console</param>
        /// <param name="window"></param>
        public InfoHandler(string prefix = null, bool window = true)
        {
            Prefix = prefix;
            ActiveWindow = window;

            if (ActiveWindow) IScreen = new InfoScreen();
        }

        public void ShowWindow()
        {
            if (ActiveWindow) IScreen.Show(); ;
        }

        public void SetLogActive(string fileLog, bool overWrite)
        {
            ActiveLog = true;
            OutPutLog = fileLog;

            if (overWrite) using (File.Open(fileLog, FileMode.Create)) ;
        }


        public void WriteLine(string message = null, bool prefix = true)
        {
            if (prefix) message = $"[{Prefix}] {message}";

            /*if (ActiveConsole)*/
            Debug.WriteLine(message);
            if (ActiveWindow) IScreen.WriteLine(message);
            if (ActiveLog)
            {
                using (StreamWriter fs = new StreamWriter(OutPutLog, true))
                {
                    fs.WriteLine(message);
                }
            }

        }

        public void Write(string message, bool prefix = true)
        {
            if (prefix) message = $"[{Prefix}] {message}";
            /*if (ActiveConsole)*/
            Debug.Write(message);
            if (ActiveWindow) IScreen.Write(message);
            if (ActiveLog)
            {
                using (StreamWriter fs = new StreamWriter(OutPutLog, true))
                {
                    fs.Write(message);
                }
            }

        }

        public void KillWindowAfter(int time)
        {
            IScreen.KillAfter(time);
        }

        public void KillWindow()
        {
            IScreen.Close();
        }




    }
}
