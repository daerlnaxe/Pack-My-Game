using Common_PMG.BackupLB;
using Common_PMG.Container.AAPP;
using Common_PMG.Container.Game;
using Common_PMG.Container.Game.LaunchBox;
using Common_PMG.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UnPack_My_Game.Models
{
    public class M_ModTargetPaths : A_Err
    {
        public LBGame Game { get; internal set; }

        #region Datas from platform
        public string TGamePath { get; internal set; }
        public string TImagesPath { get; internal set; }
        public string TManualPath { get; internal set; }
        public string TMusicsPath { get; internal set; }
        public string TVideosPath { get; internal set; }
        #endregion

        private string _Pivot;
        public string Pivot
        {
            get => _Pivot;
            set
            {

                /*if (Pivot && !Pivot.Contains(@"/") && !Pivot.Contains(@"\"))
                {
                }*/

                _Pivot = value;
                OnPropertyChanged();
                Test_NullValue(value);

                if (string.IsNullOrEmpty(value))
                    return;


                StringsToReplace();
            }
        }


        #region Game
        private string _ToReplaceGame;
        public string ToReplaceGame
        {
            get => _ToReplaceGame;
            set
            {
                _ToReplaceGame = value;
                OnPropertyChanged();
                OGameP = Game.ApplicationPath.Replace(value, TGamePath);

            }
        }


        private string _OGameP;
        public string OGameP
        {
            get => _OGameP;
            set
            {
                _OGameP = value;
                OnPropertyChanged();
            }
        }

        private bool _OptNameChecked;
        public bool OptNameChecked
        {
            get => _OptNameChecked;
            set
            {
                _OptNameChecked = value;
                if (_OptNameChecked)
                    TGamePath = Path.Combine(TGamePath, Game.Title);
                else
                    TGamePath = Path.GetDirectoryName(TGamePath);

                ToReplaceGame = FindMee(Game.ApplicationPath);

            }
        }
        #endregion

        /*
        private string _ToReplaceImages;
        public string ToReplaceImages
        {
            get => _ToReplaceImages;
            set
            {
                _ToReplaceImages = value;
                OnPropertyChanged();

            }
        }*/

        #region Manual
        private string _ToReplaceManual;
        public string ToReplaceManual
        {
            get => _ToReplaceManual;
            set
            {
                _ToReplaceManual = value;
                OnPropertyChanged();
                OManualP = Game.ManualPath.Replace(value, TManualPath);

            }
        }

        private string _OManualP;
        public string OManualP
        {
            get => _OManualP;
            set
            {
                _OManualP = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region

        private string _ToReplaceMusics;
        public string ToReplaceMusics
        {
            get => _ToReplaceMusics;
            set
            {
                _ToReplaceMusics = value;
                OnPropertyChanged();
                OMusicsP = Game.MusicPath.Replace(value, TMusicsPath);

            }
        }

        private string _OMusicsP;
        public string OMusicsP
        {
            get => _OMusicsP;
            set
            {
                _OMusicsP = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Videos
        private string _ToReplaceVideos;
        public string ToReplaceVideos
        {
            get => _ToReplaceVideos;
            set
            {
                _ToReplaceVideos = value;
                OnPropertyChanged();
                OVideosP = Game.VideoPath.Replace(value, TVideosPath);

            }

        }
        private string _OVideosP;
        public string OVideosP
        {
            get => _OVideosP;
            set
            {
                _OVideosP = value;
                OnPropertyChanged();
            }
        }

        public List<AdditionalApplication> Clones { get; internal set; }
        #endregion

        // --- 

        public M_ModTargetPaths()
        {
            OptNameChecked = Common.Config.UseGameNameFolder;
        }

        internal void InitializeDatas()
        {
            //Pivot = Path.Combine(Game.ApplicationPath.Split(Game.Platform)[0], Game.Platform);
            Pivot = Game.Platform;

            StringsToReplace();
            // ChangePaths();

            //ToReplaceImages = Game.im
        }

        private void StringsToReplace()
        {
            if (string.IsNullOrEmpty(Pivot))
                return;
            ToReplaceGame = FindMee(Game.ApplicationPath);
            ToReplaceManual = FindMee(Game.ManualPath);
            ToReplaceMusics = FindMee(Game.MusicPath);
            ToReplaceVideos = FindMee(Game.VideoPath);
        }


        [Obsolete]
        private void ChangePaths()
        {
            if (string.IsNullOrEmpty(Pivot))
                return;

            // Travail sur les chaines de remplacement


            //string toReplace = FindStringToReplace();

            //ToReplaceGame = Game.ApplicationPath;
            //OGameP = FindStringToReplace(Game.ApplicationPath, TGamePath);

            //OManualP = FindStringToReplace(Game.ManualPath, TManualPath);

            //OMusicsP = FindStringToReplace(Game.MusicPath, TMusicsPath);

            //OVideosP = FindStringToReplace(Game.VideoPath, TVideosPath);
        }

        private string FindMee(string archivePath)
        {
            var test = archivePath.Split($@"{Pivot}");
            return Path.Combine(test[0], $@"{Pivot}");
            //return archivePath.Replace(Path.Combine(test[0], $@"{Pivot}", "");
        }

        private string FindStringToReplace(string archivePath, string targetPath)
        {
            var test = archivePath.Split(@$"{Pivot}");

            return archivePath.Replace(Path.Combine(test[0], @$"{Pivot}"), targetPath);
        }

        internal void AssignPaths()
        {
            Game.ApplicationPath = OGameP;
            foreach (var AApp in Clones)
                AApp.ApplicationPath = FindStringToReplace(AApp.ApplicationPath, TGamePath);

            Game.ManualPath = OManualP;
            Game.MusicPath = OMusicsP;
            Game.VideoPath = OVideosP;
        }

    }
}
