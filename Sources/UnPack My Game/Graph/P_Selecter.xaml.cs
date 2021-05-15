using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UnPack_My_Game.Language;
using UnPack_My_Game.Models;

namespace UnPack_My_Game.Graph
{
    /// <summary>
    /// Logique d'interaction pour P_Selecter.xaml
    /// </summary>
    public partial class P_Selecter : Page
    {
        public static readonly RoutedUICommand AddCmd = new RoutedUICommand(LanguageManager.Instance.Lang.Word_Add, "AddCmd", typeof(P_Selecter));
        public static readonly RoutedUICommand RemoveCmd = new RoutedUICommand(LanguageManager.Instance.Lang.Word_Remove, "RemoveCmd", typeof(P_Selecter));
        public static readonly RoutedUICommand ProcessCmd = new RoutedUICommand(LanguageManager.Instance.Lang.Word_Process, "ProcessCmd", typeof(P_Selecter));

        public I_Select Model { get; set; }

        public P_Selecter()
        {
            InitializeComponent();
            DataContext = null;
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
