using Common_PMG.Container;
using DxLocalTransf;
using DxLocalTransf.Progress.ToImp;
using DxTBoxCore.Box_Progress.Basix;
using Hermes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnPack_My_Game.Decompression;

namespace UnPack_My_Game.Cores
{
    /// <summary>
    /// Tout ce qui est en rapport avec le DPG
    /// </summary>
    internal class DPGCore : A_AsyncProgressL//, I_AsyncProgress
    {
        /*
        public override event DoubleHandler UpdateProgress;
        public override event DoubleHandler MaximumProgress;
        public override event MessageHandler UpdateStatus;

        public override event DoubleHandler UpdateProgressT;
        public override event MessageHandler UpdateStatusT;
        public override event DoubleHandler MaximumProgressT;
        */
        public Queue<DataRep> RejectedDatas { get; private set; } = new Queue<DataRep>();

        internal bool MakeFileDPG(IEnumerable<DataRep> archives)
        {
            try
            {
                // prérequis ? Doit être zip, 7zip | ou dossier (A venir)

                foreach (DataRep zF in archives)
                {
                    ArchiveMode mode = ArchiveMode.None;

                    string gamePath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(zF.Name));
                    string fileExt = Path.GetExtension(zF.ALinkToThePast).TrimStart('.');

                    // Création du dossier de destination
                    Directory.CreateDirectory(gamePath);

                    // Détection du mode
                    if (fileExt.Equals("zip", StringComparison.OrdinalIgnoreCase))
                        mode = ArchiveMode.Zip;
                    if (fileExt.Equals("7zip", StringComparison.OrdinalIgnoreCase) || fileExt.Equals("7z", StringComparison.OrdinalIgnoreCase))
                        mode = ArchiveMode.SevenZip;

                    else
                        RejectedDatas.Enqueue(zF); // pas sur que ça serve

                    //
                    if (mode == ArchiveMode.Zip)
                        DPGZipCore(zF, gamePath);
                    else if (mode == ArchiveMode.SevenZip)
                        DPG7ZipCore(zF, gamePath);


                }
                return true;
            }
            catch (Exception exc)
            {
                HeTrace.WriteLine(exc.Message);
                return false;
            }
            finally
            {
            }
        }

        internal bool MakeDpg(DataRep zipFile)
        {



            try
            {
                // ---

                return true;
            }
            catch (Exception exc)
            {
                HeTrace.WriteLine(exc.Message);
                return false;
            }
        }


        void DPGZipCore(DataRep archive, string gamePath)
        {
            // Lecture des préréquis ? <= pas utile je crois

            // Extraction des fichiers xml
            ZipDecompression.StatExtractByExtension(archive.ALinkToThePast, gamePath, "xml");

        }

        void DPG7ZipCore(DataRep zF, string gamePath)
        {
            SevenZipDecompression.StatExtractSpecificFiles(zF.ALinkToThePast, gamePath,
                                                        "TBGame.xml", "EBGame.xml", "DPGame.json");

        }

        /*public  void StopTask()
        {
            throw new NotImplementedException();
        }*/
    }
}
