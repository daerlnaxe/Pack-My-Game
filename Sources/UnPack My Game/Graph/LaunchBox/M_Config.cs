using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnPack_My_Game.Models;
using UnPack_My_Game.Resources;
using PS = UnPack_My_Game.Properties.Settings;

namespace UnPack_My_Game.Graph.LaunchBox
{
    public class M_Config: A_Err
    {
        //private string _LaunchBoxPath;
        public string LaunchBoxPath
        {
            get => PS.Default.LastLBpath;
            set
            {
                PS.Default.LastLBpath = value;
                OnPropertyChanged();

                Test_NullValue(value);
            }
        }

        // ---

        #region Folders
        //private string _Games;
        public string Games
        {
            get => PS.Default.Games;
            set
            {
                PS.Default.Games = value;
                OnPropertyChanged();

                Test_NullValue(value);

            }
        }



        //private string _CheatCodes;
        public string CheatCodes
        {
            get => PS.Default.CheatCodes;
            set
            {
                PS.Default.CheatCodes = value;
                OnPropertyChanged();

                Test_NullValue(value);
            }
        }

        //public string _Images;
        public string Images
        {
            get => PS.Default.Images;
            set
            {
                PS.Default.Images = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }

        //private string _Manuals;
        public string Manuals
        {
            get => PS.Default.Manuals;
            set
            {
                PS.Default.Manuals = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }

        //private string _Musics;
        public string Musics
        {
            get => PS.Default.Musics;
            set
            {
                PS.Default.Musics = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }

        //private string _Videos;
        public string Videos
        {
            get => PS.Default.Videos;
            set
            {
                PS.Default.Videos = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }
        #endregion

        // ---

        public bool ChangePlatform
        {
            get => PS.Default.ChangePlatform;
            set
            {
                PS.Default.ChangePlatform = value;
                OnPropertyChanged();

            }
        }

        // ---

        // ---

        public bool WithCustomFields
        {
            get => PS.Default.wCustomFields;
            set
            {
                PS.Default.wCustomFields = value;
                PS.Default.Save();
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

        internal bool Save()
        {
            Test_NullValue(LaunchBoxPath, nameof(LaunchBoxPath));
            Test_NullValue(Games, nameof(Games));
            Test_NullValue(CheatCodes, nameof(CheatCodes));
            Test_NullValue(Images, nameof(Images));
            Test_NullValue(Manuals, nameof(Manuals));
            Test_NullValue(Musics, nameof(Musics));
            Test_NullValue(Videos, nameof(Videos));

            if (HasErrors)
                return false;

            PS.Default.LastLBpath = LaunchBoxPath;
            PS.Default.Games = Games;
            PS.Default.CheatCodes = CheatCodes;
            PS.Default.Images = Images;
            PS.Default.Manuals = Manuals;
            PS.Default.Musics = Musics;
            PS.Default.Videos = Videos;
            PS.Default.wCustomFields = WithCustomFields;
            PS.Default.Save();

            return true;

        }
    }
}
