using AsyncProgress;
using AsyncProgress.Cont;
using AsyncProgress.Basix;
using Common_PMG.Container;
using Common_PMG.Container.AAPP;
using Common_PMG.Container.Game;
using Common_PMG.Container.Game.LaunchBox;
using DxLocalTransf;
using DxPaths.Windows;
using DxTBoxCore.Box_Decisions;
using DxTBoxCore.Common;
using DxTBoxCore.Box_MBox;
using HashCalc;
using Hermes;
using Hermes.Messengers;
using Pack_My_Game.Compression;
using Pack_My_Game.Cont;
using Pack_My_Game.Files;
using Pack_My_Game.IHM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Windows;
using Common_PMG.XML;
using DxLocalTransf.Copy;
using Common_Graph;
//using static Pack_My_Game.Properties.Settings;
using static Pack_My_Game.Common;
using Pack_My_Game.Language;
//using Pack_My_Game.Compression;

namespace Pack_My_Game.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Dans la mesure du possible on n'utilise pas LBGame, on va copier directement le xml
    /// </remarks>
    partial class LaunchBoxCore : A_ProgressPersistD, I_ASBase
    {
        public delegate object SignalBoxHandler(object sender, params string[] parameters);
        public delegate void SignalRecapHandler(object sender, string destination, string title, ContPlatFolders platform);
        public delegate E_Decision AskForConflict(object sender, string source, string destination, E_DxConfB buttons = E_DxConfB.All);
        public delegate string[] GetFilesHandler(object sender, List<string> location, string mediatype);

        /*
        public event DoubleHandler UpdateProgressT;
        public event MessageHandler UpdateStatusT;
        public event DoubleHandler MaximumProgressT;
        public event DoubleHandler UpdateProgress;
        public event MessageHandler UpdateStatus;
        public event DoubleHandler MaximumProgress;*/

        // ---

        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();
        public CancellationToken CancelToken { get; }

        public bool IsPaused { get; set; }

        public bool IsInterrupted { get; private set; }
        public bool CancelFlag { get; private set; }


        // ---

        /// <summary>
        /// Liste des jeux
        /// </summary>
        public static ObservableCollection<ShortGame> _SelectedGames { get; private set; }

        /// <summary>
        /// Object containing folders
        /// </summary>
        ContPlatFolders _ZePlatform { get; }


        /// <summary>
        /// Fichier xml contenant les jeux
        /// </summary>
        private string _XMLPlatformFile { get; }


        /// <summary>
        /// Repertoire de travail
        /// </summary>
        private string _WFolder { get; }

        /// <summary>
        /// Répertoire de travail selon la machine (voir si utile)
        /// </summary>
        public string _SystemPath { get; }




        #region Décisions
        /// <summary>
        /// Indique la décision précédente en cas de all.
        /// </summary>
        E_Decision MemorizedDecision;
        /// <summary>
        /// Valable uniquement durant le traitement
        /// </summary>
        E_Decision TempDecision;
        /// <summary>
        /// Used to memorize action to 
        /// </summary>
        E_Decision _FileConflictDecision;

        #endregion
        /// <summary>
        /// Répertoire de travail pour le jeu (voir si utile)
        /// </summary>
        //private string _GamePath;

        internal static bool PrepareToContinue(ObservableCollection<ShortGame> selectedGames, string platformName)
        {
            // Vérification des fichiers pour chaque jeu (ne gère pas les vidéos)
            LaunchBoxFunc.CheckGamesValidity(selectedGames, platformName);


            if (selectedGames.Count == 0)
            {
                if (selectedGames.Count > 1)
                    DxMBox.ShowDial("No game selected");

                return false;
            }
            _SelectedGames = selectedGames;
            /*W_Games gamesWindow = new W_Games();
            gamesWindow.SelectedGames = selectedGames;

            if (gamesWindow.ShowDialog() != true)
            {
                return false;
            }

            if (selectedGames.Count == 0)
            {
                if (selectedGames.Count > 1)
                    DxMBox.ShowDial("No game selected");

                return false;
            }
            _SelectedGames = gamesWindow.SelectedGames;
            */
            return true;


        }

        /// <summary>
        /// Sert au traitement de packaging pour un jeu
        /// </summary>
        /// <param name="iD"></param>
        /// <param name="platformName"></param>
        public LaunchBoxCore(string platformName)
        {
            if (string.IsNullOrEmpty(platformName))
                throw new Exception();


            //_PlatformName = platformName;

            #region Initialisation des chemins
            _WFolder = Config.HWorkingFolder;
            // Chemin du dossier temporaire du system
            _SystemPath = Path.Combine(_WFolder, platformName);

            #endregion

            _XMLPlatformFile = Path.Combine(Config.HLaunchBoxPath, Config.PlatformsFolder, $"{platformName}.xml");
            _ZePlatform = XML_Platforms.GetPlatformPaths(Path.Combine(Config.HLaunchBoxPath, Config.PlatformsFile), platformName);


            #region Messages
            MeDebug mdb = new MeDebug()
            {
                ByPass = true,
            };
            HeTrace.AddMessenger("Debug", mdb);

            // Réorientation via les signaux
            MeEmit mdE = new MeEmit()
            {
                ByPass = true,
            };
            HeTrace.AddMessenger("Signal", mdE);
            mdE.SignalWrite += (x, y) => SetStatus(x, new StateArg(y, false));
            mdE.SignalWriteLine += (x, y) => SetStatus(x, new StateArg(y, false));

            #endregion

            XML_Games.Signal += (x, y) => SetStatus(x, new StateArg(y, false));

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeSleep"></param>
        /// <returns></returns>
        public object Run(int timeSleep = 10)
        {
            foreach (var zeGame in _SelectedGames)
            {

                HeTrace.WriteLine($"[Main] PackMe for '{zeGame.Title}' | '{zeGame.Id}'");

                // Système de log par jeu
                if (Config.UseLogFile)
                {
                    MeSimpleLog gameLog = new MeSimpleLog(Path.Combine(_WFolder, $"{_ZePlatform.Name} - {zeGame.ExploitableFileName}.log"))
                    {

                    };
                    gameLog.AddCaller(this);
                    HeTrace.AddLogger("game", gameLog);
                }

                try
                {
                    PackMe(zeGame);

                }
                catch (Exception exc)
                {
                    HeTrace.WriteLine(exc.Message, this);
                    HeTrace.WriteLine(exc.StackTrace, this);
                    SafeBoxes.Dispatch_Mbox(this, exc.Message, "Error", E_DxButtons.Ok);
                }
                finally
                {
                    HeTrace.RemoveLogger("game");

                }
            }
            return null;
        }

        // --- 

        /// <summary>
        /// Travail pour un jeu
        /// </summary>
        /// <param name="shGame"></param>
        public void PackMe(ShortGame shGame)
        {
            TempDecision = MemorizedDecision;

            // Verif
            if (shGame == null || string.IsNullOrEmpty(shGame.Id))
            {
                HeTrace.WriteLine("Game property: null");
                return;
            }


            // Dossiers
            string gamePath = Path.Combine(_SystemPath, $"{shGame.ExploitableFileName}");             // New Working Folder
                                                                                                      //Compress_ZipMode(gamePath, shGame.Title);
                                                                                                      // Compress_7ZipMode(gamePath, shGame.Title);
                                                                                                      // Contrôle de collisions pour les dossiers
            if (Directory.Exists(gamePath))
            {
                HeTrace.WriteLine($"Directory Exists '{gamePath}'", this);
                // Demande à l'utilisateur si aucune précédente
                if (MemorizedDecision == E_Decision.None)
                {
                    Application.Current.Dispatcher?.Invoke(() =>
                        TempDecision = MBDecision.ShowDial(null, gamePath, LanguageManager.Instance.Lang.Folder_Ex, E_DxConfB.Trash | E_DxConfB.OverWrite));

                    switch (TempDecision)
                    {
                        /*   // Gestion des stops
                           case E_Decision.Stop:
                               HeTrace.WriteLine("Stopped by user", this);
                               HeTrace.RemoveLogger("game");
                               return;
                           case E_Decision.StopAll:
                               HeTrace.WriteLine("Stopped by user", this);
                               HeTrace.RemoveLogger("game");
                               throw new OperationCanceledException("Stopped by user");
                        */
                        case E_Decision.OverWriteAll:
                        case E_Decision.TrashAll:
                            MemorizedDecision = TempDecision;
                            break;
                    }

                    switch (TempDecision)
                    {
                        case E_Decision.Trash:
                        case E_Decision.TrashAll:
                            HeTrace.WriteLine($"Trash existing folder: '{gamePath}'", this);
                            OpFolders.Trash(@gamePath);
                            break;
                    }
                }
            }
            // --- On part du principe que tout peut être overwritté à partir de là.

            // Construction de la structure
            var tree = MakeStructure(gamePath);

            // ---

            #region Original Backup Game - Before all modifications
            if (Config.CreateTBGame)
            {
                XML_Games.TrueBackup(_XMLPlatformFile, shGame.Id, gamePath);
            }
            else
            {
                HeTrace.WriteLine("[Run] Original Backup Game disabled");
            }
            #endregion

            #region Backup without paths
            XML_Games.NPBackup(_XMLPlatformFile, shGame.Id, gamePath);

            #endregion

            // Récupération du jeu
            LBGame lbGame = XML_Games.Scrap_LBGame<LBGame>(_XMLPlatformFile, "ID", shGame.Id);

            // Récupération des clones


            HeTrace.WriteLine("Alarms about not managed field are not important except if it's about a path containing datas");
            HeTrace.WriteLine("EBGames and TBGames don't use a class they copy directly from xml to xml");

            #region Creation of the Infos.xml (on ne récupère que ce que l'on veut)
            if (Config.CreateInfos)
            {
                // --- Get game from Launchbox (on a besoin que jusqu'au game info)
                XML_Custom.Make_InfoGame(gamePath, lbGame);
            }
            else
            {
                HeTrace.WriteLine("[Run] Make info disabled", this);
            }
            #endregion


            // --- Récupération des fichiers
            GameDataCont gdC = new GameDataCont(lbGame.Title, lbGame.Platform);

            GetFiles(lbGame, gdC);

            if (PackMe_IHM.LaunchBoxCore_Prev(gamePath, _ZePlatform, gdC) != true)
                throw new Exception("Stopped by user");

            // --- Prepare files;
            PrepareList(gdC.Applications, tree, Config.KeepGameStruct, "Game");
            PrepareList(gdC.CheatCodes, tree, Config.KeepCheatStruct, "CheatCode");
            PrepareList(gdC.Manuals, tree, Config.KeepManualStruct, "Manual");
            PrepareList(gdC.Musics, tree, Config.KeepMusicStruct, "Music");
            PrepareList(gdC.Videos, tree, true, "Video");
            PrepareImages(gdC.Images, tree.Children[Common.Images].Path);

            // --- Copie des fichiers
            CopyFiles(gdC, tree);

            // --- Récapitulatif permettant de rajouter ou lever des fichiers au pack
            if (PackMe_IHM.LaunchBoxCore_Recap(gamePath, _ZePlatform, gdC) != true)
                throw new Exception("Stopped by user");

            // --- GamePaths --- 
            GamePaths gpX = MakeGamePaths(lbGame, gdC, tree);


            #region Serialization / improved backup of Launchbox datas (with found medias missing) 
            /* - En théorie on est toujours sur du relative path
             * - On a fait un assign sur les dossiers spécifiques
             * - On va récupérer la structure exacte sans aucune interprétation et modifier ce que l'on veut
             *      - Les Paths
             */
            if (Config.CreateEBGame)
            {
                Make_EnhanceBackup(gdC, lbGame, gamePath);
            }
            else
            {
                HeTrace.WriteLine($"[Run] Enhanced Backup Game disabled", this);
            }

            #endregion

            // --- Création d'un fichier conservant les fichiers par défaut définis par l'utilisateur en vue de réutilisation plus tard

            gpX.WriteToJson(Path.Combine(gamePath, "DPGame.json"));


            // --- On complète l'arborescence
            FoncSchem.MakeListFolder(tree.Children[Common.Manuals]);
            FoncSchem.MakeListFolder(tree.Children[Common.Images]);
            FoncSchem.MakeListFolder(tree.Children[Common.Musics]);
            FoncSchem.MakeListFolder(tree.Children[Common.Videos]);

            #region Save Struct
            if (Config.CreateTreeV)
            {
                FoncSchem.MakeStruct(tree, gamePath);
            }
            else
            {
                HeTrace.WriteLine($"[Run] Save Struct disabled", this);
            }
            #endregion

            #region 2020 choix du nom
            string name = PackMe_IHM.AskName(shGame.ExploitableFileName, _SystemPath);


            // Changement de nom du dossier <= Pour le moment ça ne fait que vérifier s'il peut écrire 
            //si un dossier a le même nom ça ne pourra pas le renommer
            ushort i = 0;

            string destFolder = Path.Combine(_SystemPath, name);

            if (!gamePath.Equals(destFolder))
                while (i < 10)
                    try
                    {
                        Directory.Move(gamePath, destFolder);
                        HeTrace.WriteLine("Folder successfully renamed");

                        // Attribution du résultat
                        gamePath = destFolder;

                        // Sortie
                        break;
                    }
                    catch (IOException ioe)
                    {
                        HeTrace.WriteLine($"Try {i}: {ioe}");
                        Thread.Sleep(10);
                        i++;
                    }

            gamePath = destFolder;

            #endregion

            #region Compression
            // Zip
            if (Config.ZipCompression)
            {
                Compress_ZipMode(gamePath);
            }
            else
            {
                HeTrace.WriteLine($"[Run] Zip Compression disabled", this);
            }

            // 7zip
            if (Config.SevZipCompression)
            {
                Compress_7ZipMode(gamePath);
            }
            else
            {
                HeTrace.WriteLine($"[Run] 7Zip Compression disabled", this);
            }
            #endregion

            #region suppression du dossier de travail
            if (SafeBoxes.Dispatch_Mbox(this, "Would you want to ERASE the temp folder", "Erase", E_DxButtons.No | E_DxButtons.Yes, optMessage: shGame.ExploitableFileName) == true)
            {

                // Erase the temp folder
                try
                {
                    Directory.SetCurrentDirectory(_WFolder);
                    Directory.Delete(gamePath, true);
                    HeTrace.WriteLine($"[Run] folder {gamePath} erased", this);

                }
                catch (Exception exc)
                {
                    HeTrace.WriteLine($"[Run] Error when Erasing temp folder {gamePath}\n{exc.Message }", this);
                }
            }
            #endregion

            SetStatus(this, new StateArg($"Finished: {lbGame.Title}", CancelFlag));
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="gpX"></param>
        /// <param name="lbGame"></param>
        /// <param name="gamePath"></param>
        /// <remarks>
        /// Modifie le lbgame
        /// </remarks>
        private void Make_EnhanceBackup(GameDataCont gdC, LBGame lbGame, string gamePath)
        {
            lbGame.ApplicationPath = DxPath.To_RelativeOrNull(Common.Config.HLaunchBoxPath, gdC.DefaultApp?.CurrentPath);
            lbGame.ManualPath = DxPath.To_RelativeOrNull(Common.Config.HLaunchBoxPath, gdC.DefaultManual?.CurrentPath);
            lbGame.MusicPath = DxPath.To_RelativeOrNull(Common.Config.HLaunchBoxPath, gdC.DefaultMusic?.CurrentPath);
            lbGame.VideoPath = DxPath.To_RelativeOrNull(Common.Config.HLaunchBoxPath, gdC.DefaultVideo?.CurrentPath);
            lbGame.ThemeVideoPath = DxPath.To_RelativeOrNull(Common.Config.HLaunchBoxPath, gdC.DefaultThemeVideo?.CurrentPath);

            XML_Games.EnhancedBackup(_XMLPlatformFile, lbGame, gamePath);
        }


        /// <summary>
        /// Création de la structure de base
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private Folder MakeStructure(string root)
        {
            if (!root.Contains(_WFolder))
                throw new Exception($"[CreateFolders] Erreur la chaine '{root}' ne contient pas '{_WFolder}'");

            //ITrace.WriteLine(prefix: false);
            HeTrace.WriteLine("[MakeStructure] Creation of the tree", this);


            Folder tree = new Folder("Root", root);

            // Creation lvl 1
            CreateHFolders(tree,
                            Common.Games,
                            Common.CheatCodes,
                            Common.Images,
                            Common.Manuals,
                            Common.Musics,
                            Common.Videos);

            // Back to system menu to unlock game folder.
            Directory.SetCurrentDirectory(_SystemPath);

            return tree;
        }


        #region Get Files

     

        /// <summary>
        /// Renvoie les possibilités à chercher
        /// </summary>
        /// <param name="gameTitle"></param>
        /// <returns></returns>
        private IEnumerable<string> SplitTitle(string gameTitle)
        {
            List<string> words = new List<string>();
            if (
                gameTitle.StartsWith("The ", StringComparison.OrdinalIgnoreCase) ||
                gameTitle.StartsWith("Les ", StringComparison.OrdinalIgnoreCase)
                )
                gameTitle = gameTitle.Substring(4);
            else if (
                gameTitle.StartsWith("Le ", StringComparison.OrdinalIgnoreCase) ||
                gameTitle.StartsWith("La ", StringComparison.OrdinalIgnoreCase)
                )
                gameTitle = gameTitle.Substring(3);
            else if (gameTitle.StartsWith("A ", StringComparison.OrdinalIgnoreCase))
                gameTitle = gameTitle.Substring(2);


            string tmp = gameTitle
                            /*.Replace(" the ", "|", StringComparison.OrdinalIgnoreCase)
                            .Replace(" of ", "|", StringComparison.OrdinalIgnoreCase)
                            .Replace(" a ", "|", StringComparison.OrdinalIgnoreCase)
                            .Replace(" le ", "|", StringComparison.OrdinalIgnoreCase)
                            .Replace(" la ", "|", StringComparison.OrdinalIgnoreCase)
                            .Replace(" les ", "|", StringComparison.OrdinalIgnoreCase)*/
                            .Replace("  ", "|")
                            .Replace("\'n", "|")
                            .Replace("__", "|")
                            .Replace(',', '|')
                            .Replace(' ', '|')
                            .Replace('_', '|')
                            .Replace('-', '|')
                            .Replace(':', '|')
                            .Replace('\'', '|');

            foreach (string word in tmp.Split('|'))
            {
                if (string.IsNullOrEmpty(word))
                    continue;

                words.Add(word);
            }

            return words;
        }

        private Dictionary<string, string> FindPossibilities(LBGame game, IEnumerable<string> titleElements)
        {
            Dictionary<string, string> possibilities = new Dictionary<string, string>();
            possibilities.Add("id", game.Id);

            int i = 1;
            string sEmpty, sSpace, s8;
            sEmpty = sSpace = s8 = string.Empty;
            foreach (string element in titleElements)
            {
                sEmpty += element;
                sSpace += $"{element} ";
                s8 += $"{element}_";

                if (i == 3)
                {
                    possibilities.Add("3Empty", new string(sEmpty));
                    possibilities.Add("3Space", new string(sSpace).TrimEnd(' '));
                    possibilities.Add("3_", new string(s8).TrimEnd('_'));
                }
                i++;
            }

            possibilities.Add("FullEmpty", sEmpty);
            possibilities.Add("FullSpace", sSpace.TrimEnd(' '));
            possibilities.Add("Full_", s8.TrimEnd('_'));

            return possibilities;
        }




   

        #endregion

        #region Préparation

        private void PrepareList<T>(IEnumerable<T> elems, Folder tree, bool keepStruct, string mediatype) where T : DataTrans
        {
            HeTrace.WriteLine($"[{nameof(PrepareList)}] for {mediatype}", this);
            string destLocation = tree.Children[$"{mediatype}s"].Path;


            // protection
            if (mediatype == "CheatCode" && string.IsNullOrEmpty(Config.HCCodesPath))
                return;



            string folder = null;
            //PlatformFolder folder = null;
            if (mediatype == "Game")
            {
                folder = _ZePlatform.FolderPath ??= Path.Combine("Games", _ZePlatform.Name);
            }
            else if (mediatype == "CheatCode")
            {
                folder = Path.Combine(Config.HCCodesPath, _ZePlatform.Name);
            }
            else if (!string.IsNullOrEmpty(mediatype))
            {
                // On récupère le dossier concerné par le média
                PlatformFolder pFolder = _ZePlatform.PlatformFolders.FirstOrDefault(
                              (x) => x.MediaType.Equals(mediatype, StringComparison.OrdinalIgnoreCase));

                folder = pFolder.FolderPath;
            }

            if (folder.Equals(Config.HLaunchBoxPath) || folder.Equals(AppDomain.CurrentDomain.BaseDirectory))
                throw new Exception($"Error on target folder '{folder}'");


            folder = Path.GetFullPath(folder, Config.HLaunchBoxPath);
            foreach (var fichier in elems)
                PrepareFile(fichier, destLocation, keepStruct, folder);

        }

        #region
        /* private /*string*//* void PrepareList(List<DataRep> elems, Folder tree, bool keepStruct, string mediatype)
         {
             HeTrace.WriteLine($"[{nameof(PrepareList)}] for {mediatype}", this);

             string destLocation = tree.Children[$"{mediatype}s"].Path;

             PlatformFolder folder = null;
             if (keepStruct && !string.IsNullOrEmpty(mediatype))
             {
                 // On récupère le dossier concerné par le média
                 folder = _ZePlatform.PlatformFolders.FirstOrDefault(
                             (x) => x.MediaType.Equals(mediatype, StringComparison.OrdinalIgnoreCase));
             }

             //string futurLink, tail, gpAssign;
             //gpAssign = string.Empty;
             foreach (DataRep fichier in elems)
             {

                 PrepareFile(fichier, destLocation, keepStruct, folder);
                 // --- Renvoie pour le GP.
               /*  if (fichier.IsSelected)
                     gpAssign = DxPath.To_Relative(destLocation, futurLink);*/
        /* }

         //return gpAssign;
     }*/
        #endregion

        private void PrepareFile(DataTrans fichier, string destLocation, bool keepStruct, string foldertoReplace /*PlatformFolder folder*/)
        {
            string futurLink = string.Empty;
            //tail = string.Empty;

            if (keepStruct && !string.IsNullOrEmpty(foldertoReplace) /*!= null && fichier.CurrentPath.Contains(folder.FolderPath)*/)
            {
                string tail = fichier.CurrentPath.Replace(foldertoReplace /*folder.FolderPath*/, string.Empty).TrimStart('\\');
                futurLink = Path.Combine(destLocation, tail);
            }
            else
            {
                futurLink = Path.Combine(destLocation, Path.GetFileName(fichier.CurrentPath));
            }

            fichier.DestPath = futurLink;
        }



        private void PrepareImages(List<DataRepExt> images, string destLocation)
        {
            E_Decision resMem = E_Decision.None;
            string tail;
            foreach (DataRepExt pkFile in images)
            {
                tail = string.Empty;
                // On récupère la tail
                PlatformFolder pFolder = _ZePlatform.PlatformFolders.First(
                                          (x) => x.MediaType.Equals(pkFile.Categorie, StringComparison.OrdinalIgnoreCase));

                string toReplace = Path.GetFullPath(pFolder.FolderPath, Common.Config.HLaunchBoxPath);
                if (toReplace != null && pkFile.CurrentPath.Contains(toReplace))
                {
                    tail = pkFile.CurrentPath.Replace(toReplace, string.Empty).TrimStart('\\');

                    pkFile.DestPath = Path.Combine(destLocation, pkFile.Categorie, tail);
                }
                else
                {
                    pkFile.DestPath = Path.Combine(destLocation, Path.GetFileName(pkFile.CurrentPath));
                }
            }
        }

        #endregion

        #region copy
        private GamePaths MakeGamePaths(LBGame lbGame, GameDataCont gdC, Folder tree)
        {
            GamePaths gpX = GamePaths.CreateBasic(lbGame);

            //  gpX.ApplicationPath = AssignDefaultPath(tree.Children[Common.Games].Path, gdC.DefaultApp);
            gpX.SetApplications = gdC.Applications.Select(x =>
                                        new DataPlus()
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            CurrentPath = x.Name,
                                            IsSelected = x.IsSelected,
                                        });

            gpX.ManualPath = AssignDefaultPath(tree.Children[Common.Manuals].Path, gdC.DefaultManual);
            gpX.MusicPath = AssignDefaultPath(tree.Children[Common.Musics].Path, gdC.DefaultMusic);
            gpX.VideoPath = AssignDefaultPath(tree.Children[Common.Videos].Path, gdC.DefaultVideo);
            gpX.ThemeVideoPath = AssignDefaultPath(tree.Children[Common.Videos].Path, gdC.DefaultThemeVideo);
            return gpX;
        }


        private string AssignDefaultPath(string destPath, DataRep defaultPath)
        {
            if (defaultPath == null)
                return null;

            return DxPath.To_Relative(destPath, defaultPath.DestPath); ;

        }

        // ---

        private void CopyFiles(GameDataCont gdC, Folder tree)
        {
            HashCopy objCopy = new HashCopy();
            objCopy.AskToUser += PackMe_IHM.Ask4_FileConflict2;

            HeTrace.WriteLine("[CopyFiles] All files except images");
            // Fusion des fichiers sauf les images
            List<DataTrans> Fichiers = new List<DataTrans>();
            Fichiers.AddRange(gdC.Applications);
            Fichiers.AddRange(gdC.CheatCodes);
            Fichiers.AddRange(gdC.Manuals);
            Fichiers.AddRange(gdC.Musics);
            Fichiers.AddRange(gdC.Videos);

            PackMe_IHM.DoubleProgress(objCopy, "Copy",
                (test) => test = objCopy.CopySevNVerif(Fichiers),
                (test) => test = objCopy.CopySevNVerif(gdC.Images));
            //copyObj.CopySNVerif(Fichiers);

        }
        #endregion


        #region tree
        /// <summary>
        /// Create Horizontal arborescence
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="folders"></param>
        private void CreateHFolders(Folder tree, params string[] folders)
        {
            foreach (string folderName in folders)
            {
                string folderPath = Path.Combine(tree.Path, folderName);
                HeTrace.WriteLine($"\t[CreateHFolders] Creation of '{folderPath}'", this);

                Folder tmp = new Folder(folderName, folderPath);
                tree.Children.Add(folderName, tmp);

                // Création
                Directory.CreateDirectory(folderPath);
            }
        }

        /*

        /// <summary>
        /// Créer des dossiers à la verticale dans un répertoire, ajoute au dictionnaire
        /// </summary>
        /// <param name="basePath">Starting Directory</param>
        /// <param name="folders"></param>
        void CreateVFolders(Folder basePath, string tail)
        {
            string[] arrTail = tail.Split('\\');

            string path = basePath.Path; // 22/10/2020
            foreach (string name in arrTail)
            {
                path = Path.Combine(path, name);

                HeTrace.WriteLine($"[CreateFolders] Creation of the folder: '{path}'");

                // Creation
                Directory.CreateDirectory(path);
            }


        }*/
        #endregion tree




        #region Compression & Hash
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gamePath"></param>
        /// <param name="title">Tite du jeu</param>
        private void Compress_ZipMode(string gamePath)
        {
            var archiveLink = Path.Combine(_SystemPath, $"{Path.GetFileName(gamePath)}.zip");
            if (File.Exists(archiveLink))
            {
                var Length = DxLocalTransf.Tools.FileSizeFormatter.Convert(new FileInfo(archiveLink).Length);
                var resConflict = SafeBoxes.Ask4_DestConflict
                    (
                        LanguageManager.Instance.Lang.File_Ex, archiveLink, Length,
                        E_DxConfB.OverWrite | E_DxConfB.Trash
                    );

                switch (resConflict)
                {
                    case E_Decision.OverWriteAll:
                    case E_Decision.OverWrite:
                        File.Delete(archiveLink);
                        break;
                    case E_Decision.Trash:
                    case E_Decision.TrashAll:
                        OpDFiles.Trash(archiveLink);
                        break;
                }
            }


            ZipCompression zippy = new ZipCompression(_SystemPath)
            {
                IsPaused = this.IsPaused,
                TokenSource = this.TokenSource,
            };
            /*
            zippy.UpdateProgressT += this.SetProgress;
            zippy.UpdateStatus += this.SetStatus;
            zippy.MaximumProgressT += this.SetMaximum;*/
            zippy.UpdateStatus += (x, y) => HeTrace.WriteLine(y.Message);



            var res = PackMe_IHM.ZipCompressFolder(zippy, () => zippy.CompressFolder(
                                         gamePath, Path.GetFileName(gamePath), Config.ZipLvlCompression), "Compression Zip");

            //ZipCompression.CompressFolder(gamePath, Path.Combine(_SystemPath, shGame.ExploitableFileName), PS.Default.c7zCompLvl);
            if (res != true)
                return;

            #region Création du fichier  MD5
            if (Config.CreateMD5)
            {
                Gen_PlusCalculator calculator = Gen_PlusCalculator.Create(CancelToken);

                string sum = string.Empty;
                PackMe_IHM.LaunchOpProgress(calculator,
                        () => sum = calculator.Calcul(zippy.ArchiveLink, () => MD5.Create()),
                        "Calcul de somme");

                HashCalc.Files.ClassicParser.Write(zippy.ArchiveLink, sum, HashType.md5, overwrite: true);
            }
            else
            {
                HeTrace.WriteLine($"[Run] MD5 disabled", this);
            }
            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gamePath"></param>
        /// <param name="title">Titre du jeu</param>
        private void Compress_7ZipMode(string gamePath)
        {
            var archiveLink = Path.Combine(_SystemPath, $"{Path.GetFileName(gamePath)}.7z");
            if (File.Exists(archiveLink))
            {
                var Length = DxLocalTransf.Tools.FileSizeFormatter.Convert(new FileInfo(archiveLink).Length);
                var resConflict = SafeBoxes.Ask4_DestConflict
                    (
                        LanguageManager.Instance.Lang.File_Ex, archiveLink, Length,
                        E_DxConfB.OverWrite | E_DxConfB.Trash
                    );

                switch (resConflict)
                {
                    case E_Decision.OverWriteAll:
                    case E_Decision.OverWrite:
                        File.Delete(archiveLink);
                        break;
                    case E_Decision.Trash:
                    case E_Decision.TrashAll:
                        OpDFiles.Trash(archiveLink);
                        break;
                }
            }

            SevenZipCompression sevZippy = new SevenZipCompression(_SystemPath)
            {
                IsPaused = this.IsPaused,
                TokenSource = this.TokenSource
            };
            sevZippy.UpdateStatus += (x, y) => HeTrace.WriteLine(y.Message);


            var res = (bool?)PackMe_IHM.ZipCompressFolder(sevZippy, () => sevZippy.CompressFolder(
                                    gamePath, Path.GetFileName(gamePath), Config.SevZipLvlCompression), "Compression 7z");

            if (res != true)
                return;

            #region Création du fichier  MD5
            if (Config.CreateMD5)
            {
                Gen_PlusCalculator calculator = Gen_PlusCalculator.Create(CancelToken);

                string sum = string.Empty;
                PackMe_IHM.LaunchOpProgress(calculator,
                                    () => sum = calculator.Calcul(sevZippy.ArchiveLink, () => MD5.Create()),
                                    "Calcul de somme");

                HashCalc.Files.ClassicParser.Write(sevZippy.ArchiveLink, sum, HashType.md5, overwrite: true);

            }
            else
            {
                HeTrace.WriteLine($"[Run] MD5 disabled", this);
            }
            #endregion
        }

        #endregion



        public void StopTask()
        {
            throw new NotImplementedException();
        }

        public void Pause(int timeSleep)
        {
            throw new NotImplementedException();
        }
    }
}

