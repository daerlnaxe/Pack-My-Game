using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common_PMG.Container
{
    public class DataPlus : DataRep
    {
        public string Id { get; set; }

        [JsonPropertyName("Path")]
        public override string CurrentPath { get; set; }

        [JsonIgnore]
        public override string DestPath {get;set;}


        public DataPlus()
        {

        }


        public DataPlus(DataPlus elem)
        {
            this.Id = elem.Id;
            this.Name = elem.Name;
            this.CurrentPath = elem.CurrentPath;
            this.DestPath = elem.DestPath;
            this.IsSelected = elem.IsSelected;
        }

        public static DataPlus MakeChosen(string id, string n, string p)
        {
            return new DataPlus()
            {
                Id = id,
                Name = n,
                CurrentPath = p,
                IsSelected = true
            };
        }

        public static DataPlus MakeNormal(string id, string n, string p)
        {
            return new DataPlus()
            {
                Id = id,
                Name = n,
                CurrentPath = p,
                IsSelected = false
            };
        }


    }
}
