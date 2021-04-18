
using AsyncProgress;
using AsyncProgress.Cont;
using AsyncProgress.ToImp;
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
    class SevenZipCompression : A_ASBase, I_AsyncSigD, IEcoProgress
    {
        public delegate bool DecisionHandler(string message, string title);

        public static event DecisionHandler Error;

        public event ProgressHandler UpdateProgress;
        public event StateHandler UpdateStatus;
        public event ProgressHandler UpdateProgressT;
        public event StateHandler UpdateStatusT;

        public int TimeLimit { get; set; } = 100;

        public Stopwatch Timer { get; private set; } = new Stopwatch();

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
        public bool CompressFolder(string folderSrc, string archiveDest, int cplLvl)
        {
            Debug.WriteLine("SevenZip compressfolder");
            if (!Directory.Exists(folderSrc))
                throw new FileNotFoundException(folderSrc);

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

                    szp.CompressDirectory(folderSrc, Path.Combine(DestinationFolder, tmp));

                    // on renomme
                    File.Move(tmp, ArchiveLink, true);

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
            this.UpdateProgress?.Invoke(this, new ProgressArg(0, 100, CancelFlag));
            this.UpdateProgressT?.Invoke(this, new ProgressArg(0, e.Value, CancelFlag));
            Debug.WriteLine($"Files: {e.Value} Found");
        }

        // Se produit après files found #2, donne l'état d'avancement global et quel fichier va être traité
        private void FileCompressionStarted(object sender, FileNameEventArgs e)
        {
            Timer.Restart();
            if (CancelToken.IsCancellationRequested)
            {
                e.Cancel = true;
                IsInterrupted = true;
            }

            this.UpdateProgressT?.Invoke(this, new ProgressArg(e.PercentDone, 100, CancelFlag));
            this.UpdateStatus?.Invoke(this, new StateArg(e.FileName, CancelFlag));

            Debug.WriteLine($"File being compressed: {e.FileName}");
            Debug.WriteLine($"fcs Percent done: {e.PercentDone}%");
        }

        /// <summary>
        /// Serait la progression sur chaque fichier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// Niveau de compression
        /// </remarks>
        private void Compressing(object sender, ProgressEventArgs e)
        {
            if (Timer.ElapsedMilliseconds < TimeLimit)
                return;

            this.UpdateProgress?.Invoke(this, new ProgressArg( e.PercentDone, 100, CancelFlag));


            Debug.WriteLine($"\t\tCompressing - {e.PercentDone} %"); // on dirait la compression totale
            Debug.WriteLine($"\t\tCompressing - Delta: {e.PercentDelta}");

            Timer.Restart();
        }

        // Arrive à la fin de la compression d'un fichier
        private void FileCompressionFinished(object sender, EventArgs e)
        {
            // Event args est vide, aucune information
            this.UpdateProgress?.Invoke(this, new ProgressArg(100,100, CancelFlag));
            Debug.WriteLine($"FileCompressionFinished: {e.ToString()}");
        }

        // A la fin de la compression totale
        private void CompressionFinished(object sender, EventArgs e)
        {
            //Aucune info nonplus

            this.UpdateProgressT?.Invoke(this, new ProgressArg(100,100, CancelFlag));
            this.UpdateStatus?.Invoke(this, new StateArg( "Finished", CancelFlag));
            Thread.Sleep(500);
        }

        public override void StopTask()
        {
            TokenSource.Cancel();

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
