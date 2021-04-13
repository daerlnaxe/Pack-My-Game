using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;
using System.Text;
using System.Diagnostics;
using System.Threading;
using DxTBoxCore.Box_Progress;
using DxLocalTransf;
using System.Linq;
using DxLocalTransf.Progress.ToImp;
using DxLocalTransf.Progress;

namespace UnPack_My_Game.Decompression
{
    internal class ZipDecompression: I_ASBase
    {

        public event DoubleHandler UpdateProgress;
        public event MessageHandler UpdateStatus;
        public event DoubleHandler MaximumProgress;


        public event DoubleHandler UpdateProgressT;
        public event MessageHandler UpdateStatusT;
        public event DoubleHandler MaximumProgressT;

        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();
        public CancellationToken CancelToken { get; }

        public bool IsPaused { get; set; }



        #region gestion des events
        private static void ArchiveZ_ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            // Debug.WriteLine($"{e.ArchiveName}, {e.CurrentEntry.FileName}, {e.EntriesExtracted}/{e.EntriesTotal}");
        }


        private static void ArchiveZ_ZipError(object sender, ZipErrorEventArgs e)
        {
            Debug.WriteLine(e.Exception.Message);
        }
        #endregion event

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

        }


        public bool UncompressArchive(string fileArchive, string destFolder, CancellationToken token)
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

                UpdateProgress?.Invoke(this, 0);
                UpdateStatus?.Invoke(this, "Zip Decompression");
                MaximumProgress?.Invoke(this, archiveZ.Entries.Count);

                int i = 0;
                foreach (ZipEntry ze in archiveZ.Entries)
                {
                    UpdateProgress?.Invoke(this, i);
                    UpdateStatus?.Invoke(this, $"\tZipExtraction: {ze.FileName}");

                    if (token.IsCancellationRequested)
                        return false;

                    ze.Extract(destFolder, ExtractExistingFileAction.OverwriteSilently);

                    i++;
                }

                UpdateProgress?.Invoke(this, archiveZ.Entries.Count);
            }


            return true;
        }


        internal static bool StatExtractByExtension(string fileArchive, string destFolder, params string[] extensions)
        {
            ZipDecompression decomp = new ZipDecompression();


            return decomp.ExtractByExtension(fileArchive, destFolder, extensions);

        }

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


        private bool Extractfiles(ZipFile archiveZ, IEnumerable<ZipEntry> files, string destFolder)
        {
            int max = files.Count();

            UpdateProgress?.Invoke(this, 0);
            UpdateStatus?.Invoke(this, "Zip Decompression");

            MaximumProgress?.Invoke(this, max);

            int i = 0;
            foreach (var ze in files)
            {
                if (TokenSource != null && TokenSource.IsCancellationRequested)
                    return false;

                UpdateProgress?.Invoke(this, i);
                UpdateStatus?.Invoke(this, $"\tZipExtraction: {ze.FileName}");
                ze.Extract(destFolder, ExtractExistingFileAction.OverwriteSilently);
            }

            UpdateProgress?.Invoke(this, max);
            return true;
        }

        public void StopTask()
        {
            throw new NotImplementedException();
        }
    }
}

