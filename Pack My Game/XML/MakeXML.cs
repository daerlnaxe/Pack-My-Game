using DlnxLocalTransfert;
using DxTrace;
using Pack_My_Game.BackupLB;
using Pack_My_Game.Container;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
        public static bool InfoGame(string path, GameInfo zeGame)
        {
            ITrace.WriteLine(prefix: false);
            ITrace.WriteLine($"[MakeInfo] Creation of file 'Infos.xml'");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path); 

            string xmlDest = Path.Combine(path, "Infos.xml");

            var infoRes = OPFiles.SingleVerif(xmlDest, "InfoGame", log: (string message) => ITrace.WriteLine(message, true));
            switch (infoRes)
            {
                case EOPResult.NotExisting:
                case EOPResult.OverWrite:
                case EOPResult.Trashed:
                    ITrace.WriteLine("[InfoGame] Serialization to xml");

                    try
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(GameInfo));
                        using (StreamWriter wr = new StreamWriter(xmlDest))
                        {
                            xs.Serialize(wr, zeGame);
                        }
                        return true;
                    }
                    catch (Exception exc)
                    {
                        ITrace.WriteLine(exc.ToString());
                        return false;
                    }
                default:
                    return false;
            }

        }

        /// <summary>
        /// Conversion des données du jeu en xml tb/eb
        /// </summary>
        /// <param name="path"></param>
        /// <param name="zeGame"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool Backup_Game(string path, Game zeGame, string title)
        {
            ITrace.WriteLine(prefix: false);
            ITrace.WriteLine($"[MakeInfo] Creation of file '{title}.xml'");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path); 

            string xmlDest = Path.Combine(path, $"{title}.xml");

            var infoRes = OPFiles.SingleVerif(xmlDest, "Backup_Game", log: (string message) => ITrace.WriteLine(message, true));
            switch (infoRes)
            {
                case EOPResult.NotExisting:
                case EOPResult.OverWrite:
                case EOPResult.Trashed:
                    /*try
                    {*/
                    ITrace.WriteLine("[Backup_Game] Serialization to xml");
                    XmlSerializer xGame = new XmlSerializer(typeof(Game));
                    XmlSerializer xLCFields = new XmlSerializer(typeof(CustomField));
                    XmlSerializer xLAApp = new XmlSerializer(typeof(AdditionalApplication));
                    //////using (StreamWriter wr = new StreamWriter(xmlDest, false))
                    //////{
                    //////    xGame.Serialize(wr, zeGame, null);
                    //////    foreach (var cFields in zeGame.CustomFields) xLCFields.Serialize(wr, cFields, null);
                    //////    foreach (var aApp in zeGame.AdditionalApplications) xLAApp.Serialize(wr, aApp, null);
                    //////}

                    XmlWriterSettings xws = new XmlWriterSettings();
                    xws.OmitXmlDeclaration = true;                              // remove declaration
                    xws.Indent = true;
                    xws.ConformanceLevel = ConformanceLevel.Auto;
                    


                    using (XmlWriter xw = XmlWriter.Create(xmlDest, xws))
                    {
                        var xmlns = new XmlSerializerNamespaces();              // Remove xml namespaces
                        xmlns.Add("","");

                        xw.WriteStartElement("LaunchBox_Backup");
                        xw.WriteComment("Put this between <LaunchBox> </LaunchBox> in 'platform'.xml (try to organize it)");
                        xGame.Serialize(xw, zeGame, xmlns);
                        xw.WriteComment("For the roms files it represents clones, trainers...");
                        
                        foreach (var aApp in zeGame.AdditionalApplications)
                            xLAApp.Serialize(xw, aApp, xmlns);
                        
                        xw.WriteComment("It's the custom fields that you can create in Launchbox)");
                        foreach (var cFields in zeGame.CustomFields)
                            xLCFields.Serialize(xw, cFields, xmlns);
                        xw.WriteEndElement();
                    }


                    return true;
                /*}
                catch (Exception exc)
                {
                    ITrace.WriteLine(exc.ToString());
                    //return false;
                    ITrace.WriteLine(exc.Message);
                    return false;
                }*/
                default:
                    return false;
            }

        }

    }

}

