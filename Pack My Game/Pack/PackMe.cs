using Pack_My_Game.Container;
using Pack_My_Game.XML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Microsoft.VisualBasic.FileIO;
//using Pack_My_Game.IHM;
using System.ComponentModel;
using SevenZip;
using Pack_My_Game.Compression;
using System.Diagnostics;
using DlnxLocalTransfert;
using DlnxLocalTransfert.IHM;
using Pack_My_Game.IHM;
using DxTrace;
using Pack_My_Game.BackupLB;
using Pack_My_Game.Properties;
using DxPaths;

namespace Pack_My_Game.Pack
{

    /*
        For images files, don't make the tree for empty sources (useless and maze for the pc)
      
        Exit Code:
            - 0:    Ok
            - 200:  User Abort
            //- 300:  File Missing
            //- 500:  Folder existing && user pass
            //- 501: Zip File Existing
       
        Development guide:
            - //Move to Recycle.bin (double vérification)
              FileSystem.DeleteDirectory(_GamePath, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
         */
    class PackMe
    {
        //public string Log { get; private set; }      
        public int ExitCode { get; private set; }    // Exit code (see comment above)

        private string ID { get; set; }               // Id of the game

        private DirectoryInfo _LBoxDI;               // DirectoryInfo of Launchbox (useful ?)
        private string _WFolder;                     // working folder (the output folder)
        private string _SystemName { get; set; }       // Name of the platform
        private string _SystemPath;                  // Platform path
        private string _GamePath;                    // Path where the files will be copy
        private Game _zBackGame;
        private GameInfo _ZeGame;               // Game object
        private Platform _ZePlatform;       // Platform object

        #region résultats
        bool vGame;
        bool vApps;
        bool vManual;
        bool vMusic;
        bool vVideo;
        #endregion

        private XML_Functions _XFunctions;
        //private Dictionary<string, Folder> _Tree;
        private Folder _Tree;

