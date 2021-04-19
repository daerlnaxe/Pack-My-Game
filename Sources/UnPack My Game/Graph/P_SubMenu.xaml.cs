using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnPack_My_Game.Models.Submenus;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UnPack_My_Game.Graph
{
    /// <summary>
    /// Logique d'interaction pour P_SubMenu.xaml
    /// </summary>
    public partial class P_SubMenu : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)) ;
        }


        private ISubMenu _Driver;
        public ISubMenu Driver 
        {
            get => _Driver;
            set
            {
                _Driver = value;
                OnPropertyChanged();
            }
        }

        public P_SubMenu()
        {
            InitializeComponent();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Driver;
        }
    }
}
