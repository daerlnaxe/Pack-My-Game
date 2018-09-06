using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pack_My_Game.BackupLB
{
    class AdditionalApplication: Common
    {
        string Id{get; set;}                  //22dfd169-f99d-4929-acc1-039e6df37129

        string GameID{get; set;}              //d99692c7-51c3-4e50-8592-7fab98295f5b
        bool AutoRunAfter{get; set;}          //false
        bool AutoRunBefore{get; set;}         //false
        string Name{get; set;}                // Jouer la version(1991-04)(Sega)[Sonic Mega Collection]...
        bool UseEmulator{get; set;}           //true
        bool WaitForExit{get; set;}           //false


        string LastPlayed{get; set;}          //2018-09-05T08:07:29.1600786+02:00
        string EmulatorId{get; set;}          //46980dd1-982d-4b30-ad3b-fda6139752c9
        bool SideA{get; set;}                 //false
        bool SideB{get; set;}                 //false
        int Priority{get; set;}               //1
    }
}
