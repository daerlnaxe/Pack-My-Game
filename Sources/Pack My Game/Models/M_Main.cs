using System;
using System.ComponentModel;
using PS = Pack_My_Game.Properties.Settings;
using Pack_My_Game.Cont;
using System.Collections.ObjectModel;
using System.IO;
using Hermes;
using LaunchBox_XML.XML;
using Common_PMG.Container;
using System.Collections.Generic;
using DxTBoxCore.MBox;
using DxTBoxCore.Common;
using Pack_My_Game.Core;
using DxTBoxCore.Box_Progress;
using Common_PMG.Container.Game;
using DxTBoxCore.Box_Collec;
using System.Linq;
using Cst = LaunchBox_XML.Common;

namespace Pack_My_Game.Models
{
    public class M_Main : A_Err, I_Lang
    {

        // private Language _Lang;
        public Language Lang => Common.ObjectLang;


        /// <summary>
        /// Dossier de LaunchBox
        /// </summary>
        public string LaunchBoxPath => PS.Default.LBPath;


        public string WorkingFolder => PS.Default.OutPPath;

        public ObservableCollection<string> Options { get; internal set; } = new ObservableCollection<string>();

        // ---

        public Platform SelectedPlatform { get; set; }
        public ObservableCollection<Platform> Platforms { get; private set; } = new ObservableCollection<Platform>();

        // ---

