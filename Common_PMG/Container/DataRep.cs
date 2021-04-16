using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Common_PMG.Container
{
    /// <summary>
    /// File representation
    /// </summary>
    public class DataRep : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public string Name { get; set; }
        public string ALinkToThePath { get; set; }
        public string DestPath { get; set; }


        private bool _IsSelected;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">Name</param>
        /// <param name="p">PathLink</param>
        public DataRep(string n, string p)
        {
            Name = n;
            ALinkToThePath = p;
        }

        public DataRep(string p)
        {
            Name = System.IO.Path.GetFileName(p);
            ALinkToThePath = p;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public static DataRep MakeChosen(string link)
        {
            return new DataRep(System.IO.Path.GetFileName(link), link)
            {
                IsSelected = true
            };
        }

        public static DataRep MakeNormal(string link)
        {
            return new DataRep(System.IO.Path.GetFileName(link), link)
            {
                IsSelected = false
            };
        }

    }
}
