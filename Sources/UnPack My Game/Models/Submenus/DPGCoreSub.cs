using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace UnPack_My_Game.Models.Submenus
{
    class DPGCoreSub : ISubMenu
    {
        public ObservableCollection<ICommand> Buttons => throw new NotImplementedException();

        public Page ActivePage => throw new NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
