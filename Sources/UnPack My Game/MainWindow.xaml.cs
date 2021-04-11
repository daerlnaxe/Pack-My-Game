using DxTBoxCore.MBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnPack_My_Game.Graph;
using UnPack_My_Game.Graph.LaunchBox;
using UnPack_My_Game.Models;
using UnPack_My_Game.Resources;

namespace UnPack_My_Game
{
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        /// Title Showed on Main Window
        /// </summary>
        public string Title
        {
            get
            {
                System.Reflection.Assembly asmbly = System.Reflection.Assembly.GetExecutingAssembly();
                return $"{asmbly.GetName().Name} - {asmbly.GetName().Version}";
            }
        }



        /// <summary>

        public MainWindow()
        {
            try
            {
                if (!System.IO.Directory.Exists(Common.BackUp))
                    System.IO.Directory.CreateDirectory(Common.BackUp);

                // Logs folder
                System.IO.Directory.CreateDirectory(Common.Logs);
            }
            catch(System.IO.IOException exc)
            {
                DxMBox.ShowDial(Lang.Err_FolderC, "Error", DxTBoxCore.Common.E_DxButtons.Ok, exc.Message);
                return;
            }


                InitializeComponent();
            DataContext = this;
        }


        private void LaunchBox_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            new W_Common()
            {
                ActivePage = new LaunchBox_Start()
            }.ShowDialog();

            this.Show();
        }

        private void LaunchBoxRevo_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            new W_Common()
            {
                ActivePage = new P_LaunchBox_Main()

            }.ShowDialog();

            this.Show();
        }
    }
}
