﻿using Common_PMG.Container;
using Common_PMG.Models;
using DxTBoxCore.Box_Progress;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Forms;
using UnPack_My_Game.Cores;

namespace UnPack_My_Game.Models.LaunchBox
{
    class M_DPGFolder : A_Err, I_Select
    {
        public string Information => "euh";
        public string SelectSentence => "Ajoutez des dossiers via le menu contextuel";

        public ObservableCollection<DataRep> Elements { get; set; } = new ObservableCollection<DataRep>();

        public void Add()
        {
            FolderBrowserDialog ofd = new FolderBrowserDialog()
            {
                Description = "Select a folder containing the game",
                RootFolder = Environment.SpecialFolder.UserProfile,
                ShowNewFolderButton = false,
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.LastSpath = ofd.SelectedPath;
                Properties.Settings.Default.Save();

                Elements.Add(new DataRep(ofd.SelectedPath));
                Test_HasElement(Elements, nameof(Elements));
            }
        }


        public void RemoveElement(object element)
        {
            throw new NotImplementedException();
        }

        public void RemoveElements(IList<object> parameter)
        {
            throw new NotImplementedException();
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