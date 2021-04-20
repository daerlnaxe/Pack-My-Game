using AsyncProgress;
using AsyncProgress.Basix;
using Common_PMG;
using Common_PMG.Container;
using Common_PMG.XML;
using DxTBoxCore.Common;
using DxTBoxCore.Languages;
using DxTBoxCore.MBox;
using Hermes;
using Hermes.Cont;
using Hermes.Messengers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using UnPack_My_Game.Decompression;
using UnPack_My_Game.Graph;
using UnPack_My_Game.Resources;
using PS = UnPack_My_Game.Properties.Settings;

namespace UnPack_My_Game.Cores
{
    internal abstract class C_LaunchBox : A_ProgressPersistD, I_ASBase
    {

        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();
        public CancellationToken CancelToken { get; }

        public bool CancelFlag { get; private set; }

        public bool IsPaused { get; set; }

        public bool IsInterrupted { get; private set; }

        public abstract bool InjectGames(ICollection<DataRep> folders);

        #region Depacking

        internal bool Depacking(ObservableCollection<DataRep> games)
        {
            ProgressTotal = games.Count();
            try
            {
                List<DataRep> folders = new List<DataRep>();

                // Dépack
                foreach (DataRep game in games)
                {
                    string tmpPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(game.Name));

                    Depacking(game, tmpPath);

                    folders.Add(new DataRep(tmpPath));
                }

                // Injection
                InjectGames(folders);

                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }


        public bool Depacking(DataRep g, string tmpPath)
        {
            string fileExt = Path.GetExtension(g.ALinkToThePath).TrimStart('.');


            // Décompression
            // Détection du mode
            if (fileExt.Equals("zip", StringComparison.OrdinalIgnoreCase))
                DepackZip(g.ALinkToThePath, tmpPath);
            else if (fileExt.Equals("7zip", StringComparison.OrdinalIgnoreCase) || fileExt.Equals("7z", StringComparison.OrdinalIgnoreCase))
                Depack7Zip(g.ALinkToThePath, tmpPath);
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

            IHMStatic.LaunchDouble(zippy, () => zippy.ExtractAll(pathLink, tmpPath), "Zip Extraction");
        }


        private void Depack7Zip(string pathLink, string tmpPath)
        {
            throw new NotImplementedException();
        }

        #endregion Depacking



        public bool CheckIfInjectionNeeded(string machineName)
        {
            // --- Lecture du fichier source
            string platformsFile = Path.Combine(PS.Default.LastLBpath, PS.Default.fPlatforms);

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
            string platformsFile = Path.Combine(PS.Default.LastLBpath, PS.Default.fPlatforms);

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