        //Loggers
        List<IMessage> _Loggers { get; set; } = new List<IMessage>();
        InfoScreen _IScreen;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="sn">Platform</param>
        internal PackMe(string id, string sn)
        {
            ID = id;
            _SystemName = sn;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xFile">believe... xml file </param>
        /// <param name="GameFile">Game file (exploitable)</param>
        internal int Initialize(string xFile, ShortGame sGame)
        {
            // Verif
            if (string.IsNullOrEmpty(ID)) throw new Exception("Id property: null");
            if (string.IsNullOrEmpty(_SystemName)) throw new Exception();

            _WFolder = Properties.Settings.Default.OutPPath;
            _SystemPath = Path.Combine(_WFolder, _SystemName);
            _GamePath = Path.Combine(_SystemPath, $"{sGame.ExploitableFileName}");             // New Working Folder
            string logFile = Path.Combine(_WFolder, $"{_SystemName} - {sGame.ExploitableFileName}.log");


            var folderRes = OPFolders.SVerif(_GamePath, "Initialize", Dcs_Buttons.NoPass, (string message) => ITrace.WriteLine(message, true));
            if (folderRes == OPResult.Stop)
            {
                // todo
                ITrace.WriteLine("[Initialize] GoodBye !");
                _IScreen.Close();

                ITrace.RemoveListener(_IScreen);

                return 200;
            }

            #region System d'affichage
            string prefix = "PackMe";
            //window
            if (Settings.Default.opLogWindow)
            {
                _IScreen = new InfoScreen();
                _IScreen.Prefix = prefix;
                _IScreen.Show();
                _Loggers.Add(_IScreen);
            }

            // file
            if (Settings.Default.opLogFile)
            {
                InfoToFile iLog = new InfoToFile(logFile, true);
                iLog.Prefix = prefix;
                _Loggers.Add(iLog);
            }

            // debug
            if (Debugger.IsAttached)
            {
                InfoToConsole iConsole = new InfoToConsole();
                iConsole.Prefix = prefix;
                _Loggers.Add(iConsole);
            }

            ITrace.AddListeners(_Loggers);


            ITrace.WriteLine("===== Report of errors: =====");
            ITrace.WriteLine($"[Initialize] ID:\t'{ID}'");
            ITrace.WriteLine($"[Initialize] {Lang.SystemSelected}: '{_SystemName}'");
            ITrace.WriteLine($"[Initialize] {Lang.GameSelected}: '{sGame.Title}' - Rom: '{sGame.ExploitableFileName}'");


            #endregion
            /*
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(BwWork); // PackMe.Initialize(_XmlFPlatform);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BWRunWorkerCompleted);
            bw.RunWorkerAsync();
            */

            // Lecture du fichier platform
            XML_Functions xmlPlatform = new XML_Functions();
            xmlPlatform.ReadFile(Path.Combine(Properties.Settings.Default.LBPath, Properties.Settings.Default.fPlatforms));

            _ZePlatform = xmlPlatform.ScrapPlatform(_SystemName);

            // Reconstruct PlatformFolder Path
            foreach (PlatformFolder plfmFolder in _ZePlatform.PlatformFolders)
            {
                plfmFolder.FolderPath = ReconstructPath(plfmFolder.FolderPath);
            }

            // Lecture du fichier des jeux
            _XFunctions = new XML_Functions();
            _XFunctions.ReadFile(xFile);

            // Get Main infos
            _zBackGame = _XFunctions.ScrapBackupGame(ID);
            _ZeGame = (GameInfo)_zBackGame;

            #region Original Backup Game
            if (Settings.Default.opOBGame)
            {
                MakeXML.Backup_Game(_GamePath, _zBackGame, "TBGame");
            }
            else
            {
                ITrace.WriteLine("[Run] Original Backup Game disabled");
            }
            #endregion

            _LBoxDI = new DirectoryInfo(Settings.Default.LBPath);

            // Set active Directory to Root
            Directory.SetCurrentDirectory(_WFolder);

            #region Paths reconstruction

            _zBackGame.ApplicationPath = ReconstructPath(_zBackGame.ApplicationPath);
            _zBackGame.ManualPath = ReconstructPath(_zBackGame.ManualPath);
            _zBackGame.MusicPath = ReconstructPath(_zBackGame.MusicPath);
            _zBackGame.VideoPath = ReconstructPath(_zBackGame.VideoPath);
            #endregion


            return 0;

        }

        /// <summary>
        /// 
        /// </summary>
        internal bool Run()
        {
            ITrace.WriteLine(prefix: false);

            #region
            // Verifications 
            // Todo Verifications with zip and 7z
            //if (Directory.Exists(_GamePath))
            //{


            //}
            #endregion

            // Creation of System folder and working assign            
            ITrace.WriteLine($"[Run] {Lang.CreationFolder}: '{_SystemName}'");
            Directory.CreateDirectory(_SystemName);
            Directory.SetCurrentDirectory(_SystemPath);

            // Creation of Game folder
            ITrace.WriteLine($"[Run] {Lang.CreationFolder}: '{_GamePath}'");
            Directory.CreateDirectory(_GamePath);
            Directory.SetCurrentDirectory(_GamePath);


            #region Creation of the Infos.xml
            if (Settings.Default.opInfos)
            {
                MakeXML.InfoGame(_GamePath, _ZeGame);
            }
            else
            {
                ITrace.WriteLine("[Run] Make info disabled");
            }
            #endregion

            // Tree root + lvl1
            MakeStructure();

            // Copy Roms, Video, Music, Manual
            vApps = CopySpecific(_zBackGame.ApplicationPath, _Tree.Children["Roms"].Path, "Roms", x => _zBackGame.ApplicationPath = x);
            vManual = CopySpecific(_zBackGame.ManualPath, _Tree.Children["Manuals"].Path, "Manuals", x => _zBackGame.ManualPath = x);
            vMusic = CopySpecific(_zBackGame.MusicPath, _Tree.Children["Musics"].Path, "Music", x => _zBackGame.MusicPath = x);
            vVideo = CopySpecific(_zBackGame.VideoPath, _Tree.Children["Videos"].Path, "Video", x => _zBackGame.VideoPath = x);
            // CopySpecificFiles() old way;

            // Copy images
            CopyImages();

            #region Copy CheatCodes
            if (Settings.Default.opCheatCodes && !string.IsNullOrEmpty(Settings.Default.CCodesPath))
            {
                CopyCheatCodes();
            }
            else
            {
                ITrace.WriteLine("[Run] Copy Cheat Codes disabled");
            }
            #endregion

            #region Copy Clones
            if (Settings.Default.opClones)
            {
                CopyClones();
            }
            else
            {
                ITrace.WriteLine($"[Run] Clone copy disabled");
            }
            #endregion

            #region Serialization / backup ameliorated of launchbox datas (with found medias missing) 
            if (Settings.Default.opEBGame)
            {
                MakeXML.Backup_Game(_GamePath, _zBackGame, "EBGame");
            }
            else
            {
                ITrace.WriteLine($"[Run] Enhanced Backup Game disabled");
            }

            #endregion

            #region Save Struct
            if (Settings.Default.opTreeV)
            {
                GetStruc();
            }
            else
            {
                ITrace.WriteLine($"[Run] Save Struct disabled");
            }
            #endregion

            // Archive
            string destArchive = Path.Combine(_SystemPath, _ZeGame.ExploitableFileName);

            #region Compressions
            // Zip
            if (Properties.Settings.Default.opZip)
            {

              //  MessageBox.Show("test "+ destArchive);
                //     Make_Zip(destArchive);
                ZipCompression.Make_Folder(_GamePath, _SystemPath, destArchive);
            }
            else
            {
                ITrace.WriteLine($"[Run] Zip Compression disabled");
            }

            // 7-Zip
            if (Properties.Settings.Default.op7_Zip)
            {
                //MessageBox.Show("test " + destArchive);

                SevenZipCompression.Make_Folder(_GamePath, _SystemPath, destArchive);
            }
            else
            {
                ITrace.WriteLine($"[Run] 7z Compression disabled");
            }
            #endregion

            // Erase the temp folder
            if (MessageBox.Show($"{Lang.EraseTmpFolder} '{_ZeGame.ExploitableFileName}' ?", Lang.Erase, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Directory.SetCurrentDirectory(_WFolder);

                    Directory.Delete(_GamePath, true);
                    Console.WriteLine($"[Run] folder {_GamePath} erased");

                }
                catch (Exception exc)
                {
                    Console.WriteLine($"[Run] Error when Erasing temp folder {_GamePath}\n{exc.Message }");

                }
            }

            // Résultats
            PackMeRes.ShowDialog(vGame, vManual, vMusic, vVideo);

            // Stop loggers
            if (_IScreen != null) _IScreen.KillAfter(10);

            ITrace.RemoveListerners(_Loggers);

            return true;
        }

        #region WriteData


        private void GetStruc()
        {
            string path = Path.Combine(_GamePath, "Struct.info");
            using (StreamWriter fs = new StreamWriter(path, false))
            {
                fs.WriteLine(FoncSchem.TraitListFolder(_Tree, 0));
            }
        }
        #endregion

        /// <summary>
        /// Fais une arborescence selon un schéma - Make a tree according to a template
        /// Todo xml pour permettre de modifier à volonté
        /// </summary>
        private bool MakeStructure()
        {
            Directory.SetCurrentDirectory(_GamePath);

            ITrace.WriteLine(prefix: false);
            ITrace.WriteLine("[MakeStructure] Creation of the tree");

            _Tree = new Folder("Root", _GamePath);
            //_Tree.Add("Root", root);

            // Creation lvl 1
            CreateHFolders(_Tree, "CheatCodes", "Images", "Manuals", "Musics", "Videos", "Roms");

            // Disc ?
            return true;
        }

        #region Copy functions
        /// <summary>
        /// Copy files (Roms, Manuals, Videos, Music)
        /// </summary>
        /// <returns></returns>
        private bool CopySpecificFiles()
        {
            /*
            ITrace.WriteLine(prefix: false);
            //IWrite.NewLine($"Dossier actif:{Directory.GetCurrentDirectory()}");
            Console.WriteLine(Directory.GetCurrentDirectory());

            OPFiles opFiles = new OPFiles();
            opFiles.IWriteLine += (string message) => ITrace.WriteLine(message);
            opFiles.IWrite += (string message) => ITrace.BeginLine(message);
            opFiles.Buttons = Dcs_Buttons.NoStop;*/

            // rom            
            //var romRes = opFiles.Compare(_ZeGame.ApplicationPath, _Tree.Children["Roms"].Path, whocallme: "Copy_Rom");
            //CopyFile(_ZeGame.ApplicationPath, _Tree.Children["Roms"].Path, romRes);
            vGame = CopySpecific(_zBackGame.ApplicationPath, _Tree.Children["Roms"].Path, "Game", x => _zBackGame.ApplicationPath = x);


            // Manual
            /*
            var manualRes = opFiles.Compare(_zBackGame.ManualPath, _Tree.Children["Manuals"].Path, whocallme: "Copy_Manual");
            CopyFile(_zBackGame.ManualPath, _Tree.Children["Manuals"].Path, manualRes);*/

            // Music            
            /*var musicRes = opFiles.Compare(_ZeGame.MusicPath, _Tree.Children["Musics"].Path, whocallme: "Copy_Music");
            CopyFile(_ZeGame.MusicPath, _Tree.Children["Musics"].Path, musicRes);*/

            // Video
            /* var videoRes = opFiles.Compare(_zBackGame.VideoPath, _Tree.Children["Videos"].Path, whocallme: "Copy_Videos");
             CopyFile(_zBackGame.VideoPath, _Tree.Children["Videos"].Path, videoRes);*/

            //var videores = Destination.Verif(_Tree.Children["Videos"].Path, srcFile: _ZeGame.VideoPath,  whocallme: "Video");
            //if (videores != Verif_Result.Pass && videores != Verif_Result.Source_Error)
            //{
            //    CopyFile(_ZeGame.VideoPath, _Tree.Children["Videos"].Path);
            //}
            return true;
        }

        /// <summary>
        /// Function advcanced
        /// </summary>
        private bool CopySpecific(string dbPath, string destLocation, string mediatype, Func<string, string> Assignation)
        {
            // Normal copy
            if (!string.IsNullOrEmpty(dbPath))
            {
                OPResult res = OPFiles.SingleCompare(dbPath, destLocation, $"Copy_{mediatype}", Dcs_Buttons.NoStop, x => ITrace.WriteLine(x));
                Assignation(DxPaths.Windows.DxPath.ToRelative(Settings.Default.LBPath, dbPath));
                return CopyFile(dbPath, destLocation, res);
            }
            else
            {
                PlatformFolder zeOne = null;
                foreach (var folder in _ZePlatform.PlatformFolders)
                {
                    if (folder.MediaType.Equals(mediatype))
                    {
                        zeOne = folder;
                        break;
                    }
                }
                //MessageBox.Show(_ZeGame.Title +"  "+ zeOne.FolderPath);

                // 2020 - Formatage de la chaine pour éviter les erreurs
                string tosearch = _ZeGame.Title.Replace(':', '_');

                List<string> dFiles = OPFiles.Search_Files(tosearch, zeOne.FolderPath, System.IO.SearchOption.TopDirectoryOnly, "-", " -");

                if (dFiles.Count == 0) return false;

                foreach (string fichier in dFiles)
                {
                    var res = OPFiles.SingleCompare(fichier, destLocation, $"Copy_{mediatype}", Dcs_Buttons.NoStop, x => ITrace.WriteLine(x));
                    CopyFile(fichier, destLocation, res);
                }
                Assignation(DxPaths.Windows.DxPath.ToRelative(Settings.Default.LBPath, dFiles[0]));
                return true;

            }
            //
            //if (resCopy) return;

            //
            return false;
        }

        /// <summary>
        /// Copie les images / Copy the images
        /// </summary>      
        /// <remarks>To Add Mask see 'Where'</remarks>
        private void CopyImages()
        {
            var res = MB_SimpleAll.Show($"{Lang.ImagesP} ?", Lang.ImagesTitle, buttons: Dcs_Buttons2.OverWrite | Dcs_Buttons2.Pass);

            ITrace.WriteLine(prefix: false);
                                 
            ITrace.WriteLine(prefix: false);
            Queue<PackFile> lPackFile = new Queue<PackFile>();
            
            // Get All images (To Add mask see at Where)
            Console.WriteLine($"[CopyImages] Search all images for '{_ZeGame.Title}'");
            foreach (PlatformFolder plfmFolder in _ZePlatform.PlatformFolders)
            {
                //filtre
                switch (plfmFolder.MediaType)
                {
                    case "Manual":
                    case "Music":
                    case "Theme Video":
                    case "Video":
                        continue;
                }

                // 2020 - on modify pour certains titres, la recherche
                string toSearch = _ZeGame.Title.Replace(':', '_');

                // Liste du contenu des dossiers
                foreach (var fichier in Directory.EnumerateFiles(plfmFolder.FolderPath, "*.*", System.IO.SearchOption.AllDirectories)
                    .Where(s => Path.GetFileName(s).StartsWith($"{toSearch}-") || Path.GetFileName(s).StartsWith($"{_ZeGame.Title}.{_ZeGame.ID}-")))
                {

                    Console.WriteLine($"\t\t[CopyImages] Found '{fichier}' in '{plfmFolder.FolderPath}'");

                    PackFile tmp = new PackFile(plfmFolder.MediaType, fichier);

                    lPackFile.Enqueue(tmp);
                }
            }

            ITrace.WriteLine(prefix: false);
            // Copy Process
            while (lPackFile.Count != 0)
            {
                var pkFile = lPackFile.Dequeue();
                int pos = pkFile.LinkToThePath.IndexOf(pkFile.Categorie);
                string tail1 = Path.GetDirectoryName(pkFile.LinkToThePath).Substring(pos);
                string dest = Path.Combine(_Tree.Children["Images"].Path, tail1);

                // Création des dossiers
                Console.WriteLine(dest);
                if (!Directory.Exists(dest))
                {
                    CreateVFolders(_Tree.Children["Images"], tail1);
                }
                FoncSchem.MakeListFolder(_Tree.Children["Images"]);

                // copy
                CopyFile(pkFile.LinkToThePath, dest, res);

            }
        }

        /// <summary>
        /// Copie les cheatcodes / Copy all cheatcodes
        /// </summary>
        private void CopyCheatCodes()
        {
            string CCodesDir = Path.Combine(Properties.Settings.Default.CCodesPath, _SystemName);
            ITrace.BeginLine($"[CopyCheatCodes] Search in: '{CCodesDir}' of files beginning by '{_ZeGame.Title}-': ");

            string[] fichier = Directory.GetFiles(CCodesDir, $"{_ZeGame.Title}-*.*", System.IO.SearchOption.AllDirectories);
            ITrace.EndlLine($"{fichier.Length} found");

            OPFiles opFiles = new OPFiles();
            opFiles.IWriteLine += (string message) => ITrace.WriteLine(message);
            opFiles.IWrite += (string message) => ITrace.BeginLine(message);
            opFiles.Buttons = Dcs_Buttons.NoStop;

            foreach (string file in fichier)
            {
                var cheatCodeRes = opFiles.Compare(file, _Tree.Children["CheatCodes"].Path, whocallme: "CheatCodes");
                CopyFile(file, _Tree.Children["CheatCodes"].Path, cheatCodeRes);
            }
        }

        /// <summary>
        /// Copie les clones / Clones copy
        /// </summary>
        private void CopyClones()
        {
            ITrace.WriteLine(prefix: false);

            OPFiles opFiles = new OPFiles();
            opFiles.IWriteLine += (string message) => ITrace.WriteLine(message);
            opFiles.IWrite += (string message) => ITrace.BeginLine(message);
            opFiles.Buttons = Dcs_Buttons.NoStop;

            List<Clone> clones = new List<Clone>();
            _XFunctions.ListClones(clones, _ZeGame.ID);

            // tri des doublons / filter duplicates
            var fClones = clones.Distinct().ToList();

            foreach (Clone zeClone in fClones)
            {
                zeClone.ApplicationPath = ReconstructPath(zeClone.ApplicationPath);
      
                var cloneRes = opFiles.Compare(zeClone.ApplicationPath, _Tree.Children["Roms"].Path, whocallme: "Clone");
                CopyFile(zeClone.ApplicationPath, _Tree.Children["Roms"].Path, cloneRes);
            }
        }

        // Todo coller un trycatch
        private bool CopyFile(string fichier, string dest, OPResult result)
        {
            bool overwrite;
            switch (result)
            {

                case OPResult.Ok:
                case OPResult.Trash:
                case OPResult.TrashAll:
                    overwrite = false;
                    break;
                case OPResult.OverWrite:
                case OPResult.OverWriteAll:
                    overwrite = true;
                    break;
                case OPResult.Source_Error:
                    ITrace.WriteLine("File missing");
                    return false;

                default:
                    return false;


            }

            ITrace.BeginLine($"[CopyFiles] Copy of the file '{fichier}': ");


            Directory.SetCurrentDirectory(dest);
            string filename = Path.GetFileName(fichier);
            string destLink = Path.Combine(dest, filename);

            try
            {
                File.Copy(fichier, destLink, overwrite);
                ITrace.EndlLine("Successful");
            }
            catch (Exception e)
            {
                ITrace.EndlLine("Error");
                ITrace.WriteLine(e.Message);
            }

            Directory.SetCurrentDirectory(_GamePath);
            return true;
        }



        #endregion


        #region Fonctions secondaires

        /// <summary>
        /// Reconstruction d'un chemin relatif - Reconstruction of a relative path
        /// </summary>
        /// <param name="path">Relative path</param>
        /// <returns>Reconstructed path</returns>
        string ReconstructPath(string path)
        {
            string pReconstruct = null;
            string previousDir = Directory.GetCurrentDirectory();

            Directory.SetCurrentDirectory(Properties.Settings.Default.LBPath);

            try
            {
                pReconstruct = Path.GetFullPath(path);
            }
            catch
            {
                pReconstruct = null;
            }

            ITrace.WriteLine($"[ReformPath] {path} => {pReconstruct}");

            Directory.SetCurrentDirectory(previousDir);

            return pReconstruct;
        }

        /// <summary>
        /// Créer des dossiers à l'horizontale dans un répertoire, ajoute au dictionnaire /  Create folders horizontally
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="folders"></param>
        void CreateHFolders(Folder basePath, params string[] folders)
        {
            ITrace.WriteLine(prefix: false);
            if (!basePath.Path.Contains(_WFolder))
            {
                ITrace.WriteLine($"[CreateFolders] Erreur la chaine {basePath} ne contient pas {_WFolder}");
                return;
            }

            Directory.SetCurrentDirectory(basePath.Path);
            ITrace.WriteLine($"[CreateFolders] Current Directory '{Directory.GetCurrentDirectory()}'");

            foreach (string name in folders)
            {
                ITrace.WriteLine($"[CreateFolders] Creation of the folder: '{name}'");
                string path = Path.Combine(basePath.Path, name);

                // Add to the Dictionary Tree
                Folder tmp = new Folder(name, path);
                basePath.Children.Add(name, tmp);

                // Creation
                Directory.CreateDirectory(name);
            }

        }


        /// <summary>
        /// Créer des dossiers à l'horizontale dans un répertoire, ajoute au dictionnaire
        /// </summary>
        /// <param name="basePath">Starting Directory</param>
        /// <param name="folders"></param>
        void CreateVFolders(Folder basePath, string tail)
        {
            ITrace.WriteLine(prefix: false);

            // Double Security
            if (!basePath.Path.Contains(_WFolder))
            {
                ITrace.WriteLine($"[CreateFolders] Erreur la chaine {basePath} ne contient pas {_WFolder}");
                return;
            }

            string[] arrTail = tail.Split('\\');

            Directory.SetCurrentDirectory(basePath.Path);

            Folder currentFolder = basePath;
            foreach (string name in arrTail)
            {
                string currentDir = Directory.GetCurrentDirectory();


                Console.WriteLine($"Rep actuel {currentDir}");
                string path = Path.Combine(currentDir, name);

                ITrace.WriteLine($"[CreateFolders] Creation of the folder: '{path}'");

                // Creation
                Directory.CreateDirectory(name);
                Directory.SetCurrentDirectory(name);

                //// Add to the Dictionary Tree
                //Folder tmp = new Folder(name, path);

                //if (!currentFolder.Children.ContainsKey(name))
                //{
                //    currentFolder.Children.Add(name, tmp);
                //    currentFolder = tmp;
                //}
                //else
                //{
                //    currentFolder = currentFolder.Children[name];
                //}
            }


        }

        /* Pas assez puissant => inutile pour le moment. Futur => rentrer un link puis parcours total dico par dico
        /// <summary>
        /// Récupère la position dans l'arbre selon une position
        /// </summary>
        /// <param name="name"></param>
        /// <param name="zeFolder"></param>
        /// <returns></returns>
        private Folder GetCurrentFolder(string name, Folder zeFolder)
        {
            foreach (var child in zeFolder.Children)
            {
                if (child.Key.Equals(name)) return child.Value;
            }

            return null;
        }
        */
        #endregion


        ~PackMe()
        {
            Console.WriteLine("End of PackMe");
        }
    }
}
