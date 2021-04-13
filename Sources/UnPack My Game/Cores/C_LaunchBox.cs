using DxLocalTransf;
using DxLocalTransf.Progress.ToImp;
using DxTBoxCore.Box_Progress;
using DxTBoxCore.Common;
using DxTBoxCore.Languages;
using DxTBoxCore.MBox;
using Hermes;
using Hermes.Cont;
using Hermes.Messengers;
using Common_PMG.Container.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using UnPack_My_Game.Decompression;
using UnPack_My_Game.Graph.LaunchBox;
using UnPack_My_Game.Resources;
using PS = UnPack_My_Game.Properties.Settings;
using Common_PMG.Container;
using DxLocalTransf.Progress;

namespace UnPack_My_Game.Cores
{
    internal abstract class C_LaunchBox : I_ASBase
    {
        #region events
        public abstract event DoubleHandler UpdateProgressT;
        public abstract event MessageHandler UpdateStatusT;
        public virtual event DoubleHandler MaximumProgressT;

        public virtual event DoubleHandler UpdateProgress;
        public virtual event MessageHandler UpdateStatus;
        public virtual event DoubleHandler MaximumProgress;
        #endregion

        // ---

        #region Async
        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        public abstract CancellationToken CancelToken { get; }

        public bool IsPaused { get; set; }
        #endregion

        public List<DataRep> Games { get; }


        protected string TGamesP { get; set; }
        protected string TCheatsCodesP { get; set; }
        protected string TImagesP { get; set; }
        protected string TManualsP { get; set; }
        protected string TMusicsP { get; set; }
        protected string TVideosP { get; set; }

        /// <summary>
        /// Indique quel mode 
        /// </summary>
        public E_Method Mode { get; internal set; }


        public C_LaunchBox(string logTitle, List<DataRep> games)
        {
            Games = games;

            // Tracing
            MeSimpleLog log = new MeSimpleLog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.Logs, $"{DateTime.Now.ToFileTime()}.log"))
            {
                LogLevel = 1,
                FuncPrefix = EPrefix.Horodating,
            };

            log.AddCaller(this);
            HeTrace.AddLogger(logTitle, log);

            UpdateStatus += (x, y) => HeTrace.WriteLine(y, this);

            //
            MaximumProgressT?.Invoke(this, games.Count);
            MaximumProgress?.Invoke(this, 100);
        }

        /*
        /// <summary>
        /// 
        /// </summary>
        /// <param name="machine">Machine cible</param>
        /// <param name="games"></param>
        internal void Run(string baseLB, string machine, ref List<FileObj> games)
        {


        }
        */
        /*
        protected void RedirectSignals()
        {

        }
        */





        /// <summary>
        /// Backup Platform xml file
        /// </summary>
        /// <param name="machineSrc"></param>
        protected static void BackupPlatformFile(string machineSrc)
        {
            string targetfP = Path.Combine(
                                AppDomain.CurrentDomain.BaseDirectory,
                                Common.BackUp,
                                $"{Path.GetFileNameWithoutExtension(machineSrc)} - ");

            for (int i = 00; i < 100; i++)
            {
                string tempo = targetfP + i.ToString("00") + ".xml";


                if (!File.Exists(tempo))
                {
                    targetfP = tempo;
                    break;
                }

                if (i == 99)
                    targetfP += "00.xml";
            }

            //File.Copy(machine,);
            File.Copy(machineSrc, targetfP, true);
        }


                // --- Backup
        /// <summary>
        /// Backup Platform xml file
        /// </summary>
        /// <param name="platformsFile"></param>
        protected static void BackupPlatformsFile(string platformsFile)
        {
            string targetfP = Path.Combine(
                                AppDomain.CurrentDomain.BaseDirectory,
                                Common.BackUp,
                                $"{Path.GetFileNameWithoutExtension(platformsFile)} - ");

            for (int i = 00; i < 100; i++)
            {
                string tempo = targetfP + i.ToString("00") + ".xml";


                if (!File.Exists(tempo))
                {
                    targetfP = tempo;
                    break;
                }

                if (i == 99)
                    targetfP += "00.xml";
            }

            //File.Copy(machine,);
            File.Copy(platformsFile, targetfP, true);
        }

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
        }


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
        }


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

        protected bool? AskDxMBox(string message, string title, E_DxButtons buttons, string optionnalMessage = null)
        {
            bool? res = false;
            Application.Current.Dispatcher?.Invoke(() =>
            {
                res = DxMBox.ShowDial(message, title, buttons, optionnalMessage);
            }
                );

            return res;
        }
        public void StopTask()
        {
            throw new NotImplementedException();
        }
    }
}
