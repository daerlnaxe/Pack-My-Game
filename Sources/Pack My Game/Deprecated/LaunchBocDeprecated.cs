using Common_PMG.Container;
using Common_PMG.Container.AAPP;
using Common_PMG.Container.Game;
using DxLocalTransf;

using DxTBoxCore.Common;
using Hermes;
using LaunchBox_XML.XML;
using Pack_My_Game.Cont;
using Pack_My_Game.Files;
using Pack_My_Game.IHM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using PS = Pack_My_Game.Properties.Settings;

namespace Pack_My_Game.Core
{
    partial class LaunchBoxCoreDeprecated
    {


        [Obsolete]
        private void CopyFiles(LBGame lbGame, Folder tree)
        {
            
            // 2021 - Formatage de la chaine pour éviter les erreurs
            string toSearch = lbGame.Title.Replace(':', '_').Replace('\'', '_').Replace("__", "_");

            // Jeu
            CopyRoms(lbGame.ApplicationPath, tree.Children[Common.Games].Path);
            // Video, Music, Manual
            CopySpecific(toSearch, lbGame.ManualPath, tree.Children[Common.Manuals].Path, "Manual", x => lbGame.ManualPath = x);
            CopySpecific(toSearch, lbGame.MusicPath, tree.Children[Common.Musics].Path, "Music", x => lbGame.MusicPath = x);
            CopySpecific(toSearch, lbGame.VideoPath, tree.Children[Common.Videos].Path, "Video", x => lbGame.VideoPath = x); ;
            // Images
            CopyImages(lbGame, tree.Children[Common.Images].Path);

            // CheatCodes
            #region Copy CheatCodes
            if (PS.Default.opCheatCodes && !string.IsNullOrEmpty(PS.Default.CCodesPath))
            {
                CopyCheatCodes(toSearch, tree.Children[Common.CheatCodes].Path);
            }
            else
            {
                HeTrace.WriteLine("[Run] Copy Cheat Codes disabled", this);
            }
            #endregion

            // Clones
            #region Copy Clones
            if (PS.Default.opClones)
            {
                CopyClones(lbGame, tree.Children[Common.Games].Path);
            }
            else
            {
                HeTrace.WriteLine($"[Run] Clone copy disabled", this);
            }
            #endregion

        }

        /// <summary>
        /// Copy games files
        /// </summary>
        /// <remarks>
        /// Ne conserve pas les subfolders
        /// </remarks>
        /// <param name="applicationPath"></param>
        /// <param name="destLocation"></param>
        [Obsolete]
        private void CopyRoms(string applicationPath, string destLocation)
        {
            /*
             * On part du principe que le dossier rom DOIT être rempli
            */
            if (string.IsNullOrEmpty(applicationPath))
                throw new ArgumentNullException("[CopyRoms] Folder of application path is empty");

            string extension = Path.GetExtension(applicationPath);

            // si aucune extension on ne pack pas le fichier
            if (string.IsNullOrEmpty(extension))
                throw new ArgumentNullException("[CopyRoms] Extension of application is null");


            string srcFile = Path.GetFullPath(applicationPath, PS.Default.LBPath);

            // --- Cas des extensions cue
            if (extension.Equals(".cue", StringComparison.OrdinalIgnoreCase))
            {
                //Lecture du fichier cue
                Cue_Scrapper cuecont = new Cue_Scrapper(srcFile);

                //Folder containing files
                string sourceFold = Path.GetDirectoryName(srcFile);


                // Fonctionne avec le nom du fichier
                foreach (string fileName in cuecont.Files)
                {
                    // Donne le lien complet vers le fichier
                    string source = Path.Combine(sourceFold, fileName);

                    SimpleCopyManager(source, destLocation, ref _FileConflictDecision);
                }
            }

            // --- Copy of application (.cue file too)
            SimpleCopyManager(srcFile, destLocation, ref _FileConflictDecision);

        }


