using DxTBoxCore.BoxChoose;
using Pack_My_Game.Models;
using Pack_My_Game.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PS = Pack_My_Game.Properties.Settings;


namespace Pack_My_Game.IHM
{
    /// <summary>
    /// Logique d'interaction pour W_Config.xaml
    /// </summary>
    public partial class W_Config : Window
    {
        private M_Config _Model = new M_Config();
        //public static bool? Result { get; set; }



        public W_Config()
        {
          //  Result = null;
         //   _SelectedLanguage = PS.Default.Language;
            InitializeComponent();
            DataContext = _Model;
        }


        /* Fonctionne avec fichier resource
        public void ReloadUI()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(SelectedLanguage);
            W_Config window = new W_Config();
            window.SelectedLanguage = SelectedLanguage;
            //Application.Current.MainWindow = window;
            //DialogResult = window.DialogResult;
            this.Hide();
            window.ShowDialog();
            //this.Show();

            this.Close();
        }*/

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _Model.Relocalize();

            /*      if (string.IsNullOrEmpty(_Model.SelectedLanguage))
                      return;


                  //InitializeComponent();

                  /*switch( _Model.SelectedLanguage)
                  {
                      case "English":
                          break;

                      case "Français":
                          break;
                  }*/
        }


        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            //ReloadUI();
        }

        /// <summary>
        /// Choose the LaunchBoxPath
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseLBPath_Click(object sender, RoutedEventArgs e)
        {
            TreeChoose tc = new TreeChoose()
            {
                Model = new M_ChooseFolder()
                {
                    HideWindowsFolder = true,
                    Info = _Model.Lang.Ch_LBPath,
                    ShowFiles = true,
                    StartingFolder = PS.Default.LastKPath,
                    
                },
                
            };

            if (tc.ShowDialog() == true)
            {
                PS.Default.LastKPath = tc.LinkResult;
                PS.Default.Save();
                _Model.LaunchBoxPath = tc.LinkResult;
            }


        }

        /// <summary>
        /// Choose the working path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseWP_Click(object sender, RoutedEventArgs e)
        {
            TreeChoose tc = new TreeChoose()
            {
                Model = new M_ChooseFolder()
                {
                    HideWindowsFolder = true,
                    Info = _Model.Lang.Ch_LBPath,
                    ShowFiles = true,
                    StartingFolder = PS.Default.LastKPath,

                },

            };

            if (tc.ShowDialog() == true)
            {
                PS.Default.LastKPath = tc.LinkResult;
                PS.Default.Save();
                _Model.WorkingPath = tc.LinkResult;
            }
        }
        private void ChooseCheatsPath_Click(object sender, RoutedEventArgs e)
        {
            TreeChoose tc = new TreeChoose()
            {
                Model = new M_ChooseFolder()
                {
                    HideWindowsFolder = true,
                    Info = _Model.Lang.Ch_LBPath,
                    ShowFiles = true,
                    StartingFolder = PS.Default.LastKPath,

                },

            };

            if (tc.ShowDialog() == true)
            {
                PS.Default.LastKPath = tc.LinkResult;
                PS.Default.Save();
                _Model.CheatCodesPath = tc.LinkResult;
            }
        }

        private void Exec_Submit(object sender, ExecutedRoutedEventArgs e)
        {
            //PS.Default.Language = SelectedLanguage;
            Common.ObjectLang = _Model.Lang;
            PS.Default.Language = _Model.SelectedLanguage.Lang;
            // --- Chemins
            PS.Default.LBPath = _Model.LaunchBoxPath;
            PS.Default.OutPPath = _Model.WorkingPath;
            PS.Default.CCodesPath = _Model.CheatCodesPath;
            // --- Compression
            PS.Default.cZipCompLvl = _Model.ZipLvlCompression;
            PS.Default.c7zCompLvl = _Model.SevZipLvlCompression;
            // Options 
            PS.Default.opInfos = _Model.InfosGame;
            PS.Default.opOBGame = _Model.OriginalXMLBackup;
            PS.Default.opEBGame = _Model.EnhancedXMLBackup;
            PS.Default.opTreeV = _Model.TreeviewFile;
            PS.Default.opClones = _Model.ClonesCopy;
            PS.Default.opCheatCodes = _Model.CheatsCopy;
            PS.Default.opMd5 = _Model.MD5Calcul;
            PS.Default.opZip = _Model.ZipCompression;
            PS.Default.op7_Zip = _Model.SevCompression;
            PS.Default.opLogFile = _Model.FileLog;
            PS.Default.opLogWindow = _Model.WindowLog;


            PS.Default.Save();

            DialogResult = true;

            this.Close();
        }

    }
}
