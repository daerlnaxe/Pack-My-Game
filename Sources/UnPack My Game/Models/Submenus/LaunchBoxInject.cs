using System;
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
    class LaunchBoxInject : ISubMenu
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


        public LaunchBoxInject()
        {
            Buttons.Add(new MyButtonCommand( "Files", InjectByFiles));
            Buttons.Add(new MyButtonCommand( "Folders", InjectByFolders));
        }

        private void InjectByFiles(object parameter)
        {
            ActivePage = new P_Selecter()
            {
                Model = new M_DPGInjectFi(),
            };
        }
        private void InjectByFolders(object parameter)
        {
            ActivePage = new P_Selecter()
            {
                Model = new M_DPGInjectFo(),
            };
        }
    }
}
