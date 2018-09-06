using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Pack_My_Game.Container
{
    public class ShortGame
    {

        [XmlElement(Order = 1)]
        public string Title { get; set; }

        [XmlElement(Order =2)]
        public string ID { get; set; }


        [XmlElement(Order = 3)]
        public string Region { get; set; }

        [XmlIgnore]
        public string FileName { get; set; }

        [XmlIgnore]
        public string ExploitableFileName { get; set;}

    }
}
