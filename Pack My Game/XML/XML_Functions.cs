using Pack_My_Game.Container;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using Pack_My_Game.Pack;

namespace Pack_My_Game.XML
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

    class XML_Functions
    {
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
                MessageBox.Show($"'{_XmlFile}' not found", "Error - list of machines", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }


        /// <summary>
        /// Lis le fichier xml dédié pour retourner la liste des plateformes - Read dedicated xml file to get the list of platforms
        /// </summary>
        /// <param name="lMachines"></param>
        internal void ListMachine(List<string> lMachines)
        {
            XPathNavigator nav = _XDoc.CreateNavigator();
            XPathNodeIterator nodes = nav.Select(nav.Compile("//LaunchBox/Platform"));

            if (nodes.Count != 0)
            {

                while (nodes.MoveNext())
                {
                    Console.WriteLine(nodes.Current.Name);
                    nodes.Current.MoveToFirstChild();

                    while (nodes.Current.MoveToNext())
                    {
                        if (nodes.Current.Name == "Name")
                        {
                            Console.WriteLine(nodes.Current.Value);
                            lMachines.Add(nodes.Current.Value);
                        }
                    }
                }
            }

            // return lMachines;
        }

        /// <summary>
        /// Lis le fichier xml dédié pour retourner la liste des jeux - Read dedicated xml file to get the list of games
        /// </summary>
        /// <param name="lGames"></param>
        internal Dictionary<string, ShortGame> ListGames()
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
                                zeGame.Title = StringOp.Convert(nodes.Current.Value);
                                break;

                            case "ID":
                                Console.WriteLine(nodes.Current.Value);
                                zeGame.ID = nodes.Current.Value;
                                break;

                            case "ApplicationPath":
                                Console.WriteLine(nodes.Current.Value);
                                zeGame.FileName = Path.GetFileName(nodes.Current.Value);
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

                    lGames.Add(zeGame.ID, zeGame);
                }

            }
            return lGames;
        }
     
        /// <summary>
        /// Lis le fichier xml dédié pour retourner la liste des clones - Read dedicated xml file to get the list of clones
        /// </summary>
        /// <param name="lGames"></param>
        internal void ListClones(List<Clone> lClones, string ID)
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
                                zeClone.GameID = nodes.Current.Value;
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
        internal Game ScrapGame(string ID)
        {
            Game zeGame = null;

            XPathNavigator nav = _XDoc.CreateNavigator();
            Console.WriteLine($"//LaunchBox/Game[ID='{ID}']");
            // ID pour les games principaux et Id pour les secondaires
            XPathNodeIterator nodes = nav.Select(nav.Compile($"//LaunchBox/Game[ID='{ID}']"));


            Console.WriteLine($"{nodes.Count} trouvés avec {ID}");

            if (nodes.Count != 0)
            {
                zeGame = new Game();

                nodes.MoveNext();

                Console.WriteLine(nodes.Current.Name);
                nodes.Current.MoveToFirstChild();

                zeGame.ID = ID;

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

                            zeGame.ID = nodes.Current.Value;
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
                            zeGame.ReleaseDate = nodes.Current.Value;
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
                            break;
                        #endregion

                        default:
                            Console.WriteLine();
                            break;

                    }
                } while (nodes.Current.MoveToNext());

            }


            return zeGame;
        }


        /// <summary>
        /// Convert a platform in object
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        internal Platform ScrapPlatform(string Name)
        {
            Platform zePlatform = new Platform();
            zePlatform.Name = Name;

            XPathNavigator nav = _XDoc.CreateNavigator();

            // Get Images path
            XPathNodeIterator nodIImages = nav.Select(nav.Compile($"//LaunchBox/PlatformFolder[Platform='{Name}']"));

            if (nodIImages.Count != 0)
            {

                while (nodIImages.MoveNext())
                {
                    //Console.WriteLine(nodIImages.Current.Name);
                    nodIImages.Current.MoveToFirstChild();
                    PlatformFolder pfFolder = new PlatformFolder();


                    do
                    {
                        //Console.WriteLine($"\t{nodIImages.Current.Name} = {nodIImages.Current.Value}");

                        switch (nodIImages.Current.Name)
                        {
                            case "MediaType":
                                pfFolder.MediaType = nodIImages.Current.Value;
                                break;
                            case "FolderPath":
                                pfFolder.FolderPath = nodIImages.Current.Value;
                                break;
                        }

                    } while (nodIImages.Current.MoveToNext());

                    zePlatform.PlatformFolders.Add(pfFolder);
                }

                //nodIImages.MoveNext();


            }



            return zePlatform;
        }
    }
}