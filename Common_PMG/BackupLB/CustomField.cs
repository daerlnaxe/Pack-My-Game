using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Common_PMG.BackupLB
{
    public class CustomField
    {
        [XmlElement(Order =1)]
        public string GameID;

        [XmlElement(Order = 2)]
        public string Name;

        [XmlElement(Order = 3)]
        public string Value;

    }
}
