using System;
using System.Collections.Generic;
using System.Text;

namespace LaunchBox_XML.Container
{
    /// <summary>
    /// File representation
    /// </summary>
    public class Filerep
    {
        public string Name { get; set; }
        public string PathLink { get; set; }

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
    }
}
