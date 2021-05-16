using Common_PMG.Container;
using Common_PMG.Container.Game;
using DxTBoxCore.Box_MBox;
using Pack_My_Game.Files;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using static Pack_My_Game.Common;

namespace Pack_My_Game.Models
{
    /// <summary>
    /// Model de l'IHM de présentation des fichiers à copier
    /// </summary>
    public class M_PackMePrev : M_PackMe
    {

        #region Recherche
        private string _SearchString;
        public string SearchString
        {
            get => _SearchString;
            set
            {
                _SearchString = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Cheat suggéré selectionné
        /// </summary>
        public string EligibleCheatSelected { get; set; }

        /// <summary>
        /// Manuel suggéré selectionné
        /// </summary>
        public string EligibleManualSelected { get; set; }

        /// <summary>
        /// Musique suggérée sélectionnée
        /// </summary>
        public string EligibleMusicSelected { get; set; }

        /// <summary>
        /// Vidéo suggérée sélectionnée
        /// </summary>
        public string EligibleVideoSelected { get; set; }

        /// <summary>
        /// Liste de mots à chercher dans chaque fichier
        /// </summary>
        public ObservableCollection<string> WordsToSearch { get; set; } = new ObservableCollection<string>();


        /// <summary>
        /// Manuels trouvés par la recherche
        /// </summary>
        public ObservableCollection<string> EligibleCheats{ get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// Manuels trouvés par la recherche
        /// </summary>
        public ObservableCollection<string> EligibleManuals { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// Musiques trouvées par la recherche
        /// </summary>
        public ObservableCollection<string> EligibleMusics { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// Vidéos trouvées par la recherche
        /// </summary>
        public ObservableCollection<string> EligibleVideos { get; set; } = new ObservableCollection<string>();
        #endregion

        // ---

        public M_PackMePrev(ContPlatFolders platform, GameDataCont gdC) : base(Config.HLaunchBoxPath, platform, gdC)
        {
            _GamesPath = Path.GetFullPath(Platform.FolderPath, Config.HLaunchBoxPath);

            _CheatsPath = Config.HCCodesPath;

            var manTail = platform.PlatformFolders.First(x => x.MediaType.Equals("Manual")).FolderPath;
            if (!string.IsNullOrEmpty(manTail))
                _ManualsPath = Path.GetFullPath(manTail, Config.HLaunchBoxPath);

            var musTail = platform.PlatformFolders.First(x => x.MediaType.Equals("Music")).FolderPath;
            if (!string.IsNullOrEmpty(musTail))
                _MusicsPath = Path.GetFullPath(musTail, Config.HLaunchBoxPath);

            var vidTail = platform.PlatformFolders.First(x => x.MediaType.Equals("Video")).FolderPath;
            if (!string.IsNullOrEmpty(vidTail))
                _VideosPath = Path.GetFullPath(vidTail, Config.HLaunchBoxPath);

            Init();
        }

        public override void Init()
        {
            // Création des collections (par rapport au changement de nom
            MakeCollection(GameDataC.Applications, GamesCollection, _GamesPath);
            MakeCollection(GameDataC.CheatCodes, CheatsCollection, _CheatsPath);
            MakeCollection(GameDataC.Manuals, ManualsCollection, _ManualsPath);
            MakeCollection(GameDataC.Musics, MusicsCollection, _MusicsPath);
            MakeCollection(GameDataC.Videos, VideosCollection, _VideosPath);

            // Initialisation des fichiers par défaut.
            ChosenGame = GamesCollection.FirstOrDefault(x => x.CurrentPath.Equals(GameDataC.DefaultApp?.CurrentPath));
            ChosenManual = ManualsCollection.FirstOrDefault(x => x.CurrentPath.Equals(GameDataC?.DefaultManual?.CurrentPath));
            ChosenMusic = MusicsCollection.FirstOrDefault(x => x.CurrentPath.Equals(GameDataC.DefaultMusic?.CurrentPath));
            ChosenVideo = VideosCollection.FirstOrDefault(x => x.CurrentPath.Equals(GameDataC.DefaultVideo?.CurrentPath));
            ChosenThemeVideo = VideosCollection.FirstOrDefault(x => x.CurrentPath.Equals(GameDataC.DefaultThemeVideo?.CurrentPath));

            FillWords(GameDataC.Title);
            SearchFiles();
        }

        /// <summary>
        /// Initialize une collection avec une copie modifiée des datareps pour montrer l'arborescence dans le nom
        /// </summary>
        /// <param name="srcCollected"></param>
        /// <param name="targetedCollec"></param>
        /// <param name="mediatype"></param>
        protected void MakeCollection(IEnumerable<DataRep> srcCollected, ObservableCollection<DataRep> targetedCollec, string link)
        {
            targetedCollec.Clear();
            foreach (DataRep elem in srcCollected)
            {
                DataRep dr = DataRep.DataRepFactory(elem);
                dr.Name = dr.CurrentPath.Replace(link, ".");
                targetedCollec.Add(dr);
            }
        }

        /// <summary>
        /// Initialize une collection avec une copie modifiée des datareps pour montrer l'arborescence dans le nom
        /// </summary>
        /// <param name="srcCollected"></param>
        /// <param name="targetedCollec"></param>
        /// <param name="mediatype"></param>
        protected void MakeCollection(IEnumerable<DataPlus> srcCollected, ObservableCollection<DataPlus> targetedCollec, string link)
        {
            targetedCollec.Clear();
            foreach (DataPlus elem in srcCollected)
            {
                DataPlus dp = new DataPlus(elem);
                dp.Name = elem.CurrentPath.Replace(link, ".");
                targetedCollec.Add(dp);
            }
        }

        #region Recherche de fichiers supplémentaires

        internal void FillWords(string searchString)
        {
            var words = FilesFunc.GetDefiningWords(searchString);
            if (words != null)
                foreach (string word in words)
                {
                    bool res = false;
                    foreach (string refWord in WordsToSearch)
                    {
                        if (word.Equals(refWord, StringComparison.OrdinalIgnoreCase))
                        {
                            res = true;
                            break;
                        }
                    }

                    if (!res)
                        WordsToSearch.Add(word);
                }

            SearchString = null;
        }


        internal void RemoveWord(object tag)
        {
            WordsToSearch.Remove((string)tag);
        }

        internal void ResetSearch()
        {
            WordsToSearch.Clear();
        }
        internal void SearchFiles()
        {
            //SearchIn("");
            SearchIn("Manual", EligibleManuals, ManualsCollection);
            SearchIn("Music", EligibleMusics, MusicsCollection);
            SearchIn("Video", EligibleVideos, VideosCollection);

            void SearchIn(string mediatype, ObservableCollection<string> collection, ObservableCollection<DataRep> collectionRef)
            {
                PlatformFolder pFolder = Platform.PlatformFolders.FirstOrDefault((x) => x.MediaType == mediatype);
                if (pFolder == null)
                    return;

                string srcFolder = Path.GetFullPath(pFolder.FolderPath, Config.HLaunchBoxPath);

                if (!Directory.Exists(srcFolder))
                    return;

                collection.Clear();

                foreach (string f in Directory.EnumerateFiles(srcFolder, "*.*", SearchOption.AllDirectories))
                {
                    bool res = true;
                    foreach (string word in WordsToSearch)
                        res &= f.Contains(word, StringComparison.OrdinalIgnoreCase);

                    if (res && collectionRef.FirstOrDefault(x => x.CurrentPath.Equals(f)) == null)
                        collection.Add(f);
                }
            }

        }

        #endregion

        internal void AddCheat()
        {
            if (string.IsNullOrEmpty(EligibleCheatSelected))
                return;

            if (CheckCollection(CheatsCollection, EligibleCheatSelected))
                return;

            DataRep tmp = new DataRep(EligibleManualSelected);
            tmp.Name = tmp.CurrentPath.Replace(_CheatsPath, ".");
            CheatsCollection.Add(tmp);
            EligibleCheats.Remove(EligibleCheatSelected);
        }



        /// <summary>
        /// Ajoute un manuel à la liste à copier
        /// </summary>
        internal void AddManual()
        {
            if (string.IsNullOrEmpty(EligibleManualSelected))
                return;

            if (CheckCollection(ManualsCollection, EligibleManualSelected))
                return;

            DataRep tmp = new DataRep(EligibleManualSelected);
            tmp.Name = tmp.CurrentPath.Replace(_ManualsPath, ".");
            ManualsCollection.Add(tmp);
            EligibleManuals.Remove(EligibleManualSelected);
        }

        internal void AddMusic()
        {
            if (string.IsNullOrEmpty(EligibleMusicSelected))
                return;

            if (CheckCollection(MusicsCollection, EligibleMusicSelected))
                return;

            DataRep tmp = new DataRep(EligibleMusicSelected);
            tmp.Name = tmp.CurrentPath.Replace(_MusicsPath, ".");
            MusicsCollection.Add(tmp);
            EligibleMusics.Remove(EligibleMusicSelected);
        }

        internal void AddVideo()
        {
            if (string.IsNullOrEmpty(EligibleVideoSelected))
                return;

            if (CheckCollection(VideosCollection, EligibleVideoSelected))
                return;

            DataRep tmp = new DataRep(EligibleVideoSelected);
            tmp.Name = tmp.CurrentPath.Replace(_VideosPath, ".");
            VideosCollection.Add(tmp);
            EligibleVideos.Remove(EligibleVideoSelected);
        }

        private bool CheckCollection(ObservableCollection<DataRep> Collection, string selected)
        {
            foreach (var elem in Collection)
                if (elem.CurrentPath.Equals(selected, StringComparison.OrdinalIgnoreCase))
                    return true;

            return false;
        }

        #region

        internal void RemoveCheat()
        {
            CheatsCollection.Remove(SelectedCheatFile);
        }


        /// <summary>
        /// Enlève un manuel de la liste à copier
        /// </summary>
        internal void RemoveManual()
        {
            if (!SelectedManual.IsSelected)
                ManualsCollection.Remove(SelectedManual);
            else
                DxMBox.ShowDial("Default file, you can't remove it for the while");
        }

        /// <summary>
        /// Enlève une musique de la liste à copier
        /// </summary>
        internal void RemoveMusic()
        {
            if (!SelectedMusic.IsSelected)
                MusicsCollection.Remove(SelectedMusic);
            else
                DxMBox.ShowDial("Default file, you can't remove it for the while");
        }

        /// <summary>
        /// Enlève une vidéo de la liste à copier
        /// </summary>
        internal void RemoveVideo()
        {
            if (!SelectedVideo.IsSelected)
                VideosCollection.Remove(SelectedVideo);
            else
                DxMBox.ShowDial("Default file, you can't remove it for the while");
        }

        #endregion


        internal bool Apply_Modifs()
        {
            this.Test_HasElement(GamesCollection, nameof(GamesCollection));

            if (HasErrors)
                return false;

            // Jeux
            GameDataC.SetApplications = GamesCollection;
            // Cheats
            GameDataC.SetDCheatCodes = CheatsCollection;
            // Manuals
            GameDataC.SetManuals = ManualsCollection;
            // Musics
            GameDataC.SetMusics = MusicsCollection;
            // Videos
            GameDataC.SetDefault(nameof(GameDataC.DefaultVideo), ChosenVideo);
            GameDataC.SetDefault(nameof(GameDataC.DefaultThemeVideo), ChosenThemeVideo);
            GameDataC.AddDVideos = VideosCollection;

            return true;
        }

    }
}
