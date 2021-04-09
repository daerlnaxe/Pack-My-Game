using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common_PMG.Container.Game
{
    /// <summary>
    /// Version de base des jeux
    /// </summary>
    public class ShortGame : BasicGame, I_BasicGame, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // --- A ignorer

        private bool _IsSelected;
        [XmlIgnore]
        public bool IsSelected
        {
            get => _IsSelected;
            set
            {
                _IsSelected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
            }
        }

        [XmlIgnore]
        public string FileName { get; set; }

        [XmlIgnore]
        public string ExploitableFileName { get; set; }

    }
}
