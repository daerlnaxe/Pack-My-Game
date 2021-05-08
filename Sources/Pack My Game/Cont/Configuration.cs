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
    internal class Configuration
    {
        public string Language { get; set; }


        #region Paths

        /// <summary>
        /// Path for LaunchBox
        /// </summary>
        public string LaunchBoxPath { get; set; }

        /// <summary>
        /// Dossier temporaire de travail
        /// </summary>
        public string WorkingFolder { get; set; }

        /// <summary>
        /// Dossier des cheats codes
        /// </summary>
        public string CCodesPath { get; set; }

        /// <summary>
        /// Dernier dossier utilisé
        /// </summary>
        public string LastPath { get; set; }

        /// <summary>
        /// Tail to find "platforms.xml"
        /// </summary>
        public string PlatformsFile { get; set; }

        /// <summary>
        /// Tail to find 'Platforms' folder
        /// </summary>
        public string PlatformsFolder { get; set; }
        #endregion

        // ---

        #region Compression

        /// <summary>
        /// Activation de la compression zip
        /// </summary>
        public bool ZipCompression { get; set; }

        /// <summary>
        /// Niveau de compression zip (max 10)
        /// </summary>
        [Range(1, 10, ErrorMessage = "The number must be between 0 and 11")]
        public int ZipLvlCompression { get; set; }

        /// <summary>
        /// Niveau de compression maximum pour zip
        /// </summary>
        [JsonIgnore]
        public int ZipMaxLvlCompression => 10;


        /// <summary>
        /// Activation de la compression 7Z
        /// </summary>
        public bool SevZipCompression { get; set; }

        /// <summary>
        /// Niveau de compression seven zip (max 6)
        /// </summary>
        [Range(1, 6, ErrorMessage = "The number must be between 0 and 7")]
        public int SevZipLvlCompression { get; set; }

        /// <summary>
        /// Niveau de compression maximum pour seven zip
        /// </summary>
        [JsonIgnore]
        public int SevZipMaxLvlCompression => 6;

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


        /// <summary>
        /// Version du fichier de configuration
        /// </summary>
        public string Version { get; set; }



        public Configuration()
        {

        }


        public Configuration(Configuration config)
        {
            this.Version = config.Version;
            this.Language = config.Language;
            //
            this.LaunchBoxPath = config.LaunchBoxPath;
            this.WorkingFolder = config.WorkingFolder;
            this.LastPath = config.LastPath;
            this.CCodesPath = config.CCodesPath;
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

            if (config.SevZipLvlCompression > 6 || config.SevZipLvlCompression < 1)
                throw new Exception("Problem on SevenZip level compression, value must be 0<x<7");

            if (config.ZipLvlCompression > 10 || config.ZipLvlCompression < 1)
                throw new Exception("Problem on Zip level compression, value must be 0<x<11");

            return config;
        }


        internal void Save()
        {
            WriteConfig("Config.json");
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
                ZipLvlCompression = 10,
                SevZipLvlCompression = 6,
            };
        }

    }
}
