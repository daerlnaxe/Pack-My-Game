using AsyncProgress.Cont;
using Common_Graph;
using Common_PMG.Container;
using Common_PMG.Container.AAPP;
using Common_PMG.Container.Game;
using Common_PMG.Container.Game.LaunchBox;
using Common_PMG.XML;
using DxTBoxCore.Common;
using Hermes;
using Pack_My_Game.Files;
using Pack_My_Game.Language;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static Pack_My_Game.Common;


namespace Pack_My_Game.Core
{
    /// <summary>
    /// Find files
    /// </summary>
    partial class LaunchBoxCore
    {

        /// <summary>
        /// Récupère tous les fichiers en fonction du LBGame, ainsi que des dossiers de la plateforme
        /// </summary>
        /// <param name="lbGame"></param>
        /// <param name="gpX"></param>
        private void GetFiles(LBGame lbGame, GameDataCont gdC)
        {
            HeTrace.WriteLine($"[GetFiles]", this);

            var possibilities = GetBasicPossibilities(lbGame.Id, lbGame.Title);

            // Jeu principal + Clones
            gdC.SetApplications = GetFilesForGames(lbGame);

            // CheatCodes
            if (!string.IsNullOrEmpty(Config.HCCodesPath))
                gdC.SetSCheatCodes = GetFilesByPredict(possibilities, Path.Combine(Config.HCCodesPath, _ZePlatform.Name), "CheatCodes");

            // Manuels
            gdC.SetDefaultManual = GetFileForSpecifics(lbGame.ManualPath, Common.Config.HLaunchBoxPath);
            gdC.AddSManuals = GetMoreFiles("Manual", possibilities);

            // Musics 
            gdC.SetDefaultMusic = GetFileForSpecifics(lbGame.MusicPath, Common.Config.HLaunchBoxPath);
            gdC.AddSMusics = GetMoreFiles("Music", possibilities);

            // Videos
            gdC.SetDefaultVideo = GetFileForSpecifics(lbGame.VideoPath, Common.Config.HLaunchBoxPath);
            gdC.SetDefaultThemeVideo = GetFileForSpecifics(lbGame.ThemeVideoPath, Common.Config.HLaunchBoxPath);
            gdC.AddSVideos = GetMoreFiles("Video", possibilities);

            //GetMoreFiles(toSearch, gpX.CompVideos, "Video", gpX.VideoPath, gpX.ThemeVideoPath);

            // Images
            gdC.Images = GetImagesFiles(lbGame, possibilities);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationPath"></param>
        /// <param name="applications"></param>
        /// <returns>Le fichier sélectionné</returns>
        private ICollection<DataPlus> GetFilesForGames(LBGame lbGame)
        {
            List<DataPlus> games = new List<DataPlus>();

            HeTrace.WriteLine($"\t[GetFilesForGames]", this);

            if (string.IsNullOrEmpty(lbGame.ApplicationPath))
                throw new ArgumentNullException("[GetFiles] Folder of application path is empty");

            string extension = Path.GetExtension(lbGame.ApplicationPath);

            // si aucune extension on ne pack pas le fichier
            if (string.IsNullOrEmpty(extension))
                throw new ArgumentNullException("[GetFiles] Extension of application is null");


            // --- Cas des extensions cue
            if (extension.Equals(".cue", StringComparison.OrdinalIgnoreCase))
            {
                string srcFile = Path.GetFullPath(lbGame.ApplicationPath, Common.Config.HLaunchBoxPath);

                //Lecture du fichier cue
                Cue_Scrapper cuecont = new Cue_Scrapper(srcFile);

                //Folder containing files
                string sourceFold = Path.GetDirectoryName(srcFile);

                // Fonctionne avec le nom du fichier
                foreach (string fileName in cuecont.Files)
                {
                    // Donne le lien complet vers le fichier
                    games.Add(DataPlus.MakeNormal(Path.Combine(sourceFold, fileName)));
                }
            }

            games.Add(DataPlus.MakeChosen(lbGame.Id, lbGame.Title, Path.GetFullPath(lbGame.ApplicationPath, Common.Config.HLaunchBoxPath)));


            // ---  Récupération des clones
            List<Clone> clones = XML_Games.ListClones(_XMLPlatformFile, "GameID", lbGame.Id).ToList();

            // tri des doublons / filter duplicates
            List<Clone> fClones = FilesFunc.DistinctClones(clones, lbGame.ApplicationPath, Common.Config.HLaunchBoxPath);


            if (fClones.Any())
            {
                HeTrace.WriteLine($"\t[{nameof(XML_Games.ListClones)}] found: '{fClones.Count()}'", this);

                foreach (Clone c in fClones)
                {
                    string path = Path.GetFullPath(c.ApplicationPath, Common.Config.HLaunchBoxPath);

                    if (File.Exists(path))
                        games.Add(DataPlus.MakeNormal(c.Id, Path.GetFileName(path), path));

                }
            }

            return games;
        }




        /// <summary>
        /// Récupère les images
        /// </summary>
        /// <param name="possibilities">Possibilités de nom des fichiers</param>
        /// <param name="lbGame"></param>
        /// <returns></returns>
        private List<DataRepExt> GetImagesFiles(LBGame lbGame, Dictionary<string, string> possibilities)
        {
            //Queue<string> lPackFile = new Queue<string>();
            List<DataRepExt> images = new List<DataRepExt>();

            foreach (PlatformFolder plfmFolder in _ZePlatform.PlatformFolders)
            {
                //filtre sur tout ce qui n'est pas image
                switch (plfmFolder.MediaType)
                {
                    case "Manual":
                    case "Music":
                    case "Theme Video":
                    case "Video":
                        continue;
                }

                if (string.IsNullOrEmpty(plfmFolder.FolderPath))
                {
                    SetStatus(this, new StateArg($"Directory link is empty: '{plfmFolder.FolderPath}'", CancelFlag));
                    continue;
                }


                string folder = Path.GetFullPath(plfmFolder.FolderPath, Config.HLaunchBoxPath);

                // Test si le dossier existe
                if (!Directory.Exists(folder))
                {
                    SetStatus(this, new StateArg($"Directory doesn't exist: '{folder}'", CancelFlag));
                    continue;
                }


                // Liste du contenu des dossiers
                foreach (var fichier in Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories))
                {
                    string fileName = Path.GetFileName(fichier);

                    bool res = false;

                    // Test des possibilités de nom
                    foreach (var possibility in possibilities)
                    {
                        if (possibility.Key.Equals("ID")
                            && fileName.Contains(possibility.Value, StringComparison.OrdinalIgnoreCase))
                            res = true;
                        else if (fileName.StartsWith($"{possibility.Value}-", StringComparison.OrdinalIgnoreCase))
                            res = true;

                        if (res)
                            break;
                    }

                    if (res)
                    {
                        HeTrace.WriteLine($"\t[GetImages] Found '{fichier}' in '{folder}'");
                        DataRepExt tmp = new DataRepExt(plfmFolder.MediaType, fichier);
                        images.Add(tmp);
                        
                    }
                }
            }

            return images;
        }

