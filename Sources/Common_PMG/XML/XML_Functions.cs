﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Globalization;
using Common_PMG.Container;
using Common_PMG.BackupLB;
using System.Collections.ObjectModel;
using Common_PMG.Container.Game;
using Common_PMG.Container.AAPP;
using Common_PMG.Container.Game.LaunchBox;
using AsyncProgress.Cont;

namespace Common_PMG.XML
{

    /*
     * Balise de LaunchBox:
     *  - LaunchBox: Balise générale
     *  - Platform: pour chaque nouvelle
     *  - Name: pour le nom utilisé (qui correspond au nom du fichier)
     *   
     * Data du Settings:
     *  - fPlatforms: general xmlFile with all the platforms listed
     *  - dPlatforms: directory root for all the platforms xml files
    */
    [Obsolete]
    public class XML_Functions
    {
        public event MessageHandler Error;

        XmlDocument _XDoc;
        string _XmlFile;


        public bool ReadFile(string xmlFile)
        {
            _XmlFile = xmlFile;
            _XDoc = new XmlDocument();

            try
            {
                _XDoc.Load(_XmlFile);
            }
            catch
            {
                Error?.Invoke(this, new MessageArg($"'{_XmlFile}' not found"));
                //MessageBox.Show($"'{_XmlFile}' not found", "Error - list of machines", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }




        /// <summary>
        /// Lis le fichier xml dédié pour retourner la liste des jeux - Read dedicated xml file to get the list of games
        /// </summary>
        /// <param name="lGames"></param>
        public Dictionary<string, ShortGame> ListGames()
        {
            Dictionary<string, ShortGame> lGames = new Dictionary<string, ShortGame>();

            XPathNavigator nav = _XDoc.CreateNavigator();
            XPathNodeIterator nodes = nav.Select(nav.Compile("//LaunchBox/Game"));

            if (nodes.Count != 0)
            {

                while (nodes.MoveNext())
                {
                    Console.WriteLine(nodes.Current.Name);
                    nodes.Current.MoveToFirstChild();

                    ShortGame zeGame = new ShortGame();

                    do
                    {

                        switch (nodes.Current.Name)
                        {
                            case "Title":
                                Console.WriteLine(nodes.Current.Value);
                                zeGame.Title = nodes.Current.Value.Replace(";amp", "&");
                                break;

                            case "ID":
                                Console.WriteLine(nodes.Current.Value);
                                zeGame.Id = nodes.Current.Value;
                                break;

                            case "ApplicationPath":
                                Console.WriteLine(nodes.Current.Value);
                                zeGame.FileName = Path.GetFileName(nodes.Current.Value);
                                zeGame.ExploitableFileName = Path.GetFileNameWithoutExtension(zeGame.FileName);//2020zeGame.FileName.Split('.')[0];
                                break;

                            case "Region":
                                Console.WriteLine(nodes.Current.Value);
                                zeGame.Region = nodes.Current.Value;
                                break;

                                //case "Version":
                                //    Console.WriteLine(nodes.Current.Value);

                                //    zeGame.Version = nodes.Current.Value;
                                //    break;
                        }
                    } while (nodes.Current.MoveToNext());

                    lGames.Add(zeGame.Id, zeGame);
                }

            }
            return lGames;
        }

        /// <summary>
        /// Lis le fichier xml dédié pour retourner la liste des clones - Read dedicated xml file to get the list of clones
        /// </summary>
        /// <param name="lGames"></param>
        [Obsolete]
        public void ListClones(List<Clone> lClones, string ID)
        {
            XPathNavigator nav = _XDoc.CreateNavigator();
            XPathNodeIterator nodes = nav.Select(nav.Compile($"//LaunchBox/AdditionalApplication[GameID='{ID}']"));

            if (nodes.Count != 0)
            {

                while (nodes.MoveNext())
                {
                    Console.WriteLine(nodes.Current.Name);
                    nodes.Current.MoveToFirstChild();

                    Clone zeClone = new Clone();

                    do
                    {

                        switch (nodes.Current.Name)
                        {
                            case "ApplicationPath":
                                Console.WriteLine(nodes.Current.Value);
                                zeClone.ApplicationPath = nodes.Current.Value;
                                break;

                            case "GameID":
                                Console.WriteLine(nodes.Current.Value);
                                zeClone.GameId = nodes.Current.Value;
                                break;

                            case "Id":
                                Console.WriteLine(nodes.Current.Value);
                                zeClone.Id = nodes.Current.Value;
                                break;

                        }
                    } while (nodes.Current.MoveToNext());

                    lClones.Add(zeClone);
                }

            }
        }

        /// <summary>
        /// Convert a game in object
        /// </summary>
        /// <param name="game"></param>
        [Obsolete]
        public MedGame ScrapGame(string ID)
        {
            MedGame zeGame = null;

            XPathNavigator nav = _XDoc.CreateNavigator();
            Console.WriteLine($"//LaunchBox/Game[ID='{ID}']");
            // ID pour les games principaux et Id pour les secondaires
            XPathNodeIterator nodes = nav.Select(nav.Compile($"//LaunchBox/Game[ID='{ID}']"));


            Console.WriteLine($"{nodes.Count} trouvés avec {ID}");

            if (nodes.Count != 0)
            {
                zeGame = new MedGame();

                nodes.MoveNext();

                Console.WriteLine(nodes.Current.Name);
                nodes.Current.MoveToFirstChild();

                zeGame.Id = ID;

                string tmp;
                do
                {
                    Console.Write($"\t{nodes.Current.Name}\t\t\t\t");

                    switch (nodes.Current.Name)
                    {
                        #region ShortGame
                        case "Title":
                            tmp = nodes.Current.Value;
                            //tmp = StringOp.Convert(tmp);

                            Console.WriteLine(tmp);
                            zeGame.Title = tmp;
                            break;

                        case "ID":
                            Console.WriteLine(nodes.Current.Value);

                            zeGame.Id = nodes.Current.Value;
                            break;

                        case "Version":
                            Console.WriteLine(nodes.Current.Value);

                            zeGame.Version = nodes.Current.Value;
                            break;
                        #endregion

                        #region Infos
                        case "Platform":
                            Console.WriteLine(nodes.Current.Value);
                            zeGame.Platform = nodes.Current.Value;
                            break;

                        case "Developer":
                            Console.WriteLine(nodes.Current.Value);
                            zeGame.Developer = nodes.Current.Value;
                            break;

                        case "Publisher":
                            Console.WriteLine(nodes.Current.Value);
                            zeGame.Publisher = nodes.Current.Value;
                            break;

                        case "Rating":
                            Console.WriteLine(nodes.Current.Value);
                            zeGame.Rating = nodes.Current.Value;
                            break;

                        case "ReleaseDate":
                            Console.WriteLine(nodes.Current.Value);
                            //2021    zeGame.ReleaseDate = nodes.Current.Value;
                            break;

                        case "Genre":
                            Console.WriteLine(nodes.Current.Value);
                            zeGame.Genre = nodes.Current.Value;
                            break;

                        case "Series":
                            Console.WriteLine(nodes.Current.Value);
                            zeGame.Series = nodes.Current.Value;
                            break;

                        case "Region":
                            Console.WriteLine(nodes.Current.Value);
                            zeGame.Region = nodes.Current.Value;
                            break;

                        case "Notes":
                            Console.WriteLine(nodes.Current.Value);
                            zeGame.Notes = nodes.Current.Value;
                            break;
                        #endregion
                        /*
                                                #region files
                                                case "ManualPath":
                                                    Console.WriteLine(nodes.Current.Value);
                                                    zeGame.ManualPath = nodes.Current.Value;
                                                    break;

                                                case "MusicPath":
                                                    Console.WriteLine(nodes.Current.Value);
                                                    zeGame.MusicPath = nodes.Current.Value;
                                                    break;

                                                case "VideoPath":
                                                    Console.WriteLine(nodes.Current.Value);
                                                    zeGame.VideoPath = nodes.Current.Value;
                                                    break;

                                                case "ApplicationPath":
                                                    Console.WriteLine(nodes.Current.Value);
                                                    zeGame.ApplicationPath = nodes.Current.Value;
                                                    zeGame.FileName = Path.GetFileName(nodes.Current.Value);
                                                    zeGame.ExploitableFileName = zeGame.FileName.Split('.')[0];
                                                    break;
                                                #endregion
                            */
                        default:
                            Console.WriteLine();
                            break;

                    }
                } while (nodes.Current.MoveToNext());

            }


            return zeGame;
        }


        /*
        /// <summary>
        /// Get a game
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [Obsolete]
        public LBGame ScrapBackupGame(string ID)
        {
            LBGame backGame = null;

            XPathNavigator nav = _XDoc.CreateNavigator();
            Debug.WriteLine($"//LaunchBox/Game[ID='{ID}']");
            // ID pour les games principaux et Id pour les secondaires

            // Get main part
            XPathNodeIterator nodes = nav.Select(nav.Compile($"//LaunchBox/Game[ID='{ID}']"));

            Debug.WriteLine($"{nodes.Count} trouvés avec {ID}");

            if (nodes.Count != 0)
            {
                backGame = new LBGame();

                nodes.MoveNext();

                Debug.WriteLine(nodes.Current.Name);
                nodes.Current.MoveToFirstChild();

                backGame.Id = ID;

                string tmp;
                do
                {
                    Console.Write($"\t{nodes.Current.Name}\t\t\t\t");

                    switch (nodes.Current.Name)
                    {
                        case "ApplicationPath":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.ApplicationPath = nodes.Current.Value;
                            break;

                        case "CommandLine":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.CommandLine = nodes.Current.Value;
                            break;

                        case "Completed":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.Completed = bool.Parse(nodes.Current.Value);
                            break;

                        case "ConfigurationCommandLine":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.ConfigurationCommandLine = nodes.Current.Value;
                            break;

                        case "ConfigurationPath":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.ConfigurationPath = nodes.Current.Value;
                            break;

                        case "DateAdded":
                            Console.WriteLine(nodes.Current.Value);
                            //2021backGame.DateAdded = nodes.Current.Value;
                            break;

                        case "DateModified":
                            Console.WriteLine(nodes.Current.Value);
                            //2021backGame.DateModified = nodes.Current.Value;

                            break;
                        case "Developer":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.Developer = nodes.Current.Value;
                            break;

                        case "DosBoxConfigurationPath":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.DosBoxConfigurationPath = nodes.Current.Value;
                            break;

                        case "EmulatorId":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.EmulatorId = nodes.Current.Value;
                            break;

                        case "Favorite":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.Favorite = bool.Parse(nodes.Current.Value);
                            break;

                        case "ID":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.Id = nodes.Current.Value;
                            break;

                        case "LastPlayedDate":
                            Console.WriteLine(nodes.Current.Value);
                            //2021backGame.LastPlayedDate = nodes.Current.Value;
                            break;

                        case "ManualPath":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.ManualPath = nodes.Current.Value;
                            break;

                        case "MusicPath":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.MusicPath = nodes.Current.Value;
                            break;

                        case "Notes":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.Notes = nodes.Current.Value;
                            break;

                        case "Platform":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.Platform = nodes.Current.Value;
                            break;

                        case "Publisher":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.Publisher = nodes.Current.Value;
                            break;

                        case "Rating":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.Rating = nodes.Current.Value;
                            break;

                        case "ReleaseDate":
                            Console.WriteLine(nodes.Current.Value);
                            //2021backGame.ReleaseDate = nodes.Current.Value;
                            break;

                        case "RootFolder":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.RootFolder = nodes.Current.Value;
                            break;

                        case "ScummVMAspectCorrection":
                        case "ScummVmAspectCorrection":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.ScummVmAspectCorrection = bool.Parse(nodes.Current.Value);
                            break;

                        case "ScummVMFullscreen":
                        case "ScummVmFullscreen":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.ScummVmFullscreen = bool.Parse(nodes.Current.Value);
                            break;

                        case "ScummVMGameDataFolderPath":
                        case "ScummVmGameDataFolderPath":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.ScummVmGameDataFolderPath = nodes.Current.Value;
                            break;

                        case "ScummVMGameType":
                        case "ScummVmGameType":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.ScummVmGameType = nodes.Current.Value;
                            break;

                        case "SortTitle":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.SortTitle = nodes.Current.Value;
                            break;

                        case "Source":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.Source = nodes.Current.Value;
                            break;

                        case "StarRatingFloat":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.StarRatingFloat = int.Parse(nodes.Current.Value);
                            break;

                        case "StarRating":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.StarRating = int.Parse(nodes.Current.Value);
                            break;

                        case "CommunityStarRating":
                            try
                            {
                                string raoul = nodes.Current.Value;
                                
                                // float.Parse(nodes.Current.Value, CultureInfo.InvariantCulture.NumberFormat);
                                string valeur = raoul.Replace('.', ',');
                                backGame.CommunityStarRating = float.Parse(valeur);
                                
                            }
                            catch (Exception exc)
                            {
                                Error?.Invoke(this, $"{nodes.Current.Value} \r\n" + exc.ToString());
                                //MessageBox.Show($"{nodes.Current.Value} \r\n" + exc.ToString());
                            }
                            break;

                        case "CommunityStarRatingTotalVotes":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.CommunityStarRatingTotalVotes = int.Parse(nodes.Current.Value);
                            break;

                        case "Status":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.Status = nodes.Current.Value;
                            break;

                        case "DatabaseID":
                        case "LaunchBoxDbId":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.LaunchBoxDbId = int.Parse(nodes.Current.Value);
                            break;

                        case "WikipediaURL":
                        case "WikipediaUrl":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.WikipediaUrl = nodes.Current.Value;
                            break;

                        case "Title":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.Title = nodes.Current.Value;
                            break;

                        case "UseDosBox":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.UseDosBox = bool.Parse(nodes.Current.Value);
                            break;

                        case "UseScummVM":
                        case "UseScummVm":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.UseScummVm = bool.Parse(nodes.Current.Value);
                            break;

                        case "Version":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.Version = nodes.Current.Value;
                            break;

                        case "Series":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.Series = nodes.Current.Value;
                            break;

                        case "PlayMode":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.PlayMode = nodes.Current.Value;
                            break;

                        case "Region":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.Region = nodes.Current.Value;
                            break;

                        case "PlayCount":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.PlayCount = int.Parse(nodes.Current.Value);
                            break;

                        case "Portable":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.Portable = bool.Parse(nodes.Current.Value);
                            break;

                        case "VideoPath":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.VideoPath = nodes.Current.Value;
                            break;

                        case "Hide":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.Hide = bool.Parse(nodes.Current.Value);
                            break;

                        case "Broken":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.Broken = bool.Parse(nodes.Current.Value);
                            break;

                        case "Genre":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.Genre = nodes.Current.Value;
                            break;

                        case "MissingVideo":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.MissingVideo = bool.Parse(nodes.Current.Value);
                            break;

                        case "MissingBoxFrontImage":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.MissingBoxFrontImage = bool.Parse(nodes.Current.Value);
                            break;

                        case "MissingScreenshotImage":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.MissingScreenshotImage = bool.Parse(nodes.Current.Value);
                            break;

                        case "MissingClearLogoImage":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.MissingClearLogoImage = bool.Parse(nodes.Current.Value);
                            break;

                        case "MissingBackgroundImage":
                            Console.WriteLine(nodes.Current.Value);
                            backGame.MissingBackgroundImage = bool.Parse(nodes.Current.Value);
                            break;

                        default:
                            Console.WriteLine();
                            break;

                    }
                } while (nodes.Current.MoveToNext());

            }

            // Get addionnal apps
            XPathNodeIterator aaNodes = nav.Select(nav.Compile($"//LaunchBox/AdditionalApplication[GameID='{ID}']"));
            if (aaNodes.Count != 0)
            {
                //2020 --- modification initialisation dès le début backGame.AdditionalApplications = new List<AdditionalApplication>();

                // Loop on additionnal apps
                while (aaNodes.MoveNext())
                {
                    aaNodes.Current.MoveToFirstChild();
                    var tmpAApp = new AdditionalApplication();

                    do
                    {
                        Console.WriteLine("Case " + aaNodes.Current.Name);
                        switch (aaNodes.Current.Name)
                        {
                            case "Id":
                                tmpAApp.Id = aaNodes.Current.Value;
                                break;

                            case "PlayCount":
                                tmpAApp.PlayCount = int.Parse(aaNodes.Current.Value);
                                break;

                            case "GameID":
                                tmpAApp.GameID = aaNodes.Current.Value;
                                break;

                            case "ApplicationPath":
                                tmpAApp.ApplicationPath = aaNodes.Current.Value;
                                break;

                            case "AutoRunAfter":
                                tmpAApp.AutoRunAfter = bool.Parse(aaNodes.Current.Value);
                                break;

                            case "AutoRunBefore":
                                tmpAApp.AutoRunBefore = bool.Parse(aaNodes.Current.Value);
                                break;

                            case "CommandLine":
                                tmpAApp.CommandLine = aaNodes.Current.Value;
                                break;

                            case "Name":
                                tmpAApp.Name = aaNodes.Current.Value;
                                break;

                            case "UseDosBox":
                                tmpAApp.UseDosBox = bool.Parse(aaNodes.Current.Value);
                                break;

                            case "UseEmulator":
                                tmpAApp.UseEmulator = bool.Parse(aaNodes.Current.Value);
                                break;

                            case "WaitForExit":
                                tmpAApp.WaitForExit = bool.Parse(aaNodes.Current.Value);
                                break;

                            case "Developer":
                                tmpAApp.Developer = aaNodes.Current.Value;
                                break;

                            case "Publisher":
                                tmpAApp.Publisher = aaNodes.Current.Value;
                                break;

                            case "Region":
                                tmpAApp.Region = aaNodes.Current.Value;
                                break;

                            case "Version":
                                tmpAApp.Version = aaNodes.Current.Value;
                                break;

                            case "Status":
                                tmpAApp.Status = aaNodes.Current.Value;
                                break;

                            case "EmulatorId":
                                tmpAApp.EmulatorId = aaNodes.Current.Value;
                                break;

                            case "SideA":
                                tmpAApp.SideA = bool.Parse(aaNodes.Current.Value);
                                break;

                            case "SideB":
                                tmpAApp.SideB = bool.Parse(aaNodes.Current.Value);
                                break;

                            case "Priority":
                                tmpAApp.Priority = int.Parse(aaNodes.Current.Value);
                                break;

                            default:
                                Console.WriteLine($"Cas non traité: {aaNodes.Current.Name}");
                                break;
                        }

                    } while (aaNodes.Current.MoveToNext());

                    backGame.AdditionalApplications.Add(tmpAApp);
                }

            }

            // Get addionnal fields
            XPathNodeIterator cfNodes = nav.Select(nav.Compile($"//LaunchBox/CustomField[GameID='{ID}']"));
            if (cfNodes.Count != 0)
            {
                //backGame.CustomFields = new List<CustomField>();

                // Loop on custom fields
                while (cfNodes.MoveNext())
                {
                    cfNodes.Current.MoveToFirstChild();
                    var tmpCField = new CustomField();

                    do
                    {
                        Debug.WriteLine("addionnal fields" + cfNodes.Current.Name);

                        switch (cfNodes.Current.Name)
                        {
                            case "GameID":
                                tmpCField.GameID = cfNodes.Current.Value;
                                break;

                            case "Name":
                                tmpCField.Name = cfNodes.Current.Value;
                                break;

                            case "Value":
                                tmpCField.Value = cfNodes.Current.Value;
                                break;
                        }
                    } while (cfNodes.Current.MoveToNext());

                    backGame.CustomFields.Add(tmpCField);
                }
            }

            return backGame;
        }
        */




        /// <summary>
        /// Convert a platform in object (partiel)
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        /*2021
        [Obsolete]
        public Platform ScrapPlatform(string Name)
        {
            Platform zePlatform = new Platform();
            zePlatform.Name = Name;


            XPathNavigator nav = _XDoc.CreateNavigator();

            // Infos
            XPathNodeIterator nodInfos = nav.Select(nav.Compile($"//LaunchBox/Platform[Name='{Name}']"));
            if (nodInfos.Count != 0)
            {
                nodInfos.MoveNext();
                nodInfos.Current.MoveToFirstChild();
                do
                {
                    switch (nodInfos.Current.Name)
                    {
                        // Correction sur le xml originel
                        case "Folder":
                            /*2020 n'était jamais utilisé, une erreur en prime car pas ajouté à la liste
                            PlatformFolder pfFolder = new PlatformFolder();
                            pfFolder.FolderPath = nodInfos.Current.Value;
                            pfFolder.MediaType = "AppPath";*/

        /*2021
                            zePlatform.FolderPath = nodInfos.Current.Value;
                            break;
                    }
                } while (nodInfos.Current.MoveToNext());
            }

            // Get path
            XPathNodeIterator nodPaths = nav.Select(nav.Compile($"//LaunchBox/PlatformFolder[Platform='{Name}']"));

            if (nodPaths.Count != 0)
            {

                while (nodPaths.MoveNext())
                {
                    //Console.WriteLine(nodIImages.Current.Name);
                    nodPaths.Current.MoveToFirstChild();
                    PlatformFolder pfFolder = new PlatformFolder();

                    do
                    {
                        //Console.WriteLine($"\t{nodIImages.Current.Name} = {nodIImages.Current.Value}");

                        switch (nodPaths.Current.Name)
                        {
                            case "MediaType":
                                pfFolder.MediaType = nodPaths.Current.Value;
                                break;

                            case "FolderPath":
                                pfFolder.FolderPath = nodPaths.Current.Value;
                                break;
                        }

                    } while (nodPaths.Current.MoveToNext());

                    zePlatform.PlatformFolders.Add(pfFolder);
                }


                //nodIImages.MoveNext();


            }



            return zePlatform;
        }2021*/
    }
}