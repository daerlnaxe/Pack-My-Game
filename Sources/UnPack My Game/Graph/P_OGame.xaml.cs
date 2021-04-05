using DxTBoxCore.BoxChoose;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using UnPack_My_Game.Cont;
using UnPack_My_Game.Models;
using UnPack_My_Game.Resources;

namespace UnPack_My_Game.Graph
{
    /*
        Select one game
     */

    /// <summary>
    /// Logique d'interaction pour P_LaunchBoxO.xaml
    /// </summary>
    public partial class P_OGame : Page, I_Source
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /*
        private IEnumerable<Game> _Games = new Game[0];
        public IEnumerable<Game> Games
        {
            get => _Games;
            set
            {
                _Games = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Games)));
            }
        }*/

        public A_Source Model { get; set; } = new M_OSource();

        //M_Source Model = new M_Source();

        public P_OGame()
        {
            InitializeComponent();
            DataContext = Model;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TreeChoose tc = new TreeChoose()
            {
                Model = new M_ChooseRaw()
                {
                    Info = Lang.S_Package,
                    StartingFolder = Properties.Settings.Default.LastSpath,
                    Mode = ChooseMode.File,
                    ShowFiles = true,
                }
            };

            if (tc.ShowDialog() != true)
                return;

            Model.Add_Game(tc.LinkResult);


        }

    }
}
