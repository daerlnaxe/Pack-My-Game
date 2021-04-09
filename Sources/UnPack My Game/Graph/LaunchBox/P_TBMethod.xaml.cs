using DxTBoxCore.BoxChoose;
using System;
using System.Collections.Generic;
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
using UnPack_My_Game.Resources;

namespace UnPack_My_Game.Graph.LaunchBox
{
    /// <summary>
    /// Logique d'interaction pour P_TBMethode.xaml
    /// </summary>
    public partial class P_TBMethod : Page, I_Method
    {
        public A_Method Model { get; set; } = new M_TBMethod();

        public P_TBMethod()
        {
            InitializeComponent();
            DataContext = Model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TreeChoose tc = new TreeChoose()
            {
                Model = new M_ChooseRaw()
                {
                    Info = Lang.S_Package,
                    StartingFolder = Properties.Settings.Default.LastSpath,
                    FilesExtension = new[] { "xml" },
                    Mode = ChooseMode.File,
                    ShowFiles = true,
                }
            };

            if (tc.ShowDialog() != true)
                return;

            ((M_TBMethod)Model).AssignXML(tc.LinkResult);
        }
    }
}
