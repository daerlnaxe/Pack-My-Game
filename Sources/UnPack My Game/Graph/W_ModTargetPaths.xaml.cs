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
using System.Windows.Shapes;
using UnPack_My_Game.Models;
using UnPack_My_Game.Resources;

namespace UnPack_My_Game.Graph
{
    /// <summary>
    /// Logique d'interaction pour W_ModTargetPathsaml.xaml
    /// </summary>
    public partial class W_ModTargetPaths : Window
    {
        public M_ModTargetPaths Model { get; set; }

        public readonly static RoutedUICommand ValidateCommand = new RoutedUICommand(Lang.Validate, "ValidateCommand", typeof(W_ModTargetPaths));

        public W_ModTargetPaths()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
            Model.InitializeDatas();
        }

        private void Validate_CanEx(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Model != null)
                e.CanExecute = !Model.HasErrors;
        }

        private void Validate_Exec(object sender, ExecutedRoutedEventArgs e)
        {
            Model.AssignPaths();
            this.DialogResult = true;
            this.Close();
        }
    }
}
