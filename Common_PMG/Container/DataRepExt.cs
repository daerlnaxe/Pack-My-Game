using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_PMG.Container.Game
{
    public class DataRepExt: DataRep
    {
        public string Categorie { get; set; }
        
  //      public string LinkToThePath { get; set; }       // yes it's a joke.


        public DataRepExt(string c, string l2tp) : base(l2tp)
        { 
            Categorie = c;
         //   LinkToThePath = l2tp; 
        }
    }
}
