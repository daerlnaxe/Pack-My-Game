using System;
using System.Collections.Generic;
using System.Text;

namespace Common_PMG.XML
{
    public static class Tag
    {
        #region Common
        public static string Name => "Name";
        #endregion



        #region Games
        public static string AddApp = "AdditionalApplication";
        public static string AltName = "AlternateName";
        public static string AppPath = "ApplicationPath";
        public static string CustField = "CustomField";
        public static string Game = "Game";
        public static string GameId = "GameID";
        public static string ManPath = "ManualPath";
        public static string MusPath = "MusicPath";
        public static string VidPath= "VideoPath";
        public static string ThVidPath = "ThemeVideoPath";
        /// <summary>
        /// ID
        /// </summary>
        public static string ManualP = "ManualPath";
        #endregion

        #region Platforms
        public static string Platform = "Platform";
        public static string PlatformFolder = "PlatformFolder";
        public static string MediaTManual = "Manual";
        public static string MediaTMusic = "Music";
        public static string MediaTVideo = "Video";
        public static string MediaTThemeVideo = "ThemeVideo";

        #endregion
    }
}
