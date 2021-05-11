using AsyncProgress;
using AsyncProgress.Basix;
using AsyncProgress.Cont;
using Common_Graph;
using Common_PMG.Container;
using Common_PMG.Container.Game;
using Common_PMG.Container.Game.LaunchBox;
using Common_PMG.XML;
using Hermes;
using Hermes.Messengers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using UnPack_My_Game.Decompression;
using UnPack_My_Game.Graph;
using static UnPack_My_Game.Common;


namespace UnPack_My_Game.Cores
{
    /// <summary>
    /// Tout ce qui est en rapport avec le DPG
    /// </summary>
    internal class DPGMakerCore : A_ProgressPersistD, I_ASBase
    {

        public Queue<DataRep> RejectedDatas { get; private set; } = new Queue<DataRep>();

        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        public CancellationToken CancelToken => TokenSource.Token;
        public bool CancelFlag { get; private set; }

        public bool IsPaused { get; set; }

        public bool IsInterrupted { get; private set; }

        public DPGMakerCore()
        {
            MeEmit mee = new MeEmit()
            {
                ByPass = true,  
            };
            mee.SignalWrite += (x, y) => this.SetStatus(x, new StateArg(y, false) );
            mee.SignalWriteLine += (x, y ) => this.SetStatus(x, new StateArg(y, false) );
            HeTrace.AddMessenger("Mee", mee);
            
        }

        internal bool MakeDPG_Comp(IEnumerable<DataRep> archives)
        {
            // Check de LaunchBox
            if (!File.Exists(Path.Combine(Config.HLaunchBoxPath, Config.PlatformsFile)))
            {
                DxTBoxCore.Box_MBox.DxMBox.ShowDial("Wrong LaunchBoxPath");
                return false;
            }

            try
            {
                // prérequis ? Doit être zip, 7zip

                foreach (DataRep zF in archives)
                {
                    ArchiveMode mode = ArchiveMode.None;

                    string gamePath = Path.Combine(Config.HWorkingFolder, Path.GetFileNameWithoutExtension(zF.Name));
                    string fileExt = Path.GetExtension(zF.CurrentPath).TrimStart('.');

                    // Création du dossier de destination
                    Directory.CreateDirectory(gamePath);

                    // Détection du mode
                    if (fileExt.Equals("zip", StringComparison.OrdinalIgnoreCase))
                        mode = ArchiveMode.Zip;
                    if (fileExt.Equals("7zip", StringComparison.OrdinalIgnoreCase) || fileExt.Equals("7z", StringComparison.OrdinalIgnoreCase))
                        mode = ArchiveMode.SevenZip;
                    else
                        RejectedDatas.Enqueue(zF); // pas sur que ça serve

                    //
                    if (mode == ArchiveMode.Zip)
                        DPGZipCore(zF, gamePath);
                    else if (mode == ArchiveMode.SevenZip)
                        DPG7ZipCore(zF, gamePath);

                    MakeDPG(gamePath, mode, zF.CurrentPath);

                }
                return true;
            }
            catch (Exception exc)
            {
                HeTrace.WriteLine(exc.Message);
                return false;
            }
            finally
            {
            }
        }


        internal bool MakeDPG_Folders(ICollection<DataRep> folders)
        {

            try
            {
                foreach (DataRep game in folders)
                {
                    MakeDPG_Folder(game.CurrentPath);
                }

                return true;
            }
            catch (Exception exc)
            {
                HeTrace.WriteLine(exc.Message);
                HeTrace.WriteLine(exc.StackTrace);
                return false;
            }
        }

        internal bool MakeDPG_Folder(string path)
        {
            MakeDPG(path, ArchiveMode.Folder, path);

            return true;
        }


