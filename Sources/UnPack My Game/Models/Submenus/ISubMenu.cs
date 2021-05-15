using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace UnPack_My_Game.Models.Submenus
{
    public interface ISubMenu: INotifyPropertyChanged
    {
        string Category { get; }

        /// <summary>
        /// Liste des boutons à afficher
        /// </summary>
        ObservableCollection<ICommand> Buttons { get; }

        /// <summary>
        /// Page à afficher
        /// </summary>
         Page ActivePage { get; }
    }
}
