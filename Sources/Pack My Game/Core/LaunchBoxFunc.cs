﻿using Common_PMG.Container.Game;
using Common_PMG.Container.Game.LaunchBox;
using DxTBoxCore.Common;
using DxTBoxCore.MBox;
using LaunchBox_XML.XML;
using Pack_My_Game.IHM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using Cst = LaunchBox_XML.Common;
using PS = Pack_My_Game.Properties.Settings;

namespace Pack_My_Game.Core
{
    class LaunchBoxFunc
    {

        internal static void CheckGamesValidity(ObservableCollection<ShortGame> selectedGames, string selectedPlatform)
        {
            string platformXmlFile = Path.Combine(PS.Default.LBPath, PS.Default.dPlatforms, $"{selectedPlatform}.xml");

            IEnumerable<ShortGame> filteredGames = new List<ShortGame>( selectedGames);
            foreach(var g in filteredGames)
            {
                
                GamePaths game = (GamePaths)XML_Games.Scrap_LBGame<LBGame>(platformXmlFile, Cst.Id, g.Id);
                string? res = CheckGameValidity(game);

                bool? keepit = true;
                if (res != null)
                    keepit = DxMBox.ShowDial($"Game '{g.Title}' is not 'complete', keep it ?", "Question", E_DxButtons.No | E_DxButtons.Yes, res);

                if (keepit != true)
                {
                    selectedGames.Remove(g);
                    g.IsSelected = false;
                    continue;
                }

            }           

            //return selectedGames;
        }


        internal static string? CheckGameValidity(GamePaths game)
        {
            string res = null;
            if (!CheckValidity(game.ApplicationPath))
                res += $"Game link broken or null\r\n";

            if (!CheckValidity(game.ManualPath))
                res += $"Manual link broken or null \r\n";

            if (!CheckValidity(game.MusicPath))
                res += $"Music link broken or null \r\n";

            return res;
        }

        /// <summary>
        ///  Vérifie que le jeu sélectionné a bien le manuel, la musique et le jeu
        /// </summary>
        internal static bool CheckValidity(string link)
        {
            string tmp;

            tmp = Path.GetFullPath(link, PS.Default.LBPath);
            if (!File.Exists(tmp))
                return false;

            return true;
        }


    }
}
