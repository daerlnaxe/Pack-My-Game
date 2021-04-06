using LaunchBox_XML.BackupLB;
using LaunchBox_XML.Container;
using LaunchBox_XML.Container.AAPP;
using LaunchBox_XML.Container.Game;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using Unbroken.LaunchBox.Plugins.Data;
using System.ComponentModel;


namespace LaunchBox_XML.XML
{



    /// <summary>
    /// XML Methods used for Launchbox
    /// </summary>
    /// <remarks>
    /// Ne plus utiliser la sérialization pour mieux faire apparaitre les modifications et assurer une meilleure compatibilité
    /// </remarks>
    public class XML_Games
    {
        public static event MessageHandler Signal;

        public static void NewPlatform(string machineXMLFile)
        {
            XElement xelPlatform = new XElement("LaunchBox");
            xelPlatform.Save(machineXMLFile);
        }

        /// <summary>
        /// Make a list of games with minimum informations
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        public static IEnumerable<ShortGame> ListShortGames(string xmlFile)
        {
            ICollection<ShortGame> games = new List<ShortGame>();

            try
            {
                foreach (var g in XElement.Load(xmlFile).Elements("Game"))
                {
                    ShortGame game = new ShortGame();
                    foreach (var field in g.Descendants())
                    {
                        if (field.Value == null)
                            continue;

                        if (Make_BasicGame<ShortGame>(game, field))
                            continue;

                        if (field.Name.LocalName == "ApplicationPath")
                        {
                            game.FileName = Path.GetFileName(field.Value);
                            game.ExploitableFileName = Path.GetFileNameWithoutExtension(game.FileName);//2020zeGame.FileName.Split('.')[0];
                        }
                    }

                    games.Add(game);
                }
            }
            catch (Exception exc)
            {
                Signal?.Invoke(nameof(ListShortGames), exc.Message);
            }

            return games.OrderBy(x => x.Title);
        }

        /// <summary>
        /// Scrape un jeu en mode InfoGame (basic + qqs infos en plus)
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        public static T Scrap_InfoGame<T>(string xmlFile, string fieldName, string fieldValue) where T : I_MedGame, new()
        {
            T gameInf = new T();
            IEnumerable<XElement> games = from game in XElement.Load(xmlFile).Elements("Game")
                                          where (string)game.Element(fieldName).Value == fieldValue
                                          select game;


            foreach (var field in games.ElementAt(0).Descendants())
            {

                if (field.Value == null)
                    continue;

                if (Make_BasicGame(gameInf, field))
                    continue;

                if (Make_InfoGame(gameInf, field))
                    continue;

            }
            return gameInf;
        }

        /// <summary>
        /// Scrape un jeu en mode LBGame en fonction d'un critère.
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        public static LBGame Scrap_LBGame<T>(string xmlFile, string fieldName, string fieldValue) where T : LBGame
        {
            LBGame gameLB = new LBGame();

            IEnumerable<XElement> games = from game in XElement.Load(xmlFile).Elements("Game")
                                          where (string)game.Element(fieldName).Value == fieldValue
                                          select game;

            foreach (var field in games.ElementAt(0).Descendants())
            {
                if (field.Value == null)
                    continue;

                if (Make_BasicGame(gameLB, field))
                    continue;

                if (Make_InfoGame(gameLB, field))
                    continue;

                if (Make_LBGame(gameLB, field))
                    continue;
            }
            return gameLB;
        }





        /*    public static LBGame Scrap_LBGame<T>(string xmlFile) where T : LBGame
            {
                LBGame gameLB = new LBGame();

                IEnumerable<XElement> games = from game in XElement.Load(xmlFile).Elements("Game")
                                              where (string)game.Element(fieldName).Value == fieldValue
                                              select game;

                foreach (var field in games.ElementAt(0).Descendants())
                {
                    if (field.Value == null)
                        continue;

                    if (Make_BasicGame(gameLB, field))
                        continue;

                    if (Make_InfoGame(gameLB, field))
                        continue;

                    if (Make_LBGame(gameLB, field))
                        continue;
                }
                return gameLB;
            }*/


