﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Pack_My_Game.BackupLB
{
    public class AdditionalApplication
    {
        #region info jeu

        [XmlElement(Order = 0)]
        public string Id { get; set; }                  //22dfd169-f99d-4929-acc1-039e6df37129

        [XmlElement(Order = 1)]
        public string GameID { get; set; }              //d99692c7-51c3-4e50-8592-7fab98295f5b

        [XmlElement(Order = 2)]
        public string Name { get; set; }                // Jouer la version(1991-04)(Sega)[Sonic Mega Collection]...

        [XmlElement(Order = 3)]
        public string Developer { get; set; }                   //Malibu Interactive

        // Editeur        
        [XmlElement(Order = 4)]
        public string Publisher { get; set; }                   //Sony Imagesoft
        [XmlElement(Order = 6)]
        public string Region { get; set; }                      //United States
        #endregion

        #region complément jeu (fini, status...)
        [XmlElement(Order = 30)]
        public string Status { get; set; }

        [XmlElement(Order = 37)]
        public string Version { get; set; }                     //(1994)(Sony Imagesoft) (US)
        #endregion


        #region chemins
        [XmlElement(Order = 50)]
        public string ApplicationPath { get; set; }             //..\..\Games\Roms\Sega Mega Drive\Pending\3 Ninjas Kick Back(1994)(Sony Imagesoft) (US).zip
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
        public string LastPlayed { get; set; }          //2018-09-05T08:07:29.1600786+02:00

        #endregion

        [XmlElement(Order = 300)]
        public bool SideA { get; set; }                 //false

        [XmlElement(Order = 301)]
        public bool SideB { get; set; }                 //false

        [XmlElement(Order = 302)]
        public int Priority { get; set; }               //1
    }
}