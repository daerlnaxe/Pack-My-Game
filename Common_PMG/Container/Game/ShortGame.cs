using Common_PMG.Container.AAPP;
using Common_PMG.Container.Game.LaunchBox;
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

        /*
        /// <summary>
        /// Chemin de l'application
        /// </summary>
        /// <remarks>
        /// ex: '..\..\Games\Roms\Sega Mega Drive\Pending\3 Ninjas Kick Back(1994)(Sony Imagesoft) (US).zip'
        /// </remarks>
        [XmlIgnore]
        public string ApplicationPath {get; set;}

        /// <summary>
        /// Manuel
        /// </summary>
        [XmlIgnore]
        public string ManualPath { get; set; }

        [XmlIgnore]
        public string MusicPath { get; set; }

        [XmlIgnore]
        public string VideoPath { get; set; }

        [XmlIgnore]
        public string ThemeVideoPath { get; set; }

        [XmlIgnore]
        public List<Clone> AdditionalApplications { get; set; } = new List<Clone>();
        */


    }
}
