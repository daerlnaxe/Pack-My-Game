﻿using DxTBoxCore.Box_Collec;
using DxTBoxCore.BoxChoose;
using DxTBoxCore.MBox;
using LaunchBox_XML.Container;
using LaunchBox_XML.Container.Game;
using LaunchBox_XML.XML;
using Pack_My_Game.IHM;
using Pack_My_Game.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;
using PS = Pack_My_Game.Properties.Settings;

namespace Pack_My_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly RoutedUICommand ExtractPlatformCmd = new RoutedUICommand("Extract", "ExtractPlatformCmd", typeof(MainWindow));

        private M_Main _Model = new M_Main();


        public Visibility ShowParameters
        {
            get
            {
                if (string.IsNullOrEmpty(_Model.LaunchBoxPath) || string.IsNullOrEmpty(_Model.WorkingFolder))
                    return Visibility.Hidden;
                else
                    return Visibility.Visible;
            }
        }


        public string WindowTitle
        {
            get
            {
                Assembly asmbly = Assembly.GetExecutingAssembly();
                return $"Pack My Game - {asmbly.GetName().Version}";

            }


        }


        public MainWindow()
        {
            _Model = new M_Main();

            InitializeComponent();
            DataContext = _Model;
        }






        /// <summary>
        /// MenuItem Config
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Config_Click(object sender, RoutedEventArgs e)
        {
            W_Config cfg = new IHM.W_Config();


            if (cfg.ShowDialog() == true)
            {
                _Model.Relocalize();
                _Model.ReloadConfig();

                //this.Hide();
                /*this.lLaunchBoxPath.Text = _lbPath = Properties.Settings.Default.LBPath;
                */
                // Change language
                /*   Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
                   MainWindow child = new MainWindow();
                   this.Close();
                   child.ShowDialog();*/
                //this.InitializeComponent();
                /*LoadUI();*/
            }
        }

        #region explorer
        private void LaunchBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", _Model.LaunchBoxPath);
        }

        private void WorkingPath_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", _Model.WorkingFolder);
        }
        #endregion

        /// <summary>
        /// Platform selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Platform_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (_Model.SelectedPlatform == null)
                return;

            _Model.LoadGames();
        }

        private void Game_Checked(object sender, RoutedEventArgs e)
        {
            ShortGame sgame = (ShortGame)((CheckBox)sender).Tag;
            _Model.AddGame(sgame);

        }

        private void Game_Unchecked(object sender, RoutedEventArgs e)
        {
            ShortGame sgame = (ShortGame)((CheckBox)sender).Tag;
            _Model.RemoveGame(sgame);
        }

        private void Game_WheelMouse(object sender, MouseWheelEventArgs e)
        {
            sv.ScrollToVerticalOffset(sv.VerticalOffset - e.Delta);

        }

        private void ListGame_DblClick(object sender, MouseButtonEventArgs e)
        {
            var game = (ShortGame)((ListViewItem)sender).Content;
            game.IsSelected = true;

        }

        private void Can_Process(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !_Model.HasErrors;
        }

        private void Exec_Process(object sender, ExecutedRoutedEventArgs e)
        {
            _Model.Process();
        }


        #region Windows from core



        #endregion

        private void button_Click(object sender, RoutedEventArgs e)
        {
            /*
            BoxProgress bp = new BoxProgress();
            bp.ShowDialog();*/
            W_Cheat window = new W_Cheat(@"G:\Temp\pmg - temp\Super Nintendo Entertainment System\Donkey Kong Country (E) (M3) (V1.0) [f1]\CheatCodes", "Donkey Kong Country", @"Donkey Kong Country-01.txt");
            window.ShowDialog();
        }

        #region crédits & help

        private void Credits_Click(object sender, RoutedEventArgs e)
        {
            W_Info window = new W_Info()
            {
                Title = "Credits",
                Message = "Credits",
                Texteuh = _Model.Lang.Credits,
            };
            window.ShowDialog();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            W_Info window = new W_Info()
            {
                Title = "Help",
                Message = "Help",
                Texteuh = _Model.Lang.Help,
            };
            window.ShowDialog();
        }
        #endregion

        #region Extraction de plateforme

        private void Can_ExtractPlatform(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _Model.SelectedPlatform != null;
        }

        private void Exec_ExtractPlatform(object sender, ExecutedRoutedEventArgs e)
        {
            TreeSave ts = new TreeSave()
            {
                Model = new M_SaveFile()
                {
                    StartingFolder = System.IO.Path.Combine(PS.Default.OutPPath, _Model.SelectedPlatform.Name),
                    ShowFiles = true,
                    Info = "Save to ...",
                    FileValue = "TBPlatform.xml"
                    
                }
            };
           
            if(ts.ShowDialog()== true)
            {
                XML_Platforms.ExtractPlatform(Path.Combine(PS.Default.LBPath,PS.Default.fPlatforms), _Model.SelectedPlatform.Name, ts.Model.LinkResult);
            }

           
        }
        #endregion
    }
}