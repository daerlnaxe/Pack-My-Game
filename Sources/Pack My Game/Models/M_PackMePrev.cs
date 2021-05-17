using Common_PMG.Container;
using Common_PMG.Container.Game;
using DxTBoxCore.Box_MBox;
using Pack_My_Game.Files;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using static Pack_My_Game.Common;

namespace Pack_My_Game.Models
{
    /// <summary>
    /// Model de l'IHM de présentation des fichiers à copier
    /// </summary>
    public class M_PackMePrev : M_PackMe
    {
        public DataRepImg ImageSelected { get; set; }
        public ObservableCollection<DataRepImg> ImagesCollection { get; set; } = new ObservableCollection<DataRepImg>();


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
        /// Image suggéré selectionné
        /// </summary>
        public DataRepImg EligibleImageSelected { get; set; }

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
        public ObservableCollection<string> EligibleCheats { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// Images trouvés par la recherche
        /// </summary>
        public ObservableCollection<DataRepImg> EligibleImages { get; set; } = new ObservableCollection<DataRepImg>();


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
        public int FilesFound => EligibleCheats.Count() +
                                    EligibleImages.Count() +
                                    EligibleManuals.Count +
                                    EligibleMusics.Count() +
                                    EligibleVideos.Count();

        #endregion

        #region Colors


        public Brush CheatsColor
        {
            get
            {
                if (EligibleCheats.Any())
                    return Brushes.Blue;
                else if (!CheatsCollection.Any())
                    return Brushes.Red;
                else
                    return Brushes.Black;
            }
        }
        public Brush ImagesColor
        {
            get
            {
                if (EligibleImages.Any())
                    return Brushes.Blue;
                else if (!ImagesCollection.Any())
                    return Brushes.Red;
                else
                    return Brushes.Black;
            }
        }

        public Brush ManualsColor
        {
            get
            {
                if (EligibleManuals.Any())
                    return Brushes.Blue;
                else if (!ManualsCollection.Any())
                    return Brushes.Red;
                else
                    return Brushes.Black;
            }
        }

        public Brush MusicsColor
        {
            get
            {
                if (EligibleMusics.Any())
                    return Brushes.Blue;
                else if (!MusicsCollection.Any())
                    return Brushes.Red;
                else
                    return Brushes.Black;
            }
        }
        public Brush VideosColor
        {
            get
            {
                if (EligibleVideos.Any())
                    return Brushes.Blue;
                else if (!VideosCollection.Any())
                    return Brushes.Red;
                else
                    return Brushes.Black;
            }
        }


        #endregion
        // ---

        public M_PackMePrev(ContPlatFolders platform, GameDataCont gdC) : base(platform, gdC)
        {
            if (string.IsNullOrEmpty(platform.FolderPath))
                _GamesPath = Path.Combine(Config.HLaunchBoxPath, "Games", platform.Name);
            else
                _GamesPath = Path.GetFullPath(Platform.FolderPath, Config.HLaunchBoxPath);

            _CheatsPath = Config.HCCodesPath;

            /*var tmpPlat = platform.PlatformFolders.First(x => x.FolderPath.Contains("Box"));
            string imgTail = tmpPlat.FolderPath.Substring(0, tmpPlat.FolderPath.IndexOf(platform.Name));

            if (!string.IsNullOrEmpty(imgTail))
                _ImagesPath = Path.GetFullPath(imgTail, Config.HLaunchBoxPath);*/

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
            MakeCollection(GameDataC.Images);

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

        private void MakeCollection(List<DataRepImg> images)
        {
            ImagesCollection.Clear();
            foreach (DataRepImg elem in images)
            {
                var dre = new DataRepImg(elem);
                dre.Name = $@".\{dre.CurrentPath.Substring(dre.CurrentPath.IndexOf(dre.Categorie))}";
                ImagesCollection.Add(dre);
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
            EligibleCheats.Clear();
            EligibleImages.Clear();
            EligibleManuals.Clear();
            EligibleMusics.Clear();
            EligibleVideos.Clear();

            OnPropertyChanged(nameof(CheatsColor));
            OnPropertyChanged(nameof(ImagesColor));
            OnPropertyChanged(nameof(ManualsColor));
            OnPropertyChanged(nameof(MusicsColor));
            OnPropertyChanged(nameof(VideosColor));
            OnPropertyChanged(nameof(FilesFound));
        }
        internal void SearchFiles()
        {
            //SearchIn("");
            //SearchIn("Manual", EligibleManuals, ManualsCollection);
            SearchIn(_CheatsPath, EligibleCheats, CheatsCollection);
            SearchIn(_ManualsPath, EligibleManuals, ManualsCollection);
            SearchIn(_MusicsPath, EligibleMusics, MusicsCollection);
            SearchIn(_VideosPath, EligibleVideos, VideosCollection);

            // Images
            SearchImages();

            OnPropertyChanged(nameof(FilesFound));
            OnPropertyChanged(nameof(CheatsColor));
            OnPropertyChanged(nameof(ImagesColor));
            OnPropertyChanged(nameof(ManualsColor));
            OnPropertyChanged(nameof(MusicsColor));
            OnPropertyChanged(nameof(VideosColor));

            void SearchIn(string srcFolder, ObservableCollection<string> collection, ObservableCollection<DataRep> collectionRef)
            {
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

            void SearchImages()
            {
                foreach (PlatformFolder platformPath in Platform.PlatformFolders)
                {
                    if (
                        platformPath.MediaType.Equals("Manual", StringComparison.OrdinalIgnoreCase) ||
                        platformPath.MediaType.Equals("Music", StringComparison.OrdinalIgnoreCase) ||
                        platformPath.MediaType.Equals("Video", StringComparison.OrdinalIgnoreCase) ||
                        platformPath.MediaType.Equals("Theme Video", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    // --- Images

                    string hpath = Path.GetFullPath(platformPath.FolderPath, Config.HLaunchBoxPath);

                    if (!Directory.Exists(hpath))
                        continue;

                    foreach (string f in Directory.EnumerateFiles(hpath, "*.*", SearchOption.AllDirectories))
                    {
                        bool res = true;
                        foreach (string word in WordsToSearch)
                            res &= f.Contains(word, StringComparison.OrdinalIgnoreCase);

                        if (res && ImagesCollection.FirstOrDefault(x => x.CurrentPath.Equals(f)) == null)
                        {
                            string categFolder = Path.GetFileName(hpath);
                            DataRepImg drI = new DataRepImg(platformPath.MediaType, f);
                            drI.Name = $".\\{f.Substring(hpath.IndexOf(categFolder))}";
                            EligibleImages.Add(drI);
                        }
                    }
                }

            }

        }
        #endregion

        internal void AddCheat()
        {
            /*if (string.IsNullOrEmpty(EligibleCheatSelected))
                return;

            if (CheckCollection(CheatsCollection, EligibleCheatSelected))
                return;

            DataRep tmp = new DataRep(EligibleManualSelected);
            tmp.Name = tmp.CurrentPath.Replace(_CheatsPath, ".");
            CheatsCollection.Add(tmp);
            EligibleCheats.Remove(EligibleCheatSelected);*/
            AddBehavior(new DataRep(EligibleCheatSelected), _CheatsPath, CheatsCollection, EligibleCheats);
            OnPropertyChanged(nameof(CheatsColor));
        }

        internal void AddImage()
        {
            if (EligibleImageSelected == null)
                return;

            foreach (var elem in ImagesCollection)
                if (elem.CurrentPath.Equals(EligibleImageSelected.CurrentPath, StringComparison.OrdinalIgnoreCase))
                    return;

            ImagesCollection.Add(EligibleImageSelected);
            EligibleImages.Remove(EligibleImageSelected);

            OnPropertyChanged(nameof(FilesFound));
            OnPropertyChanged(nameof(ImagesColor));
        }


        /// <summary>
        /// Ajoute un manuel à la liste à copier
        /// </summary>
        internal void AddManual()
        {
            /*if (string.IsNullOrEmpty(EligibleManualSelected))
                return;

            if (CheckCollection(ManualsCollection, EligibleManualSelected))
                return;

            DataRep tmp = new DataRep(EligibleManualSelected);
            tmp.Name = tmp.CurrentPath.Replace(_ManualsPath, ".");
            ManualsCollection.Add(tmp);
            EligibleManuals.Remove(EligibleManualSelected);*/
            AddBehavior(new DataRep(EligibleManualSelected), _ManualsPath, ManualsCollection, EligibleManuals);
            OnPropertyChanged(nameof(ManualsColor));

        }

        internal void AddMusic()
        {
            /*if (string.IsNullOrEmpty(EligibleMusicSelected))
                return;

            if (CheckCollection(MusicsCollection, EligibleMusicSelected))
                return;

            DataRep tmp = new DataRep(EligibleMusicSelected);
            tmp.Name = tmp.CurrentPath.Replace(_MusicsPath, ".");
            MusicsCollection.Add(tmp);
            EligibleMusics.Remove(EligibleMusicSelected);*/
            AddBehavior(new DataRep(EligibleMusicSelected), _MusicsPath, MusicsCollection, EligibleMusics);
            OnPropertyChanged(nameof(MusicsColor));
        }

        internal void AddVideo()
        {
            /*if (string.IsNullOrEmpty(EligibleVideoSelected))
                return;

            if (CheckCollection(VideosCollection, EligibleVideoSelected))
                return;

            DataRep tmp = new DataRep(EligibleVideoSelected);
            tmp.Name = tmp.CurrentPath.Replace(_VideosPath, ".");
            VideosCollection.Add(tmp);
            EligibleVideos.Remove(EligibleVideoSelected);*/
            AddBehavior(new DataRep(EligibleVideoSelected), _VideosPath, VideosCollection, EligibleVideos);
            OnPropertyChanged(nameof(VideosColor));
        }

        private void AddBehavior<T>(T value, string categPath, ObservableCollection<T> collection, ObservableCollection<string> eligibleCollec) where T : DataRep, new()
        {
            if (value == null)
                return;

            foreach (var elem in collection)
                if (elem.CurrentPath.Equals(value.CurrentPath, StringComparison.OrdinalIgnoreCase))
                    return;

            value.Name = value.CurrentPath.Replace(categPath, ".");
            collection.Add(value);
            eligibleCollec.Remove(value.CurrentPath);

            OnPropertyChanged(nameof(FilesFound));
        }

        #region


        internal void OpenImage(string imgSelected)
        {
            if (String.IsNullOrEmpty(imgSelected))
                return;

            //string path = Path.Combine(Root, Common.Musics, SelectedMusic);
            Process p = new Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = imgSelected;
            p.Start();

        }



        internal void RemoveCheat()
        {
            RemoveBehavior(SelectedCheatFile, CheatsCollection, EligibleCheats);
            OnPropertyChanged(nameof(CheatsColor));
        }

        internal void RemoveImage()
        {
            if (ImageSelected == null)
                return;

            bool find = false;
            foreach (var elem in EligibleImages)
                if (elem.CurrentPath.Equals(ImageSelected.CurrentPath, StringComparison.OrdinalIgnoreCase))
                {
                    find = true;
                    break;
                }

            if (!find)
                EligibleImages.Add(ImageSelected);

            ImagesCollection.Remove(ImageSelected);

            OnPropertyChanged(nameof(FilesFound));
            OnPropertyChanged(nameof(ImagesColor));
        }

        /// <summary>
        /// Enlève un manuel de la liste à copier
        /// </summary>
        internal void RemoveManual()
        {
            RemoveBehavior(SelectedManual, ManualsCollection, EligibleManuals);
            OnPropertyChanged(nameof(ManualsColor));
        }

        /// <summary>
        /// Enlève une musique de la liste à copier
        /// </summary>
        internal void RemoveMusic()
        {
            /*if (!SelectedMusic.IsSelected)
                MusicsCollection.Remove(SelectedMusic);
            else
                DxMBox.ShowDial("Default file, you can't remove it for the while");*/
            RemoveBehavior(SelectedMusic, MusicsCollection, EligibleMusics);
            OnPropertyChanged(nameof(MusicsColor));
        }

        /// <summary>
        /// Enlève une vidéo de la liste à copier
        /// </summary>
        internal void RemoveVideo()
        {
            RemoveBehavior(SelectedVideo, VideosCollection, EligibleVideos);
            OnPropertyChanged(nameof(VideosColor));
        }

        private void RemoveBehavior<T>(T selected, Collection<T> collec, Collection<string> eligibleCollec) where T : DataRep
        {
            if (selected == null)
                return;

            if (selected.IsSelected)
            {
                DxMBox.ShowDial("Default file, you can't remove it for the while");
                return;
            }


            foreach (var elem in eligibleCollec)
                if (elem.Equals(selected.CurrentPath, StringComparison.OrdinalIgnoreCase))
                    return;

            eligibleCollec.Add(selected.CurrentPath);
            collec.Remove(selected);

            OnPropertyChanged(nameof(FilesFound));
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