        /// <summary>
        /// Load a game (first)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="root"></param>
        /// <param name="withCF">Switch to have CustomFields</param>        
        public static LBGame Scrap_LBGame(string xmlFile)
        {
            XElement game = XElement.Load(xmlFile).Element("Game");
            LBGame gameLB = new LBGame();

            foreach (var field in game.Descendants())
            {
                if (field.Value == null)
                    continue;

                if (Make_BasicGame(gameLB, field))
                    continue;

                if (Make_InfoGame(gameLB, field))
                    continue;

                if (Make_LBGame(gameLB, field))
                    continue;
            }
            /* 2021
            XmlSerializer xs;
            LBGame g;

            XDocument xDoc = XDocument.Load(file);

            Debug.WriteLine("Normal error about serializer coming soon");
            xs = new XmlSerializer(typeof(LBGame));
            using (var xr = xDoc.Element(root).Element("Game").CreateReader())
            {
                g = (LBGame)xs.Deserialize(xr);
            }

            if (g == null)
                return null;

            //xs = new XmlSerializer(typeof(AdditionalApplication));

            foreach (var elem in xDoc.Element(root).Elements("AdditionalApplication"))
            {
                AdditionalApplication aApp = new AdditionalApplication();
                foreach (var field in elem.Descendants())
                {
                    Make_Clone(field, aApp);
                    Make_AddApp(field, aApp);
                }
                g.AdditionalApplications.Add(aApp);

            }

            if (withCF)
                foreach (var elem in xDoc.Element(root).Elements("CustomField"))
                    g.CustomFields.Add(Make_CustomField(elem));

            /*
    (from step in xDoc.Elements("AdditionalApplication")
     select new AdditionalApplication()
     {

     });*/
            /*2021
            return g;*/
            return gameLB;
        }




