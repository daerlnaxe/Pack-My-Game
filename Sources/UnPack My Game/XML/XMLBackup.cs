using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using UnPack_My_Game.Cont;

namespace UnPack_My_Game.XML
{
    class XMLBackup
    {
        /// <summary>
        /// Read Enhanced Backup Game
        /// </summary>
        public static void Read_EBGame(string file)
        {
            XmlDocument _XDoc = new XmlDocument();
            _XDoc.Load(file);

            XPathNavigator nav = _XDoc.CreateNavigator();

            // Infos
            XPathNodeIterator nodInfos = nav.Select(nav.Compile($"//LaunchBox_Backup/Game"));
            if (nodInfos.Count != 0)
            {
                while (nodInfos.MoveNext())
                {
                };
            }


        }

        public static void Change_Platform(string ebFile, string platformName)
        {
            XElement xelEB = XElement.Load(ebFile);
            var ee = xelEB.Element("Game").Element("Platform");
            ee.Value = platformName;
            xelEB.Save(ebFile);
        }

        public static void Copy_EBGame(string gameName, string ebFile, string platformFile)
        {
            Debug.WriteLine($"Injection of EBGame to {platformFile}");

            XElement xelEB = XElement.Load(ebFile);
            XElement xelPlatform = XElement.Load(platformFile);

            XmlNode nodCopied;

            // Récupération des données            
            var game = xelEB.Element("Game");                       // Game to add
            var aApps = xelEB.Elements("AdditionalApplication");    // Applications to add
            var custFields = xelEB.Elements("CustomField");          // CustomFields to add

            // Comment
            var comment = new XComment($"{gameName} By UnPack My Game");
            comment = null;

            // Transfert des données
            // Game
            var lastGame = xelPlatform.Elements("Game");
            if (lastGame.Count() > 0)
                lastGame.Last().AddAfterSelf(comment, game);
            else
                xelPlatform.Add(comment, game);

            // Applications
            var lastApp = xelPlatform.Elements("AdditionalApplication");
            if (lastApp.Count() > 0)
                lastApp.Last().AddAfterSelf(comment, aApps, comment);
            else if (game != null)
                xelPlatform.Elements("Game").Last().AddAfterSelf(aApps);

            // CustomFields
            var lastCustField = xelPlatform.Elements("CustomField");
            if (lastCustField.Count() > 0)
                lastCustField.Last().AddAfterSelf(comment, custFields, comment);
            else if (aApps.Count() > 0)
                xelPlatform.Elements("AdditionalApplication").Last().AddAfterSelf(custFields);
            else
                xelPlatform.Elements("Game").Last().AddAfterSelf(custFields);



            //xelPlatform.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test", "mee.xml"));
            xelPlatform.Save(platformFile);


            // Game
            /*
            XmlNode nodSrc = xdocEB.SelectSingleNode("//LaunchBox_Backup/Game");

            nodCopied = xdocPlatform.ImportNode(nodSrc, true);
            xdocPlatform.DocumentElement.AppendChild(nodCopied);

            // AdditionalApplication
            foreach (XmlNode node in xdocEB.SelectNodes("//LaunchBox_Backup/AdditionalApplication"))
            {
                nodCopied = xdocPlatform.ImportNode(node, true);
                xdocPlatform.DocumentElement.AppendChild(nodCopied);
            }

            // CustomField
            foreach(XmlNode node in xdocEB.SelectNodes("//LaunchBox_Backup/CustomField"))
            {
                nodCopied = xdocPlatform.ImportNode(node, true);
                xdocPlatform.DocumentElement.AppendChild(nodCopied);
            }

            xdocPlatform.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test", "mee.xml"));
            */
        }

    
        internal static void Alter_EBPaths(FileObj g, string destPath)
        {
            string tmpFile = Path.Combine(destPath, $"tmp.xml");
            // Backup de l'EBGame            
            File.Copy(Path.Combine(destPath, "EBGame.xml"), tmpFile, true);

            // Altération de la copie
            XDocument xelBack = XDocument.Load(tmpFile);

            /*
                Liste des paths à altérer
                    GamePath
                    Addapplication path
                    manual
                    music
                    videopath
             */

            // Effacement de la backup
            File.Delete(tmpFile);
        }
    }
}
