using AsyncProgress;
using AsyncProgress.Cont;
using AsyncProgress.ToImp;
using Ionic.Zip;
using Ionic.Zlib;
using System;
using System.Diagnostics;
using System.IO;

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
    class ZipCompression : A_ASBase, I_AsyncSigD
    {
        public delegate bool DecisionHandler(string message, string title);

        public static event DecisionHandler Error;

        public event ProgressHandler UpdateProgress;
        public event StateHandler UpdateStatus;

        public event ProgressHandler UpdateProgressT;
        public event StateHandler UpdateStatusT;

        /// <summary>
        /// Permet de régler un timer pour le calcul des pourcentages des progressions et les émissions
        /// du signal
        /// </summary>
        /// <remarks>
        /// Milliseconds
        /// </remarks>
        public int InternalTimer { get; set; }
        Stopwatch _SW = new Stopwatch();

        /// <summary>
        /// Répertoire de destination
        /// </summary>
        public string DestinationFolder { get; internal set; }



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

                        //MaximumProgress?.Invoke(this, 100);

                        UpdateStatus?.Invoke(this, new StateArg( "Compression Zip", CancelFlag));

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
            {
                e.Cancel = true;
                IsInterrupted = true;
                /*zip.Dispose();
                throw new OperationCanceledException("Zip Compression stopped by user");*/
            }


            switch (e.EventType)
            {
                // Begin compression
                // --- Note: on ne peut pas avoir le nombre total
                case ZipProgressEventType.Saving_Started:
                    _SW.Restart();
                    UpdateStatus?.Invoke(this, new StateArg( $"Début de la compression vers: {e.ArchiveName}", CancelFlag));
                    break;

                case ZipProgressEventType.Saving_BeforeWriteEntry:
                   // UpdateProgress?.Invoke(this, 0);
                    UpdateStatus?.Invoke(this, new StateArg( e.CurrentEntry.FileName, CancelFlag));

                    string txt = null;
                    // --- Création d'un dossier => On indique la tâche en cours + raz barre de progression
                    if (e.CurrentEntry.IsDirectory)
                    {
                        txt = $"Folder Creation: { e.CurrentEntry.FileName}";
                    }
                    // -- Intervient au début de la compression du fichier && exclusion des dossiers => Tâche en cours + raz barre de progression
                    else
                    {
                        txt = $"File to Compression: { e.CurrentEntry.FileName}";
                     //   MaximumProgressT?.Invoke(this, e.EntriesTotal);
                    }

                    Debug.WriteLine($"\tspA {txt}");
                    break;

                // ---  Progress: Envoie de X>0 jusqu'au 100% => Only progress EntryBar
                case ZipProgressEventType.Saving_EntryBytesRead:
                    Debug.WriteLine($"\t\tspP Transfert saved {e.BytesTransferred} / {e.TotalBytesToTransfer}");
                    //     BoxProgress.dProgress.EntryCalcUpdate(e.BytesTransferred, e.TotalBytesToTransfer);
                    if (e.BytesTransferred < e.TotalBytesToTransfer)
                        Debug.WriteLine("");

                    //this.MaximumProgress?.Invoke(this, e.TotalBytesToTransfer);
                    this.UpdateProgress?.Invoke(this, new ProgressArg( e.BytesTransferred, e.TotalBytesToTransfer, CancelFlag));
                    //Task.Delay(5000);
                    break;

                // Intervient à la fin de la compression du fichier ou des dossiers => on update le global
                case ZipProgressEventType.Saving_AfterWriteEntry:
                    Debug.WriteLine($"\t\tspE Entries saved {e.EntriesSaved} / {e.EntriesTotal}");
                    //   BoxProgress.dProgress.GlobalProgress(e.EntriesSaved, e.EntriesTotal);
                    this.UpdateProgressT?.Invoke(this, new ProgressArg( e.EntriesSaved, e.EntriesTotal, CancelFlag));
                    break;

                // End of Compression
                case ZipProgressEventType.Saving_Completed:
                    _SW.Stop();
                    Debug.WriteLine("Done");
                    //     BoxProgress.StopIt();
                    UpdateStatus?.Invoke(this, new StateArg( "Done", CancelFlag));
                    break;

                default:
                    Debug.WriteLine($"======> Non géré {e.EventType} ");
                    break;
            };

        }

        private static void AddProgress(object sender, AddProgressEventArgs e)
        {
            Debug.WriteLine($"ap {e.BytesTransferred} || {e.TotalBytesToTransfer}");
        }





        public override void StopTask()
        {
            TokenSource.Cancel();
        }
    }
}
