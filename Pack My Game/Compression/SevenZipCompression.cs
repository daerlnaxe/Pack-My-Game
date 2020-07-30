
using Pack_My_Game.IHM;
using SevenZip;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using DlnxLocalTransfert;
using DxTrace;

namespace Pack_My_Game.Compression
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Modèle avec Task</remarks>
    class SevenZipCompression
    {
        static ProgressCompFolder BoxProgress;

        static string _ArchiveName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder">Folder</param>
        /// <param name="archiveDest">Archive name</param>
        /// <param name="cplLvl">Level of compression</param>
        /// <returns></returns>
        public static bool CompressFolder(string folder, string archiveDest, int cplLvl)
        {

            if (!Directory.Exists(folder)) return false;

            _ArchiveName = archiveDest + ".7z";
            //string sSeventZipLink = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "x86", "7z.dll");
            string sSeventZipLink = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Environment.Is64BitProcess ? "x64" : "x86", "7z.dll");

            //var sevenZipPath = Path.Combine( Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),Environment.Is64BitProcess ? "x64" : "x86", "7z.dll");
            if (!File.Exists(sSeventZipLink))
            {
                MessageBox.Show("7z.dll missing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            SevenZipCompressor.SetLibraryPath(sSeventZipLink);
            //  var k = SevenZipCompressor.CurrentLibraryFeatures;


            SevenZipCompressor szp = new SevenZipCompressor();


            szp.CompressionLevel = CompressionLevel.Ultra;
            szp.CompressionMode = CompressionMode.Create;
            szp.ArchiveFormat = OutArchiveFormat.SevenZip;
            szp.DirectoryStructure = true;

            try
            {

                szp.FilesFound += FilesFound;
                szp.Compressing += Compressing;


                szp.FileCompressionStarted += FileCompressionStarted;
                szp.FileCompressionFinished += FileCompressionFinished;
                szp.CompressionFinished += CompressionFinished;


                BoxProgress = new ProgressCompFolder();
                BoxProgress.Text = "Compression 7z";



                Task t = Task.Run(() =>
                {
                    szp.CompressDirectory(folder, _ArchiveName);
                }
                );

                BoxProgress.ShowDialog();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return true;
        }


        //avant compressing - #1
        private static void FilesFound(object sender, IntEventArgs e)
        {


            BoxProgress.dProgress.InfoSup = _ArchiveName;
            // BoxProgress.FilesDone = 0;
            BoxProgress.dProgress.TotalFiles = e.Value;
            BoxProgress.dProgress.GlobalProgress(BoxProgress.dProgress.FilesDone, BoxProgress.dProgress.TotalFiles);
            Console.WriteLine($"Files: {e.Value} Found");
        }

        // Se produit après files found
        private static void FileCompressionStarted(object sender, FileNameEventArgs e)
        {
            Console.WriteLine($"File being compressed: {e.FileName}");

            BoxProgress.dProgress.EntryUpdate(e.PercentDone);
            BoxProgress.dProgress.CurrentInfo = e.FileName;
            //BoxProgress.AsyncUpdateEntryTxt(e.PercentDone);
            Console.WriteLine($"fcs Percent done: {e.PercentDone}%");
        }

        private static void Compressing(object sender, ProgressEventArgs e)
        {
            BoxProgress.dProgress.EntryUpdate(e.PercentDone);
            //BoxProgress.AsyncUpdateEntryTxt(e.PercentDone);
            Console.WriteLine($"\t\tcomp {e.PercentDone} %");
            Console.WriteLine($"\t\tcomp Delta: {e.PercentDelta}");
        }

        private static void FileCompressionFinished(object sender, EventArgs e)
        {
            //Apparement aucune info si ce n'est que c'est fini
            BoxProgress.dProgress.FilesDone++;
            BoxProgress.dProgress.EntryUpdate(100);
            BoxProgress.dProgress.GlobalProgress(BoxProgress.dProgress.FilesDone, BoxProgress.dProgress.TotalFiles);

            Console.WriteLine($"\t\t {100} %");
            Console.WriteLine($"Compression du fichier terminée{e.ToString()}");
        }

        // #?
        private static void CompressionFinished(object sender, EventArgs e)
        {
            //Aucune info nonplus
            int totalFiles = BoxProgress.dProgress.TotalFiles;
            BoxProgress.dProgress.FilesDone = totalFiles;
            BoxProgress.dProgress.GlobalProgress(totalFiles, totalFiles);

            Console.WriteLine($"Compression tout fini{e.ToString()}");
            Thread.Sleep(500);
            BoxProgress.StopIt();

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="destArchive"></param>
        /// <param name="path">Work folder</param>
        /// <param name="folder2Comp"></param>
        /// <returns></returns>
        public static bool Make_Folder(string folder2Comp, string path, string destArchive)
        {
            #region 2020 choix du nom

            GameName gnWindows = new GameName();
            gnWindows.SuggestedGameName = destArchive;
            gnWindows.ShowDialog();

            //string destArchLink = Path.Combine(path, $"{destArchive}");
            string destArchLink = Path.Combine(path, gnWindows.ChoosenGameName);

            #endregion

            // var zipres = Destination.Verif(destArchive + ".zip");
            var sevenZRes = OPFiles.SingleVerif(destArchLink, "Make_SevenZip", log: (string message) => ITrace.WriteLine(message, true));

            switch (sevenZRes)
            {
                case OPResult.OverWrite:
                case OPResult.Ok:
                case OPResult.Trash:
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
        }
    }
}