        /// <summary>
        /// Récupère les informations basiques
        /// </summary>
        /// <param name="game"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool Make_BasicGame<T>(T game, XElement field) where T : I_BasicGame
        {
            switch (field.Name.LocalName)
            {
                case "ID":
                    game.Id = field.Value;
                    return true;

                case "Title":
                    game.Title = field.Value;
                    return true;

                case "Region":
                    game.Region = field.Value;
                    return true;

                case "Version":
                    game.Version = field.Value;
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Récupère les informations un peu plus avancées
        /// </summary>
        /// <param name="gameInf"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool Make_InfoGame<T>(T gameInf, XElement field) where T : I_MedGame
        {
            switch (field.Name.LocalName)
            {
                case "Developer":
                    gameInf.Developer = field.Value;
                    return true;

                case "Genre":
                    gameInf.Genre = field.Value;
                    return true;

                case "MaxPlayers":
                    gameInf.MaxPlayers = string.IsNullOrEmpty(field.Value) ? null : (int?)int.Parse(field.Value);
                    return true;

                case "Notes":
                    gameInf.Notes = field.Value;
                    return true;

                case "Platform":
                    gameInf.Platform = field.Value;
                    return true;

                case "PlayMode":
                    gameInf.PlayMode = field.Value;
                    return true;

                case "Publisher":
                    gameInf.Publisher = field.Value;
                    return true;

                case "ReleaseDate":
                    gameInf.ReleaseDate = string.IsNullOrEmpty(field.Value) ? null : (DateTime?)DateTime.Parse(field.Value);
                    return true;

                case "Rating":
                    gameInf.Rating = field.Value;
                    return true;

                case "Series":
                    gameInf.Series = field.Value;
                    return true;
            }

            return false;
        }


        /// <summary>
        /// Complete LBGame avec des informations supplémentaires
        /// </summary>
        /// <param name="gameLB"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        private static bool Make_LBGame(LBGame gameLB, XElement field)
        {
            switch (field.Name.LocalName)
            {
                #region complete
                case "Completed":
                    gameLB.Completed = Boolean.Parse(field.Value);
                    return true;

                case "DateAdded":
                    gameLB.DateAdded = DateTime.Parse(field.Value);
                    return true;

                case "DateModified":
                    gameLB.DateModified = DateTime.Parse(field.Value);
                    return true;

                case "Favorite":
                    gameLB.Favorite = Boolean.Parse(field.Value);
                    return true;

                case "DatabaseID":
                case "LaunchBoxDbId":
                    gameLB.LaunchBoxDbId = (int?)int.Parse(field.Value);
                    return true;

                case "SortTitle":
                    gameLB.SortTitle = field.Value;
                    return true;

                case "Status":
                    gameLB.Status = field.Value;
                    return true;

                case "VideoUrl":
                    gameLB.VideoUrl = field.Value;
                    return true;

                case "WikipediaURL":
                case "WikipediaUrl":
                    gameLB.WikipediaUrl = field.Value;
                    return true;

                case "WikipediaId":
                    gameLB.WikipediaId = (int?)int.Parse(field.Value);
                    return true;
                #endregion

                // --- 

                #region Paths
                case "ApplicationPath":
                    gameLB.ApplicationPath = field.Value;
                    return true;

                case "ManualPath":
                    gameLB.ManualPath = field.Value;
                    return true;

                case "MusicPath":
                    gameLB.MusicPath = field.Value;
                    return true;

                case "VideoPath":
                    gameLB.VideoPath = field.Value;
                    return true;

                case "ThemeVideoPath":
                    gameLB.ThemeVideoPath = field.Value;
                    return true;

                case "CustomDosBoxVersionPath":
                case "DosBoxConfigurationPath":
                    gameLB.DosBoxConfigurationPath = field.Value;
                    return true;
                #endregion

                // --- ImagesPath

                // --- 
                #region Stats
                case "CommunityStarRating":
                    gameLB.CommunityStarRating = float.Parse(field.Value.Replace('.', ','));
                    return true;

                case "CommunityStarRatingTotalVotes":
                    gameLB.CommunityStarRatingTotalVotes = int.Parse(field.Value);
                    return true;

                case "LastPlayedDate":
                    gameLB.LastPlayedDate = string.IsNullOrEmpty(field.Value) ? null : (DateTime?)DateTime.Parse(field.Value);
                    return true;

                case "PlayCount":
                    gameLB.PlayCount = int.Parse(field.Value);
                    return true;

                case "StarRating":
                    gameLB.StarRating = int.Parse(field.Value);
                    return true;

                case "StarRatingFloat":
                    gameLB.StarRatingFloat = float.Parse(field.Value);
                    return true;
                #endregion

                // ---

                #region Params de launchbox
                case "AggressiveWindowHiding":
                    gameLB.AggressiveWindowHiding = bool.Parse(field.Value);
                    return true;

                case "DisableShutdownScreen":
                    gameLB.DisableShutdownScreen = bool.Parse(field.Value);
                    return true;

                case "Hide":
                    gameLB.Hide = bool.Parse(field.Value);
                    return true;

                case "HideMouseCursorInGame":
                    gameLB.HideMouseCursorInGame = bool.Parse(field.Value);
                    return true;
                #endregion

                // ---

                #region Paramètres de lancement
                case "CommandLine":
                    gameLB.CommandLine = field.Value;
                    return true;

                case "ConfigurationCommandLine":
                    gameLB.ConfigurationCommandLine = field.Value;
                    return true;

                case "ConfigurationPath":
                    gameLB.ConfigurationPath = field.Value;
                    return true;

                case "Emulator":
                case "EmulatorId":
                    gameLB.EmulatorId = field.Value;
                    return true;

                case "StartupLoadDelay":
                    gameLB.StartupLoadDelay = int.Parse(field.Value);
                    return true;

                case "UseScummVM":
                case "UseScummVm":
                    gameLB.UseScummVm = Boolean.Parse(field.Value);
                    return true;

                case "UseDosBox":
                    gameLB.UseDosBox = Boolean.Parse(field.Value);
                    return true;
                #endregion

                // ---

                #region 
                case "ScummVMAspectCorrection":
                case "ScummVmAspectCorrection":
                    gameLB.ScummVmAspectCorrection = Boolean.Parse(field.Value);
                    return true;

                case "ScummVMFullscreen":
                case "ScummVmFullscreen":
                    gameLB.ScummVmFullscreen = Boolean.Parse(field.Value);
                    return true;

                case "ScummVMGameDataFolderPath":
                case "ScummVmGameDataFolderPath":
                    gameLB.ScummVmGameDataFolderPath = field.Value;
                    return true;

                case "ScummVMGameType":
                case "ScummVmGameType":
                    gameLB.ScummVmGameType = field.Value;
                    return true;

                #endregion

                case "RootFolder":
                    gameLB.RootFolder = field.Value;
                    return true;

                case "Source":
                    gameLB.Source = field.Value;
                    return true;

                case "MissingVideo":
                    gameLB.MissingVideo = bool.Parse(field.Value);
                    return true;

                case "MissingBoxFrontImage":
                    gameLB.MissingBoxFrontImage = bool.Parse(field.Value);
                    return true;

                case "MissingScreenshotImage":
                    gameLB.MissingScreenshotImage = bool.Parse(field.Value);
                    return true;

                case "MissingClearLogoImage":
                    gameLB.MissingClearLogoImage = bool.Parse(field.Value);
                    return true;

                case "MissingBackgroundImage":
                    gameLB.MissingBackgroundImage = bool.Parse(field.Value);
                    return true;

                case "Broken":
                    gameLB.Broken = bool.Parse(field.Value);
                    return true;

                case "Portable":
                    gameLB.Portable = bool.Parse(field.Value);
                    return true;

                case "UseStartupScreen":
                    gameLB.UseStartupScreen = bool.Parse(field.Value);
                    return true;

                case "OverrideDefaultStartupScreenSettings":
                    gameLB.OverrideDefaultStartupScreenSettings = bool.Parse(field.Value);
                    return true;

                case "HideAllNonExclusiveFullscreenWindows":
                    gameLB.HideAllNonExclusiveFullscreenWindows = bool.Parse(field.Value);
                    return true;

                case "ReleaseType":
                    gameLB.ReleaseType = field.Value;
                    return true;

                // ---

                default:
                    Signal?.Invoke(nameof(Make_LBGame), $"Not managed: {field.Name.LocalName}");
                    return false;

            }

        }


        public static List<Clone> ListClones(string xmlFile)
        {
            List<Clone> aAppz = new List<Clone>();

            foreach (var element in XElement.Load(xmlFile).Elements(nameof(AdditionalApplication)))
            {
                Clone cln = new Clone();

                foreach (var field in element.Descendants())
                {
                    Make_Clone(field, cln);
                }
                aAppz.Add(cln);
            }

            return aAppz;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public static IEnumerable<Clone> ListClones(string xmlFile, string fieldName, string fieldValue)
        {
            //var root = XElement.Load(xmlFile);
            List<Clone> aAppz = new List<Clone>();
            var elements = from addApp in XElement.Load(xmlFile).Elements("AdditionalApplication")
                           where (string)addApp.Element(fieldName).Value == fieldValue
                           select addApp;

            foreach (var element in elements)
            {
                Clone cln = new Clone();

                foreach (var field in element.Descendants())
                {
                    Make_Clone(field, cln);
                }
                aAppz.Add(cln);
            }

            return aAppz;
        }

        public static List<AdditionalApplication> ListAddApps(string xmlFile)
        {
            List<AdditionalApplication> aAppz = new List<AdditionalApplication>();

            foreach (var element in XElement.Load(xmlFile).Elements(nameof(AdditionalApplication)))
            {
                AdditionalApplication addApp = new AdditionalApplication();

                foreach (var field in element.Descendants())
                {
                    Make_Clone(field, addApp);
                    Make_AddApp(field, addApp);
                }
                aAppz.Add(addApp);
            }

            return aAppz;
        }

        /// <summary>
        /// Base des Additionnal Applications
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool Make_Clone<T>(XElement elem, T clone) where T : I_Clone
        {
            switch (elem.Name.LocalName)
            {
                case nameof(clone.Id):
                    clone.Id = elem.Value;
                    break;

                case nameof(clone.GameID):
                    clone.GameID = elem.Value;
                    break;

                case nameof(clone.ApplicationPath):
                    clone.ApplicationPath = elem.Value;
                    break;
            }


            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool Make_AddApp(XElement elem, AdditionalApplication AApp)
        {
            switch (elem.Name.LocalName)
            {
                case nameof(AApp.PlayCount):
                    AApp.PlayCount = int.Parse(elem.Value);
                    break;

                case nameof(AApp.AutoRunAfter):
                    AApp.AutoRunAfter = bool.Parse(elem.Value);
                    break;

                case nameof(AApp.AutoRunBefore):
                    AApp.AutoRunBefore = bool.Parse(elem.Value);
                    break;

                case nameof(AApp.CommandLine):
                    AApp.CommandLine = elem.Value;
                    break;

                case nameof(AApp.Name):
                    AApp.Name = elem.Value;
                    break;

                case nameof(AApp.UseDosBox):
                    AApp.UseDosBox = bool.Parse(elem.Value);
                    break;

                case nameof(AApp.UseEmulator):
                    AApp.UseEmulator = bool.Parse(elem.Value);
                    break;

                case nameof(AApp.WaitForExit):
                    AApp.WaitForExit = bool.Parse(elem.Value);
                    break;

                case nameof(AApp.Developer):
                    AApp.Developer = elem.Value;
                    break;

                case nameof(AApp.Publisher):
                    AApp.Publisher = elem.Value;
                    break;

                case nameof(AApp.Region):
                    AApp.Region = elem.Value;
                    break;

                case nameof(AApp.Version):
                    AApp.Version = elem.Value;
                    break;

                case nameof(AApp.Status):
                    AApp.Status = elem.Value;
                    break;

                case nameof(AApp.EmulatorId):
                    AApp.EmulatorId = elem.Value;
                    break;

                case nameof(AApp.SideA):
                    AApp.SideA = bool.Parse(elem.Value);
                    break;

                case nameof(AApp.SideB):
                    AApp.SideB = bool.Parse(elem.Value);
                    break;

                case nameof(AApp.Priority):
                    AApp.Priority = int.Parse(elem.Value);
                    break;


                default:
                    Debug.WriteLine($"Non pris en charge: {elem.Name}");
                    break;
            }
            return true;
        }

        public static List<CustomField> ListCustomFields(string xmlFile, string balise)
        {
            List<CustomField> customFields = new List<CustomField>();

            foreach (var element in XElement.Load(xmlFile).Elements(balise))
            {

                customFields.Add(Make_CustomField(element));

            }

            return customFields;
        }

        private static CustomField Make_CustomField(XElement element)
        {
            CustomField cf = new CustomField();

            foreach (var elem in element.Elements())
            {
                switch (elem.Name.LocalName)
                {
                    case nameof(cf.GameID):
                        cf.GameID = elem.Value;
                        break;

                    case nameof(cf.Name):
                        cf.Name = elem.Value;
                        break;

                    case nameof(cf.Value):
                        cf.Value = elem.Value;
                        break;

                    default:
                        Debug.WriteLine($"Non pris en charge: {elem.Name}");
                        break;
                }
            }

            return cf;
        }

        // ---

        public static void InjectGame(LBGame lbGame, string platformFile)
        {
            XElement xelPlatform = XElement.Load(platformFile);

            IOrderedEnumerable<PropertyInfo> mee = typeof(LBGame).GetProperties()
                .Where(p => p.CustomAttributes.Any(y => y.AttributeType == typeof(XmlElementAttribute)))
                .OrderBy(g => g.GetCustomAttributes(false).OfType<XmlElementAttribute>().First().Order);


            /*var infos = typeof(LBGame).GetProperties()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(IsViewable))).ToList();*/
            /*
            foreach (var m in mee)
            {
                m.GetCustomAttributes(false);

            }*/


            /*        IEnumerable<string> t = typeof(LBGame)
                            .GetProperties()
                                  .OrderBy(p => p.GetCustomAttributes(false).OfType<XmlElementAttribute>().First().Order)
                                      .Select(p => p.Name);

                    ;*/

            XElement xelGame = new XElement("Game");


            foreach (System.Reflection.PropertyInfo p in mee)
            {
                var value = p.GetValue(lbGame);

                // On passe quand c'est null sur un type int car sans ça, LaunchBox plante
                if (p.PropertyType == typeof(int?) && value == null)
                    continue;

                if (p.PropertyType == typeof(string) && string.IsNullOrEmpty((string)value))
                    value = null;

                //Debug.WriteLine($"{p.Name}: {p.GetValue(lbGame)}");
                switch (p.Name)
                {
                    case nameof(lbGame.Id):
                        xelGame.Add(new XElement(nameof(lbGame.Id).ToUpper(), p.GetValue(lbGame))); ;
                        break;
                    default:
                        xelGame.Add(new XElement(p.Name, p.GetValue(lbGame)));
                        break;
                }
            }

            /*
            IEnumerable<XElement> basicPart = ConvertBasicGame(lbGame);

            IEnumerable<XElement> medPart = ConvertMedGame(lbGame);

            IEnumerable<XElement> lbPart = ConvertLBGame(lbGame);
            */


            /*XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;                              // remove declaration
            xws.Indent = true;
            xws.ConformanceLevel = ConformanceLevel.Auto;

            var xmlns = new XmlSerializerNamespaces();              // Remove xml namespaces
            xmlns.Add("", "");

            // 
            XmlSerializer xs;
            StringBuilder sb = new StringBuilder();

            //XmlWriter sw = XmlWriter.Create(sb, xws);

            // Jeu
            xs = new XmlSerializer(typeof(LBGame));
            Debug.WriteLine("Exception about serializer coming soon");
            using (XmlWriter sw = XmlWriter.Create(sb, xws))
                xs.Serialize(sw, lbGame, xmlns);

            XElement xelGame = XElement.Parse(sb.ToString());
            */



            var games = xelPlatform.Elements("Game");
            if (games.Count() != 0)
                games.Last().AddAfterSelf(xelGame);
            else
                xelPlatform.Add(xelGame);


            //xelPlatform.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Test", "mee.xml"));
            xelPlatform.Save(platformFile);
        }


        public static void InjectAddApps(IEnumerable<AdditionalApplication> addApps, string platformFile)
        {
            XElement xelPlatform = XElement.Load(platformFile);

            // Conversion en XElements
            IEnumerable<XElement> xelAddApp = from addApp in addApps
                                              select ConvertAddApp(addApp);

            // On cherche les autres Applications additionnelles
            IEnumerable<XElement> exAApps = xelPlatform.Elements(nameof(AdditionalApplication));

            // Dans le cas où il y en a d'autres
            if (exAApps.Count() > 0)
            {
                exAApps.Last().AddAfterSelf(xelAddApp);
                xelPlatform.Save(platformFile);
                return;
            }

            // Sinon on ajoute après les jeux
            IEnumerable<XElement> exGames = xelPlatform.Elements("Game");
            if (exGames.Count() > 0)
            {
                exGames.Last().AddAfterSelf(xelAddApp);
                xelPlatform.Save(platformFile);
                return;
            }

        }

        public static void InjectCustomFields(IEnumerable<CustomField> cFields, string platformFile)
        {
            XElement xelPlatform = XElement.Load(platformFile);

            // Conversion en XElements
            IEnumerable<XElement> xelCFields = from cField in cFields
                                               select ConvertCField(cField);

            xelPlatform.Add(xelCFields);
            xelPlatform.Save(platformFile);
        }


        // ---

        /// <summary>
        /// Récupère les infos relatives à un game et injecte dans un autre fichier (aucune altération)
        /// </summary>
        /// <param name="id"></param>
        public static void TrueBackup(string xmlFile, string id, string destFolder)
        {
            XElement root = XElement.Load(xmlFile);

            XElement game = root.Elements("Game")
                              .Where((x) => x.Element("ID").Value == id)
                              .SingleOrDefault();

            var addApps = from aApp in root.Elements("AdditionalApplication")
                          where (string)aApp.Element("GameID").Value == id
                          select aApp;

            var customF = from cf in root.Elements("CustomField")
                          where (string)cf.Element("GameID").Value == id
                          select cf;


            XElement tbGame = new XElement("TBGame");
            tbGame.Add(new XComment("Verbatim backup"));
            tbGame.Add(game, addApps, customF);

            tbGame.Save(Path.Combine(destFolder, "TBGame.xml"));
        }


        /// <summary>
        /// Backup with modification on media paths
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <param name="lbGame"></param>
        /// <param name="destFolder"></param>
        /// <remarks>
        /// Theme video unmanaged for the while
        /// </remarks>
        public static void EnhancedBackup(string xmlFile, LBGame lbGame, string destFolder)
        {
            XElement root = XElement.Load(xmlFile);

            XElement game = root.Elements("Game")
                              .Where((x) => x.Element("ID").Value == lbGame.Id)
                              .SingleOrDefault();

            game.Element("ManualPath").Value = lbGame.ManualPath;
            game.Element("MusicPath").Value = lbGame.MusicPath;
            game.Element("VideoPath").Value = lbGame.VideoPath;
            //game.Element("ThemeVideoPath").Value = lbGame.ThemeVideoPath;


            var addApps = from aApp in root.Elements("AdditionalApplication")
                          where (string)aApp.Element("GameID").Value == lbGame.Id
                          select aApp;

            var customF = from cf in root.Elements("CustomField")
                          where (string)cf.Element("GameID").Value == lbGame.Id
                          select cf;


            XElement ebGame = new XElement("EBGame");
            ebGame.Add(new XComment("Backup with media paths"));


            ebGame.Add(game, addApps, customF);

            ebGame.Save(Path.Combine(destFolder, "EBGame.xml"));
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="iD"></param>
        /// <param name="platformFile"></param>
        public static void Remove_Game(string iD, string platformFile)
        {
            XElement xelPlatform = XElement.Load(platformFile);

            IEnumerable<XElement> games = from game in xelPlatform.Elements("Game")
                                          where (string)game.Element("ID").Value == iD
                                          select game;

            xelPlatform.Elements("Game")
                .Where(x => x.Element("ID").Value == iD)
                .Remove();

            xelPlatform.Elements("AdditionalApplication")
                .Where(x => x.Element("GameID").Value == iD)
                .Remove();

            xelPlatform.Elements("CustomField")
                .Where(x => x.Element("GameID").Value == iD)
                .Remove();

            /* if(games.Any())
             {
                 foreach (XElement g in games)
                 {
                     g.Remove();

                 }

             }
            */
            xelPlatform.Save(platformFile);
        }


        // --- Conversions vers XElement

        public static IEnumerable<XElement> ConvertBasicGame(BasicGame shGame)
        {
            List<XElement> values = new List<XElement>();

            values.Add(new XElement(nameof(shGame.Id), shGame.Id));
            values.Add(new XElement(nameof(shGame.Title), shGame.Title));
            values.Add(new XElement(nameof(shGame.Region), shGame.Region));
            values.Add(new XElement(nameof(shGame.Version), shGame.Version));

            return values;
        }

        public static IEnumerable<XElement> ConvertMedGame(MedGame medGame)
        {
            List<XElement> values = new List<XElement>();

            values.Add(new XElement(nameof(medGame.Platform), medGame.Platform));
            values.Add(new XElement(nameof(medGame.Series), medGame.Series));
            values.Add(new XElement(nameof(medGame.Genre), medGame.Genre));
            values.Add(new XElement(nameof(medGame.PlayMode), medGame.PlayMode));
            values.Add(new XElement(nameof(medGame.MaxPlayers), medGame.MaxPlayers));
            values.Add(new XElement(nameof(medGame.Developer), medGame.Developer));
            values.Add(new XElement(nameof(medGame.Publisher), medGame.Publisher));
            values.Add(new XElement(nameof(medGame.ReleaseDate), medGame.ReleaseDate));
            values.Add(new XElement(nameof(medGame.Rating), medGame.Rating));
            values.Add(new XElement(nameof(medGame.Notes), medGame.Notes));

            return values;
        }

        public static IEnumerable<XElement> ConvertLBGame(LBGame lbGame)
        {
            List<XElement> values = new List<XElement>();



            return values;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addApp"></param>
        /// <returns></returns>
        public static XElement ConvertAddApp(AdditionalApplication addApp)
        {
            XElement xel = new XElement(nameof(AdditionalApplication));
            xel.Add
                (
                    new XElement(nameof(addApp.Id), addApp.Id),
                    new XElement(nameof(addApp.PlayCount), addApp.PlayCount),
                    new XElement(nameof(addApp.GameID), addApp.GameID),
                    new XElement(nameof(addApp.ApplicationPath), addApp.ApplicationPath),
                    new XElement(nameof(addApp.AutoRunAfter), addApp.AutoRunAfter),
                    new XElement(nameof(addApp.AutoRunBefore), addApp.AutoRunBefore),
                    new XElement(nameof(addApp.Name), addApp.Name),
                    new XElement(nameof(addApp.UseDosBox), addApp.UseDosBox),
                    new XElement(nameof(addApp.UseEmulator), addApp.UseEmulator),
                    new XElement(nameof(addApp.WaitForExit), addApp.WaitForExit),
                    new XElement(nameof(addApp.Version), addApp.Version),
                    new XElement(nameof(addApp.Status), addApp.Status),
                    new XElement(nameof(addApp.Version), addApp.Version),
                    new XElement(nameof(addApp.EmulatorId), addApp.EmulatorId),
                    new XElement(nameof(addApp.SideA), addApp.SideA),
                    new XElement(nameof(addApp.SideB), addApp.SideB),
                    new XElement(nameof(addApp.Priority), addApp.Priority)
                );
            return xel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cField"></param>
        /// <returns></returns>
        private static XElement ConvertCField(CustomField cField)
        {
            XElement xel = new XElement(nameof(CustomField));
            xel.Add
                (
                    new XElement(nameof(cField.GameID), cField.GameID),
                    new XElement(nameof(cField.Name), cField.Name),
                    new XElement(nameof(cField.Value), cField.Value)
                );

            return xel;
        }

        // --- Transferts direct d'un fichier xml à un autre (verbatim)

        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcXmlFile"></param>
        /// <param name="destXmlFile"></param>
        /// <remarks>
        /// Evite normalement les doublons
        /// </remarks>
        public static void Trans_CustomF(string srcXmlFile, string destXmlFile)
        {
            XElement xSource = XElement.Load(srcXmlFile);
            XElement xDestination = XElement.Load(destXmlFile);

            var cSrcFields = xSource.Elements(nameof(CustomField));
            var cDestFields = xDestination.Elements(nameof(CustomField));

            foreach (var cField in cSrcFields)
            {
                if (cDestFields.FirstOrDefault((x) => x.Element("GameID") == cField.Element("GameID")) != null)
                    continue;

                xDestination.Add(cField);
            }

            xDestination.Save(destXmlFile);
        }
    }
}