        internal bool MakeDPG(string gamePath, ArchiveMode mode, string archiveLink)
        {
            GamePaths gpX = null;

            // Lecture des fichiers
            if (File.Exists(Path.Combine(gamePath, "DPGame.json")))
            {
                HeTrace.WriteLine("DPG Found");
                gpX = GamePaths.ReadFromJson(Path.Combine(gamePath, "DPGame.json"));
            }
            else if (File.Exists(Path.Combine(gamePath, "EBGame.xml")))
            {
                HeTrace.WriteLine("DPG Missing, work with EBGame");
                gpX = GetMainsInfo(gamePath, "EBGame.xml");
            }
            else if (File.Exists(Path.Combine(gamePath, "TBGame.xml")))
            {
                HeTrace.WriteLine("DPG Missing, work with TBGame");
                gpX = GetMainsInfo(gamePath, "TBGame.xml");
            }
            else if (File.Exists(Path.Combine(gamePath, "NBGame.xml")))
            {
                HeTrace.WriteLine("DPG Missing, work with NBGame");
                gpX = GetMainsInfo(gamePath, "NBGame.xml");
            }

            if (gpX == null)
                throw new Exception("Impossible to continue, no data file available");

            GameDataCont gpC = (GameDataCont)gpX;
            GameDataCompletion(gpC, mode, archiveLink);

            if (gpC.Applications.Count <= 0)
                throw new Exception("No game to inject");

            // Affichage
            IHMStatic.ShowDPG(gpC, gpX, gamePath);

            // Sauvegarde
            gpX.WriteToJson(Path.Combine(gamePath, "DPGame.json"));

            HeTrace.WriteLine("DPG Done");

            return true;
        }



        #region Compression
        void DPGZipCore(DataRep archive, string gamePath)
        {
            // Extraction des fichiers xml
            ZipDecompression zippy = new ZipDecompression()
            {
                TokenSource = this.TokenSource,
                IsPaused = this.IsPaused,
            };


            SafeBoxes.LaunchDouble(zippy, () => zippy.ExtractSpecificFiles(archive.CurrentPath, gamePath,
                                                        "NBGame.xml","TBGame.xml", "EBGame.xml", "DPGame.json"),
                                                        "Zip Extraction");
            /*var res = PackMe_IHM.ZipCompressFolder(zippy, () => zippy.CompressFolder(
                                         gamePath, title, PS.Default.cZipCompLvl), "Compression Zip");*/

        }

        void DPG7ZipCore(DataRep zF, string gamePath)
        {
            // Extraction des fichiers xml
            SevenZipDecompression zippy = new SevenZipDecompression()
            {
                TokenSource = this.TokenSource,
                IsPaused = this.IsPaused,
            };

            SafeBoxes.LaunchDouble(zippy, () => zippy.ExtractSpecificFiles(zF.CurrentPath, gamePath,
                                                        "NBGame.xml","TBGame.xml", "EBGame.xml", "DPGame.json"),
                                                        "SevenZip Extraction");
        }

        #endregion

        #region DPG


        /// <summary>
        /// On récupère les informations principales, dont les chemins (EBGame, TBGame)
        /// </summary>
        /// <param name="gamePath"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <remarks>
        /// Si les chemins sont vides, on conserve quand même pas de tri
        /// </remarks>
        private GamePaths GetMainsInfo(string gamePath, string file)
        {
            string xmlFile = Path.Combine(gamePath, file);

            GamePaths gpX = null;
            using (XML_Games xelGames = new XML_Games(xmlFile))
            {
                var xelG = xelGames.GetGameNode();

                gpX = GamePaths.CreateBasic(xelG);
                gpX.Complete(xelGames.GetNodes(Tag.AddApp, Tag.GameId, gpX.Id));

                // Essaie de récupération des paths
                foreach (var app in gpX.Applications)
                    app.CurrentPath = SimuRelativePath(gpX.Platform, app.CurrentPath);

                // --- 
                gpX.ManualPath = SimuRelativePath(gpX.Platform, gpX.ManualPath);
                gpX.MusicPath = SimuRelativePath(gpX.Platform, gpX.MusicPath);
                gpX.VideoPath = SimuRelativePath(gpX.Platform, gpX.VideoPath);
                gpX.ThemeVideoPath = SimuRelativePath(gpX.Platform, gpX.ThemeVideoPath);


            }

            return gpX;
        }

        private string SimuRelativePath(string platform, string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            string res = string.Empty;
            int pos = path.IndexOf(platform);

            /*if (pos < 0)
                return null;*/

            res = path.Substring(pos).Replace(platform, $".");

            HeTrace.WriteLine($"\t[{nameof(SimuRelativePath)}]: {path} | {res}");
            return res;
        }

