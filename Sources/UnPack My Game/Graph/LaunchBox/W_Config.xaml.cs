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
using System.Windows.Shapes;
using UnPack_My_Game.Resources;
using PS = UnPack_My_Game.Properties.Settings;

namespace UnPack_My_Game.Graph.LaunchBox
{
    /// <summary>
    /// Logique d'interaction pour W_Config.xaml
    /// </summary>
    public partial class W_Config : Window
    {
        private const string _Games = "Roms";
        private const string _CheatCodes = "CheatCodes";
        private const string _Images = "Images";
        private const string _Manuals = "Manuals";
        private const string _Musics = "Musics";
        private const string _Videos = "Videos";


        M_Config _Model = new M_Config();



        public W_Config()
        {
            InitializeComponent();
            DataContext = _Model;
        }



        /// <summary>
        /// When clicked on browse button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Find_Launchbox(object sender, RoutedEventArgs e)
        {
            TreeChoose tc = new TreeChoose()
            {
                Model = new M_ChooseRaw()
                {
                    Info = Lang.ChooseLBf,
                    StartingFolder = Properties.Settings.Default.LastLBpath,
                    Mode = ChooseMode.Folder,
                    ShowFiles = false,

                }
            };
            if (tc.ShowDialog() == true)
            {
                _Model.LaunchBoxPath = tc.LinkResult;
                //_Model.Set_LaunchBoxPath();
            }
        }

        private void tbLBPath_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                _Model.Set_LaunchBoxPath();

        }

        #region Reset
        private void ResetT_Click(object sender, RoutedEventArgs e)
        {
            _Model.Games = _Games;
            _Model.CheatCodes = _CheatCodes;
            _Model.Images = _Images;
            _Model.Manuals = _Manuals;
            _Model.Musics = _Musics;
            _Model.Videos = _Videos;

            Properties.Settings.Default.Save();
        }
        private void Raz_Games(object sender, RoutedEventArgs e)
        {
            _Model.Games = _Games;
            Properties.Settings.Default.Save();
        }

        private void Raz_CheatsCodes(object sender, RoutedEventArgs e)
        {
            _Model.CheatCodes = _CheatCodes;
            Properties.Settings.Default.Save();
        }

        private void Raz_Manuals(object sender, RoutedEventArgs e)
        {
            _Model.Manuals = _Manuals;
            Properties.Settings.Default.Save();
        }

        private void Raz_Images(object sender, RoutedEventArgs e)
        {
            _Model.Images = _Images;
            Properties.Settings.Default.Save();
        }

        private void Raz_Musics(object sender, RoutedEventArgs e)
        {
            _Model.Musics = _Musics;
            Properties.Settings.Default.Save();
        }

        private void Raz_Videos(object sender, RoutedEventArgs e)
        {
            _Model.Videos = _Videos;
            Properties.Settings.Default.Save();
        }



        #endregion

        private void Ergo_Checked(object sender, RoutedEventArgs e)
        {
            _Model.ChangePlatform = true;
            PS.Default.Save();
        }

        private void Ergo_UnChecked(object sender, RoutedEventArgs e)
        {
            _Model.ChangePlatform = false;
            PS.Default.Save();
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        #region 
        private void Can_Submit(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !_Model.HasErrors;
        }

        private void Exec_Submit(object sender, ExecutedRoutedEventArgs e)
        {
            if (_Model.Save() == true)
            {
                this.DialogResult = true;
                Close();

            }
        }
        #endregion
    }

}
