using Common_PMG.Container;
using Common_PMG.Container.Game;
using Common_PMG.Container.Game.LaunchBox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common_PMG.Container.Game
{
    /// <summary>
    /// Contient la liste des fichiers à transférer pour un jeu (plus rien à voir avec le GamePaths)
    /// </summary>
    public sealed class GameDataCont
    {

        public List<DataRep> Apps { get; } = new List<DataRep>();
        public List<DataRep> Manuals { get;/* set; */} = new List<DataRep>();
        public List<DataRep> Musics { get; /*set;*/ } = new List<DataRep>();
        public List<DataRep> Videos { get; /*set;*/ } = new List<DataRep>();
        public List<DataRep> CheatCodes { get; /*set;*/ } = new List<DataRep>();
        public List<DataRepExt> Images { get; set; } = new List<DataRepExt>();

        public int NumberofFiles => Apps.Count() +
                                    Manuals.Count() +
                                    Musics.Count() +
                                    Videos.Count() +
                                    CheatCodes.Count() +
                                    Images.Count();

        

        public DataRep DefaultApp { get; private set; }
        public DataRep DefaultManual { get; private set; }
        public DataRep DefaultMusic { get; private set; }
        public DataRep DefaultVideo { get; private set; }
        public DataRep DefaultThemeVideo { get; private set; }


        public string SetMainApplication
        {
            set
            {
                if (value != null)
                {
                    var dr = DataRep.MakeChosen(value);
                    Apps.Add(dr);
                    DefaultApp = dr;
                }
            }
        }

        public string SetManualPath
        {
            set
            {
                if (value != null)
                {
                    var dr = DataRep.MakeChosen(value);
                    Manuals.Add(dr);
                    DefaultManual = dr;
                }
            }
        }

        public string SetMusicPath
        {
            set
            {
                if (value != null)
                {
                    var dr = DataRep.MakeChosen(value);
                    Musics.Add(dr);
                    DefaultMusic = dr;
                }
            }
        }
        public string SetVideoPath
        {
            set
            {
                if (value != null)
                {
                    var dr = DataRep.MakeChosen(value);
                    Videos.Add(dr);
                    DefaultVideo = dr;
                }
            }
        }

        public string SetThemeVideoPath
        {
            set
            {
                if (value != null)
                {
                    var dr = DataRep.MakeChosen(value);
                    Videos.Add(dr);
                    DefaultThemeVideo = dr;
                }
            }
        }

        // ---

        public GameDataCont(string title)
        {
            Title = title;
        }

        // ---

        public void AddApplication(string fichier)
        {
            AddWVerif(fichier, Apps);
        }

        public List<string> SetCheatCodes
        {
            set
            {
                foreach (var f in value)
                {
                    AddWVerif(f, CheatCodes);
                }
            }
        }

        public List<string> SetManuals
        {
            set
            {
                foreach (var f in value)
                {
                    AddWVerif(f, Manuals);
                }
            }
        }

        public List<string> SetMusics
        {
            set
            {
                foreach (var f in value)
                {
                    AddWVerif(f, Musics);
                }
            }
        }

        public List<string> SetVideos
        {
            set
            {
                foreach (var f in value)
                {
                    AddWVerif(f, Videos);
                }
            }
        }

        public string Title { get; }







        /*
        public GameDataCont(GamePaths g)
        {
            this.Id = g.Id;
            this.Title = g.Title;
            this.Platform = g.Platform;
            this.ApplicationPath = g.ApplicationPath;
            this.ManualPath = g.ManualPath;
            this.MusicPath = g.MusicPath;
            this.VideoPath = g.VideoPath;
            this.ThemeVideoPath = g.ThemeVideoPath;
        }*/


        private void AddWVerif(string fichier, List<DataRep> liste)
        {
            bool contains = false;
            foreach (var fr in liste)
                if (fr.ALinkToThePath.Equals(fichier))
                {
                    contains = true;
                    break;
                }


            if (!contains)
            {
                liste.Add(DataRep.MakeNormal(fichier));
            }
        }

        [Obsolete]
        internal static void Add(DataRep filerep, List<DataRep> liste)
        {
            liste.Add(filerep);
        }


        /*  public static explicit operator GameDataCont(LBGame v)
          {
              return new GameDataCont()
              {
                  Id = v.Id,
                  Title = v.Title,
                  Platform = v.Platform,
                  ApplicationPath = v.ApplicationPath,
                  ManualPath = v.ManualPath,
                  MusicPath = v.MusicPath,
                  VideoPath = v.VideoPath,
                  ThemeVideoPath = v.ThemeVideoPath,
              };
          }*/
    }
}
