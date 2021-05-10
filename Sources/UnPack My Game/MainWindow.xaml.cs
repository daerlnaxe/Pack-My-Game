using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UnPack_My_Game.Cores;
using UnPack_My_Game.Cores.LaunchBox;
using UnPack_My_Game.Models.LaunchBox;
using UnPack_My_Game.Models.Submenus;
using static UnPack_My_Game.Common;
using System.Diagnostics;
using UnPack_My_Game.Models;
using System.Reflection;

namespace UnPack_My_Game.Graph.LaunchBox
{
    /// <summary>
    /// Logique d'interaction pour LaunchBox_Main.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _LaunchBoxOk;

        public static readonly RoutedCommand DepackCmd = new RoutedCommand();
        public static readonly RoutedCommand InjectGCmd = new RoutedCommand();
        public static readonly RoutedCommand InjectPCmd = new RoutedCommand();



        M_Main Model { get; } = new M_Main();

        public MainWindow()
        {
            Assembly asmbly = Assembly.GetExecutingAssembly();

            InitializeComponent();

            this.Title = $"Unpack My Game - {asmbly.GetName().Version}";
            DataContext = Model;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Check de LaunchBox
            if (string.IsNullOrEmpty(Config.LaunchBoxPath) ||
                !File.Exists(Path.Combine(Config.LaunchBoxPath, Config.PlatformsFile)))
            {
                DxTBoxCore.Box_MBox.DxMBox.ShowDial("Wrong LaunchBox path", "Warning");
            }
            else
            {
                _LaunchBoxOk = true;
            }
        }


        private void CanRun(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _LaunchBoxOk;
        }

        /// <summary>
        /// Fonctions relatives au Depacking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exec_Depack(object sender, ExecutedRoutedEventArgs e)
        {
            Model.Depack();

        }

        private void Exec_InjectG(object sender, ExecutedRoutedEventArgs e)
        {
            Model.InjectGame();
        }

        /// <summary>
        /// Créateur de DPGame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DPGMaker_Click(object sender, RoutedEventArgs e)
        {
            Model.DpgMaker();
        }

        /// <summary>
        /// Injection d'une plateforme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exec_InjectPlatform(object sender, ExecutedRoutedEventArgs e)
        {
            LBFunc.InjectPlatform();
        }

        /// <summary>
        /// Configuration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Config_Click(object sender, RoutedEventArgs e)
        {
            if (Model.Config())
            {
                _LaunchBoxOk = true;
            }

        }

        #region explorer
        private void LaunchBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", Config.LaunchBoxPath);
        }

        private void WorkingPath_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", Config.WorkingFolder);
        }
        #endregion
    }
}
