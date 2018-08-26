using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pack_My_Game.Container
{
    class Clone
    {
        /// <summary>
        /// ID du parent ?
        /// </summary>
        public string GameID { get; set; }
        //[XmlIgnore]
        public string ApplicationPath { get; set; }

        /// <summary>
        /// ID du clone
        /// </summary>
        public string Id { get; set; }
    }
}
