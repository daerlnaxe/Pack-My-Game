using Common_PMG.Container;
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
using UnPack_My_Game.Models;
using UnPack_My_Game.Resources;

namespace UnPack_My_Game.Graph
{

    /*
        Manage Extraction for several games
    */

    /// <summary>
    /// Logique d'interaction pour P_LaunchBoxS.xaml
    /// </summary>
    public partial class P_SGames : Page, I_Source
    {
        public static readonly RoutedUICommand SelectAllCommand = new RoutedUICommand("Lang.Select_All", "Select_All", typeof(Page));
        public static readonly RoutedUICommand DeselectAllCommand = new RoutedUICommand("Lang.DeSelect_all", "Deselect_All", typeof(Page));



        public A_Source Model { get; set; } = new M_SSource();

        public P_SGames()
        {
            InitializeComponent();
            DataContext = Model;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TreeChoose tc = new TreeChoose()
            {
                Model = new M_ChooseFolder()
                {
                    HideWindowsFolder = true,
                    ShowFiles = true,
                    StartingFolder = Properties.Settings.Default.LastSpath,
                    FilesExtension = new string[] { "zip", "7zip" }
                }
            };

            if (tc.ShowDialog() != true)
                return;

            M_SSource src= (M_SSource)Model;
            src.SelectedFolder = tc.LinkResult;
            src.Initialize_AvailableGames(tc.LinkResult);
        }

        private void Game_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = e.Source as CheckBox;
            Model.Add_Game((DataRep)cb.Tag);
        //    Model.CheckErrors();
        }

        private void Game_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = e.Source as CheckBox;
            Model.Remove_Game((DataRep) cb.Tag);
          //  Model.CheckErrors();
        }

        private void Can_Command(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !Model.HasErrors;
        }

        private void Exec_SelectAll(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (var g in ((M_SSource)Model).AvailableGames)
                g.IsSelected = true; // Ajoute automatiquement grâce au bind
        }

        private void Exec_DeselectAll(object sender, ExecutedRoutedEventArgs e)
        {
            foreach (var g in ((M_SSource)Model).AvailableGames)
                g.IsSelected = false; // Enlève automatiquement grâce au bind
        }
    }
}
