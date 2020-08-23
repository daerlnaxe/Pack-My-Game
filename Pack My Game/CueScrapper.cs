using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pack_My_Game
{
    internal class Cue_Scrapper
    {
        /// <summary>
        /// 
        /// </summary>
        internal HashSet<string> Files = new HashSet<string>();

        public Cue_Scrapper(string file)
        {
            if (!File.Exists(file))
                return;

            foreach( string line in File.ReadAllLines(file))
            {
                if (line.Contains("\""))
                    Files.Add(GetFilename(line));
            }
        }

        private string GetFilename(string line)
        {
            string tmp = null;
            tmp = line.Substring(line.IndexOf("\"")+1);
            tmp = tmp.Substring(0, tmp.LastIndexOf("\""));


            return tmp;
        }
    }
}
