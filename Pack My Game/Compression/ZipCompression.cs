using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Compression;
using System.Threading.Tasks;
using Ionic.Zip;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using Pack_My_Game.IHM;
using DlnxLocalTransfert;
using DxTrace;

namespace Pack_My_Game.Compression
{
    /* Notes personnelles:
     *      e.CurrentEntry: Nom du fichier en cours
     *      e.TotalBytesToTransfer: Taille du fichier à compresser
     *      e.BytesTransferred: Etat de la compression
     *      e.archive: Nom de l'archive de sortie
     *      
     *      .saveprogress: event lors de la compression
     */
    class ZipCompression
    {

        static ProgressCompFolder BoxProgress;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder">Folder to Compress</param>
        /// <param name="archiveDest">Enter a name without extension</param>
        /// <param name="cplLvl">Niveau de compression 0/1/2</param>
        /// <returns></returns>
        public static bool CompressFolder(string folder, string archiveDest, int cplLvl)
        {

            if (!Directory.Exists(folder))
                return false;

            string zipName = archiveDest + ".zip";

            // Verification
            if (File.Exists(zipName))
            {
                //var res = MessageBox.Show(", do you want to delete it or abort ?", "ok", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                ////var res = MB_Decision.Show("Zip File Exists for this game", "Alert", destination: zipName, buttons: MB_Decision.Mode.NoStop);
                ////if (res == MB_Decision.Result.Pass)
                ////{
                ////    //MessageBox.Show("Zip Compression Aborted", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ////    return false;
                ////}
                ////else if (res == MB_Decision.Result.Trash)
                ////{
                ////    // Move to Recycle.Bin
                ////    try
                ////    {
                ////        FileSystem.DeleteDirectory(zipName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                ////    }
                ////    catch (Exception e)
                ////    {
                ////        Debug.WriteLine(e.Message);
                ////    }
                ////}
            }


            while (true)
            {

                try
                {
                    // Get value from enum
                    CompressionLevel cpLvl = (CompressionLevel)cplLvl;
                    using (Ionic.Zip.ZipFile zipFile = new Ionic.Zip.ZipFile())
                    {
                        zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                        //zipFile.BufferSize = 4096;
                        zipFile.AddDirectory(folder);
                        zipFile.AddProgress += addProgress;

                        zipFile.SaveProgress += SaveProgress;

                        BoxProgress = new ProgressCompFolder();
                        BoxProgress.Text = "Compression Zip";

                        Task.Run(() => zipFile.Save(zipName));

                        BoxProgress.ShowDialog();
                    }

                    return true;
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Erreur de compression {e.Message}");
                    var res = MessageBox.Show($"Erreur lors de la compression zip, merci de régler le problème et decliquer sur retry:\n{e}", "Erreur", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Exclamation);

                    // Exit if abort
                    if (res == DialogResult.Abort) return false;
                }
            }

        }


        /*
         * 
         * EX de cycle
         *      BeforeWriteEntry:
         *          - init total 0/42   || 4/42 (des folders entre)
         *          - 0/0               || 0/0
         *      Progress                                                +1 cycle car pas fini
         *          - 0/0               || 0/0 les fichiers traités sont remis à 0 à ce moment 
         *          - 924/924           || 32768/40353                  40353/40353
         *      End:
         *          - 1/42              || 5/42
         *          - 0/0               || 0/0             !Attention remet à 0 les bytes lus!
         */
        private static void SaveProgress(object sender, SaveProgressEventArgs e)
        {
            // Begin compression
            // Note: on ne peut pas avoir le nombre total
            if (e.EventType == ZipProgressEventType.Saving_Started)
            {
                Debug.WriteLine($"Début de la compression vers: {e.ArchiveName}");

                BoxProgress.dProgress.InfoSup = e.ArchiveName;
            }

            // Création d'un dossier => On indique la tâche en cours + raz barre de progression
            else if (e.EventType == ZipProgressEventType.Saving_BeforeWriteEntry && e.CurrentEntry.IsDirectory)
            {
                string txt = $"Folder Creation: { e.CurrentEntry.FileName}";
                Debug.WriteLine($"\tspA {txt}");

                BoxProgress.dProgress.EntryInit();
                BoxProgress.dProgress.CurrentInfo =e.CurrentEntry.FileName;
            }

            // Intervient au début de la compression du fichier && exclusion des dossiers => Tâche en cours + raz barre de progression
            else if (e.EventType == ZipProgressEventType.Saving_BeforeWriteEntry && !e.CurrentEntry.IsDirectory)
            {
                string txt = $"File to Compression: { e.CurrentEntry.FileName}";

                Debug.WriteLine($"\tspA {txt}");

                BoxProgress.dProgress.EntryInit();
                BoxProgress.dProgress.CurrentInfo = e.CurrentEntry.FileName;
            }

            // Progress: Envoie de X>0 jusqu'au 100% => Only progress EntryBar
            else if (e.EventType == ZipProgressEventType.Saving_EntryBytesRead)
            {
                Debug.WriteLine($"\t\tspP Transfert saved {e.BytesTransferred} / {e.TotalBytesToTransfer}");
                BoxProgress.dProgress.EntryCalcUpdate(e.BytesTransferred, e.TotalBytesToTransfer);
            }

            // Intervient à la fin de la compression du fichier ou des dossiers => on update le global
            else if (e.EventType == ZipProgressEventType.Saving_AfterWriteEntry)
            {
                Debug.WriteLine($"\t\tspE Entries saved {e.EntriesSaved} / {e.EntriesTotal}");
                BoxProgress.dProgress.GlobalProgress(e.EntriesSaved, e.EntriesTotal);
            }

            // End of Compression
            else if (e.EventType == ZipProgressEventType.Saving_Completed)
            {
                Debug.WriteLine("Done");
                BoxProgress.dProgress.CurrentInfo= "Done";
                BoxProgress.StopIt();

            }
            else
            {
                Debug.WriteLine($"======> Non géré {e.EventType} ");
            }
        }

        private static void addProgress(object sender, AddProgressEventArgs e)
        {
            Console.WriteLine($"ap {e.BytesTransferred} || {e.TotalBytesToTransfer}");

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder2Comp">Dossier à compresser</param>
        /// <param name="path">Chemin de stockage de l'archive</param>
        /// <param name="gameName">Lien vers </param>
        /// <returns></returns>
        public static bool Make_Folder(string folder2Comp, string path, string gameName)
        {
/*
            #region 2020 choix du nom

            GameName gnWindows = new GameName();
            gnWindows.SuggestedGameName = gameName;
            gnWindows.ShowDialog();

            //string destArchLink = Path.Combine(path, $"{destArchive}");
            string destArchLink = Path.Combine(path, gnWindows.ChoosenGameName);

            #endregion
*/
            string destArchLink = Path.Combine(path, gameName);

            //string destArchLink = Path.Combine(path, $"{destArchive}.7z");
            //string destArchLink = Path.Combine(path, $"{destArchive}.zip");

            var zipRes = OPFiles.SingleVerif(destArchLink, "Make_Zip", log: (string message) => ITrace.WriteLine(message, true));

            switch (zipRes)
            {
                case OPResult.OverWrite:
                case OPResult.Ok:
                case OPResult.Trash:
                    if (!ZipCompression.CompressFolder(folder2Comp, destArchLink, Properties.Settings.Default.cZipCompLvl))
                    {
                        ITrace.WriteLine("[Make_Zip] Zip Compression canceled");
                        return false;
                    }
                    ITrace.WriteLine($"[Make_Zip] Zip Compression begin");
                    return true;

                default:
                    return false;
            }
        }
    }
}
