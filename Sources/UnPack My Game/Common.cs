using System;
using System.Collections.Generic;
using System.Text;
using UnPack_My_Game.Cont;

namespace UnPack_My_Game
{
    public static class Common
    {
        public static string ConfigVersion => "1.0.0.0";


        /// <summary>
        /// Language Version
        /// </summary>
        public static string LangVersion => "1.0.0.1";

        public static Configuration Config 
        { 
            get;
            internal set;
        }

        public static string PlatformsFile { get; set; }

        /// <summary>
        /// Backup directory name
        /// </summary>
        public static readonly string BackUp = "Backup";
        public static readonly string Logs = "Logs";

        //
        internal const string _Games = "Games";
        internal const string _CheatCodes = "CheatCodes";
        internal const string _Images = "Images";
        internal const string _Manuals = "Manuals";
        internal const string _Musics = "Musics";
        internal const string _Videos = "Videos";
    }
}
