using Common_PMG.Container;
using Common_PMG.Models;
using DxTBoxCore.Async_Box_Progress;
using DxTBoxCore.Box_Progress;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using UnPack_My_Game.Cores;
using UnPack_My_Game.Decompression;
using UnPack_My_Game.Resources;

namespace UnPack_My_Game.Models.LaunchBox
{
    class M_DPGUnpack : A_Err, I_Select
    {
        public string Information => Lang.I_DPGZipFile;
        public string SelectSentence => Lang.ContM_AddFile;

        public ObservableCollection<DataRep> Elements { get; set; } = new ObservableCollection<DataRep>();

        public M_DPGUnpack()
        {
            /*Elements.Add(new DataRep("Chat", "animal/chat"));
            Elements.Add(new DataRep("Chien", "animal/chien"));*/
        }


        public void Add()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog()
            {
                Multiselect = true,
                Filter = "Zip, 7Zip | *.zip;*.7zip;*.7z",
                InitialDirectory = Common.Config.LastArchivePath,
            };

            if (ofd.ShowDialog() == true)
            {
                Common.Config.LastArchivePath = Path.GetDirectoryName(ofd.FileName);
                Common.Config.Save();

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

            DPGMakerCore dpgC = new DPGMakerCore();
            TaskLauncher launcher = new TaskLauncher()
            {
                AutoCloseWindow = false,
                ProgressIHM = new DxStateProgress(dpgC),
                MethodToRun = () => dpgC.MakeDPG_Comp(Elements),                 
            };

            launcher.Launch(dpgC);

            return true;
        }
    }
}
