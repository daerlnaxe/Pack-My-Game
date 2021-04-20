using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using UnPack_My_Game.Graph.LaunchBox;

namespace UnPack_My_Game.Cores
{
    class LaunchBoxCore_IHM
    {
        internal static void CreateDPG(string tmpPath)
        {
            string xmlFile = Path.Combine(tmpPath, "EBGame.xml");
            if (!File.Exists(xmlFile))
            {
                xmlFile = Path.Combine(tmpPath, "TBGame.xml");
            }


            /*

            Application.Current.Dispatcher?.Invoke(() =>
            {
                W_DPGMaker window = new W_DPGMaker()
                {
                    Model = new M_DPGMaker(tmpPath),
                };
                if (window.ShowDialog()== true)
                {

                }
                else
                {
                    throw new OperationCanceledException("Interrupted by user");
                }
            });*/
        }
    }
}
