using Hermes;
using Pack_My_Game.Language;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Pack_My_Game.XML
{
    /// <summary>
    /// Create xml language files
    /// </summary>
    class XMLLang
    {
        private const string _Language = "Language";
        private const string _LangAttr = "Lang";
        private const string _VersionAttr = "Version";
        private const string _Name = "Name";
        private const string _Value = "Value";

        public static void Make_ENVersion()
        {
            XElement xelRoot = new XElement(_Language,
                );

            xelRoot.Add(
   
                       

            xelRoot.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.LangFolder, "Lang.en-EN.xml"));
        }




        internal static LangContent ShortLoad(string xmlFile)
        {
            XElement xelRoot = XElement.Load(xmlFile);
            LangContent lang = new LangContent();

            lang.Lang = xelRoot.Attribute(_LangAttr).Value;
            lang.Version = xelRoot.Attribute(_VersionAttr).Value;
            lang.Name = xelRoot.Attribute(_Name).Value;

            return lang;
        }


        /// <summary>
        /// Load Language File
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        internal static LangContent Load(string xmlFile)
        {

            XElement xelRoot = XElement.Load(xmlFile);
            LangContent lang = new LangContent();

            lang.Lang = xelRoot.Attribute(_LangAttr).Value;
            lang.Version = xelRoot.Attribute(_VersionAttr).Value;

            HeTrace.WriteLine($"Language {lang.Lang} - {lang.Version}, loading...");

            foreach (var element in xelRoot.Descendants())
            {
                switch (element.Name.LocalName)
                {
                    case "Add":
                        lang.AddExp = element.Attribute(_Value).Value;
                        break;

                    case "CCopy":
                        lang.CCopyExp = element.Attribute(_Value).Value;
                        break;

                    case "Ch_Games":
                        lang.Ch_Games = element.Attribute(_Value).Value;
                        break;

                    case "Ch_LBPath":
                        lang.Ch_LBPath = element.Attribute(_Value).Value;
                        break;

                    case "Ch_Path":
                        lang.Ch_Path = element.Attribute(_Value).Value;
                        break;

                    case "Ch_System":
                        lang.Ch_System = element.Attribute(_Value).Value;
                        break;

                    case "Cancel":
                        lang.CancelExp = element.Attribute(_Value).Value;
                        break;

                    case "Cheats":
                        lang.CheatsExp = element.Attribute(_Value).Value;
                        break;

                    case "CheatsCopy":
                        lang.Copy_Cheats = element.Attribute(_Value).Value;
                        break;

                    case "Compression":
                        lang.CompExp = element.Attribute(_Value).Value;
                        break;

                    case "Comp_LBMin":
                        lang.CompLBMin = element.Attribute(_Value).Value;
                        break;

                    case "Comp_LBMoy":
                        lang.CompLBMoy = element.Attribute(_Value).Value;
                        break;

                    case "Comp_LBMax":
                        lang.CompLBMax = element.Attribute(_Value).Value;
                        break;

                    case "Comp_Z":
                        lang.CompZExp = element.Attribute(_Value).Value;
                        break;

                    case "Comp_7Z":
                        lang.Comp7ZExp = element.Attribute(_Value).Value;
                        break;

                    case "CheatsPath":
                        lang.CheatsPathExp = element.Attribute(_Value).Value;
                        break;

                    case "CompLvl":
                        lang.LvlCompExp = element.Attribute(_Value).Value;
                        break;

                    case "Config":
                        lang.ConfigExp = element.Attribute(_Value).Value;
                        break;

                    case "Credits":
                        lang.Credits = element.Attribute(_Value).Value;
                        break;

                    case "Delete":
                        lang.DeleteExp = element.Attribute(_Value).Value;
                        break;

                    case "EnXB":
                        lang.EnXBExp = element.Attribute(_Value).Value;
                        break;

                    case "File":
                        lang.FileExp = element.Attribute(_Value).Value;
                        break;
                    case "FileExists":
                        lang.FileEExp = element.Attribute(_Value).Value;
                        break;

                    case "FilesFound":
                        lang.FilesFoundExp = element.Attribute(_Value).Value;
                        break;

                    case "FolderExists":
                        lang.FolderEEXp = element.Attribute(_Value).Value;
                        break;

                    case "Games":
                        lang.GamesExp = element.Attribute(_Value).Value;
                        break;

                    case "General":
                        lang.GeneralExp = element.Attribute(_Value).Value;
                        break;

                    case "Help":
                        lang.Help = element.Attribute(_Value).Value;
                        break;

                    case "Infos":
                        lang.InfExp = element.Attribute(_Value).Value;
                        break;

                    case "InfosG":
                        lang.InfosGExp = element.Attribute(_Value).Value;
                        break;

                    case "L_Games":
                        lang.ListGamesExp = element.Attribute(_Value).Value;
                        break;

                    case "LBPath":
                        lang.LBPathExp = element.Attribute(_Value).Value;
                        break;

                    case "Loggers":
                        lang.LoggersExp = element.Attribute(_Value).Value;
                        break;

                    case "Main":
                        lang.MainExp = element.Attribute(_Value).Value;
                        break;

                    case "Manuals":
                        lang.ManualsExp = element.Attribute(_Value).Value;
                        break;

                    case "Musics":
                        lang.MusicsExp = element.Attribute(_Value).Value;
                        break;

                    case "OK":
                        lang.OkExp = element.Attribute(_Value).Value;
                        break;

                    case "Options":
                        lang.OptionsExp = element.Attribute(_Value).Value;
                        break;

                    case "OriXB":
                        lang.OriXBExp = element.Attribute(_Value).Value;
                        break;

                    case "Params":
                        lang.ParamsExp = element.Attribute(_Value).Value;
                        break;

                    case "Paths":
                        lang.PathsExp = element.Attribute(_Value).Value;
                        break;

                    case "Process":
                        lang.ProcessExp = element.Attribute(_Value).Value;
                        break;

                    case "Q_Pack":
                        lang.Q_Pack = element.Attribute(_Value).Value;
                        break;

                    case "Region":
                        lang.RegionExp = element.Attribute(_Value).Value;
                        break;

                    case "Result":
                        lang.ResultExp = element.Attribute(_Value).Value;
                        break;

                    case "S_Language":
                        lang.S_Language = element.Attribute(_Value).Value;
                        break;

                    case "Select":
                        lang.Select = element.Attribute(_Value).Value;
                        break;

                    case "Selected":
                        lang.SelectedExp = element.Attribute(_Value).Value;
                        break;


                    case "Submit":
                        lang.SubmitExp = element.Attribute(_Value).Value;
                        break;


                    case "Title":
                        lang.TitleExp = element.Attribute(_Value).Value;
                        break;

                    case "TViewFile":
                        lang.TViewFExp = element.Attribute(_Value).Value;
                        break;

                    case "TwoRulz":
                        lang.Rulz = element.Attribute(_Value).Value;
                        break;

                    case "Version":
                        lang.VersionExp = element.Attribute(_Value).Value;
                        break;

                    case "Videos":
                        lang.VideosExp = element.Attribute(_Value).Value;
                        break;

                    case "WPath":
                        lang.WPathExp = element.Attribute(_Value).Value;
                        break;

                    case "Window":
                        lang.WindowExp = element.Attribute(_Value).Value;
                        break;


                    default:
                        HeTrace.WriteLine($"\t{element.Name.LocalName} is not supported");
                        break;

                }
            }

            return lang;
        }
    }
}
