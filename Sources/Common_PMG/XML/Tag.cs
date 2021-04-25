using System;
using System.Collections.Generic;
using System.Text;

namespace Common_PMG.XML
{
    public static class Tag
    {
        #region Common
        public static readonly string Name = "Name";
        #endregion



        #region Games
        public static readonly string AddApp = "AdditionalApplication";
        public static readonly string AltName = "AlternateName";
        public static readonly string AppPath = "ApplicationPath";
        public static readonly string CustField = "CustomField";
        public static readonly string Game = "Game";
        public static readonly string GameId = "GameID";

        /// <summary>
        /// ID
        /// </summary>
        public static readonly string ManualP = "ManualPath";
        #endregion

        #region Platforms
        public static readonly string Platform = "Platform";
        public static readonly string PlatformFolder = "PlatformFolder";
        public static readonly string Games = "Games";
        public static readonly string MediaTManual = "Manual";
        public static readonly string MediaTMusic = "Music";
        public static readonly string MediaTVideo = "Video";
        public static readonly string MediaTThemeVideo = "ThemeVideo";

        #endregion
    }
}
