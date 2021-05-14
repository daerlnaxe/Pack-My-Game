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
using static Pack_My_Game.Common;


namespace Pack_My_Game.IHM
{
    /// <summary>
    /// Logique d'interaction pour W_Config.xaml
    /// </summary>
    public partial class W_Config : Window
    {
        private M_Config _Model;
        //public static bool? Result { get; set; }



        public W_Config()
        {
            //  Result = null;
            //   _SelectedLanguage = PS.Default.Language;
            InitializeComponent();
            DataContext = _Model = new M_Config();
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
            //_Model.Relocalize();

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
            _Model.ChooseLaunchBoxPath();

        }

        /// <summary>
        /// Choose the working path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseWP_Click(object sender, RoutedEventArgs e)
        {
            _Model.ChooseWorkingPath();        
        }

        private void ChooseCheatsPath_Click(object sender, RoutedEventArgs e)
        {
            _Model.ChooseCheatsPath();
        }

        private void Exec_Submit(object sender, ExecutedRoutedEventArgs e)
        {
            _Model.Save();

            DialogResult = true;

            this.Close();
        }

    }
}
