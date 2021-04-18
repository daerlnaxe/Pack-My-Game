using Common_PMG.Container.Game.LaunchBox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common_PMG.Container.Game
{
    /// <summary>
    /// Classe minimale contenant principalement les paths
    /// </summary>
    public class GamePaths
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Platform { get; set; }

        // ---

        public string ApplicationPath { get; set; }
        public string ManualPath { get; set; }
        public string MusicPath { get; set; }
        public string VideoPath { get; set; }
        public string ThemeVideoPath { get; set; }

        public void WriteToJson(string fileName)
        {
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };

            string jsonString = JsonSerializer.Serialize(this, options).ToString();
            File.WriteAllText(fileName, jsonString);
        }

        public static T ReadFromJson<T>(string fileName)where T: GamePaths
        {            
            string jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<T>(jsonString);
        }

        /// <summary>
        /// N'utilise pas les chemins
        /// </summary>
        /// <param name="lbGame"></param>
        /// <returns></returns>
        public static GamePaths CreateBasic(LBGame lbGame)
        {
            return new GamePaths()
            {
                Id = lbGame.Id,
                Title = lbGame.Title,
                Platform = lbGame.Platform,
            };
        }
        
        public static explicit operator GamePaths(ShortGame v)
        {
            return new GamePaths
            {
                Id = v.Id,
                Title = v.Title,
                Platform = v.Platform,
            };
        }

        public static explicit operator GamePaths(LBGame v)
        {
            return new GamePaths()
            {
                Id = v.Id,
                Title = v.Title,
                Platform = v.Platform,
                ApplicationPath = v.ApplicationPath,
                ManualPath = v.ManualPath,
                MusicPath = v.MusicPath,
                VideoPath = v.VideoPath,
                ThemeVideoPath = v.ThemeVideoPath,
            };
        }


    }
}