        // ------------

        /// <summary>
        /// Récupère les possibilités de nom d'un fichier (niveau basique)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetBasicPossibilities(string id, string title)
        {
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            tmp.Add("ID", id);
            if (title.Contains(":") || title.Contains("'"))
            {
                tmp.Add("LaunchBox1", title.Replace(':', '_').Replace('\'', '_').Replace("__", "_"));
                tmp.Add("LaunchBox2", title.Replace(':', ' ').Replace('\'', ' '));
            }
            else
            {
                tmp.Add("Normal", title);
            }

            tmp.Add("NoSpace", title.Replace(":", string.Empty).Replace("\'", string.Empty).Replace(" ", string.Empty));
            tmp.Add("_", title.Replace(':', '_').Replace('\'', '_').Replace(' ', '_'));

            return tmp;
        }


        /// <summary>
        /// Fait les vérifications nécessaires avant de renvoyer le lien d'un fichier entré dans la db
        /// </summary>
        /// <param name="dbGamePath"></param>
        /// <returns>
        /// Vérifie et retourne le fichier sélectionné
        /// </returns>
        internal static string GetFileForSpecifics(string dbGamePath, string baseApp)
        {
            // Si le fichier indiqué dans le fichier du jeu existe
            if (!string.IsNullOrEmpty(dbGamePath))
            {
                HeTrace.WriteLine($"\t[{nameof(GetFileForSpecifics)}]");
                string linkFile = Path.GetFullPath(dbGamePath, baseApp);
                if (File.Exists(linkFile))
                {
                    return linkFile;
                }
            }
            return null;
        }

