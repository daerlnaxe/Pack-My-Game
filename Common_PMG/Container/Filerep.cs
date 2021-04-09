using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common_PMG.Container
{
    /// <summary>
    /// File representation
    /// </summary>
    public class Filerep
    {
        public string Name { get; set; }
        public string PathLink { get; set; }

        public bool IsSelected { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="n">Name</param>
        /// <param name="p">PathLink</param>
        public Filerep(string n, string p)
        {
            Name = n;
            PathLink = p;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public static Filerep MakeChosen(string link)
        {
            return new Filerep(Path.GetFileName(link), link)
            {
                IsSelected = true
            };
        }
        public static Filerep MakeNormal(string link)
        {
            return new Filerep(Path.GetFileName(link), link)
            {
                IsSelected = false
            };
        }

    }
}
