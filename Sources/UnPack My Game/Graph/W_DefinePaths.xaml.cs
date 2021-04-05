using DxTBoxCore.BoxChoose;
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
using System.Windows.Shapes;
using UnPack_My_Game.Models;
using UnPack_My_Game.Resources;

namespace UnPack_My_Game.Graph
{
    /// <summary>
    /// Logique d'interaction pour W_DefinePaths.xaml
    /// </summary>
    public partial class W_DefinePaths : Window
    {
        public M_ModDefinePaths Model { get; set; }

        public W_DefinePaths()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;

        }

        private void Cheats_Click(object sender, RoutedEventArgs e)
        {
            string result = null;
            if ((result = ChooseFolder()) != null)
            {
                Model.RootCheats = result;
            }

        }
        private void Images_Click(object sender, RoutedEventArgs e)
        {
            string result = null;
            if ((result = ChooseFolder()) != null)
            {
                Model.RootImg = result;
            }
        }

        private string ChooseFolder()
        {
            TreeChoose tc = new TreeChoose()
            {
                Model = new M_ChooseFolder()
                {
                    Info = Lang.Choose_Root,
                    HideWindowsFolder = true,
                    ShowFiles = true,
                    StartingFolder = Properties.Settings.Default.LastTargetPath,

                }
            };
            if (tc.ShowDialog() == true)
            {
                return tc.Model.LinkResult;
            }
            return null;
        }

        private void Can_Valide(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Model != null)
                e.CanExecute = !Model.HasErrors;
        }

        private void Exec_Valide(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
