﻿using DxTBoxCore.Box_Progress;
using DxTBoxCore.Languages;
using DxTBoxCore.MBox;
using Hermes;
using LaunchBox_XML.BackupLB;
using LaunchBox_XML.Container;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using UnPack_My_Game.Cont;
using UnPack_My_Game.Graph.LaunchBox;
using UnPack_My_Game.Resources;
using PS = UnPack_My_Game.Properties.Settings;

namespace UnPack_My_Game.Cores
{
    internal abstract class C_LaunchBox : I_ASBaseC
    {
        #region events
        public abstract event DoubleDel UpdateProgressT;
        public abstract event StringDel UpdateStatusT;
        public abstract event DoubleDel MaximumProgressT;

        public virtual event DoubleDel UpdateProgress;
        public virtual event StringDel UpdateStatus;
        public virtual event DoubleDel MaximumProgress;
        #endregion

        // ---

        #region Async
        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        public abstract CancellationToken CancelToken { get; }

        public bool IsPaused { get; set; }
        #endregion


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



        public abstract object Run(int timeSleep = 10);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="machine">Machine cible</param>
        /// <param name="games"></param>
        internal void Run(string baseLB, string machine, ref List<FileObj> games)
        {


        }








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
                    UpdateStatus?.Invoke($"\t{Lang.Source}: {f}");

                    string tail = f.Replace(dossier, "");
                    Debug.WriteLine($"t:{tail}");

                    target = Path.Combine(folderPath, tail.Substring(1));
                    UpdateStatus?.Invoke($"\t{Lang.Target}: {target}");


                    Directory.CreateDirectory(Path.GetDirectoryName(target));

                    File.Copy(f, target, true);
                }
                catch (IOException ioex)
                {
                    UpdateStatus?.Invoke($"\t***Error copy {target}\r\n {ioex.Message}");
                }
                catch (UnauthorizedAccessException UnAuthExc)
                {
                    UpdateStatus?.Invoke($"\t{UnAuthExc.Message}");

                }
                catch (Exception exc)
                {
                    UpdateStatus?.Invoke($"\t{exc.Message}");
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


        public void StopTask()
        {
            throw new NotImplementedException();
        }
    }
}