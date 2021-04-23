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
using UnPack_My_Game.Models.LaunchBox;

namespace UnPack_My_Game.Graph.LaunchBox
{
    /// <summary>
    /// Logique d'interaction pour P_LaunchBox.xaml
    /// </summary>
    public partial class P_LaunchBox : Page, INotifyPropertyChanged//, I_Source
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private M_LaunchBoxRevo _Model = new M_LaunchBoxRevo();

        public static readonly RoutedUICommand Process = new RoutedUICommand("Process", "btProcess", typeof(LaunchBox_Start));


        public P_LaunchBox()
        {
            InitializeComponent();
            DataContext = _Model;
        }


        private void Process_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _Model.Process();
        }

        private void OneG_Click(object sender, RoutedEventArgs e)
        {
            //_Model.ActiveSrcPage = new P_OGame();
        }

        private void SevG_Click(object sender, RoutedEventArgs e)
        {
            //_Model.ActiveSrcPage = new P_SGames();
        }

        private void Process_CanExec(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !_Model.HasErrors &&
                                _Model.ActiveSrcPage != null && !_Model.ActiveSrcPage.Model.HasErrors;
                                


        }
        private void Config_Click(object sender, RoutedEventArgs e)
        {
            new W_Config().ShowDialog();
            // var platform = _Model.SelectPlatform;

            I_Source p = _Model.ActiveSrcPage;


            _Model = new M_LaunchBoxRevo();
            _Model.ActiveSrcPage = p;
            //_Model.SelectPlatform = platform;

            DataContext = _Model;
        }
    }
}
