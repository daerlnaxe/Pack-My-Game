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

namespace UnPack_My_Game.Graph
{
    /// <summary>
    /// Logique d'interaction pour W_PackMeRes.xaml
    /// </summary>
    public partial class W_DPGMaker : Window
    {
        public static readonly RoutedUICommand DeleteCmd = new RoutedUICommand("Delete", "DeleteCmd", typeof(W_DPGMaker));

        public static readonly RoutedUICommand AddGameCmd = new RoutedUICommand("Add Game", "AddGameCmd", typeof(W_DPGMaker));
        public static readonly RoutedUICommand RemoveGameCmd = new RoutedUICommand("Remove Game", "RemoveGameCmd", typeof(W_DPGMaker));

        public static readonly RoutedUICommand AddCheatCmd = new RoutedUICommand("Add Cheat", "AddCheatCmd", typeof(W_DPGMaker));
        public static readonly RoutedUICommand NewCheatCmd = new RoutedUICommand("New Cheat", "NewCheatCmd", typeof(W_DPGMaker));
        public static readonly RoutedUICommand OpenCheatCmd = new RoutedUICommand("Open", "OpenCheatCmd", typeof(W_DPGMaker));
        public static readonly RoutedUICommand RemoveCheatCmd = new RoutedUICommand("Delete", "RemoveCheatCmd", typeof(W_DPGMaker));

        public static readonly RoutedUICommand AddManualCmd = new RoutedUICommand("Add Manual", "AddManualCmd", typeof(W_DPGMaker));
        public static readonly RoutedUICommand OpenManualCmd = new RoutedUICommand("Open", "OpenManualCmd", typeof(W_DPGMaker));
        public static readonly RoutedUICommand RemoveManualCmd = new RoutedUICommand("Delete", "RemoveManualCmd", typeof(W_DPGMaker));

        public static readonly RoutedUICommand AddMusicCmd = new RoutedUICommand("Add Music", "AddMusicCmd", typeof(W_DPGMaker));
        public static readonly RoutedUICommand OpenMusicCmd = new RoutedUICommand("Open", "OpenMusicCmd", typeof(W_DPGMaker));
        public static readonly RoutedUICommand RemoveMusicCmd = new RoutedUICommand("Delete", "RemoveMusicCmd", typeof(W_DPGMaker));

        public static readonly RoutedUICommand AddVideoCmd = new RoutedUICommand("Add Video", "AddVideoCmd", typeof(W_DPGMaker));
        public static readonly RoutedUICommand OpenVideoCmd = new RoutedUICommand("Open", "OpenVideoCmd", typeof(W_DPGMaker));
        public static readonly RoutedUICommand RemoveVideoCmd = new RoutedUICommand("Delete", "RemoveVideoCmd", typeof(W_DPGMaker));

        public static readonly RoutedUICommand SetVideoCmd = new RoutedUICommand("Set Video", "SetVideoCmd", typeof(W_DPGMaker));
        public static readonly RoutedUICommand UnsetVideoCmd = new RoutedUICommand("Unset Video", "UnsetVideoCmd", typeof(W_DPGMaker));
        public static readonly RoutedUICommand SetThemeVideoCmd = new RoutedUICommand("Set ThemeVideo", "SetThemeVideoCmd", typeof(W_DPGMaker));
        public static readonly RoutedUICommand UnsetThemeVideoCmd = new RoutedUICommand("Unset ThemeVideo", "UnsetThemeVideoCmd", typeof(W_DPGMaker));



        public M_DPGMaker Model { get; set; }

        public W_DPGMaker()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;

        }


        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            Model.LoadFolder();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Model.Init();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (Model.Apply_Modifs())
            {

                DialogResult = true;
                this.Close();
            }

        }


        private void Game_Handler(object sender, RoutedEventArgs e)
        {
            RadioButton cb = (RadioButton)sender;
            Model.Game_Handler(cb.Tag);
        }

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

        // ---


        private void Can_Video(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.SelectedVideo != null;
        }


        private void Can_UnsetVideo(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.SelectedVideo != null & Model.SelectedVideo == Model.ChosenVideo;
        }

        private void Can_UnsetThemeVideo(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.SelectedVideo != null & Model.SelectedVideo == Model.ChosenThemeVideo;
        }

        private void Exec_HandleVideo(object sender, ExecutedRoutedEventArgs e)
        {
            Model.Video_Handler(Model.SelectedVideo, bool.Parse(e.Parameter.ToString()));

        }

        private void Exec_HandleThemeVideo(object sender, ExecutedRoutedEventArgs e)
        {
            Model.ThemeVideo_Handler(Model.SelectedVideo, bool.Parse(e.Parameter.ToString()));
        }

        #region Additionnals
        private void Can_Advanced(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.Advanced;
        }

        // --- Games

        private void Exec_AddGame(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Can_Game(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.Advanced && Model.SelectedGame != null;
        }

        private void Exec_RemoveGame(object sender, ExecutedRoutedEventArgs e)
        {

        }

        // --- Manuals

        private void Exec_AddManual(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Can_Manual(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.Advanced && Model.SelectedManual != null;
        }

        private void Exec_OpenManual(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Exec_RemoveManual(object sender, ExecutedRoutedEventArgs e)
        {

        }

        // --- Musics

        private void Exec_AddMusic(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Can_Music(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.Advanced && Model.SelectedMusic != null;

        }

        private void Exec_OpenMusic(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Exec_RemoveMusic(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Exec_AddVideo(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Can_AdvVideo(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.Advanced && Model.SelectedVideo != null;
        }

        private void Exec_OpenVideo(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Exec_RemoveVideo(object sender, ExecutedRoutedEventArgs e)
        {

        }

        // --- CheatCodes

        private void Exec_AddCheat(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Can_Cheat(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.Advanced && Model.SelectedCheatFile != null;

        }

        private void Exec_NewCheat(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Exec_OpenCheat(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Exec_RemoveCheat(object sender, ExecutedRoutedEventArgs e)
        {

        }

        #endregion


    }
}
