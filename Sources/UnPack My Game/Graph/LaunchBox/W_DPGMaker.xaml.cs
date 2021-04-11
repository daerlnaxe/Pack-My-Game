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

namespace UnPack_My_Game.Graph.LaunchBox
{
    /// <summary>
    /// Logique d'interaction pour W_PackMeRes.xaml
    /// </summary>
    public partial class W_DPGMaker : Window
    {
        public static readonly RoutedUICommand DeleteCmd = new RoutedUICommand("Delete", "DeleteCmd", typeof(W_DPGMaker));

        public static readonly RoutedUICommand OpenCheatCmd = new RoutedUICommand("Open", "OpenCheatCmd", typeof(W_DPGMaker));
        public static readonly RoutedUICommand RemoveCheatCmd = new RoutedUICommand("Delete", "RemoveCheatCmd", typeof(W_DPGMaker));

        public static readonly RoutedUICommand OpenManualCmd = new RoutedUICommand("Open", "OpenManualCmd", typeof(W_DPGMaker));
        public static readonly RoutedUICommand RemoveManualCmd = new RoutedUICommand("Delete", "RemoveManualCmd", typeof(W_DPGMaker));

        public static readonly RoutedUICommand OpenMusicCmd = new RoutedUICommand("Open", "OpenMusicCmd", typeof(W_DPGMaker));
        public static readonly RoutedUICommand RemoveMusicCmd = new RoutedUICommand("Delete", "RemoveMusicCmd", typeof(W_DPGMaker));

        public static readonly RoutedUICommand OpenVideoCmd = new RoutedUICommand("Open", "OpenVideoCmd", typeof(W_DPGMaker));
        public static readonly RoutedUICommand RemoveVideoCmd = new RoutedUICommand("Delete", "RemoveVideoCmd", typeof(W_DPGMaker));

        public M_DPGMaker Model;

        public W_DPGMaker()
        {


            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        #region
        private void Add_GameF(object sender, RoutedEventArgs e)
        {
            Model.CopyGameF();
        }

        private void Remove_GameF(object sender, RoutedEventArgs e)
        {
            Model.RemoveGameF();
        }
        #endregion


        #region
        private void Can_Manual(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.ManualsCollection.Any();
        }

        private void Add_ManualF(object sender, RoutedEventArgs e)
        {
            Model.CopyManualF();
        }

        private void Exec_OpenManual(object sender, ExecutedRoutedEventArgs e)
        {
            Model.OpenManual();
        }

        private void Exec_RemoveManual(object sender, ExecutedRoutedEventArgs e)
        {
            Model.RemoveManualF();
        }

        #endregion

        #region Musique
        private void Can_Music(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.MusicsCollection.Any();

        }
        private void Add_MusicF(object sender, RoutedEventArgs e)
        {
            Model.CopyMusicF();
        }

        private void Exec_OpenMusic(object sender, ExecutedRoutedEventArgs e)
        {
            Model.OpenMusic();
        }

        private void Exec_RemoveMusic(object sender, ExecutedRoutedEventArgs e)
        {
            Model.RemoveMusicF();
        }
        #endregion

        #region Video
        private void Can_Video(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.VideosCollection.Any();
        }

        private void Add_VideoF(object sender, RoutedEventArgs e)
        {
            Model.CopyVideoF();
        }

        private void Exec_OpenVideo(object sender, ExecutedRoutedEventArgs e)
        {
            Model.OpenVideo();
        }

        private void Exec_RemoveVideo(object sender, ExecutedRoutedEventArgs e)
        {
            Model.RemoveVideoF();
        }
        #endregion

        #region Cheats
        private void Can_Cheat(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.CheatsCollection.Any();
        }

        private void Add_CheatF(object sender, RoutedEventArgs e)
        {
            Model.CopyAstuceF();
        }


        private void Exec_OpenCheat(object sender, ExecutedRoutedEventArgs e)
        {
            Model.OpenCheat();
        }

        private void Exec_RemoveCheat(object sender, ExecutedRoutedEventArgs e)
        {
            Model.RemoveCheatF();
        }
        #endregion


        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            Model.LoadFiles();
        }

        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            Model.LoadFolder();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();

        }

        private void JVCom_Click(object sender, RoutedEventArgs e)
        {
            Model.LaunchPage("https://www.jeuxvideo.com/rechercher.php?q=");
            
        }
    }
}
