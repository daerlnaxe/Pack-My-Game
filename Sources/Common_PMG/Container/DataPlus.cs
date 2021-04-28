using Common_PMG.JSon;
using DxLocalTransf.Copy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common_PMG.Container
{
    [JsonConverter(typeof(DataPlusConverter))]
    public class DataPlus : DataTrans, IDataRep, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private bool _IsSelected;

        [JsonPropertyName("Default")]
        public bool IsSelected
        {
            get => _IsSelected;
            set
            {
                if (_IsSelected == value)
                    return;

                _IsSelected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
            }
        }
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
        public static DataPlus MakeNormal(string p)
        {
            return new DataPlus()
            {
                Name = System.IO.Path.GetFileName(p),
                CurrentPath = p,
                IsSelected = false
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

        public static DataPlus Copy(DataPlus src)
        {
            return new DataPlus()
            {
                Id= src.Id,
                Name =src.Name,
                CurrentPath = src.CurrentPath,
                DestPath = src.DestPath,
                IsSelected = src.IsSelected,
            };
        }
    }
}
