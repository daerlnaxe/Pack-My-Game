using System;
using System.Collections.Generic;
using System.Text;

namespace UnPack_My_Game.Models.LaunchBox
{
    enum E_Method    
    {
        None, 
        EBMethod, // Basé sur les chemins donnés dans EBGame.xml
        LBMethod, // Basé sur les chemins donnés par la plateforme dans Launchbox
        TBMethod, // Basé sur les chemins donnés par un fichier plateforme 
    }
}
