using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnPack_My_Game.Models.LaunchBox;
using UnPack_My_Game.Models.Submenus;

namespace UnPack_My_Game.Graph.LaunchBox
{
    /// <summary>
    /// Logique d'interaction pour LaunchBox_Main.xaml
    /// </summary>
    public partial class P_LaunchBox_Main : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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



        public P_LaunchBox_Main()
        {
            InitializeComponent();
            DataContext = this;
        }


        private void Depack_Click(object sender, RoutedEventArgs e)
        {
            ActivePage = new P_Selecter()
            {
                Model = new M_LBcDPGUnpack(),
            };
        }

        private void InjectG_Click(object sender, RoutedEventArgs e)
        {
            ActivePage = new P_Selecter()
            {
                Model = new M_LBCDPGFolder(),
            };
        }

        private void DPGMaker_Click(object sender, RoutedEventArgs e)
        {
            ActivePage = new P_SubMenu()
            {
                Driver = new DPGMakerSub(),
            };
        }


        private void InjectPlatform_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Config_Click(object sender, RoutedEventArgs e)
        {
            if (new W_Config().ShowDialog() == true)
            {

            }
            // var platform = _Model.SelectPlatform;
            /*
            I_Source p = _Model.ActiveSrcPage;


            _Model = new M_LaunchBox();
            _Model.ActiveSrcPage = p;
            //_Model.SelectPlatform = platform;

            DataContext = _Model;*/
        }

    }
}
