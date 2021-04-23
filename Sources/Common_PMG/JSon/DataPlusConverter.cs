using Common_PMG.Container;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common_PMG.JSon
{
    class DataPlusConverter : JsonConverter<DataPlus>
    {
        public override DataPlus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DataPlus dp = dp = new DataPlus(); ;
            while (reader.Read())
            {                   

                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    if (string.IsNullOrEmpty(dp.Name))
                        dp.Name = dp.CurrentPath;

                    return dp;
                }

                // ---

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();

                    while (reader.TokenType == JsonTokenType.PropertyName || reader.TokenType == JsonTokenType.Comment )
                        reader.Read();

                    switch (propertyName)
                    {
                        case "Id":
                            dp.Id = reader.GetString();
                            break;
                        case "IsDefault":
                            dp.IsSelected = reader.GetBoolean();
                            break;
                        case "Path":
                            dp.CurrentPath = reader.GetString();
                            break;
                        default:
                            throw new ArgumentException();

                    }
                }
            }
            return null;
        }

        public override void Write(Utf8JsonWriter writer, DataPlus value, JsonSerializerOptions options)
        {
            if (string.IsNullOrEmpty(value.Id))
                return;

            writer.WriteStartObject();

            writer.WriteString(nameof(value.Id), value.Id);
            writer.WriteBoolean("IsDefault", value.IsSelected);
            writer.WriteString("Path", value.Name);
            writer.WriteEndObject();
        }
    }
}
