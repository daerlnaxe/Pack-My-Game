using Common_PMG.Container;
using Common_PMG.Container.Game;
using Common_PMG.Container.Game.LaunchBox;
using Hermes;
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

        private List<DataPlus> _Applications = new List<DataPlus>();
        public IReadOnlyCollection<DataPlus> Applications => _Applications.AsReadOnly();

        private List<DataRep> _Manuals /* set; */ = new List<DataRep>();
        public IReadOnlyList<DataRep> Manuals => _Manuals.AsReadOnly();

        public List<DataRep> Musics { get; /*set;*/ } = new List<DataRep>();
        public List<DataRep> Videos { get; /*set;*/ } = new List<DataRep>();
        public List<DataRep> CheatCodes { get; /*set;*/ } = new List<DataRep>();
        public List<DataRepExt> Images { get; set; } = new List<DataRepExt>();

        public int NumberofFiles => Applications.Count() +
                                    Manuals.Count() +
                                    Musics.Count() +
                                    Videos.Count() +
                                    CheatCodes.Count() +
                                    Images.Count();

        public DataPlus DefaultApp { get; private set; }
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
                    _Manuals.Add(dr);
                    DefaultManual = dr;
                }
            }
        }
        public void UnsetDefaultManual()
        {
            _Manuals.Remove(DefaultManual);
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
            Musics.Remove(DefaultMusic);
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

        // ---

        /*
        public void AddApplication(string fichier)
        {
            AddWVerif(fichier, Apps);
        }
        */

        // ---

        /// <summary>
        /// Initialize or replace applications
        /// </summary>
        /// <remarks>
        /// Initialize Default
        /// </remarks>
        public IEnumerable<DataPlus> SetApplications
        {
            set
            {
                if (value != null)
                {
                    _Applications.Clear();
                    foreach (DataPlus app in value)
                    {
                        if (app.IsSelected)
                            DefaultApp = app;

                        AddWVerif(app, _Applications);
                    }
                }
            }
        }

        public IEnumerable<string> SSetApplications
        {
            set
            {
                if (value != null)
                    foreach (var f in value)
                        AddWVerif(DataPlus.MakeNormal(f), _Applications);
            }
        }


        public List<DataRep> SetCheatCodes
        {
            set
            {
                if (value != null)
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

        /// <summary>
        /// Initialize or replace manuals
        /// </summary>
        public IEnumerable<DataRep> SetManuals
        {
            set
            {
                if (value != null)
                {
                    _Manuals.Clear();
                    foreach (var f in value)
                    {
                        AddWVerif(f, _Manuals);
                    }
                }
            }
        }
        public List<string> SSetManuals
        {
            set
            {
                foreach (var f in value)
                {
                    AddWVerif(f, _Manuals);
                }
            }
        }


        public List<DataRep> SetMusics
        {
            set
            {

                if (value == null)
                    return;

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
                if (value == null)
                    return;

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

        public GameDataCont(string title)
        {
            Title = title;
        }




        public void RemoveApp(DataPlus app)
        {
            if (app == DefaultApp)
                DefaultApp = null;

            _Applications.Remove(app);
        }


        /// <summary>
        /// Empêche les doublons et les path vides
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fichier"></param>
        /// <param name="liste"></param>
        private void AddWVerif<T>(T fichier, List<T> liste) where T : IData
        {
            if (string.IsNullOrEmpty(fichier.CurrentPath))
                return;

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
            HeTrace.WriteLine($"Cast {nameof(GameDataCont)} with {nameof(GamePaths)}");

            GameDataCont GDC = new GameDataCont(v.Title);
            GDC.SetApplications = v.Applications.Select(x => new DataPlus(x));
            //GDC.SetDefaultApplication = v.Applications.Select


            GDC.SetDefaultManual = v.ManualPath;
            GDC.SetDefaultMusic = v.MusicPath;
            GDC.SetDefaultVideo = v.VideoPath;
            GDC.SetDefaultThemeVideo = v.ThemeVideoPath;

            return GDC;

        }


    }
}