        /// <summary>
        /// Copie les fichiers qui sont peut être identifiés dans le xml
        /// </summary>
        /// <param name="dbGamePath">Tiré des données du jeu</param>
        /// <param name="destLocation">Dossier de destination</param>
        /// <param name="mediatype">Utilisé pour retrouver dans l'objet plateforme</param>
        /// <param name="Assignation"></param>
        /// <remarks>
        /// </remarks>
        [Obsolete]
        private void CopySpecific(string toSearch, string dbGamePath, string destLocation, string mediatype, Func<string, string> Assignation)
        {
            List<string> files = new List<string>();

            // Normal copy, if path get by xml exists
            // 05/04/2021 Modifications pour lever le ou et mettre un système de "Et"
            if (!string.IsNullOrEmpty(dbGamePath))
            {
                string linkFile = Path.GetFullPath(dbGamePath, PS.Default.LBPath);
                if (File.Exists(linkFile))
                    //05/04/2021 SimpleCopyManager(dbPath, destLocation/*, mediatype*/, ref _FileConflictDecision);
                    files.Add(linkFile);

                // Ici on met sous forme relative
                // Assignation(DxPaths.Windows.DxPath.To_Relative(PS.Default.LBPath, linkFile));
            }



            // On récupère le dossier concerné par le média
            PlatformFolder folder = _ZePlatform.PlatformFolders.First(
                                            (x) => x.MediaType.Equals(mediatype, StringComparison.OrdinalIgnoreCase));
            /*foreach (PlatformFolder pFormfolder in _ZePlatform.PlatformFolders)
            {
                if (pFormfolder.MediaType.Equals(mediatype))
                {
                    folder = pFormfolder;
                    break;
                }
            }*/

            List<string> filteredFiles = GetFilesByPredict(toSearch, folder.FolderPath, mediatype);


            // En cas d'abandon ou de fichier non trouvé on sort
            if (files.Count == 0 && (filteredFiles == null || filteredFiles.Count == 0))
                return;

            // On assigne si nécessaire
            /*  if (files.Count == 0 && filteredFiles.Count > 0)
                  Assignation(DxPaths.Windows.DxPath.To_Relative(PS.Default.LBPath, filteredFiles[0]));*/


            files.AddRange(filteredFiles);

            // On enlève les doublons
            files = files.Distinct().ToList();

            #region
            // Traitement des fichiers pour la copie
            foreach (string fichier in filteredFiles)
            {
                // Sachant qu'ici on a le nom du dossier source on peut conserver la structure
                string tail = string.Empty;

                if (fichier.Contains(folder.FolderPath))
                    tail = fichier.Replace(folder.FolderPath, "").Replace(Path.GetFileName(fichier), "");

                //Recherche de fichiers déjà présents
                //string destFile = Path.Combine(destLocation, Path.GetFileName(fichier));

                if (string.IsNullOrEmpty(tail))
                    SimpleCopyManager(fichier, destLocation, ref _FileConflictDecision);
                else
                    SimpleCopyManager(fichier, Path.Combine(destLocation, tail.Substring(1)), ref _FileConflictDecision);
            }


            #endregion




        }





        /// <summary>
        /// Copie les images / Copy the images
        /// </summary>      
        /// <remarks>To Add Mask see 'Where'</remarks>
        private void CopyImages(LBGame lbGame, string imgsFolder)
        {
            if (!imgsFolder.Contains(_WFolder))
                throw new Exception($"[CreateFolders] Erreur la chaine '{imgsFolder}' ne contient pas '{_WFolder}'");

            //ITrace.WriteLine(prefix: false);

            Queue<PackFile> lPackFile = new Queue<PackFile>();

            // Get All images (To Add mask see at Where)
            Console.WriteLine($"[CopyImages] Search all images for '{lbGame.Title}'");


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

                // 2020 - on modify pour certains titres, la recherche
                string toSearch = lbGame.Title.Replace(':', '_').Replace('\'', '_').Replace("__", "_");

                // Liste du contenu des dossiers
                foreach (var fichier in Directory.EnumerateFiles(plfmFolder.FolderPath, "*.*", SearchOption.AllDirectories))
                {
                    string fileName = Path.GetFileName(fichier);

                    if (
                        !fileName.StartsWith($"{toSearch}-") &&
                        !fileName.StartsWith($"{ lbGame.Title}.{ lbGame.Id}-"))
                        continue;

                    HeTrace.WriteLine($"\t\t[CopyImages] Found '{fichier}' in '{plfmFolder.FolderPath}'");

                    PackFile tmp = new PackFile(plfmFolder.MediaType, fichier);

                    lPackFile.Enqueue(tmp);
                }
            }

