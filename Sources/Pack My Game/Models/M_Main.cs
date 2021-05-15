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
using static Pack_My_Game.Common;
using Common_Graph;
using DxTBoxCore.BoxChoose;
using System.Collections.Generic;
using Pack_My_Game.IHM;
using Pack_My_Game.Language;

namespace Pack_My_Game.Models
{
    public class M_Main : A_Err//, I_Lang
    {

        // private Language _Lang;
        //public LangContent Lang => Common.ObjectLang;

        /// <summary>
        /// Dossier de LaunchBox
        /// </summary>
        public string LaunchBoxPath => Config.HLaunchBoxPath;


        public string WorkingFolder => Config.HWorkingFolder;

        public ObservableCollection<string> Options { get; internal set; } = new ObservableCollection<string>();

        // ---

        private ContPlatFolders _OldPlatform { get; set; }

        public ContPlatFolders _SelectedPlatforms;
        public ContPlatFolders SelectedPlatform
        {
            get => _SelectedPlatforms;
            set
            {
                _SelectedPlatforms = value;
                OnPropertyChanged();
            }
        }


        private List<ContPlatFolders> _Platforms;
        public List<ContPlatFolders> Platforms
        {
            get => _Platforms;
            set
            {
                _Platforms = value;
                OnPropertyChanged();
            }
        }

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

        internal void ShowConfig()
        {
            W_Config cfg = new W_Config();
            var oldPlatform = SelectedPlatform;

            // Conservation de l'ancienne configuration
            var oldConfig = new Configuration(Common.Config);
            var oldLanguage = LanguageManager.Instance.CurrentLanguage;

            if (cfg.ShowDialog() == true)
            {
                // On recharge le fichier langue
            //    Relocalize();
                ReloadConfig(oldConfig);
                return;
            }

            LanguageManager.Instance.CurrentLanguage = oldLanguage;
            oldConfig = null;

        }


     /*   internal void Relocalize()
        {
            OnPropertyChanged(nameof(Lang));
        }*/

        internal void ReloadConfig(Configuration oldConfig)
        {
            _Errors.Clear();
            if (oldConfig.HLaunchBoxPath == null || !oldConfig.HLaunchBoxPath.Equals(Config.HLaunchBoxPath))
            {
                OnPropertyChanged(nameof(LaunchBoxPath));
                LoadPlatforms();
                SelectedPlatform = null;
                _OldPlatform = null;
                AvailableGames = null;
                SelectedGames.Clear();
            }

            OnPropertyChanged(nameof(WorkingFolder));
            LoadOptions();
        }

        private void LoadOptions()
        {
            Options.Clear();

            if (Config.CreateInfos)
                Options.Add(LanguageManager.Instance.Lang.Opt_InfosG);

            if (Config.CreateTBGame)
                Options.Add(LanguageManager.Instance.Lang.Opt_OriXB);

            if (Config.CreateEBGame)
                Options.Add(LanguageManager.Instance.Lang.Opt_EnXB);

            if (Config.CreateTreeV)
                Options.Add(LanguageManager.Instance.Lang.Opt_TViewF);

            if (Config.CopyClones)
                Options.Add(LanguageManager.Instance.Lang.Opt_CopyClones);

            if (Config.CopyCheats)
                Options.Add(LanguageManager.Instance.Lang.Opt_CopyCheats);

            if (Config.ZipCompression)
                Options.Add(LanguageManager.Instance.Lang.Comp_Zip);

            if (Config.SevZipCompression)
                Options.Add(LanguageManager.Instance.Lang.Comp_7Z);
        }

        internal void PlatformChanged()
        {
            if (SelectedPlatform == null || SelectedPlatform.Name.Equals(_OldPlatform?.Name))
                return;

            LoadGames();
            _OldPlatform = SelectedPlatform;
        }

        internal void Refresh_Platforms()
        {
            LoadPlatforms();

            if (_OldPlatform != null)
                SelectedPlatform = Platforms.FirstOrDefault(x => x.Name.Equals(_OldPlatform.Name));
            else
                SelectedGames.Clear();
        }


        internal void LoadPlatforms()
        {
            if (!Directory.Exists(Config.HLaunchBoxPath))
                Add_Error(LanguageManager.Instance.Lang.Err_LaunchBoxF, nameof(LaunchBoxPath));
            if (!Directory.Exists(Config.HWorkingFolder))
                Add_Error("Working folder doesn't exist", nameof(WorkingFolder));

            if (HasErrors)
                return;


            string xmlFilePlat = Path.Combine(LaunchBoxPath, Config.PlatformsFile);
            if (!File.Exists(xmlFilePlat))
            {
                HeTrace.WriteLine($"File doesn't exist '{xmlFilePlat}'");
                return;
            }

            Platforms = XML_Platforms.ListShortPlatforms(xmlFilePlat)
                            .OrderBy(x => x.Name)
                            .ToList();
        }

