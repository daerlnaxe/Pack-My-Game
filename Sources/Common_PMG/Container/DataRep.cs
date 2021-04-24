using DxLocalTransf.Copy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;

namespace Common_PMG.Container
{
    /// <summary>
    /// File representation
    /// </summary>
    public class DataRep : DataTrans, IData, INotifyPropertyChanged
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

        public DataRep()
        {

        }

        public DataRep(string p)
        {
            SetBase(p);
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">Name</param>
        /// <param name="p">PathLink</param>
        public static DataRep MakeNameNPath(string n, string p)
        {
            return new DataRep()
            {
                Name = n,
                CurrentPath = p,
            };

        }


        public static DataRep DataRepFactory(DataRep elem)
        {
            return new DataRep(elem.CurrentPath)
            {
                DestPath = elem.DestPath,
                IsSelected = elem.IsSelected,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public static DataRep MakeChosen(string link)
        {
            return new DataRep()
            {
                Name = System.IO.Path.GetFileName(link),
                CurrentPath = link,
                IsSelected = true
            };
        }

        public static DataRep MakeNormal(string link)
        {
            return new DataRep()
            {
                Name = System.IO.Path.GetFileName(link),
                CurrentPath = link,
                IsSelected = false
            };
        }


        public static DataRep Copy(DataRep src)
        {
            return new DataRep()
            {
                Name = src.Name,
                CurrentPath = src.CurrentPath,
                DestPath = src.DestPath,
                IsSelected = src.IsSelected,
            };
        }

        /*
        public static T MakeNormal<T>(string link) where T : DataRep, new()
        {
            return new T()
            {
                Name = System.IO.Path.GetFileName(link),
                CurrentPath = link,
                IsSelected = false
            };
        }*/
    }
}
