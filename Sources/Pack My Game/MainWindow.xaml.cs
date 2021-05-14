using Common_PMG.Container.Game;
using Common_PMG.XML;
using DxTBoxCore.BoxChoose;
using Pack_My_Game.IHM;
using Pack_My_Game.Language;
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
        /// <summary>
        /// Vérifie si les liens sont valides
        /// </summary>
        public static readonly RoutedUICommand CheckGameValidityCmd =
            new RoutedUICommand(LanguageManager.Instance.Lang.Cmd_TestGmLinks, nameof(CheckGameValidityCmd), typeof(MainWindow));
        /// <summary>
        /// Extrait un fichier True Backup
        /// </summary>
        public static readonly RoutedUICommand ExtractTBGameCmd =
            new RoutedUICommand(LanguageManager.Instance.Lang.Cmd_ExtTBGame, nameof(ExtractTBGameCmd), typeof(MainWindow));
        /// <summary>
        /// Extrait un fichier No Path Backup
        /// </summary>
        public static readonly RoutedUICommand ExtractNBGameCmd =
            new RoutedUICommand(LanguageManager.Instance.Lang.Cmd_ExtNBGame, nameof(ExtractNBGameCmd), typeof(MainWindow));
        /// <summary>
        /// Extrait un fichier plateforme
        /// </summary>
        public static readonly RoutedUICommand ExtractPlatformCmd =
            new RoutedUICommand(LanguageManager.Instance.Lang.Cmd_ExtPlatform, "ExtractPlatformCmd", typeof(MainWindow));
        /// <summary>
        /// Extrait un fichier DPGame.json
        /// </summary>
        public static readonly RoutedUICommand ExtractDefaultFilesCmd =
            new RoutedUICommand(LanguageManager.Instance.Lang.Cmd_ExtDef, "ExtractDefaultFilesCmd", typeof(MainWindow));
        /// <summary>
        /// Sélectionne tout
        /// </summary>
        public static readonly RoutedUICommand SelectAllCmd =
            new RoutedUICommand(LanguageManager.Instance.Lang.Select_All, "SelectAllCmd", typeof(MainWindow));
        /// <summary>
        /// Sélectionne rien
        /// </summary>
        public static readonly RoutedUICommand SelectNoneCmd =
            new RoutedUICommand(LanguageManager.Instance.Lang.Select_None, "SelectNoneCmd", typeof(MainWindow));

        private M_Main _Model = new M_Main();

        /// <summary>
        /// Titre de la fenêtre
        /// </summary>
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
                Texteuh = LanguageManager.Instance.Lang.Word_Credits,
            };
            window.ShowDialog();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            W_Info window = new W_Info()
            {
                Title = "Help",
                Message = "Help",
                Texteuh = LanguageManager.Instance.Lang.Word_Help,
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
                    StartingFolder = System.IO.Path.Combine(Config.HWorkingFolder, _Model.SelectedPlatform.Name),
                    ShowFiles = true,
                    Info = "Save to ...",
                    FileValue = $"TBPlatform - {_Model.SelectedPlatform.Name}.xml"
                }
            };

            if (ts.ShowDialog() == true)
            {
                XML_Platforms.ExtractPlatform(Path.Combine(Config.HLaunchBoxPath, Config.PlatformsFile), _Model.SelectedPlatform.Name, ts.Model.LinkResult);
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

        private void Exec_ExtractNBGame(object sender, ExecutedRoutedEventArgs e)
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
