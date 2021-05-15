using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text.Json;
using DxTBoxCore.Box_MBox;
using Common_Graph.Language;
using System.Text.Json.Serialization;

namespace Pack_My_Game.Language
{
    public class LangContent : ALangCont// : INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        /*private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/



        /// <summary>
        /// Choose game(s)
        /// </summary>
        public string Choose_Games { get; set; } = "Choose game(s)";

        /// <summary>
        /// Choose a system
        /// </summary>
        public string Choose_System { get; set; } = "Choose a system (Consoles)";

        // ---
        /// <summary>
        /// Command Test Game Links
        /// </summary>
        [JsonPropertyName("Cmd.TestGmLinks")]
        public string Cmd_TestGmLinks { get; set; } = "Test game links";

        /// <summary>
        /// Command Extract TBGame
        /// </summary>
        [JsonPropertyName("Cmd.ExtTBGame")]
        public string Cmd_ExtTBGame { get; set; } = " Extract True Backup";

        /// <summary>
        /// Command extract NPGame
        /// </summary>
        [JsonPropertyName("Cmd.ExtNBGame")]
        public string Cmd_ExtNBGame { get; set; } = "Extract NoPath Backup";

        /// <summary>
        /// Command extract Platform
        /// </summary>
        [JsonPropertyName("Cmd.ExtPlatform")]
        public string Cmd_ExtPlatform { get; set; } = "Extract Platform";

        /// <summary>
        /// Command extract default file
        /// </summary>
        [JsonPropertyName("Cmd.ExtDef")]
        public string Cmd_ExtDef { get; internal set; } = "Extract Default File";

        // ---

        /// <summary>
        /// Level of Compression
        /// </summary>
        public string Comp_Lvl { get; set; } = "Level of compression";

        /// <summary>
        /// Label min compression
        /// </summary>
        public string Comp_Min { get; set; } = "Stock";

        /// <summary>
        /// Label moy compression
        /// </summary>
        public string Comp_Moy { get; set; } = "Fast";

        /// <summary>
        /// Label strong compression forte
        /// </summary>
        public string Comp_Max { get; set; } = "Optimal (Best)";

        /// <summary>
        /// Zip compression
        /// </summary>
        public string Comp_Zip { get; set; } = "Zip Compression";

        /// <summary>
        /// 7Zip Compression
        /// </summary>
        public string Comp_7Z { get; set; } = "7Zip Compression";


        // ---

        #region options

        /// <summary>
        /// Clones copy
        /// </summary>
        public string Opt_CopyClones { get; set; } = "Copy clones";

        /// <summary>
        /// Cheats Copy
        /// </summary>
        public string Opt_CopyCheats { get; set; } = "Copy cheats";

        /// <summary>
        /// Enhanced XML backup
        /// </summary>
        public string Opt_EnXB { get; set; } = "Enhanced XML backup";

        /// <summary>
        /// Infos Games
        /// </summary>
        public string Opt_InfosG { get; set; } = "Infos Games";

        /// <summary>
        /// Keep Games Structure
        /// </summary>
        public string Opt_KeepGmStruct { get; set; } = "Keep games Structure";

        /// <summary>
        /// Keep Cheat Structure
        /// </summary>
        public string Opt_KeepCheatStruct { get; set; } = "Keep cheats Structure";

        /// <summary>
        /// Keep Manual Structure
        /// </summary>
        public string Opt_KeepManStruct { get; set; } = "Keep manuals Structure";

        /// <summary>
        /// Keep Music Structure
        /// </summary>
        public string Opt_KeepMusStruct { get; set; } = "Keep musics Structure";



        /// <summary>
        /// Original xlm backup
        /// </summary>
        public string Opt_OriXB { get; set; } = "Original XML Backup";

        /// <summary>
        /// TreeView File
        /// </summary>
        public string Opt_TViewF { get; set; } = "Treeview File";



        /// <summary>
        /// Log window
        /// </summary>
        public string Opt_Window { get; set; } = "Log window";


        #endregion

        // ---
        /// <summary>
        /// Question, keep it ?
        /// </summary>
        public object Q_KeepIt { get; set; } = "Do you want to keep it ?";

        /// <summary>
        /// Question pack this game
        /// </summary>
        public string Q_Pack { get; set; } = "Pack this game";

        public string Q_PackGames { get; set; } = "Are you sure to want to pack this games?";

        // ---


        /// <summary>
        /// Archive Name
        /// </summary>
        public string S_ArchiveName { get; set; } = "Archive name";

        /// <summary>
        /// Enter your cheat codes
        /// </summary>
        public string S_EnterYourCC { get; set; } = "Enter your cheat codes";

        /// <summary>
        /// Game Status
        /// </summary>
        public string S_GameStatus { get; set; } = "Game status";

        /// <summary>
        /// Game Status for
        /// </summary>
        public string S_GameStatus4 { get; set; } = "Game status for";

