using AsyncProgress;
using AsyncProgress.Basix;
using AsyncProgress.Cont;
using Common_PMG.Container;
using Common_PMG.Container.Game;
using Common_PMG.XML;
using DxPaths.Windows;
using DxTBoxCore.Common;
using Hermes;
using Hermes.Cont;
using Hermes.Messengers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using UnPack_My_Game.Decompression;
using UnPack_My_Game.Graph;
using UnPack_My_Game.Graph.LaunchBox;
using static UnPack_My_Game.Properties.Settings;
using static Common_PMG.Tool;
using Common_Graph;
using Common_PMG;

namespace UnPack_My_Game.Cores
{
    /// <summary>
    /// Processus évolutif
    /// </summary>
    internal class DataPackGCore : C_LaunchBox
    {
        public DataPackGCore()
        {

        }



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

                // Dépack
                foreach (DataRep game in games)
                {
                    string tmpPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(game.Name));
                    game.DestPath = tmpPath;

                    Depacking(game, tmpPath);

                    folders.Add(new DataRep(tmpPath));
                }

                // Injection
                foreach (DataRep game in games)
                {
                    InjectGame(game.DestPath);

                }
                //InjectGames(folders);

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
        public override bool InjectGames(ICollection<DataRep> games)
        {
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

                    InjectGame(game.CurrentPath);
                }
                return true;
            }

            catch (Exception exc)
            {
                HeTrace.WriteLine(exc.Message);
                HeTrace.WriteLine(exc.StackTrace);

            }
            finally
            {
                HeTrace.RemoveMessenger("Mee");
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gamePath"></param>
        private void InjectGame(string gamePath)
        {
            //bool backupDone = false;
            string backupFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.BackUp);

            if (CheckDirectoryStructure(gamePath) != true)
                return;

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
            string platformsFile = Path.Combine(Default.LaunchBoxPath, Default.fPlatforms);


            bool CheckIfInjectionNeeded = !XML_Custom.TestPresence(platformsFile, Tag.Platform, Tag.Name, gpX.Platform);
            if (CheckIfInjectionNeeded)
            {
                HeTrace.WriteLine($"Backup of platforms file");
                // Backup du fichier de la plateforme;
                BackupFile(platformsFile, backupFolder);

                string newPFile = IHMStatic.GetAFile(Default.LastTargetPath, $"Platform '{gpX.Platform}' doesn't exist. Select the xml file to inject for this platform", "xml");
                if (string.IsNullOrEmpty(newPFile))
                    throw new Exception("File for injection is not filled");

                HeTrace.WriteLine($"Injecting {gpX.Platform} in platforms file for {gpX.Platform}");
                InjectPlatform(gpX.Platform, newPFile);
            }


            ContPlatFolders zePlatform = XML_Platforms.GetPlatformPaths(platformsFile, gpX.Platform);

            HeTrace.WriteLine("Preparing files");
            // Préparation des fichiers
            GameDataCont gdC = PrepareGDC(gamePath, gpX);

            // Manipulation des fichiers
            AssignTargets(gdC, gamePath, zePlatform);


            // Copie des fichiers (on s'appuie sur les dossiers de la plateforme)
            CopyFiles(gdC);


            // --- Lecture de la plateforme
            string machineXMLFile = Path.Combine(Default.LaunchBoxPath, Default.dPlatforms, $"{gpX.Platform}.xml");
            if (!File.Exists(machineXMLFile))
                XML_Games.NewPlatform(machineXMLFile);

            // Backup platform file
            BackupFile(machineXMLFile, backupFolder);
            HeTrace.WriteLine($"Backup of '{machineXMLFile}'");

            InjectInXMLFile(gamePath, gdC, machineXMLFile, zePlatform.FolderPath);

            HeTrace.WriteLine($"Done: {gdC.Title}");
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
            string gameF = Path.Combine(root, Default.Games);
            bool gameDirOk = Directory.Exists(gameF);

            if (!gameDirOk)
            {
                gameF = SafeBoxes.SelectFolder(gameF, "Game folder doesn't match, select folder");
                if (!gameF.StartsWith(root))
                    return false;

                gameDirOk = Directory.Exists(gameF);
                if (!gameDirOk)
                    return false;

                Default.Games = Path.GetFileName(gameF);
                Default.Save();
            }

            bool cheatCDirOk = Directory.Exists(Path.Combine(root, Default.CheatCodes));
            bool imageDirOk = Directory.Exists(Path.Combine(root, Default.Images));
            bool manualDirOk = Directory.Exists(Path.Combine(root, Default.Manuals));
            bool musicDirOk = Directory.Exists(Path.Combine(root, Default.Musics));
            bool videoDirOk = Directory.Exists(Path.Combine(root, Default.Videos));

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

            GameDataCont gdc = new GameDataCont(gpX.Title);

            string tmp = string.Empty;

            HeTrace.WriteLine($"\tGames: {Default.Games}");
            // Games
            tmp = Path.Combine(root, Default.Games);
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
            HeTrace.WriteLine($"\tManuals: {Default.Manuals}");
            tmp = Path.Combine(root, Default.Manuals);
            gdc.SetDefaultManual = gpX.ManualPath == null ? null : Path.GetFullPath(gpX.ManualPath, tmp);
            if (Directory.Exists(tmp))
                gdc.AddSManuals = Directory.GetFiles(Path.Combine(tmp), "*.*", SearchOption.AllDirectories).ToList();

            // Musics
            HeTrace.WriteLine($"\tMusics: {Default.Musics}");
            tmp = Path.Combine(root, Default.Musics);
            gdc.SetDefaultMusic = gpX.MusicPath == null ? null : Path.GetFullPath(gpX.MusicPath, tmp);
            if (Directory.Exists(tmp))
                gdc.AddSMusics = Directory.GetFiles(tmp, "*.*", SearchOption.AllDirectories).ToList();

            // Videos
            HeTrace.WriteLine($"\tVideos: {Default.Videos}");
            tmp = Path.Combine(root, Default.Videos);
            gdc.SetDefaultVideo = gpX.VideoPath == null ? null : Path.GetFullPath(gpX.VideoPath, tmp);
            gdc.SetDefaultThemeVideo = gpX.ThemeVideoPath == null ? null : Path.GetFullPath(gpX.ThemeVideoPath, tmp);
            if (Directory.Exists(tmp))
                gdc.AddSVideos = Directory.GetFiles(tmp, "*.*", SearchOption.AllDirectories);

            // Cheat Codes
            HeTrace.WriteLine($"\tCheatCodes: {Default.CheatCodes}");
            tmp = Path.Combine(root, Default.CheatCodes);
            if (Directory.Exists(tmp))
                gdc.SetSCheatCodes = Directory.GetFiles(tmp, "*.*", SearchOption.AllDirectories).ToList();

            // Images
            HeTrace.WriteLine("\tImages");
            tmp = Path.Combine(root, Default.Images);
            if (Directory.Exists(tmp))
                gdc.Images = PrepareImages(tmp);


            return gdc;
        }

        public List<DataRepExt> PrepareImages(string root)
        {
            List<DataRepExt> files = new List<DataRepExt>();
            var folders = Directory.GetDirectories(root);

            foreach (string d in folders)
                foreach (string f in Directory.EnumerateFiles(d, "*.*", SearchOption.AllDirectories))
                    files.Add(new DataRepExt(Path.GetFileName(d), f));

            return files;
        }


        private void AssignTargets(GameDataCont gdC, string root, ContPlatFolders zePlatform)
        {
            string platFolderPath = zePlatform.FolderPath;
            if (string.IsNullOrEmpty(platFolderPath))
                platFolderPath = Path.Combine(Default.LaunchBoxPath, "Games");
            else
                platFolderPath = Path.GetFullPath(platFolderPath, Default.LaunchBoxPath);

            string appTarget = string.Empty;

            if (Default.wGameNameFolder)
                appTarget = Path.Combine(platFolderPath, Tool.WindowsConv_TitleToFileName(gdC.Title));
            else
                appTarget = platFolderPath;

            // Games
            foreach (var app in gdC.Applications)
                app.DestPath = app.CurrentPath.Replace(Path.Combine(root, Default.Games), appTarget);

            AssignTarget(gdC.Manuals, root, Default.Manuals, Tag.MediaTManual, zePlatform);
            AssignTarget(gdC.Musics, root, Default.Musics, Tag.MediaTMusic, zePlatform);
            AssignTarget(gdC.Videos, root, Default.Videos, Tag.MediaTVideo, zePlatform);

            //    string destPath = zePlatform.PlatformFolders.FirstOrDefault((x => x.MediaType.Equals(mediatype))).FolderPath;

            AssignImages(gdC.Images, root, zePlatform);


            void AssignTarget(IEnumerable<DataRep> elems, string root, string subFolder, string mediaType, ContPlatFolders zePlatform)
            {
                var dFolder = zePlatform.PlatformFolders.First(x => x.MediaType.Equals(mediaType));
                string destFolder = Path.GetFullPath(dFolder.FolderPath, Default.LaunchBoxPath);

                string src = Path.Combine(root, subFolder);

                foreach (var e in elems)
                    e.DestPath = e.CurrentPath.Replace(src, destFolder);

            }


            void AssignImages(List<DataRepExt> images, string root, ContPlatFolders zePlatform)
            {
                string prevMedType = string.Empty;
                string toReplace = string.Empty;
                string target = string.Empty;

                foreach (DataRepExt image in images)
                {
                    if (!prevMedType.Equals(image.Categorie))
                    {
                        prevMedType = image.Categorie;
                        toReplace = Path.Combine(root, Default.Images, image.Categorie);

                        PlatformFolder pTarget = zePlatform.PlatformFolders.FirstOrDefault(x => x.MediaType.Equals(image.Categorie));
                        target = Path.GetFullPath(pTarget.FolderPath, Default.LaunchBoxPath);
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

            //
            if (xmlGame == null)
                throw new Exception("No XML file to inject");

            // Vérification pour voir si le jeu est présent
            using (XML_Games xmlSrc = new XML_Games(xmlGame))
            using (XML_Games xmlPlat = new XML_Games(xmlPlatform))
            {
                bool? remove = false;
                if (xmlPlat.Exists(GameTag.ID, gdC.DefaultApp.Id))
                    remove = IHMStatic.AskDxMBox("Game is already present, remove it ? ", "Question", E_DxButtons.No | E_DxButtons.Yes, gdC.DefaultApp.Name);

                // ----------------- On enlève si désiré
                if (remove == true)
                {
                    xmlPlat.Remove_Game(gdC.DefaultApp.Id);
                    xmlPlat.Remove_AddApps(gdC.DefaultApp.Id);
                    xmlPlat.Remove_CustomF(gdC.DefaultApp.Id);
                    xmlPlat.Remove_AlternateN(gdC.DefaultApp.Id);
                }

                // ----------------- Traitement du jeu.
                XElement xelGame = xmlSrc.GetGameNode();

                // Modifications
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
                if (Default.wCustomFields)
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
                var value = elem == null ? string.Empty : DxPath.To_Relative(Default.LaunchBoxPath, elem.DestPath);
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

