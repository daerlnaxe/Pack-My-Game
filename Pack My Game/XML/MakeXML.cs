using DlnxLocalTransfert;
using DxTrace;
using Pack_My_Game.Container;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Pack_My_Game.XML
{
    static class MakeXML
    {

        /// <summary>
        /// Création d'un fichier infos.xml
        /// </summary>
        /// <returns></returns>
        // todo a question box avec une loupe pour lancer les fichiers à comparer  + ecraser envoyer poubelle, non
        public static bool InfoGame(string path, Game zeGame )
        {
            ITrace.WriteLine(prefix: false);
            ITrace.WriteLine($"[MakeInfo] Creation of file 'Infos.xml'");

            string xmlDest = Path.Combine(path, "Infos.xml");

            var infoRes = OPFiles.SingleVerif(xmlDest, "MakeInfo", log: (string message) => ITrace.WriteLine(message, true));
            switch (infoRes)
            {
                case OPResult.Ok:
                case OPResult.OverWrite:
                case OPResult.Trash:
                    ITrace.WriteLine("[MakeInfo] Serialization to xml");
                    XmlSerializer xs = new XmlSerializer(typeof(Game));
                    using (StreamWriter wr = new StreamWriter(xmlDest))
                    {
                        xs.Serialize(wr, zeGame);
                    }
                    return true;

                default:
                    return false;
            }

        }

    }
}