        /// <summary>
        /// Game title
        /// </summary>
        public string S_GameTitle { get; set; } = "Game title";

        /// <summary>
        /// Give an archive name
        /// </summary>
        public string S_GiveArchName { get; set; } = "Give an archive name";

        /// <summary>
        /// Main link application is broken
        /// </summary>
        public string S_MainLAppBroken { get; set; } = "Main link application is broken";

        /// <summary>
        /// Search for
        /// </summary>
        public string S_SearchFor { get; set; } = "Search for";

        /// <summary>
        /// Width Limit
        /// </summary>
        public string S_WidthLimit { get; set; } = "Width limit";
        // ---

        #region Words
        /// <summary>
        /// Active
        /// </summary>
        [JsonPropertyName("Word.Active")]
        public string Word_Active { get; set; } = "Active";

        /// <summary>
        /// Cheats
        /// </summary>
        [JsonPropertyName("Word.Cheats")]
        public string Word_Cheats { get; set; } = "Cheats";

        /// <summary>
        /// Compression
        /// </summary>
        [JsonPropertyName("Word.Compression")]
        public string Word_Compression { get; set; } = "Compression";

        /// <summary>
        /// Credits
        /// </summary>
        [JsonPropertyName("Word.Credits")]
        public string Word_Credits { get; set; } = "Credits";

        /// <summary>
        /// Extract
        /// </summary>
        [JsonPropertyName("Word.Extract")]
        public string Word_Extract { get; set; } = "Extract";

        /// <summary>
        /// Format
        /// </summary>
        [JsonPropertyName("Word.Format")]
        public string Word_Format { get; set; } = "Format";

        /// <summary>
        /// Help
        /// </summary>
        [JsonPropertyName("Word.Help")]
        public string Word_Help { get; set; } = "Help";

        /// <summary>
        /// Parameters
        /// </summary>
        [JsonPropertyName("Word.Params")]
        public string Word_Params { get; set; } = "Parameters";

        /// <summary>
        /// Word Region
        /// </summary>
        public string Word_Region { get; set; } = "Region";

        /// <summary>
        /// Title
        /// </summary>
        public string Word_Title { get; set; } = "Title";

        /// <summary>
        /// Word Version
        /// </summary>
        public string Word_Version { get; set; } = "Version";

        #endregion

        // ---

        /// <summary>
        /// Credits
        /// </summary>
        public string Long_Credits { get; set; } = "See Credits";

        /// <summary>
        /// Help
        /// </summary>
        public string Long_Help { get; set; } = "See Help";

        /// <summary>
        /// 2Rules
        /// </summary>
        public string Long_Rulz { get; set; } = "See Rulz";



        //--- a rajouter
        public string ListGamesExp { get; internal set; }
        

        internal static LangContent Load(string langFile)
        {
            string jsonString = File.ReadAllText(langFile);
            return JsonSerializer.Deserialize<LangContent>(jsonString);
        }

