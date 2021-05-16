using Pack_My_Game.Language;
using Pack_My_Game.Models;
using System;
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
using System.Windows.Shapes;

namespace Pack_My_Game.IHM
{
    /// <summary>
    /// Logique d'interaction pour W_PackMeRes.xaml
    /// </summary>
    public partial class W_PackMePrev : Window
    {
        public M_PackMePrev Model;

        public W_PackMePrev()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;

        }

        #region Manuels
        private void Can_Manual(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.ManualsCollection.Any();
        }

        private void Add_Manual(object sender, RoutedEventArgs e)
        {
            Model.AddManual();
        }

        private void Exec_OpenManual(object sender, ExecutedRoutedEventArgs e)
        {
            Model.OpenManual();
        }

        private void Exec_RemoveManual(object sender, ExecutedRoutedEventArgs e)
        {
            Model.RemoveManual();
        }

        #endregion

        #region Musique
        private void Can_Music(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.MusicsCollection.Any();

        }
        private void Add_Music(object sender, RoutedEventArgs e)
        {
            Model.AddMusic();
        }

        private void Exec_OpenMusic(object sender, ExecutedRoutedEventArgs e)
        {
            Model.OpenMusic();
        }

        private void Exec_RemoveMusic(object sender, ExecutedRoutedEventArgs e)
        {
            Model.RemoveMusic();
        }
        #endregion

        #region Video
        private void Can_Video(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.VideosCollection.Any();
        }

        private void Add_Video(object sender, RoutedEventArgs e)
        {
            Model.AddVideo();
        }

        private void Exec_OpenVideo(object sender, ExecutedRoutedEventArgs e)
        {
            Model.OpenVideo();
        }

        private void Exec_RemoveVideo(object sender, ExecutedRoutedEventArgs e)
        {
            Model.RemoveVideo();
        }
        #endregion

        #region Cheats
        private void Can_Cheat(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.CheatsCollection.Any();
        }

        private void Add_Cheat(object sender, RoutedEventArgs e)
        {
            Model.AddCheat();
        }

        private void Exec_OpenCheat(object sender, ExecutedRoutedEventArgs e)
        {
            Model.OpenCheatInDefaultEditor();
        }

        private void Exec_RemoveCheat(object sender, ExecutedRoutedEventArgs e)
        {
            Model.RemoveCheat();
        }
        #endregion


        private void Reset(object sender, RoutedEventArgs e)
        {
            Model.Init();
        }


        #region Recherche
        private void Recherche_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && (Keyboard.IsKeyDown(Key.LeftCtrl) ||  Keyboard.IsKeyDown(Key.RightCtrl) ))
            {
                Model.SearchString = string.Empty;
                e.Handled = true;
            }

            if (e.Key == Key.Enter)
            {
                Model.FillWords(Model.SearchString);
            }
        }

        private void Remove_Word(object sender, RoutedEventArgs e)
        {
            Model.RemoveWord(((Button)sender).Tag);
        }


        private void Can_Search(object sender, CanExecuteRoutedEventArgs e)
        {
            if (Model != null)
                e.CanExecute = Model.WordsToSearch.Any();
        }

        private void Exec_ResetWords(object sender, ExecutedRoutedEventArgs e)
        {
            Model.ResetSearch();
        }

        private void Exec_Search(object sender, ExecutedRoutedEventArgs e)
        {
            Model.SearchFiles();
        }

        #endregion

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (Model.Apply_Modifs())
            {
                DialogResult = true;
                this.Close();
            }

        }


    }
}
