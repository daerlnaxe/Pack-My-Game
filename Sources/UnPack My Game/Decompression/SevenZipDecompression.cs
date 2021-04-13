using DxLocalTransf;
using DxLocalTransf.Progress;
using DxLocalTransf.Progress.ToImp;
using SevenZip;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace UnPack_My_Game.Decompression
{
    class SevenZipDecompression : I_ASBase
    {
        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        public CancellationToken CancelToken { get; }

        public bool IsPaused { get; set; }

        public event DoubleHandler UpdateProgress;
        public event MessageHandler UpdateStatus;
        public event DoubleHandler MaximumProgress;

        public event DoubleHandler UpdateProgressT;
        public event MessageHandler UpdateStatusT;
        public event DoubleHandler MaximumProgressT;



        public static void Read(string archiveFile)
        {
            if (!File.Exists(archiveFile))
                throw new FileNotFoundException(archiveFile);


            //string sSeventZipLink = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "x86", "7z.dll");
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

            }
        }

        internal static bool StatExtractSpecificFiles(string archiveFile, string destFolder, params string[] files)
        {
            SevenZipDecompression decomp = new SevenZipDecompression();
            return decomp.ExtractSpecificFiles(archiveFile, destFolder, files);


            //return decomp.ExtractByExtension(fileArchive, destFolder, extensions);
        }

        private bool ExtractSpecificFiles(string archiveFile, string destFolder, params string[] files)
        {
            if (!File.Exists(archiveFile))
                return false;

            //string sSeventZipLink = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "x86", "7z.dll");
            string sSeventZipLink = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Environment.Is64BitProcess ? "x64" : "x86", "7z.dll");

            //var sevenZipPath = Path.Combine( Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),Environment.Is64BitProcess ? "x64" : "x86", "7z.dll");
            if (!File.Exists(sSeventZipLink))
                throw new Exception("7Zip dll Missing you must copy the dll according to the version at the root");

            SevenZipCompressor.SetLibraryPath(sSeventZipLink);

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


        // ---

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

        private bool ExtractFiles(SevenZipExtractor sevenZipExtr, IEnumerable< ArchiveFileInfo> files, string destFolder)
        {
            int max = files.Count();

            UpdateProgress?.Invoke(this, 0);
            UpdateStatus?.Invoke(this, "7Zip Decompression");

            MaximumProgress?.Invoke(this, max);

            int i = 0;
            foreach (ArchiveFileInfo archiveFI in files)
            {
                if (TokenSource != null && TokenSource.IsCancellationRequested)
                    return false;

                UpdateProgress?.Invoke(this, i);
                UpdateStatus?.Invoke(this, $"\tZipExtraction: {archiveFI.FileName}");

                string fileDestination = Path.Combine(destFolder, archiveFI.FileName);

                using (FileStream fs = new FileStream(fileDestination, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    sevenZipExtr.ExtractFile(archiveFI.Index, fs);
                }
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
