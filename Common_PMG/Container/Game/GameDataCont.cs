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

        public List<DataPlus> Apps { get; private set; } = new List<DataPlus>();
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



        public DataPlus DefaultApp { get;          private set;        }
        public DataRep DefaultManual { get; private set; }
        public DataRep DefaultMusic { get; private set; }
        public DataRep DefaultVideo { get; private set; }
        public DataRep DefaultThemeVideo { get; private set; }

        /*
        public string SetDefaultApplication
        {
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var dr = DataPlus.MakeChosen(value);
                    Apps.Add((DataPlus)dr);
                    DefaultApp = dr;
                }
            }
        }*/

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
        public void UnsetDefaultManual()
        {
            Manuals.Remove(DefaultManual);
            DefaultManual = null;
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

        public void UnsetDefaultMusic()
        {
            Manuals.Remove(DefaultMusic);
            DefaultMusic = null;
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

        public void UnsetDefaultVideo()
        {
            Videos.Remove(DefaultVideo);
            DefaultVideo = null;
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
        public void UnsetDefaultThemeVideo()
        {
            Videos.Remove(DefaultThemeVideo);
            DefaultThemeVideo = null;
        }

        // ---

        public GameDataCont(string title)
        {
            Title = title;
        }

        // ---

        /*
        public void AddApplication(string fichier)
        {
            AddWVerif(fichier, Apps);
        }
        */




        // ---

        public List<DataPlus> SetApplications
        {
            set
            {
                foreach (DataPlus app in value)
                {
                    if (app.IsSelected)
                        DefaultApp = app;
                }

                Apps = value;

            }

        }
        public List<string> SSetApplications
        {
            set
            {
                foreach (var f in value)
                {
                    //if(Apps.FirstOrDefault(x => x.CurrentPath.Equals(f) )==null))
                    AddWVerif(DataPlus.MakeNormal(f), Apps);
                }
            }
        }

        public void RemoveApp(DataPlus app)
        {
            if (app == DefaultApp)
                DefaultApp = null;

            Apps.Remove(app);
        }



        public List<DataRep> SetCheatCodes
        {
            set
            {
                foreach (var f in value)
                {
                    AddWVerif(f, CheatCodes);
                }
            }
        }
        public List<string> SSetCheatCodes
        {
            set
            {
                foreach (var f in value)
                {
                    AddWVerif(f, CheatCodes);
                }
            }
        }

        public List<DataRep> SetManuals
        {
            set
            {
                foreach (var f in value)
                {
                    AddWVerif(f, Manuals);
                }
            }
        }
        public List<string> SSetManuals
        {
            set
            {
                foreach (var f in value)
                {
                    AddWVerif(f, Manuals);
                }
            }
        }


        public List<DataRep> SetMusics
        {
            set
            {
                foreach (var f in value)
                {
                    AddWVerif(f, Musics);
                }
            }
        }
        public List<string> SSetMusics
        {
            set
            {
                foreach (var f in value)
                {
                    AddWVerif(f, Musics);
                }
            }
        }

        public List<DataRep> SetVideos
        {
            set
            {
                foreach (var f in value)
                {
                    AddWVerif(f, Videos);
                }
            }
        }
        public List<string> SSetVideos
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






        private void AddWVerif<T>(T fichier, List<T> liste) where T : IData
        {
            foreach (var fr in liste)
                if (fr.CurrentPath.Equals(fichier.CurrentPath))
                    return;


            liste.Add(fichier);
        }

        private void AddWVerif(string fichier, List<DataRep> liste)
        {
            foreach (var fr in liste)
                if (fr.CurrentPath.Equals(fichier))
                    return;

            liste.Add(DataRep.MakeNormal(fichier));
        }


            /// <summary>
            /// Reinitialize a list with a collection
            /// </summary>
            /// <param name="list"></param>
            /// <param name="collection"></param>
            /// <param name="defauts"></param>
            public void Reinitialize(List<DataRep> list, IEnumerable<DataRep> collection)
            {
                list.Clear();
                foreach (var elem in collection)
                    list.Add(elem);
                //
            }

            public void SetDefault(string propertyName, DataRep value)
            {
                this.GetType().GetProperty(propertyName).SetValue(this, value);
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


        public static explicit operator GameDataCont(GamePaths v)
        {
            GameDataCont GDC = new GameDataCont(v.Title);
            GDC.SetApplications = v.Applications;
            //GDC.SetDefaultApplication = v.Applications.Select


            GDC.SetDefaultManual = v.ManualPath;
            GDC.SetDefaultMusic = v.MusicPath;
            GDC.SetDefaultVideo = v.VideoPath;
            GDC.SetDefaultThemeVideo = v.ThemeVideoPath;

            return GDC;

        }


    }
}
