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
using AsyncProgress.Tools;
using Common_PMG.Models;

namespace UnPack_My_Game.Graph
{

    public class M_DPGMaker : A_Err
    {
        public bool Advanced => false;

        public string Title { get; set; }
        public string Platform { get; set; }


        #region Chosen
        DataPlus _ChosenGame;
        public DataPlus ChosenGame
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
            get => _ChosenMusic;
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





        public string Root { get; private set; }

        //  public Language Lang => Common.ObjectLang;

        public DataPlus SelectedGame { get; set; }
        public ObservableCollection<DataPlus> GamesCollection { get; set; } = new ObservableCollection<DataPlus>();

        public DataRep SelectedManual { get; set; }
        public ObservableCollection<DataRep> ManualsCollection { get; set; } = new ObservableCollection<DataRep>();

        public DataRep SelectedMusic { get; set; }
        public ObservableCollection<DataRep> MusicsCollection { get; set; } = new ObservableCollection<DataRep>();

        public DataRep SelectedVideo { get; set; }
        public ObservableCollection<DataRep> VideosCollection { get; set; } = new ObservableCollection<DataRep>();

        public string SelectedCheatFile { get; set; }
        public ObservableCollection<DataRep> CheatsCollection { get; set; } = new ObservableCollection<DataRep>();


        public GameDataCont GameDataC { get; }
        public GamePaths GamePaths { get; }


        public M_DPGMaker(GameDataCont gpC, GamePaths gpx, string root)
        {
            Root = root;

            GameDataC = gpC;
            GamePaths = gpx;

            Init();
        }

        /*
        private void LoadGames(GamePaths gpX)
        {
            foreach (var game in gpX.CompApps)
            {
                DataRep d = new DataRep(game);
                if (d.ALinkToThePast.Equals(gpX.ApplicationPath))
                    d.IsSelected = true;

                GamesCollection.Add(new DataRep(game));
            }
        }*/

        internal void Init()
        {
            Title = GamePaths.Title;
            Platform = GamePaths.Platform;

            // Création des collections (par rapport au changement de nom
            MakeCollection(GameDataC.Applications, GamesCollection, PS.Default.Games);
            MakeCollection(GameDataC.CheatCodes, CheatsCollection, PS.Default.CheatCodes);
            MakeCollection(GameDataC.Manuals, ManualsCollection, PS.Default.Manuals);
            MakeCollection(GameDataC.Musics, MusicsCollection, PS.Default.Musics);
            MakeCollection(GameDataC.Videos, VideosCollection, PS.Default.Videos);

            // Initialisation des fichiers par défaut.
            ChosenGame = GamesCollection.FirstOrDefault(x => x.Name.Equals(GameDataC.DefaultApp?.CurrentPath));
            ChosenManual = ManualsCollection.FirstOrDefault(x => x.Name.Equals(GameDataC.DefaultManual?.CurrentPath));
            ChosenMusic = MusicsCollection.FirstOrDefault(x => x.Name.Equals(GameDataC.DefaultMusic?.CurrentPath));
            ChosenVideo = VideosCollection.FirstOrDefault(x => x.Name.Equals(GameDataC.DefaultVideo?.CurrentPath));
            ChosenThemeVideo = VideosCollection.FirstOrDefault(x => x.Name.Equals(GameDataC.DefaultThemeVideo?.CurrentPath));
        }

        private void MakeCollection<T>(IEnumerable<T> srcCollected, ObservableCollection<T> targetedCollec, string mediatype) where T: IData, new()
        {
            string pRoot = Path.Combine(Root, mediatype);
            targetedCollec.Clear();
            foreach (T elem in srcCollected)
            {
                T dr = new T()
                {
                    Name = elem.CurrentPath,
                    CurrentPath = Path.GetFullPath(elem.CurrentPath, pRoot),
                    IsSelected = elem.IsSelected,
                };

                //dr.ALinkToThePath =
                targetedCollec.Add(dr);
            }
        }


        #region check/uncheck

        internal void Game_Handler(object tag)
        {
            SetDefault((DataPlus)tag, GamesCollection, (x) => ChosenGame = x);
            /* else
                 UnsetDefault(tag, (x) => ChosenGame = x);*/
        }

        internal void Manual_Handler(object tag, bool? isChecked)
        {
            if (isChecked == true)
                SetDefault((DataRep)tag, ManualsCollection, (x) => ChosenManual = x);
            /* else
                 UnsetDefault(tag, (x) => ChosenManual = x);*/
        }

