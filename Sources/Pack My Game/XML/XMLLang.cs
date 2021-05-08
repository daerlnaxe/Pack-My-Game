using Hermes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Pack_My_Game.XML
{
    /// <summary>
    /// Create xml language files
    /// </summary>
    class XMLLang
    {
        private const string _Language = "Language";
        private const string _LangAttr = "Lang";
        private const string _VersionAttr = "Version";
        private const string _Name = "Name";
        private const string _Value = "Value";

        public static void Make_ENVersion()
        {
            XElement xelRoot = new XElement(_Language,
                new XAttribute(_LangAttr, "en-EN"),
                new XAttribute(_VersionAttr, Common.LangVersion),
                new XAttribute(_Name, "English")
                );

            xelRoot.Add(
                new XElement("Add", new XAttribute(_Value, "Add")),
                new XElement("CCopy", new XAttribute(_Value, "Clones copy")),
                new XElement("Ch_Games", new XAttribute(_Value, "Choose game(s)")),
                new XElement("Ch_Path", new XAttribute(_Value, "Choose the path")),
                new XElement("Ch_LBPath", new XAttribute(_Value, "Choose the LaunchBox path")),
                new XElement("Ch_System", new XAttribute(_Value, "Choose a system (Consoles)")),
                new XElement("Cancel", new XAttribute(_Value, "Cancel")),
                new XElement("Cheats", new XAttribute(_Value, "Cheats")),
                new XElement("CheatsCopy", new XAttribute(_Value, "Cheat codes copy")),
                new XElement("CheatsPath", new XAttribute(_Value, "Cheat codes path")),
                new XElement("Compression", new XAttribute(_Value, "Compression")),
                new XElement("CompLvl", new XAttribute(_Value, "Level of compression")),
                new XElement("Comp_LBMin", new XAttribute(_Value, "Stock")),
                new XElement("Comp_LBMoy", new XAttribute(_Value, "Fast")),
                new XElement("Comp_LBMax", new XAttribute(_Value, "Optimal(Best)")),
                new XElement("Comp_Z", new XAttribute(_Value, "Zip compression")),
                new XElement("Comp_7Z", new XAttribute(_Value, "7Zip compression")),
                new XElement("Config", new XAttribute(_Value, "Configuration")),
                new XElement("Credits", new XAttribute(_Value,
                            "#Crédits, Thanks to:\r\n" +
                            "\t- My dear daughter waiting about her father, meanwhile i pack roms... In order to make her discover my youthness.\r\n" +
                            "\t- My ex/wife (she don't want to leave but it's useful, it's something between us) :D.\r\n" +
                            "\t- My beloved cats, those again here, those who left to early ... \r\n" +
                            "\t- DotNetZip creator, very good job, amazing ! \r\n"+
                            "\t- SevenZip creator, big thanks & bravo ! \r\n"+
                            "\r\n\r\n\r\n\t\t\t\t\t\tGreetings.")),
                new XElement("Delete", new XAttribute(_Value, "Delete")),
                new XElement("EnXB", new XAttribute(_Value, "Enhanced XML backup")),
                new XElement("File", new XAttribute(_Value, "File")),
                new XElement("FileExists", new XAttribute(_Value, "File exists")),
                new XElement("FilesFound", new XAttribute(_Value, "Files found")),
                new XElement("FolderExists", new XAttribute(_Value, "Folder exists")),
                new XElement("Games", new XAttribute(_Value, "Games")),
                new XElement("General", new XAttribute(_Value, "General")),
                                new XElement("Help", new XAttribute(_Value,
                            "Step to the left, step to the righ, clap your hands....Ok... sorry... " +
                            "\r\n\r\nIt's a bacic to help you." +
                            "\r\n\r\nWhat is it for... nothing, get out of the way ^^:" +
                            "\r\n\t- In short, it get ONE rom, its images and its xml data to build a compressed file with everything." +
                            "\r\n\t- Be careful, it will not be the same structure but (update) the unpacker is out to help you if you want to reinject" +
                            "\r\n\t- No, it does not affect your info, files, images, etc." +
                            "\r\n\r\n#1: Go to settings to select the Lauchbox folder." +
                            "\r\n\r\n#2: Select an output directory where to put the pack !!! DON'T SELECT the LauncherBox folder !!!" +
                            "\r\n\r\n#3: Optional you can specify the compression algorithm" +
                            "\r\n\r\n#4: The following steps are intuitive, you select the machine, then the game ... go."
                )),
                new XElement("Infos", new XAttribute(_Value, "Informations")),
                new XElement("InfosG", new XAttribute(_Value, "Infos games")),
                new XElement("LBPath", new XAttribute(_Value, "LaunchBox path")),
                new XElement("L_Games", new XAttribute(_Value, "Are you sure to want to pack this games ?")),
                new XElement("Loggers", new XAttribute(_Value, "Loggers")),
                new XElement("Main", new XAttribute(_Value, "Main")),
                new XElement("Manuals", new XAttribute(_Value, "Manuals")),
                new XElement("Musics", new XAttribute(_Value, "Musics")),
                new XElement("OK", new XAttribute(_Value, "OK")),
                new XElement("Options", new XAttribute(_Value, "Options")),
                new XElement("OriXB", new XAttribute(_Value, "Original XML backup")),
                new XElement("Params", new XAttribute(_Value, "Parameters")),
                new XElement("Paths", new XAttribute(_Value, "Paths")),
                new XElement("Process", new XAttribute(_Value, "Process")),
                new XElement("Region", new XAttribute(_Value, "Region")),
                new XElement("Result", new XAttribute(_Value, "Result")),
                new XElement("Q_Pack", new XAttribute(_Value, "Pack This game")),
                new XElement("S_Language", new XAttribute(_Value, "Select a language")),
                new XElement("Select", new XAttribute(_Value, "Select")),
                new XElement("Selected", new XAttribute(_Value, "Selected")),
                new XElement("Submit", new XAttribute(_Value, "Submit")),
                new XElement("Title", new XAttribute(_Value, "Title")),
                new XElement("TViewFile", new XAttribute(_Value, "Treeview file")),
                new XElement("TwoRulz", new XAttribute(_Value,
                "Two rules:\n" +
                "- Each Manual must be in a folder having the same system name AS the name in launchbox.\n" +
                @"    ex: F:\CheadCodes\Sega Megadrive\ && in LaunchBox the system is named Sega Megadrive => Enter F:\CheadCodes\ \n" +
                "-To select the manuals, they must have a name beginning by the Name of the game + \"-\" + (optionnal)something \n" +
                @"    ex: For Sonic, Cheadcode archive must begin by 'Sonic-'\n\n" +
                "Note: As much archive as you want.")),
                new XElement("Version", new XAttribute(_Value, "Version")),
                new XElement("Videos", new XAttribute(_Value, "Videos")),
                new XElement("Window", new XAttribute(_Value, "Window")),
                new XElement("WPath", new XAttribute(_Value, "Working path"))
                );


            xelRoot.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.LangFolder, "Lang.en-EN.xml"));
        }

        internal static void Make_FRVersion()
        {

            XElement xelRoot = new XElement(_Language,
                new XAttribute(_LangAttr, "fr-FR"),
                new XAttribute(_VersionAttr, Common.LangVersion),
                new XAttribute(_Name, "Français")
                );

            xelRoot.Add(
                new XElement("Add", new XAttribute(_Value, "Ajouter")),
                new XElement("CCopy", new XAttribute(_Value, "Copie des clones")),
                new XElement("Ch_Games", new XAttribute(_Value, "Choisir un/des jeu(x)")),
                new XElement("Ch_LBPath", new XAttribute(_Value, "Choisissez le chemin de LaunchBox")),
                new XElement("Ch_Path", new XAttribute(_Value, "Choisissez le chemin")),
                new XElement("Ch_System", new XAttribute(_Value, "Choisissez un système (Consoles)")),
                new XElement("Cancel", new XAttribute(_Value, "Annuler")),
                new XElement("CheatsCopy", new XAttribute(_Value, "Copie des cheatcodes")),
                new XElement("CheatsPath", new XAttribute(_Value, "Chemin cheat codes")),
                new XElement("CompLvl", new XAttribute(_Value, "Niveau de compression")),
                new XElement("Comp_LBMoy", new XAttribute(_Value, "Rapide")),
                new XElement("Comp_Z", new XAttribute(_Value, "Compression Zip")),
                new XElement("Comp_7Z", new XAttribute(_Value, "Compression 7Zip")),
                new XElement("Credits", new XAttribute(_Value,
                            "#Crédits, Merci à:\r\n" +
                            "\t- Ma petite puce qui patiente pendant que papa fait des packs de rom... Afin de lui faire découvrir mon adolescence.\r\n" +
                            "\t- Mon ex - femme(qui se tape l'incruste, mais c'est pratique) :D.\r\n" +
                            "\t- Mes chachats, ceux qui sont là, ceux qui sont partis trop tôt... \r\n" +
                            "\t- Le créateur de DotNetZip, très bon job, vraiment balaise! \r\n" +
                            "\t- Le créateur de SevenZip, merci et bravo ! \r\n" +
                            "\r\n\r\n\r\n\t\t\t\t\t\tAmitiés.")),
                new XElement("Delete", new XAttribute(_Value, "Effacer")),
                new XElement("EnXB", new XAttribute(_Value, "Copie XML amélioré")),
                new XElement("File", new XAttribute(_Value, "Fichier")),
                new XElement("FileExists", new XAttribute(_Value, "Le fichier existe")),
                new XElement("FilesFound", new XAttribute(_Value, "Fichiers trouvés")),
                new XElement("FolderExists", new XAttribute(_Value, "Le dossier existe")),
                new XElement("General", new XAttribute(_Value, "Général")),
                new XElement("Help", new XAttribute(_Value,
                            "Step to the left, step to the righ, clap your hands....Ok... sorry... " +
                            "\r\n\r\nC'est une version basique pour vous aider." +
                            "+\r\n\r\nA quoi ça sert...  a rien, barrez vous ^ ^:" +
                            "\r\n\t- Bref, ça récupère UNE rom, ses images et ses datas xml pour construire un zip avec le tout." +
                            "\r\n\t- Attention, ça ne sera pas la même structure mais (update) l'unpacker est sorti pour vous aider si vous voulez réinjecter" +
                            "\r\n\t- Non, ça n'altère en rien vos infos, vos fichiers, vos images, etc" +
                            "\r\n\r\n#1: Allez dans les paramètres pour sélectionner le dossier de Lauchbox." +
                            "\r\n\r\n#2: Selectionner un répertoire de sortie où mettre le pack. !!! NE SELECTIONNEZ PAS le dossier LauncherBox !!!" +
                            "\r\n\r\n#3: Optionnel vous pouvez spécifier l'algorithme de compression" +
                            "\r\n\r\n#4: Les étapes suivantes sont intuitives, vous sélectionez la machine, puis le jeu... go."
                )),
                new XElement("LBPath", new XAttribute(_Value, "Chemin launchBox")),
                new XElement("L_Games", new XAttribute(_Value, "Etes vous sûr(e) de vouloir packer ces jeux ?")),
                new XElement("Loggers", new XAttribute(_Value, "Traçage")),
                new XElement("Main", new XAttribute(_Value, "Principal")),
                new XElement("Manuals", new XAttribute(_Value, "Manuels")),
                new XElement("Musics", new XAttribute(_Value, "Musiques")),
                new XElement("OriXB", new XAttribute(_Value, "Copie XML originel")),
                new XElement("Params", new XAttribute(_Value, "Paramètres")),
                new XElement("Paths", new XAttribute(_Value, "Chemins")),
                new XElement("Process", new XAttribute(_Value, "Processus")),
                new XElement("Q_Pack", new XAttribute(_Value, "Packer ce jeu")),
                new XElement("Result", new XAttribute(_Value, "Résultat")),
                new XElement("Title", new XAttribute(_Value, "Titre")),
                new XElement("TViewFile", new XAttribute(_Value, "Fichier d'arborescence")),
                new XElement("TwoRulz", new XAttribute(_Value,
                "Deux règles:" + Environment.NewLine +
                "-Chaque Manuel doit être dans un dossier ayant le même nom de système que le nom dans LaunchBox." + Environment.NewLine +
                @"    ex: F:\CheadCodes\Sega Megadrive\ && dans LaunchBox le système est nommée Sega Megadrive => Entrez F:\CheadCodes\" + Environment.NewLine +
                "-Pour sélectionner les manuels, ils doivent avoir un nom commençant par le Nom du jeu + \"-\" + (optionnel)quelque chose." + Environment.NewLine +
                @"    ex: Pour Sonic, l'Archive Cheadcode doit commencer par  'Sonic-'" + Environment.NewLine + Environment.NewLine +
                "Note: Autant d'archives que vous voulez.")),
                new XElement("S_Language", new XAttribute(_Value, "Sélectionnez une langue")),
                new XElement("Select", new XAttribute(_Value, "Sélectionnez")),
                new XElement("Selected", new XAttribute(_Value, "Sélection")),
                new XElement("Submit", new XAttribute(_Value, "Soumettre")),
                new XElement("Videos", new XAttribute(_Value, "Vidéos")),
                new XElement("Window", new XAttribute(_Value, "Fenêtre")),
                new XElement("WPath", new XAttribute(_Value, "Dossier de travail"))
                );

            xelRoot.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.LangFolder, "Lang.fr-FR.xml"));
        }



        internal static Cont.Language ShortLoad(string xmlFile)
        {
            XElement xelRoot = XElement.Load(xmlFile);
            Cont.Language lang = new Cont.Language();

            lang.Lang = xelRoot.Attribute(_LangAttr).Value;
            lang.Version = xelRoot.Attribute(_VersionAttr).Value;
            lang.Name = xelRoot.Attribute(_Name).Value;

            return lang;
        }


        /// <summary>
        /// Load Language File
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        internal static Cont.Language Load(string xmlFile)
        {

            XElement xelRoot = XElement.Load(xmlFile);
            Cont.Language lang = new Cont.Language();

            lang.Lang = xelRoot.Attribute(_LangAttr).Value;
            lang.Version = xelRoot.Attribute(_VersionAttr).Value;

            HeTrace.WriteLine($"Language {lang.Lang} - {lang.Version}, loading...");

            foreach (var element in xelRoot.Descendants())
            {
                switch (element.Name.LocalName)
                {
                    case "Add":
                        lang.AddExp = element.Attribute(_Value).Value;
                        break;

                    case "CCopy":
                        lang.CCopyExp = element.Attribute(_Value).Value;
                        break;

                    case "Ch_Games":
                        lang.Ch_Games = element.Attribute(_Value).Value;
                        break;

                    case "Ch_LBPath":
                        lang.Ch_LBPath = element.Attribute(_Value).Value;
                        break;

                    case "Ch_Path":
                        lang.Ch_Path = element.Attribute(_Value).Value;
                        break;

                    case "Ch_System":
                        lang.Ch_System = element.Attribute(_Value).Value;
                        break;

                    case "Cancel":
                        lang.CancelExp = element.Attribute(_Value).Value;
                        break;

                    case "Cheats":
                        lang.CheatsExp = element.Attribute(_Value).Value;
                        break;

                    case "CheatsCopy":
                        lang.CheatsCopyExp = element.Attribute(_Value).Value;
                        break;

                    case "Compression":
                        lang.CompExp = element.Attribute(_Value).Value;
                        break;

                    case "Comp_LBMin":
                        lang.CompLBMin = element.Attribute(_Value).Value;
                        break;

                    case "Comp_LBMoy":
                        lang.CompLBMoy = element.Attribute(_Value).Value;
                        break;

                    case "Comp_LBMax":
                        lang.CompLBMax = element.Attribute(_Value).Value;
                        break;

                    case "Comp_Z":
                        lang.CompZExp = element.Attribute(_Value).Value;
                        break;

                    case "Comp_7Z":
                        lang.Comp7ZExp = element.Attribute(_Value).Value;
                        break;

                    case "CheatsPath":
                        lang.CheatsPathExp = element.Attribute(_Value).Value;
                        break;

                    case "CompLvl":
                        lang.LvlCompExp = element.Attribute(_Value).Value;
                        break;

                    case "Config":
                        lang.ConfigExp = element.Attribute(_Value).Value;
                        break;

                    case "Credits":
                        lang.Credits = element.Attribute(_Value).Value;
                        break;

                    case "Delete":
                        lang.DeleteExp = element.Attribute(_Value).Value;
                        break;

                    case "EnXB":
                        lang.EnXBExp = element.Attribute(_Value).Value;
                        break;

                    case "File":
                        lang.FileExp = element.Attribute(_Value).Value;
                        break;
                    case "FileExists":
                        lang.FileEExp = element.Attribute(_Value).Value;
                        break;

                    case "FilesFound":
                        lang.FilesFoundExp = element.Attribute(_Value).Value;
                        break;

                    case "FolderExists":
                        lang.FolderEEXp = element.Attribute(_Value).Value;
                        break;

                    case "Games":
                        lang.GamesExp = element.Attribute(_Value).Value;
                        break;     
                    
                    case "General":
                        lang.GeneralExp = element.Attribute(_Value).Value;
                        break;

                    case "Help":
                        lang.Help = element.Attribute(_Value).Value;
                        break;

                    case "Infos":
                        lang.InfExp = element.Attribute(_Value).Value;
                        break;

                    case "InfosG":
                        lang.InfosGExp = element.Attribute(_Value).Value;
                        break;

                    case "L_Games":
                        lang.ListGamesExp = element.Attribute(_Value).Value;
                        break;

                    case "LBPath":
                        lang.LBPathExp = element.Attribute(_Value).Value;
                        break;

                    case "Loggers":
                        lang.LoggersExp = element.Attribute(_Value).Value;
                        break;

                    case "Main":
                        lang.MainExp = element.Attribute(_Value).Value;
                        break;

                    case "Manuals":
                        lang.ManualsExp = element.Attribute(_Value).Value;
                        break; 
                    
                    case "Musics":
                        lang.MusicsExp = element.Attribute(_Value).Value;
                        break;

                    case "OK":
                        lang.OkExp = element.Attribute(_Value).Value;
                        break;

                    case "Options":
                        lang.OptionsExp = element.Attribute(_Value).Value;
                        break;

                    case "OriXB":
                        lang.OriXBExp = element.Attribute(_Value).Value;
                        break;

                    case "Params":
                        lang.ParamsExp = element.Attribute(_Value).Value;
                        break;

                    case "Paths":
                        lang.PathsExp = element.Attribute(_Value).Value;
                        break;

                    case "Process":
                        lang.ProcessExp = element.Attribute(_Value).Value;
                        break;

                    case "Q_Pack":
                        lang.Q_Pack = element.Attribute(_Value).Value;
                        break;

                    case "Region":
                        lang.RegionExp = element.Attribute(_Value).Value;
                        break;

                    case "Result":
                        lang.ResultExp = element.Attribute(_Value).Value;
                        break;

                    case "S_Language":
                        lang.S_Language = element.Attribute(_Value).Value;
                        break;

                    case "Select":
                        lang.Select = element.Attribute(_Value).Value;
                        break;

                    case "Selected":
                        lang.SelectedExp = element.Attribute(_Value).Value;
                        break;


                    case "Submit":
                        lang.SubmitExp = element.Attribute(_Value).Value;
                        break;


                    case "Title":
                        lang.TitleExp = element.Attribute(_Value).Value;
                        break;

                    case "TViewFile":
                        lang.TViewFExp = element.Attribute(_Value).Value;
                        break;

                    case "TwoRulz":
                        lang.Rulz = element.Attribute(_Value).Value;
                        break;

                    case "Version":
                        lang.VersionExp = element.Attribute(_Value).Value;
                        break;

                    case "Videos":
                        lang.VideosExp = element.Attribute(_Value).Value;
                        break;

                    case "WPath":
                        lang.WPathExp = element.Attribute(_Value).Value;
                        break;

                    case "Window":
                        lang.WindowExp = element.Attribute(_Value).Value;
                        break;


                    default:
                        HeTrace.WriteLine($"\t{element.Name.LocalName} is not supported");
                        break;

                }
            }

            return lang;
        }
    }
}
