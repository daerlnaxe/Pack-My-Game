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

namespace UnPack_My_Game.Cores
{
    /// <summary>
    /// Processus évolutif
    /// </summary>
    internal class DataPackGCore : C_LaunchBox
    {
        public DataPackGCore()
        {
            // Tracing
            MeSimpleLog log = new MeSimpleLog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.Logs, $"{DateTime.Now.ToFileTime()}.log"))
            {
                LogLevel = 1,
                FuncPrefix = EPrefix.Horodating,
            };

            log.AddCaller(this);
            HeTrace.AddLogger("C_LBDPG", log);

            /*

            MeEmit mee = new MeEmit()
            {
                ByPass = true,
            };
            mee.SignalWrite += (x, y)=> SetStatus(x, (StateArg)y);
            HeTrace.AddMessenger("mee", mee);*/
        }




        #region
        public override bool InjectGames(ICollection<DataRep> games)
        {
            try
            {
                foreach (var game in games)
                {
                    //string tmpPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(game.Name));

                    InjectGame(game.CurrentPath);
                }
                return true;
            }
            catch (Exception exc)
            {
                return false;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gamePath"></param>
        private void InjectGame(string gamePath)
        {
            string dpgFile = Path.Combine(gamePath, "DPGame.json");
            bool backupDone = false;

            // Check if DPG file exists
            if (!File.Exists(dpgFile))
            {
                DPGMakerCore dpgMaker = new DPGMakerCore();
                dpgMaker.MakeDPG_Folder(gamePath);
            }

            GamePaths gpX = GamePaths.ReadFromJson<GamePaths>(dpgFile);

            string platformsFile = Path.Combine(Default.LastLBpath, Default.fPlatforms);
            bool CheckIfInjectionNeeded = !XML_Custom.TestPresence(platformsFile, Tag.Platform, Tag.Name, gpX.Platform);
            if (CheckIfInjectionNeeded)
            {
                HeTrace.WriteLine($"Backup of platforms file");
                // Backup du fichier de la plateforme;
                Common_PMG.Tool.BackupFile(platformsFile,
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.BackUp));

                string newPFile = IHMStatic.GetAFile(Default.LastTargetPath, "Select the platform xml file", "xml");
                if (string.IsNullOrEmpty(newPFile))
                    throw new Exception("File for injection is not filled");

                HeTrace.WriteLine($"Injecting {gpX.Platform} in platforms file");
                InjectPlatform(gpX.Platform, newPFile);
            }


            Platform zePlatform = XML_Platforms.GetPlatformPaths(platformsFile, gpX.Platform, Default.LastLBpath);

            // Préparation des fichiers
            GameDataCont gdC = PrepareGDC(gamePath, gpX);

            // Manipulation des fichiers
            AssignTargets(gdC, gamePath, zePlatform);


            // Copie des fichiers (on s'appuie sur les dossiers de la plateforme)
            CopyFiles(gdC);


            // --- Lecture de la plateforme
            string machineXMLFile = Path.Combine(Default.LastLBpath, Default.dPlatforms, $"{gpX.Platform}.xml");
            if (!File.Exists(machineXMLFile))
                XML_Games.NewPlatform(machineXMLFile);

            // Backup platform file
            if (!backupDone)
            {
                //   BackupPlatformFile(machineXMLFile);
                HeTrace.WriteLine($"Backup of '{machineXMLFile}'");
                backupDone = true;
            }

            InjectInXMLFile(gamePath, gdC, machineXMLFile);


        }



        private GameDataCont PrepareGDC(string root, GamePaths gpX)
        {
            GameDataCont gdc = new GameDataCont(gpX.Title);

            string tmp = string.Empty;

            // Games
            tmp = Path.Combine(root, Default.Games);
            gdc.SetApplications = gpX.Applications.Select(x =>
                                        new DataPlus()
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
                                            CurrentPath = Path.GetFullPath(x.CurrentPath, tmp),
                                            IsSelected = x.IsSelected,
                                        }).ToList();

            gdc.SSetApplications = Directory.GetFiles(tmp, "*.*", SearchOption.AllDirectories).ToList();
            /*gdc.SetDefaultApplication = gpX.ApplicationPath == null ? null : Path.GetFullPath(gpX.ApplicationPath, tmp);
            gdc.SetApplications = Directory.GetFiles(tmp, "*.*", SearchOption.AllDirectories).ToList();*/

            // Manuals
            tmp = Path.Combine(root, Default.Manuals);
            gdc.SetDefaultManual = gpX.ManualPath == null ? null : Path.GetFullPath(gpX.ManualPath, tmp);
            gdc.SSetManuals = Directory.GetFiles(Path.Combine(tmp), "*.*", SearchOption.AllDirectories).ToList();

            // Musics
            tmp = Path.Combine(root, Default.Musics);
            gdc.SetDefaultMusic = gpX.MusicPath == null ? null : Path.GetFullPath(gpX.MusicPath, tmp);
            gdc.SSetMusics = Directory.GetFiles(tmp, "*.*", SearchOption.AllDirectories).ToList();

            // Videos
            tmp = Path.Combine(root, Default.Videos);
            gdc.SetDefaultVideo = gpX.VideoPath == null ? null : Path.GetFullPath(gpX.VideoPath, root);
            gdc.SetDefaultThemeVideo = gpX.ThemeVideoPath == null ? null : Path.GetFullPath(gpX.ThemeVideoPath, root);
            gdc.SSetVideos = Directory.GetFiles(tmp, "*.*", SearchOption.AllDirectories).ToList();

            // Cheat Codes
            tmp = Path.Combine(root, Default.CheatCodes);
            gdc.SSetCheatCodes = Directory.GetFiles(tmp, "*.*", SearchOption.AllDirectories).ToList();

            // Images
            tmp = Path.Combine(root, Default.Images);
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


        private void AssignTargets(GameDataCont gdC, string root, Platform zePlatform)
        {
            string appTarget = string.Empty;

            if (Default.wGameNameFolder)
                appTarget = Path.Combine(zePlatform.FolderPath, gdC.Title);
            else
                appTarget = zePlatform.FolderPath;

            // Games            
            gdC.Apps.ForEach(x =>
            x.DestPath = x.CurrentPath.Replace(Path.Combine(root, Default.Games), appTarget));

            AssignTarget(gdC.Manuals, root, Default.Manuals, Tag.MediaTManual, zePlatform);
            AssignTarget(gdC.Musics, root, Default.Musics, Tag.MediaTMusic, zePlatform);
            AssignTarget(gdC.Videos, root, Default.Videos, Tag.MediaTVideo, zePlatform);

            //    string destPath = zePlatform.PlatformFolders.FirstOrDefault((x => x.MediaType.Equals(mediatype))).FolderPath;

            AssignImages(gdC.Images, root, zePlatform);
        }


        private void AssignTarget(List<DataRep> elems, string root, string subFolder, string mediaType, Platform zePlatform)
        {
            var destFolder = zePlatform.PlatformFolders.First(x => x.MediaType.Equals(mediaType));
            string src = Path.Combine(root, subFolder);

            foreach (var e in elems)
                e.DestPath = e.CurrentPath.Replace(src, destFolder.FolderPath);

        }


        private void AssignImages(List<DataRepExt> images, string root, Platform zePlatform)
        {
            string prevMedType = string.Empty;
            string toReplace = string.Empty;
            PlatformFolder target = null;

            foreach (DataRepExt image in images)
            {
                if (!prevMedType.Equals(image.Categorie))
                {
                    prevMedType = image.Categorie;
                    toReplace = Path.Combine(root, Default.Images, image.Categorie);
                    target = zePlatform.PlatformFolders.FirstOrDefault(x => x.MediaType.Equals(image.Categorie));
                }

                image.DestPath = image.CurrentPath.Replace(toReplace, target.FolderPath);

            }

        }


        private void InjectInXMLFile(string gamePath, GameDataCont gdC, string xmlPlatform)
        {
            DataPlus defGame = gdC.Apps.FirstOrDefault(x => x.IsSelected);

            if (defGame == null)
                throw new Exception("No default game chosen");

            // Lecture du fichier EBGame
            string xmlGame = null;
            if (File.Exists(Path.Combine(gamePath, "TBGame.xml")))
                xmlGame = Path.Combine(gamePath, "TBGame.xml");
            else if (File.Exists(Path.Combine(gamePath, "EBGame.xml")))
                xmlGame = Path.Combine(gamePath, "EBGame.xml");


            if (xmlGame == null)
                throw new Exception("No XML file to inject");


            // Vérification pour voir si le jeu est présent
            using (XML_Games xmlSrc = new XML_Games(xmlGame))
            using (XML_Games xmlPlat = new XML_Games(xmlPlatform))
            {
                bool? remove = false;
                if (xmlPlat.Exists(GameTag.ID, defGame.Id))
                    remove = IHMStatic.AskDxMBox("Game is already present, remove it ? ", "Question", E_DxButtons.No | E_DxButtons.Yes, defGame.Name);

                // On enlève si désiré
                if (remove == true)
                {
                    xmlPlat.Remove_Game(defGame.Id);
                    xmlPlat.Remove_AddApps(defGame.Id);
                    xmlPlat.Remove_CustomF(defGame.Id);
                    xmlPlat.Remove_AlternateN(defGame.Id);
                }

                // --- Récupération du jeu.
                XElement xelGame = xmlSrc.GetGameNode();

                // Modifications
                // App
                xelGame.Element(Tag.AppPath).Value = DxPath.To_RelativeOrNull(Default.LastLBpath, defGame.DestPath);
                // Manuel
                xelGame.Element(Tag.ManPath).Value = gdC.DefaultManual == null ? string.Empty :
                        DxPath.To_RelativeOrNull(Default.LastLBpath, gdC.DefaultManual?.DestPath);
                // Musique
                xelGame.Element(Tag.MusPath).Value = gdC.DefaultMusic == null ? string.Empty :
                        DxPath.To_RelativeOrNull(Default.LastLBpath, gdC.DefaultMusic?.DestPath);
                // Video
                xelGame.Element(Tag.VidPath).Value = gdC.DefaultVideo == null ? string.Empty :
                        DxPath.To_RelativeOrNull(Default.LastLBpath, gdC.DefaultVideo?.DestPath);
                // ThemeVideo
                xelGame.Element(Tag.ThVidPath).Value = gdC.DefaultThemeVideo == null ? string.Empty :
                        DxPath.To_RelativeOrNull(Default.LastLBpath, gdC.DefaultThemeVideo.DestPath);

                // --- Récupération des clones + modification
                var xelClones = xmlSrc.GetNodes(Tag.AddApp);
                foreach (XElement clone in xelClones)
                {
                    var file = gdC.Apps.FirstOrDefault(x => x.Id == clone.Element(CloneTag.Id).Value);
                    if (file != null)
                        clone.Element(Tag.AppPath).Value = 
                            DxPath.To_RelativeOrNull(Default.LastLBpath, file.DestPath);
                }

                // --- Récupération des Custom Fields
                var xelCFields = xmlSrc.GetNodes(Tag.CustField);

                // --- Récupération des Alternate Names
                var xelANs = xmlSrc.GetNodes(Tag.AltName);

                // --------------------------------- Injection ------------------
                xmlPlat.InjectGame(xelGame);
                xmlPlat.InjectAddApps(xelClones);
                xmlPlat.InjectCustomF(xelCFields);
                xmlPlat.InjectAltName(xelANs);

                xmlPlat.Root.Save(xmlPlatform);
            }
            /*
                    elem.Value = DxPath.To_RelativeOrNull(Default.LastLBpath, defGame.DestPath);
            }


            // Modification du fichier xml pour l'injection
            */
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