        internal void Music_Handler(object tag, bool? isChecked)
        {
            if (isChecked == true)
                SetDefault((DataRep)tag, MusicsCollection, (x) => ChosenMusic = x);
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

        private void SetDefault<T>(T dr, ObservableCollection<T> collection, Action<T> SetChosen) where T: class, IData
        {
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

        // ---

        internal void GameSelect()
        {
            foreach (var game in GamesCollection)
            {
                if (SelectedGame == game)
                    continue;
                game.IsSelected = false;
            }
        }

        #region initialisation



        private void LoadManuals()
        {
            ManualsCollection.Clear();
            string manualsPath = Path.Combine(Root, PS.Default.Manuals);
            foreach (string f in Directory.EnumerateFiles(manualsPath, "*.*", SearchOption.AllDirectories))
            {
                string tmp = f.Replace(manualsPath, ".");
                /*          if (tmp.Equals(GamePaths.ManualPath))
                          {
                          //    ChosenManual = new DataRep(tmp, f);
                              continue;
                          }*/

                // ManualsCollection.Add(new DataRep(tmp, f));
            }
        }


        private void LoadMusics()
        {
            MusicsCollection.Clear();
            string musicsPath = Path.Combine(Root, PS.Default.Musics);
            foreach (string f in Directory.EnumerateFiles(musicsPath, "*.*", SearchOption.AllDirectories))
            {
                string tmp = f.Replace(musicsPath, ".");
                /*   if (tmp.Equals(GamePaths.MusicPath))
                   {
            //           ChosenMusic = new DataRep(tmp, f);
                       continue;
                   }*/

                //                MusicsCollection.Add(new DataRep(tmp, f));
            }
        }

        private void LoadVideos()
        {
            VideosCollection.Clear();
            string videosPath = Path.Combine(Root, PS.Default.Videos);
            foreach (string f in Directory.EnumerateFiles(videosPath, "*.*", SearchOption.AllDirectories))
            {
                string tmp = f.Replace(videosPath, ".");
                /*    if (tmp.Equals(GamePaths.VideoPath))
                    {
            //            ChosenVideo = new DataRep(tmp, f);
                        continue;
                    }*/

                //        VideosCollection.Add(new DataRep(tmp, f));
            }
        }

        internal void LoadCheatCodes()
        {
            string cheatsPath = Path.Combine(Root, PS.Default.CheatCodes);
            CheatsCollection.Clear();
            foreach (string f in Directory.EnumerateFiles(cheatsPath, "*.*", SearchOption.TopDirectoryOnly))
            {
                string tmp = f.Replace(cheatsPath, ".");
                //            CheatsCollection.Add(new DataRep(tmp, f));
            }
        }

        #endregion





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
            p.StartInfo.FileName = SelectedManual.CurrentPath;
            p.Start();

        }

        internal void LaunchPage(string page)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = true;
            //  p.StartInfo.FileName = @$"{page}{GameName.Replace(' ', '+')}";
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
                OpDFiles.Trash(SelectedManual.CurrentPath);
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
            p.StartInfo.FileName = SelectedMusic.CurrentPath;
            p.Start();

        }

        internal void RemoveMusicF()
        {
            if (SelectedMusic != null)
            {
                OpDFiles.Trash(SelectedMusic.CurrentPath);
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
            p.StartInfo.FileName = SelectedVideo.CurrentPath;
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
                OpDFiles.Trash(SelectedVideo.CurrentPath);
                LoadVideos();
            }
        }

        #endregion

        #region Cheats

        internal void CopyAstuceF()
        {
            if (Copy2(PS.Default.LaunchBoxPath, PS.Default.CheatCodes, "Select a cheat codes file"))
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

            OpDFilesExt copyObj = new OpDFilesExt();
            MawEvo mawEvo = new MawEvo(copyObj);
            TaskLauncher launcher = new TaskLauncher()
            {
                AutoCloseWindow = true,
                ProgressIHM = new DxDoubleProgress(mawEvo),
                // MethodToRun = ()=> copyObj.CopyANVerif(fileSrc, destFile, true),                 
            };
            launcher.Launch(copyObj);

            /*UpdateStatus(this, $"Copy {fileSrc}");
            UpdateProgress?.Invoke(this, 0);
            MaximumProgress?.Invoke(this, 1);*/
            //File.Copy(fileSrc, destFile);
            /*UpdateProgress?.Invoke(this, 1);*/

            // --- Vérification des sommes
            /*UpdateStatus(this, $"Copy verification");
            MaximumProgress?.Invoke(this, 100);
            */

            /*        DxProgressB1 progressBox = new DxProgressB1();*/

            //bool? res = _ObjectFiles.VerifByHash_Sync(fileSrc, destFile, () => MD5.Create());

            //objectFiles.SignalProgression += progressBox.CurrentProgress;

            /*objectFiles.Finished += ((x) => Application.Current.Dispatcher?.Invoke
                                                        (() => { progressBox.Close(); }));
            */
            /*
            EFileResult res = EFileResult.None;
            Task.Run(() => res = objectFiles.DeepVerif(fileSrc, destFile, () => MD5.Create()));

            progressBox.ShowDialog();

            UpdateStatus?.Invoke(this, new StateArg( $"Check verif: {res}");*/
            return true;
        }

        public bool Apply_Modifs()
        {
            this.Test_HasElement(GamesCollection, nameof(GamesCollection));
            this.Test_NullValue(ChosenGame, nameof(GamesCollection));

            if (HasErrors)
                return false;

            /*GamePaths.ApplicationPath = this.ChosenGame.Name;*/
            GamePaths.SetApplications = GamesCollection;
            GamePaths.ManualPath = this.ChosenManual == null ? null: ChosenManual.Name;
            GamePaths.MusicPath = this.ChosenMusic == null? null: ChosenMusic.Name;
            GamePaths.VideoPath = this.ChosenVideo == null ? null: ChosenVideo.Name;
            GamePaths.ThemeVideoPath = this.ChosenThemeVideo == null ? null: ChosenThemeVideo.Name;

            return true;
        }
    }
}