        private ObservableCollection<ShortGame> _AvailableGames;
        public ObservableCollection<ShortGame> AvailableGames
        {
            get => _AvailableGames;
            set
            {
                _AvailableGames = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<ShortGame> SelectedGames = new ObservableCollection<ShortGame>();

        // ---
        public bool NoGame { get; set; } = true;

        public M_Main()
        {
            LoadOptions();
            LoadPlatforms();
        }


        internal void Relocalize()
        {
            OnPropertyChanged(nameof(Lang));
        }

        internal void ReloadConfig()
        {
            OnPropertyChanged(nameof(LaunchBoxPath));
            OnPropertyChanged(nameof(WorkingFolder));
            LoadOptions();
            //LoadPlatforms();
        }

        private void LoadOptions()
        {
            Options.Clear();

            if (PS.Default.opInfos)
                Options.Add(Lang.InfosGExp);

            if (PS.Default.opOBGame)
                Options.Add(Lang.OriXBExp);

            if (PS.Default.opEBGame)
                Options.Add(Lang.EnXBExp);

            if (PS.Default.opTreeV)
                Options.Add(Lang.TViewFExp);

            if (PS.Default.opClones)
                Options.Add(Lang.CCopyExp);

            if (PS.Default.opCheatCodes)
                Options.Add(Lang.CheatsCopyExp);

            if (PS.Default.opZip)
                Options.Add(Lang.CompZExp);

            if (PS.Default.op7_Zip)
                Options.Add(Lang.Comp7ZExp);
        }

        private void LoadPlatforms()
        {
            string xmlFilePlat = Path.Combine(LaunchBoxPath, PS.Default.fPlatforms);
            if (!File.Exists(xmlFilePlat))
            {
                HeTrace.WriteLine($"File doesn't exist '{xmlFilePlat}'");
                return;
            }

            Platforms.Clear();
            XML_Platforms.ListShortPlatforms(xmlFilePlat, Platforms);

        }

        internal void LoadGames()
        {
            SelectedGames.Clear();

            string xmlFile = Path.Combine(PS.Default.LBPath, PS.Default.dPlatforms, $"{SelectedPlatform.Name}.xml");
            AvailableGames = new ObservableCollection<ShortGame>(XML_Games.ListShortGames(xmlFile));
            // throw new NotImplementedException();
        }

        internal void AddGame(ShortGame sgame)
        {
            if (!SelectedGames.Contains(sgame))
                SelectedGames.Add(sgame);

            Test_HasElement(SelectedGames, nameof(NoGame));
        }

        internal void RemoveGame(ShortGame sgame)
        {
            SelectedGames.Remove(sgame);
            Test_HasElement(SelectedGames, nameof(NoGame));
        }


        internal void SelectNone()
        {
            foreach (var g in AvailableGames)
                g.IsSelected = false;

            SelectedGames.Clear();
            Test_HasElement(SelectedGames, nameof(NoGame));
        }

        internal void SelectAll()
        {
            foreach (var g in AvailableGames)
            {
                g.IsSelected = true;
                if (!SelectedGames.Contains(g))
                    SelectedGames.Add(g);
            }

            Test_HasElement(SelectedGames, nameof(NoGame));

        }


        /// <summary>
        /// 
        /// </summary>
        internal void Process()
        {
            // Vérifier qu'une plateforme est sélectionnée
            Test_NullValue(SelectedPlatform);

            // Vérifie que des jeux ont été sélectionnés
            Test_HasElement(SelectedGames, nameof(NoGame));

            if (HasErrors)
                return;            


            if (LaunchBoxCore.PrepareToContinue(SelectedGames, SelectedPlatform.Name))
            {
                LaunchBoxCore lbCore = new LaunchBoxCore(SelectedPlatform.Name);

                DxAsStateProgress progressW = new DxAsStateProgress()
                {
                    AutoClose = false,
                    Model = (M_ProgressCC)M_ProgressC.Create<LaunchBoxCore, M_ProgressCC>(lbCore, () => lbCore.Run()),

                    /*new M_ProgressCC()
                    {
                        MaxProgressT = SelectedGames.Count,
                    },
                    TaskToRun = lbCore*/
                };
                progressW.ShowDialog();

                // On vérifie que la tâche est allée jusqu'au bout
                if (progressW.Model.TaskRunning.Status != System.Threading.Tasks.TaskStatus.RanToCompletion)
                {
                    HeTrace.WriteLine("Task not ran to completion");
                    return;

                }

                if (DxMBox.ShowDial($"Remove game(s) from the list ?", "Remove game(s)", E_DxButtons.No | E_DxButtons.Yes) == true)
                {
                    HeTrace.WriteLine($"Remove game(s) from list");
               /*     foreach (ShortGame sgame in selectedGames)
                        AvailableGames.Remove(sgame);*/
                }
            }

            // traiter pour retirer ce qui est traité? voir etc etc
        }


        // ---

        /// <summary>
        /// Vérifie que les jeux ont les liens pour les manuels, musiques et jeux valides.
        /// </summary>
        internal void CheckGamesValidity()
        {
            string platformXmlFile = Path.Combine(PS.Default.LBPath, PS.Default.dPlatforms, $"{SelectedPlatform.Name}.xml");

            bool ok = true;
            foreach (ShortGame g in SelectedGames)
            {
                GamePaths game = (GamePaths)XML_Games.Scrap_LBGame<LBGame>(platformXmlFile, Cst.Id, g.Id);

                string res = LaunchBoxFunc.CheckGameValidity(game);

                if (!string.IsNullOrEmpty(res))
                {
                    DxMBox.ShowDial($"Game {game.Title} is not ok", "Warning", E_DxButtons.Ok, res);
                    ok = false;
                }
            }

            if (ok)
                DxMBox.ShowDial("Games are ok", buttons: E_DxButtons.Ok);
        }




        /// <summary>
        /// Extrait les fichiers par défaut choisis dans LaunchBox par rapport à leur racine
        /// Le but est uniquement de déterminer l'emplacement dans la nouvelle hiérarchie
        /// </summary>
        /// <remarks>
        /// Il est impératif que:
        ///     - les liens soient à jour.
        ///     - les liens se servent des dossier de la machine
        /// Sinon on ne récupère pas.
        /// </remarks>
        internal void ExtractDefaultFiles()
        {
            string platformXmlFile = Path.Combine(PS.Default.LBPath, PS.Default.dPlatforms, $"{SelectedPlatform.Name}.xml");
            Platform p = XML_Platforms.GetPlatformPaths(Path.Combine(LaunchBoxPath, PS.Default.fPlatforms), SelectedPlatform.Name);

            foreach (var g in SelectedGames)
            {

                GamePaths game = (GamePaths)XML_Games.Scrap_LBGame<LBGame>(platformXmlFile, Cst.Id, g.Id);

                game.ApplicationPath = Assign(game.ApplicationPath, SelectedPlatform.FolderPath);


                game.ManualPath = Assign(game.ManualPath, p, "Manual");

                game.MusicPath = Assign(game.MusicPath, p, "Music");

                game.VideoPath = Assign(game.VideoPath, p, "Video");

                game.ThemeVideoPath = Assign(game.ThemeVideoPath, p, "Video");

                string tmp = Path.Combine(WorkingFolder, SelectedPlatform.Name, Cst.WindowsConv_TitleToFileName(game.Title));
                Directory.CreateDirectory(tmp);
                tmp = Path.Combine(tmp, "DPGame.json");
                game.WriteToJson(tmp);

                //  Check_AllDefaultFiles(lbGame);
            }

        }

        private string Assign(string file, Platform platform, string category)
        {
            var platformFolder = platform.PlatformFolders.First((x) => x.MediaType.Equals(category, StringComparison.OrdinalIgnoreCase));

            if (platformFolder != null)
            {
                return Assign(file, platformFolder.FolderPath);
            }
            else
            {
                return Assign(file, null);
            }


            //    Path.Combine(PS.Default.LBPath, Common.Manuals, SelectedPlatform.Name)); ;

        }




        /// <summary>
        /// Cette version ne fait pas d'assignation par défaut
        /// </summary>
        /// <param name="file"></param>
        /// <param name="platformPath"></param>
        /// <param name="defaultPlatformPath"></param>
        /// <returns></returns>
        private string Assign(string file, string platformPath/*, string defaultPlatformPath*/)
        {
            file = Path.GetFullPath(file, PS.Default.LBPath);

            // si c'est vide on laisse tomber
            if (string.IsNullOrEmpty(file))
                return string.Empty;

            // Quand le platformPath est null c'est qu'on utilise les dossiers par défaut
            /*if (string.IsNullOrEmpty(platformPath))
                platformPath = defaultPlatformPath;*/

            platformPath = Path.GetFullPath(platformPath, PS.Default.LBPath);

            // si l'on ne retrouve pas on n'assigne rien
            if (!File.Exists(file))
            {
                return string.Empty;
            }

            // si c'est bien linké selon les dossiers de la plateforme
            if (file.Contains(platformPath))
                return file.Replace(platformPath, string.Empty);

            // Si ce n'est pas bien linké
            return $"\\{Path.GetFileName(file)}";
        }

        private void Check_AllDefaultFiles(LBGame g)
        {

        }
    }
}
