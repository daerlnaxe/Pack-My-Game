using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Unbroken.LaunchBox.Plugins.Data;

namespace Common_PMG.Container.AAPP
{
    //[XmlRoot("AdditionalApplication")]
    public class AdditionalApplication: Clone//, IAdditionalApplication
    {
        #region info jeu

        [XmlElement(Order = 2)]
        public string Name { get; set; }                // Jouer la version(1991-04)(Sega)[Sonic Mega Collection]...

        [XmlElement(Order = 3)]
        public string Developer { get; set; }                   //Malibu Interactive

        // Editeur        
        [XmlElement(Order = 4)]
        public string Publisher { get; set; }                   //Sony Imagesoft
        [XmlElement(Order = 6)]
        public string Region { get; set; }                      //United States

        [XmlElement(Order = 7)]
        public DateTime? ReleaseDate { get; set; }


        #endregion

        #region complément jeu (fini, status...)
        [XmlElement(Order = 30)]
        public string Status { get; set; }

        [XmlElement(Order = 37)]
        public string Version { get; set; }                     //(1994)(Sony Imagesoft) (US)
        #endregion



        #region params de lancement
        [XmlElement(Order = 100)]
        public bool UseDosBox { get; set; }                     //false

        [XmlElement(Order = 101)]
        public string CommandLine { get; set; }

        [XmlElement(Order = 102)]
        public bool UseEmulator { get; set; }           //true

        [XmlElement(Order = 103)]
        public string EmulatorId { get; set; }          //46980dd1-982d-4b30-ad3b-fda6139752c9

        [XmlElement(Order = 104)]
        public bool AutoRunAfter { get; set; }          //false

        [XmlElement(Order = 105)]
        public bool AutoRunBefore { get; set; }         //false

        [XmlElement(Order = 106)]
        public bool WaitForExit { get; set; }           //false
        #endregion

        #region Stats
        [XmlElement(Order = 200)]
        public int PlayCount { get; set; }                      //2

        [XmlElement(Order = 201)]
        public DateTime? LastPlayed { get; set; }          //2018-09-05T08:07:29.1600786+02:00

        #endregion

        [XmlElement(Order = 300)]
        public bool SideA { get; set; }                 //false

        [XmlElement(Order = 301)]
        public bool SideB { get; set; }                 //false

        [XmlElement(Order = 302)]
        public int Priority { get; set; }               //1

        [XmlElement(Order =303)]
        public int? Disc { get; set; }

        [XmlElement(Order =304)]
        public bool? Installed { get; set; }


    }
}
