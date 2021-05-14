using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Common_Graph.Language
{
    public abstract class ALangCont
    {
        public string Lang { get; set; }
        public string Version { get; set; }
        public string Name { get; set; }

        // ---

        /// <summary>
        /// Choose the LaunchBoxPath
        /// </summary>
        public string Choose_LBPath { get; set; } = "Choose the LaunchBox path";

        /// <summary>
        /// Choose a language
        /// </summary>
        public string Choose_Language { get; set; } = "Choose a Language";

        /// <summary>
        /// Choose the path
        /// </summary>
        public string Choose_Path { get; set; } = "Choose the path";

        // ---

        /// <summary>
        /// Cheats codes path
        /// </summary>
        [JsonPropertyName("Path.CheatCodes")]
        public string Path_CheatCodes { get; set; } = "Cheats codes path";

        /// <summary>
        /// Launchbox path
        /// </summary>
        [JsonPropertyName("Path.LaunchBox")]
        public string Path_LaunchBox { get; set; } = "LaunchBox path";

        /// <summary>
        /// Working path
        /// </summary>
        [JsonPropertyName("Path.Working")]
        public string Path_Working { get; set; } = "Working path";

        /// <summary>
        /// Select all
        /// </summary>
        [JsonPropertyName("Select.All")]
        public string Select_All { get; set; } = "Select all";

        /// <summary>
        /// Select none
        /// </summary>
        [JsonPropertyName("Select.None")]
        public string Select_None { get; set; } = "Select none";

        #region words
        /// <summary>
        /// Add
        /// </summary>
        [JsonPropertyName("Word.Add")]
        public string Word_Add { get; set; } = "Add";

        /// <summary>
        /// All
        /// </summary>
        [JsonPropertyName("Word.All")]
        public string Word_All { get; set; } = "All";

        /// <summary>
        /// Cancel
        /// </summary>
        [JsonPropertyName("Word.Cancel")]
        public string Word_Cancel { get; set; } = "Cancel";

        /// <summary>
        /// Configuration
        /// </summary>
        [JsonPropertyName("Word.Configuration")]
        public string Word_Configuration { get; set; } = "Configuration";

        /// <summary>
        /// Delete
        /// </summary>
        [JsonPropertyName("Word.Delete")]
        public string Word_Delete { get; set; } = "Delete";

        /// <summary>
        /// File
        /// </summary>
        [JsonPropertyName("Word.File")]
        public string Word_File { get; set; } = "File";

        /// <summary>
        /// Games
        /// </summary>
        [JsonPropertyName("Word.Games")]
        public string Word_Games { get; set; } = "Games";

        /// <summary>
        /// General
        /// </summary>
        [JsonPropertyName("Word.General")]
        public string Word_General { get; set; } = "General";

        /// <summary>
        /// Images
        /// </summary>
        [JsonPropertyName("Word.Images")]
        public string Word_Images { get; set; } = "Images";

        /// <summary>
        /// Informations
        /// </summary>
        [JsonPropertyName("Word.Informations")]
        public string Word_Informations { get; set; } = "Informations";

        /// <summary>
        /// Main
        /// </summary>
        [JsonPropertyName("Word.Main")]
        public string Word_Main { get; set; } = "Main";

        /// <summary>
        /// Manuels
        /// </summary>
        [JsonPropertyName("Word.Manuals")]
        public string Word_Manuals { get; set; } = "Manuals";

        /// <summary>
        /// Musics
        /// </summary>
        [JsonPropertyName("Word.Musics")]
        public string Word_Musics { get; set; } = "Musics";

        /// <summary>
        /// None
        /// </summary>
        [JsonPropertyName("Word.None")]
        public string Word_None { get; set; } = "None";

        /// <summary>
        /// Ok
        /// </summary>
        [JsonPropertyName("Word.Ok")]
        public string Word_Ok { get; set; } = "OK";

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
        /// Process
        /// </summary>
        [JsonPropertyName("Word.Process")]
        public string Word_Process { get; set; } = "Process";

        /// <summary>
        /// Select
        /// </summary>
        [JsonPropertyName("Word.Select")]
        public string Word_Select { get; set; } = "Select";

        /// <summary>
        /// Submit
        /// </summary>
        [JsonPropertyName("Word.Submit")]
        public string Word_Submit { get; set; } = "Submit";

        /// <summary>
        /// Videos
        /// </summary>
        [JsonPropertyName("Word.Videos")]
        public string Word_Videos { get; set; } = "Videos";
        #endregion



        protected void SetDefault(string version)
        {
            Version = version;
            Lang = "en-US";
            Name = "English";
        }

        protected void SetFrenchDefault(string langVersion)
        {
            Version = langVersion;
            Lang = "fr-FR";
            Name = "Français";
            //

            Choose_LBPath = "Choisissez le chemin de LaunchBox";
            Choose_Language = "Choisissez une langue";
            Choose_Path = "Choisissez le chemin";
            //
            Path_CheatCodes = "Chemin cheat codes";
            Path_LaunchBox = "Chemin LaunchBox";
            Path_Working = "Dossier de travail";
            //
            Select_All = "Tout sélectionner";
            Select_None = "Sélectionner aucun";
            //
            Word_Add = "Ajouter";
            Word_All = "Tous";
            Word_Cancel = "Annuler";
            Word_Delete = "Effacer";
            Word_File = "Fichier";
            Word_General = "Général";
            Word_Main = "Principal,";
            Word_Manuals = "Manuels";
            Word_Musics = "Musics";
            Word_None = "Aucun";
            Word_Paths = "Chemins";
            Word_Process = "Procéder";
            Word_Select = "Sélectionnez";
            Word_Submit = "Soumettre";
            Word_Videos = "Videos";
            //

        }
    }
}
