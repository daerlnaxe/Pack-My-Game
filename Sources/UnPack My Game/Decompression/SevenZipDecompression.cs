using AsyncProgress;
using AsyncProgress.Cont;
using AsyncProgress.ToImp;
using SevenZip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace UnPack_My_Game.Decompression
{
    class SevenZipDecompression : A_ASBase, I_AsyncSigD
    {
        public event ProgressHandler UpdateProgress;
        public event StateHandler UpdateStatus;

        public event ProgressHandler UpdateProgressT;
        public event StateHandler UpdateStatusT;


        public SevenZipDecompression()
        {
            //string sSeventZipLink = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "x86", "7z.dll");
            string sSeventZipLink = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Environment.Is64BitProcess ? "x64" : "x86", "7z.dll");

            //var sevenZipPath = Path.Combine( Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),Environment.Is64BitProcess ? "x64" : "x86", "7z.dll");
            if (!File.Exists(sSeventZipLink))
                throw new Exception("7Zip dll Missing you must copy the dll according to the version at the root");

            SevenZipCompressor.SetLibraryPath(sSeventZipLink);
        }




        #region Read

        public static void Read(string archiveFile)
        {
            if (!File.Exists(archiveFile))
                throw new FileNotFoundException(archiveFile);


            /*//string sSeventZipLink = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "x86", "7z.dll");
            string sSeventZipLink = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Environment.Is64BitProcess ? "x64" : "x86", "7z.dll");

            //var sevenZipPath = Path.Combine( Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),Environment.Is64BitProcess ? "x64" : "x86", "7z.dll");
            if (!File.Exists(sSeventZipLink))
                throw new Exception("7Zip dll Missing you must copy the dll according to the version at the root");

            SevenZipCompressor.SetLibraryPath(sSeventZipLink);

            using (var zip = new SevenZipExtractor(archiveFile))
            {
                foreach (ArchiveFileInfo aFileName in zip.ArchiveFileData)
                {

                    Debug.WriteLine(aFileName.FileName);
                    //Debug.WriteLine(aFileName.IsDirectory);

                }
            */

        }




        #region Get Files

        #region Get Files Starting By

        internal static IEnumerable<string> StaticGetFilesStartingBy(string archiveFile, string startString)
        {
            SevenZipDecompression sevenZip = new SevenZipDecompression();
            return sevenZip.GetFilesStartingBy(archiveFile, startString);
        }


        internal IEnumerable<string> GetFilesStartingBy(string archiveFile, string startString)
        {
            using (var zip = new SevenZipExtractor(archiveFile))
            {
                return GetFilesStartingBy(zip, startString).Select((x) => x.FileName);
            }
        }

        internal IEnumerable<ArchiveFileInfo> GetFilesStartingBy(SevenZipExtractor sevenZip, string startString)
        {
            Queue<ArchiveFileInfo> selectedFiles = new Queue<ArchiveFileInfo>();
            foreach (ArchiveFileInfo aFileName in sevenZip.ArchiveFileData)
            {
                if (aFileName.FileName.StartsWith(startString, StringComparison.OrdinalIgnoreCase))
                    selectedFiles.Enqueue(aFileName);
            }

            return selectedFiles;
        }
        #endregion

        private IEnumerable<ArchiveFileInfo> GetFilesByName(SevenZipExtractor sevenZip, string[] files)
        {
            Queue<ArchiveFileInfo> selectedFiles = new Queue<ArchiveFileInfo>();
            foreach (ArchiveFileInfo aFileName in sevenZip.ArchiveFileData)
            {
                if (aFileName.IsDirectory)
                    continue;

                string fileName = Path.GetFileName(aFileName.FileName);

                if (files.Contains(fileName))
                    selectedFiles.Enqueue(aFileName);


            }
            return selectedFiles;

        }
        #endregion

        #endregion



        #region Extraction


        #region Extract specific files
        internal static bool StatExtractSpecificFiles(string archiveFile, string destFolder, params string[] files)
        {
            SevenZipDecompression decomp = new SevenZipDecompression();
            return decomp.ExtractSpecificFiles(archiveFile, destFolder, files);

            //return decomp.ExtractByExtension(fileArchive, destFolder, extensions);
        }

        public bool ExtractSpecificFiles(string archiveFile, string destFolder, params string[] files)
        {
            if (!File.Exists(archiveFile))
                return false;

            /*  //string sSeventZipLink = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "x86", "7z.dll");
              string sSeventZipLink = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Environment.Is64BitProcess ? "x64" : "x86", "7z.dll");

              //var sevenZipPath = Path.Combine( Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),Environment.Is64BitProcess ? "x64" : "x86", "7z.dll");
              if (!File.Exists(sSeventZipLink))
                  throw new Exception("7Zip dll Missing you must copy the dll according to the version at the root");

              SevenZipCompressor.SetLibraryPath(sSeventZipLink);*/

            using (var sevenZip = new SevenZipExtractor(archiveFile))
            {
                IEnumerable<ArchiveFileInfo> selected = GetFilesByName(sevenZip, files);

                if (selected.Any())
                {
                    ExtractFiles(sevenZip, selected, destFolder);
                }
                return true;
            }
        }
        #endregion

        private bool ExtractFiles(SevenZipExtractor sevenZipExtr, IEnumerable<ArchiveFileInfo> files, string destFolder)
        {
            sevenZipExtr.FileExtractionStarted += SevenZipExtr_FileExtractionStarted;
            sevenZipExtr.FileExtractionFinished += SevenZipExtr_FileExtractionFinished;
            sevenZipExtr.FileExists += SevenZipExtr_FileExists; ;

            int max = files.Count();
            bool cancelflag = false;

            UpdateStatus?.Invoke(this, new StateArg("7Zip Decompression", cancelflag));
            UpdateProgress?.Invoke(this, new ProgressArg(0, 100, cancelflag));

            int i = 0;
            foreach (ArchiveFileInfo archiveFI in files)
            {
                UpdateProgressT?.Invoke(this, new ProgressArg(i, max, cancelflag));
                UpdateStatus?.Invoke(this, new StateArg($"Extracting {archiveFI.FileName}", cancelflag));

                if (TokenSource != null && TokenSource.IsCancellationRequested)
                    return false;


                string fileDestination = Path.Combine(destFolder, archiveFI.FileName);

                using (FileStream fs = new FileStream(fileDestination, FileMode.Create, FileAccess.Write))
                {
                    sevenZipExtr.ExtractFile(archiveFI.Index, fs);
                }

                i++;
                //UpdateStatus?.Invoke(this, $"\tZipExtraction: {archiveFI.FileName}");
            }

            UpdateProgressT?.Invoke(this, new ProgressArg(max, max, cancelflag));
            return true;
        }

        #endregion

        // ---

        private void SevenZipExtr_FileExists(object sender, FileOverwriteEventArgs e)
        {
            Debug.WriteLine($"FileExists: {e.FileName}");
        }

        // En premier
        private void SevenZipExtr_FileExtractionStarted(object sender, FileInfoEventArgs e)
        {
            Debug.WriteLine($"FileExtraction Started {e.PercentDone} | {e.FileInfo.FileName}");
        }

        private void SevenZipExtr_FileExtractionFinished(object sender, FileInfoEventArgs e)
        {
            Debug.WriteLine($"FileExtraction Finished {e.PercentDone}");
            UpdateProgress?.Invoke(this, new ProgressArg(e.PercentDone, 100, CancelFlag));
        }


        public override void StopTask()
        {
            TokenSource.Cancel();
            //IsInterrupted = true;
        }
    }
}
