using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_PMG.Container.Game
{
    public class DataRepImg : DataRep
    {

        public string Categorie { get; set; }


        //      public string LinkToThePath { get; set; }       // yes it's a joke.

        public DataRepImg()
        { }

        public DataRepImg(string c, string l2tp) : base(l2tp)
        {
            Categorie = c;
            //   LinkToThePath = l2tp; 
        }

        public DataRepImg(DataRepImg elem)
        {
            Name = elem.Name;
            Categorie = elem.Categorie;
            CurrentPath = elem.CurrentPath;
            DestPath = elem.DestPath;
            IsSelected = elem.IsSelected;
        }
    }
}
