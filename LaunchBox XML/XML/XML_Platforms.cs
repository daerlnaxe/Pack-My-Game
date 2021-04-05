using LaunchBox_XML.Container;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace LaunchBox_XML.XML
{
    public static class XML_Platforms
    {
        public static event MessageHandler Error;
        public static event MessageHandler Signal;

        /*
        public static List<Platform> LoadPlatforms(string xmlFile)
        {
            XElement
        }*/

        /// <summary>
        /// Lis le fichier xml dédié pour retourner la liste des plateformes - Read dedicated xml file to get the list of platforms
        /// </summary>
        /// <param name="platforms"></param>
        public static void ListShortPlatforms(string xmlFile, ObservableCollection<Platform> platforms)
        {
            try
            {
                foreach (XElement p in XElement.Load(xmlFile).Descendants("Platform"))
                {
                    var name = p.Element("Name");
                    var folder = p.Element("Folder");

                    if (name == null || folder == null)
                        continue;

                    platforms.Add(new Platform()
                    {
                        Name = name.Value,
                        FolderPath = folder.Value,
                    });
                }
            }
            catch (Exception exc)
            {
                Error?.Invoke(nameof(XML_Platforms), exc.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <param name="name"></param>
        /// <param name="baseFolder"></param>
        /// <returns></returns>
        public static Platform GetPlatformPaths(string xmlFile, string name, string baseFolder = null)
        {
            Platform zePlatform = new Platform();
            zePlatform.Name = name;

            XElement root = XElement.Load(xmlFile);

            // --- Folder

            var platforms = from platform in root.Elements("Platform")
                            where (string)platform.Element("Name").Value == name
                            select platform;

            foreach (var field in platforms.ElementAt(0).Descendants())
            {
                if (field.Value == null)
                    continue;

                if (field.Name.LocalName == "Folder")
                    zePlatform.FolderPath = string.IsNullOrEmpty(baseFolder) ? field.Value : Path.GetFullPath(field.Value, baseFolder);
            }

            // --- Additionnal paths

            var platformFolders = from folder in root.Elements("PlatformFolder")
                                  where (string)folder.Element("Platform").Value == name
                                  select folder;

            foreach (var platformFolder in platformFolders)
            {
                PlatformFolder pfFolder = new PlatformFolder();

                foreach (var field in platformFolder.Descendants())
                {
                    if (field.Value == null)
                        continue;

                    switch (field.Name.LocalName)
                    {
                        case "MediaType":
                            pfFolder.MediaType = field.Value;
                            break;

                        case "FolderPath":
                            pfFolder.FolderPath = string.IsNullOrEmpty(baseFolder) ? field.Value: Path.GetFullPath(field.Value, baseFolder);
                            break;

                        default:
                            Signal?.Invoke("XML_Platforms", $"Unmanaged {field.Name.LocalName}");
                            break;
                    }
                }
                zePlatform.PlatformFolders.Add(pfFolder);
            }


            return zePlatform;
        }

        /// <summary>
        /// Save data about a platform
        /// </summary>
        public static void ExtractPlatform(string xmlFile, string name, string destFile)
        {
            XElement root = XElement.Load(xmlFile);

            var platforms = from platform in root.Elements("Platform")
                            where (string)platform.Element("Name").Value == name
                            select platform;

            var additionnalPaths = from additionnalPath in root.Elements("PlatformFolder")
                                   where (string)additionnalPath.Element("Platform").Value == name
                                   select additionnalPath;

            XElement tbPlatform = new XElement("TBPlatform");
            tbPlatform.Add(new XComment("Verbatim backup"));
            tbPlatform.Add(platforms.ElementAt(0), additionnalPaths);

            tbPlatform.Save(destFile);
        }
    }
}

