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
using System.Security.AccessControl;
using System.Threading;
using Pack_My_Game.Enum;
using Pack_My_Game.Files;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;

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

        /// <summary>
        /// Work file / Dossier de travail
        /// </summary>
        private string _WFolder;
        private string _SystemName { get; set; }       // Name of the platform
        private string _SystemPath;                  // Platform path
        private string _GamePath;                    // Path where the files will be copy

        /// <summary>
        /// 
        /// </summary>
        private Game _zBackGame;

        /// <summary>
        /// 
        /// </summary>
        private GameInfo _ZeGame;               // Game object
        /// <summary>
        /// 
        /// </summary>
        private Platform _ZePlatform;       // Platform object

        #region résultats

        /// <summary>
        /// Résultat de la copie du jeu
        /// </summary>
        bool vGame;

        bool vApps;
        bool vManual;
        bool vMusic;
        bool vVideo;
        #endregion

        private XML_Functions _XFunctions;
        //private Dictionary<string, Folder> _Tree;
        /// <summary>
        /// Arborescence de 
        /// </summary>
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
        /// ...
        /// Créer un fichier xml après collection des données dans le fichier xml de launchbox si activé
        /// </summary>
        /// <param name="xFile">believe... xml file </param>
        /// <param name="GameFile">Game file (exploitable)</param>
        internal int Initialize(string xFile, ShortGame sGame)
        {
            // Verif
            if (string.IsNullOrEmpty(ID))
                throw new Exception("Id property: null");

            if (string.IsNullOrEmpty(_SystemName))
                throw new Exception();

            _WFolder = Properties.Settings.Default.OutPPath;

            // Chemin du dossier temporaire du system
            _SystemPath = Path.Combine(_WFolder, _SystemName);
            _GamePath = Path.Combine(_SystemPath, $"{sGame.ExploitableFileName}");             // New Working Folder
            string logFile = Path.Combine(_WFolder, $"{_SystemName} - {sGame.ExploitableFileName}.log");


            // Todo peut être déclencher un event sur le stop pour couper net ?
            var folderRes = OPFolders.SVerif(_GamePath, "Initialize", Dcs_Buttons.NoPass | Dcs_Buttons.NoRename, (string message) => ITrace.WriteLine(message, true));
            if (folderRes == EDestDecision.Stop)
            {
                // 
                ITrace.WriteLine("[Initialize] GoodBye !");
                if (_IScreen != null)
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

            #region 2020

            // Creation of System folder and working assign            
            ITrace.WriteLine($"[Run] {Lang.CreationFolder}: '{_SystemName}'");
            Directory.CreateDirectory(_SystemName);
            Directory.SetCurrentDirectory(_SystemPath);

            // Creation of Game folder
            ITrace.WriteLine($"[Run] {Lang.CreationFolder}: '{_GamePath}'");
            Directory.CreateDirectory(_GamePath);
            #endregion


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

            /* Déplacé 2020
            // Creation of System folder and working assign            
            ITrace.WriteLine($"[Run] {Lang.CreationFolder}: '{_SystemName}'");
            Directory.CreateDirectory(_SystemName);
            Directory.SetCurrentDirectory(_SystemPath);

            // Creation of Game folder
            ITrace.WriteLine($"[Run] {Lang.CreationFolder}: '{_GamePath}'");
            Directory.CreateDirectory(_GamePath);
            */
            //2020 Directory.SetCurrentDirectory(_GamePath);


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



            // Copy Roms
            #region 22/08/2020 new rom management
            vApps = CopyRoms(_zBackGame.ApplicationPath, _Tree.Children[nameof(SubFolder.Roms)].Path);

            //vApps = CopySpecific(_zBackGame.ApplicationPath, _Tree.Children["Roms"].Path, "Roms", x => _zBackGame.ApplicationPath = x);
            #endregion


            // Video, Music, Manual
            vManual = CopySpecific(_zBackGame.ManualPath, _Tree.Children[nameof(SubFolder.Manuals)].Path, "Manual", x => _zBackGame.ManualPath = x);
            vMusic = CopySpecific(_zBackGame.MusicPath, _Tree.Children[nameof(SubFolder.Musics)].Path, "Music", x => _zBackGame.MusicPath = x);
            vVideo = CopySpecific(_zBackGame.VideoPath, _Tree.Children[nameof(SubFolder.Videos)].Path, "Video", x => _zBackGame.VideoPath = x);


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


            #region 2020 Résultats 
            // au lieu de mettre à true le bool au moment de la copie,
            //on fait un recheck au cas où l'utilisateur a modifié le contenu)
            //PackMeRes.ShowDialog(vGame, vManual, vMusic, vVideo, vApps);


            PackMeRes2 pmr2 = new PackMeRes2(_Tree.Path)
            {
                GameName = _zBackGame.Title,
                // Destinations
                /*CheatPath = _Tree.Children[nameof(SubFolder.CheatCodes)].Path,
                ManualPath = _Tree.Children[nameof(SubFolder.Manuals)].Path,
                MusicPath = _Tree.Children[nameof(SubFolder.Musics)].Path,
                RomPath = _Tree.Children[nameof(SubFolder.Roms)].Path,
                VideoPath = _Tree.Children[nameof(SubFolder.Videos)].Path,*/
                // Sources
                SourceRomPath = _ZePlatform.FolderPath,
                SourceManuelPath = _ZePlatform.PlatformFolders.FirstOrDefault(x => x.MediaType == "Manual"),
                SourceMusicPath = _ZePlatform.PlatformFolders.FirstOrDefault(x => x.MediaType == "Music"),
                SourceVideoPath = _ZePlatform.PlatformFolders.FirstOrDefault(x => x.MediaType == "Video"),
            };

            // Liste des manuels

            //pmr2.Musics = Directory.GetFiles(_Tree.Children["Musics"].Path, "*.*", System.IO.SearchOption.TopDirectoryOnly);

            pmr2.LoadDatas();
            pmr2.ShowDialog();


            //pmr2.AddManual();
            #endregion

            #region 2020 choix du nom
            // Fenêtre pour le choix du nom
            GameName gnWindows = new GameName();
            gnWindows.SuggestedGameName = _ZeGame.ExploitableFileName;
            gnWindows.ShowDialog();

            // Changement de nom du dossier
            ushort i = 0;

            string destFolder = Path.Combine(_SystemPath, gnWindows.ChoosenGameName);

            if (!_GamePath.Equals(destFolder))
                while (i < 10)
                    try
                    {
                        Directory.Move(_GamePath, destFolder);
                        ITrace.WriteLine("Folder successfully renamed");

                        // Attribution du résultat
                        _GamePath = destFolder;

                        // Sortie
                        break;
                    }
                    catch (IOException ioe)
                    {
                        ITrace.WriteLine($"Try {i}: {ioe}");
                        Thread.Sleep(10);
                        i++;
                    }

            //string destArchLink = Path.Combine(path, $"{destArchive}");
            //string destArchLink = Path.Combine(path, gnWindows.ChoosenGameName);

            // On verra si on dissocie un jour 
            _ZeGame.ExploitableFileName = gnWindows.ChoosenGameName;
            #endregion

            // Archive
            //string destArchive = Path.Combine(_SystemPath, _ZeGame.ExploitableFileName);

            #region Compression
            // Zip
            if (Properties.Settings.Default.opZip)
            {

                //  MessageBox.Show("test "+ destArchive);
                //     Make_Zip(destArchive);
                // ZipCompression.Make_Folder(_GamePath, _SystemPath, destArchive);
                ZipCompression.Make_Folder(_GamePath, _SystemPath, _ZeGame.ExploitableFileName);
            }
            else
            {
                ITrace.WriteLine($"[Run] Zip Compression disabled");
            }
            // 7-Zip
            if (Properties.Settings.Default.op7_Zip)
            {
                //MessageBox.Show("test " + destArchive);

                // SevenZipCompression.Make_Folder(_GamePath, _SystemPath, destArchive);
                SevenZipCompression.Make_Folder(_GamePath, _SystemPath, _ZeGame.ExploitableFileName);
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


            // Stop loggers
            if (_IScreen != null)
                _IScreen.KillAfter(10);

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
            CreateHFolders(_Tree,
                            nameof(SubFolder.CheatCodes),
                            nameof(SubFolder.Images),
                            nameof(SubFolder.Manuals),
                            nameof(SubFolder.Musics),
                            nameof(SubFolder.Videos),
                            nameof(SubFolder.Roms));

            // Back to system menu to unlock game folder.
            Directory.SetCurrentDirectory(_SystemPath);

            // Disc ?
            return true;
        }

        #region Copy functions

        /// <summary>
        /// Copie les roms, regroupe les fichiers sur les images 
        /// </summary>
        /// <param name="dbPath"></param>
        /// <param name="destLocation"></param>
        /// <returns></returns>
        private bool CopyRoms(string dbPath, string destLocation)
        {
            /*
            * On part du principe que le dossier rom DOIT être rempli
            */
            if (_zBackGame.ApplicationPath.Length <= 0)
                return false;


            string extRom = Path.GetExtension(_zBackGame.ApplicationPath);
            // si aucune extension on ne pack pas le fichier
            if (extRom.Length <= 0)
                return false;
            try
            {
                // résultat pour définir si on copie ou pas
                EOPResult res;

                // Traitement dans le cas d'un fichier cue
                if (extRom.Equals(".cue", StringComparison.OrdinalIgnoreCase))
                {
                    //Lecture du fichier cue
                    Cue_Scrapper cuecont = new Cue_Scrapper(_zBackGame.ApplicationPath);

                    //Folder containing files
                    string sourceFold = Path.GetDirectoryName(_zBackGame.ApplicationPath);

                    // Copie de tous les fichiers


                    // Fonctionne avec le nom du fichier
                    foreach (string fileName in cuecont.Files)
                    {
                        // Donne le lien complet vers le fichier
                        string source = Path.Combine(sourceFold, fileName);

                        /*18/10/2020
                        // Hardlink
                        // string destination = Path.Combine(destLocation, fileName);


                        // 2020 Todo
                        // Test if already copied
                        

                        res = OPFiles.FileNameCompare(source, destLocation, $"Copy_Rom", Dcs_Buttons.NoStop, x => ITrace.WriteLine(x));

                        // Copy
                        // 2020
                        /*
                        CopyFile(source, destLocation, res);
                        */

                        /*18 / 10 / 2020
                        FilesFunc.Copy(source, Path.Combine(des, res);
                        */
                        // 18/10/2020


                        // 2020 remplacement
                        string destFile = Path.Combine(destLocation, fileName);

                        // On fait un prétest pour limiter - Le but est d'évaluer si le fichier de destination existe déjà                    
                        if (File.Exists(destFile))
                        //if(fichier.Equals(fichier1))
                        {


                            /* 


                                 /*
                                  * On teste les fichiers pour voir s'ils sont les mêmes en plus d'avoir le même nom
                                  * - Si c'est le cas on va passer, on va passer, puisque l'intégrite est vérifiée
                                  * - S'ils sont différents, on va proposer de renommer, passer.                          
                                  *                          
                                  */

                            // 2020-10-16
                            //Normalement copy handler doit tout gérer
                            /*    Copy_Handler(fichier, destLocation, mediatype);



                                /*
                                OPFiles opF = new OPFiles()
                                {

                                };
                                opF.IWriteLine += new RetourMessage(x => ITrace.WriteLine(x));

                                // Vérification totale
                                var res = opF.DeepVerif(fichier, destFile, () => MD5.Create());

                                */
                            //Demande s'il n'y a pas correspondance mais fichiers similaires.
                            //if(res == EFileResult.NoMatch)


                            // test similarity
                            //var res = OPFiles.DeepCompare(fichier, destFile, $"Copy_{mediatype}", () => MD5.Create(), Dcs_Buttons.All, x=> ITrace.WriteLine(x));
                        }
                        else
                        {
                            FilesFunc.Copy(source, destFile, false);
                        }
                        //2020

                    }
                    //        return true;
                }

                // Traitement dans tous les cas (si c'est un cue on en a aussi besoin)
                /*else
                /*{*/
                //2020/18/10 res = OPFiles.FileNameCompare(dbPath, destLocation, $"Copy_Rom", Dcs_Buttons.NoStop, x => ITrace.WriteLine(x));
                _zBackGame.ApplicationPath = DxPaths.Windows.DxPath.ToRelative(Settings.Default.LBPath, dbPath);


                //TODO voir peut être si présence avant de copier ? 
                // On copie le fichier donné dans la base
                // Copy handler ne peut fonctionner que si on lui file un fichier source et un ficher destination à analyser


                return Copy_Handler(dbPath, destLocation, "Rom");

                /*
                return CopyFile(dbPath, destLocation, res);
                */


                //return true;
                // }
            }
            catch (Exception exc)
            {
                return false;
            }

        }

        /// <summary>
        /// Function advanced
        /// </summary>
        /// <param name="dbPath">Chemin déjà stocké en db</param>
        /// <param name="destLocation">Emplacement de destination</param>
        /// <param name="mediatype">Type de media, fourni par le fichier xml
        ///   <remarks>(Manual, Music, Video...)</remarks>
        /// </param>
        /// <param name="Assignation">Définit à quelle variable on va assigner le chemin relatif calculé</param>
        private bool CopySpecific(string dbPath, string destLocation, string mediatype, Func<string, string> Assignation)
        {

            // Normal copy, if path exists
            if (File.Exists(dbPath))
            {
                Copy_Handler(dbPath, destLocation, mediatype);

                // L'assignation permet de transmettre le résultat d'une fonction à une variable
                Assignation(DxPaths.Windows.DxPath.ToRelative(Settings.Default.LBPath, dbPath));
            }
            else
            {
                // On récupère le dossier concerné par le média
                PlatformFolder plafFolder = null;
                foreach (PlatformFolder pFormfolder in _ZePlatform.PlatformFolders)
                {
                    if (pFormfolder.MediaType.Equals(mediatype))
                    {
                        plafFolder = pFormfolder;
                        break;
                    }
                }
                //MessageBox.Show(_ZeGame.Title +"  "+ zeOne.FolderPath);

                // 2020 - Formatage de la chaine pour éviter les erreurs
                string tosearch = _ZeGame.Title.Replace(':', '_').Replace('\'', '_');
                tosearch = tosearch.Replace("__", "_");

                // array of files with a part of the name
                string[] files = Directory.GetFiles(plafFolder.FolderPath, $"{tosearch}*.*", System.IO.SearchOption.AllDirectories);

                /*
                // case array is empty
                if (files.Length <= 0)
                {
                    MessageBox.Show($"Searching for {mediatype} returned 0 results", $"Searching for {mediatype}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }*/


                #region processing on files founded

                MB_ListFiles mbL = new MB_ListFiles();
                mbL.Message = $"Select {mediatype} matching.\rNote: There is an automatic preselection ";

                // bypass 
                string[] bypass = new string[] { "-", " -" };

                ushort numberF = 0;
                foreach (string fichier in files)
                {
                    string filename = Path.GetFileNameWithoutExtension(fichier);
                    // Test if total match
                    if (filename.Equals(tosearch))
                    {
                        // On ajoute à la liste des checkbox
                        mbL.AddItem(fichier, true);
                        numberF++;
                    }
                    //Test if match with bypass
                    else
                    {
                        // On test avec chaque bypass
                        foreach (var b in bypass)
                        {
                            if (filename.StartsWith(tosearch + b))
                            {
                                // On ajoute à la liste des checkbox
                                mbL.AddItem(fichier, true);
                                numberF++;
                                break;
                            }
                        }
                    }
                }

                //if(mbL.Menu.MenuItems.Count)
                #endregion

                // 2020
                // case array is empty or no corresponding file even with bypass
                //if (files.Length <= 0 || mbL.Menu == null)
                if (numberF <= 0)
                {
                    MessageBox.Show($"Searching for {mediatype} returned {numberF} results", $"Searching for {mediatype}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                // 2020

                // Affichage de la boite de selection;
                List<string> dFiles = null;
                if (mbL.ShowDialog() == DialogResult.Yes)
                {
                    dFiles = mbL.CheckedFiles;
                }
                // old List<string> dFiles = OPFiles.Search_Files(tosearch, plafFolder.FolderPath, System.IO.SearchOption.TopDirectoryOnly, "-", " -");


                // En cas d'abandon ou de fichier non trouvé on renvoie false
                if (dFiles == null || dFiles.Count == 0)
                    return false;

                #region
                // Traitement des fichiers pour la copie
                foreach (string fichier in dFiles)
                {
                    // 2020
                    // Analyses du fichier
                    //if()
                    // 2020

                    //Recherche de fichiers déjà présents
                    string destFile = Path.Combine(destLocation, Path.GetFileName(fichier));

                    // On fait un prétest pour limiter - Le but est d'évaluer si le fichier de destination existe déjà                    
                    if (File.Exists(destFile))
                    //if(fichier.Equals(fichier1))
                    {
                        /*
                         * On teste les fichiers pour voir s'ils sont les mêmes en plus d'avoir le même nom
                         * - Si c'est le cas on va passer, on va passer, puisque l'intégrite est vérifiée
                         * - S'ils sont différents, on va proposer de renommer, passer.                          
                         *                          
                         */

                        // 2020-10-16
                        //Normalement copy handler doit tout gérer
                        Copy_Handler(fichier, destLocation, mediatype);



                        /*
                        OPFiles opF = new OPFiles()
                        {

                        };
                        opF.IWriteLine += new RetourMessage(x => ITrace.WriteLine(x));

                        // Vérification totale
                        var res = opF.DeepVerif(fichier, destFile, () => MD5.Create());

                        */
                        //Demande s'il n'y a pas correspondance mais fichiers similaires.
                        //if(res == EFileResult.NoMatch)


                        // test similarity
                        //var res = OPFiles.DeepCompare(fichier, destFile, $"Copy_{mediatype}", () => MD5.Create(), Dcs_Buttons.All, x=> ITrace.WriteLine(x));
                    }
                    else
                    {
                        FilesFunc.Copy(fichier, destFile, false);
                    }
                    //var res = OPFiles.FileNameCompare(fichier, destLocation, $"Copy_{mediatype}", Dcs_Buttons.NoStop, x => ITrace.WriteLine(x));
                    //  CopyFile(fichier, destLocation, res);
                }
                Assignation(DxPaths.Windows.DxPath.ToRelative(Settings.Default.LBPath, dFiles[0]));
                return true;
                #endregion
            }
            //
            //if (resCopy) return;

            //
            return false;
        }

        /// <summary>
        /// Gère la copie en examinant la totale similitude des fichiers
        /// </summary>
        /// <param name="srcFile"></param>
        /// <param name="destLocation"></param>
        /// <param name="mediatype"></param>
        private bool Copy_Handler(string srcFile, string destLocation, string mediatype)
        {
            // 2020
            // Test de la similitude en profondeur
            //FilesFunc.Check4Crash();
            OPFiles neoOPF = new OPFiles()
            {

            };
            neoOPF.IWrite += new RetourMessage((x) => ITrace.BeginLine(x));
            neoOPF.IWriteLine += new RetourMessage((x) => ITrace.WriteLine(x));

            // Booleen pour déterminer si l'on écrase ou pas
            bool overW = false;

            // Vérification en profondeur
            // Annulé EOPResult res = OPFiles.Copy_DeepVMode(dbPath, destLocation, $"Copy_{mediatype}", () => MD5.Create(), Dcs_Buttons.NoStop, x => ITrace.WriteLine(x));
            string fileName = Path.GetFileName(srcFile);
            string destFile = Path.Combine(destLocation, fileName);
            EFileResult verif = neoOPF.DeepVerif(srcFile, destFile, () => MD5.Create());

            bool copyRes = false;                   // Stocke le résultat de la copie

  
            switch (verif)
            {
                case EFileResult.DifferentSize:
                case EFileResult.DifferentHash:


                // Check selon les résultats de ce qu'il faut faire


                    // Demande à l'utilisateur
                   // EDestDecision res = MB_Decision.Show($"Copy_Handler: {Lang.Dest_File_Exists}, { Lang.Replace_Question} ?", $"{Lang.Alert} - Copy_Handler", destination: destLocation, buttons: Dcs_Buttons.All);
                    EDestDecision res = MB_Decision.Show($"Copy_Handler: {Lang.Dest_File_Exists}, { Lang.Replace_Question} ?", $"{Lang.Alert} - Copy_Handler", source: srcFile, destination: destFile, buttons: Dcs_Buttons.All);


                    // On passe si l'utilisateur ne veut pas écraser ou renommer
                    /*if (res == EDestDecision.Pass || res == EDestDecision.PassAll)
                        return;
                    */


                    // On utilise une fonction de traitement sur le fichier de destination (renommer, envoyer à la poubelle)
                    neoOPF.DestFileAction(res, destFile);
                    

                    // Selon le résultat de la boite on copie ou non le fichier
                    bool? overwrite = Handle_Copy(srcFile,ref destFile, res);
                    if (overwrite != null)
                        copyRes = FilesFunc.Copy(srcFile, destFile, (bool)overwrite);

                    break;

                // Gère si la source a une taille 0 (entre autre)
                case EFileResult.Source_Error:
                    break;

                case EFileResult.Destination_Error:
                case EFileResult.NoMatch:
                    ITrace.WriteLine($"PackMe: Copy of '{fileName}'");
                    copyRes = FilesFunc.Copy(srcFile, destFile, false);

                    break;

                default:
                    ITrace.WriteLine($"PackMe: Copy of '{fileName}' avoided");
                    break;
            }


            return copyRes;
            // 2020


            //2020 EOPResult res = OPFiles.FileNameCompare(dbPath, destLocation, $"Copy_{mediatype}", Dcs_Buttons.NoStop, x => ITrace.WriteLine(x));

            // assignation du chemin relatif à la variable


            // return CopyFile(dbPath, destLocation, res);
        }


        /// <summary>
        /// Copie les images / Copy the images
        /// </summary>      
        /// <remarks>To Add Mask see 'Where'</remarks>
        private void CopyImages()
        {
            //2020/10/28 déplacé var res = MB_SimpleAll.Show($"{Lang.ImagesP} ?", Lang.ImagesTitle, buttons: Dcs_Buttons2.OverWrite | Dcs_Buttons2.Pass);
            
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
                string toSearch = _ZeGame.Title.Replace(':', '_').Replace('\'', '_');
                toSearch = toSearch.Replace("__", "_");

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
            EDestDecision res = EDestDecision.None;
            // Copy Process
            while (lPackFile.Count != 0)
            {
                var pkFile = lPackFile.Dequeue();
                int pos = pkFile.LinkToThePath.IndexOf(pkFile.Categorie);
                string tail1 = Path.GetDirectoryName(pkFile.LinkToThePath).Substring(pos);

                // Dossier de destination
                string destFolder = Path.Combine(_Tree.Children[nameof(SubFolder.Images)].Path, tail1);
                // Fichier de destination
                string destFile = Path.Combine(destFolder, Path.GetFileName(pkFile.LinkToThePath));

                // Création des dossiers
                Console.WriteLine(destFolder);
                if (!Directory.Exists(destFolder))
                {
                    //2020 CreateVFolders(_Tree.Children["Images"], tail1);
                    CreateVFolders(_Tree.Children[nameof(SubFolder.Images)], tail1);

                }
                //2020 FoncSchem.MakeListFolder(_Tree.Children["Images"]);
                FoncSchem.MakeListFolder(_Tree.Children[nameof(SubFolder.Images)]);

                // copy
                // TODO
                // 21/10/2020
                // CopyFile(pkFile.LinkToThePath, dest, res);
                bool? rezDec = false;


                // On déclenche en cas de conflit un handle copy + une demande avant
                if (File.Exists(destFile))
                {
                    // On va demander une fois que faire en cas de conflit.
                    if(res == EDestDecision.None)
                        res = MB_SimpleAll.Show($"{Lang.ImagesP} ?", Lang.ImagesTitle, buttons: Dcs_Buttons2.OverWrite | Dcs_Buttons2.Pass);
                    //
                    rezDec = Handle_Copy(pkFile.LinkToThePath,ref destFile, res);

                }

                if (rezDec == null)
                    continue;


                File.Copy(pkFile.LinkToThePath, destFile, (bool)rezDec);
                //21/10/2020
            }
        }

        /// <summary>
        /// Copie les cheatcodes / Copy all cheatcodes
        /// </summary>
        private void CopyCheatCodes()
        {
            string CCodesDir = Path.Combine(Properties.Settings.Default.CCodesPath, _SystemName);
            ITrace.BeginLine($"[CopyCheatCodes] Search in: '{CCodesDir}' of files beginning by '{_ZeGame.Title}-': ");

            if (!Directory.Exists(CCodesDir))
                return;

            string usableT = _ZeGame.Title.Replace(":","-");

            string[] fichiers = Directory.GetFiles(CCodesDir, usableT, System.IO.SearchOption.AllDirectories);
            ITrace.EndlLine($"{fichiers.Length} found");

            OPFiles opF = new OPFiles()
            {
                WhoCallMe = "CheatCodes",
                Buttons = Dcs_Buttons.NoStop,
            };

            opF.IWriteLine += (string message) => ITrace.WriteLine(message);
            opF.IWrite += (string message) => ITrace.BeginLine(message);


            //
            foreach (string file in fichiers)
            {

                // TODO URGENCE
                //21/10/2020
                //var cheatCodeRes = opF.FileNameCompare(file, _Tree.Children[nameof(SubFolder.CheatCodes)].Path);
                //CopyFile(file, _Tree.Children[nameof(SubFolder.CheatCodes)].Path, cheatCodeRes);
                Copy_Handler(file, _Tree.Children[nameof(SubFolder.CheatCodes)].Path, "Cheats");
                //21/10/2020
            }
        }

        /// <summary>
        /// Copie les clones / Clones copy
        /// </summary>
        private void CopyClones()
        {
            ITrace.WriteLine(prefix: false);

            OPFiles opF = new OPFiles()
            {
                Buttons = Dcs_Buttons.NoStop,
                WhoCallMe = "Clone"
            };
            opF.IWriteLine += (string message) => ITrace.WriteLine(message);
            opF.IWrite += (string message) => ITrace.BeginLine(message);

            List<Clone> clones = new List<Clone>();
            _XFunctions.ListClones(clones, _ZeGame.ID);

            // tri des doublons / filter duplicates

            List<Clone> fClones;
            //fClones= clones.Distinct().ToList();
            fClones = FilesFunc.DistinctClones(clones, _ZeGame.FileName);

            // On va vérifier que chaque clone n'est pas déjà présent et selon déjà copier
            foreach (Clone zeClone in fClones)
            {
                zeClone.ApplicationPath = ReconstructPath(zeClone.ApplicationPath);

                /* 20/10/2020
                 var cloneRes = opF.FileNameCompare(zeClone.ApplicationPath, _Tree.Children[nameof(SubFolder.Roms)].Path);
                 CopyFile(zeClone.ApplicationPath, _Tree.Children[nameof(SubFolder.Roms)].Path, cloneRes);
                */
                Copy_Handler(zeClone.ApplicationPath, _Tree.Children[nameof(SubFolder.Roms)].Path, "Roms");
            }
        }

        // Todo une fenêtre de copie ? 
        // Todo créer une fonctoin de copie dans OpFile avec progression
        // Todo coller un trycatch
        /// <summary>
        /// Décide s'il doit y avoir copier ou non, s'il faut écraser ou non
        /// </summary>
        /// <param name="fichier"></param>
        /// <param name="dest"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool? Handle_Copy(string fichier, ref string dest, EDestDecision result)
        {
            bool overwrite;
            switch (result)
            {
                case EDestDecision.Stop:
                    // TODO
                    return null;

                // Passer
                case EDestDecision.Pass:
                case EDestDecision.PassAll:
                    Debug.WriteLine("Pass");
                    return null;

                case EDestDecision.Trash:
                case EDestDecision.TrashAll:
                    Debug.WriteLine("Trash");
                    return false;


                // --- En cas de renommage
                case EDestDecision.Rename:
                    // Le fichier cible devra être renommé.
                    dest = FilesFunc.Choose_AName(dest, $"{_ZeGame.ExploitableFileName}-");
                    return false;

                case EDestDecision.OverWrite:
                case EDestDecision.OverWriteAll:
                    Debug.WriteLine("Overwrite");

                    return true;

                default:
                    Debug.WriteLine("Default");

                    return false;
                    /*2020

                    -------- plus la peine car automatiquement on vérifie si le fichier existe et non plus seulement
                    si la variable est nulle;

                    case EOPResult.Source_Error:
                        ITrace.WriteLine("File missing");
                        return false;


                    */

            }

            // 2020 enlevé Directory.SetCurrentDirectory(dest);
            /* string filename = Path.GetFileName(fichier);
             string destLink = Path.Combine(dest, filename);
            */


            // 2020 enlevé Directory.SetCurrentDirectory(_GamePath);
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
        /// Créer des dossiers à la verticale dans un répertoire, ajoute au dictionnaire
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

            //2020 Directory.SetCurrentDirectory(basePath.Path);

            //2020 Folder currentFolder = basePath;
            string path = basePath.Path; // 22/10/2020
            foreach (string name in arrTail)
            {
                //2020 string currentDir = Directory.GetCurrentDirectory();
                //2020 Console.WriteLine($"Rep actuel {currentDir}");
                //2020 modif
                // 2020 10 22 string path = Path.Combine(basePath.Path, name); 
                path = Path.Combine(path, name);
                //2020 modif

                ITrace.WriteLine($"[CreateFolders] Creation of the folder: '{path}'");

                // Creation
                Directory.CreateDirectory(path);
                //2020 Directory.SetCurrentDirectory(name);

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
