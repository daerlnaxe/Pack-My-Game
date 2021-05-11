using Common_PMG.Container;
using DxPaths.Windows;
using DxTBoxCore.Box_MBox;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pack_My_Game.Cont
{
    [Serializable]
    internal class Configuration : IConfiguration
    {
        public string Version { get; set; }

        public string Language { get; set; }


        #region Paths
        [JsonIgnore]
        public string HLaunchBoxPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// get est utilisé par le serializer donc il renvoie un relative link
        /// set est utilisé pour stocker le chemin donc il place un hardlink
        /// </remarks>
        [JsonPropertyName("LaunchBoxPath")]
        public string RLaunchBoxPath
        {
            get => DxPath.To_RelativeOrNull(AppDomain.CurrentDomain.BaseDirectory, HLaunchBoxPath);
            set => HLaunchBoxPath = string.IsNullOrEmpty(value)? null : Path.GetFullPath(value, AppDomain.CurrentDomain.BaseDirectory);
        }

        [JsonIgnore]
        public string HWorkingFolder { get; set; }

        [JsonPropertyName("WorkingFolder")]
        public string RWorkingFolder
        {
            get => DxPath.To_RelativeOrNull(AppDomain.CurrentDomain.BaseDirectory, HWorkingFolder);
            set => HWorkingFolder = string.IsNullOrEmpty(value) ? null : Path.GetFullPath(value, AppDomain.CurrentDomain.BaseDirectory);
        }

        [JsonIgnore]
        public string HCCodesPath { get; set; }

        [JsonPropertyName("CCodesPath")]
        public string RCCodesPath
        {
            get => DxPath.To_RelativeOrNull(AppDomain.CurrentDomain.BaseDirectory, HCCodesPath);
            set => HCCodesPath = string.IsNullOrEmpty(value) ? null : Path.GetFullPath(value, AppDomain.CurrentDomain.BaseDirectory);
        }


        public string LastPath { get; set; }

        public string PlatformsFile { get; set; }

        public string PlatformsFolder { get; set; }
        #endregion

        // ---

        #region Compression

        /// <summary>
        /// Activation de la compression zip
        /// </summary>
        public bool ZipCompression { get; set; }

        /// <summary>
        /// Niveau de compression zip (max 9)
        /// </summary>
        [Range(0, 9, ErrorMessage = "The number must be between -1 and 10")]
        public int ZipLvlCompression { get; set; }

        /// <summary>
        /// Niveau de compression maximum pour zip
        /// </summary>
        [JsonIgnore]
        public int ZipMaxLvlCompression => 9;


        /// <summary>
        /// Activation de la compression 7Z
        /// </summary>
        public bool SevZipCompression { get; set; }

        /// <summary>
        /// Niveau de compression seven zip (max 6)
        /// </summary>
        [Range(0, 5, ErrorMessage = "The number must be between -1 and 6")]
        public int SevZipLvlCompression { get; set; }

        /// <summary>
        /// Niveau de compression maximum pour seven zip
        /// </summary>
        [JsonIgnore]
        public int SevZipMaxLvlCompression => 5;

        #endregion

        // ---

        #region Structures
        /// <summary>
        /// Keep structure for games
        /// </summary>
        public bool KeepGameStruct { get; set; }



        /// <summary>
        /// Keep structure for Cheats
        /// </summary>
        public bool KeepCheatStruct { get; set; }


        /// <summary>
        /// Keep structure for manuals
        /// </summary>
        public bool KeepManualStruct { get; set; }

        /// <summary>
        /// Keep structure for Music
        /// </summary>
        public bool KeepMusicStruct { get; set; }

        #endregion

        // ---

        #region Options de management
        /// <summary>
        /// Switch pour gérer les cheats codes
        /// </summary>
        public bool CopyCheats { get; set; }

        /// <summary>
        /// Switch pour gérer les clones
        /// </summary>
        public bool CopyClones { get; set; }

        /// <summary>
        /// Switch pour gérer EBGame
        /// </summary>
        public bool CreateEBGame { get; set; }

        /// <summary>
        /// Switch pour gérer TBGame
        /// </summary>
        public bool CreateTBGame { get;  set; }

        /// <summary>
        /// Switch pour gérer le fichier infos
        /// </summary>
        public bool CreateInfos { get; set; }

        /// <summary>
        /// Switch pour gérer le TreeView
        /// </summary>
        public bool CreateTreeV { get; set; }

        /// <summary>
        /// Switch pour gérer le fichier log
        /// </summary>
        public bool UseLogFile { get; set; }

        /// <summary>
        /// Switch pour gérer la fenêtre de log
        /// </summary>
        public bool UseLogWindow { get; set; }

        /// <summary>
        /// Switch pour manager le MD5
        /// </summary>
        public bool CreateMD5 { get; set; }
        #endregion





        public Configuration()
        {

        }


        public Configuration(Configuration config)
        {
            this.Version = config.Version;
            this.Language = config.Language;
            //
            this.HLaunchBoxPath = config.HLaunchBoxPath;
            this.HWorkingFolder = config.HWorkingFolder;
            this.LastPath = config.LastPath;
            this.HCCodesPath = config.HCCodesPath;
            this.PlatformsFile = config.PlatformsFile;
            this.PlatformsFolder = config.PlatformsFolder;
            //
            this.ZipCompression = config.ZipCompression;
            this.SevZipCompression = config.SevZipCompression;
            this.ZipLvlCompression = config.ZipLvlCompression;
            this.SevZipLvlCompression = config.SevZipLvlCompression;
            //
            this.KeepGameStruct = config.KeepGameStruct;
            this.KeepCheatStruct = config.KeepCheatStruct;
            this.KeepManualStruct = config.KeepManualStruct;
            this.KeepMusicStruct = config.KeepMusicStruct;
            //
            this.CopyCheats = config.CopyCheats;
            this.CopyClones = config.CopyClones;
            this.CreateEBGame = config.CreateEBGame;
            this.CreateInfos = config.CreateInfos;
            this.UseLogFile = config.UseLogFile;
            this.UseLogWindow = config.UseLogWindow;
            this.CreateMD5 = config.CreateMD5;
            this.CreateTBGame = config.CreateTBGame;
            this.CreateTreeV = config.CreateTreeV;

        }


        internal static Configuration ReadConfig()
        {
            Configuration config = null;
            try
            {
                string jsonString = File.ReadAllText("Config.json");
                config = JsonSerializer.Deserialize<Configuration>(jsonString);
            }
            catch (IOException ioExc)
            {
                DxMBox.ShowDial(ioExc.Message, "Error on reading config file");
                System.Windows.Application.Current.Shutdown();
            }

            if (config.SevZipLvlCompression > 5 || config.SevZipLvlCompression < 0)
                throw new Exception("Problem on SevenZip level compression, value must be -1<x<6");

            if (config.ZipLvlCompression > 9 || config.ZipLvlCompression < 0)
                throw new Exception("Problem on Zip level compression, value must be -1<x<10");

            if (!Directory.Exists(config.LastPath))
                config.LastPath = null;


            return config;
        }


        internal void Save()
        {
            string destConfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.json");
            WriteConfig(destConfig);
        }

        internal void WriteConfig(string outputFile)
        {
            try
            {
                byte[] jsonUtf8Bytes;
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(this, options);

                using (var sw = File.Create(outputFile))
                {
                    sw.Write(jsonUtf8Bytes);
                }
            }
            catch (IOException ioExc)
            {
                DxMBox.ShowDial(ioExc.Message, "Error on writing config file");
            }
        }

        public static Configuration MakeDefault()
        {
            return new Configuration
            {
                Version = Common.ConfigVersion,
                Language = "en-EN",

                PlatformsFile = @"Data\Platforms.xml",
                PlatformsFolder = @"Data\Platforms\",
                //CCodesPath
                //LastPath
                CopyCheats = false,
                CopyClones = true,
                CreateEBGame = true,
                CreateTBGame = true,
                CreateInfos = true,
                
                UseLogWindow = true,
                CreateMD5 = true,

                CreateTreeV = true,
                UseLogFile = true,

                KeepGameStruct = false,
                KeepCheatStruct = true,
                KeepManualStruct = true,
                KeepMusicStruct = true,

                ZipCompression = true,
                ZipLvlCompression = 9,
                SevZipLvlCompression = 5,
            };
        }

        /// <summary>
        /// Met à jour le fichier de configuration
        /// </summary>
        internal void Update()
        {
            Version = Common.ConfigVersion;
            // Language
            if (string.IsNullOrEmpty(Language))
                Language = "en-EN";
        }


    }
}
