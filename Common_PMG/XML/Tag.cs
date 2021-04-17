using System;
using System.Collections.Generic;
using System.Text;

namespace Common_PMG.XML
{
    public class Tag
    {
        #region Common
        public static string Name => "Name";
        #endregion



        #region Games
        public static string AddApp = "AdditionnalApplication";
        public static string AltName = "AlternateName";
        public static string CustField = "CustomField";
        public static string Game = "Game";
        public static string GameId = "GameID";
        /// <summary>
        /// ID
        /// </summary>
        public static string Id = "ID";
        public static string ManualP = "ManualPath";
        #endregion

        #region Platforms
        public static string Platform = "Platform";
        public static string PlatformFolder = "PlatformFolder";
        #endregion
    }
}
