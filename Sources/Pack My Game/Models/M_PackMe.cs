using Common_PMG.Container;
using Common_PMG.Container.Game;
using Common_PMG.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Pack_My_Game.Models
{
    /// <summary>
    /// Classe commune pour M_PackMePrev & M_PackMeRes
    /// </summary>
    public abstract class M_PackMe : A_Err
    {
        #region Selected
        /// <summary>
        /// Cheat code sélectionné
        /// </summary>
        public DataRep SelectedCheatFile { get; set; }

        /// <summary>
        /// Manuel sélectionné
        /// </summary>
        public DataRep SelectedManual { get; set; }

        /// <summary>
        /// Musique sélectionnée
        /// </summary>
        public DataRep SelectedMusic { get; set; }

        /// <summary>
        /// Vidéo sélectionnée
        /// </summary>
        public DataRep SelectedVideo { get; set; }
        #endregion


        #region Chosen
        DataPlus _ChosenGame;
        public DataPlus ChosenGame
        {
            get => _ChosenGame;
            set
            {
                _ChosenGame = value;
                OnPropertyChanged();
            }
        }

        //public DataRep ChosenCheatF { get; set; }


        DataRep _ChosenManual;
        public DataRep ChosenManual
        {
            get => _ChosenManual;
            set
            {
                _ChosenManual = value;
                OnPropertyChanged();
            }
        }


        DataRep _ChosenMusic;
        public DataRep ChosenMusic
        {
            get => _ChosenMusic;
            set
            {
                _ChosenMusic = value;
                OnPropertyChanged();
            }
        }



        DataRep _ChosenVideo;
        public DataRep ChosenVideo
        {
            get => _ChosenVideo;
            set
            {
                _ChosenVideo = value;
                OnPropertyChanged();
            }
        }

        DataRep _ChosenThemeVideo;
        public DataRep ChosenThemeVideo
        {
            get => _ChosenThemeVideo;
            set
            {
                _ChosenThemeVideo = value;
                OnPropertyChanged();
            }
        }
        #endregion




        //public DataPlus SelectedGame { get; set; }
        /// <summary>
        /// Liste des cheats à copier
        /// </summary>
        public ObservableCollection<DataRep> CheatsCollection { get; set; } = new ObservableCollection<DataRep>();

        /// <summary>
        /// Liste des Jeux
        /// </summary>
        public ObservableCollection<DataPlus> GamesCollection { get; set; } = new ObservableCollection<DataPlus>();

        /// <summary>
        /// Liste des manuels
        /// </summary>
        public ObservableCollection<DataRep> ManualsCollection { get; set; } = new ObservableCollection<DataRep>();

        /// <summary>
        /// Liste des musiques
        /// </summary>
        public ObservableCollection<DataRep> MusicsCollection { get; set; } = new ObservableCollection<DataRep>();

        /// <summary>
        /// Liste des vidéos
        /// </summary>
        public ObservableCollection<DataRep> VideosCollection { get; set; } = new ObservableCollection<DataRep>();


        //  public string Root { get; private set; }
        protected string _CheatsPath;
        protected string _GamesPath;
        protected string _ManualsPath;
        protected string _MusicsPath;
        protected string _VideosPath;

        /// <summary>
        /// GameDataCont de référence
        /// </summary>
        public GameDataCont GameDataC { get; }

        /// <summary>
        /// Contient les chemins de la plateforme
        /// </summary>
        public ContPlatFolders Platform { get; }

        // ---

        /// <summary>
        /// Constructeur obligatoire
        /// </summary>
        /// <param name="platform"></param>
        public M_PackMe(ContPlatFolders platform, GameDataCont gdC)
        {
            Platform = platform;
            GameDataC = gdC;

        }

        /// <summary>
        /// Initialisation
        /// </summary>
        public abstract void Init();




        // ---

        #region ouvertures

        /// <summary>
        /// Ouverture d'un manuel
        /// </summary>
        internal void OpenManual(string manual)
        {
            if (string.IsNullOrEmpty(manual))
                return;

            //string path = Path.Combine(Root, Common.Manuals, SelectedManual);
            Process p = new Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = manual;
            p.Start();

        }

        /// <summary>
        /// Ouverture d'une musique
        /// </summary>
        internal void OpenMusic(string music)
        {
            if (string.IsNullOrEmpty(music))
                return;

            //string path = Path.Combine(Root, Common.Musics, SelectedMusic);
            Process p = new Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = music;
            p.Start();

        }

        /// <summary>
        /// Ouverture d'une vidéo
        /// </summary>
        internal void OpenVideo(string video)
        {
            if (string.IsNullOrEmpty(video))
                return;

            //string path = Path.Combine(Root, Common.Videos, SelectedVideo);
            Process p = new Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = video;
            p.Start();
        }

        /// <summary>
        /// Ouverture d'un cheat code dans l'éditeur par défaut
        /// </summary>
        internal void OpenCheatInDefaultEditor(string cheatF)
        {
            if (string.IsNullOrEmpty(cheatF))
                return;

            Process.Start(cheatF);
        }

        #endregion
    }
}
