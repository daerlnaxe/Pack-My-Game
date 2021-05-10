using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using UnPack_My_Game.Graph;
using UnPack_My_Game.Graph.LaunchBox;
using UnPack_My_Game.Models.Submenus;

namespace UnPack_My_Game.Models
{
    class M_Main: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Dossier de LaunchBox
        /// </summary>
        public string LaunchBoxPath => Common.Config.LaunchBoxPath;

        public string WorkingFolder => Common.Config.WorkingFolder;


        private Page _ActivePage;
        public Page ActivePage
        {
            get => _ActivePage;
            set
            {
                _ActivePage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActivePage)));
            }
        }

        internal void Depack()
        {
            ActivePage = new P_SubMenu()
            {
                Driver = new DPGCoreSub(),
            };
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
            if (new W_Config().ShowDialog() == true)
            {
                OnPropertyChanged(nameof(LaunchBoxPath));
                OnPropertyChanged(nameof(WorkingFolder));
                return true;
            }

            return false;
        }
    }
}