            //      ITrace.WriteLine(prefix: false);
            E_Decision resMem = E_Decision.None;
            while (lPackFile.Count != 0)
            {
                var pkFile = lPackFile.Dequeue();

                // On récupère la tail
                int pos = pkFile.LinkToThePath.IndexOf(pkFile.Categorie);
                string tail1 = Path.GetDirectoryName(pkFile.LinkToThePath).Substring(pos);

                // Dossier de destination
                //25/03/2021string destFolder = Path.Combine(_Tree.Children[nameof(SubFolder.Images)].Path, tail1);
                string destFolder = Path.Combine(imgsFolder, tail1);

                SimpleCopyManager(pkFile.LinkToThePath, destFolder, ref resMem);
            }

        }

        /// <summary>
        /// Copie les cheatcodes / Copy all cheatcodes
        /// </summary>
        private void CopyCheatCodes(string toSearch, string destLocation)
        {

            string CCodesDir = Path.Combine(PS.Default.CCodesPath, _ZePlatform.Name);
            if (!Directory.Exists(CCodesDir))
            {
                this.UpdateStatus?.Invoke(this, $"Directory doesn't exist: '{CCodesDir}'");
                return;
            }

            var filteredFiles = GetFilesByPredict(toSearch, CCodesDir, "CheatCodes");


            // En cas d'abandon ou de fichier non trouvé on sort
            if (filteredFiles == null || filteredFiles.Count == 0)
                return;

            //
            foreach (string fichier in filteredFiles)
            {
                SimpleCopyManager(fichier, destLocation, ref _FileConflictDecision);

            }
        }



        private void CopyClones(LBGame lBGame, string destLocation)
        {
            //ITrace.WriteLine(prefix: false);
            /* 26/03/2021
            OPFiles opF = new OPFiles()
            {
                Buttons = Dcs_Buttons.NoStop,
                WhoCallMe = "Clone"
            };
            opF.IWriteLine += (string message) => ITrace.WriteLine(message);
            opF.IWrite += (string message) => ITrace.BeginLine(message);*/

            List<Clone> clones = XML_Games.ListClones(_XMLPlatformFile, "GameID", lBGame.Id).ToList();

            // tri des doublons / filter duplicates
            List<Clone> fClones = FilesFunc.DistinctClones(clones, lBGame.ApplicationPath, PS.Default.LBPath);

            // On va vérifier que chaque clone n'est pas déjà présent et selon déjà copier
            foreach (Clone zeClone in fClones)
            {
                //waiting zeClone.ApplicationPath = ReconstructPath(zeClone.ApplicationPath);

                SimpleCopyManager(Path.GetFullPath(zeClone.ApplicationPath, PS.Default.LBPath), destLocation, ref _FileConflictDecision);
            }
        }



        /// <summary>
        /// Verifiy if file exist, if it is doesn't copy + HashVerif at the end
        /// </summary>
        /// <param name="fileSrc"></param>
        /// <param name="destFile"></param>
        /// <remarks>
        /// Ne gère pas les tails, doit être fait en amont
        /// </remarks>
        [Obsolete]
        internal void SimpleCopyManager(string fileSrc, string destFolder, ref E_Decision previousDec)
        {
            if (CancelToken.IsCancellationRequested)
                throw new OperationCanceledException("Operation stopped");

            /*if (!imgsFolder.Contains(_WFolder))
                throw new Exception($"[CreateFolders] Erreur la chaine '{imgsFolder}' ne contient pas '{_WFolder}'");
            */

            // Création des dossiers
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);

            string destFile = Path.Combine(destFolder, Path.GetFileName(fileSrc));

            E_Decision? conflictDec = previousDec;

