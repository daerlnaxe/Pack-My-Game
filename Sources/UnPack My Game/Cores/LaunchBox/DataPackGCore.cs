using AsyncProgress.Cont;
using Common_Graph;
using Common_PMG;
using Common_PMG.Container;
using Common_PMG.Container.Game;
using Common_PMG.XML;
using DxPaths.Windows;
using DxTBoxCore.Common;
using Hermes;
using Hermes.Messengers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnPack_My_Game.Graph;
using static Common_PMG.Tool;
using static UnPack_My_Game.Common;

namespace UnPack_My_Game.Cores
{

    /// <summary>
    /// Processus évolutif
    /// </summary>
    internal class DataPackGCore : C_LaunchBox
    {
        private string _BackupFolder;

        public DataPackGCore()
        {
            _BackupFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.BackUp);
        }

        /// <summary>
        /// Depack, Copy, Inject
        /// </summary>
        /// <param name="games"></param>
        /// <returns></returns>
        internal bool Depacking(ObservableCollection<DataRep> games)
        {
            ProgressTotal = games.Count();
            try
            {
                MeEmit mee = new MeEmit()
                {
                    ByPass = true,
                };
                mee.SignalWrite += (x, y) => this.SetStatus(x, new StateArg(y, false));
                mee.SignalWriteLine += (x, y) => this.SetStatus(x, new StateArg(y, true));
                HeTrace.AddMessenger("Mee", mee);

                List<DataRep> folders = new List<DataRep>();

                // Depack
                foreach (DataRep game in games)
                {
                    string tmpPath = Path.Combine(Config.HWorkingFolder, Path.GetFileNameWithoutExtension(game.Name));
                    game.DestPath = tmpPath;

                    Depacking(game, tmpPath);

                    folders.Add(new DataRep(tmpPath));
                }

                // Exportation
                foreach (DataRep game in games)
                {
                    ExportGame(game.DestPath);
                }

                return true;
            }
            catch (Exception exc)
            {
                HeTrace.WriteLine(exc.Message);
                HeTrace.WriteLine(exc.StackTrace);
                return false;
            }
            finally
            {
                HeTrace.RemoveMessenger("Mee");
            }
        }

        #region

        internal bool InjectGamesFi(ICollection<DataRep> games)
        {
            ProgressTotal = games.Count();
            try
            {
                MeEmit mee = new MeEmit()
                {
                    ByPass = true,
                };
                mee.SignalWrite += (x, y) => this.SetStatus(x, new StateArg(y, false));
                mee.SignalWriteLine += (x, y) => this.SetStatus(x, new StateArg(y, true));
                HeTrace.AddMessenger("Mee", mee);

                List<DataRep> folders = new List<DataRep>();

                // Extraction des données
                foreach (DataRep game in games)
                {
                    string tmpPath = Path.Combine(Config.HWorkingFolder, Path.GetFileNameWithoutExtension(game.Name));
                    game.DestPath = tmpPath;

                    // extraction
                    ExtractDataFiles(game, tmpPath);

                    folders.Add(new DataRep(tmpPath));
                }

                // Exportation
                foreach (DataRep game in games)
                {
                    InjectGame(game.DestPath);

                }

                return true;
            }
            catch (Exception exc)
            {
                HeTrace.WriteLine(exc.Message);
                HeTrace.WriteLine(exc.StackTrace);
                return false;
            }
            finally
            {
                HeTrace.RemoveMessenger("Mee");
            }
        }


        // ---

        public override bool ExportGames(ICollection<DataRep> games)
        {
            bool result = false;
            try
            {
                MeEmit mee = new MeEmit()
                {
                    ByPass = true,
                };
                mee.SignalWrite += (x, y) => this.SetStatus(x, new StateArg(y, false));
                mee.SignalWriteLine += (x, y) => this.SetStatus(x, new StateArg(y, false));
                HeTrace.AddMessenger("Mee", mee);

                foreach (var game in games)
                {
                    //string tmpPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(game.Name));

                    ExportGame(game.CurrentPath);
                }
                result = true;
            }

            catch (Exception exc)
            {
                HeTrace.WriteLine(exc.Message);
                HeTrace.WriteLine(exc.StackTrace);
                result = false;
            }
            finally
            {
                HeTrace.RemoveMessenger("Mee");
            }
            return result;
        }




