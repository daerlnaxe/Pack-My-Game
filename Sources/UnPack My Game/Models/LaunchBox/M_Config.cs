using Common_PMG.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnPack_My_Game.Models;
using UnPack_My_Game.Resources;
using static UnPack_My_Game.Common;
using UnPack_My_Game.Cont;
using DxTBoxCore.BoxChoose;
using System.Globalization;
using System.ComponentModel;
using System.Windows.Data;
using UnPack_My_Game.Language;

namespace UnPack_My_Game.Models.LaunchBox
{
    public class M_Config : A_Err
    {
       // public LangProvider Language => LanguageManager.Lang;

        public ICollectionView Languages { get; set; }

       // private CultureInfo _Langue;
      /*  public CultureInfo ChosenLanguage
        {
            get => LanguageManager.CurrentLanguage;
            set
            {
                if (value != LanguageManager.CurrentLanguage)
                {
         //           _Langue = value;
                    LanguageManager.CurrentLanguage = value;
                }
            }
        }*/

        Configuration Config
        {
            get ;
            set;
        }


        //private string _LaunchBoxPath;
        public string LaunchBoxPath
        {
            get => Config.HLaunchBoxPath; //_LaunchBoxPath;
            set
            {
                Config.HLaunchBoxPath = value;
                OnPropertyChanged();
                Remove_Errors();

                Test_NullValue(value);
            }
        }

        public string WorkingPath
        {
            get => Config.HWorkingFolder;
            set
            {
                Config.HWorkingFolder = value;
                OnPropertyChanged();
                Remove_Errors();

                Test_NullValue(value);
            }
        }
        public string CheatCodesPath
        {
            get => Config.HCCodesPath;
            set
            {
                Config.HCCodesPath = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }
        // ---

        #region Folders
        //private string _Games;
        public string Games
        {
            get => Config.Games;
            set
            {
                Config.Games = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }

        //private string _CheatCodes;
        public string CheatCodes
        {
            get => Config.CheatCodes;
            set
            {
                Config.CheatCodes = value;
                OnPropertyChanged();

                Test_NullValue(value);
            }
        }

        //public string _Images;
        public string Images
        {
            get => Config.Images;
            set
            {
                Config.Images = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }



        //private string _Manuals;
        public string Manuals
        {
            get => Config.Manuals;
            set
            {
                Config.Manuals = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }

        //private string _Musics;
        public string Musics
        {
            get => Config.Musics;
            set
            {
                Config.Musics = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }

        //private string _Videos;
        public string Videos
        {
            get => Config.Videos;
            set
            {
                Config.Videos = value;
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

        //private bool _WithCustomFields;
        public bool WithCustomFields
        {
            get => Config.UseCustomFields;
            set
            {
                Config.UseCustomFields = value;
                OnPropertyChanged();
            }
        }

        //private bool _WithFolderGameName;
        public bool WithFolderGameName
        {
            get => Config.UseGameNameFolder;
            set
            {
                Config.UseGameNameFolder = value;
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
            // Copy sinon provoque une erreur
            Languages = CollectionViewSource.GetDefaultView(LanguageManager.Langues);
            Languages.CurrentChanged += (s,e) => LanguageManager.CurrentManager.CurrentLanguage = (CultureInfo)Languages.CurrentItem;

            Config = new Configuration(Common.Config);
        }

        public void InitFolders()
        {
            CheatCodes = Common.Config.CheatCodes;
            Games = Common.Config.Games;
            Images = Common.Config.Images;
            Manuals = Common.Config.Manuals;
            Musics = Common.Config.Musics;
            Videos = Common.Config.Videos;

        }


        internal void Find_LaunchBox()
        {
            TreeChoose tc = new TreeChoose()
            {
                Model = new M_ChooseRaw()
                {
                    Info = Lang.ChooseLBf,
                    StartingFolder = Common.Config.HLaunchBoxPath,
                    Mode = ChooseMode.Folder,
                    ShowFiles = false,

                }
            };
            if (tc.ShowDialog() == true)
            {
                LaunchBoxPath = tc.LinkResult;
                Common.Config.LastPath = Config.LastPath = tc.LinkResult;
                Common.Config.Save();
                //_Model.Set_LaunchBoxPath();
            }
        }

        internal void ChooseWorkingPath()
        {
            TreeChoose tc = new TreeChoose()
            {
                Model = new M_ChooseFolder()
                {
                    HideWindowsFolder = true,
                    Info = LanguageManager.Lang.Folder_Working,
                    ShowFiles = true,
                    StartingFolder = Path.GetTempPath(),
                },
            };

            if (tc.ShowDialog() == true)
            {
                Common.Config.LastPath = tc.LinkResult;
                Common.Config.Save();

                Config.LastPath = WorkingPath = tc.LinkResult;
            }
        }
        internal void ChooseCheatsPath()
        {
            TreeChoose tc = new TreeChoose()
            {
                Model = new M_ChooseFolder()
                {
                    HideWindowsFolder = true,
                    Info = Lang.CheatCodes,
                    ShowFiles = true,
                    StartingFolder = Config.LastPath,

                },

            };

            if (tc.ShowDialog() == true)
            {
                Common.Config.LastPath = tc.LinkResult;
                Common.Config.Save();

                Config.LastPath = CheatCodesPath = tc.LinkResult;
            }
        }
        internal bool Save()
        {
            if (string.IsNullOrEmpty(LaunchBoxPath))
                Add_Error("Path is null", nameof(LaunchBoxPath));
            else if (!File.Exists(Path.Combine(LaunchBoxPath, Config.PlatformsFile)))
                Add_Error("Bad Path", nameof(LaunchBoxPath));

            if (!Directory.Exists(WorkingPath))
                Add_Error("Wrong working path", nameof(WorkingPath));

            Test_NullValue(LaunchBoxPath, nameof(LaunchBoxPath));
            Test_NullValue(Games, nameof(Games));
            Test_NullValue(CheatCodes, nameof(CheatCodes));
            Test_NullValue(Images, nameof(Images));
            Test_NullValue(Manuals, nameof(Manuals));
            Test_NullValue(Musics, nameof(Musics));
            Test_NullValue(Videos, nameof(Videos));

            if (HasErrors)
                return false;

            /* Config.LaunchBoxPath = LaunchBoxPath;
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
             Default.Save();*/


            Common.Config = Config;
            Common.Config.Save();

            return true;
        }
    }
}
