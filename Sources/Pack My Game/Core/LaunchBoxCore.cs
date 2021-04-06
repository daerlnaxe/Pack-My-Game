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
using LaunchBox_XML.Container.Game;
using LaunchBox_XML.XML;
using System.Windows;
using Pack_My_Game.Cont;
using Pack_My_Game.Files;
using System.Security.Cryptography;
using LaunchBox_XML.Container;
using System.Linq;
using LaunchBox_XML.Container.AAPP;
using Pack_My_Game.Models;
using Pack_My_Game.Compression;
using HashCalc;
using DxLocalTransf.Progress.ToImp;
//using Pack_My_Game.Compression;

namespace Pack_My_Game.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Dans la mesure du possible on n'utilise pas LBGame, on va copier directement le xml
    /// </remarks>
    class LaunchBoxCore : I_ASBaseC
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

        // ---

        /// <summary>
        /// Liste des jeux
        /// </summary>
        public ObservableCollection<ShortGame> _SelectedGames { get; private set; }

        /// <summary>
        /// Fichier xml contenant les jeux
        /// </summary>
        private string _XMLPlatformFile { get; }

        /// <summary>
        /// Nom de la plateforme
        /// </summary>
        //private string _PlatformName { get; }

        /// <summary>
        /// Repertoire de travail
        /// </summary>
        private string _WFolder { get; }

        /// <summary>
        /// Répertoire de travail selon la machine (voir si utile)
        /// </summary>
        public string _SystemPath { get; }

        /// <summary>
        /// Objet gérant les vérifications
        /// </summary>
        OpDFilesExt _ObjectFiles { get; }

        /// <summary>
        /// Object containing folders
        /// </summary>
        Platform _ZePlatform { get; }

        public bool IsPaused { get; set; }

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

        /// <summary>
        /// Répertoire de travail pour le jeu (voir si utile)
        /// </summary>
        //private string _GamePath;

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
        /// <param name="shGames"></param>
        /// <returns></returns>
        public bool Initialize(ObservableCollection<ShortGame> shGames)
        {
            W_Games gamesWindow = new W_Games();
            gamesWindow.SelectedGames = shGames;

            if (gamesWindow.ShowDialog() != true)
            {
                return false;
            }

            _SelectedGames = gamesWindow.SelectedGames;

            return true;
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

                // Contrôle de collisions
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

                // --- Copie des fichiers
                CopyFiles(lbGame, tree);



                #region Serialization / improved backup of Launchbox datas (with found medias missing) 
                /* - En théorie on est toujours sur du relative path
                 * - On a fait un assign sur les dossiers spécifiques
                 * - On va récupérer la structure exacte sans aucune interprétation et modifier ce que l'on veut
                 *      - Les Paths
                 */
                if (PS.Default.opEBGame)
                {
                    XML_Games.EnhancedBackup(_XMLPlatformFile, lbGame, gamePath);
                }
                else
                {
                    HeTrace.WriteLine($"[Run] Enhanced Backup Game disabled", this);
                }

                #endregion

                PackMe_IHM.LaunchBoxCore_Recap(gamePath, _ZePlatform, shGame.Title);


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


        #region copies
        private void CopyFiles(LBGame lbGame, Folder tree)
        {
            // Jeu
            CopyRoms(lbGame.ApplicationPath, tree.Children[Common.Games].Path);
            // Video, Music, Manual
            CopySpecific(lbGame, lbGame.ManualPath, tree.Children[Common.Manuals].Path, "Manual", x => lbGame.ManualPath = x);
            CopySpecific(lbGame, lbGame.MusicPath, tree.Children[Common.Musics].Path, "Music", x => lbGame.MusicPath = x);
            CopySpecific(lbGame, lbGame.VideoPath, tree.Children[Common.Videos].Path, "Video", x => lbGame.VideoPath = x); ;
            // Images
            CopyImages(lbGame, tree.Children[Common.Images].Path);

            // CheatCodes
            #region Copy CheatCodes
            if (PS.Default.opCheatCodes && !string.IsNullOrEmpty(PS.Default.CCodesPath))
            {
                CopyCheatCodes(lbGame, tree.Children[Common.CheatCodes].Path);
            }
            else
            {
                HeTrace.WriteLine("[Run] Copy Cheat Codes disabled", this);
            }
            #endregion

            // Clones
            #region Copy Clones
            if (PS.Default.opClones)
            {
                CopyClones(lbGame, tree.Children[Common.Games].Path);
            }
            else
            {
                HeTrace.WriteLine($"[Run] Clone copy disabled", this);
            }
            #endregion

        }


        /// <summary>
        /// Copy games files
        /// </summary>
        /// <remarks>
        /// Ne conserve pas les subfolders
        /// </remarks>
        /// <param name="applicationPath"></param>
        /// <param name="destLocation"></param>
        private void CopyRoms(string applicationPath, string destLocation)
        {
            /*
             * On part du principe que le dossier rom DOIT être rempli
            */
            if (string.IsNullOrEmpty(applicationPath))
                throw new ArgumentNullException("[CopyRoms] Folder of application path is empty");

            string extension = Path.GetExtension(applicationPath);

            // si aucune extension on ne pack pas le fichier
            if (string.IsNullOrEmpty(extension))
                throw new ArgumentNullException("[CopyRoms] Extension of application is null");


            string srcFile = Path.GetFullPath(applicationPath, PS.Default.LBPath);

            // --- Cas des extensions cue
            if (extension.Equals(".cue", StringComparison.OrdinalIgnoreCase))
            {
                //Lecture du fichier cue
                Cue_Scrapper cuecont = new Cue_Scrapper(srcFile);

                //Folder containing files
                string sourceFold = Path.GetDirectoryName(srcFile);


                // Fonctionne avec le nom du fichier
                foreach (string fileName in cuecont.Files)
                {
                    // Donne le lien complet vers le fichier
                    string source = Path.Combine(sourceFold, fileName);

                    SimpleCopyManager(source, destLocation, ref _FileConflictDecision);
                }
            }

            // --- Copy of application (.cue file too)
            SimpleCopyManager(srcFile, destLocation, ref _FileConflictDecision);

        }

        /// <summary>
        /// Copie les fichiers qui sont peut être identifiés dans le xml
        /// </summary>
        /// <param name="dbGamePath">Tiré des données du jeu</param>
        /// <param name="destLocation">Dossier de destination</param>
        /// <param name="mediatype">Utilisé pour retrouver dans l'objet plateforme</param>
        /// <param name="Assignation"></param>
        /// <remarks>
        /// </remarks>
        private void CopySpecific(LBGame lbGame, string dbGamePath, string destLocation, string mediatype, Func<string, string> Assignation)
        {
            List<string> files = new List<string>();

            // Normal copy, if path get by xml exists
            // 05/04/2021 Modifications pour lever le ou et mettre un système de "Et"
            if (!string.IsNullOrEmpty(dbGamePath))
            {
                string linkFile = Path.GetFullPath(dbGamePath, PS.Default.LBPath);
                if (File.Exists(linkFile))
                {

                    //05/04/2021 SimpleCopyManager(dbPath, destLocation/*, mediatype*/, ref _FileConflictDecision);
                    files.Add(linkFile);


                    // L'assignation permet de transmettre le résultat d'une fonction à une variable             
                    Assignation(DxPaths.Windows.DxPath.To_Relative(PS.Default.LBPath, linkFile));
                }
            }


            // On récupère le dossier concerné par le média
            PlatformFolder folder = _ZePlatform.PlatformFolders.First(
                                            (x) => x.MediaType.Equals(mediatype, StringComparison.OrdinalIgnoreCase));
            /*foreach (PlatformFolder pFormfolder in _ZePlatform.PlatformFolders)
            {
                if (pFormfolder.MediaType.Equals(mediatype))
                {
                    folder = pFormfolder;
                    break;
                }
            }*/

            List<string> filteredFiles = GetFilesByPredict(lbGame, folder.FolderPath, mediatype);


            // En cas d'abandon ou de fichier non trouvé on sort
            if (files.Count == 0 && (filteredFiles == null || filteredFiles.Count == 0))
                return;

            // On assigne si nécessaire
            if (files.Count == 0 && filteredFiles.Count > 0)
                Assignation(DxPaths.Windows.DxPath.To_Relative(PS.Default.LBPath, filteredFiles[0]));


            files.AddRange(filteredFiles);

            // On enlève les doublons
            files = files.Distinct().ToList();

            #region
            // Traitement des fichiers pour la copie
            foreach (string fichier in filteredFiles)
            {
                // Sachant qu'ici on a le nom du dossier source on peut conserver la structure
                string tail = fichier.Replace(folder.FolderPath, "").Replace(Path.GetFileName(fichier), "");

                //Recherche de fichiers déjà présents
                //string destFile = Path.Combine(destLocation, Path.GetFileName(fichier));

                if (tail.Length == 1)
                    SimpleCopyManager(fichier, destLocation, ref _FileConflictDecision);
                else
                    SimpleCopyManager(fichier, Path.Combine(destLocation, tail.Substring(1)), ref _FileConflictDecision);
            }


            #endregion




        }

        /// <summary>
        /// Copie les images / Copy the images
        /// </summary>      
        /// <remarks>To Add Mask see 'Where'</remarks>
        private void CopyImages(LBGame lbGame, string imgsFolder)
        {
            if (!imgsFolder.Contains(_WFolder))
                throw new Exception($"[CreateFolders] Erreur la chaine '{imgsFolder}' ne contient pas '{_WFolder}'");

            //ITrace.WriteLine(prefix: false);

            Queue<PackFile> lPackFile = new Queue<PackFile>();

            // Get All images (To Add mask see at Where)
            Console.WriteLine($"[CopyImages] Search all images for '{lbGame.Title}'");


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

                // 2020 - on modify pour certains titres, la recherche
                string toSearch = lbGame.Title.Replace(':', '_').Replace('\'', '_').Replace("__", "_");

                // Liste du contenu des dossiers
                foreach (var fichier in Directory.EnumerateFiles(plfmFolder.FolderPath, "*.*", SearchOption.AllDirectories))
                {
                    string fileName = Path.GetFileName(fichier);

                    if (
                        !fileName.StartsWith($"{toSearch}-") &&
                        !fileName.StartsWith($"{ lbGame.Title}.{ lbGame.Id}-"))
                        continue;

                    HeTrace.WriteLine($"\t\t[CopyImages] Found '{fichier}' in '{plfmFolder.FolderPath}'");

                    PackFile tmp = new PackFile(plfmFolder.MediaType, fichier);

                    lPackFile.Enqueue(tmp);
                }
            }

            //      ITrace.WriteLine(prefix: false);
            E_Decision resMem = E_Decision.None;
            while (lPackFile.Count != 0)
            {
                var pkFile = lPackFile.Dequeue();

                // On récupère la tail
                int pos = pkFile.LinkToThePath.IndexOf(pkFile.Categorie);
                string tail1 = Path.GetDirectoryName(pkFile.LinkToThePath).Substring(pos);

                // Dossier de destination
                //25/03/2021string destFolder = Path.Combine(_Tree.Children[nameof(SubFolder.Images)].Path, tail1);
                string destFolder = Path.Combine(imgsFolder, tail1);

                SimpleCopyManager(pkFile.LinkToThePath, destFolder, ref resMem);
            }

        }

        /// <summary>
        /// Copie les cheatcodes / Copy all cheatcodes
        /// </summary>
        private void CopyCheatCodes(LBGame lBGame, string destLocation)
        {

            string CCodesDir = Path.Combine(PS.Default.CCodesPath, _ZePlatform.Name);
            if (!Directory.Exists(CCodesDir))
            {
                this.UpdateStatus?.Invoke(this, $"Directory doesn't exist: '{CCodesDir}'");
                return;
            }

            var filteredFiles = GetFilesByPredict(lBGame, CCodesDir, "CheatCodes");


            // En cas d'abandon ou de fichier non trouvé on sort
            if (filteredFiles == null || filteredFiles.Count == 0)
                return;

            //
            foreach (string fichier in filteredFiles)
            {
                SimpleCopyManager(fichier, destLocation, ref _FileConflictDecision);

            }
        }

        private void CopyClones(LBGame lBGame, string destLocation)
        {
            //ITrace.WriteLine(prefix: false);
            /* 26/03/2021
            OPFiles opF = new OPFiles()
            {
                Buttons = Dcs_Buttons.NoStop,
                WhoCallMe = "Clone"
            };
            opF.IWriteLine += (string message) => ITrace.WriteLine(message);
            opF.IWrite += (string message) => ITrace.BeginLine(message);*/

            List<Clone> clones = XML_Games.ListClones(_XMLPlatformFile, "GameID", lBGame.Id).ToList();

            // tri des doublons / filter duplicates
            List<Clone> fClones = FilesFunc.DistinctClones(clones, lBGame.ApplicationPath, PS.Default.LBPath);

            // On va vérifier que chaque clone n'est pas déjà présent et selon déjà copier
            foreach (Clone zeClone in fClones)
            {
                //waiting zeClone.ApplicationPath = ReconstructPath(zeClone.ApplicationPath);

                SimpleCopyManager(Path.GetFullPath(zeClone.ApplicationPath, PS.Default.LBPath), destLocation, ref _FileConflictDecision);
            }
        }
        #endregion





        /// <summary>
        /// Récupère les fichiers par "prédiction"
        /// </summary>
        internal List<string> GetFilesByPredict(LBGame lbGame, string folder, string mediatype)
        {
            // 2020 - Formatage de la chaine pour éviter les erreurs
            string toSearch = lbGame.Title.Replace(':', '_').Replace('\'', '_').Replace("__", "_");

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


        /// <summary>
        /// Verifiy if file exist, if it is doesn't copy + HashVerif at the end
        /// </summary>
        /// <param name="fileSrc"></param>
        /// <param name="destFile"></param>
        internal void SimpleCopyManager(string fileSrc, string destFolder, ref E_Decision previousDec)
        {
            if (CancelToken.IsCancellationRequested)
                throw new OperationCanceledException("Operation stopped");

            // Création des dossiers
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);

            string destFile = Path.Combine(destFolder, Path.GetFileName(fileSrc));
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