        internal bool InjectGamesFo(ICollection<DataRep> games)
        {
            bool result = false;
            try
            {
                MeEmit mee = new MeEmit()
                {
                    ByPass = true,
                };
                mee.SignalWrite += (x, y) => this.SetStatus(x, new StateArg(y, false));
                mee.SignalWriteLine += (x, y) => this.SetStatus(x, new StateArg(y, false));
                HeTrace.AddMessenger("Mee", mee);

                foreach (var game in games)
                {
                    InjectGame(game.CurrentPath);
                }

                result = true;
            }
            catch (Exception exc)
            {
                HeTrace.WriteLine(exc.Message);
                HeTrace.WriteLine(exc.StackTrace);
                result = false;
            }
            finally
            {
                HeTrace.RemoveMessenger("Mee");

            }
            return result;
        }



        /// <summary>
        /// Copy from folder, inject
        /// </summary>
        /// <param name="gamePath"></param>
        private void ExportGame(string gamePath)
        {
            if (CheckDirectoryStructure(gamePath) != true)
                return;

            var gdC = InjectGame(gamePath);

            // Copie des fichiers (on s'appuie sur les dossiers de la plateforme)
            if (CopyFiles(gdC) == false)
                return;

            HeTrace.WriteLine($"Game exported: {gdC.Title}");
        }


        public GameDataCont InjectGame(string gamePath)
        {
            HeTrace.WriteLine("Dpg Step");

            string dpgFile = Path.Combine(gamePath, "DPGame.json");

            // Check if DPG file exists
            if (!File.Exists(dpgFile))
            {
                DPGMakerCore dpgMaker = new DPGMakerCore();
                dpgMaker.MakeDPG_Folder(gamePath);
            }

            GamePaths gpX = GamePaths.ReadFromJson(dpgFile);

            HeTrace.WriteLine($"Platform Step for '{gpX.Platform}'");
            string platformsFile = Path.Combine(Config.HLaunchBoxPath, Config.PlatformsFile);

            bool CheckIfInjectionNeeded = !XML_Custom.TestPresence(platformsFile, Tag.Platform, Tag.Name, gpX.Platform);
            if (CheckIfInjectionNeeded)
            {
                HeTrace.WriteLine($"Backup of platforms file");
                // Backup du fichier de la plateforme;
                BackupFile(platformsFile, _BackupFolder);

                string newPFile = IHMStatic.GetAFile(Config.LastTargetPath, $"Platform '{gpX.Platform}' doesn't exist. Select the xml file to inject for this platform", "xml");
                if (string.IsNullOrEmpty(newPFile))
                    throw new Exception("File for injection is not filled");

                // Vérification que c'est la bonne plateforme
                if (!XML_Custom.TestPresence(newPFile, Tag.Platform, Tag.Name, gpX.Platform))
                    throw new Exception("File doesn't contain the good platform");

                HeTrace.WriteLine($"Injecting {gpX.Platform} in platforms file for {gpX.Platform}");
                InjectPlatform(gpX.Platform, newPFile);
            }

            ContPlatFolders zePlatform = XML_Platforms.GetPlatformPaths(platformsFile, gpX.Platform);

            HeTrace.WriteLine("Preparing files");
            // Préparation des fichiers
            GameDataCont gdC = PrepareGDC(gamePath, gpX);

            // Manipulation des fichiers
            AssignTargets(gdC, gamePath, zePlatform);

            // --- Lecture de la plateforme
            string machineXMLFile = Path.Combine(Config.HLaunchBoxPath, Config.PlatformsFolder, $"{gpX.Platform}.xml");
            if (!File.Exists(machineXMLFile))
                XML_Games.NewPlatform(machineXMLFile);

            // Backup platform file
            BackupFile(machineXMLFile, _BackupFolder);
            HeTrace.WriteLine($"Backup of '{machineXMLFile}'");

            InjectInXMLFile(gamePath, gdC, machineXMLFile, zePlatform.FolderPath);

            HeTrace.WriteLine($"Game injected: {gdC.Title}");

            return gdC;
        }


