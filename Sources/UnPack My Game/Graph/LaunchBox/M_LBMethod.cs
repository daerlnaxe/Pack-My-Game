﻿using LaunchBox_XML.XML;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnPack_My_Game.Models;
using UnPack_My_Game.Resources;
using PS = UnPack_My_Game.Properties.Settings;

namespace UnPack_My_Game.Graph.LaunchBox
{
    public class M_LBMethod : A_Method
    {
        public event BasicHandler Error;

        public ObservableCollection<string> Machines { get; set; } = new ObservableCollection<string>();

        private string _SelectedPlatform;
        public string SelectPlatform
        {
            get => _SelectedPlatform;
            set
            {
                _SelectedPlatform = value;
                Test_NullValue(value);
            }
        }

        public M_LBMethod()
        {
            Load_Platforms();
        }

        internal void Load_Platforms()
        {
            Remove_Error(nameof(SelectPlatform));
            try
            {

            // Extracting XML datas
            XML_Functions xf = new XML_Functions();
            if (xf.ReadFile(Common.PlatformsFile))
                xf.ListMachine(Machines);
            }
            catch
            {
                Debug.WriteLine("Problem on platform.xml opening");
            }

        }

        public override void CheckError()
        {
            Test_NullValue(SelectPlatform, nameof(SelectPlatform));
        }
    }
}