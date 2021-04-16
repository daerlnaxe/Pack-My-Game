using System;
using System.Text.RegularExpressions;

namespace LaunchBox_XML
{
    public class Common
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

        /*
            '<>':   replaced
            ':' :   get first part only
            '"' :   replaced
            '/' :   replaced
            '\' :   replaced
            '|' :   replaced
            '?' :   removed
            '*' :   replaced
        */
        public static string WindowsConv_TitleToFileName(string title)
        {
            string tmp = title.Split(":")[0];                      

            tmp = new Regex(@"[<>""/\\|*]").Replace(tmp, " ");
            tmp = new Regex(@"[?]").Replace(tmp, "");

            return tmp;
        }

    }
}