        /// <summary>
        /// Check if directory structure match with real structure
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private bool? CheckDirectoryStructure(string root)
        {
            HeTrace.WriteLine("Check directory structure");


            // Vérification des dossiers;
            string gameF = Path.Combine(root, Config.Games);
            bool gameDirOk = Directory.Exists(gameF);

            if (!gameDirOk)
            {
                gameF = SafeBoxes.SelectFolder(gameF, "Game folder doesn't match, select folder");
                if (!gameF.StartsWith(root))
                    return false;

                gameDirOk = Directory.Exists(gameF);
                if (!gameDirOk)
                    return false;

                Config.Games = Path.GetFileName(gameF);

                Config.Save();
            }

            bool cheatCDirOk = Directory.Exists(Path.Combine(root, Config.CheatCodes));
            bool imageDirOk = Directory.Exists(Path.Combine(root, Config.Images));
            bool manualDirOk = Directory.Exists(Path.Combine(root, Config.Manuals));
            bool musicDirOk = Directory.Exists(Path.Combine(root, Config.Musics));
            bool videoDirOk = Directory.Exists(Path.Combine(root, Config.Videos));

            bool isOk = true;
            isOk &= gameDirOk;
            isOk &= cheatCDirOk;
            isOk &= imageDirOk;
            isOk &= manualDirOk;
            isOk &= musicDirOk;
            isOk &= videoDirOk;

            bool? res = true;
            if (!isOk)
                res = SafeBoxes.ShowStatus("Structure seems to have directories missing or different, continue ? ", "Warning",
                    new Dictionary<string, bool?>()
                 {
                    { "Games", gameDirOk } , { "Cheats", cheatCDirOk}, { "Images", imageDirOk }, {"Manuals", manualDirOk}, { "Musics", musicDirOk }, { "Videos", videoDirOk}
                 },
                    "#FF60DC32", "#FFFF2323"
                    );
            //   IHMStatic.AskDxMBox("Structure seems to have directories missing or different, continue ? ", "Warning", E_DxButtons.Yes | E_DxButtons.No, "If you continue, files into folders will not been copied");

            return res;
        }

        private GameDataCont PrepareGDC(string root, GamePaths gpX)
        {
            HeTrace.WriteLine($"[{nameof(PrepareGDC)}]");

            GameDataCont gdc = new GameDataCont(gpX.Title, gpX.Platform);

            string tmp = string.Empty;

            HeTrace.WriteLine($"\tGames: {Config.Games}");
            // Games
            tmp = Path.Combine(root, Config.Games);
            gdc.SetApplications = gpX.Applications.Select(x =>
                                        new DataPlus()
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            CurrentPath = Path.GetFullPath(x.CurrentPath, tmp),
                                            IsSelected = x.IsSelected,
                                        });

            if (Directory.Exists(tmp))
                gdc.SSetApplications = Directory.GetFiles(tmp, "*.*", SearchOption.AllDirectories);
            /*gdc.SetDefaultApplication = gpX.ApplicationPath == null ? null : Path.GetFullPath(gpX.ApplicationPath, tmp);
            gdc.SetApplications = Directory.GetFiles(tmp, "*.*", SearchOption.AllDirectories).ToList();*/

            // Manuals
            HeTrace.WriteLine($"\tManuals: {Config.Manuals}");
            tmp = Path.Combine(root, Config.Manuals);
            gdc.SetDefaultManual = string.IsNullOrEmpty(gpX.ManualPath)? null : Path.GetFullPath(gpX.ManualPath, tmp);
            if (Directory.Exists(tmp))
                gdc.AddSManuals = Directory.GetFiles(Path.Combine(tmp), "*.*", SearchOption.AllDirectories).ToList();

            // Musics
            HeTrace.WriteLine($"\tMusics: {Config.Musics}");
            tmp = Path.Combine(root, Config.Musics);
            gdc.SetDefaultMusic = string.IsNullOrEmpty(gpX.MusicPath) ? null : Path.GetFullPath(gpX.MusicPath, tmp);
            if (Directory.Exists(tmp))
                gdc.AddSMusics = Directory.GetFiles(tmp, "*.*", SearchOption.AllDirectories).ToList();

            // Videos
            HeTrace.WriteLine($"\tVideos: {Config.Videos}");
            tmp = Path.Combine(root, Config.Videos);
            gdc.SetDefaultVideo =  string.IsNullOrEmpty(gpX.VideoPath) ? null : Path.GetFullPath(gpX.VideoPath, tmp);
            gdc.SetDefaultThemeVideo = string.IsNullOrEmpty(gpX.ThemeVideoPath) ? null : Path.GetFullPath(gpX.ThemeVideoPath, tmp);
            if (Directory.Exists(tmp))
                gdc.AddSVideos = Directory.GetFiles(tmp, "*.*", SearchOption.AllDirectories);

