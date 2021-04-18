using AsyncProgress;
using AsyncProgress.Basix;
using Common_PMG.Container;
using Common_PMG.Container.Game;
using Common_PMG.Container.Game.LaunchBox;
using Common_PMG.XML;
using Hermes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using UnPack_My_Game.Decompression;
using UnPack_My_Game.Graph;
using PS = UnPack_My_Game.Properties.Settings;

namespace UnPack_My_Game.Cores
{
    /// <summary>
    /// Tout ce qui est en rapport avec le DPG
    /// </summary>
    internal class DPGCore : A_ProgressPersistD, I_ASBase
    {

        public Queue<DataRep> RejectedDatas { get; private set; } = new Queue<DataRep>();

        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        public CancellationToken CancelToken => TokenSource.Token;
        public bool CancelFlag { get; private set; }

        public bool IsPaused { get; set; }

        public bool IsInterrupted { get; private set; }


        internal bool MakeFileDPG(IEnumerable<DataRep> archives)
        {
            try
            {
                // prérequis ? Doit être zip, 7zip | ou dossier (A venir)

                foreach (DataRep zF in archives)
                {
                    ArchiveMode mode = ArchiveMode.None;

                    string gamePath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(zF.Name));
                    string fileExt = Path.GetExtension(zF.ALinkToThePath).TrimStart('.');
                    GamePaths gpX = null;
                    GameDataCont gpC = new GameDataCont($"tmp-{zF.Name}");

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

#if DEBUG
                    Process.Start("explorer.exe", gamePath);
#endif

                    GameDataCompletion(gpC, mode, zF.ALinkToThePath);
                    // Lecture des fichiers
                    if (File.Exists(Path.Combine(gamePath, "DPGame.json")))
                    {
                        gpX = GamePaths.ReadFromJson<GamePaths>(Path.Combine(gamePath, "DPGame.json"));
                    }
                    else if (File.Exists(Path.Combine(gamePath, "EBGame.xml")))
                    {
                        gpX = GetMainsInfo(gamePath, "EBGame.xml", gpC);
                    }
                    else if (File.Exists(Path.Combine(gamePath, "TBGame.xml")))
                    {
                        gpX = GetMainsInfo(gamePath, "TBGame.xml", gpC);
                    }

                    if (gpX == null)
                        return false;

                    //gpC = (GameDataCont)gpX;
                    gpC.Title = gpX.Title;


                    // Affichage
                    IHMStatic.ShowDPG(gpC, gpX, gamePath);

                    // Sauvegarde
                    gpX.WriteToJson(Path.Combine(gamePath, "DPGame.json"));
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

        internal bool MakeDpg(DataRep zipFile)
        {
            try
            {
                // ---

                return true;
            }
            catch (Exception exc)
            {
                HeTrace.WriteLine(exc.Message);
                return false;
            }
        }

        #region Compression
        void DPGZipCore(DataRep archive, string gamePath)
        {
            // Lecture des préréquis ? <= pas utile je crois

            // Extraction des fichiers xml
            ZipDecompression zippy = new ZipDecompression()
            {
                TokenSource = this.TokenSource,
                IsPaused = this.IsPaused,
            };


            IHMStatic.LaunchDouble(zippy, () => zippy.ExtractSpecificFiles(archive.ALinkToThePath, gamePath,
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

            IHMStatic.LaunchDouble(zippy, () => zippy.ExtractSpecificFiles(zF.ALinkToThePath, gamePath,
                                                        "TBGame.xml", "EBGame.xml", "DPGame.json"),
                                                        "SevenZip Extraction");
        }

        #endregion

        #region DPG



        private GamePaths GetMainsInfo(string gamePath, string file, GameDataCont gpC)
        {
            string xmlFile = Path.Combine(gamePath, file);
            var lbGame = XML_Games.Scrap_LBGame(xmlFile);
            GamePaths gpX = GamePaths.CreateBasic(lbGame);

            // Essaie de récupération des paths
            gpX.ApplicationPath = TryToFindMedia(lbGame, lbGame.ApplicationPath, gpC.Apps);
            gpX.ManualPath = TryToFindMedia(lbGame, lbGame.ManualPath, gpC.Manuals);
            gpX.MusicPath = TryToFindMedia(lbGame, lbGame.MusicPath, gpC.Musics);
            gpX.VideoPath = TryToFindMedia(lbGame, lbGame.VideoPath, gpC.Videos);
            gpX.ThemeVideoPath = TryToFindMedia(lbGame, lbGame.ThemeVideoPath, gpC.Videos);

            T'as oublié d'initializer les default pour gamedateacon
            return gpX;
        }

        private string TryToFindMedia(LBGame lbGame, string path, List<DataRep> elems)
        {
            string res = string.Empty;
            int pos = path.IndexOf(lbGame.Platform);

            if (pos < 0)
                return string.Empty;

            res = path.Substring(pos).Replace(lbGame.Platform, $".") ;

            var found = elems.FirstOrDefault((x)=> x.ALinkToThePath == res);

            if (found == null)
                return string.Empty;

            found.IsSelected = true;

            return found.ALinkToThePath;
        }


        private void SetSelected(GamePaths gpX, GameDataCont gpC)
        {
            var app = gpC.Apps.FirstOrDefault(x => x.ALinkToThePath.Equals(gpX.ApplicationPath));
            if (app != null)
                app.IsSelected = true;
            var manual = gpC.Apps.FirstOrDefault(x => x.ALinkToThePath.Equals(gpX.ApplicationPath));
            var music = gpC.Apps.FirstOrDefault(x => x.ALinkToThePath.Equals(gpX.ApplicationPath));
            var video = gpC.Apps.FirstOrDefault(x => x.ALinkToThePath.Equals(gpX.ApplicationPath));
            var themevideo = gpC.Apps.FirstOrDefault(x => x.ALinkToThePath.Equals(gpX.ApplicationPath));
        }


        private void GameDataCompletion(GameDataCont gpC, ArchiveMode mode, string archiveName)
        {
            IEnumerable<string> Files = null;
            if (mode == ArchiveMode.Zip)
            {
                Files = ZipDecompression.StaticGetAllFiles(archiveName);

                gpC.SetApplications = GameDataCompletion(Files, PS.Default.Games, "/", "\\");
                gpC.SetCheatCodes = GameDataCompletion(Files, PS.Default.CheatCodes, "/", "\\");
                gpC.SetManuals = GameDataCompletion(Files, PS.Default.Manuals, "/", "\\");
                gpC.SetMusics = GameDataCompletion(Files, PS.Default.Musics, "/", "\\");
                gpC.SetVideos = GameDataCompletion(Files, PS.Default.Videos, "/", "\\");
            }
            else if (mode == ArchiveMode.SevenZip)
            {
                Files = SevenZipDecompression.StaticGetAllFiles(archiveName);

                gpC.SetApplications = GameDataCompletion(Files, PS.Default.Games, "\\", "\\");
                gpC.SetCheatCodes = GameDataCompletion(Files, PS.Default.CheatCodes, "\\", "\\");
                gpC.SetManuals = GameDataCompletion(Files, PS.Default.Manuals, "\\", "\\");
                gpC.SetMusics = GameDataCompletion(Files, PS.Default.Musics, "\\", "\\");
                gpC.SetVideos = GameDataCompletion(Files, PS.Default.Videos, "\\", "\\");
            }
        }

        private List<string> GameDataCompletion(IEnumerable<string> files, string mediatype, string sep, string repSep)
        {
            List<string> res = new List<string>();
            foreach (string f in files)
            {
                if (f.Contains($"{mediatype}{sep}"))
                {
                    string tmp = f.Replace($"{mediatype}{sep}", $".{repSep}").Replace(sep, repSep);
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
