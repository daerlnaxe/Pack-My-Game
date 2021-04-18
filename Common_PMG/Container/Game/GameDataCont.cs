using Common_PMG.Container;
using Common_PMG.Container.Game;
using Common_PMG.Container.Game.LaunchBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common_PMG.Container.Game
{
    /// <summary>
    /// Contient la liste des fichiers à transférer pour un jeu (plus rien à voir avec le GamePaths)
    /// </summary>
    public sealed class GameDataCont
    {
        public string Title { get; set; }
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


        public string SetDefaultApplication
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var dr = DataRep.MakeChosen(value);
                    Apps.Add(dr);
                    DefaultApp = dr;
                }
            }
        }

        public string SetDefaultManual
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

        public string SetDefaultMusic
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
        public string SetDefaultVideo
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

        public string SetDefaultThemeVideo
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


        public List<string> SetApplications
        {
            set
            {
                foreach(var f in value)
                {
                    AddWVerif(f, Apps);
                }
            }
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

        public void Reinitialize( List<DataRep> list, IEnumerable<DataRep> collection, params KeyValuePair<string, DataRep>[] defauts)
        {
            list.Clear();
            foreach (var elem in collection)
                list.Add(elem);

            foreach(var action in defauts)
            {
                this.GetType().GetProperty(action.Key).SetValue(this, action.Value);
            }
                //
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

        // ---
        /*
        public static explicit operator GameDataCont(GamePaths v)
        {
            GameDataCont GDC = new GameDataCont(v.Title);
            GDC.SetDefaultApplication = v.ApplicationPath;
            GDC.SetDefaultManual = v.ManualPath;
            GDC.SetDefaultMusic = v.MusicPath;
            GDC.SetDefaultVideo = v.VideoPath;
            GDC.SetDefaultThemeVideo = v.ThemeVideoPath;

            return GDC;

        }*/

    }
}
