using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UnPack_My_Game.Cores;
using UnPack_My_Game.Cores.LaunchBox;
using UnPack_My_Game.Models.LaunchBox;
using UnPack_My_Game.Models.Submenus;
using static UnPack_My_Game.Properties.Settings;
using static UnPack_My_Game.Common;

namespace UnPack_My_Game.Graph.LaunchBox
{
    /// <summary>
    /// Logique d'interaction pour LaunchBox_Main.xaml
    /// </summary>
    public partial class W_LaunchBox_Main : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _LaunchBoxOk;

        public static readonly RoutedCommand DepackCmd = new RoutedCommand();
        public static readonly RoutedCommand InjectGCmd = new RoutedCommand();
        public static readonly RoutedCommand InjectPCmd = new RoutedCommand();


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



        public W_LaunchBox_Main()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Check de LaunchBox
            if (File.Exists(Path.Combine(Config.LaunchBoxPath, Default.fPlatforms)))
            {
                _LaunchBoxOk = true;
            }
            else
            {
                DxTBoxCore.Box_MBox.DxMBox.ShowDial("Wrong LaunchBox path", "Warning");
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
            /*   ActivePage = new P_Selecter()
               {
                   Model = new M_LBcDPGUnpack(),
               };*/

            ActivePage = new P_SubMenu()
            {
                Driver = new DPGCoreSub(),
            };
        }

        private void Exec_InjectG(object sender, ExecutedRoutedEventArgs e)
        {
            /*    ActivePage = new P_Selecter()
                {
                    Model = new M_LBCDPGFolder(),
                };*/
            ActivePage = new P_SubMenu()
            {
                Driver = new LaunchBoxInject(),
            };
        }

        /// <summary>
        /// Créateur de DPGame
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DPGMaker_Click(object sender, RoutedEventArgs e)
        {
            ActivePage = new P_SubMenu()
            {
                Driver = new DPGMakerSub(),
            };
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
            if (new W_Config().ShowDialog() == true)
            {
                _LaunchBoxOk = true;
            }

        }






    }
}