        internal void Save(string outputFile)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outputFile));

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
                DxMBox.ShowDial(ioExc.Message, $"Error on writing lang file '{outputFile}'");
            }
        }


        /*
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
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static LangContent MakeDefault()
        {
            LangContent lang = new LangContent()
            {

                //
                Long_Credits =
                "#Crédits, Thanks to:\r\n" +
                "\t- My dear daughter waiting about her father, meanwhile i pack roms... In order to make her discover my youthness.\r\n" +
                "\t- My ex/wife (she don't want to leave but it's useful, it's something between us) :D.\r\n" +
                "\t- My beloved cats, those again here, those who left to early ... \r\n" +
                "\t- DotNetZip creator, very good job, amazing ! \r\n" +
                "\t- SevenZip creator, big thanks & bravo ! \r\n" +
                "\r\n\r\n\r\n\t\t\t\t\t\tGreetings.",
                Long_Help =
                "Step to the left, step to the righ, clap your hands....Ok... sorry... " +
                "\r\n\r\nIt's a bacic to help you." +
                "\r\n\r\nWhat is it for... nothing, get out of the way ^^:" +
                "\r\n\t- In short, it get ONE rom, its images and its xml data to build a compressed file with everything." +
                "\r\n\t- Be careful, it will not be the same structure but (update) the unpacker is out to help you if you want to reinject" +
                "\r\n\t- No, it does not affect your info, files, images, etc." +
                "\r\n\r\n#1: Go to settings to select the Lauchbox folder." +
                "\r\n\r\n#2: Select an output directory where to put the pack !!! DON'T SELECT the LauncherBox folder !!!" +
                "\r\n\r\n#3: Optional you can specify the compression algorithm" +
                "\r\n\r\n#4: The following steps are intuitive, you select the machine, then the game ... go.",
                Long_Rulz =
                "Two rules:\n" +
                "- Each Manual must be in a folder having the same system name AS the name in launchbox.\n" +
                @"    ex: F:\CheadCodes\Sega Megadrive\ && in LaunchBox the system is named Sega Megadrive => Enter F:\CheadCodes\ \n" +
                "-To select the manuals, they must have a name beginning by the Name of the game + \"-\" + (optionnal)something \n" +
                @"    ex: For Sonic, Cheadcode archive must begin by 'Sonic-'\n\n" +
                "Note: As much archive as you want.",
            };

            lang.SetDefault(Common.LangVersion);

            return lang;
        }


        internal static LangContent DefaultFrench()
        {
            LangContent lang = new LangContent()
            {
                Choose_Games = "Choisir un/des jeu(x)",
                Choose_System = "Choisissez un système (Consoles)",
                //
                Comp_Lvl = "Niveau de compression",
                Comp_Moy = "Rapide",
                Comp_Zip = "Compression Zip",
                Comp_7Z = "Compression 7Zip",
                //
                Opt_CopyClones = "Copie des clones",
                Opt_CopyCheats = "Copie des cheatcodes",
                Opt_EnXB = "Copie XML amélioré",
                Opt_OriXB = "Copie XML originel",
                Opt_TViewF = "Fichier d'arborescence",
                Opt_Window = "Fenêtre de traçage",
                //
                Q_KeepIt = "Voulez vous le garder ?",
                Q_Pack = "Packer ce jeu",
                Q_PackGames = "Etes vous sûr(e) de vouloir packer ces jeux ?",
                //
                S_ArchiveName = "Nom d'archive",

                S_EnterYourCC = "Entre vos cheats codes",
                S_GameStatus = "Etat du jeu",
                S_GameStatus4 = "Etat du jeu pour",
                S_GameTitle = "Titre du jeu",
                S_GiveArchName = "Donnez un nom d'archive",
                S_MainLAppBroken = "Lien de l'application principale cassé",
                S_SearchFor = "Recherche de",
                S_WidthLimit = "Limite de largeur",
                //
                Word_Active = "Activé",
                Word_Extract = "Extraire",
                Word_Format = "Formatter",
                Word_Params = "Paramètres",
                Word_Region = "Région",
                Word_Title = "Titre",

                //
                Long_Credits =
                "#Crédits, Merci à:\r\n" +
                "\t- Ma petite puce qui patiente pendant que papa fait des packs de rom... Afin de lui faire découvrir mon adolescence.\r\n" +
                "\t- Mon ex - femme(qui se tape l'incruste, mais c'est pratique) :D.\r\n" +
                "\t- Mes chachats, ceux qui sont là, ceux qui sont partis trop tôt... \r\n" +
                "\t- Le créateur de DotNetZip, très bon job, vraiment balaise! \r\n" +
                "\t- Le créateur de SevenZip, merci et bravo ! \r\n" +
                "\r\n\r\n\r\n\t\t\t\t\t\tAmitiés.",
                Long_Help =
                "Step to the left, step to the righ, clap your hands....Ok... sorry... " +
                "\r\n\r\nC'est une version basique pour vous aider." +
                "+\r\n\r\nA quoi ça sert...  a rien, barrez vous ^ ^:" +
                "\r\n\t- Bref, ça récupère UNE rom, ses images et ses datas xml pour construire un zip avec le tout." +
                "\r\n\t- Attention, ça ne sera pas la même structure mais (update) l'unpacker est sorti pour vous aider si vous voulez réinjecter" +
                "\r\n\t- Non, ça n'altère en rien vos infos, vos fichiers, vos images, etc" +
                "\r\n\r\n#1: Allez dans les paramètres pour sélectionner le dossier de Lauchbox." +
                "\r\n\r\n#2: Selectionner un répertoire de sortie où mettre le pack. !!! NE SELECTIONNEZ PAS le dossier LauncherBox !!!" +
                "\r\n\r\n#3: Optionnel vous pouvez spécifier l'algorithme de compression" +
                "\r\n\r\n#4: Les étapes suivantes sont intuitives, vous sélectionez la machine, puis le jeu... go.",
                Long_Rulz =
                "Deux règles:" + Environment.NewLine +
                "-Chaque Manuel doit être dans un dossier ayant le même nom de système que le nom dans LaunchBox." + Environment.NewLine +
                @"    ex: F:\CheadCodes\Sega Megadrive\ && dans LaunchBox le système est nommée Sega Megadrive => Entrez F:\CheadCodes\" + Environment.NewLine +
                "-Pour sélectionner les manuels, ils doivent avoir un nom commençant par le Nom du jeu + \"-\" + (optionnel)quelque chose." + Environment.NewLine +
                @"    ex: Pour Sonic, l'Archive Cheadcode doit commencer par  'Sonic-'" + Environment.NewLine + Environment.NewLine +
                "Note: Autant d'archives que vous voulez.",
            };

            lang.SetFrenchDefault(Common.LangVersion);
            return lang;
        }


    }
}
