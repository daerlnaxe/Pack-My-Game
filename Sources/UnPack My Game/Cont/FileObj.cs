using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace UnPack_My_Game.Cont
{
    /// <summary>
    /// Used to list, to have Name of the game and path
    /// </summary>
    [Obsolete]
    public class FileObj: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        

        public string Nom { get; set; }
        public string Path { get; set; }

        private bool _Selected;
        public bool Selected 
        {
            get => _Selected;
            set 
            {
                if (_Selected == value)
                    return;

                _Selected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Selected)));
            }
        }

        public FileObj(string p)
        {
            Nom = System.IO.Path.GetFileName(p);
            Path = p;
        }

    }
}
