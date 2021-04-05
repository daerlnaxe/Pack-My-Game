using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pack_My_Game.Cont
{
    class Folder
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public Dictionary<string, Folder> Children { get; set; } = new Dictionary<string, Folder>();

        public Folder(string n, string p) { Name = n; Path = p; }
    }
}
