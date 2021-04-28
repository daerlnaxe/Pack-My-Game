using Common_PMG;
using Common_PMG.Container;
using Common_PMG.Container.Game;
using Common_PMG.Container.Game.LaunchBox;
using Common_PMG.Models;
using Common_PMG.XML;
using DxTBoxCore.Box_Progress;
using DxTBoxCore.Common;
using DxTBoxCore.Box_MBox;
using Hermes;
using Pack_My_Game.Cont;
using Pack_My_Game.Core;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using PS = Pack_My_Game.Properties.Settings;
using Common_Graph;
using DxTBoxCore.BoxChoose;

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

        public ContPlatFolders SelectedPlatform { get; set; }
        public ObservableCollection<ContPlatFolders> Platforms { get; private set; } = new ObservableCollection<ContPlatFolders>();

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

            if (PS.Default.opTBGame)
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

                TaskLauncher taskLauncher = new TaskLauncher()
                {
                    ProgressIHM = new DxStateProgress()
                    {
                        Model = lbCore,
                    }
                    ,
                    AutoCloseWindow = false,
                    MethodToRun = () => lbCore.Run(),

                };
                bool? Res = taskLauncher.Launch(lbCore);

                /*
            DxAsStateProgress progressW = new DxAsStateProgress()
            {
                AutoClose = false,
                Model = lbCore,
                Launcher = lbCore

                //= M_ProgressD.Create<LaunchBoxCore, M_ProgressDL>(lbCore, () => lbCore.Run()),

                 * new M_ProgressCC()
                {
                    MaxProgressT = SelectedGames.Count,
                },
                TaskToRun = lbCore*/
                /*};
                progressW.ShowDialog();

                // On vérifie que la tâche est allée jusqu'au bout
                if (progressW.Launcher.TaskRunning.Status != System.Threading.Tasks.TaskStatus.RanToCompletion)
                {
                    HeTrace.WriteLine("Task not ran to completion");
                    return;

                }
                */

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

            foreach (ShortGame g in SelectedGames)
            {
                var res = LaunchBoxFunc.CheckGameValidity(g, platformXmlFile);

                SafeBoxes.ShowStatus($"Check  {g.Title}", "Game status", res, buttons: E_DxButtons.Ok);

            }
        }

        internal void ExtractTBGames()
        {
            /*
            string destFolder;
            TreeChoose cf = new TreeChoose()
            {
                Model = new M_ChooseFolder()
                {
                    HideWindowsFolder = true,
                    Info = "Select a folder to extract",
                    StartingFolder = PS.Default.OutPPath,
                    ShowFiles = false,
                }
            };
            if (cf.ShowDialog() == true)
            {
                PS.Default.LastKPath = cf.Model.LinkResult;
                PS.Default.Save();
                destFolder = cf.Model.LinkResult;
            }*/

            string platformFile = Path.Combine(PS.Default.LBPath, PS.Default.dPlatforms, $"{SelectedPlatform.Name}.xml");

            foreach (var game in SelectedGames)
            {
                string gamefolder = Tool.WindowsConv_TitleToFileName(game.Title);
                string destFolder = Path.Combine(WorkingFolder, SelectedPlatform.Name, gamefolder);
                Directory.CreateDirectory(destFolder);
                XML_Games.TrueBackup(platformFile, game.Id, destFolder);
            }

            DxMBox.ShowDial("Done");
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
            ContPlatFolders p = XML_Platforms.GetPlatformPaths(Path.Combine(LaunchBoxPath, PS.Default.fPlatforms), SelectedPlatform.Name);

            foreach (var g in SelectedGames)
            {
                GamePaths gp = (GamePaths)XML_Games.Scrap_LBGame<LBGame>(platformXmlFile, GameTag.ID, g.Id);

                //clones
                foreach (var clone in XML_Games.ListClones(platformXmlFile, Tag.GameId, g.Id))
                    gp.AddApplication(clone.Id, clone.Id, clone.ApplicationPath);

                if (PS.Default.opClones)
                    foreach (var app in gp.Applications)
                        app.Name = Assign(app.CurrentPath, SelectedPlatform.FolderPath, PS.Default.KeepGameStruct);

                gp.ManualPath = Assign(gp.ManualPath, p, "Manual", PS.Default.KeepManualStruct);
                gp.MusicPath = Assign(gp.MusicPath, p, "Music", PS.Default.KeepMusicStruct);
                gp.VideoPath = Assign(gp.VideoPath, p, "Video", true);
                gp.ThemeVideoPath = Assign(gp.ThemeVideoPath, p, "Video", true);

                string tmp = Path.Combine(WorkingFolder, SelectedPlatform.Name, Tool.WindowsConv_TitleToFileName(gp.Title));
                Directory.CreateDirectory(tmp);
                tmp = Path.Combine(tmp, "DPGame.json");

                gp.WriteToJson(tmp);
            }
            DxMBox.ShowDial("Done");

        }

        private string Assign(string file, ContPlatFolders platform, string category, bool keepStruct)
        {
            var platformFolder = platform.PlatformFolders.First((x) => x.MediaType.Equals(category, StringComparison.OrdinalIgnoreCase));

            if (platformFolder != null)
            {
                return Assign(file, platformFolder.FolderPath, keepStruct);
            }
            else
            {
                return Assign(file, null, keepStruct);
            }
        }


        /// <summary>
        /// Cette version ne fait pas d'assignation par défaut
        /// </summary>
        /// <param name="file"></param>
        /// <param name="platformPath"></param>
        /// <param name="defaultPlatformPath"></param>
        /// <returns></returns>
        private string Assign(string file, string platformPath, bool keepStruct)
        {
            file = Path.GetFullPath(file, PS.Default.LBPath);

            // si c'est vide on laisse tomber
            if (string.IsNullOrEmpty(file))
                return string.Empty;


            platformPath = Path.GetFullPath(platformPath, PS.Default.LBPath);

            // si l'on ne retrouve pas on n'assigne rien
            if (!File.Exists(file))
            {
                return string.Empty;
            }

            // si c'est bien linké selon les dossiers de la plateforme
            if (keepStruct && file.Contains(platformPath))
                return file.Replace(platformPath, ".");

            // Si ce n'est pas bien linké
            return $".\\{Path.GetFileName(file)}";
        }

        private void Check_AllDefaultFiles(LBGame g)
        {

        }
    }
}
