using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pack_My_Game.BackupLB
{
    class Common
    {
        public string ApplicationPath { get; set; }             //..\..\Games\Roms\Sega Mega Drive\Pending\3 Ninjas Kick Back(1994)(Sony Imagesoft) (US).zip
        public bool UseDosBox { get; set; }                     //false
        public string ReleaseDate { get; set; }                 //1994-06-01T09:00:00+02:00
        public string Developer { get; set; }                   //Malibu Interactive
        // Editeur
        public string Publisher { get; set; }                   //Sony Imagesoft

        public string Version { get; set; }                     //(1994)(Sony Imagesoft) (US)
        public string Region { get; set; }                      //United States
        public int PlayCount { get; set; }                      //2
        public string CommandLine { get; set; }
        public string Status { get; set; }                      //
    }
}
