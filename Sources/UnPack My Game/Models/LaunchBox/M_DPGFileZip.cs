using Common_PMG.Container;
using DxLocalTransf.Progress.ToImp;
using DxTBoxCore.Box_Progress;
using DxTBoxCore.Box_Progress_2;
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
    class M_DPGFileZip : A_Err, I_DPG
    {
        public string Information => Lang.I_DPGZipFile;
        public string SelectSentence => "Ajoutez des fichiers via le menu contextuel";

        public ObservableCollection<DataRep> Elements { get; set; } = new ObservableCollection<DataRep>();

        public M_DPGFileZip()
        {
            /*Elements.Add(new DataRep("Chat", "animal/chat"));
            Elements.Add(new DataRep("Chien", "animal/chien"));*/
        }



        public void SelectFunc()
        {
            throw new NotImplementedException();
        }

        public void Add()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog()
            {
                Multiselect = true,
                Filter = "Zip, 7Zip | *.zip;*.7zip;*.7z",
                InitialDirectory = Properties.Settings.Default.LastSpath,
            };

            if (ofd.ShowDialog() == true)
            {
                Properties.Settings.Default.LastSpath = Path.GetDirectoryName(ofd.FileName);
                Properties.Settings.Default.Save();

                foreach (var file in ofd.FileNames)
                {
                    if (Elements.FirstOrDefault((x) => x.ALinkToThePast.Equals(file)) == null)
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

           /* DxAsDoubleProgress dd = new DxAsDoubleProgress()
            {
                Model = new UnModeleAHeriter(),
            };*/


            DPGCore dpgC = new DPGCore();
            DxAsStateProgress daspW = new DxAsStateProgress()
            {
                Model = dpgC,
                //  Model =
                //   Model = M_ProgressC.Create<DPGCore, M_ProgressCC>(dpgC, () => dpgC.MakeFileDPG(Elements)),

            };
            daspW.ShowDialog();
            return true;
        }

    }
}
