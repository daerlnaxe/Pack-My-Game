using DxLocalTransf;
using DxTBoxCore.Box_Progress;
using DxTBoxCore.BoxChoose;
using HashCalc;
using LaunchBox_XML.Container;
using Pack_My_Game.Cont;
using Pack_My_Game.Files;
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

namespace Pack_My_Game.Models
{

    public class M_PackMeRes
    {
        public static event DoubleHandler UpdateProgress;
        public static event MessageHandler UpdateStatus;
        public static event DoubleHandler MaximumProgress;

        public string Root { get; private set; }

        public Language Lang => Common.ObjectLang;

        public string SelectedGame { get; set; }
        public ObservableCollection<string> GamesCollection { get; set; } = new ObservableCollection<string>();

        public Filerep SelectedManual { get; set; }
        public ObservableCollection<Filerep> ManualsCollection { get; set; } = new ObservableCollection<Filerep>();

        public Filerep SelectedMusic { get; set; }
        public ObservableCollection<Filerep> MusicsCollection { get; set; } = new ObservableCollection<Filerep>();

        public Filerep SelectedVideo { get; set; }
        public ObservableCollection<Filerep> VideosCollection { get; set; } = new ObservableCollection<Filerep>();

        public string SelectedCheatFile { get; set; }
        public ObservableCollection<string> CheatsCollection { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// Contient les chemins de la plateforme
        /// </summary>
        public Platform Platform { get; }

        public string GameName { get; }

        public M_PackMeRes(string root, Platform platform, string gameName)
        {
            Root = root;
            Platform = platform;
            GameName = gameName;

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
            foreach (string f in Directory.EnumerateFiles(Path.Combine(Root, Common.Games), "*.*", SearchOption.TopDirectoryOnly))
                GamesCollection.Add(Path.GetFileName(f));

        }

        private void LoadManuals()
        {
            ManualsCollection.Clear();
            string manualsPath = Path.Combine(Root, Common.Manuals);
            foreach (string f in Directory.EnumerateFiles(manualsPath, "*.*", SearchOption.AllDirectories))
                ManualsCollection.Add(new Filerep(f.Replace($"{manualsPath}\\", string.Empty), f));
        }


        private void LoadMusics()
        {
            MusicsCollection.Clear();
            string musicsPath = Path.Combine(Root, Common.Musics);
            foreach (string f in Directory.EnumerateFiles(musicsPath, "*.*", SearchOption.AllDirectories))
                MusicsCollection.Add(new Filerep(f.Replace($"{musicsPath}\\", string.Empty), f));
        }

        private void LoadVideos()
        {
            VideosCollection.Clear();
            string videosPath = Path.Combine(Root, Common.Videos);
            foreach (string f in Directory.EnumerateFiles(videosPath, "*.*", SearchOption.AllDirectories))
                VideosCollection.Add(new Filerep(f.Replace($"{videosPath}\\", string.Empty), f));
        }

        internal void LoadCheatCodes()
        {
            CheatsCollection.Clear();
            foreach (string f in Directory.EnumerateFiles(Path.Combine(Root, Common.CheatCodes), "*.*", SearchOption.TopDirectoryOnly))
                CheatsCollection.Add(Path.GetFileName(f));
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
            if (Copy2(Platform.FolderPath, Common.Games, "Select a game file"))
            {
                LoadGames();
            }
        }



        internal void RemoveGameF()
        {
            if (!string.IsNullOrEmpty(SelectedGame))
            {
                OpDFiles.Trash(Path.Combine(Root, Common.Games, SelectedGame));
                LoadGames();
            }
        }


        #region Manuals
        internal void CopyManualF()
        {
            PlatformFolder pFolder = Platform.PlatformFolders.FirstOrDefault((x) => x.MediaType == "Manual");
            string folder = pFolder == null ? string.Empty : pFolder.FolderPath;

            if (Copy2(folder, Common.Manuals, "Select a manual"))
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
            p.StartInfo.FileName = SelectedManual.PathLink;
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
                OpDFiles.Trash(SelectedManual.PathLink);
                LoadManuals();
            }
        }
        #endregion

        #region Musique
        internal void CopyMusicF()
        {
            PlatformFolder pFolder = Platform.PlatformFolders.FirstOrDefault((x) => x.MediaType == "Music");
            string folder = pFolder == null ? string.Empty : pFolder.FolderPath;

            if (Copy2(folder, Common.Musics, "Select a music file"))
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
            p.StartInfo.FileName = SelectedMusic.PathLink;
            p.Start();

        }

        internal void RemoveMusicF()
        {
            if (SelectedMusic != null)
            {
                OpDFiles.Trash(SelectedMusic.PathLink);
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
            p.StartInfo.FileName = SelectedVideo.PathLink;
            p.Start();
        }

        internal void CopyVideoF()
        {
            PlatformFolder pFolder = Platform.PlatformFolders.FirstOrDefault((x) => x.MediaType == "Video");
            string folder = pFolder == null ? string.Empty : pFolder.FolderPath;

            if (Copy2(folder, Common.Videos, "Select a video file"))
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
                OpDFiles.Trash(SelectedVideo.PathLink);
                LoadVideos();
            }
        }

        #endregion

        #region Cheats

        internal void CopyAstuceF()
        {
            if (Copy2(Properties.Settings.Default.LastKPath, Common.CheatCodes, "Select a cheat codes file"))
            {
                LoadCheatCodes();
            }
        }

        internal void OpenCheat()
        {
            if (String.IsNullOrEmpty(SelectedCheatFile))
                return;

            string path = Path.Combine(Root, Common.CheatCodes, SelectedCheatFile);
            if (File.Exists(path))
            {
                Process.Start(path);
            }
        }


        internal void RemoveCheatF()
        {
            if (!string.IsNullOrEmpty(SelectedCheatFile))
            {
                OpDFiles.Trash(Path.Combine(Root, Common.CheatCodes, SelectedCheatFile));
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
            FilesFunc.Copy(fileSrc, destFile);
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
