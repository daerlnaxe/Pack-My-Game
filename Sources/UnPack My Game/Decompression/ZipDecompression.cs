using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zip;
using System.Text;
using System.Diagnostics;
using System.Threading;
using DxTBoxCore.Box_Progress;

namespace UnPack_My_Game.Decompression
{
    internal class ZipDecompression
    {
        public static event DoubleDel CurrentProgress;
        public static event StringDel CurrentStatus;
        public static event DoubleDel MaxProgress;
        

        public static bool UncompressArchive(string fileArchive, string destFolder, CancellationToken token)
        {
            /*if (!Directory.Exists(destFolder))
                return false;*/

            if (!File.Exists(fileArchive))
                return false;


            
            
            using (ZipFile archiveZ = ZipFile.Read(fileArchive))
            {
                archiveZ.ExtractProgress += ArchiveZ_ExtractProgress;
                archiveZ.ZipError += ArchiveZ_ZipError;

                CurrentProgress?.Invoke(0);
                CurrentStatus?.Invoke("Zip Decompression");
                MaxProgress?.Invoke(archiveZ.Entries.Count);

                int i = 0;
                foreach(ZipEntry ze in archiveZ.Entries)
                {
                    CurrentProgress?.Invoke(i);
                    CurrentStatus?.Invoke($"\tZipExtraction: {ze.FileName}");

                    if (token.IsCancellationRequested)
                        return false;

                    ze.Extract(destFolder, ExtractExistingFileAction.OverwriteSilently);

                    i++;
                }

                CurrentProgress?.Invoke(archiveZ.Entries.Count);
            }


            return true;
        }


        private static void ArchiveZ_ExtractProgress(object sender, ExtractProgressEventArgs e)
        {
           // Debug.WriteLine($"{e.ArchiveName}, {e.CurrentEntry.FileName}, {e.EntriesExtracted}/{e.EntriesTotal}");
        }

        private static void ArchiveZ_ZipError(object sender, ZipErrorEventArgs e)
        {
            Debug.WriteLine(e.Exception.Message);
        }

    }
}
