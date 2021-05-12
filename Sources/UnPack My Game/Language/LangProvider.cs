using DxTBoxCore.Box_MBox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UnPack_My_Game.Language
{
    public class LangProvider
    {
        /// <summary>
        /// Version du fichier
        /// </summary>
        public string Version { get; set; }

        public string Name { get; set; } = "Marre";

        #region Commun
        /// <summary>
        /// Chemin vers LaunchBox
        /// </summary>
        public string Folder_LaunchBox { get; set; }

        /// <summary>
        /// Dossier de travail
        /// </summary>
        public string Folder_Working { get; set; }

        /// <summary>
        /// Dossier des cheats codes
        /// </summary>
        public string Folder_CheatCodes { get; set; }
        #endregion

        /// <summary>
        /// Annuler
        /// </summary>
        [JsonPropertyName("Word.Cancel")]
        public string Word_Cancel { get; set; } = "Cancel";

        /// <summary>
        /// Jeux
        /// </summary>
        [JsonPropertyName("Word.Games")]
        public string Word_Games { get; set; } = "Games";

        /// <summary>
        /// Onglet général
        /// </summary>
        [JsonPropertyName("Word.General")]
        public string Word_General { get; set; } = "General";

        [JsonPropertyName("Word.Images")]
        public string Word_Images { get; set; } = "Images";

        /// <summary>
        /// Mot Informations
        /// </summary>
        [JsonPropertyName("Word.Informations")]
        public string Word_Informations { get; set; } = "Informations";

        /// <summary>
        /// Langue
        /// </summary>
        [JsonPropertyName("Word.Languages")]
        public string Word_Languages { get; set; } = "Languages";

        /// <summary>
        /// Principal
        /// </summary>
        [JsonPropertyName("Word.Main")]
        public string Word_Main { get; set; } = "Main";

        /// <summary>
        /// Manuels
        /// </summary>
        [JsonPropertyName("Word.Manuals")]
        public string Word_Manuals { get; set; } = "Manuals";

        /// <summary>
        /// Musiques
        /// </summary>
        [JsonPropertyName("Word.Musics")]
        public string Word_Musics { get; set; } = "Musics";

        /// <summary>
        /// Sauvegarder
        /// </summary>
        [JsonPropertyName("Word.Save")]
        public string Word_Save { get; set; } = "Save";

        /// <summary>
        /// Options
        /// </summary>
        [JsonPropertyName("Word.Options")]
        public string Word_Options { get; set; } = "Options";

        /// <summary>
        /// Paths
        /// </summary>
        [JsonPropertyName("Word.Paths")]
        public string Word_Paths { get; set; } = "Paths";

        /// <summary>
        /// Video
        /// </summary>
        [JsonPropertyName("Word.Video")]
        public string Word_Video { get; set; } = "Video";

        /// <summary>
        /// Video
        /// </summary>
        [JsonPropertyName("Word.Videos")]
        public string Word_Videos { get; set; } = "Videos";

        // ---
        public string Archive_Structure { get; set; } = "Archive structure";

        /// <summary>
        /// Remise à zéro des paramètres
        /// </summary>
        public string Reset_Factory { get; set; } = "Reset factory";

        /// <summary>
        /// Reset d'un dossier
        /// </summary>
        public string Reset_Folder { get; set; } = "Reset a folder";

        /// <summary>
        /// Injection des custom fields
        /// </summary>
        public string InjectCustomFields { get; set; } = "Inject Custom Fields";

        /// <summary>
        /// Placer les roms dans un dossier au nom du jeu
        /// </summary>
        public string PlaceRomsToGameName { get; set; } = "Place roms into a folder with the game name";

        /// <summary>
        /// Chercher le dossier launchbox
        /// </summary>
        public string ChoosePath { get; set; } = "Choose the path";

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

        internal static LangProvider Default()
        {
            return new LangProvider()
            {
                Version = Common.LangVersion,
                Folder_LaunchBox = "LaunchBox folder",
                Folder_Working = "Working folder",
                Folder_CheatCodes = "CheatCodes folder"
                // --- Configuration


            };
        }


        internal static LangProvider DefaultFrench()
        {
            return new LangProvider()
            {
                Version = Common.LangVersion,

                Folder_CheatCodes = "Dossier CheatCodes",
                Folder_LaunchBox = "Dossier LaunchBox",
                Folder_Working = "Dossier de travail",
                // 
                Word_Cancel = "Annuler",
                Word_General = "Général",
                Word_Languages = "Langues",
                Word_Main = "Principal",
                Word_Manuals = "Manuals",
                Word_Musics = "Musiques",
                Word_Paths = "Chemins",
                Word_Save = "Sauver",
                //
                Archive_Structure = "Structure de l'archive",
                Reset_Folder = "Reset un dossier",
                Reset_Factory = "Réglages d'usine",
                InjectCustomFields = "Injecter les champs personnalisés",
                PlaceRomsToGameName = "Placer les roms dans un dossier portant le nom du jeu",
                //
                ChoosePath = "Choisir le chemin",
                


            };
    }
}
}
