using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pack_My_Game.Pack
{
    static class StringOp
    {
        /// <summary>
        /// Change strange symbols
        /// </summary>
        /// <param name="chaine"></param>
        public static string Convert(string chaine)
        {
            chaine = chaine.Replace(";amp", "&");

            return chaine;
        }
    }
}
