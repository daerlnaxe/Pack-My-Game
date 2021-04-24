using AsyncProgress.Cont;
using Common_PMG.Container;
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

namespace Common_PMG.XML
{
    public sealed class XML_Platforms : IDisposable
    {
        public static event MessageHandler Error;
        public static event MessageHandler Signal;

        // --- Non static

        public string _XmlPlatformFile { get; }

        public XElement Root { get; private set; }

        public XML_Platforms(string xmlPlatformFile)
        {
            _XmlPlatformFile = xmlPlatformFile;
            Root = XElement.Load(xmlPlatformFile);
        }




        // --- Exists

        public bool Exists(string field, string value, string balise = "Platform")
        {
            var platforms = from platform in Root.Elements(balise)
                            where (string)platform.Element(field).Value == value
                            select platform;

            return platforms.Any();
        }



        // --- Read

        public static XElement Read(string xmlFile)
        {
            using (XML_Platforms xelPlaform = new XML_Platforms(xmlFile))
            {
                return xelPlaform.Root;
            }
        }

        // --- 

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
                Error?.Invoke(nameof(XML_Platforms), new MessageArg(exc.Message));
            }
        }


        // --- Get Platform



        // --- GetPlatformPaths

        public static Platform GetDirectPlatform(string xmlFile, int number = 0, string baseFolder = null)
        {
            Platform zePlatform = new Platform();
            XElement root = XElement.Load(xmlFile);

            var platforms = root.Elements("Platform");

            if (platforms.Count() == 0)
                return null;

            XElement xelPlatform = platforms.ElementAt(number);
            foreach (XElement element in xelPlatform.Elements())
            {
                switch (element.Name.LocalName)
                {
                    case "Name":
                        zePlatform.Name = element.Value;
                        break;
                    case "Folder":
                        zePlatform.FolderPath = element.Value;
                        break;
                }
            }

            // --- Additionnal paths

            foreach (var platformFolder in root.Elements("PlatformFolder"))
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
                            pfFolder.FolderPath = string.IsNullOrEmpty(baseFolder) ? field.Value : Path.GetFullPath(field.Value, baseFolder);
                            break;

                        default:
                            Signal?.Invoke("XML_Platforms", new MessageArg($"Unmanaged {field.Name.LocalName}"));
                            break;
                    }
                }
                zePlatform.PlatformFolders.Add(pfFolder);
            }


            return zePlatform;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <param name="name"></param>
        /// <param name="baseFolder">Dossier de référence pour les chemins relatifs</param>
        /// <returns></returns>
        /// 
        // le but est d'extraire les chemins pour un nom donné
        public static Platform GetPlatformPaths(string xmlFile, string name, string baseFolder = null)
        {
            Platform zePlatform = new Platform();
            zePlatform.Name = name;

            XElement root = XElement.Load(xmlFile);

            // --- Folder

            var platforms = from platform in root.Elements("Platform")
                            where (string)platform.Element("Name").Value == name
                            select platform;

            foreach (var field in platforms.ElementAt(0).Elements())
            {
                if (field.Value == null)
                    continue;

                if (field.Name.LocalName == "Folder")
                    zePlatform.FolderPath = string.IsNullOrEmpty(baseFolder) ? field.Value : Path.Combine( baseFolder, Tag.Games.ToString(), name);
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
                            Signal?.Invoke("XML_Platforms", new MessageArg($"Unmanaged {field.Name.LocalName}"));
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


        // --- 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcPlatform"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static object GetByTag(XElement srcPlatform, string search)
        {

            var element = srcPlatform.Element(search);

            if (element == null)
                return null;

            return element.Value;
        }

        #region verbatim
        // --- Trans method (verbatim)
        /// <summary>
        /// Jamais testé
        /// </summary>
        /// <param name="selectedPlatformXML"></param>
        /// <param name="destFile"></param>
        public static void TransPlatform(string selectedPlatformXML, string destFile)
        {
            XElement xelSrc = XElement.Load(selectedPlatformXML);


            var platform = xelSrc.Element("Platform");

            var additionnalPaths = xelSrc.Elements("PlatformFolder");

            /*XElement tbPlatform = new XElement("TBPlatform");
            tbPlatform.Add(new XComment("Verbatim backup"));
            tbPlatform.Add(platforms.ElementAt(0), additionnalPaths);

            tbPlatform.Save(destFile);*/
        }

        /// <summary>
        /// Récupère un élément de type platform
        /// </summary>
        /// <param name="srcPlatformXML"></param>
        /// <returns></returns>
        public static XElement GetPlatformNode(string srcPlatformXML)
        {
            XElement xelSrc = XElement.Load(srcPlatformXML);

            return xelSrc.Element("Platform");
        }

        public static IEnumerable<XElement> GetFoldersNodes(string srcPlatformXML)
        {
            XElement xelSrc = XElement.Load(srcPlatformXML);

            return xelSrc.Elements("PlatformFolder");
        }

        // --- Les méthodes non static ne sauvegardent pas


        /// <summary>
        /// 
        /// </summary>
        /// <param name="platformFile"></param>
        /// <returns></returns>
        /// <remarks>
        /// Protection antidoublon
        /// </remarks>
        public bool InjectPlatformExt(string platformFile)
        {
            XElement root = XElement.Load(platformFile);

            // Vérification anti doublon
            if ( Exists(Tag.Name, root.Element(Tag.Platform)?.Element(Tag.Name)?.Value))
                return false;

            // Injection de la plateforme
            XElement newPlat = root.Element(Tag.Platform);
            this.InjectPlatform(newPlat);

            // Injection des dossiers
            var verbatimPFolders = root.Elements(Tag.PlatformFolder);
            this.InjectPlatFolders(verbatimPFolders);

            return true;
        }

        public void InjectPlatform(XElement verbatimPlatform)
        {
            var platforms = Root.Elements("Platform");


            if (platforms.Count() == 0)
                Root.Add(verbatimPlatform);
            else
                platforms.Last().AddAfterSelf(verbatimPlatform);

        }

        public void InjectPlatFolders(IEnumerable<XElement> verbatimPFolders)
        {
            Root.Add(verbatimPFolders);
        }


        // --- Remove
        public static void RemovePlatform(string platformsFile, string tag, string value)
        {
            using (XML_Platforms xPlat = new XML_Platforms(platformsFile))
            {
                XElement root = XElement.Load(platformsFile);
                xPlat.RemoveElemByChild(Tag.Platform, tag, value);
                xPlat.RemoveElemByChild(Tag.PlatformFolder, tag, value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="what">Nom de la balise parent à enlever</param>
        /// <param name="field">Champ enfant</param>
        /// <param name="value">Valeur du champ enfant</param>
        public void RemoveElemByChild(string what, string field, string value)
        {
            Root.Elements(what)
                .Where(x => x.Element(field).Value == value)
                .Remove();
        }


        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="platformsFile"></param>
        public void Save(string platformsFile)
        {
            try
            {
                Root.Save(platformsFile);
            }
            catch (Exception exc)
            {
                Error?.Invoke(this, new MessageArg(exc.Message));
            }

        }


        public void Dispose()
        {
            Root = null;
        }
    }
}

