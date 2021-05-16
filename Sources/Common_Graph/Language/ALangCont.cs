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
        /// Error LaunchBox folder doesn't exist
        /// </summary>
        [JsonPropertyName("Err.LaunchBoxF")]
        public string Err_LaunchBoxF { get; set; } = "LaunchBox folder doesn't exist.";

        // ---

        /// <summary>
        /// File exists
        /// </summary>
        public string File_Ex { get; set; } = "File exists";

        /// <summary>
        /// Files found
        /// </summary>
        public string Files_Found { get; set; } = "Files found";

        // ---

        /// <summary>
        /// Folder exists
        /// </summary>
        public string Folder_Ex { get; internal set; } = "Folder exists";

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

        // ---

        /// <summary>
        /// Default video
        /// </summary>
        public string S_DefaultVideo { get; set; } = "Default video";

        /// <summary>
        /// Default theme video
        /// </summary>
        public string S_DefaultThemeVideo { get; set; } = "Default theme video";

        /// <summary>
        /// Delete by yourself
        /// </summary>
        public string S_DeleteByYs { get; set; } = "Delete By Yourself";
        /// <summary>
        /// Open Folder
        /// </summary>
        public string S_OpenFolder { get; set; } = "Open folder";
        // ---

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
        /// Backup
        /// </summary>
        [JsonPropertyName("Word.Backup")]
        public string Word_Backup { get; set; } = "Backup";

        /// <summary>
        /// Cancel
        /// </summary>
        [JsonPropertyName("Word.Cancel")]
        public string Word_Cancel { get; set; } = "Cancel";

        /// <summary>
        /// Cheat Codes
        /// </summary>
        [JsonPropertyName("Word.CheatCodes")]
        public string Word_CheatCodes { get; set; } = "Cheat Codes";

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
        /// Default
        /// </summary>
        [JsonPropertyName("Word.Default")]
        public string Word_Default { get; set; } = "Default";
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
        /// Information
        /// </summary>
        [JsonPropertyName("Word.Information")]
        public string Word_Information { get; set; } = "Information";

        /// <summary>
        /// Informations
        /// </summary>
        [JsonPropertyName("Word.Informations")]
        public string Word_Informations { get; set; } = "Informations";

        /// <summary>
        /// Loggers
        /// </summary>
        [JsonPropertyName("Word.Loggers")]
        public string Word_Loggers { get; set; } = "Loggers";

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
        /// Message
        /// </summary>
        [JsonPropertyName("Word.Message")]
        public string Word_Message { get; set; } = "Message";

        /// <summary>
        /// Musics
        /// </summary>
        [JsonPropertyName("Word.Musics")]
        public string Word_Musics { get; set; } = "Musics";

        /// <summary>
        /// New
        /// </summary>
        [JsonPropertyName("Word.New")]
        public string Word_New { get; set; } = "New";

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
        /// Open
        /// </summary>
        [JsonPropertyName("Word.Open")]
        public string Word_Open { get; set; } = "Open";

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
        /// Platform
        /// </summary>
        [JsonPropertyName("Word.Platform")]
        public string Word_Platform { get; set; } = "Platform";

        /// <summary>
        /// Procedure
        /// </summary>
        [JsonPropertyName("Word.Procedure")]
        public string Word_Procedure { get; set; } = "Procedure";

        /// <summary>
        /// Process
        /// </summary>
        [JsonPropertyName("Word.Process")]
        public string Word_Process { get; set; } = "Process";

        /// <summary>
        /// Reload
        /// </summary>
        [JsonPropertyName("Word.Reload")]
        public string Word_Reload { get; set; } = "Reload";

        /// <summary>
        /// Remove
        /// </summary>
        [JsonPropertyName("Word.Remove")]
        public string Word_Remove { get; set; } = "Remove";

        /// <summary>
        /// Reset
        /// </summary>
        [JsonPropertyName("Word.Reset")]
        public string Word_Reset { get; set; } = "Reset";

        /// <summary>
        /// Result
        /// </summary>
        [JsonPropertyName("Word.Result")]
        public string Word_Result { get; set; } = "Result";

        /// <summary>
        /// Sauvegarder
        /// </summary>
        [JsonPropertyName("Word.Save")]
        public string Word_Save { get; set; } = "Save";

        /// <summary>
        /// Search
        /// </summary>
        [JsonPropertyName("Word.Search")]
        public string Word_Search { get; set; } = "Search";

        /// <summary>
        /// Select
        /// </summary>
        [JsonPropertyName("Word.Select")]
        public string Word_Select { get; set; } = "Select";

        /// <summary>
        /// Selected
        /// </summary>
        [JsonPropertyName("Word.Selected")]
        public string Word_Selected { get; set; } = "Selected";

        /// <summary>
        /// Structure
        /// </summary>
        [JsonPropertyName("Word.Structure")]
        public string Word_Structure { get; set; } = "Structure";

        /// <summary>
        /// Submit
        /// </summary>
        [JsonPropertyName("Word.Submit")]
        public string Word_Submit { get; set; } = "Submit";

        /// <summary>
        /// Suggested
        /// </summary>
        [JsonPropertyName("Word.Suggested")]
        public string Word_Suggested { get; set; } = "Suggested";

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
            Err_LaunchBoxF = "Le dossier LaunchBox n'existe pas.";
            //
            Files_Found = "Fichiers trouvés";
            File_Ex = "Le fichier existe";
            //
            Folder_Ex = "Le dossier existe";
            //
            Path_CheatCodes = "Chemin cheat codes";
            Path_LaunchBox = "Chemin LaunchBox";
            Path_Working = "Dossier de travail";
            //
            S_DefaultThemeVideo = "Thème video par défaut";
            S_DefaultVideo = "Video par défaut";
            S_DeleteByYs = "Effacez par vous même";
            S_OpenFolder = "Ouvrir le dossier";
            //
            Select_All = "Tout sélectionner";
            Select_None = "Sélectionner aucun";
            //
            Word_Add = "Ajouter";
            Word_All = "Tous";
            Word_Cancel = "Annuler";
            Word_Default = "Défaut";
            Word_Delete = "Effacer";
            Word_File = "Fichier";
            Word_General = "Général";
            Word_Loggers = "Traçage";
            Word_Main = "Principal,";
            Word_Manuals = "Manuels";
            Word_Musics = "Musics";
            Word_New = "Nouveau";
            Word_None = "Aucun";
            Word_Open = "Ouvrir";
            Word_Paths = "Chemins";
            Word_Platform = "Plateforme";
            Word_Procedure = "Procédure";
            Word_Process = "Procéder";
            Word_Reload = "Recharger";
            Word_Remove = "Enlever";
            Word_Reset = "Réinitialiser";
            Word_Result = "Résultat";
            Word_Save = "Sauver";
            Word_Search = "Cherche";
            Word_Select = "Sélectionnez";
            Word_Selected = "Sélectionné";
            Word_Submit = "Soumettre";
            Word_Suggested = "Suggéré";
            Word_Videos = "Videos";
            //

        }
    }
}
