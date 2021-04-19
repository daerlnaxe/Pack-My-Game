﻿using System;
using System.Collections.Generic;
using System.Linq;
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

namespace UnPack_My_Game.Graph
{
    /// <summary>
    /// Logique d'interaction pour P_Selecter.xaml
    /// </summary>
    public partial class P_Selecter : Page
    {
        public static readonly RoutedUICommand AddCmd = new RoutedUICommand(Lang.Add, "AddCmd", typeof(P_Selecter));
        public static readonly RoutedUICommand RemoveCmd = new RoutedUICommand(Lang.Remove, "RemoveCmd", typeof(P_Selecter));
        public static readonly RoutedUICommand ProcessCmd = new RoutedUICommand(Lang.Process, "ProcessCmd", typeof(P_Selecter));

        public I_Select Model { get; set; }

        public P_Selecter()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
        }

        private void Can_Add(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model != null;
        }

        private void Exec_Add(object sender, ExecutedRoutedEventArgs e)
        {
            Model.Add();
        }

        private void Can_Remove(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model != null && Model.Elements.Any();
        }

        private void Exec_Remove(object sender, ExecutedRoutedEventArgs e)
        {
            Model.RemoveElements((IList<object>)e.Parameter);
        }

        private void Remove_Element(object sender, RoutedEventArgs e)
        {
            Model.RemoveElement(((Button)sender).Tag);
        }



        #region process
        private void Can_Process(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Model != null)
                e.CanExecute = !Model.HasErrors;
        }

        private void Exec_Process(object sender, ExecutedRoutedEventArgs e)
        {
            Model.Process();
        }
        #endregion
    }
}
