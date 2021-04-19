using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UnPack_My_Game.Graph.LaunchBox
{
    /// <summary>
    /// Logique d'interaction pour LaunchBox_Main.xaml
    /// </summary>
    public partial class P_LaunchBox_Main : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Page _ActivePage;
        public Page ActivePage
        {
            get => _ActivePage;
            set
            {
                _ActivePage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActivePage)));
            }
        }
        /*
        public List<ICommand> Buttons
        {
            get;
            set;
        } = new List<ICommand>() { MeeCmd };

        public static readonly RoutedCommand MeeCmd = new RoutedCommand("Mee", typeof(P_LaunchBox_Main));
        */


        public P_LaunchBox_Main()
        {
            /*Buttons.Add("Atchoum", "") ;
            Buttons.Add("Simplet", "") ;*/
            //this.CommandBindings.Add(new CommandBinding(MeeCmd, Mee_Exec, Mee_CanExec));
            InitializeComponent();
            DataContext = this;
        }

        private void Mee_CanExec(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }



        private void Depack_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DPGMaker_Click(object sender, RoutedEventArgs e)
        {
            ActivePage = new P_Selecter();
            return;
        }



        private void InjectG_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InjectPlatform_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Config_Click(object sender, RoutedEventArgs e)
        {
            if (new W_Config().ShowDialog() == true)
            {

            }
            // var platform = _Model.SelectPlatform;
            /*
            I_Source p = _Model.ActiveSrcPage;


            _Model = new M_LaunchBox();
            _Model.ActiveSrcPage = p;
            //_Model.SelectPlatform = platform;

            DataContext = _Model;*/
        }

    }
}
