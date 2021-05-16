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
    public partial class W_PackMeRes : Window
    {

        public static readonly RoutedUICommand DeleteCmd = new RoutedUICommand("Delete", "DeleteCmd", typeof(W_PackMeRes));

        public static readonly RoutedUICommand OpenCheatCmd = new RoutedUICommand("Open", "OpenCheatCmd", typeof(W_PackMeRes));
        public static readonly RoutedUICommand RemoveCheatCmd = new RoutedUICommand("Delete", "RemoveCheatCmd", typeof(W_PackMeRes));

        public static readonly RoutedUICommand OpenManualCmd = new RoutedUICommand("Open", "OpenManualCmd", typeof(W_PackMeRes));
        public static readonly RoutedUICommand RemoveManualCmd = new RoutedUICommand("Delete", "RemoveManualCmd", typeof(W_PackMeRes));

        public static readonly RoutedUICommand OpenMusicCmd = new RoutedUICommand("Open", "OpenMusicCmd", typeof(W_PackMeRes));
        public static readonly RoutedUICommand RemoveMusicCmd = new RoutedUICommand("Delete", "RemoveMusicCmd", typeof(W_PackMeRes));

        public static readonly RoutedUICommand OpenVideoCmd = new RoutedUICommand("Open", "OpenVideoCmd", typeof(W_PackMeRes));
        public static readonly RoutedUICommand RemoveVideoCmd = new RoutedUICommand("Delete", "RemoveVideoCmd", typeof(W_PackMeRes));

        public static readonly RoutedUICommand SetVideoCmd = new RoutedUICommand("Set Video", "SetVideoCmd", typeof(W_PackMeRes));
        public static readonly RoutedUICommand UnsetVideoCmd = new RoutedUICommand("Unset Video", "UnsetVideoCmd", typeof(W_PackMeRes));
        public static readonly RoutedUICommand SetThemeVideoCmd = new RoutedUICommand("Set ThemeVideo", "SetThemeVideoCmd", typeof(W_PackMeRes));
        public static readonly RoutedUICommand UnsetThemeVideoCmd = new RoutedUICommand("Unset ThemeVideo", "UnsetThemeVideoCmd", typeof(W_PackMeRes));



        public M_PackMeRes Model;
        public W_PackMeRes()
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
            //Model.CopyGameF();
        }

        private void Remove_GameF(object sender, RoutedEventArgs e)
        {
            //Model.RemoveGameF();
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

        private void New_CheatF(object sender, RoutedEventArgs e)
        {
            Model.NewCheatF();
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

        private void Reset(object sender, RoutedEventArgs e)
        {
            Model.Init();
        }
        private void Reload_Click(object sender, RoutedEventArgs e)
        {
            Model.LoadFiles();
        }

        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            Model.LoadFolder();
        }



        private void JVCom_Click(object sender, RoutedEventArgs e)
        {
            Model.LaunchPage("https://www.jeuxvideo.com/rechercher.php?q=");

        }

        #region Check / Uncheck
        /*
        private void Game_Handler(object sender, RoutedEventArgs e)
        {
            RadioButton cb = (RadioButton)sender;
            Model.Game_Handler(cb.Tag, cb.IsChecked);
        }*/

        private void Manual_Handler(object sender, RoutedEventArgs e)
        {
            RadioButton cb = (RadioButton)sender;
            Model.Manual_Handler(cb.Tag, cb.IsChecked);
        }


        private void Music_Handler(object sender, RoutedEventArgs e)
        {
            RadioButton cb = (RadioButton)sender;
            Model.Music_Handler(cb.Tag, cb.IsChecked);
        }

        private void Can_UnsetVideo(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.SelectedVideo != null & Model.SelectedVideo == Model.ChosenVideo;
        }

        private void Exec_HandleVideo(object sender, ExecutedRoutedEventArgs e)
        {
            Model.Video_Handler(Model.SelectedVideo, bool.Parse(e.Parameter.ToString()));
        }


        private void Can_UnsetThemeVideo(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.SelectedVideo != null & Model.SelectedVideo == Model.ChosenThemeVideo;
        }
        private void Exec_HandleThemeVideo(object sender, ExecutedRoutedEventArgs e)
        {
            Model.ThemeVideo_Handler(Model.SelectedVideo, bool.Parse(e.Parameter.ToString()));
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (Model.Apply_Modifs())
            {
                DialogResult = true;
                this.Close();
            }

        }
    }
    #endregion Check / Uncheck
}
