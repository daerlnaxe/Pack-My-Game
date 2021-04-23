using AsyncProgress;
using AsyncProgress.Basix;
using AsyncProgress.Cont;
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
using static UnPack_My_Game.Properties.Settings;


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
            mee.SignalWrite += (x, y ) => this.SetStatus(x, (StateArg)y );
            mee.SignalWriteLine += (x, y ) => this.SetStatus(x, (StateArg)y );
            HeTrace.AddMessenger("Mee", mee);
            
        }

        internal bool MakeDPG_Comp(IEnumerable<DataRep> archives)
        {
            // Check de LaunchBox
            if (!File.Exists(Path.Combine(Default.LaunchBoxPath, Default.fPlatforms)))
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

                    string gamePath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(zF.Name));
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

            if (gpX == null)
                throw new Exception("Impossible to continue, no data file available");

            GameDataCont gpC = (GameDataCont)gpX;
            GameDataCompletion(gpC, mode, archiveLink);


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


            IHMStatic.LaunchDouble(zippy, () => zippy.ExtractSpecificFiles(archive.CurrentPath, gamePath,
                                                        "TBGame.xml", "EBGame.xml", "DPGame.json"),
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

            IHMStatic.LaunchDouble(zippy, () => zippy.ExtractSpecificFiles(zF.CurrentPath, gamePath,
                                                        "TBGame.xml", "EBGame.xml", "DPGame.json"),
                                                        "SevenZip Extraction");
        }

        #endregion

        #region DPG



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
                /*gpX.ApplicationPath = TryToFindMedia(lbGame, lbGame.ApplicationPath);*/
                /*gpX.AddApplication( , TryToFindMedia(lbGame, lbGame.ApplicationPath));
                */
                foreach (var app in gpX.Applications)
                    app.CurrentPath = TryToFindMedia(gpX.Platform, app.CurrentPath);

                // --- 
                gpX.ManualPath = TryToFindMedia(gpX.Platform, gpX.ManualPath);
                gpX.MusicPath = TryToFindMedia(gpX.Platform, gpX.MusicPath);
                gpX.VideoPath = TryToFindMedia(gpX.Platform, gpX.VideoPath);
                gpX.ThemeVideoPath = TryToFindMedia(gpX.Platform, gpX.ThemeVideoPath);


            }

            return gpX;
        }

        private string TryToFindMedia(string platform, string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            string res = string.Empty;
            int pos = path.IndexOf(platform);

            /*if (pos < 0)
                return null;*/

            res = path.Substring(pos).Replace(platform, $".");

            HeTrace.WriteLine($"\t[{nameof(TryToFindMedia)}]: {path} | {res}");
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

                /*   gpC.SetApplications = GameDataCompletion(Files, PS.Default.Games, "/", "\\");
                   gpC.SetCheatCodes = GameDataCompletion(Files, PS.Default.CheatCodes, "/", "\\");
                   gpC.SetManuals = GameDataCompletion(Files, PS.Default.Manuals, "/", "\\");
                   gpC.SetMusics = GameDataCompletion(Files, PS.Default.Musics, "/", "\\");
                   gpC.SetVideos = GameDataCompletion(Files, PS.Default.Videos, "/", "\\");*/
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

            foreach (DataPlus app in gpC.Applications.ToList())
            {
                tmp = $@"{Default.Games}{app.Name?.Substring(1)}";
                if (!files.Contains(tmp))
                    gpC.RemoveApp(app);
            }

            /*tmp = $@"{PS.Default.Games}{gpC.DefaultApp?.CurrentPath?.Substring(1)}";
            if (!files.Contains(tmp))
                gpC.UnsetDefaultApp();*/

            if (!files.Contains($@"{Default.Manuals}{gpC.DefaultManual?.CurrentPath.Substring(1)}"))
                gpC.UnsetDefaultManual();

            if (!files.Contains($@"{Default.Musics}{gpC.DefaultMusic?.CurrentPath.Substring(1)}"))
                gpC.UnsetDefaultMusic();

            if (!files.Contains($@"{Default.Videos}{gpC.DefaultVideo?.CurrentPath.Substring(1)}"))
                gpC.UnsetDefaultVideo();

            if (!files.Contains($@"{Default.Videos}{gpC.DefaultThemeVideo?.CurrentPath.Substring(1)}"))
                gpC.UnsetDefaultThemeVideo();

            gpC.SSetApplications = GameDataCompletion(files, Default.Games, "\\", "\\");
            gpC.SSetCheatCodes = GameDataCompletion(files, Default.CheatCodes, "\\", "\\");
            gpC.SSetManuals = GameDataCompletion(files, Default.Manuals, "\\", "\\");
            gpC.SSetMusics = GameDataCompletion(files, Default.Musics, "\\", "\\");
            gpC.SSetVideos = GameDataCompletion(files, Default.Videos, "\\", "\\");
            //}
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
