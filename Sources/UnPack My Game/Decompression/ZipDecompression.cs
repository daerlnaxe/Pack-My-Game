using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using AsyncProgress.ToImp;
using AsyncProgress;
using AsyncProgress.Cont;

namespace UnPack_My_Game.Decompression
{
    internal class ZipDecompression : A_ASBase, I_AsyncSigD, IEcoProgress
    {
        public int TimeLimit { get; set; } = 100;

        public Stopwatch Timer { get; } = new Stopwatch();

        public event ProgressHandler UpdateProgress;
        public event StateHandler UpdateStatus;

        public event ProgressHandler UpdateProgressT;
        public event StateHandler UpdateStatusT;



        #region gestion des events
        private void ArchiveZ_ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            CancelFlag = e.Cancel;

            // Debug.WriteLine($"{e.ArchiveName}, {e.CurrentEntry.FileName}, {e.EntriesExtracted}/{e.EntriesTotal}");
            // On peut placer le cancel token ici
            if (CancelToken.IsCancellationRequested)
            {
                e.Cancel = true;
                IsInterrupted = true;
            }

            #region Event Type
            switch (e.EventType)
            {
                case ZipProgressEventType.Extracting_BeforeExtractAll:
                    Debug.WriteLine("BeforeExtractAll");
                    break;

                // #1 - on a le nom du fichier
                case ZipProgressEventType.Extracting_BeforeExtractEntry:
                    Timer.Restart();
                    Debug.WriteLine("BeforeExtractEntry");
                    this.UpdateProgress?.Invoke(this, new ProgressArg(0, 100, e.Cancel));
                    this.UpdateStatus?.Invoke(this, new StateArg($"Begin extraction: {e.CurrentEntry.FileName}", CancelFlag));
                    break;

                // #2 - on a les bytes transferés, la somme totale a transférer, le nom du ficiher
                case ZipProgressEventType.Extracting_EntryBytesWritten:
                    if (Timer.ElapsedMilliseconds < TimeLimit)
                        return;

                    Debug.WriteLine($"EntryBytesWritten: {e.BytesTransferred}|{e.TotalBytesToTransfer}");
                    double percent = Math.Round((Double)e.BytesTransferred * 100 / e.TotalBytesToTransfer);
                    this.UpdateProgress?.Invoke(this, new ProgressArg(percent, e.TotalBytesToTransfer, CancelFlag));
                    break;

                // Apparu à la fin d'un fichier - on a le nom du fichier
                case ZipProgressEventType.Extracting_AfterExtractEntry:
                    Timer.Stop();
                    Debug.WriteLine("AfterExtractEntry");
                    this.UpdateProgress?.Invoke(this, new ProgressArg(100, 100, CancelFlag));
                    this.UpdateStatus?.Invoke(this, new StateArg($"Extraction finished: {e.CurrentEntry.FileName}", CancelFlag));
                    break;

                case ZipProgressEventType.Extracting_ExtractEntryWouldOverwrite:
                    Debug.WriteLine("ExtractEntryWouldOverwrit");
                    break;
                case ZipProgressEventType.Extracting_AfterExtractAll:
                    break;
            };
            #endregion 
        }


        private static void ArchiveZ_ZipError(object sender, ZipErrorEventArgs e)
        {
            Debug.WriteLine(e.Exception.Message);
        }
        #endregion event

        /*
        public static bool UnCompressArchive(string fileArchive, string destFolder, CancellationToken token, I_TProgress asker = null)
        {
            ZipDecompression zc = new ZipDecompression();
            if (asker != null)
            {
                zc.UpdateProgress += asker.SetProgress;
                zc.UpdateStatus += asker.SetStatus;
                zc.MaximumProgress += asker.SetMaximum;

            }

            return zc.UncompressArchive(fileArchive, destFolder, token);

        }*/


        public bool ExtractAll(string fileArchive, string destFolder)
        {
            /*if (!Directory.Exists(destFolder))
                return false;*/

            if (!File.Exists(fileArchive))
                return false;

            //string notFinishedF = Path.Combine(destFolder, "notFinished");

            using (ZipFile archiveZ = ZipFile.Read(fileArchive))
            {
                archiveZ.ExtractProgress += ArchiveZ_ExtractProgress;
                archiveZ.ZipError += ArchiveZ_ZipError;

                UpdateStatus?.Invoke(this, new StateArg("Zip Decompression", CancelFlag));

                int i = 0;
                int max = archiveZ.Entries.Count();
                foreach (ZipEntry ze in archiveZ.Entries)
                {
                    UpdateProgressT?.Invoke(this, new ProgressArg(i, max, CancelFlag));
                    UpdateStatus?.Invoke(this, new StateArg($"\tZipExtraction: {ze.FileName}", CancelFlag));

                    if (CancelToken.IsCancellationRequested)
                        return false;

                    ze.Extract(destFolder, ExtractExistingFileAction.OverwriteSilently);

                    i++;
                }

                UpdateProgress?.Invoke(this, new ProgressArg(max, max, CancelFlag));
            }


            return true;
        }







