﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using UnPack_My_Game.Graph;
using UnPack_My_Game.Models.LaunchBox;

namespace UnPack_My_Game.Models.Submenus
{
    class DPGCoreSub : ISubMenu
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
            Buttons.Add(new MyButtonCommand("File(s)", DPGCore_File));
            Buttons.Add(new MyButtonCommand("Folder(s)", DPGCore_Folder));
        }


        private void DPGCore_File(object parameter)
        {
            ActivePage = new P_Selecter()
            {
                Model = new M_LBcDPGUnpack(),
            };
        }

        private void DPGCore_Folder(object obj)
        {
            ActivePage = new P_Selecter()
            {
                Model = new M_LBcDPGFolder(),
            };
        }

    }
}
