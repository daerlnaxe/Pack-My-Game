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
                Reset_Folder = "Reset un dossier",
                Reset_Factory = "Réglages d'usine",
                InjectCustomFields = "Injecter les champs personnalisés",
                PlaceRomsToGameName = "Placer les roms dans un dossier portant le nom du jeu",

            };
            lang.SetFrenchDefault(Common.LangVersion);
            return lang;
        }
    }
}
