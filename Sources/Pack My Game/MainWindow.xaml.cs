using Common_PMG.Container.Game;
using Common_PMG.XML;
using DxTBoxCore.BoxChoose;
using Pack_My_Game.IHM;
using Pack_My_Game.Models;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
//using System.Windows.Shapes;
using static Pack_My_Game.Common;

namespace Pack_My_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly RoutedUICommand CheckGameValidityCmd = new RoutedUICommand("Test game links", nameof(CheckGameValidityCmd), typeof(MainWindow));
        public static readonly RoutedUICommand ExtractTBGameCmd = new RoutedUICommand("Extract True Backup", nameof(ExtractTBGameCmd), typeof(MainWindow));
        public static readonly RoutedUICommand ExtractNPGameCmd = new RoutedUICommand("Extract NoPath Backup", nameof(ExtractTBGameCmd), typeof(MainWindow));
        public static readonly RoutedUICommand ExtractPlatformCmd = new RoutedUICommand("Extract Platform", "ExtractPlatformCmd", typeof(MainWindow));
        public static readonly RoutedUICommand ExtractDefaultFilesCmd = new RoutedUICommand("Extract Default Files", "ExtractDefaultFilesCmd", typeof(MainWindow));
        public static readonly RoutedUICommand SelectAllCmd = new RoutedUICommand("Select All", "SelectAllCmd", typeof(MainWindow));
        public static readonly RoutedUICommand SelectNoneCmd = new RoutedUICommand("Select None", "SelectNoneCmd", typeof(MainWindow));

        private M_Main _Model = new M_Main();


        public string WindowTitle
        {
            get
            {
                Assembly asmbly = Assembly.GetExecutingAssembly();
                return $"Pack My Game - {asmbly.GetName().Version}";
            }
        }


        public MainWindow()
        {
             _Model = new M_Main();

            InitializeComponent();
            DataContext = _Model;
        }


        /// <summary>
        /// MenuItem Config
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Config_Click(object sender, RoutedEventArgs e)
        {
            _Model.ShowConfig();
        }

        #region explorer
        private void LaunchBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", _Model.LaunchBoxPath);
        }

        private void WorkingPath_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", _Model.WorkingFolder);
        }
        #endregion


        private void Refresh_Platforms(object sender, RoutedEventArgs e)
        {
            _Model.Refresh_Platforms();
        }

        /// <summary>
        /// Platform selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Platform_Selected(object sender, SelectionChangedEventArgs e)
        {
            _Model.PlatformChanged();
        }

        private void Game_Checked(object sender, RoutedEventArgs e)
        {
            ShortGame sgame = (ShortGame)((CheckBox)sender).Tag;
            _Model.AddGame(sgame);
        }

        private void Game_Unchecked(object sender, RoutedEventArgs e)
        {
            ShortGame sgame = (ShortGame)((CheckBox)sender).Tag;
            _Model.RemoveGame(sgame);
        }

        private void Game_WheelMouse(object sender, MouseWheelEventArgs e)
        {
            sv.ScrollToVerticalOffset(sv.VerticalOffset - e.Delta);
        }

        private void ListGame_DblClick(object sender, MouseButtonEventArgs e)
        {
            var game = (ShortGame)((ListViewItem)sender).Content;
            game.IsSelected = true;

        }

        private void Can_Process(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !_Model.HasErrors;
        }

        private void Exec_Process(object sender, ExecutedRoutedEventArgs e)
        {
            _Model.Process();
        }


        #region Windows from core



        #endregion

        private void button_Click(object sender, RoutedEventArgs e)
        {
            /*
            BoxProgress bp = new BoxProgress();
            bp.ShowDialog();*/
            W_Cheat window = new W_Cheat(@"G:\Temp\pmg - temp\Super Nintendo Entertainment System\Donkey Kong Country (E) (M3) (V1.0) [f1]\CheatCodes", "Donkey Kong Country", @"Donkey Kong Country-01.txt");
            window.ShowDialog();
        }

        #region crédits & help

        private void Credits_Click(object sender, RoutedEventArgs e)
        {
            W_Info window = new W_Info()
            {
                Title = "Credits",
                Message = "Credits",
                Texteuh = _Model.Lang.Credits,
            };
            window.ShowDialog();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            W_Info window = new W_Info()
            {
                Title = "Help",
                Message = "Help",
                Texteuh = _Model.Lang.Help,
            };
            window.ShowDialog();
        }
        #endregion

        #region Extraction de plateforme

        private void Can_ExtractPlatform(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _Model.SelectedPlatform != null;
        }

        private void Exec_ExtractPlatform(object sender, ExecutedRoutedEventArgs e)
        {
            TreeSave ts = new TreeSave()
            {
                Model = new M_SaveFile()
                {
                    StartingFolder = System.IO.Path.Combine(Config.WorkingFolder, _Model.SelectedPlatform.Name),
                    ShowFiles = true,
                    Info = "Save to ...",
                    FileValue = $"TBPlatform - {_Model.SelectedPlatform.Name}.xml"
                }
            };

            if (ts.ShowDialog() == true)
            {
                XML_Platforms.ExtractPlatform(Path.Combine(Config.LaunchBoxPath, Config.PlatformsFile), _Model.SelectedPlatform.Name, ts.Model.LinkResult);
            }
        }
        #endregion

        #region Games
        private void Can_GamesSelected(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _Model.SelectedGames.Any();
        }

        /// <summary>
        /// Création d'un fichier indiquant les fichiers choisis par l'utilisateur dans LaunchBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exec_ExtractDefFiles(object sender, ExecutedRoutedEventArgs e)
        {
            _Model.ExtractDefaultFiles();
        }

        /// <summary>
        /// Création d'un fichier TBGame.Xml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exec_ExtractTBGames(object sender, ExecutedRoutedEventArgs e)
        {
            _Model.ExtractTBGames();
        }

        private void Exec_ExtractNPGame(object sender, ExecutedRoutedEventArgs e)
        {
            _Model.Extract_NPBackups();
        }

        /// <summary>
        /// Vérifie que le manuel, la musique et le jeu sont correctement linkés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exec_CheckGameValidity(object sender, ExecutedRoutedEventArgs e)
        {

            _Model.CheckGamesValidity();
        }
        #endregion
        private void Can_SelectAll(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _Model.AvailableGames != null &&
                                _Model.AvailableGames.Any() &&
                                _Model.AvailableGames.Count != _Model.SelectedGames.Count;
        }

        private void Exec_SelectAll(object sender, ExecutedRoutedEventArgs e)
        {
            _Model.SelectAll();
        }



        private void Can_DeselectAll(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _Model.AvailableGames !=null &&
                                _Model.AvailableGames.Any() &&
                                _Model.SelectedGames.Any();
        }

        private void Exec_SelectNone(object sender, ExecutedRoutedEventArgs e)
        {
            _Model.SelectNone();
        }


    }
}
