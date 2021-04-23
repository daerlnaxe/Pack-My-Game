using Common_PMG.Container;
using Common_PMG.Models;
using DxTBoxCore.Box_Progress;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using UnPack_My_Game.Cores;

namespace UnPack_My_Game.Models.LaunchBox
{
    class M_LBcDPGUnpack : A_Err, I_Select
    {
        public string Information => "Unpack to LaunchBox";

        public string SelectSentence => "Ajoutez des fichiers via le menu contextuel";

        public ObservableCollection<DataRep> Elements { get; set; } = new ObservableCollection<DataRep>();


        public void Add()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog()
            {
                Multiselect = true,
                Filter = "Zip, 7Zip | *.zip;*.7zip;*.7z",
                InitialDirectory = Properties.Settings.Default.LastFilePath,
            };

            if (ofd.ShowDialog() == true)
            {
                Properties.Settings.Default.LastFilePath = Path.GetDirectoryName(ofd.FileName);
                Properties.Settings.Default.Save();

                foreach (var file in ofd.FileNames)
                {
                    if (Elements.FirstOrDefault((x) => x.CurrentPath.Equals(file)) == null)
                        Elements.Add(new DataRep(file));
                }
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
                MethodToRun = () => lbDPGCore.Depacking(Elements),
            };

            launcher.Launch(lbDPGCore);

            return true;
        }


    }
}
