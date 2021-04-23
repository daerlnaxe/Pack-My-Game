using Common_PMG.Container.Game.LaunchBox;
using Common_PMG.JSon;
using Common_PMG.XML;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Common_PMG.Container.Game
{
    /// <summary>
    /// Classe minimale contenant principalement les paths
    /// </summary>
    [JsonConverter(typeof(GamepathConverter))]
    public class GamePaths
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Platform { get; set; }

        // ---

        private List<DataPlus> _Applications = new List<DataPlus>();
        //   public string ApplicationPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ReadOnlyCollection<DataPlus> Applications
        {
            get => _Applications.AsReadOnly();
            set => SetApplications = value;
        }

        public IEnumerable<DataPlus> SetApplications
        {
            set
            {
                if (value == null)
                    return;

                foreach (var elem in value)
                    AddApplication(elem);
            }
        }
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

        public static GamePaths ReadFromJson(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            //return JsonSerializer.Deserialize<T>(jsonString);
            return JsonSerializer.Deserialize<GamePaths>(jsonString);

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

        public static GamePaths CreateBasic(XElement xelG)
        {
            GamePaths gp = new GamePaths();

            foreach (var element in xelG.Elements())
            {
                string name = element.Name.LocalName.ToLower();

                // id
                if (name == GameTag.ID.ToLower())
                    gp.Id = element.Value;
                // title
                else if (name == GameTag.Title.ToLower())
                    gp.Title = element.Value;
                // platform
                else if (name == GameTag.Platform.ToLower())
                    gp.Platform = element.Value;
                // application path
                else if (name == Tag.AppPath.ToLower())
                {
                    string appN = Path.GetFileName(element.Value);
                    gp.AddApplication(DataPlus.MakeChosen(gp.Id, appN, element.Value));
                }
                else if (name == GameTag.ManPath.ToLower())
                    gp.ManualPath = element.Value;
                else if (name == GameTag.MusPath.ToLower())
                    gp.MusicPath = element.Value;
                else if (name == GameTag.VidPath.ToLower())
                    gp.VideoPath = element.Value;
                else if (name == GameTag.ThVidPath.ToLower())
                    gp.ThemeVideoPath = element.Value;
            }
            return gp;
        }

        public void Complete(IEnumerable<XElement> clones)
        {
            foreach (var clone in clones)
            {
                DataPlus dp = new DataPlus();
                foreach (var element in clone.Elements())
                {
                    string name = element.Name.LocalName.ToLower();
                    if (name == CloneTag.Id.ToLower())
                        dp.Id = element.Value;
                    if (name == Tag.AppPath.ToLower())
                    {
                        dp.Name = Path.GetFileName(element.Value);
                        dp.CurrentPath = element.Value;
                    }
                }

                AddApplication(dp);
            }
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
            gp.AddApplication(DataPlus.MakeChosen(v.Id, v.Title, v.ApplicationPath));
            return gp;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <remarks>
        /// Refuse les path vides
        /// </remarks>
        public void AddApplication(string id, string name, string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            _Applications.Add(DataPlus.MakeNormal(id, name, path));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <remarks>
        /// Refuse les path vides
        /// </remarks>
        public void AddApplication(DataPlus data)
        {
            if (string.IsNullOrEmpty(data.CurrentPath))
                return;

            _Applications.Add(data);
        }

    }
}
