using Common_PMG.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using UnPack_My_Game.Graph;
using UnPack_My_Game.Graph.LaunchBox;
using UnPack_My_Game.Language;
using UnPack_My_Game.Models.Submenus;
using static UnPack_My_Game.Common;


namespace UnPack_My_Game.Models
{
    class M_Main: A_Err
    {

        /// <summary>
        /// Dossier de LaunchBox
        /// </summary>
        public string LaunchBoxPath => Common.Config.HLaunchBoxPath;

        public string WorkingFolder => Common.Config.HWorkingFolder;


        private Page _ActivePage;
        public Page ActivePage
        {
            get => _ActivePage;
            set
            {
                _ActivePage = value;
                OnPropertyChanged();
            }
        }

        internal void Depack()
        {
            ActivePage = new P_SubMenu()
            {
                Driver = new DPGCoreSub(),
            };
        }

        internal void CheckPaths()
        {
            _Errors.Clear();
            if (string.IsNullOrEmpty(LaunchBoxPath))
            {
                Add_Error("LaunchBox path is null", nameof(LaunchBoxPath));
            }
            else if(!File.Exists(Path.Combine(LaunchBoxPath, Common.Config.PlatformsFile)))
            {
                Add_Error("LaunchBox path doesn't contain platform file", nameof(LaunchBoxPath));
            }

            if (string.IsNullOrEmpty(WorkingFolder))
            {
                Add_Error("Working folder is null", nameof(WorkingFolder));
            }
            else if(!Directory.Exists(WorkingFolder))
            {
                Add_Error("Working folder doesn't exist", nameof(WorkingFolder));
            }
        }

        internal void InjectGame()
        {
            ActivePage = new P_SubMenu()
            {
                Driver = new LaunchBoxInject(),
            };
        }

        internal void DpgMaker()
        {
            ActivePage = new P_SubMenu()
            {
                Driver = new DPGMakerSub(),
            };
        }

        internal bool Config()
        {
            var oldLang = LanguageManager.Instance.CurrentLanguage;
            if (new W_Config().ShowDialog() == true)
            {
                _Errors.Clear();
                OnPropertyChanged(nameof(LaunchBoxPath));
                OnPropertyChanged(nameof(WorkingFolder));
                return true;
            }

            LanguageManager.Instance.CurrentLanguage = oldLang;
            return false;
        }
    }
}
