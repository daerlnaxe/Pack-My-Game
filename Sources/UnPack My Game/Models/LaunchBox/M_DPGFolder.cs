using Common_PMG.Container;
using Common_PMG.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using UnPack_My_Game.Cores;
using UnPack_My_Game.Language;

namespace UnPack_My_Game.Models.LaunchBox
{
    class M_DPGFolder : A_Err, I_Select
    {
        public string Information => LanguageManager.Instance.Lang.S_DPGwFolder;
        public string SelectSentence => LanguageManager.Instance.Lang.T_AddFolderCM;

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

            DPGMakerCore dpgC = new DPGMakerCore();
            /*TaskLauncher launcher = new TaskLauncher()
            {
                AutoCloseWindow = false,
                ProgressIHM = new DxStateProgress(dpgC),
                MethodToRun = () => dpgC.MakeDPG_Comp(Elements),
            };

            launcher.Launch(dpgC);*/
            dpgC.MakeDPG_Folders(Elements);

            return true;
        }
    }
}
