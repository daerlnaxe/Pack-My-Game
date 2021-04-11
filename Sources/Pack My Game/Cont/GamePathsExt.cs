using Common_PMG.Container;
using Common_PMG.Container.Game;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pack_My_Game.Cont
{
    class GamePathsExt:GamePaths
    {

        public List<string> CompApps { get; internal set; } = new List<string>();
        public List<string> CompManuals { get; internal set; } = new List<string>();
        public List<string> CompMusics { get; internal set; } = new List<string>();
        public List<string> CompVideos { get; internal set; } = new List<string>();

        public List<string> CompCheatCodes { get; internal set; } = new List<string>();
        public List<PackFile> Images { get; internal set; } = new List<PackFile>();

        /// <summary>
        /// N'utilise pas les chemins
        /// </summary>
        /// <param name="lbGame"></param>
        /// <returns></returns>
        internal static GamePathsExt CreateBasic(LBGame lbGame)
        {
            return new GamePathsExt()
            {
                    Id = lbGame.Id,
                    Title = lbGame.Title,
                    Platform = lbGame.Platform,
            };
        }

        internal static void AddWVerif(string fichier, List<string> liste)
        {
            bool contains = false;
            foreach (var fr in liste)
                if (fr.Equals(fichier))
                {
                    contains = true;
                    break;
                }


            if (!contains)
                liste.Add(fichier);
        }

        internal static void Add(DataRep filerep, List<DataRep> liste)
        {
            liste.Add(filerep);
        }
    }
}
