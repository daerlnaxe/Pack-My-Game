using Common_PMG.Container;
using DxTBoxCore.Box_MBox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace UnPack_My_Game.Cont
{
    public class Configuration : IConfiguration
    {
        public string Version { get; set; }

        public string Language { get; set; }

        #region Paths
        public string LaunchBoxPath { get; set; }

        public string WorkingFolder { get; set; }

        public string CCodesPath { get; set; }

        public string LastPath { get; set; }

        public string PlatformsFile { get; set; }

        public string PlatformsFolder { get; set; }
        #endregion

        #region Categories (structure interne des archives à faire correspondre aux dossiers LaunchBox)

        public string CheatCodes { get; set; }

        public string Games { get; set; }

        public string Images { get; set; }

        public string Manuals { get; set; }

        public string Musics { get; set; }

        public string Videos { get; set; }
        #endregion

        // ---

        #region features
        /// <summary>
        /// Utilisation des custom fields
        /// </summary>
        public bool UseCustomFields { get; set; }

        /// <summary>
        /// Placer les jeux dans un dossier portant leur nom
        /// </summary>
        public bool UseGameNameFolder { get; set; }


        #endregion

        #region Temp
        /// <summary>
        /// Dernier chemin visité pour l'archive
        /// </summary>
        public string LastArchivePath { get; set; }



        /// <summary>
        /// Dernier chemin visité pour les archives déjà dépackées
        /// </summary>
        public string LastFolderPath { get; set; }

        public string LastTargetPath { get; set; }
        #endregion

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
            this.CheatCodes = config.CheatCodes;
            this.Games = config.Games;
            this.Images = config.Images;
            this.Manuals = config.Manuals;
            this.Musics = config.Musics;
            this.Videos = config.Videos;
            //
            this.UseCustomFields = config.UseCustomFields;
            this.UseGameNameFolder = config.UseGameNameFolder;
            this.LastArchivePath = config.LastArchivePath;
            this.LastFolderPath = config.LastFolderPath;
            this.LastTargetPath = config.LastTargetPath;


            /*  this.ZipCompression = config.ZipCompression;
              this.SevZipCompression = config.SevZipCompression;
              this.ZipLvlCompression = config.ZipLvlCompression;
              this.SevZipLvlCompression = config.SevZipLvlCompression;*/
            //
            /*   this.KeepGameStruct = config.KeepGameStruct;
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
               this.CreateTreeV = config.CreateTreeV;*/

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

            if (!Directory.Exists(config.LastArchivePath))
                config.LastArchivePath = null;

            /*if (config.SevZipLvlCompression > 6 || config.SevZipLvlCompression < 1)
                throw new Exception("Problem on SevenZip level compression, value must be 0<x<7");

            if (config.ZipLvlCompression > 10 || config.ZipLvlCompression < 1)
                throw new Exception("Problem on Zip level compression, value must be 0<x<11");*/

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
                // 
                CheatCodes = Common._CheatCodes,
                Games = Common._Games,
                Images = Common._Images,
                Manuals = Common._Manuals,
                Musics = Common._Musics,
                Videos = Common._Videos ,
                //
                UseGameNameFolder = true,
                UseCustomFields = false,
                //LastPath
                /*     CopyCheats = false,
                     CopyClones = true,
                     CreateEBGame = true,
                     CreateTBGame = true,
                     CreateInfos = true,

                     UseLogWindow = true,
                    // CreateMD5 = true,

                     CreateTreeV = true,
                     UseLogFile = true,

                     KeepGameStruct = false,
                     KeepCheatStruct = true,
                     KeepManualStruct = true,
                     KeepMusicStruct = true,*/

                /* ZipCompression = true,
                 ZipLvlCompression = 10,
                 SevZipLvlCompression = 6,*/
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

        internal void InitPaths()
        {
                LaunchBoxPath = String.IsNullOrEmpty(LaunchBoxPath) ? null :
                                       Path.GetFullPath(LaunchBoxPath, AppDomain.CurrentDomain.BaseDirectory);
                WorkingFolder = String.IsNullOrEmpty(WorkingFolder) ? null :
                                       Path.GetFullPath(WorkingFolder, AppDomain.CurrentDomain.BaseDirectory);
                CCodesPath = String.IsNullOrEmpty(CCodesPath) ? null :
                                        Path.GetFullPath(CCodesPath, AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
