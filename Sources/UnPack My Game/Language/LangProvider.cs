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

        public string Name { get; set; }

        #region Commun
        /// <summary>
        /// Chemin vers LaunchBox
        /// </summary>
        public string Folder_LaunchBox { get; set; }

        /// <summary>
        /// Dossier de travail
        /// </summary>
        public string Folder_Working { get; set; }
        #endregion


        /// <summary>
        /// Mot Informations
        /// </summary>
        [JsonPropertyName("Word.Informations")]
        public string Word_Informations { get; set; } = "Informations";


        #region Configuration
        /// <summary>
        /// Onglet général
        /// </summary>
        [JsonPropertyName("Word.General")]
        public string Word_General { get; set; }

        /// <summary>
        /// Langue
        /// </summary>
        public string Language { get; set; }
        #endregion

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
                // --- Configuration
                Word_General = "General",
                Language = "Language",
                
            };
        }


        internal static LangProvider DefaultFrench()
        {
            return new LangProvider()
            {
                Version = Common.LangVersion,
                Folder_LaunchBox = "Dossier LaunchBox",
                Folder_Working = "Dossier de travail",
                Word_General = "Général",
                // --- Configuration
                Language = "Langue",
            };
        }
    }
}
