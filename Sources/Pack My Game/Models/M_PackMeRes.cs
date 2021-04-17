using DxLocalTransf;
using DxTBoxCore.Box_Progress;
using DxTBoxCore.BoxChoose;
using HashCalc;
using Common_PMG.Container;
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
using Common_PMG.Container.Game;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PS = Pack_My_Game.Properties.Settings;
using AsyncProgress.Tools;
using DxLocalTransf.Copy;

namespace Pack_My_Game.Models
{

    public class M_PackMeRes : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /*public static event DoubleHandler UpdateProgress;
        public static event MessageHandler UpdateStatus;
        public static event DoubleHandler MaximumProgress;*/

        public string Root { get; private set; }

        public Language Lang => Common.ObjectLang;

        #region Chosen
        DataRep _ChosenGame;
        public DataRep ChosenGame
        {
            get => _ChosenGame;
            set
            {
                _ChosenGame = value;
                OnPropertyChanged();
            }
        }

        //public DataRep ChosenCheatF { get; set; }


        DataRep _ChosenManual;
        public DataRep ChosenManual
        {
            get => _ChosenManual;
            set
            {
                _ChosenManual = value;
                OnPropertyChanged();
            }
        }


        DataRep _ChosenMusic;
        public DataRep ChosenMusic
        {
            get => _ChosenManual;
            set
            {
                _ChosenMusic = value;
                OnPropertyChanged();
            }
        }



        DataRep _ChosenVideo;
        public DataRep ChosenVideo
        {
            get => _ChosenVideo;
            set
            {
                _ChosenVideo = value;
                OnPropertyChanged();
            }
        }

        DataRep _ChosenThemeVideo;
        public DataRep ChosenThemeVideo
        {
            get => _ChosenThemeVideo;
            set
            {
                _ChosenThemeVideo = value;
                OnPropertyChanged();
            }
        }
        #endregion



        //public DataRep SelectedGame { get; set; }
        public ObservableCollection<DataRep> GamesCollection { get; set; } = new ObservableCollection<DataRep>();

        public DataRep SelectedManual { get; set; }
        public ObservableCollection<DataRep> ManualsCollection { get; set; } = new ObservableCollection<DataRep>();

        public DataRep SelectedMusic { get; set; }
        public ObservableCollection<DataRep> MusicsCollection { get; set; } = new ObservableCollection<DataRep>();


        public DataRep SelectedVideo { get; set; }
        public ObservableCollection<DataRep> VideosCollection { get; set; } = new ObservableCollection<DataRep>();

        public string SelectedCheatFile { get; set; }
        public ObservableCollection<DataRep> CheatsCollection { get; set; } = new ObservableCollection<DataRep>();

        /// <summary>
        /// Contient les chemins de la plateforme
        /// </summary>
        public Platform Platform { get; }

        public string GameName { get; }

        public GameDataCont GameDataC { get; }

        public M_PackMeRes(string root, Platform platform, GameDataCont gdC)
        {
            Root = root;
            Platform = platform;
            GameName = gdC.Title;
            GameDataC = gdC;

            // Création des collections (par rapport au changement de nom
            MakeCollection(gdC.Apps, GamesCollection, Common.Games);
            MakeCollection(gdC.CheatCodes, CheatsCollection, Common.CheatCodes);
            MakeCollection(gdC.Manuals, ManualsCollection, Common.Manuals);
            MakeCollection(gdC.Musics, MusicsCollection, Common.Musics);
            MakeCollection(gdC.Videos, VideosCollection, Common.Videos);

            // Initialisation des fichiers par défaut.
            ChosenGame = GamesCollection.FirstOrDefault(x => x.DestPath.Equals(gdC.DefaultApp?.DestPath));
            ChosenManual = ManualsCollection.FirstOrDefault(x => x.DestPath.Equals(gdC?.DefaultManual.DestPath));
            ChosenMusic = MusicsCollection.FirstOrDefault(x => x.DestPath.Equals(gdC.DefaultMusic?.DestPath));
            ChosenVideo = VideosCollection.FirstOrDefault(x => x.DestPath.Equals(gdC.DefaultVideo?.DestPath));
            ChosenThemeVideo = VideosCollection.FirstOrDefault(x => x.DestPath.Equals(gdC.DefaultThemeVideo?.DestPath));

            LoadFiles();
        }



