using DxLocalTransf;
using DxTBoxCore.Box_Progress;
using DxTBoxCore.BoxChoose;
using HashCalc;
using Common_PMG.Container;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Common_PMG.Container.Game;
using PS = UnPack_My_Game.Properties.Settings;

namespace UnPack_My_Game.Graph.LaunchBox
{

    public class M_DPGMaker
    {
        public static event DoubleHandler UpdateProgress;
        public static event MessageHandler UpdateStatus;
        public static event DoubleHandler MaximumProgress;

        public string Root { get; private set; }

      //  public Language Lang => Common.ObjectLang;

        public DataRep ChosenGame { get; set; }
        public DataRep SelectedGame { get; set; }
        public ObservableCollection<DataRep> GamesCollection { get; set; } = new ObservableCollection<DataRep>();

        public DataRep ChosenManual { get; set; }
        public DataRep SelectedManual { get; set; }
        public ObservableCollection<DataRep> ManualsCollection { get; set; } = new ObservableCollection<DataRep>();

        public DataRep ChosenMusic { get; set; }
        public DataRep SelectedMusic { get; set; }
        public ObservableCollection<DataRep> MusicsCollection { get; set; } = new ObservableCollection<DataRep>();

        public DataRep ChosenVideo { get; set; }
        public DataRep SelectedVideo { get; set; }
        public ObservableCollection<DataRep> VideosCollection { get; set; } = new ObservableCollection<DataRep>();

        public DataRep ChosenCheatF { get; set; }
        public string SelectedCheatFile { get; set; }
        public ObservableCollection<DataRep> CheatsCollection { get; set; } = new ObservableCollection<DataRep>();

        /// <summary>
        /// Contient les chemins de la plateforme
        /// </summary>
        public string Platform { get; set; }

        public string GameName { get; }
        public GamePaths GamePaths { get; }

        public M_DPGMaker(string root)
        {
            Root = root;


            LoadFiles();
        }

        #region initialisation
        internal void LoadFiles()
        {
            LoadGames();
            LoadManuals();
            LoadMusics();
            LoadVideos();
            LoadCheatCodes();
        }

        private void LoadGames()
        {
            GamesCollection.Clear();
            string gamesPath = Path.Combine(Root, PS.Default.Games);
            foreach (string f in Directory.EnumerateFiles(gamesPath, "*.*", SearchOption.TopDirectoryOnly))
            {
                string tmp = f.Replace(gamesPath, ".");
                if (tmp.Equals(GamePaths.ApplicationPath))
                {
                    ChosenGame = new DataRep(tmp, f);
                    continue;
                }

                GamesCollection.Add(new DataRep(tmp, f));
            }
        }

        private void LoadManuals()
        {
            ManualsCollection.Clear();
            string manualsPath = Path.Combine(Root, PS.Default.Manuals);
            foreach (string f in Directory.EnumerateFiles(manualsPath, "*.*", SearchOption.AllDirectories))
            {
                string tmp = f.Replace(manualsPath, ".");
                if (tmp.Equals(GamePaths.ManualPath))
                {
                    ChosenManual = new DataRep(tmp, f);
                    continue;
                }

                ManualsCollection.Add(new DataRep(tmp, f));
            }
        }


        private void LoadMusics()
        {
            MusicsCollection.Clear();
            string musicsPath = Path.Combine(Root, PS.Default.Musics);
            foreach (string f in Directory.EnumerateFiles(musicsPath, "*.*", SearchOption.AllDirectories))
            {
                string tmp = f.Replace(musicsPath, ".");
                if (tmp.Equals(GamePaths.MusicPath))
                {
                    ChosenMusic = new DataRep(tmp, f);
                    continue;
                }

                MusicsCollection.Add(new DataRep(tmp, f));
            }
        }

        private void LoadVideos()
        {
            VideosCollection.Clear();
            string videosPath = Path.Combine(Root, PS.Default.Videos);
            foreach (string f in Directory.EnumerateFiles(videosPath, "*.*", SearchOption.AllDirectories))
            {
                string tmp = f.Replace(videosPath, ".");
                if (tmp.Equals(GamePaths.VideoPath))
                {
                    ChosenVideo = new DataRep(tmp, f);
                    continue;
                }

                VideosCollection.Add(new DataRep(tmp, f));
            }
        }

        internal void LoadCheatCodes()
        {
            string cheatsPath = Path.Combine(Root, PS.Default.CheatCodes);
            CheatsCollection.Clear();
            foreach (string f in Directory.EnumerateFiles(cheatsPath, "*.*", SearchOption.TopDirectoryOnly))
            {
                string tmp = f.Replace(cheatsPath, ".");
                CheatsCollection.Add(new DataRep(tmp, f));
            }
        }

        #endregion

        /// <summary>
        /// Fonction de copie des fichiers
        /// </summary>
        /// <param name="sourceDirectory">Répertoire source</param>
        /// <param name="targetDirectory">Répertoire cible</param>
        public void CopyGameF()
        {/*
            TreeChoose tc = new TreeChoose()
            {
                Model = new M_ChooseRaw()
                {
                    Mode = ChooseMode.File,
                    ShowFiles = true,
                    StartingFolder = Platform.FolderPath,
                    Info = "Select a manual file",
                }
            };*/
            if (Copy2(PS.Default.LastSpath , PS.Default.Games, "Select a game file"))
            {
                LoadGames();
            }
        }



