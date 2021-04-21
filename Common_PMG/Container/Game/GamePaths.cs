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
        [JsonIgnore]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Platform { get; set; }

        // ---

        //   public string ApplicationPath { get; set; }
        [JsonPropertyName("Games")]
        public List<DataPlus> Applications { get; set; }

        // ---

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
            #region
            /*
 

            using FileStream fs = File.Create(fileName);
            using var writer = new Utf8JsonWriter(fs, options: writerOptions);
            {                
                writer.WriteStartObject();
                writer.WriteString(nameof(Id), Id );
                writer.WriteString(nameof(Platform), Platform);
                writer.WriteStartArray("Games");
                foreach(var e in Applications)
                {
                    if (string.IsNullOrEmpty(e.Id))
                        continue;
                    writer.WriteStartObject();
                    writer.WriteString(nameof(e.Id), e.Id);
                    writer.WriteBoolean("IsDefault", e.IsSelected);
                    writer.WriteString("Path", e.Name);
                    writer.WriteEndObject();
                }
                writer.WriteEndArray();
                writer.WriteEndObject();

                writer.Flush();
            }*/
            #endregion
            string jsonString = JsonSerializer.Serialize(this, options).ToString();
            

            File.WriteAllText(fileName, jsonString);
        }

        public static T ReadFromJson<T>(string fileName) where T : GamePaths
        {
            string jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<T>(jsonString);
            /*

            byte[] data = File.ReadAllBytes(fileName);
            Utf8JsonReader reader = new Utf8JsonReader(data);

            T gp = new T();

            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonTokenType.StartObject:
                        Console.WriteLine("-------------");
                        break;
                    case JsonTokenType.EndObject:
                        break;
                    case JsonTokenType.StartArray:
                    case JsonTokenType.EndArray:
                        break;
                    case JsonTokenType.PropertyName:
                        Console.Write($"{reader.GetString()}: ");
                        break;
                    case JsonTokenType.String:
                        Console.WriteLine(reader.GetString());
                        break;
                    default:
                        throw new ArgumentException();

                }
            }
            return gp;*/
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
            GamePaths gp = new GamePaths()
            {
                Id = v.Id,
                Title = v.Title,
                Platform = v.Platform,
                ManualPath = v.ManualPath,
                MusicPath = v.MusicPath,
                VideoPath = v.VideoPath,
                ThemeVideoPath = v.ThemeVideoPath,

            };
            gp.Applications = new List<DataPlus>();
            gp.Applications.Add(DataPlus.MakeChosen(v.Id, v.Title, v.ApplicationPath));
            return gp;
        }


    }
}