        #region initialisation
        private void MakeCollection(List<DataRep> srcCollected, ObservableCollection<DataRep> targetedCollec, string mediatype)
        {
            string pRoot = Path.Combine(Root, mediatype);
            targetedCollec.Clear();
            foreach (DataRep elem in srcCollected)
            {
                DataRep dr = DataRep.DataRepFactory(elem);
                dr.Name = dr.DestPath.Replace(pRoot, ".");
                targetedCollec.Add(dr);
            }
        }


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
            string gamesPath = Path.Combine(Root, Common.Games);
            LoadFiles2(gamesPath, GamesCollection);
            TestFiles(GamesCollection, ChosenGame);
        }



        private void LoadManuals()
        {
            string manualsPath = Path.Combine(Root, Common.Manuals);
            LoadFiles2(manualsPath, ManualsCollection);
            TestFiles(ManualsCollection, ChosenManual);
            #region
            /*foreach (string f in Directory.EnumerateFiles(manualsPath, "*.*", SearchOption.AllDirectories))
            {
                string tmp = f.Replace(manualsPath, ".");
                if (tmp.Equals(GameDataC.DefaultManual))
                {
                    ChosenManual = new DataRep(tmp, f);
                    continue;
                }

                ManualsCollection.Add(new DataRep(tmp, f));
            }*/
            #endregion
        }

        private void LoadMusics()
        {
            string musicsPath = Path.Combine(Root, Common.Musics);
            LoadFiles2(musicsPath, MusicsCollection);
            TestFiles(MusicsCollection, ChosenMusic);
            #region
            /*
            MusicsCollection.Clear();
            foreach (string f in Directory.EnumerateFiles(musicsPath, "*.*", SearchOption.AllDirectories))
            {
                string tmp = f.Replace(musicsPath, ".");
                if (tmp.Equals(GameDataC.DefaultMusic))
                {
                    ChosenMusic = new DataRep(tmp, f);
                    continue;
                }

                MusicsCollection.Add(new DataRep(tmp, f));
            }*/
            #endregion
        }



        private void LoadVideos()
        {
            string videosPath = Path.Combine(Root, Common.Videos);
            LoadFiles2(videosPath, VideosCollection);
            TestFiles(VideosCollection, ChosenVideo, ChosenThemeVideo);

            #region
            /*VideosCollection.Clear();
            foreach (string f in Directory.EnumerateFiles(videosPath, "*.*", SearchOption.AllDirectories))
            {
                string tmp = f.Replace(videosPath, ".");
                if (tmp.Equals(GameDataC.DefaultVideo))
                {
                    ChosenVideo = new DataRep(tmp, f);
                    continue;
                }

                VideosCollection.Add(new DataRep(tmp, f));
            }*/
            #endregion
        }


        internal void LoadCheatCodes()
        {
            string cheatsPath = Path.Combine(Root, Common.CheatCodes);
            CheatsCollection.Clear();
            foreach (string f in Directory.EnumerateFiles(Path.Combine(Root, Common.CheatCodes), "*.*", SearchOption.TopDirectoryOnly))
            {
                string tmp = f.Replace(cheatsPath, ".");
                CheatsCollection.Add(DataRep.MakeNameNPath(tmp, f));
            }
        }

        private void LoadFiles2(string path, ObservableCollection<DataRep> collection)
        {
            foreach (string f in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories))
            {
                string tmp = f.Replace(path, ".");

                // on ajoute que si non présent
                var test = collection.FirstOrDefault((x) => x.DestPath.Equals(f));
                if (test == null)
                {
                    DataRep dr = new DataRep(f);
                    dr.DestPath = f;
                    collection.Add(dr);
                }

            }
        }

        private void TestFiles(ObservableCollection<DataRep> collection, params DataRep[] chosens)
        {
            List<DataRep> tmp = new List<DataRep>(collection);
            foreach (var o in tmp)
                if (!File.Exists(o.DestPath))
                {
                    collection.Remove(o);

                    for (int i = 0; i < chosens.Count(); i++)
                        if (chosens[i] == o)
                            chosens[i] = null;
                }

        }
        #endregion

        #region Check / Uncheck

        internal void Game_Handler(object tag, bool? isChecked)
        {
            if (isChecked == true)
                SetDefault(tag, GamesCollection, (x) => ChosenGame = x);
            /* else
                 UnsetDefault(tag, (x) => ChosenGame = x);*/
        }

        internal void Manual_Handler(object tag, bool? isChecked)
        {
            if (isChecked == true)
                SetDefault(tag, ManualsCollection, (x) => ChosenManual = x);
            /* else
                 UnsetDefault(tag, (x) => ChosenManual = x);*/
        }

        internal void Music_Handler(object tag, bool? isChecked)
        {
            if (isChecked == true)
                SetDefault(tag, MusicsCollection, (x) => ChosenMusic = x);
            /*   else
                   UnsetDefault(tag, (x) => ChosenMusic = x);*/

        }

        internal void Video_Handler(DataRep selected, bool isChecked)
        {
            if (isChecked)
                SetDefault(selected, VideosCollection, ChosenThemeVideo, (x) => ChosenVideo = x);
            else
                UnsetDefault(selected, (x) => ChosenVideo = x);
        }

        internal void ThemeVideo_Handler(DataRep selected, bool isChecked)
        {
            if (isChecked)
                SetDefault(selected, VideosCollection, ChosenVideo, (x) => ChosenThemeVideo = x);
            else
                UnsetDefault(selected, (x) => ChosenThemeVideo = x);
        }


        private void SetDefault(object selected, ObservableCollection<DataRep> collection, Action<DataRep> SetChosen)
        {
            DataRep dr = (DataRep)selected;

            foreach (var elem in collection)
                if (dr != elem)
                    elem.IsSelected = false;

            SetChosen(dr);
        }

        internal void SetDefault(DataRep dr, ObservableCollection<DataRep> collection, DataRep other, Action<DataRep> SetChosen)
        {
            foreach (var elem in collection)
                if (elem != other && elem != dr)
                    elem.IsSelected = false;

            dr.IsSelected = true;
            SetChosen(dr);
        }

        private void UnsetDefault(object tag, Func<DataRep, object> SetChosen)
        {
            DataRep dr = (DataRep)tag;
            dr.IsSelected = false;
            SetChosen(null);
        }

        #endregion check/uncheck


        /// <summary>
        /// Fonction de copie des fichiers
        /// </summary>
        /// <param name="sourceDirectory">Répertoire source</param>
        /// <param name="targetDirectory">Répertoire cible</param>
        public void CopyGameF()
        {
            if (Copy2(Platform.FolderPath, Common.Games, "Select a game file"))
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
            /*  if (SelectedGame != null)
              {
                  OpDFiles.Trash(SelectedGame.ALinkToThePath);
                  LoadGames();
              }*/
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
            p.StartInfo.FileName = SelectedManual.ALinkToThePath;
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
                OpDFiles.Trash(SelectedManual.ALinkToThePath);
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
            p.StartInfo.FileName = SelectedMusic.ALinkToThePath;
            p.Start();

        }

        internal void RemoveMusicF()
        {
            if (SelectedMusic != null)
            {
                OpDFiles.Trash(SelectedMusic.ALinkToThePath);
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
            p.StartInfo.FileName = SelectedVideo.ALinkToThePath;
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
        }

        internal void RemoveVideoF()
        {
            if (SelectedVideo != null)
            {
                OpDFiles.Trash(SelectedVideo.ALinkToThePath);
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
                EphemProgress ephem = new EphemProgress();
                CopyNVerif copyZat = new CopyNVerif();

                TaskLauncher launcher = new TaskLauncher()
                {
                    AutoCloseWindow = true,
                    ProgressIHM = new DxProgressB1(ephem),
                    MethodToRun = () => copyZat.CopyANVerif(DataTrans.MakeSrcnDest(tc.LinkResult, Path.Combine(Root, subFolder))),
                };
                launcher.Launch(copyZat);
                return true;
                //return Copy(tc.LinkResult, Path.Combine(Root, subFolder));
            }
            return false;
        }

    }
}
