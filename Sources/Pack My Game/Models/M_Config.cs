using Common_PMG.Models;
using Pack_My_Game.Cont;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using PS = Pack_My_Game.Properties.Settings;

namespace Pack_My_Game.Models
{
    class M_Config : A_Err
    {
        #region Languages

        //public string[] Languages { get; set; } = { "en-EN", "fr-FR" };
        public List<Cont.Language> Languages { get; set; } = new List<Cont.Language>();

        public Language Lang { get; set; }

        private Language _SelectedLanguage;
        public Language SelectedLanguage
        {
            get => _SelectedLanguage;
            set
            {
                if (value.Equals(_SelectedLanguage))
                    return;

                _SelectedLanguage = value;

            }
        }

        internal void Relocalize()
        {
            string xmlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.LangFolder, $"Lang.{SelectedLanguage.Lang}.xml");
            Lang = XML.XMLLang.Load(xmlFile);
            OnPropertyChanged(nameof(Lang));
            OnPropertyChanged(nameof(LaunchBoxPath));
            OnPropertyChanged(nameof(CheatCodesPath));
        }
        #endregion

        #region Paths
        private string _LaunchBoxPath;
        public string LaunchBoxPath
        {
            get => _LaunchBoxPath;
            set
            {
                _LaunchBoxPath = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }

        private string _WorkingPath;
        public string WorkingPath
        {
            get => _WorkingPath;
            set 
            {
                _WorkingPath = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }


        private string _CheatCodesPath;
        public string CheatCodesPath
        {
            get => _CheatCodesPath;
            set
            {
                _CheatCodesPath = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }
        #endregion

        #region
        public int ZipLvlCompression { get; set; }
        public int ZipMaxLvl => PS.Default.cZMaxComp;
        public int SevZipLvlCompression 
        { 
            get;
            set;
        }
        public int SevZipMaxLvl => PS.Default.c7ZMaxComp;
        #endregion

        #region
        public bool InfosGame { get; set; }
        public bool OriginalXMLBackup { get; set; }
        public bool EnhancedXMLBackup { get; set; }
        public bool TreeviewFile { get; set; }
        public bool ClonesCopy { get; set; }
        public bool CheatsCopy { get; set; }
        
        public bool MD5Calcul { get; set; }
        // --- 

        public bool ZipCompression { get; set; }
        public bool SevCompression { get; set; }

        // ---

        public bool FileLog { get; set; }
        public bool WindowLog { get; set; }

        #endregion

        public M_Config()
        {
            Lang = Common.ObjectLang;
            LaunchBoxPath = PS.Default.LBPath;
            WorkingPath = PS.Default.OutPPath;
            CheatCodesPath = PS.Default.CCodesPath;
            // ---
            ZipLvlCompression = 6;//PS.Default.cZipCompLvl;
            SevZipLvlCompression = PS.Default.c7zCompLvl;
            // ---
            InfosGame = PS.Default.opInfos;
            OriginalXMLBackup = PS.Default.opOBGame;
            EnhancedXMLBackup = PS.Default.opEBGame;
            TreeviewFile = PS.Default.opTreeV;
            ClonesCopy = PS.Default.opClones;
            CheatsCopy = PS.Default.opCheatCodes;
            MD5Calcul = PS.Default.opMd5;
            // ---
            ZipCompression = PS.Default.opZip;
            SevCompression = PS.Default.op7_Zip;
            // ---
            FileLog = PS.Default.opLogFile;
            WindowLog = PS.Default.opLogWindow;

            // Loading of all xml languages files
            foreach (var f in Directory.EnumerateFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.LangFolder), "*.xml", SearchOption.TopDirectoryOnly))
            {
                Cont.Language lang = XML.XMLLang.ShortLoad(f);
                if (!lang.Version.Equals(Common.LangVersion))
                    continue;

                Languages.Add(lang);
            }

            SelectedLanguage = Languages.FirstOrDefault(x => x.Lang.Equals(Common.ObjectLang.Lang));
        }
    }
}
