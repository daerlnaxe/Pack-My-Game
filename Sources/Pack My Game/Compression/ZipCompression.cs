using DxLocalTransf;
using DxLocalTransf.Progress;
using Ionic.Zip;
using Ionic.Zlib;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

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
    class ZipCompression: I_AsyncSigD
    {
        public delegate bool DecisionHandler(string message, string title);

        public event DoubleHandler UpdateProgress;
        public event MessageHandler UpdateStatus;
        public event MessageHandler UpdateStatusNL;
        public event DoubleHandler MaximumProgress;

        public event DoubleHandler UpdateProgressT;
        public event MessageHandler UpdateStatusT;
        public event MessageHandler UpdateStatusTNL;
        public event DoubleHandler MaximumProgressT;

        public static event DecisionHandler Error;

        public bool IsPaused { get; set; }

        /// <summary>
        /// Répertoire de destination
        /// </summary>
        public string DestinationFolder { get; internal set; }

        public CancellationTokenSource TokenSource { get; set; }

        public CancellationToken CancelToken => TokenSource.Token;

        public string ArchiveLink { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="destinationFolder"></param>
        /// <remarks>
        /// Creation of folder destination
        /// </remarks>
        public ZipCompression(string destinationFolder)
        {
            if (string.IsNullOrEmpty(destinationFolder))
                throw new ArgumentNullException(destinationFolder);


            DestinationFolder = destinationFolder;

            if (!Directory.Exists(DestinationFolder))
                Directory.CreateDirectory(destinationFolder);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder">Folder to Compress</param>
        /// <param name="archiveDest">Enter a name without extension</param>
        /// <param name="cplLvl">Niveau de compression 0/1/2</param>
        /// <returns></returns>
        public bool CompressFolder(string folder, string archiveDest, int cplLvl)
        {
            if (!Directory.Exists(folder))
                throw new FileNotFoundException(folder);

            ArchiveLink = archiveDest + ".zip";

            // Verification
            if (File.Exists(ArchiveLink))
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


            while (true)    // Le while est là pour essayer de régler un problème au lieu de simplement stopper
            {
                try
                {
                    // Get value from enum
                    CompressionLevel cpLvl = (CompressionLevel)cplLvl;
                    using (ZipFile zipFile = new ZipFile())
                    {
                        //zipFile.CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression;
                        zipFile.CompressionLevel = cpLvl;
                        //zipFile.BufferSize = 4096;
                        zipFile.AddProgress += AddProgress;
                        zipFile.SaveProgress += SaveProgress;

                        MaximumProgress?.Invoke(this,100);

                        UpdateStatusNL?.Invoke(this, "Compression Zip");

                        //Task.Run(() => zipFile.Save(zipName));
                        zipFile.AddDirectory(folder);
                        zipFile.Save(ArchiveLink);


                    }

                    return true;
                }
                catch (Exception e)
                {
                    bool? res = Error?.Invoke($"Erreur lors de la compression zip, merci de régler le problème et decliquer sur retry:\n{e}", "Error");
                    if (res == true)
                        return false;

                    throw new Exception(e.Message);
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
        private void SaveProgress(object sender, SaveProgressEventArgs e)
        {
            // On peut placer le cancel token ici
            if (CancelToken.IsCancellationRequested)
                throw new OperationCanceledException();


            // Begin compression
            // Note: on ne peut pas avoir le nombre total
            if (e.EventType == ZipProgressEventType.Saving_Started)
            {
                UpdateStatusNL?.Invoke(this, $"Début de la compression vers: {e.ArchiveName}");                
            }

            // Création d'un dossier => On indique la tâche en cours + raz barre de progression
            else if (e.EventType == ZipProgressEventType.Saving_BeforeWriteEntry && e.CurrentEntry.IsDirectory)
            {
                string txt = $"Folder Creation: { e.CurrentEntry.FileName}";
                Debug.WriteLine($"\tspA {txt}");

                UpdateProgress?.Invoke(this, 0);
                UpdateStatusNL?.Invoke(this, e.CurrentEntry.FileName);
            }

            // Intervient au début de la compression du fichier && exclusion des dossiers => Tâche en cours + raz barre de progression
            else if (e.EventType == ZipProgressEventType.Saving_BeforeWriteEntry && !e.CurrentEntry.IsDirectory)
            {
                string txt = $"File to Compression: { e.CurrentEntry.FileName}";

                Debug.WriteLine($"\tspA {txt}");

                UpdateProgress?.Invoke(this, 0);
                UpdateStatusNL?.Invoke(this, e.CurrentEntry.FileName);
                MaximumProgressT?.Invoke(this, e.EntriesTotal);
            }

            // Progress: Envoie de X>0 jusqu'au 100% => Only progress EntryBar
            else if (e.EventType == ZipProgressEventType.Saving_EntryBytesRead)
            {
                Debug.WriteLine($"\t\tspP Transfert saved {e.BytesTransferred} / {e.TotalBytesToTransfer}");
                //     BoxProgress.dProgress.EntryCalcUpdate(e.BytesTransferred, e.TotalBytesToTransfer);

                if (e.BytesTransferred < e.TotalBytesToTransfer)
                    Debug.WriteLine("");

                this.MaximumProgress?.Invoke(this, e.TotalBytesToTransfer);
                this.UpdateProgress?.Invoke(this, e.BytesTransferred);
                Task.Delay(5000);
            }

            // Intervient à la fin de la compression du fichier ou des dossiers => on update le global
            else if (e.EventType == ZipProgressEventType.Saving_AfterWriteEntry)
            {
                Debug.WriteLine($"\t\tspE Entries saved {e.EntriesSaved} / {e.EntriesTotal}");
                //   BoxProgress.dProgress.GlobalProgress(e.EntriesSaved, e.EntriesTotal);
                this.UpdateProgressT?.Invoke(this, e.EntriesSaved);
            }

            // End of Compression
            else if (e.EventType == ZipProgressEventType.Saving_Completed)
            {
                Debug.WriteLine("Done");
                UpdateStatusNL?.Invoke(this, "Done");
           //     BoxProgress.StopIt();

            }
            else
            {
                Debug.WriteLine($"======> Non géré {e.EventType} ");
            }
        }

        private static void AddProgress(object sender, AddProgressEventArgs e)
        {
            Debug.WriteLine($"ap {e.BytesTransferred} || {e.TotalBytesToTransfer}");
        }





        public void StopTask()
        {
            throw new NotImplementedException();
        }
    }
}
