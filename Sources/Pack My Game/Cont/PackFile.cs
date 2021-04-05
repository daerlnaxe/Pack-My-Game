using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pack_My_Game.Cont
{
    class PackFile
    {
        public string Categorie { get; set; }
        public string LinkToThePath { get; set; }       // yes it's a joke.

        public PackFile(string c, string l2tp) { Categorie = c; LinkToThePath = l2tp; }
    }
}
