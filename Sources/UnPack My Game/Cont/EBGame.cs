using System;
using System.Collections.Generic;
using System.Text;

namespace UnPack_My_Game.Cont
{
    /// <summary>
    /// Objet représentant une backup amélioréer de jeu
    /// </summary>
    [Obsolete]
    class EBGame
    {
        public string Main { get; set; }

        public List<string> AdditionnalsApp { get; set; } = new List<string>();

        public List<string> CustomFields { get; set; } = new List<string>();
    }
}
