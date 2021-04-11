﻿using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;
using System.Text;
using System.Diagnostics;
using System.Threading;
using DxTBoxCore.Box_Progress;
using DxLocalTransf;
using System.Linq;

namespace UnPack_My_Game.Decompression
{
    internal class ZipDecompression
    {

        public event DoubleHandler CurrentProgress;
        public event MessageHandler CurrentStatus;
        public event DoubleHandler MaxProgress;

        public static event DoubleHandler StatCurrentProgress;
        public static event MessageHandler StatCurrentStatus;
        public static event DoubleHandler StatMaxProgress;


        public static CancellationToken StatToken { get; set; }

        #region gestion des events
        private static void ArchiveZ_ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
            // Debug.WriteLine($"{e.ArchiveName}, {e.CurrentEntry.FileName}, {e.EntriesExtracted}/{e.EntriesTotal}");
        }


        private static void ArchiveZ_ZipError(object sender, ZipErrorEventArgs e)
        {
            Debug.WriteLine(e.Exception.Message);
        }

        public static bool UnCompressArchive(string fileArchive, string destFolder, CancellationToken token)
        {
            ZipDecompression zc = new ZipDecompression();
            zc.CurrentProgress += (x, y) => StatCurrentProgress?.Invoke(x, y);
            zc.CurrentStatus += (x, y) => StatCurrentStatus?.Invoke(x, y);
            zc.MaxProgress += (x, y) => StatMaxProgress?.Invoke(x, y);

            return zc.UncompressArchive(fileArchive, destFolder, token);

        }


        #endregion event

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

                CurrentProgress?.Invoke(this, 0);
                CurrentStatus?.Invoke(this, "Zip Decompression");
                MaxProgress?.Invoke(this, archiveZ.Entries.Count);

                int i = 0;
                foreach (ZipEntry ze in archiveZ.Entries)
                {
                    CurrentProgress?.Invoke(this, i);
                    CurrentStatus?.Invoke(this, $"\tZipExtraction: {ze.FileName}");

                    if (token.IsCancellationRequested)
                        return false;

                    ze.Extract(destFolder, ExtractExistingFileAction.OverwriteSilently);

                    i++;
                }

                CurrentProgress?.Invoke(this, archiveZ.Entries.Count);
            }


            return true;
        }


        internal static bool StatExtract(string fileArchive, string destFolder, params string[] extensions)
        {
            ZipDecompression decomp = new ZipDecompression();
            return decomp.Extract(fileArchive, destFolder, extensions);

        }

        internal bool Extract(string fileArchive, string destFolder, params string[] extensions)
        {
            if (!File.Exists(fileArchive))
                return false;


            using (ZipFile archiveZ = ZipFile.Read(fileArchive))
            {
                archiveZ.ExtractProgress += ArchiveZ_ExtractProgress;
                archiveZ.ZipError += ArchiveZ_ZipError;

                CurrentProgress?.Invoke(this, 0);
                CurrentStatus?.Invoke(this, "Zip Decompression");

                int i = 0;
                Queue<ZipEntry> filesToExtract = new Queue<ZipEntry>();
                foreach (ZipEntry ze in archiveZ.Entries)
                {
                    if (StatToken.IsCancellationRequested)
                        return false;


                    string extension = Path.GetExtension(ze.FileName).TrimStart('.');

                    if (extensions.Contains(extension))
                    {
                        filesToExtract.Enqueue(ze);
                        i++;
                    }




                }

                if (filesToExtract.Any())
                {
                    MaxProgress?.Invoke(this, i);

                    int j = 0;
                    foreach (var ze in filesToExtract)
                    {
                        CurrentProgress?.Invoke(this, j);
                        CurrentStatus?.Invoke(this, $"\tZipExtraction: {ze.FileName}");
                        ze.Extract(destFolder, ExtractExistingFileAction.OverwriteSilently);

                    }
                    CurrentProgress?.Invoke(this, i);
                }

            }


            return true;
        }
    }
}
