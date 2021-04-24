using Common_PMG.Container;
using Common_PMG.Container.Game;
using Hermes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common_PMG.JSon
{
    class GamepathConverter : JsonConverter<GamePaths>
    {
        public override GamePaths Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            GamePaths gpX = new GamePaths();
            while (reader.Read())
            {

                if (reader.TokenType == JsonTokenType.EndObject)
                {                    
                    return gpX;
                }

                // ---

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();

                    while (reader.TokenType == JsonTokenType.PropertyName || reader.TokenType == JsonTokenType.Comment)
                        reader.Read();

                    switch (propertyName)
                    {
                        case "Id":
                            gpX.Id = reader.GetString();
                            break;
                        case "Title":
                            gpX.Title = reader.GetString();
                            break;
                        case "Platform":
                            gpX.Platform = reader.GetString();
                            break;
                            // --- 
                        case "Games":

                            while (reader.Read())
                            {
                                if (reader.TokenType == JsonTokenType.EndArray)
                                    break;

                                if (reader.TokenType == JsonTokenType.StartObject)
                                    gpX.AddApplication(JsonSerializer.Deserialize<DataPlus>(ref reader, null));
                            }
                            //gpX.ManualPath = reader.GetString();
                            break;
                            // ---
                        case "ManualPath":
                            gpX.ManualPath = reader.GetString();
                            break;
                        case "MusicPath":
                            gpX.MusicPath = reader.GetString();
                            break;
                        case "VideoPath":
                            gpX.VideoPath = reader.GetString();
                            break;
                        case "ThemeVideoPath":
                            gpX.ThemeVideoPath = reader.GetString();
                            break;


                        default:
                            HeTrace.WriteLine($"Not managed: {propertyName}");
                            break;

                    }
                }
            }
            return null;
        }

        public override void Write(Utf8JsonWriter writer, GamePaths value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString(nameof(value.Id), value.Id);
            writer.WriteString(nameof(value.Title), value.Title);
            writer.WriteString(nameof(value.Platform), value.Platform);
            writer.WriteString(nameof(value.ManualPath), value.ManualPath);
            writer.WriteString(nameof(value.MusicPath), value.MusicPath);
            writer.WriteString(nameof(value.VideoPath), value.VideoPath);
            writer.WriteString(nameof(value.ThemeVideoPath), value.ThemeVideoPath);
            //
            writer.WriteStartArray("Games");
            foreach (var data in value.Applications)
            {
                   JsonSerializer.Serialize(writer, data);
            }
            writer.WriteEndArray();
            //
            writer.WriteEndObject();

            
        }
    }
}
