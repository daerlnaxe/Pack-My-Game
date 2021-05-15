using Common_Graph.Language;
using DxTBoxCore.Box_MBox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UnPack_My_Game.Language
{
    public class LangProvider : ALangCont
    {
        /// <summary>
        /// DPG Creator
        /// </summary>
        public string S_DPGCreator { get; set; } = "DPG creator";

        /// <summary>
        /// Select a archive to make a DPG.
        /// </summary>
        public string S_DPGwArchive { get; internal set; } = "Select an archive to make a DPG.";

        /// <summary>
        /// Select a folder to make a DPG
        /// </summary>
        public string S_DPGwFolder { get; set; } = "Select a folder to make a DPG.";

        /// <summary>
        /// Import & inject to LaunchBox
        /// </summary>
        public string S_ImportILaunchBox { get; set; } = "Import & Inject to LaunchBox";

        /// <summary>
        /// Injection by file(s)
        /// </summary>
        public string S_InjectbFile { get; set; } = "Injection by File(s)";
        /// <summary>
        /// Injection by folder(s)
        /// </summary>
        public string S_InjectbFolder { get; set; } = "Injection by Folder(s)";

        /// <summary>
        /// Injection des custom fields
        /// </summary>
        public string S_InjectCustomFields { get; set; } = "Inject Custom Fields";

        /// <summary>
        /// Inject Platform
        /// </summary>
        public string S_InjectPlatform { get; set; } = "Inject a Platform";

        /// <summary>
        /// Placer les roms dans un dossier au nom du jeu
        /// </summary>
        public string S_PlaceRomsToGameName { get; set; } = "Place roms into a folder with the game name";

        /// <summary>
        /// Remise à zéro des paramètres
        /// </summary>
        public string S_ResetFactory { get; set; } = "Reset factory";

        /// <summary>
        /// Reset d'un dossier
        /// </summary>
        public string S_ResetFolder { get; set; } = "Reset a folder";

        /// <summary>
        /// Unpack To LaunchBox
        /// </summary>
        public string S_UnpackTLaunchBox { get; set; } = "Unpack to LaunchBox";
        //

        /// <summary>
        /// Add files by contextual menu
        /// </summary>
        public string T_AddFileCM { get; set; } = "Add file(s) by the contextual menu";

        /// <summary>
        /// Add folders by contextual menu
        /// </summary>
        public string T_AddFolderCM { get; set; } = "Add folder(s) by the contextual menu";

        /// <summary>
        /// Tooltip DPG Creator
        /// </summary>
        public string T_DPGCreator { get; set; } = "Make a DPG with TBGame or EBGame";

        /// <summary>
        /// Tooltip Import
        /// </summary>
        public string T_Import { get; set; } = "Extract, Inject, Copy to import a game in LaunchBox";

        /// <summary>
        /// Tooltip Inject Game
        /// </summary>
        public string T_InjectGame { get; set; } = "Inject without copy, a game in LaunchBox";

        /// <summary>
        /// Tooltip Inject Platform
        /// </summary>
        public string T_InjectPlatform { get; set; } = "Inject a platform in LaunchBox";

        /// <summary>
        /// Archive(s)
        /// </summary>
        [JsonPropertyName("Word.Archive_S")]
        public string Word_Archive_s { get; set; } = "Archive(s)";

        /// <summary>
        /// File(s)
        /// </summary>
        [JsonPropertyName("Word.File_s")]
        public string Word_File_s { get; set; } = "File(s)";

        /// <summary>
        /// Folder(s)
        /// </summary>
        [JsonPropertyName("Word.Folder_s")]
        public string Word_Folder_s { get; set; } = "Folder(s)";

        /// <summary>
        /// Import
        /// </summary>
        [JsonPropertyName("Word.Import")]
        public string Word_Import { get; set; } = "Import";

        /// <summary>
        /// Inject
        /// </summary>
        [JsonPropertyName("Word.Inject")]
        public string Word_Inject { get; set; } = "Inject";

        /// <summary>
        /// Langue
        /// </summary>
        [JsonPropertyName("Word.Languages")]
        public string Word_Languages { get; set; } = "Languages";

        /// <summary>
        /// Video
        /// </summary>
        [JsonPropertyName("Word.Video")]
        public string Word_Video { get; set; } = "Video";

        // ---
        public string Archive_Structure { get; set; } = "Archive structure";
        

        internal void Update()
        {
            this.Version = Common.LangVersion;
        }


        internal static LangProvider Read(string file)
        {
            string jsonString = File.ReadAllText(file);
            return JsonSerializer.Deserialize<LangProvider>(jsonString);

        }


        /// <summary>
        /// Sauvegarder vers un fichier
        /// </summary>
        /// <param name="usLangFile"></param>
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

        internal static LangProvider MakeDefault()
        {
            LangProvider lang = new LangProvider()
            {

            };
            lang.SetDefault(Common.LangVersion);
            return lang;
        }


        internal static LangProvider DefaultFrench()
        {
            LangProvider lang = new LangProvider()
            {
                //
                Archive_Structure = "Structure de l'archive",
                S_DPGCreator = "Créateur DPG",
                S_DPGwArchive = "Sélectionner une archive pour faire un DPG",
                S_DPGwFolder = "Sélectionner un dossier pour faire un DPG",
                S_ImportILaunchBox = "Importe & Injecte dans LaunchBox",
                S_InjectbFile = "Injection par fichier(s)",
                S_InjectbFolder = "Injection par dossier(s)",
                S_InjectCustomFields = "Injecter les champs personnalisés",
                S_InjectPlatform = "Injecter 1 Machine",
                S_ResetFolder = "Reset un dossier",
                S_ResetFactory = "Réglages d'usine",
                S_PlaceRomsToGameName = "Placer les roms dans un dossier portant le nom du jeu",
                S_UnpackTLaunchBox = "Dépacker vers LaunchBox",
                T_AddFileCM = "Ajouter un/des fichier(s) par le menu contextuel",
                T_AddFolderCM = "Ajouter un/des dossier(s) par le menu contextuel",
                T_DPGCreator = "Crée un DPG avec un fichier TBGame ou EBGame",
                T_Import = "Extracter, Injecter, Copier pour importer un jeu dans LaunchBox",
                T_InjectGame = "Injecter sans copier, un jeu dans LaunchBox",
                T_InjectPlatform = "Injecter une machine",

                Word_File_s = "Fichier(s)",
                Word_Folder_s ="Dossier(s)",
                Word_Import = "Importer",
                Word_Inject = "Injecter",
            };
            lang.SetFrenchDefault(Common.LangVersion);
            return lang;
        }
    }
}
