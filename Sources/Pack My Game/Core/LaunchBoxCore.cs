using System;
using System.Collections.Generic;
using System.IO;
using PS = Pack_My_Game.Properties.Settings;
using Hermes.Messengers;
using Hermes;
using DxLocalTransf;
using DxTBoxCore.Box_Decisions;
using DxTBoxCore.Box_Progress;
using System.Threading;
using Pack_My_Game.IHM;
using System.Collections.ObjectModel;
using DxTBoxCore.Common;
using Common_PMG.Container.Game;
using LaunchBox_XML.XML;
using System.Windows;
using Pack_My_Game.Cont;
using Pack_My_Game.Files;
using System.Security.Cryptography;
using Common_PMG.Container;
using System.Linq;
using Common_PMG.Container.AAPP;
using Pack_My_Game.Models;
using Pack_My_Game.Compression;
using HashCalc;
using DxLocalTransf.Progress.ToImp;
using DxTBoxCore.MBox;
using DxPaths.Windows;
//using Pack_My_Game.Compression;

namespace Pack_My_Game.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Dans la mesure du possible on n'utilise pas LBGame, on va copier directement le xml
    /// </remarks>
    partial class LaunchBoxCore : IASBaseC
    {
        public delegate object SignalBoxHandler(object sender, params string[] parameters);
        public delegate void SignalRecapHandler(object sender, string destination, string title, Platform platform);
        public delegate E_Decision AskForConflict(object sender, string source, string destination, E_DxConfB buttons = E_DxConfB.All);
        public delegate string[] GetFilesHandler(object sender, List<string> location, string mediatype);

        //public static event SignalBoxHandler Mbox;
        //public static event SignalBoxHandler AskName;
        //public static event AskForConflict FileConflict;
        //public static event GetFilesHandler ValidateFiles;
        //public static event SignalRecapHandler Recap;

        public event DoubleHandler UpdateProgressT;
        public event MessageHandler UpdateStatusT;
        public event DoubleHandler MaximumProgressT;
        public event DoubleHandler UpdateProgress;
        public event MessageHandler UpdateStatus;
        public event DoubleHandler MaximumProgress;

        // ---

        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();
        public CancellationToken CancelToken { get; }

        public bool IsPaused { get; set; }

        /// <summary>
        /// Objet gérant les vérifications
        /// </summary>
        OpDFilesExt _ObjectFiles { get; }

        // ---

        /// <summary>
        /// Liste des jeux
        /// </summary>
        public static ObservableCollection<ShortGame> _SelectedGames { get; private set; }

        /// <summary>
        /// Object containing folders
        /// </summary>
        Platform _ZePlatform { get; }


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

            W_Games gamesWindow = new W_Games();
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

            CancelToken = TokenSource.Token;

            //_PlatformName = platformName;

            #region Initialisation des chemins
            _WFolder = PS.Default.OutPPath;
            // Chemin du dossier temporaire du system
            _SystemPath = Path.Combine(_WFolder, platformName);

            #endregion

            _XMLPlatformFile = Path.Combine(PS.Default.LBPath, PS.Default.dPlatforms, $"{platformName}.xml");
            _ZePlatform = XML_Platforms.GetPlatformPaths(Path.Combine(PS.Default.LBPath, PS.Default.fPlatforms), platformName, PS.Default.LBPath);


            #region Messages
            MeDebug mdb = new MeDebug()
            {
                ByPass = true,

            };
            HeTrace.AddMessenger("Debug", mdb);

            XML_Games.Signal += (x, y) => HeTrace.WriteLine(y, "Xml_Games");
            Gen_Calculator.StaticUpdateProgress += (x, y) => UpdateProgress(x, y);
            Gen_PlusCalculator.StaticUpdateProgress += (x, y) => UpdateProgress(x, y);
            M_PackMeRes.UpdateStatus += (x, y) => this.UpdateStatus(x, y);
            M_PackMeRes.UpdateProgress += (x, y) => this.UpdateProgress(x, y);
            M_PackMeRes.MaximumProgress += (x, y) => this.MaximumProgress(x, y);

            #endregion


            #region Initialisation de l'objet de copie
            _ObjectFiles = new OpDFilesExt();
            _ObjectFiles.SignalProgression += (x, y) => UpdateProgress(x, y);
            #endregion
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

                PackMe(zeGame);
                //2020 PackMe pm = new PackMe(zeGame.ID, platform);

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

            // Système de log par jeu
            if (PS.Default.opLogFile)
            {
                MeSimpleLog gameLog = new MeSimpleLog(Path.Combine(_WFolder, $"{_ZePlatform.Name} - {shGame.ExploitableFileName}.log"))
                {


                };
                gameLog.AddCaller(this);
                HeTrace.AddLogger("game", gameLog);
            }

            try
            {

                // Dossiers
                string gamePath = Path.Combine(_SystemPath, $"{shGame.ExploitableFileName}");             // New Working Folder

                // Contrôle de collisions pour les dossiers
                if (Directory.Exists(gamePath))
                {
                    HeTrace.WriteLine($"Directory Exists '{gamePath}'", this);
                    // Demande à l'utilisateur si aucune précédente
                    if (MemorizedDecision == E_Decision.None)
                    {
                        Application.Current.Dispatcher?.Invoke(() =>
                            TempDecision = MBDecision.ShowDial(gamePath, null, Common.ObjectLang.FolderEEXp, E_DxConfB.Trash | E_DxConfB.OverWrite));

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
                if (PS.Default.opOBGame)
                {
                    XML_Games.TrueBackup(_XMLPlatformFile, shGame.Id, gamePath);
                }
                else
                {
                    HeTrace.WriteLine("[Run] Original Backup Game disabled");
                }
                #endregion


                // Récupération du jeu
                LBGame lbGame = XML_Games.Scrap_LBGame<LBGame>(_XMLPlatformFile, "ID", shGame.Id);
                HeTrace.WriteLine("Alarms about not managed field are not important except if it's about a path containing datas");
                HeTrace.WriteLine("EBGames and TBGames don't use a class they copy directly from xml to xml");

                #region Creation of the Infos.xml (on ne récupère que ce que l'on veut)
                if (PS.Default.opInfos)
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
                GamePathsExt gpX = GamePathsExt.CreateBasic(lbGame);

                GetFiles(lbGame, gpX);

                // --- Copie des fichiers
                CopyFiles(gpX, tree);

                //CopyFiles(lbGame, tree);

                // --- Récapitulatif permettant de rajouter ou lever des fichiers au pack

                PackMe_IHM.LaunchBoxCore_Recap(gamePath, _ZePlatform, gpX);

                #region Serialization / improved backup of Launchbox datas (with found medias missing) 
                /* - En théorie on est toujours sur du relative path
                 * - On a fait un assign sur les dossiers spécifiques
                 * - On va récupérer la structure exacte sans aucune interprétation et modifier ce que l'on veut
                 *      - Les Paths
                 */
                if (PS.Default.opEBGame)
                {
                    Make_EnhanceBackup(gpX, lbGame, gamePath);
                    
                }
                else
                {
                    HeTrace.WriteLine($"[Run] Enhanced Backup Game disabled", this);
                }

                #endregion

                // --- Création d'un fichier conservant les fichiers par défaut définis par l'utilisateur en vue de réutilisation plus tard

                //                ManageDefaultFiles(lbGame, gamePath, tree);
                gpX.WriteToJson(Path.Combine(gamePath, "DPGame.json"));


                // --- On complète l'arborescence
                FoncSchem.MakeListFolder(tree.Children[Common.Manuals]);
                FoncSchem.MakeListFolder(tree.Children[Common.Images]);
                FoncSchem.MakeListFolder(tree.Children[Common.Musics]);
                FoncSchem.MakeListFolder(tree.Children[Common.Videos]);

                #region Save Struct
                if (PS.Default.opTreeV)
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

                //string destArchLink = Path.Combine(path, $"{destArchive}");
                //string destArchLink = Path.Combine(path, gnWindows.ChoosenGameName);

                // On verra si on dissocie un jour 
                //shGame.ExploitableFileName = destFolder;
                #endregion

                #region Compression
                // Zip
                if (PS.Default.opZip)
                {
                    Compress_ZipMode(gamePath, shGame.Title);
                }
                {
                    HeTrace.WriteLine($"[Run] Zip Compression disabled", this);
                }

                // 7zip
                if (PS.Default.op7_Zip)
                {
                    Compress_7ZipMode(gamePath, shGame.Title);
                }
                else
                {
                    HeTrace.WriteLine($"[Run] 7Zip Compression disabled", this);
                }
                #endregion



                #region suppression du dossier de travail
                if (PackMe_IHM.Dispatch_Mbox(this, "Would you want to ERASE the temp folder", "Erase", E_DxButtons.No | E_DxButtons.Yes, optMessage: shGame.ExploitableFileName) == true)
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

                UpdateStatus?.Invoke(this, $"Finished: {lbGame.Title}");
            }
            catch (Exception exc)
            {
                HeTrace.WriteLine(exc.Message, this);
                PackMe_IHM.Dispatch_Mbox(this, exc.Message, "Error", E_DxButtons.Ok);
            }
            finally
            {
                HeTrace.RemoveLogger("game");

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="gpX"></param>
        /// <param name="lbGame"></param>
        /// <param name="gamePath"></param>
        private void Make_EnhanceBackup(GamePathsExt gpX, LBGame lbGame, string gamePath)
        {
            if (string.IsNullOrEmpty(lbGame.ManualPath))
                lbGame.ManualPath = gpX.CompManuals[0];                

            if (string.IsNullOrEmpty(lbGame.MusicPath))
                lbGame.MusicPath = gpX.CompMusics[0];

            if (string.IsNullOrEmpty(lbGame.VideoPath))
                lbGame.VideoPath = gpX.CompVideos.FirstOrDefault(x => !x.Contains("Theme", StringComparison.OrdinalIgnoreCase));

            if(string.IsNullOrEmpty(lbGame.ThemeVideoPath))
                lbGame.ThemeVideoPath = gpX.CompVideos.FirstOrDefault(x => x.Contains("Theme", StringComparison.OrdinalIgnoreCase));

            lbGame.ApplicationPath = DxPath.To_Relative(PS.Default.LBPath, lbGame.ApplicationPath);
            lbGame.ManualPath = DxPath.To_Relative(PS.Default.LBPath, lbGame.ManualPath);
            lbGame.MusicPath = DxPath.To_Relative(PS.Default.LBPath, lbGame.MusicPath);
            lbGame.VideoPath = DxPath.To_Relative(PS.Default.LBPath, lbGame.VideoPath);
            lbGame.ThemeVideoPath = DxPath.To_Relative(PS.Default.LBPath, lbGame.ThemeVideoPath);

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

        /// <summary>
        /// Récupère tous les fichiers en fonction du LBGame, ainsi que des dossiers de la plateforme
        /// </summary>
        /// <param name="lbGame"></param>
        /// <param name="gpX"></param>
        private void GetFiles(LBGame lbGame, GamePathsExt gpX)
        {
            // 2021 - Formatage de la chaine pour éviter les erreurs
            string toSearch = lbGame.Title.Replace(':', '_').Replace('\'', '_').Replace("__", "_");

            gpX.ApplicationPath = GetFilesForGames(lbGame.ApplicationPath, gpX.CompApps);

            // CheatCodes
            GetCheatCodes(toSearch, gpX.CompCheatCodes);

            // Manuels
            gpX.ManualPath = GetFileForSpecifics(lbGame.ManualPath);
            GetMoreFiles(toSearch, gpX.CompManuals, "Manual", gpX.ManualPath);

            // Musics 
            gpX.MusicPath = GetFileForSpecifics(lbGame.MusicPath);
            GetMoreFiles(toSearch, gpX.CompMusics, "Music", gpX.MusicPath);

            // Videos
            gpX.VideoPath = GetFileForSpecifics(lbGame.VideoPath);
            gpX.ThemeVideoPath = GetFileForSpecifics(lbGame.ThemeVideoPath);
            GetMoreFiles(toSearch, gpX.CompVideos, "Video", gpX.VideoPath, gpX.ThemeVideoPath);

            // Clones
            GetClones(lbGame, gpX.CompApps);

            // Images
            GetImagesFiles(lbGame, toSearch, gpX.Images);


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationPath"></param>
        /// <param name="applications"></param>
        /// <returns>Le fichier sélectionné</returns>
        private string GetFilesForGames(string applicationPath, List<string> applications)
        {
            if (string.IsNullOrEmpty(applicationPath))
                throw new ArgumentNullException("[GetFiles] Folder of application path is empty");

            string extension = Path.GetExtension(applicationPath);

            // si aucune extension on ne pack pas le fichier
            if (string.IsNullOrEmpty(extension))
                throw new ArgumentNullException("[GetFiles] Extension of application is null");


            // --- Cas des extensions cue
            if (extension.Equals(".cue", StringComparison.OrdinalIgnoreCase))
            {
                string srcFile = Path.GetFullPath(applicationPath, PS.Default.LBPath);

                //Lecture du fichier cue
                Cue_Scrapper cuecont = new Cue_Scrapper(srcFile);

                //Folder containing files
                string sourceFold = Path.GetDirectoryName(srcFile);

                // Fonctionne avec le nom du fichier
                foreach (string fileName in cuecont.Files)
                {
                    // Donne le lien complet vers le fichier
                    GamePathsExt.AddWVerif(Path.Combine(sourceFold, fileName), applications);
                }
            }


            return Path.GetFullPath(applicationPath, PS.Default.LBPath);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toSearch"></param>
        /// <param name="compCheatCodes"></param>
        private void GetCheatCodes(string toSearch, List<string> compCheatCodes)
        {
            string CCodesDir = Path.Combine(PS.Default.CCodesPath, _ZePlatform.Name);
            if (!Directory.Exists(CCodesDir))
            {
                this.UpdateStatus?.Invoke(this, $"Directory doesn't exist: '{CCodesDir}'");
                return;
            }

            compCheatCodes = GetFilesByPredict(toSearch, CCodesDir, "CheatCodes");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbGamePath"></param>
        /// <returns>
        /// Vérifie et retourne le fichier sélectionné
        /// </returns>
        private string GetFileForSpecifics(string dbGamePath)
        {
            // Si le fichier indiqué dans le fichier du jeu existe
            if (!string.IsNullOrEmpty(dbGamePath))
            {
                string linkFile = Path.GetFullPath(dbGamePath, PS.Default.LBPath);
                if (File.Exists(linkFile))
                {
                    return linkFile;
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lbGame"></param>
        /// <param name="applications"></param>
        /// <remarks>
        /// Test l'existance avant
        /// </remarks>
        private void GetClones(LBGame lbGame, List<string> applications)
        {
            List<Clone> clones = XML_Games.ListClones(_XMLPlatformFile, "GameID", lbGame.Id).ToList();

            // tri des doublons / filter duplicates
            List<Clone> fClones = FilesFunc.DistinctClones(clones, lbGame.ApplicationPath, PS.Default.LBPath);

            foreach (Clone c in fClones)
            {
                string path = Path.GetFullPath(c.ApplicationPath, PS.Default.LBPath);

                if (File.Exists(path))
                    GamePathsExt.AddWVerif(path, applications);
            }
        }

        private void GetImagesFiles(LBGame lbGame, string toSearch, List<PackFile> images)
        {
            //Queue<string> lPackFile = new Queue<string>();

            foreach (PlatformFolder plfmFolder in _ZePlatform.PlatformFolders)
            {
                //filtre sur tout ce qui n'est pas image
                switch (plfmFolder.MediaType)
                {
                    case "Manual":
                    case "Music":
                    case "Theme Video":
                    case "Video":
                        continue;
                }

                // Liste du contenu des dossiers
                foreach (var fichier in Directory.EnumerateFiles(plfmFolder.FolderPath, "*.*", SearchOption.AllDirectories))
                {
                    string fileName = Path.GetFileName(fichier);

                    if (
                        !fileName.StartsWith($"{toSearch}-") &&
                        !fileName.StartsWith($"{ lbGame.Title}.{ lbGame.Id}-"))
                        continue;

                    HeTrace.WriteLine($"\t\t[GetImages] Found '{fichier}' in '{plfmFolder.FolderPath}'");

                    PackFile tmp = new PackFile(plfmFolder.MediaType, fichier);

                    //   lPackFile.Enqueue(fichier);
                    images.Add(tmp);
                }
            }
        }


        private void GetMoreFiles(string toSearch, List<string> files, string mediatype, params string[] fields)
        {
            // On récupère le dossier concerné par le média
            PlatformFolder folder = _ZePlatform.PlatformFolders.First(
                                            (x) => x.MediaType.Equals(mediatype, StringComparison.OrdinalIgnoreCase));

            List<string> filteredFiles = GetFilesByPredict(toSearch, folder.FolderPath, mediatype);

            foreach (string f in filteredFiles)
            {
                // Evite de prendre en double ceux qui sont sélectionnés
                if (fields.Contains(f))
                    continue;

                HeTrace.WriteLine($"[GetMoreFiles] found: '{f}'", this);
                GamePathsExt.AddWVerif(f, files);
            }
        }


        #region copies
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gpX"></param>
        /// <param name="tree"></param>
        /// <remarks>
        /// Altère GPX pour suivre les fichiers
        /// </remarks>
        private void CopyFiles(GamePathsExt gpX, Folder tree)
        {
            // Roms + Clones
            gpX.ApplicationPath = CopyMain(gpX.ApplicationPath, tree.Children[Common.Games].Path, false);
            CopyList(gpX.CompApps, tree.Children[Common.Games].Path, false);

            // CheatCodes
            CopyList(gpX.CompCheatCodes, tree.Children[Common.CheatCodes].Path, false);

            // Manuals
            gpX.ManualPath = CopyMain(gpX.ManualPath, tree.Children[Common.Manuals].Path, true, "Manual");
            CopyList(gpX.CompManuals, tree.Children[Common.Manuals].Path, true, "Manual");

            // Musics
            gpX.MusicPath = CopyMain(gpX.MusicPath, tree.Children[Common.Musics].Path, true, "Music");
            CopyList(gpX.CompMusics, tree.Children[Common.Musics].Path, true, "Music");

            // Videos
            gpX.VideoPath = CopyMain(gpX.VideoPath, tree.Children[Common.Videos].Path, true, "Video");
            gpX.ThemeVideoPath = CopyMain(gpX.ThemeVideoPath, tree.Children[Common.Videos].Path, true, "Video");
            CopyList(gpX.CompVideos, tree.Children[Common.Videos].Path, true, "Video");

            // Images
            CopyImages(gpX.Images, tree.Children[Common.Images].Path);
        }

        /*       /// <summary>
               /// 
               /// </summary>
               /// <param name="path"></param>
               /// <param name="compApps"></param>
               /// <param name="destLocation"></param>
               /// <returns></returns>
               /// <remarks>
               /// On ne garde pas le tail
               /// </remarks>
               private string CopyMain(string path, string destLocation)
               {
                   string futurLink = Path.Combine(destLocation, Path.GetFileName(path));

                   SimpleCopyManager(path, ref _FileConflictDecision, futurLink);

                   return DxPath.To_Relative(destLocation, futurLink);
               }*/

        private string CopyMain(string fichier, string destLocation, bool wTail, string mediatype = null)
        {
            if (string.IsNullOrEmpty(fichier))
                return string.Empty;

            PlatformFolder folder = null;
            if (wTail && !string.IsNullOrEmpty(mediatype))
            {
                // On récupère le dossier concerné par le média
                folder = _ZePlatform.PlatformFolders.First(
                            (x) => x.MediaType.Equals(mediatype, StringComparison.OrdinalIgnoreCase));
            }

            string futurLink, tail;
            futurLink = tail = string.Empty;

            if (folder != null && fichier.Contains(folder.FolderPath))
            {
                tail = fichier.Replace(folder.FolderPath, string.Empty).TrimStart('\\');
                futurLink = Path.Combine(destLocation, tail);
            }
            else
            {
                futurLink = Path.Combine(destLocation, Path.GetFileName(fichier));
            }

            SimpleCopyManager(fichier, ref _FileConflictDecision, futurLink);

            return DxPath.To_Relative(destLocation, futurLink);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="compApps"></param>
        /// <param name="folder"></param>
        /// <remarks>
        /// Conserve la structure si c'est souhaité
        /// </remarks>
        private void CopyList(List<string> elems, string destLocation, bool wTail, string mediatype = null)
        {
            PlatformFolder folder = null;
            if (wTail && !string.IsNullOrEmpty(mediatype))
            {
                // On récupère le dossier concerné par le média
                folder = _ZePlatform.PlatformFolders.First(
                            (x) => x.MediaType.Equals(mediatype, StringComparison.OrdinalIgnoreCase));
            }

            string tail, futurLink;
            foreach (string fichier in elems)
            {
                futurLink = string.Empty;
                tail = string.Empty;

                if (wTail)
                {
                    if (folder != null && fichier.Contains(folder.FolderPath))
                    {
                        tail = fichier.Replace(folder.FolderPath, string.Empty).TrimStart('\\');
                        futurLink = Path.Combine(destLocation, tail);

                        SimpleCopyManager(fichier, ref _FileConflictDecision, futurLink);
                        continue;
                    }
                }

                futurLink = Path.Combine(destLocation, Path.GetFileName(fichier));
                SimpleCopyManager(fichier, ref _FileConflictDecision, futurLink);
            }
        }

        private void CopyImages(List<PackFile> images, string destLocation)
        {
            E_Decision resMem = E_Decision.None;
            string tail, futurLink;
            foreach (PackFile pkFile in images)
            {
                tail = string.Empty;
                // On récupère la tail
                PlatformFolder folder = _ZePlatform.PlatformFolders.First(
                                          (x) => x.MediaType.Equals(pkFile.Categorie, StringComparison.OrdinalIgnoreCase));


                if (folder != null && pkFile.LinkToThePath.Contains(folder.FolderPath))
                {
                    tail = pkFile.LinkToThePath.Replace(folder.FolderPath, string.Empty).TrimStart('\\');

                    futurLink = Path.Combine(destLocation, pkFile.Categorie, tail);
                }
                else
                {
                    futurLink = Path.Combine(destLocation, Path.GetFileName(pkFile.LinkToThePath));
                }

                SimpleCopyManager(pkFile.LinkToThePath, ref resMem, futurLink);

                // Dossier de destination
                //25/03/2021string destFolder = Path.Combine(_Tree.Children[nameof(SubFolder.Images)].Path, tail1);
                //string destFolder = Path.Combine(imgsFolder, tail1);

                //SimpleCopyManager(pkFile.LinkToThePath, destFolder, ref resMem);
            }
        }





        #endregion





        /// <summary>
        /// Récupère les fichiers par "prédiction"
        /// </summary>
        internal List<string> GetFilesByPredict(string toSearch, string folder, string mediatype)
        {

            // array of files with a part of the name
            string[] files = Directory.GetFiles(Path.GetFullPath(folder, PS.Default.LBPath), $"{toSearch}*.*", SearchOption.AllDirectories);

            #region processing on files found
            // bypass - 
            string[] bypass = new string[] { "-", " -" };

            List<string> filteredFiles = new List<string>();
            foreach (string fichier in files)
            {
                string filename = Path.GetFileNameWithoutExtension(fichier);
                // Test if total match
                if (filename.Equals(toSearch))
                {
                    // On ajoute à la liste des checkbox
                    filteredFiles.Add(fichier);
                }
                //Test if match with bypass
                else
                {
                    // On test avec chaque bypass
                    foreach (var b in bypass)
                    {
                        if (filename.StartsWith(toSearch + b))
                        {
                            // On ajoute à la liste des checkbox
                            filteredFiles.Add(fichier);
                            break;
                        }
                    }
                }
            }

            if (filteredFiles.Count <= 0)
            {
                PackMe_IHM.Dispatch_Mbox(this, $"Searching for { mediatype} returned 0 result", $"Searching for { mediatype}", E_DxButtons.Ok);
            }
            else
            {
                //filteredFiles = PackMe_IHM.Validate_FilesFound(filteredFiles, mediatype).ToList();
            }

            return filteredFiles;
        }

        internal void SimpleCopyManager(string fileSrc, ref E_Decision previousDec, string destFile)
        {
            if (CancelToken.IsCancellationRequested)
                throw new OperationCanceledException("Operation stopped");

            if (!destFile.Contains(_WFolder))
                throw new Exception($"[CreateFolders] Erreur la chaine '{destFile}' ne contient pas '{_WFolder}'");


            // Création des dossiers
            string destFolder = Path.GetDirectoryName(destFile);
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);

            //string destFile = Path.Combine(destFolder, Path.GetFileName(fileSrc));

            E_Decision? conflictDec = previousDec;

            bool overwrite = false;
            if (File.Exists(destFile))
            {
                HeTrace.Write($"[CopyHandler] Destination file exists... ", this);
                if (conflictDec == E_Decision.None)
                {
                    conflictDec = PackMe_IHM.Ask4_FileConflict(fileSrc, destFile, E_DxConfB.OverWrite | E_DxConfB.Pass | E_DxConfB.Trash);

                    // Mémorisation pour les futurs conflits
                    switch (conflictDec)
                    {
                        case E_Decision.PassAll:
                        case E_Decision.OverWriteAll:
                        case E_Decision.TrashAll:
                            previousDec = conflictDec == null ? E_Decision.None : (E_Decision)conflictDec;
                            break;
                    }
                }

                HeTrace.EndLine(conflictDec.ToString(), this);
                switch (conflictDec)
                {
                    case E_Decision.Pass:
                    case E_Decision.PassAll:
                        this.UpdateStatus(this, $"Pass: {fileSrc}");
                        return;
                    case E_Decision.OverWrite:
                    case E_Decision.OverWriteAll:
                        this.UpdateStatus(this, $"OverWrite: {destFile}");
                        overwrite = true;
                        break;
                    case E_Decision.Trash:
                    case E_Decision.TrashAll:
                        this.UpdateStatus(this, $"Trash: {destFile}");
                        OpDFiles.Trash(destFile);
                        break;
                }
            }

            // --- Copie
            this.UpdateStatus(this, $"Copy {fileSrc}");
            this.UpdateProgress?.Invoke(this, 0);
            this.MaximumProgress?.Invoke(this, 1);
            FilesFunc.Copy(fileSrc, destFile, overwrite);
            this.UpdateProgress?.Invoke(this, 1);

            // --- Vérification des sommes
            this.UpdateStatus(this, $"Copy verification");
            this.MaximumProgress?.Invoke(this, 100);

            //bool? res = _ObjectFiles.VerifByHash_Sync(fileSrc, destFile, () => MD5.Create());
            var res = _ObjectFiles.DeepVerif(fileSrc, destFile, () => MD5.Create());

            this.UpdateStatus?.Invoke(this, $"Check verif: {res}");
            //this.UpdateProgress?.Invoke(100);

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
                HeTrace.WriteLine($"[CreateHFolders] Creation of '{folderPath}'", this);

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
        private void Compress_ZipMode(string gamePath, string title)
        {
            ZipCompression zippy = new ZipCompression(_SystemPath)
            {
                IsPaused = this.IsPaused,

            };

            //     Make_Zip(destArchive);
            // ZipCompression.Make_Folder(_GamePath, _SystemPath, destArchive);

            var res = PackMe_IHM.ZipCompressFolder(zippy, () => zippy.CompressFolder(
                                         gamePath, title, PS.Default.cZipCompLvl), "Compression Zip");
            //ZipCompression.CompressFolder(gamePath, Path.Combine(_SystemPath, shGame.ExploitableFileName), PS.Default.c7zCompLvl);

            #region Création du fichier  MD5
            if (PS.Default.opMd5)
            {
                Gen_PlusCalculator calculator = Gen_PlusCalculator.Create(CancelToken);
                string sum = calculator.Calcul(zippy.ArchiveLink, () => MD5.Create());
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
        private void Compress_7ZipMode(string gamePath, string title)
        {
            SevenZipCompression sevZippy = new SevenZipCompression(_SystemPath)
            {
                IsPaused = this.IsPaused,
            };

            var res = PackMe_IHM.LaunchOpProgress(sevZippy, () => sevZippy.CompressFolder(
                                    gamePath, title, PS.Default.c7zCompLvl), "Compression 7z");

            #region Création du fichier  MD5
            if (PS.Default.opMd5)
            {
                Gen_PlusCalculator calculator = Gen_PlusCalculator.Create(CancelToken);
                string sum = calculator.Calcul(sevZippy.ArchiveLink, () => MD5.Create());
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
    }
}

