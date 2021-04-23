using Common_PMG.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnPack_My_Game.Models;
using UnPack_My_Game.Resources;
using static UnPack_My_Game.Properties.Settings;

namespace UnPack_My_Game.Graph.LaunchBox
{
    public class M_Config : A_Err
    {
        private string _LaunchBoxPath;
        public string LaunchBoxPath
        {
            get => _LaunchBoxPath;
            set
            {
                _LaunchBoxPath = value;
                OnPropertyChanged();
                Remove_Error("Bad Path");

                Test_NullValue(value);
            }
        }

        // ---

        #region Folders
        private string _Games;
        public string Games
        {
            get => _Games;
            set
            {
                _Games = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }

        private string _CheatCodes;
        public string CheatCodes
        {
            get => _CheatCodes;
            set
            {
                _CheatCodes = value;
                OnPropertyChanged();

                Test_NullValue(value);
            }
        }

        public string _Images;
        public string Images
        {
            get => _Images;
            set
            {
                _Images = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }

        private string _Manuals;
        public string Manuals
        {
            get => _Manuals;
            set
            {
                _Manuals = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }

        private string _Musics;
        public string Musics
        {
            get => _Musics;
            set
            {
                _Musics = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }

        private string _Videos;
        public string Videos
        {
            get => _Videos;
            set
            {
                _Videos = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }
        #endregion

        // ---

        /*
        public bool ChangePlatform
        {
            get => Default.ChangePlatform;
            set
            {
                Default.ChangePlatform = value;
                OnPropertyChanged();

            }
        }
        */
        // ---

        // ---

        private bool _WithCustomFields;
        public bool WithCustomFields
        {
            get => _WithCustomFields;
            set
            {
                _WithCustomFields = value;
                OnPropertyChanged();
            }
        }
        private bool _WithFolderGameName;
        public bool WithFolderGameName
        {
            get => _WithFolderGameName;
            set
            {
                _WithFolderGameName = value;
                OnPropertyChanged();
            }
        }

        // ---

        /// <summary>
        /// Set LaunchBox Path
        /// </summary>
        internal void Set_LaunchBoxPath()
        {
            Remove_Error(nameof(LaunchBoxPath));

            if (!Directory.Exists(this.LaunchBoxPath))
            {
                Add_Error(Lang.Err_LaunchBoxF, nameof(LaunchBoxPath));
                return;
            }

            // Sauvegarde du chemin
            //Properties.Settings.Default.LastLBpath = LaunchBoxPath;


            //Load_Platforms();
        }

        public M_Config()
        {
            Init();
        }

        public void Init()
        {
            LaunchBoxPath = Default.LaunchBoxPath;
            //
            Games = Default.Games;
            CheatCodes = Default.CheatCodes;
            Images = Default.Images;
            Manuals = Default.Manuals;
            Musics = Default.Musics;
            Videos = Default.Videos;
            //
            WithCustomFields = Default.wCustomFields;
            WithFolderGameName = Default.wGameNameFolder;




        }


        internal bool Save()
        {
            if (!File.Exists(Path.Combine(LaunchBoxPath, Default.fPlatforms)))
                Add_Error("Bad Path", nameof(LaunchBoxPath));

            Test_NullValue(LaunchBoxPath, nameof(LaunchBoxPath));
            Test_NullValue(Games, nameof(Games));
            Test_NullValue(CheatCodes, nameof(CheatCodes));
            Test_NullValue(Images, nameof(Images));
            Test_NullValue(Manuals, nameof(Manuals));
            Test_NullValue(Musics, nameof(Musics));
            Test_NullValue(Videos, nameof(Videos));

            if (HasErrors)
                return false;

            Default.LaunchBoxPath = LaunchBoxPath;
            //
            Default.Games = Games;
            Default.CheatCodes = CheatCodes;
            Default.Images = Images;
            Default.Manuals = Manuals;
            Default.Musics = Musics;
            Default.Videos = Videos;
            //
            Default.wCustomFields = WithCustomFields;
            Default.wGameNameFolder = this.WithFolderGameName;
            //
            Default.Save();

            return true;

        }


    }
}
