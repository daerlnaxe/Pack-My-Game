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
using UnPack_My_Game.Models.LaunchBox;
using UnPack_My_Game.Resources;

namespace UnPack_My_Game.Graph.LaunchBox
{
    /// <summary>
    /// Logique d'interaction pour LaunchBox_Start.xaml
    /// </summary>
    public partial class LaunchBox_Start : Page, INotifyPropertyChanged//, I_Source
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private M_LaunchBox _Model = new M_LaunchBox();

        public static readonly RoutedUICommand Process = new RoutedUICommand("Process", "btProcess", typeof(LaunchBox_Start));
           


        public LaunchBox_Start()
        {
            InitializeComponent();
            DataContext = _Model;
        }

        private void OneG_Click(object sender, RoutedEventArgs e)
        {
            _Model.ActiveSrcPage = new P_OGame();            
        }

        private void SevG_Click(object sender, RoutedEventArgs e)
        {
            _Model.ActiveSrcPage = new P_SGames();
        }

        private void Process_CanExec(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !_Model.HasErrors &&
                                _Model.ActiveSrcPage != null && !_Model.ActiveSrcPage.Model.HasErrors &&
                                _Model.ActiveMethodPage != null && !_Model.ActiveMethodPage.Model.HasErrors;

            
        }

        /// <summary>
        /// Processus enclenché
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Process_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _Model.Process();
        }



        private void Config_Click(object sender, RoutedEventArgs e)
        {
            new W_Config().ShowDialog();
           // var platform = _Model.SelectPlatform;
            
            I_Source p = _Model.ActiveSrcPage;


            _Model = new M_LaunchBox();
            _Model.ActiveSrcPage = p;
            //_Model.SelectPlatform = platform;

            DataContext = _Model;
        }

        #region Méthodes
        private void LB_Click(object sender, RoutedEventArgs e)
        {
            _Model.ActiveMethodPage = new P_LBMethod();
            _Model.Mode = E_Method.LBMethod;
        }

        private void DPG_Click(object sender, RoutedEventArgs e)
        {
            //_Model.ActiveMethodPage = new P_DPGMethod();
            _Model.Mode = E_Method.TBMethod;
        }

        private void EB_Click(object sender, RoutedEventArgs e)
        {
            _Model.ActiveMethodPage = new P_EBMethod();
            _Model.Mode = E_Method.EBMethod;
        }
        #endregion


    }
}
