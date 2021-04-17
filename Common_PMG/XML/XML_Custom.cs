using AsyncProgress.Cont;
using Common_PMG.Container;
using Common_PMG.Container.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Common_PMG.XML
{
    /// <summary>
    /// Custom methods
    /// </summary>
    public class XML_Custom
    {
        public static event MessageHandler Signal;

        public static bool CheckValidity(string linkResult, string balise)
        {
            try
            {
                XElement root = XElement.Load(linkResult);

                var elements = root.Elements(balise);

                return elements.Any();
            }
            catch
            {
                return false;
            }


        }
        /// <summary>
        /// Création d'un fichier infos.xml
        /// </summary>
        /// <returns></returns>
        public static bool Make_InfoGame(string path, GameInfo zeGame)
        {
            string xmlDest = Path.Combine(path, "Infos.xml");

            Signal?.Invoke("Make_InfoGame", new MessageArg( "InfoGame Serialization... "));

            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(GameInfo));
                using (StreamWriter wr = new StreamWriter(xmlDest))
                {
                    xs.Serialize(wr, zeGame);
                }
                Signal?.Invoke("Make_InfoGame", new MessageArg( "InfoGame Finished"));
                return true;
            }
            catch (Exception exc)
            {
                Signal?.Invoke("Make_InfoGame", new MessageArg( exc.ToString()));
                return false;
            }
        }

        public static bool TestPresence(string xmlFile, string balise, string field, string value)
        {
            XElement xelObject = XElement.Load(xmlFile);

            List<XElement> resultat = new List<XElement>();
            foreach(XElement elems in xelObject.Elements(balise))
            {
                foreach(var f in elems.Elements(field))
                {
                    if (((string)f.Value).Equals(value))
                        resultat.Add(elems);

                }
            }
                                            /*.Where(
                                               (p) =>
                                               {
                                                   var element = p.Element(field);
                                                   if (element != null && ((string)p.Element(field).Value).Equals(value))
                                                       return true;
                                                   return false;
                                               }).ToArray();*/
                                            

            return resultat.Any();
        }
    }
}
