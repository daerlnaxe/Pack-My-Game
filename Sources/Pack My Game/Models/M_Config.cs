using Common_PMG.Models;
using DxTBoxCore.BoxChoose;
using Pack_My_Game.Cont;
using Pack_My_Game.Language;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Pack_My_Game.Models
{
    class M_Config : A_Err
    {
        #region Languages

        //public string[] Languages { get; set; } = { "en-EN", "fr-FR" };
        /*public List<Language> Languages { get; set; } = new List<Language>();

        public Language Lang { get; set; }

        private Language _SelectedLanguage;
        public Language SelectedLanguage
        {
            get => _SelectedLanguage;
            set
            {
                if (value == null || value.Lang.Equals(_SelectedLanguage?.Lang))
                    return;

                _SelectedLanguage = value;
            }
        }*/

        /*
        internal void Relocalize()
        {
            string xmlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.LangFolder, $"Lang.{SelectedLanguage.Lang}.xml");
            Lang = XML.XMLLang.Load(xmlFile);
            OnPropertyChanged(nameof(Lang));
            OnPropertyChanged(nameof(LaunchBoxPath));
            OnPropertyChanged(nameof(CheatCodesPath));
        }*/
        #endregion

        #region Paths
        //  private string _LaunchBoxPath;
        public string LaunchBoxPath
        {
            get => Config.HLaunchBoxPath;
            set
            {
                Config.HLaunchBoxPath = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }

        //private string _WorkingPath;
        public string WorkingPath
        {
            get => Config.HWorkingFolder;
            set
            {
                Config.HWorkingFolder = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }


        //private string _CheatCodesPath;
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
        #endregion


        Configuration _Config;
        public Configuration Config
        {
            get => _Config;
            private set
            {
                _Config = value;
                OnPropertyChanged();

            }
        }



        public M_Config()
        {
            // Lang = Common.ObjectLang;

            Config = new Configuration(Common.Config);

            /*
            // Loading of all xml languages files
            foreach (var f in Directory.EnumerateFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.LangFolder), "*.xml", SearchOption.TopDirectoryOnly))
            {
                Language objLang = XML.XMLLang.ShortLoad(f);
                if (!objLang.Version.Equals(Common.LangVersion))
                    continue;

                Languages.Add(objLang);
            }

            SelectedLanguage = Languages.FirstOrDefault(x => x.Lang.Equals(Common.ObjectLang.Lang));*/
        }


        internal void ChooseLaunchBoxPath()
        {

            TreeChoose tc = new TreeChoose()
            {
                Model = new M_ChooseFolder()
                {
                    HideWindowsFolder = true,
                    Info = LanguageManager.Lang.Choose_LBPath,
                    ShowFiles = true,
                    StartingFolder = Config.LastPath,// PS.Default.LastKPath,
                },

            };

            if (tc.ShowDialog() == true)
            {
                Common.Config.LastPath = tc.LinkResult;
                Common.Config.Save();

                Config.LastPath = LaunchBoxPath = tc.LinkResult;
            }
        }


        internal void ChooseWorkingPath()
        {
            TreeChoose tc = new TreeChoose()
            {
                Model = new M_ChooseFolder()
                {
                    HideWindowsFolder = true,
                    Info = LanguageManager.Lang.Choose_Path,
                    ShowFiles = true,
                    StartingFolder = Config.LastPath,
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
                    Info = LanguageManager.Lang.Choose_Path,
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

        internal void Save()
        {
            //PS.Default.Language = SelectedLanguage;
            //Common.ObjectLang = Lang;
            //Config.Language = SelectedLanguage.Lang;

            Common.Config = Config;
            Common.Config.Save();
        }
    }
}