            bool overwrite = false;
            if (File.Exists(destFile))
            {
                HeTrace.Write($"[CopyHandler] Destination file exists... ", this);
                if (conflictDec == E_Decision.None)
                {
                    conflictDec = PackMe_IHM.Ask4_FileConflict(fileSrc, destFile, E_DxConfB.OverWrite | E_DxConfB.Pass | E_DxConfB.Trash);

                    // Mémorisation pour les futurs conflits
                    switch (conflictDec)
                    {
                        case E_Decision.PassAll:
                        case E_Decision.OverWriteAll:
                        case E_Decision.TrashAll:
                            previousDec = conflictDec == null ? E_Decision.None : (E_Decision)conflictDec;
                            break;
                    }
                }

                HeTrace.EndLine(conflictDec.ToString(), this);
                switch (conflictDec)
                {
                    case E_Decision.Pass:
                    case E_Decision.PassAll:
                        this.UpdateStatus(this, $"Pass: {fileSrc}");
                        return;
                    case E_Decision.OverWrite:
                    case E_Decision.OverWriteAll:
                        this.UpdateStatus(this, $"OverWrite: {destFile}");
                        overwrite = true;
                        break;
                    case E_Decision.Trash:
                    case E_Decision.TrashAll:
                        this.UpdateStatus(this, $"Trash: {destFile}");
                        OpDFiles.Trash(destFile);
                        break;
                }
            }

            // --- Copie
            this.UpdateStatus(this, $"Copy {fileSrc}");
            this.UpdateProgress?.Invoke(this, 0);
            this.MaximumProgress?.Invoke(this, 1);
            FilesFunc.Copy(fileSrc, destFile, overwrite);
            this.UpdateProgress?.Invoke(this, 1);

            // --- Vérification des sommes
            this.UpdateStatus(this, $"Copy verification");
            this.MaximumProgress?.Invoke(this, 100);

            //bool? res = _ObjectFiles.VerifByHash_Sync(fileSrc, destFile, () => MD5.Create());
            var res = _ObjectFiles.DeepVerif(fileSrc, destFile, () => MD5.Create());

