using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pack_My_Game.Compression
{
    class ZipCompress
    {
        public string WorkingFolder { get; set; }


        public async void CompressionF(string folderToCompress, string zipName)
        {
            if (!Directory.Exists(folderToCompress))
                throw new ArgumentNullException(folderToCompress);

            string tmpFile = Path.Combine(WorkingFolder, $"{zipName}.tmp");
            string zipFile = Path.Combine(WorkingFolder, $"{zipName}.zip");

            // --- Effacement de l'ancien fichier temporaire
            if (File.Exists(tmpFile))
                File.Delete(tmpFile);

            // --- Récupération de la liste des fichiers
            string[] files = Directory.GetFiles(folderToCompress);

            // -- Sécurité à faire

            // ---
            using (FileStream zipToOpen = new FileStream(tmpFile, FileMode.Create))
            {
                using (c)
                using (zip archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                {

                    foreach (string file in files)
                    {

                    }
                }
            }



        }

        private void compressFolder(string destFile)
        {
            using (FileStream fileStreamOut = new FileStream(destFile, FileMode.Create, FileAccess.Write))
            {
            }
        }


        private void CompressFile(string fileSrc, FileStream fileStreamOut, int buffersize)
        {
            using (ZipOutputStream zipOutStream = new ZipOutputStream(fileStreamOut))
            {
                using (FileStream fileStreamIn = new FileStream(fileSrc, FileMode.Open, FileAccess.Read))
                {
                    byte[] buffer = new byte[4096];
                    ZipEntry entry = new ZipEntry(Path.GetFileName(fileSrc)) ;

                    zipOutStream.PutNextEntry(entry);
                    int size;
                    do
                    {
                        size = fileStreamIn.Read(buffer, 0, buffer.Length);
                        zipOutStream.Write(buffer, 0, size);
                    } while (size > 0);

                }
            }


            /*

            //-
            ZipArchiveEntry zipEntry = archive.CreateEntryFromFile(fileSrc, fileSrc, CompressionLevel.);

            using (var zipEStream = zipEntry.Open())
            {
                // Lecture d'un fichier par buffer
                using (FileStream fsSource = new FileStream(file, FileMode.Open, FileAccess.Read))
                {

                    // Lit la source dans un tableau de byte.
                    byte[] bytes = new byte[fsSource.Length];
                    int numBytesToRead = (int)fsSource.Length;
                    int numBytesRead = 0;
                    while (numBytesToRead > 0)
                    {
                        // Read may return anything from 0 to numBytesToRead.
                        int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                        // Break when the end of the file is reached.
                        if (n == 0)
                            break;

                        numBytesRead += n;
                        numBytesToRead -= n;


                        // 
                        zipEStream.Write(bytes);
                    }
                    numBytesToRead = bytes.Length;
                }
            }
            */




        }
    }
}

