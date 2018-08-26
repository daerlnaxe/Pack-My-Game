using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pack_My_Game.Container
{
    class Platform
    {
        public string Name { get; set;  }
        public List<PlatformFolder> PlatformFolders { get; set; } = new List<PlatformFolder>();


    }
}