            this.UpdateStatus?.Invoke(this, $"Check verif: {res}");
            //this.UpdateProgress?.Invoke(100);

        }


        // --- 


        /// <summary>
        /// Check les fichiers définis dans les données du jeu et enregistre ça dans un fichier
        /// ayant comme référent le dossier où ils sont sauvegardés.
        /// </summary>
        /// <param name="lbGame"></param>
        [Obsolete]
        private void ManageDefaultFiles(LBGame lbGame, string gamePath, Folder tree)
        {
            GamePaths gp = (GamePaths)lbGame;
            gp.ApplicationPath = ManageRomPath(lbGame.ApplicationPath, tree.Children[Common.Games]);
            gp.ManualPath = ManageDefaultFile(lbGame.ManualPath, tree.Children[Common.Manuals], "Manual");
            gp.MusicPath = ManageDefaultFile(lbGame.MusicPath, tree.Children[Common.Musics], "Music");
            //gp.VideoPath = ManageDefaultFile(lbGame.VideoPath, tree.Children[Common.Videos]);
            //gp.ThemeVideoPath = ManageDefaultFile(lbGame.ThemeVideoPath, tree.Children[Common.]);

            gp.WriteToJson(Path.Combine(gamePath, "DPGame.json"));
        }

        [Obsolete]
        private string ManageRomPath(string fileLoc, Folder folder)
        {
            string fileN = Path.GetFileName(fileLoc);

            if (File.Exists(Path.Combine(folder.Path, Path.GetFileName(fileLoc))))
                return $"\\{fileN}";
            else
                return null;
        }
        [Obsolete]
        private string ManageDefaultFile(string fileLoc, Folder folder, string media)
        {
            var motherF = _ZePlatform.PlatformFolders.FirstOrDefault((x) => x.MediaType == media);
            if (motherF == null)
                return null;

            // Conversion des chemins pour les faire pointer vers le dossier de travail
            string tmp = Path.GetFullPath(fileLoc, PS.Default.LBPath);
            string rootF = Path.GetFullPath(motherF.FolderPath, PS.Default.LBPath);

            if (!tmp.Contains(rootF))
                return null;


            tmp.Replace(rootF, folder.Path);
            // Vérification de l'existence des fichiers
            if (File.Exists(tmp))
                return DxPaths.Windows.DxPath.To_Relative(folder.Path, tmp);
            else
                return null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="gpX"></param>
        /// <param name="tree"></param>
        /// <remarks>
        /// Altère GPX pour suivre les fichiers
        /// </remarks>
        private void CopyFiles(GameDataCont gpX, Folder tree)
        {
            // Roms + Clones
            gpX.ApplicationPath = CopyMain(gpX.ApplicationPath, tree.Children[Common.Games].Path, false);
            CopyList(gpX.Apps, tree.Children[Common.Games].Path, false);

            // CheatCodes
            CopyList(gpX.CompCheatCodes, tree.Children[Common.CheatCodes].Path, false);

            // Manuals
            gpX.ManualPath = CopyMain(gpX.ManualPath, tree.Children[Common.Manuals].Path, true, "Manual");
            CopyList(gpX.CompManuals, tree.Children[Common.Manuals].Path, true, "Manual");

            // Musics
            gpX.MusicPath = CopyMain(gpX.MusicPath, tree.Children[Common.Musics].Path, true, "Music");
            CopyList(gpX.Musics, tree.Children[Common.Musics].Path, true, "Music");

            // Videos
            gpX.VideoPath = CopyMain(gpX.VideoPath, tree.Children[Common.Videos].Path, true, "Video");
            gpX.ThemeVideoPath = CopyMain(gpX.ThemeVideoPath, tree.Children[Common.Videos].Path, true, "Video");
            CopyList(gpX.Videos, tree.Children[Common.Videos].Path, true, "Video");

            // Images
            CopyImages(gpX.Images, tree.Children[Common.Images].Path);

        }

        /*   /// <summary>
       /// 
       /// </summary>
       /// <param name="path"></param>
       /// <param name="compApps"></param>
       /// <param name="destLocation"></param>
       /// <returns></returns>
       /// <remarks>
       /// On ne garde pas le tail
       /// </remarks>
       private string CopyMain(string path, string destLocation)
       {
           string futurLink = Path.Combine(destLocation, Path.GetFileName(path));

           SimpleCopyManager(path, ref _FileConflictDecision, futurLink);

           return DxPath.To_Relative(destLocation, futurLink);
       }*/

        private string CopyMain(string fichier, string destLocation, bool wTail, string mediatype = null)
        {
            if (string.IsNullOrEmpty(fichier))
                return string.Empty;

            PlatformFolder folder = null;
            if (wTail && !string.IsNullOrEmpty(mediatype))
            {
                // On récupère le dossier concerné par le média
                folder = _ZePlatform.PlatformFolders.First(
                            (x) => x.MediaType.Equals(mediatype, StringComparison.OrdinalIgnoreCase));
            }

            string futurLink, tail;
            futurLink = tail = string.Empty;

            if (folder != null && fichier.Contains(folder.FolderPath))
            {
                tail = fichier.Replace(folder.FolderPath, string.Empty).TrimStart('\\');
                futurLink = Path.Combine(destLocation, tail);
            }
            else
            {
                futurLink = Path.Combine(destLocation, Path.GetFileName(fichier));
            }

            SimpleCopyManager(fichier, ref _FileConflictDecision, futurLink);

            return DxPath.To_Relative(destLocation, futurLink);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="compApps"></param>
        /// <param name="folder"></param>
        /// <remarks>
        /// Conserve la structure si c'est souhaité
        /// </remarks>
        private void CopyList(List<string> elems, string destLocation, bool wTail, string mediatype = null)
        {
            PlatformFolder folder = null;
            if (wTail && !string.IsNullOrEmpty(mediatype))
            {
                // On récupère le dossier concerné par le média
                folder = _ZePlatform.PlatformFolders.First(
                            (x) => x.MediaType.Equals(mediatype, StringComparison.OrdinalIgnoreCase));
            }

            string tail, futurLink;
            foreach (string fichier in elems)
            {
                futurLink = string.Empty;
                tail = string.Empty;

                if (wTail)
                {
                    if (folder != null && fichier.Contains(folder.FolderPath))
                    {
                        tail = fichier.Replace(folder.FolderPath, string.Empty).TrimStart('\\');
                        futurLink = Path.Combine(destLocation, tail);

                        SimpleCopyManager(fichier, ref _FileConflictDecision, futurLink);
                        continue;
                    }
                }

                futurLink = Path.Combine(destLocation, Path.GetFileName(fichier));
                SimpleCopyManager(fichier, ref _FileConflictDecision, futurLink);
            }
        }

        private void CopyImages(List<DataRepExt> images, string destLocation)
        {
            E_Decision resMem = E_Decision.None;
            string tail, futurLink;
            foreach (DataRepExt pkFile in images)
            {
                tail = string.Empty;
                // On récupère la tail
                PlatformFolder folder = _ZePlatform.PlatformFolders.First(
                                          (x) => x.MediaType.Equals(pkFile.Categorie, StringComparison.OrdinalIgnoreCase));


                if (folder != null && pkFile.ALinkToThePath.Contains(folder.FolderPath))
                {
                    tail = pkFile.ALinkToThePath.Replace(folder.FolderPath, string.Empty).TrimStart('\\');

                    futurLink = Path.Combine(destLocation, pkFile.Categorie, tail);
                }
                else
                {
                    futurLink = Path.Combine(destLocation, Path.GetFileName(pkFile.ALinkToThePath));
                }

                SimpleCopyManager(pkFile.ALinkToThePath, ref resMem, futurLink);

                // Dossier de destination
                //25/03/2021string destFolder = Path.Combine(_Tree.Children[nameof(SubFolder.Images)].Path, tail1);
                //string destFolder = Path.Combine(imgsFolder, tail1);

                //SimpleCopyManager(pkFile.LinkToThePath, destFolder, ref resMem);
            }
        }

        internal void SimpleCopyManager(string fileSrc, ref E_Decision previousDec, string destFile)
        {
            if (CancelToken.IsCancellationRequested)
                throw new OperationCanceledException("Operation stopped");

            if (!destFile.Contains(_WFolder))
                throw new Exception($"[CreateFolders] Erreur la chaine '{destFile}' ne contient pas '{_WFolder}'");


            // Création des dossiers
            string destFolder = Path.GetDirectoryName(destFile);
            if (!Directory.Exists(destFolder))
                Directory.CreateDirectory(destFolder);

            //string destFile = Path.Combine(destFolder, Path.GetFileName(fileSrc));

            E_Decision? conflictDec = previousDec;

            bool overwrite = false;
            if (File.Exists(destFile))
            {
                HeTrace.Write($"[CopyHandler] Destination file exists... ", this);
                if (conflictDec == E_Decision.None)
                {
                    conflictDec = PackMe_IHM.Ask4_FileConflict(fileSrc, destFile, E_DxConfB.OverWrite | E_DxConfB.Pass | E_DxConfB.Trash);

                    // Mémorisation pour les futurs conflits
                    switch (conflictDec)
                    {
                        case E_Decision.PassAll:
                        case E_Decision.OverWriteAll:
                        case E_Decision.TrashAll:
                            previousDec = conflictDec == null ? E_Decision.None : (E_Decision)conflictDec;
                            break;
                    }
                }

                HeTrace.EndLine(conflictDec.ToString(), this);
                switch (conflictDec)
                {
                    case E_Decision.Pass:
                    case E_Decision.PassAll:
                        SetStatus(this, new StateArg($"Pass: {fileSrc}", CancelFlag));
                        return;
                    case E_Decision.OverWrite:
                    case E_Decision.OverWriteAll:
                        SetStatus(this, new StateArg($"OverWrite: {destFile}", CancelFlag));
                        overwrite = true;
                        break;
                    case E_Decision.Trash:
                    case E_Decision.TrashAll:
                        SetStatus(this, new StateArg($"Trash: {destFile}", CancelFlag));
                        OpDFiles.Trash(destFile);
                        break;
                }
            }

            // --- Copie
            SetStatus(this, new StateArg($"Copy {fileSrc}", CancelFlag));
            SetProgress(this, new ProgressArg(0, 1, CancelFlag));
            FilesFunc.Copy(fileSrc, destFile, overwrite);
            SetProgress(this, new ProgressArg(1, 1, CancelFlag));

            // --- Vérification des sommes
            this.SetStatus(this, new StateArg($"Copy verification", CancelFlag));
            //this.SetMaximum(this, 100);

            //bool? res = _ObjectFiles.VerifByHash_Sync(fileSrc, destFile, () => MD5.Create());
            //var res = _ObjectFiles.DeepVerif(fileSrc, destFile, () => MD5.Create());

            this.SetStatus(this, new StateArg($"Check verif: {res}", CancelFlag));
            //this.UpdateProgress?.Invoke(100);

        }


        /// <summary>
        /// Récupère les fichiers par "prédiction"
        /// </summary>
        [Obsolete]
        internal List<string> GetFilesByPredict(string toSearch, string folder, string mediatype)
        {

            // array of files with a part of the name
            string[] files = Directory.GetFiles(Path.GetFullPath(folder, Common.Config.HLaunchBoxPath), $"{toSearch}*.*", SearchOption.AllDirectories);

            #region processing on files found
            // bypass - 
            string[] bypass = new string[] { "-", " -" };

            List<string> filteredFiles = new List<string>();
            foreach (string fichier in files)
            {
                string filename = Path.GetFileNameWithoutExtension(fichier);
                // Test if total match
                if (filename.Equals(toSearch))
                {
                    // On ajoute à la liste des checkbox
                    filteredFiles.Add(fichier);
                }
                //Test if match with bypass
                else
                {
                    // On test avec chaque bypass
                    foreach (var b in bypass)
                    {
                        if (filename.StartsWith(toSearch + b))
                        {
                            // On ajoute à la liste des checkbox
                            filteredFiles.Add(fichier);
                            break;
                        }
                    }
                }
            }

            if (filteredFiles.Count <= 0)
            {
                SafeBoxes.Dispatch_Mbox(this,
                    $"{LanguageManager.Instance.Lang.S_SearchFor} {mediatype}: 0 {LanguageManager.Instance.Lang.Word_Result}",
                    $"{LanguageManager.Instance.Lang.S_SearchFor} { mediatype}",
                    E_DxButtons.Ok);
            }
            else
            {
                //filteredFiles = PackMe_IHM.Validate_FilesFound(filteredFiles, mediatype).ToList();
            }

            return filteredFiles;
        }


        /// <summary>
        /// Récupère les fichiers CheatsCodes
        /// </summary>
        /// <param name="possibilities">Possibilités des noms de fichier</param>
        /// <param name="compCheatCodes"></param>
        [Obsolete]
        private ICollection<string> GetCheatCodes(Dictionary<string, string> possibilities)
        {
            if (string.IsNullOrEmpty(Config.HCCodesPath))
                return new string[0];

            string CCodesDir = Path.Combine(Config.HCCodesPath, _ZePlatform.Name);
            if (!Directory.Exists(CCodesDir))
            {
                SetStatus(this, new StateArg($"Directory doesn't exist: '{CCodesDir}'", CancelFlag));
                return new string[0];
            }

            return GetFilesByPredict(possibilities, CCodesDir, "CheatCodes");
        }



        [Obsolete]
        private List<DataRepExt> GetImagesFiles(LBGame lbGame, string toSearch/*, List<DataRepExt> images*/)
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

                string folder = Path.GetFullPath(plfmFolder.FolderPath, Common.Config.HLaunchBoxPath);
                // Liste du contenu des dossiers
                foreach (var fichier in Directory.EnumerateFiles(folder, "*.*", SearchOption.AllDirectories))
                {
                    string fileName = Path.GetFileName(fichier);

                    if (
                        !fileName.StartsWith($"{toSearch}-") &&
                        !fileName.StartsWith($"{ lbGame.Title}.{ lbGame.Id}-"))
                        continue;

                    HeTrace.WriteLine($"\t[GetImages] Found '{fichier}' in '{folder}'");

                    DataRepExt tmp = new DataRepExt(plfmFolder.MediaType, fichier);

                    //   lPackFile.Enqueue(fichier);
                    images.Add(tmp);
                }
            }

            return images;
        }
    }
}