        internal void LoadGames()
        {
            SelectedGames.Clear();

            string xmlFile = Path.Combine(Config.HLaunchBoxPath, Config.PlatformsFolder, $"{SelectedPlatform.Name}.xml");
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
                    foreach (ShortGame sgame in SelectedGames)
                    {
                        AvailableGames.Remove(sgame);
                        sgame.IsSelected = false;
                    }
                }

                SelectedGames.Clear();
            }

            // traiter pour retirer ce qui est traité? voir etc etc
        }


        // ---

        /// <summary>
        /// Vérifie que les jeux ont les liens pour les manuels, musiques et jeux valides.
        /// </summary>
        internal void CheckGamesValidity()
        {
            string platformXmlFile = Path.Combine(Config.HLaunchBoxPath, Config.PlatformsFolder, $"{SelectedPlatform.Name}.xml");

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

            string platformFile = Path.Combine(Config.HLaunchBoxPath, Config.PlatformsFolder, $"{SelectedPlatform.Name}.xml");

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
            HeTrace.WriteLine($"[{nameof(ExtractDefaultFiles)}] Begin...");

            try
            {
                string platformXmlFile = Path.Combine(Config.HLaunchBoxPath, Config.PlatformsFolder, $"{SelectedPlatform.Name}.xml");
                string platformsFile = Path.Combine(LaunchBoxPath, Config.PlatformsFile);

                ContPlatFolders p = XML_Platforms.GetPlatformPaths(platformsFile, SelectedPlatform.Name);
                if (p.PlatformFolders.Count == 0)
                    throw new Exception("Problem with platform name, children folders have a different name");


                foreach (var g in SelectedGames)
                {
                    HeTrace.WriteLine($"[{nameof(ExtractDefaultFiles)}] extraction for '{g.Title}'");

                    GamePaths gp = (GamePaths)XML_Games.Scrap_LBGame<LBGame>(platformXmlFile, GameTag.ID, g.Id);

                    //clones
                    foreach (var clone in XML_Games.ListClones(platformXmlFile, Tag.GameId, g.Id))
                        gp.AddApplication(clone.Id, clone.Id, clone.ApplicationPath);

                    if (Config.CopyClones)
                        foreach (var app in gp.Applications)
                            app.Name = Assign(app.CurrentPath, SelectedPlatform.FolderPath, Config.KeepGameStruct);

                    gp.ManualPath = Assign(gp.ManualPath, p, "Manual", Config.KeepManualStruct);
                    gp.MusicPath = Assign(gp.MusicPath, p, "Music", Config.KeepMusicStruct);
                    gp.VideoPath = Assign(gp.VideoPath, p, "Video", true);
                    gp.ThemeVideoPath = Assign(gp.ThemeVideoPath, p, "Video", true);

                    string tmp = Path.Combine(WorkingFolder, SelectedPlatform.Name, Tool.WindowsConv_TitleToFileName(gp.Title));

                    Directory.CreateDirectory(tmp);
                    tmp = Path.Combine(tmp, "DPGame.json");

                    HeTrace.WriteLine($"[{nameof(ExtractDefaultFiles)}] write to '{tmp}'");
                    gp.WriteToJson(tmp);
                }

                DxMBox.ShowDial("Done");
                HeTrace.WriteLine($"[{nameof(ExtractDefaultFiles)}] Done...");
            }
            catch (Exception exc)
            {
                DxMBox.ShowDial(exc.Message);
                HeTrace.WriteLine(exc.Message);
                HeTrace.WriteLine(exc.StackTrace);
            }
        }

        private string Assign(string file, ContPlatFolders platform, string category, bool keepStruct)
        {
            HeTrace.WriteLine($"[Assign] '{file}' - '{category}'");
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
            file = Path.GetFullPath(file, Config.HLaunchBoxPath);

            // si c'est vide on laisse tomber
            if (string.IsNullOrEmpty(file))
                return string.Empty;


            platformPath = Path.GetFullPath(platformPath, Config.HLaunchBoxPath);

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


        internal void Extract_NPBackups()
        {
            try
            {
                string platformXmlFile = Path.Combine(Config.HLaunchBoxPath, Config.PlatformsFolder, $"{SelectedPlatform.Name}.xml");

                foreach (var game in SelectedGames)
                {
                    string gamePath = Path.Combine(WorkingFolder, SelectedPlatform.Name, Tool.WindowsConv_TitleToFileName(game.Title));

                    XML_Games.NPBackup(platformXmlFile, game.Id, gamePath);

                }

                DxMBox.ShowDial("Done");
                HeTrace.WriteLine($"[{nameof(ExtractDefaultFiles)}] Done...");
            }
            catch (Exception exc)
            {
                DxMBox.ShowDial(exc.Message);
                HeTrace.WriteLine(exc.Message);
                HeTrace.WriteLine(exc.StackTrace);
            }
        }

    }
}
