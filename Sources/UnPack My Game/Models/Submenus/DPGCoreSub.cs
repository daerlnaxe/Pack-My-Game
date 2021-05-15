using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using UnPack_My_Game.Graph;
using UnPack_My_Game.Language;
using UnPack_My_Game.Models.LaunchBox;

namespace UnPack_My_Game.Models.Submenus
{
    class DPGCoreSub : ISubMenu
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Category => LanguageManager.Instance.Lang.Word_Import;

        public ObservableCollection<ICommand> Buttons
        {
            get;
        } = new ObservableCollection<ICommand>();


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


        public DPGCoreSub()
        {
            Buttons.Add(new MyButtonCommand(LanguageManager.Instance.Lang.Word_Archive_s, LBcDPG_Unpack));
            Buttons.Add(new MyButtonCommand(LanguageManager.Instance.Lang.Word_Folder_s, LBcDPG_Folder));
        }


        private void LBcDPG_Unpack(object parameter)
        {
            ActivePage = new P_Selecter()
            {
                Model = new M_LBcDPGUnpack(),
            };
        }

        private void LBcDPG_Folder(object obj)
        {
            ActivePage = new P_Selecter()
            {
                Model = new M_LBcDPGFolder(),
            };
        }

    }
}