        #region Extract by extensions
        /*
        internal static bool StatExtractByExtension(string fileArchive, string destFolder, params string[] extensions)
        {
            ZipDecompression decomp = new ZipDecompression();


            return decomp.ExtractByExtension(fileArchive, destFolder, extensions);

        }*/



        #endregion

        #region GetFiles

        internal static IEnumerable<string> StaticGetAllFiles(string archiveFile)
        {
            ZipDecompression sevenZip = new ZipDecompression();
            return sevenZip.GetFilesStartingBy(archiveFile, "");
        }

        internal static IEnumerable<string> StaticGetFilesStartingBy(string archiveFile, string startString)
        {
            ZipDecompression sevenZip = new ZipDecompression();
            return sevenZip.GetFilesStartingBy(archiveFile, startString);
        }


        internal IEnumerable<string> GetFilesStartingBy(string archiveFile, string startString)
        {
            using (var zip = new ZipFile(archiveFile))
            {
                return GetFilesStartingBy(zip, startString).Select((x) => x.FileName);
            }
        }
        internal IEnumerable<ZipEntry> GetFilesStartingBy(ZipFile zip, string startString)
        {
            Queue<ZipEntry> selectedFiles = new Queue<ZipEntry>();
            foreach (var ze in zip.Entries)
            {
                if (!ze.IsDirectory && ze.FileName.StartsWith(startString, StringComparison.OrdinalIgnoreCase)) 
                    selectedFiles.Enqueue(ze);
            }

            return selectedFiles;
        }



        internal IEnumerable<ZipEntry> GetFilesByExtension(ZipFile archiveZ, params string[] extensions)
        {

            Queue<ZipEntry> files = new Queue<ZipEntry>();
            foreach (ZipEntry ze in archiveZ.Entries)
            {
                string extension = Path.GetExtension(ze.FileName).TrimStart('.');
                if (extensions.Contains(extension))
                {
                    files.Enqueue(ze);
                }
            }

            return files;
        }


        private IEnumerable<ZipEntry> GetFilesByName(ZipFile archiveZ, string[] files)
        {
            Queue<ZipEntry> selectedFiles = new Queue<ZipEntry>();
            foreach (ZipEntry ze in archiveZ.Entries)
            {
                if (ze.IsDirectory)
                    continue;

                string fileName = Path.GetFileName(ze.FileName);

                if (files.Contains(fileName))
                    selectedFiles.Enqueue(ze);


            }
            return selectedFiles;
        }


        #endregion

        #region Extraction
        private bool ExtractByExtension(string fileArchive, string destFolder, string[] extensions)
        {
            if (!File.Exists(fileArchive))
                return false;

            using (ZipFile archiveZ = ZipFile.Read(fileArchive))
            {
                archiveZ.ExtractProgress += ArchiveZ_ExtractProgress;
                archiveZ.ZipError += ArchiveZ_ZipError;

                IEnumerable<ZipEntry> files = GetFilesByExtension(archiveZ, extensions);

                if (files.Any())
                {
                    Extractfiles(archiveZ, files, destFolder);
                    return true;
                }

                return false;

            }
        }

        internal bool ExtractSpecificFiles(string fileArchive, string destFolder, params string[] specificfiles)
        {

            if (!File.Exists(fileArchive))
                return false;

            using (ZipFile archiveZ = ZipFile.Read(fileArchive))
            {
                IEnumerable<ZipEntry> files = GetFilesByName(archiveZ, specificfiles);

                if (files.Any())
                {
                    Extractfiles(archiveZ, files, destFolder);
                    return true;
                }

                return false;

            }
        }

        private bool Extractfiles(ZipFile archiveZ, IEnumerable<ZipEntry> files, string destFolder)
        {
            archiveZ.ExtractProgress += ArchiveZ_ExtractProgress;
            archiveZ.ZipError += ArchiveZ_ZipError;

            int max = files.Count();

            UpdateStatus?.Invoke(this, new StateArg("Zip Decompression", CancelFlag));
            UpdateProgress?.Invoke(this, new ProgressArg(0, 100, CancelFlag));
            UpdateProgressT?.Invoke(this, new ProgressArg(0, max, CancelFlag));

            int i = 0;
            foreach (var ze in files)
            {
                UpdateProgressT?.Invoke(this, new ProgressArg(i, 100, CancelFlag));

                if (TokenSource != null && TokenSource.IsCancellationRequested)
                    return false;

                //UpdateStatus?.Invoke(this, $"\tZipExtraction: {ze.FileName}");
                ze.Extract(destFolder, ExtractExistingFileAction.OverwriteSilently);
                i++;
            }

            UpdateProgressT?.Invoke(this, new ProgressArg(max, max, CancelFlag));
            return true;
        }

        #endregion

        // ---


        public override void StopTask()
        {
            TokenSource.Cancel();

        }
    }
}

