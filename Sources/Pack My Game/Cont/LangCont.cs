using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;

namespace Pack_My_Game.Cont
{
    public class LangCont : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Lang { get; internal set; }
        public string Version { get; internal set; }
        public string Name { get; internal set; }

        // ---

        /// <summary>
        /// Clones copy
        /// </summary>
        public string CCopyExp { get; internal set; } = "Clones copy";
        
        /// <summary>
        /// Choose game(s)
        /// </summary>
        public string Ch_Games { get; internal set; } = "Choose game(s)";

        /// <summary>
        /// Choose the LaunchBoxPath
        /// </summary>
        public string Ch_LBPath { get; internal set; } = "Choose the LaunchBox path";

        /// <summary>
        /// Choose a path
        /// </summary>
        public string Ch_Path { get; internal set; } = "Choose the path";

        /// <summary>
        /// Choose a system
        /// </summary>
        public string Ch_System { get; internal set; } = "Choose a system";

        /// <summary>
        /// Cheats Copy
        /// </summary>
        public string CheatsCopyExp { get; internal set; } = "Cheats copy";

        /// <summary>
        /// Cheats codes path
        /// </summary>
        public string CheatsPathExp { get; internal set; } = "Cheats codes path";
        
        /// <summary>
        /// Compression
        /// </summary>
        public string CompExp { get; internal set; } = "Compression";
        
        /// <summary>
        /// Label compression mini
        /// </summary>
        public string CompLBMin { get; internal set; } = "Stock";
        
        /// <summary>
        /// Label compression moyen
        /// </summary>
        public string CompLBMoy { get; internal set; } = "Fast";

        /// <summary>
        /// Label compression forte
        /// </summary>
        public string CompLBMax { get; internal set; } = "Optimal (Best)";

        /// <summary>
        /// Zip compression
        /// </summary>
        public string CompZExp { get; internal set; } = "Zip Compression";

        /// <summary>
        /// 7Zip Compression
        /// </summary>
        public string Comp7ZExp { get; internal set; } = "7Zip Compression";

        /// <summary>
        /// Configuration
        /// </summary>
        public string ConfigExp { get; set; } = "Configuration";

        /// <summary>
        /// Enhanced XML backup
        /// </summary>
        public string EnXBExp { get; internal set; } = "Enhanced XML backup";

        /// <summary>
        /// File
        /// </summary>
        public string FileExp { get; internal set; } = "File";

        /// <summary>
        /// File exists
        /// </summary>
        public string FileEExp { get; internal set; } = "File exists";

        /// <summary>
        /// Folder exists
        /// </summary>
        public string FolderEEXp { get; internal set; } = "Folder exists";


        /// <summary>
        /// General
        /// </summary>
        public string GeneralExp { get; set; } = "General";
        /// <summary>
        /// Informations
        /// </summary>
        public string InfExp { get; internal set; } = "Informations";
        /// <summary>
        /// Infos Games
        /// </summary>
        public string InfosGExp { get; internal set; } = "Infos Games";
        /// <summary>
        /// Launchbox path
        /// </summary>
        public string LBPathExp { get; internal set; } = "LaunchBox Path";
        /// <summary>
        /// Loggers
        /// </summary>
        public string LoggersExp { get; internal set; } = "Loggers";
        /// <summary>
        /// Level of Compression
        /// </summary>
        public string LvlCompExp { get; internal set; } = "Level of compression";
        /// <summary>
        /// Main
        /// </summary>
        public string MainExp { get; internal set; } = "Main";
        /// <summary>
        /// Options
        /// </summary>
        public string OptionsExp { get; set; } = "Options";
        /// <summary>
        /// Original xlm backup
        /// </summary>
        public string OriXBExp { get; internal set; } = "Original XML Backup";
        /// <summary>
        /// Paths
        /// </summary>
        public string PathsExp { get; internal set; } = "Paths";
        /// <summary>
        /// Parameters
        /// </summary>
        public string ParamsExp { get; internal set; } = "Parameters";
        /// <summary>
        /// Process
        /// </summary>
        public string ProcessExp { get; internal set; } = "Process";
        /// <summary>
        /// Word Region
        /// </summary>
        public string RegionExp { get; internal set; } = "Region";
        /// <summary>
        /// Question pack this game
        /// </summary>
        public string Q_Pack { get; internal set; } = "Pack this game";
        /// <summary>
        /// 2Rules
        /// </summary>
        public string Rulz { get; internal set; } = "See Rulz";
        /// <summary>
        /// Select a language
        /// </summary>
        public string S_Language { get; set; } = "Select a Language";
        /// <summary>
        /// Select
        /// </summary>
        public string Select { get; internal set; } = "Select";
        /// <summary>
        /// Word Title
        /// </summary>
        public string TitleExp { get; internal set; } = "Title";
        /// <summary>
        /// Submit
        /// </summary>
        public string SubmitExp { get; internal set; } = "Submit";
        /// <summary>
        /// TreeView File
        /// </summary>
        public string TViewFExp { get; internal set; } = "Treeview File";
        /// <summary>
        /// Word Version
        /// </summary>
        public string VersionExp { get; internal set; } = "Version";
        /// <summary>
        /// Working path
        /// </summary>
        public string WPathExp { get; set; } = "Working path";
        /// <summary>
        /// Window
        /// </summary>
        public string WindowExp { get; internal set; } = "Window";
        //--- a rajouter
        public string ListGamesExp { get; internal set; }

        public string SelectedExp { get; internal set; } = "Selected";
        public string CancelExp { get; internal set; } = "Cancel";
        public string OkExp { get; internal set; } = "OK";
        public string CheatsExp { get; internal set; } = "Cheats";
        public string ResultExp { get; internal set; } = "Result";
        public string FilesFoundExp { get; internal set; } = "Files found";
        public string GamesExp { get; internal set; } = "Games";
        public string ManualsExp { get; internal set; } = "Manuals";
        public string VideosExp { get; internal set; } = "Videos";
        public string MusicsExp { get; internal set; } = "Musics";
        public string AddExp { get; internal set; } = "Add";
        public string DeleteExp { get; internal set; } = "Delete";
        public string Credits { get; internal set; } = "Credits";
        public string Help { get; internal set; } = "Help";

        internal static void Check(string lang)
        {
            string langFile = Path.Combine(
                            AppDomain.CurrentDomain.BaseDirectory, Common.LangFolder, $"Lang.{lang}.xml");

            if (File.Exists(langFile) && XML.XMLLang.ShortLoad(langFile).Version.Equals(Common.LangVersion))
                return;

            if (lang.Equals("en-EN"))
                XML.XMLLang.Make_ENVersion();
            else if (lang.Equals("fr-FR"))
                XML.XMLLang.Make_FRVersion();
        }

    }
}