            // Cheat Codes
            HeTrace.WriteLine($"\tCheatCodes: {Config.CheatCodes}");
            tmp = Path.Combine(root, Config.CheatCodes);
            if (Directory.Exists(tmp))
                gdc.SetSCheatCodes = Directory.GetFiles(tmp, "*.*", SearchOption.AllDirectories).ToList();

            // Images
            HeTrace.WriteLine("\tImages");
            tmp = Path.Combine(root, Config.Images);
            if (Directory.Exists(tmp))
                gdc.Images = PrepareImages(tmp);


            return gdc;
        }


        public List<DataRepImg> PrepareImages(string root)
        {
            List<DataRepImg> files = new List<DataRepImg>();
            var folders = Directory.GetDirectories(root);

            foreach (string d in folders)
                foreach (string f in Directory.EnumerateFiles(d, "*.*", SearchOption.AllDirectories))
                    files.Add(new DataRepImg(Path.GetFileName(d), f));

            return files;
        }


        private void AssignTargets(GameDataCont gdC, string root, ContPlatFolders zePlatform)
        {
            string platFolderPath = zePlatform.FolderPath;
            if (string.IsNullOrEmpty(platFolderPath))
                platFolderPath = Path.Combine(Config.HLaunchBoxPath, "Games");
            else
                platFolderPath = Path.GetFullPath(platFolderPath, Config.HLaunchBoxPath);

            string appTarget = string.Empty;

            if (Config.UseGameNameFolder)
                appTarget = Path.Combine(platFolderPath, Tool.WindowsConv_TitleToFileName(gdC.Title));
            else
                appTarget = platFolderPath;

            // Games
            foreach (var app in gdC.Applications)
                app.DestPath = app.CurrentPath.Replace(Path.Combine(root, Config.Games), appTarget);

            AssignTarget(gdC.Manuals, root, Config.Manuals, Tag.MediaTManual, zePlatform);
            AssignTarget(gdC.Musics, root, Config.Musics, Tag.MediaTMusic, zePlatform);
            AssignTarget(gdC.Videos, root, Config.Videos, Tag.MediaTVideo, zePlatform);

            //    string destPath = zePlatform.PlatformFolders.FirstOrDefault((x => x.MediaType.Equals(mediatype))).FolderPath;

            AssignImages(gdC.Images, root, zePlatform);


            void AssignTarget(IEnumerable<DataRep> elems, string root, string subFolder, string mediaType, ContPlatFolders zePlatform)
            {
                var dFolder = zePlatform.PlatformFolders.First(x => x.MediaType.Equals(mediaType));
                string destFolder = Path.GetFullPath(dFolder.FolderPath, Config.HLaunchBoxPath);

                string src = Path.Combine(root, subFolder);

                foreach (var e in elems)
                    e.DestPath = e.CurrentPath.Replace(src, destFolder);

            }


            void AssignImages(List<DataRepImg> images, string root, ContPlatFolders zePlatform)
            {
                string prevMedType = string.Empty;
                string toReplace = string.Empty;
                string target = string.Empty;

                foreach (DataRepImg image in images)
                {
                    if (!prevMedType.Equals(image.Categorie))
                    {
                        prevMedType = image.Categorie;
                        toReplace = Path.Combine(root, Config.Images, image.Categorie);

                        PlatformFolder pTarget = zePlatform.PlatformFolders.FirstOrDefault(x => x.MediaType.Equals(image.Categorie));
                        target = Path.GetFullPath(pTarget.FolderPath, Config.HLaunchBoxPath);
                    }

                    image.DestPath = image.CurrentPath.Replace(toReplace, target);

                }

            }
        }


        private void InjectInXMLFile(string gamePath, GameDataCont gdC, string xmlPlatform, string platFormFolder)
        {
            //DataPlus defGame = gdC.Applications.FirstOrDefault(x => x.IsSelected);

            if (gdC.DefaultApp == null)
                throw new Exception("No default game chosen");

            // Lecture du fichier TBGame ou EBGame
            string xmlGame = null;
            if (File.Exists(Path.Combine(gamePath, "TBGame.xml")))
                xmlGame = Path.Combine(gamePath, "TBGame.xml");
            else if (File.Exists(Path.Combine(gamePath, "EBGame.xml")))
                xmlGame = Path.Combine(gamePath, "EBGame.xml");
            else if (File.Exists(Path.Combine(gamePath, "NBGame.xml")))
                xmlGame = Path.Combine(gamePath, "NBGame.xml");

            //
            if (xmlGame == null)
                throw new Exception("No XML file to inject");

            // Vérification pour voir si le jeu est présent
            using (XML_Games xmlSrc = new XML_Games(xmlGame))
            using (XML_Games xmlPlat = new XML_Games(xmlPlatform))
            {
                bool? remove = false;
                if (xmlPlat.Exists(GameTag.ID, gdC.DefaultApp.Id))
                {
                    remove = IHMStatic.AskDxMBox("Game is already present, remove it ? ", "Question", E_DxButtons.No | E_DxButtons.Yes, gdC.DefaultApp.Name);

                    if (remove != true)
                        return;

                    // ----------------- On enlève si désiré
                    xmlPlat.Remove_Game(gdC.DefaultApp.Id);
                    xmlPlat.Remove_AddApps(gdC.DefaultApp.Id);
                    xmlPlat.Remove_CustomF(gdC.DefaultApp.Id);
                    xmlPlat.Remove_AlternateN(gdC.DefaultApp.Id);
                }

                // ----------------- Traitement du jeu.
                XElement xelGame = xmlSrc.GetGameNode();

                // ---  Modifications
                // Changement de la plateforme
                if (xelGame.Element(Tag.Platform) != null)
                    xelGame.Element(Tag.Platform).Value = gdC.Platform;

                // App
                ModifElement(xelGame, Tag.AppPath, gdC.DefaultApp, true);
                // Manuel
                ModifElement(xelGame, GameTag.ManPath, gdC.DefaultManual, true);
                // Musique
                ModifElement(xelGame, GameTag.MusPath, gdC.DefaultMusic, false);
                // Video
                ModifElement(xelGame, GameTag.VidPath, gdC.DefaultVideo, false);
                // ThemeVideo
                ModifElement(xelGame, GameTag.ThVidPath, gdC.DefaultThemeVideo, false);

                // Changement du RootFolder
                if (xelGame.Element(GameTag.RootFolder) != null)
                    xelGame.Element(GameTag.RootFolder).Value = platFormFolder;


                // --- Récupération des clones + modification
                var xelClones = xmlSrc.GetNodes(Tag.AddApp);
                foreach (XElement clone in xelClones)
                {
                    var file = gdC.Applications.FirstOrDefault(x => x.Id == clone.Element(CloneTag.Id).Value);
                    if (file != null)
                        ModifElement(clone, Tag.AppPath, file, true);
                }

                xmlPlat.InjectGame(xelGame);
                xmlPlat.InjectAddApps(xelClones);

                // ----------------- Custom Fields
                if (Config.UseCustomFields)
                {
                    var xelCFields = xmlSrc.GetNodes(Tag.CustField);
                    xmlPlat.InjectCustomF(xelCFields);
                }

                // ----------------- Alternate names
                var xelANs = xmlSrc.GetNodes(Tag.AltName);
                xmlPlat.InjectAltName(xelANs);

                xmlPlat.Root.Save(xmlPlatform);
            }
            /*
                    elem.Value = DxPath.To_RelativeOrNull(Default.LastLBpath, defGame.DestPath);
            }


            // Modification du fichier xml pour l'injection
            */
            void ModifElement<T>(in XElement xelObj, string tag, T elem, bool first) where T : IDataRep
            {
                var target = xelObj.Element(tag);
                var value = elem == null ? string.Empty : DxPath.To_Relative(Config.HLaunchBoxPath, elem.DestPath);
                // Pour lever le .\
                value = value.StartsWith(@".\") ? value.Substring(2) : value;

                // Vérification
                if (target == null && first)
                    xelObj.AddFirst(new XElement(tag, value));
                else if (target == null && !first)
                    xelObj.Add(new XElement(tag, value));
                else
                    target.Value = value;
            }

        }

        #endregion




        public override void StopTask()
        {
            throw new NotImplementedException();
        }

        public override void Pause(int timeSleep)
        {
            throw new NotImplementedException();
        }
    }
}