        internal void RemoveGameF()
        {            
            /*if (!string.IsNullOrEmpty(SelectedGame))
            {
                OpDFiles.Trash(Path.Combine(Root, Common.Games, SelectedGame));
                LoadGames();
            }*/
            if (SelectedGame != null)
            {
                OpDFiles.Trash(SelectedGame.ALinkToThePast);
                LoadGames();
            }
        }


        #region Manuals
        internal void CopyManualF()
        {
            if (Copy2(PS.Default.LastSpath, PS.Default.Manuals, "Select a manual"))
            {
                LoadManuals();
            }
        }

        internal void OpenManual()
        {
            if (SelectedManual == null)
                return;

            //string path = Path.Combine(Root, Common.Manuals, SelectedManual);
            Process p = new Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = SelectedManual.ALinkToThePast;
            p.Start();

        }

        internal void LaunchPage(string page)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = @$"{page}{GameName.Replace(' ', '+')}";
            p.Start();
        }

        internal void LoadFolder()
        {
            Process.Start("explorer.exe", Root);
        }

        internal void RemoveManualF()
        {
            if (SelectedManual != null)
            {
                OpDFiles.Trash(SelectedManual.ALinkToThePast);
                LoadManuals();
            }
        }
        #endregion

        #region Musique
        internal void CopyMusicF()
        {

            if (Copy2(PS.Default.LastSpath, PS.Default.Musics, "Select a music file"))
            {
                LoadMusics();
            }
        }

        internal void OpenMusic()
        {
            if (SelectedMusic == null)
                return;

            //string path = Path.Combine(Root, Common.Musics, SelectedMusic);
            Process p = new Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = SelectedMusic.ALinkToThePast;
            p.Start();

        }

        internal void RemoveMusicF()
        {
            if (SelectedMusic != null)
            {
                OpDFiles.Trash(SelectedMusic.ALinkToThePast);
                LoadMusics();
            }
        }
        #endregion

        #region Videos

        internal void OpenVideo()
        {
            if (SelectedVideo == null)
                return;

            //string path = Path.Combine(Root, Common.Videos, SelectedVideo);
            Process p = new Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = SelectedVideo.ALinkToThePast;
            p.Start();
        }

        internal void CopyVideoF()
        {
            if (Copy2(PS.Default.LastSpath, PS.Default.Videos, "Select a video file"))
            {
                LoadVideos();
            }
            /*

            TreeChoose tc = new TreeChoose()
            {
                Model = new M_ChooseRaw()
                {
                    Mode = ChooseMode.File,
                    ShowFiles = true,
                    StartingFolder = folder,
                }
            };

            if (tc.ShowDialog() == true)
            {
                VideosCollection.Add(Path.GetFileName(tc.LinkResult));
            }*/
        }

        internal void RemoveVideoF()
        {
            if (SelectedVideo != null)
            {
                OpDFiles.Trash(SelectedVideo.ALinkToThePast);
                LoadVideos();
            }
        }

        #endregion

        #region Cheats

        internal void CopyAstuceF()
        {
            if (Copy2(PS.Default.LastLBpath, PS.Default.CheatCodes, "Select a cheat codes file"))
            {
                LoadCheatCodes();
            }
        }

        internal void OpenCheat()
        {
            if (String.IsNullOrEmpty(SelectedCheatFile))
                return;

            string path = Path.Combine(Root, PS.Default.CheatCodes, SelectedCheatFile);
            if (File.Exists(path))
            {
                Process.Start(path);
            }
        }


        internal void RemoveCheatF()
        {
            if (!string.IsNullOrEmpty(SelectedCheatFile))
            {
                OpDFiles.Trash(Path.Combine(Root, PS.Default.CheatCodes, SelectedCheatFile));
                LoadCheatCodes();
            }
        }
        #endregion


        private bool Copy2(string srcFolder, string subFolder, string message)
        {
            TreeChoose tc = new TreeChoose()
            {
                Model = new M_ChooseRaw()
                {
                    Info = message,
                    Mode = ChooseMode.File,
                    ShowFiles = true,
                    StartingFolder = srcFolder
                }
            };
            if (tc.ShowDialog() == true)
            {
                return Copy(tc.LinkResult, Path.Combine(Root, subFolder));
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSrc"></param>
        /// <param name="destFolder"></param>
        private bool Copy(string fileSrc, string destFolder)
        {

            string destFile = Path.Combine(destFolder, Path.GetFileName(fileSrc));

            if (File.Exists(destFile))
                return false;

            // --- Copie
            UpdateStatus(this, $"Copy {fileSrc}");
            UpdateProgress?.Invoke(this, 0);
            MaximumProgress?.Invoke(this, 1);
            File.Copy(fileSrc, destFile);
            UpdateProgress?.Invoke(this, 1);

            // --- Vérification des sommes
            UpdateStatus(this, $"Copy verification");
            MaximumProgress?.Invoke(this, 100);

            DxProgressB1 progressBox = new DxProgressB1();

            //bool? res = _ObjectFiles.VerifByHash_Sync(fileSrc, destFile, () => MD5.Create());
            OpDFilesExt objectFiles = new OpDFilesExt();
            objectFiles.SignalProgression += (x, y) => progressBox.CurrentProgress = y;
            objectFiles.Finished += ((x) => Application.Current.Dispatcher?.Invoke
                                                        (() => { progressBox.Close(); }));

            EFileResult res = EFileResult.None;
            Task.Run(() => res = objectFiles.DeepVerif(fileSrc, destFile, () => MD5.Create()));

            progressBox.ShowDialog();

            UpdateStatus?.Invoke(this, $"Check verif: {res}");
            return true;
        }
    }
}
