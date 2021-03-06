﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using UnPack_My_Game.Graph;
using UnPack_My_Game.Language;
using UnPack_My_Game.Models.LaunchBox;

namespace UnPack_My_Game.Models.Submenus
{
    internal class DPGMakerSub : ISubMenu
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Category => LanguageManager.Instance.Lang.S_DPGCreator;

        /// <summary>
        /// Liste des boutons à afficher
        /// </summary>
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

        public DPGMakerSub()
        {
            Buttons.Add(new MyButtonCommand(LanguageManager.Instance.Lang.Word_Archive_s, DPGCore_Archive));
            Buttons.Add(new MyButtonCommand(LanguageManager.Instance.Lang.Word_Folder_s, DPGCore_Folder));
        }


        //public RoutedUICommand QueFaire = new RoutedUICommand("test", nameof(meou), typeof(SubFilesNFolders));


        private void DPGCore_Archive(object parameter)
        {
            ActivePage = new P_Selecter()
            {
                Model = new M_DPGUnpack(),
            };
        }

        private void DPGCore_Folder(object obj)
        {
            ActivePage = new P_Selecter()
            {
                Model = new M_DPGFolder(),
            };
        }

        /*
        private void meou(object parameter)
        {
            ActivePage = null;
            Debug.WriteLine("Meou, Meou");
        }*/

    }
}
