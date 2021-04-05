
using DxLocalTransf;
using DxTBoxCore.Box_Progress;
using Pack_My_Game.IHM;
using SevenZip;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Pack_My_Game.Compression
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Modèle avec Task</remarks>
    class SevenZipCompression : DxLocalTransf.Progress.ToImp.I_ASBase
    {
        public delegate bool DecisionHandler(string message, string title);


        public event DoubleHandler UpdateProgress;
        public event MessageHandler UpdateStatus;
        public event DoubleHandler MaximumProgress;

        public static event DecisionHandler Error;

        //
        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        public CancellationToken CancelToken => TokenSource.Token;

        public bool IsPaused { get; set; }

        //
        //static string _ArchiveName;
        public string DestinationFolder { get; internal set; }

        /// <summary>
        /// Archive link
        /// </summary>
        public string ArchiveLink { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destinationFolder"></param>
        /// <remarks>
        /// Creation of folder destination
        /// </remarks>
        public SevenZipCompression(string destinationFolder)
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
        /// <param name="folder">Folder</param>
        /// <param name="archiveDest">Archive name</param>
        /// <param name="cplLvl">Level of compression</param>
        /// <returns></returns>
        public bool CompressFolder(string folder, string archiveDest, int cplLvl)
        {
            if (!Directory.Exists(folder))
                throw new FileNotFoundException(folder);

            //  string archiveName = archiveDest + ".7z";
            ArchiveLink = archiveDest + ".7z";

            //string sSeventZipLink = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "x86", "7z.dll");
            string sSeventZipLink = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Environment.Is64BitProcess ? "x64" : "x86", "7z.dll");

            //var sevenZipPath = Path.Combine( Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),Environment.Is64BitProcess ? "x64" : "x86", "7z.dll");
            if (!File.Exists(sSeventZipLink))
                throw new Exception("7Zip dll Missing you must copy the dll according to the version at the root");

            SevenZipCompressor.SetLibraryPath(sSeventZipLink);
            //  var k = SevenZipCompressor.CurrentLibraryFeatures;

            SevenZipCompressor szp = new SevenZipCompressor();

            szp.CompressionLevel = CompressionLevel.Ultra;
            szp.CompressionMode = CompressionMode.Create;
            szp.ArchiveFormat = OutArchiveFormat.SevenZip;
            szp.DirectoryStructure = true;

            while (true)    // Le while est là pour essayer de régler un problème au lieu de simplement stopper
            {
                try // conserver le try ? pas sûr
                {
                    szp.FilesFound += FilesFound;
                    szp.Compressing += Compressing;

                    szp.FileCompressionStarted += FileCompressionStarted;
                    szp.FileCompressionFinished += FileCompressionFinished;
                    szp.CompressionFinished += CompressionFinished;

                    string tmp = $"{Path.GetRandomFileName()}.7z.tmp";
                    szp.CompressDirectory(folder, Path.Combine(DestinationFolder, tmp));

                    // on renomme
                    File.Move(tmp, ArchiveLink);

                    return true;
                }
                catch (Exception e)
                {
                    bool? res = Error?.Invoke($"Erreur lors de la compression 7zip, merci de régler le problème et decliquer sur retry:\n{e}", "Error");
                    if (res == true)
                        return false;

                    throw new Exception(e.Message);

                }
            }

            return true;
        }


        //avant compressing - #1
        private void FilesFound(object sender, IntEventArgs e)
        {
            //BoxProgress.dProgress.InfoSup = _ArchiveName;
            // BoxProgress.FilesDone = 0;
            //BoxProgress.dProgress.GlobalProgress(BoxProgress.dProgress.FilesDone, BoxProgress.dProgress.TotalFiles);

            //BoxProgress.dProgress.TotalFiles = e.Value;

            //this.MaximumProgressT?.Invoke(100);
            Debug.WriteLine($"Files: {e.Value} Found");
        }

        // Se produit après files found
        private void FileCompressionStarted(object sender, FileNameEventArgs e)
        {
            Debug.WriteLine($"File being compressed: {e.FileName}");

            //BoxProgress.dProgress.EntryUpdate(e.PercentDone);

            //BoxProgress.dProgress.CurrentInfo = e.FileName;
            this.UpdateStatus?.Invoke(this, e.FileName);

            //BoxProgress.AsyncUpdateEntryTxt(e.PercentDone);
            Debug.WriteLine($"fcs Percent done: {e.PercentDone}%");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// Niveau de compression
        /// </remarks>
        private void Compressing(object sender, ProgressEventArgs e)
        {
            //BoxProgress.dProgress.EntryUpdate(e.PercentDone);
            this.UpdateProgress?.Invoke(this, e.PercentDone);

            //BoxProgress.AsyncUpdateEntryTxt(e.PercentDone);            

            Debug.WriteLine($"\t\tCompressing - {e.PercentDone} %"); // on dirait la compression totale
            Debug.WriteLine($"\t\tCompressing - Delta: {e.PercentDelta}");
        }

        private void FileCompressionFinished(object sender, EventArgs e)
        {
            //Apparement aucune info si ce n'est que c'est fini
            //  BoxProgress.dProgress.FilesDone++;

            //  BoxProgress.dProgress.EntryUpdate(100);
            //   BoxProgress.dProgress.GlobalProgress(BoxProgress.dProgress.FilesDone, BoxProgress.dProgress.TotalFiles);

            Debug.WriteLine($"Compression du fichier terminée{e.ToString()}");
        }

        // #?
        private void CompressionFinished(object sender, EventArgs e)
        {
            //Aucune info nonplus
            /*int totalFiles = BoxProgress.dProgress.TotalFiles;
       
            /*BoxProgress.dProgress.FilesDone = totalFiles;
            BoxProgress.dProgress.GlobalProgress(totalFiles, totalFiles);

            Console.WriteLine($"Compression tout fini{e.ToString()}");
            BoxProgress.StopIt();*/

            this.UpdateStatus?.Invoke(this, "Finished");
            Thread.Sleep(500);
        }

        public void StopTask()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="destArchive"></param>
        /// <param name="path">Work folder</param>
        /// <param name="folder2Comp"></param>
        /// <returns></returns>
        /*
    public static bool Make_Folder(string folder2Comp, string path, string gameName)
    {
        #region 2020 choix du nom
        GameName gnWindows = new GameName();
        gnWindows.SuggestedGameName = destArchive;
        gnWindows.ShowDialog();

        //string destArchLink = Path.Combine(path, $"{destArchive}");
        string destArchLink = Path.Combine(path, gnWindows.ChoosenGameName);
        #endregion
        */
        /*
        string destArchLink = Path.Combine(path, gameName);

        // var zipres = Destination.Verif(destArchive + ".zip");
        var sevenZRes = OPFiles.SingleVerif(destArchLink, "Make_SevenZip", log: (string message) => ITrace.WriteLine(message, true));

        switch (sevenZRes)
        {
            case EOPResult.OverWrite:
            // 2020
            case EOPResult.NotExisting:
            case EOPResult.Trashed:
                ITrace.WriteLine($"[Make_SevenZip] 7z Compression begin");
                if (!SevenZipCompression.CompressFolder(folder2Comp, destArchLink, Properties.Settings.Default.c7zCompLvl))
                {
                    ITrace.WriteLine("[Make_SevenZip] 7z Compression canceled");
                    return false;
                }
                return true;

            default:
                return false;
    }

        }*/
    }
}
