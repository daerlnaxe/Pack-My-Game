using Common_PMG.Container.Game;
using Common_PMG.Container.Game.LaunchBox;
using Common_PMG.XML;
using DxTBoxCore.Common;
using DxTBoxCore.Box_MBox;
using Pack_My_Game.IHM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using static Pack_My_Game.Common;
using Common_Graph;
using System.Linq;

namespace Pack_My_Game.Core
{
    class LaunchBoxFunc
    {

        internal static void CheckGamesValidity(ObservableCollection<ShortGame> selectedGames, string selectedPlatform)
        {
            string platformXmlFile = Path.Combine(Config.LaunchBoxPath, Config.PlatformsFolder, $"{selectedPlatform}.xml");

            //IEnumerable<ShortGame> filteredGames = new List<ShortGame>(selectedGames);
            foreach (var g in selectedGames.ToList())
            {

                Dictionary<string, bool?> res = CheckGameValidity(g, platformXmlFile);

                bool? keepit = true;

                // Erreur sur le chemin principal
                if (res == null)
                {
                    DxMBox.ShowDial($"Main link application is broken '{g.Title}'");

                    keepit = false;
                }
                else
                {
                    bool test = true;
                    // Vérification si c'est utile d'afficher la fenêtre
                    foreach (var kvp in res)
                        test &= (bool)kvp.Value;

                    if (test == false)
                        keepit = SafeBoxes.ShowStatus($"Game status for '{g.Title}'.\nDo you want to keep it ?", "Game status", res);
                    
                }

                if (keepit != true)
                {
                    selectedGames.Remove(g);
                    g.IsSelected = false;
                    continue;
                }
            }
        }

        internal static Dictionary<string, bool?> CheckGameValidity(ShortGame g, string platformXmlFile)
        {
            LBGame game = XML_Games.Scrap_LBGame<LBGame>(platformXmlFile, GameTag.ID, g.Id);

            //clones
            var clones = XML_Games.ListClones(platformXmlFile, Tag.GameId, g.Id);

            bool bMG = CheckValidity(game.ApplicationPath);

            if (!bMG)
                return null;

            Dictionary<string, bool?> yEP = new Dictionary<string, bool?>();
            yEP.Add("Main Game", bMG);
            yEP.Add("Manual", CheckValidity(game.ManualPath));
            yEP.Add("Music", CheckValidity(game.MusicPath));
            yEP.Add("Video", CheckValidity(game.VideoPath));
            yEP.Add("ThemeVideo", CheckValidity(game.ThemeVideoPath));

            foreach (var c in clones)
                yEP.Add(Path.GetFileName(c.ApplicationPath), CheckValidity(c.ApplicationPath));

            return yEP;

            /// <summary>
            ///  Vérifie que le jeu sélectionné a bien le manuel, la musique et le jeu
            /// </summary>
            bool CheckValidity(string link)
            {
                string tmp;

                tmp = Path.GetFullPath(link, Config.LaunchBoxPath);
                if (!File.Exists(tmp))
                    return false;

                return true;
            }
        }


        /*
        [Obsolete]
        internal static string? CheckGameValidity(GamePaths game)
        {
            string res = null;
            if (!CheckValidity(game.Applications[0]?.CurrentPath))
                res += $"Game link broken or null\r\n";

            if (!CheckValidity(game.ManualPath))
                res += $"Manual link broken or null \r\n";

            if (!CheckValidity(game.MusicPath))
                res += $"Music link broken or null \r\n";

            return res;
        }
*/



    }
}