        /// <summary>
        /// Interface avec predict pour récupérer le bon dossier
        /// </summary>
        /// <param name="mediatype"></param>
        /// <param name="titleElements"></param>
        /// <returns></returns>
        private List<string> GetMoreFiles(string mediatype, Dictionary<string, string> titleElements/*, params string[] fields*/)
        {
            // On récupère le dossier concerné par le média
            PlatformFolder folder = _ZePlatform.PlatformFolders.FirstOrDefault(
                                            (x) => x.MediaType.Equals(mediatype, StringComparison.OrdinalIgnoreCase));

            if (folder == null)
                return null;

            //List<string> filteredFiles = GetFilesByPredict(toSearch, folder.FolderPath, mediatype);
            return GetFilesByPredict(titleElements, folder.FolderPath, mediatype);
        }

        /// <summary>
        /// Récupère les fichiers par "prédiction"
        /// </summary>
        internal List<string> GetFilesByPredict(Dictionary<string, string> possibilities, string folder, string mediatype)
        {
            if (string.IsNullOrEmpty(folder))
            {
                SetStatus(this, new StateArg($"Directory link is empty: '{folder}'", CancelFlag));
                return null;
            }

            string path = Path.GetFullPath(folder, Config.HLaunchBoxPath);

            // Test si le dossier existe
            if (!Directory.Exists(path))
            {
                SetStatus(this, new StateArg($"Directory doesn't exist: '{path}'", CancelFlag));
                return null;
            }

            // bypass - 
            string[] bypass = new string[] { "-", " -" };

            List<string> filteredFiles = new List<string>();
            //          foreach (string fichier in files)
            foreach (string fichier in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories))
            {
                string filename = Path.GetFileNameWithoutExtension(fichier);

                bool res = false;
                // Test des possibilités de nom
                foreach (var possibility in possibilities)
                {
                    // Sans Bypass
                    if (possibility.Key.Equals("ID")
                        && filename.Contains(possibility.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        res = true;
                        break;
                    }
                    else if (filename.Equals(possibility.Value, StringComparison.OrdinalIgnoreCase))
                    {
                        res = true;
                        break;
                    }

                    // Avec Bypass
                    foreach (var b in bypass)
                    {
                        if (filename.StartsWith(possibility.Value + b, StringComparison.OrdinalIgnoreCase))
                        {
                            res = true;
                            break;
                        }
                    }

                    //
                    if (res)
                        break;

                }

                if (res)
                {
                    HeTrace.WriteLine($"\t[GetFilesByPredict] Found '{fichier}'");
                    filteredFiles.Add(fichier);
                    
                }
            }

           /* if (filteredFiles.Count <= 0)
            {
                SafeBoxes.Dispatch_Mbox(this,
                    $"{LanguageManager.Instance.Lang.S_SearchFor} {mediatype}: 0 {LanguageManager.Instance.Lang.Word_Result}",
                    $"{LanguageManager.Instance.Lang.S_SearchFor} { mediatype}",
                    E_DxButtons.Ok);
            }
            else
            {
                //filteredFiles = PackMe_IHM.Validate_FilesFound(filteredFiles, mediatype).ToList();
            }*/

            return filteredFiles;
        }


    }
}
