using LaunchBox_XML.Container;
using LaunchBox_XML.Container.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace LaunchBox_XML.XML
{
    /// <summary>
    /// Custom methods
    /// </summary>
    public class XML_Custom
    {
        public static event MessageHandler Signal;


        /// <summary>
        /// Création d'un fichier infos.xml
        /// </summary>
        /// <returns></returns>
        public static bool Make_InfoGame(string path, GameInfo zeGame)
        {
            string xmlDest = Path.Combine(path, "Infos.xml");

            Signal?.Invoke("Make_InfoGame", "InfoGame Serialization... ");

            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(GameInfo));
                using (StreamWriter wr = new StreamWriter(xmlDest))
                {
                    xs.Serialize(wr, zeGame);
                }
                Signal?.Invoke("Make_InfoGame", "InfoGame Finished");
                return true;
            }
            catch (Exception exc)
            {
                Signal?.Invoke("Make_InfoGame", exc.ToString());
                return false;
            }
        }
    }
}
