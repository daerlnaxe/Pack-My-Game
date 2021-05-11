using Common_PMG.Container;
using Common_PMG.Container.Game;
using Common_PMG.XML;
using DxLocalTransf.Copy;
using DxTBoxCore.Common;
using DxTBoxCore.Languages;
using DxTBoxCore.Box_MBox;
using Hermes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using UnPack_My_Game.Decompression;
using UnPack_My_Game.Graph;
using UnPack_My_Game.Resources;
using Common_Graph;
using AsyncProgress.Basix;
using AsyncProgress;
using static UnPack_My_Game.Common;

namespace UnPack_My_Game.Cores
{
    internal abstract class C_LaunchBox : A_ProgressPersistD, I_ASBase
    {

        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();
        public CancellationToken CancelToken { get; }

        public bool CancelFlag { get; private set; }

        public bool IsPaused { get; set; }

        public bool IsInterrupted { get; private set; }

        public abstract bool ExportGames(ICollection<DataRep> folders);

        #region Depacking

        protected bool Depacking(DataRep g, string tmpPath)
        {
            string fileExt = Path.GetExtension(g.CurrentPath).TrimStart('.');

            // Décompression
            // Détection du mode
            if (fileExt.Equals("zip", StringComparison.OrdinalIgnoreCase))
                DepackZip(g.CurrentPath, tmpPath);
            else if (fileExt.Equals("7zip", StringComparison.OrdinalIgnoreCase) || fileExt.Equals("7z", StringComparison.OrdinalIgnoreCase))
                Depack7Zip(g.CurrentPath, tmpPath);
            else
                throw new Exception("File format not managed");

            return true;
        }

        private void DepackZip(string pathLink, string tmpPath)
        {
            // Extraction des fichiers
            ZipDecompression zippy = new ZipDecompression()
            {
                TokenSource = this.TokenSource,
                IsPaused = this.IsPaused,
            };

            SafeBoxes.LaunchDouble(zippy, () => zippy.ExtractAll(pathLink, tmpPath), "Zip Extraction");
        }

        private void Depack7Zip(string pathLink, string tmpPath)
        {
            // Extraction des fichiers
            SevenZipDecompression sevZippy = new SevenZipDecompression()
            {
                TokenSource = this.TokenSource,
                IsPaused = this.IsPaused,
            };

            SafeBoxes.LaunchDouble(sevZippy, () => sevZippy.ExtractAll(pathLink, tmpPath), "Zip Extraction");

        }
        #endregion Depacking

        // ---

        #region Extraction
        protected void ExtractDataFiles(DataRep g, string tmpPath)
        {
            string fileExt = Path.GetExtension(g.CurrentPath).TrimStart('.');

            // Détection du mode
            if (fileExt.Equals("zip", StringComparison.OrdinalIgnoreCase))
            {
                // Extraction des fichiers
                ZipDecompression zippy = new ZipDecompression()
                {
                    TokenSource = this.TokenSource,
                    IsPaused = this.IsPaused,
                };
                zippy.ExtractSpecificFiles(g.CurrentPath, tmpPath, "DPGame.json", "NBGame.xml", "TBGame.xml", "EBGame.xml");
            }

            else if (fileExt.Equals("7zip", StringComparison.OrdinalIgnoreCase) || fileExt.Equals("7z", StringComparison.OrdinalIgnoreCase))
            {
                // Extraction des fichiers
                SevenZipDecompression sevZipp = new SevenZipDecompression()
                {
                    TokenSource = this.TokenSource,
                    IsPaused = this.IsPaused,
                };
                sevZipp.ExtractSpecificFiles(g.CurrentPath, tmpPath, "DPGame.json", "NBGame.xml", "TBGame.xml", "EBGame.xml");
            }
            else
                throw new Exception("File format not managed");
        }

        #endregion

        // ---

        public bool CheckIfInjectionNeeded(string machineName)
        {
            // --- Lecture du fichier source
            string platformsFile = Path.Combine(Config.HLaunchBoxPath, Config.PlatformsFile);

            bool? write = true;

            bool? resPres = XML_Custom.TestPresence(platformsFile, Tag.Platform, Tag.Name, machineName);

            // On demande pour REMPLACER la machine si elle existe déjà
            if (resPres == true)
                write = IHMStatic.AskDxMBox(
                    "Platform is Already present, replace it ?", "Question",
                    E_DxButtons.Yes | E_DxButtons.No, machineName);

            return write ?? false;
        }


        public void InjectPlatform(string platform, string newPFile)
        {
            string platformsFile = Path.Combine(Config.HLaunchBoxPath, Config.PlatformsFile);

            using (XML_Platforms xPlat = new XML_Platforms(platformsFile))
            {
                // Injection en mode true verbatim                
                if (xPlat.InjectPlatformExt(newPFile))
                {
                    HeTrace.WriteLine($"Platform {platform} injected");
                    xPlat.Save(platformsFile);
                }
            }
        }


