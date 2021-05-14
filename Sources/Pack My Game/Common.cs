using Pack_My_Game.Cont;
using Pack_My_Game.Language;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Pack_My_Game
{
    public class Common
    {
        /// <summary>
        /// Log Folder
        /// </summary>
        public static string Logs => "Logs";

        /// <summary>
        /// Lang Folder
        /// </summary>
        internal static string LangFolder => "Languages";


        /// <summary>
        /// Language Version
        /// </summary>
        public static string LangVersion => "1.0.0.6";

        public static string ConfigVersion => "1.0.0.3";

        #region Médias 
        public static string CheatCodes => "CheatCodes";
        /// <summary>
        /// Games
        /// </summary>
        public static string Games => "Games";
        public static string Images => "Images";
        public static string Manuals => "Manuals";
        public static string Musics => "Musics";
        public static string Videos => "Videos";
        #endregion

        /// <summary>
        /// Language object
        /// </summary>
     //   internal static Language ObjectLang { get; set; }

        /// <summary>
        /// Configuration object
        /// </summary>
        internal static Configuration Config 
        { 
            get;
            set;
        }


        public static readonly RoutedUICommand ProcessCommand =
            new RoutedUICommand(LanguageManager.Instance.Lang.Word_Process, "Process", typeof(Common));
        public static readonly RoutedUICommand SubmitCommand =
            new RoutedUICommand(LanguageManager.Instance.Lang.Word_Submit, "Submit", typeof(Common));
    }
}