        /*
        private void SetSelected(GamePaths gpX, GameDataCont gpC)
        {
            var app = gpC.Apps.FirstOrDefault(x => x.ALinkToThePath.Equals(gpX.ApplicationPath));
            if (app != null)
                app.IsSelected = true;
            var manual = gpC.Apps.FirstOrDefault(x => x.ALinkToThePath.Equals(gpX.ApplicationPath));
            var music = gpC.Apps.FirstOrDefault(x => x.ALinkToThePath.Equals(gpX.ApplicationPath));
            var video = gpC.Apps.FirstOrDefault(x => x.ALinkToThePath.Equals(gpX.ApplicationPath));
            var themevideo = gpC.Apps.FirstOrDefault(x => x.ALinkToThePath.Equals(gpX.ApplicationPath));
        }*/

        /// <summary>
        /// On rajoute les fichiers présents sur le disque dur
        /// </summary>
        /// <param name="gpC"></param>
        /// <param name="mode"></param>
        /// <param name="archiveName"></param>
        private void GameDataCompletion(GameDataCont gpC, ArchiveMode mode, string archiveName)
        {
            IEnumerable<string> files = null;
            if (mode == ArchiveMode.Zip)
            {
                files = ZipDecompression.StaticGetAllFiles(archiveName);
                files = files.Select(f => f.Replace('/', '\\'));
            }
            else if (mode == ArchiveMode.SevenZip)
            {
                files = SevenZipDecompression.StaticGetAllFiles(archiveName);
            }
            else if (mode == ArchiveMode.Folder)
            {
                files = Directory.GetFiles(archiveName, "*.*", SearchOption.AllDirectories);
                files = files.Select(f => f.Replace($@"{archiveName}\", string.Empty));
            }
            else
            {
                throw new Exception("Not supported");
            }


            // Vérification des fichiers entrés via le GamePaths;
            string tmp;

         //   gpC.SSetApplications = GameDataCompletion(files, Default.Games, "\\", "\\");
            foreach (DataPlus app in gpC.Applications.ToList())
            {
                tmp = $@"{Config.Games}{app.CurrentPath?.Substring(1)}";

                // Récupération du fichier correspondant
                var f = files.FirstOrDefault(x => x.StartsWith(tmp));

                // Si correspondance trouvée
                if (!string.IsNullOrEmpty(f))                
                    continue;

                HeTrace.WriteLine($"File not found {app.CurrentPath}");               
                f = files.FirstOrDefault(x => Path.GetFileName(x).Equals(Path.GetFileName(app.CurrentPath)));

                // Si remplacement possible
                if(!string.IsNullOrEmpty(f))
                {
                    app.CurrentPath = f.Replace(Config.Games, ".");
                    HeTrace.WriteLine($"Changed by {app.CurrentPath}");
                }
                else
                {
                    HeTrace.WriteLine("Removed");
                    gpC.RemoveApp(app);
                }

            }


            gpC.SetSCheatCodes = GameDataCompletion(files, Config.CheatCodes, "\\", "\\");
            // Manuals
            var tmp2 = $@"{Config.Manuals}{gpC.DefaultManual?.CurrentPath.Substring(1)}";
            if (!files.Contains(tmp2))
                gpC.UnsetDefaultManual();
            gpC.AddSManuals = GameDataCompletion(files, Config.Manuals, "\\", "\\");
            // Musics
            if (!files.Contains($@"{Config.Musics}{gpC.DefaultMusic?.CurrentPath.Substring(1)}"))
                gpC.UnsetDefaultMusic();
            gpC.AddSMusics = GameDataCompletion(files, Config.Musics, "\\", "\\");
            // Videos
            if (!files.Contains($@"{Config.Videos}{gpC.DefaultVideo?.CurrentPath.Substring(1)}"))
                gpC.UnsetDefaultVideo();

            if (!files.Contains($@"{Config.Videos}{gpC.DefaultThemeVideo?.CurrentPath.Substring(1)}"))
                gpC.UnsetDefaultThemeVideo();

            gpC.AddSVideos = GameDataCompletion(files, Config.Videos, "\\", "\\");
        }

        private List<string> GameDataCompletion(IEnumerable<string> files, string mediatype, string sep, string repSep)
        {
            List<string> res = new List<string>();
            foreach (string f in files)
            {
                if (f.Contains($"{mediatype}{sep}"))
                {
                    string tmp = f.Replace($"{mediatype}{sep}", $".{repSep}");//.Replace(sep, repSep);
                    res.Add(tmp);
                }
            }
            return res;
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

        /*public  void StopTask()
        {
            throw new NotImplementedException();
        }*/
    }
}