        protected bool CopyFiles(GameDataCont gdC)
        {
            var files = new List<DataTrans>();
            files.AddRange(gdC.Applications);
            files.AddRange(gdC.Manuals);
            files.AddRange(gdC.Musics);
            files.AddRange(gdC.Videos);

            CopyFExt copyObj = new CopyFExt();
            copyObj.AskToUser += IHMStatic.Ask4_FileConflict2;
            copyObj.UpdateStatus += (x, y) => HeTrace.WriteLine(y.Message);
            copyObj.UpdateStatusT += (x, y) => HeTrace.WriteLine(y.Message);

            bool resultat = false;
            SafeBoxes.LaunchDouble(copyObj, () => resultat = copyObj.CopyN(files), "Copy files");

            if (!resultat)
                return false;

            SafeBoxes.LaunchDouble(copyObj, () => resultat = copyObj.CopyN(gdC.Images), "Copy Images files");

            return resultat;
        }


        /*
        /// <summary>
        /// Travail sur le fichier xml contenant les plateformes
        /// </summary>
        /// <param name="platformsFile"></param>
        /// <param name="machineName"></param>
        private void WorkOnPlatformFile(string platformsFile, string machineName)
        {
            using (XML_Platforms xPlat = new XML_Platforms(platformsFile))
            {
                // Backup du fichier de la plateforme;
                BackupPlatformsFile(platformsFile);

                // On demande pour REMPLACER la machine si elle existe déjà
                if (xPlat.Exists("Name", machineName))
                {
                    bool? res = AskDxMBox("Platform is Already present, replace it ?", "Question", E_DxButtons.Yes | E_DxButtons.No, machineName);
                    if (res == true)
                    {
                        xPlat.RemoveElemByChild(Tag.Platform, Tag.Name, machineName);
                        xPlat.RemoveElemByChild(Tag.PlatformFolder, Tag.Platform, machineName);
                        UpdateStatus?.Invoke(this, "The has been removed");
                    }
                    else
                    {
                        UpdateStatus?.Invoke(this, "Injection aborted");
                        return;
                    }
                }

                // Injection en mode true verbatim
                XElement verbatimPlatform = XML_Platforms.GetPlatformNode(_SelectedPlatformXML);
                xPlat.InjectPlatform(verbatimPlatform);
                var verbatimPFolders = XML_Platforms.GetFoldersNodes(_SelectedPlatformXML);
                xPlat.InjectPlatFolders(verbatimPFolders);

                UpdateStatus?.Invoke(this, $"Platform {machineName} injected");

                xPlat.Save(platformsFile);
            }
        }*/





        /*
        /// <summary>
        /// Copie les images
        /// </summary>
        /// <param name="srcFolder"></param>
        protected void Copy_Images(string srcFolder, string destination)
        {
            foreach (string d in Directory.GetDirectories(srcFolder))
            {
         //       string dirName = Path.GetFileName(d);

                UpdateStatus?.Invoke(this, $"\t{Lang.I_Copy} {Lang.Images} '{srcFolder}' => '{TImagesP}'");

                CopyContent(d, destination);
            }
        }*/

        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dossier"></param>
        /// <param name="folderPath">Dossier de destination</param>
        /// <remarks>
        /// Recherche le répertoire définitif avant de copier, conserve la structure.
        /// </remarks>
        protected void CopyContent(string dossier, string folderPath)
        {
            if (string.IsNullOrEmpty(folderPath))
                return;

            folderPath = Path.GetFullPath(folderPath, PS.Default.LastLBpath);
            string target = null;

            // récupération des fichiers
            foreach (string f in Directory.EnumerateFiles(dossier, "*.*", SearchOption.AllDirectories))
            {
                if (CancelToken.IsCancellationRequested)
                    return;

                try
                {
                    UpdateStatus?.Invoke(this, $"\t{Lang.Source}: {f}");

                    string tail = f.Replace(dossier, "");
                    Debug.WriteLine($"t:{tail}");

                    target = Path.Combine(folderPath, tail.Substring(1));
                    UpdateStatus?.Invoke(this, $"\t{Lang.Target}: {target}");


                    Directory.CreateDirectory(Path.GetDirectoryName(target));

                    File.Copy(f, target, true);
                }
                catch (IOException ioex)
                {
                    UpdateStatus?.Invoke(this, $"\t***Error copy {target}\r\n {ioex.Message}");
                }
                catch (UnauthorizedAccessException UnAuthExc)
                {
                    UpdateStatus?.Invoke(this, $"\t{UnAuthExc.Message}");

                }
                catch (Exception exc)
                {
                    UpdateStatus?.Invoke(this, $"\t{exc.Message}");
                }
            }
            //            Debug.WriteLine($"Copy Simu: {d} <|> {folderPath}");
        }*/


        protected void Delete(string tmpPath)
        {
            try
            {
                Directory.Delete(tmpPath, true);

            }
            catch (UnauthorizedAccessException uae)
            {
                Application.Current.Dispatcher?.Invoke(() =>
                   {
                       DxMBox.ShowDial(Lang.I_DelByYourself, "Info", DxTBoxCore.Common.E_DxButtons.Ok, $"{DxTBLang.Delete}: {tmpPath}");

                   });

                Process.Start("explorer.exe", @$"{tmpPath}");
            }
        }


        public abstract void StopTask();
        public abstract void Pause(int timeSleep);
    }
}
