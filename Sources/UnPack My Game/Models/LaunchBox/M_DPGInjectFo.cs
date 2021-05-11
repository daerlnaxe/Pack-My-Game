using Common_PMG.Container;
using Common_PMG.Models;
using DxTBoxCore.Box_Progress;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UnPack_My_Game.Cores;
using UnPack_My_Game.Models.Submenus;

namespace UnPack_My_Game.Models.LaunchBox
{
    class M_DPGInjectFo : A_Err, I_Select
    {
        public string Information => "Injection by Folder(s)";

        public string SelectSentence => "Ajoutez des dossiers via le menu contextuel";

        public ObservableCollection<DataRep> Elements { get; set; } = new ObservableCollection<DataRep>();

        public void Add()
        {
            FolderBrowserDialog ofd = new FolderBrowserDialog()
            {
                Description = "Select a folder containing the game",
                RootFolder = Environment.SpecialFolder.UserProfile,
                ShowNewFolderButton = false,
                SelectedPath = Common.Config.LastFolderPath,
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Common.Config.LastFolderPath = ofd.SelectedPath;
                Common.Config.Save();

                Elements.Add(new DataRep(ofd.SelectedPath));
                Test_HasElement(Elements, nameof(Elements));
            }
        }

        public void RemoveElement(object element)
        {
            Elements.Remove((DataRep)element);
            Test_HasElement(Elements, nameof(Elements));
        }

        public void RemoveElements(IList<object> parameter)
        {
            foreach (DataRep data in parameter.ToList())
            {
                Elements.Remove(data);
            }
            Test_HasElement(Elements, nameof(Elements));
        }


        public bool Process()
        {
            Test_HasElement(Elements, nameof(Elements));

            if (HasErrors)
                return false;

            DataPackGCore lbDPGCore = new DataPackGCore();
            TaskLauncher launcher = new TaskLauncher()
            {
                AutoCloseWindow = false,
                ProgressIHM = new DxStateProgress(lbDPGCore),
                MethodToRun = () => lbDPGCore.InjectGamesFo(Elements),
            };

            launcher.Launch(lbDPGCore);

            return true;
        }
    }
}
