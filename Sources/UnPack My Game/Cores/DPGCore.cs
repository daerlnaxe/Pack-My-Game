using Common_PMG.Container;
using DxLocalTransf;
using DxLocalTransf.Progress.ToImp;
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
    internal class DPGCore : A_ASBaseC
    {
        public override event DoubleHandler UpdateProgress;
        public override event DoubleHandler MaximumProgress;
        public override event MessageHandler UpdateStatus;

        public override event DoubleHandler UpdateProgressT;
        public override event MessageHandler UpdateStatusT;
        public override event DoubleHandler MaximumProgressT;

        internal bool MakeZipDPG(List<DataRep> zipFiles)
        {
            ZipDecompression.StatCurrentProgress += UpdateProgress;
            ZipDecompression.StatCurrentStatus += UpdateStatus;
            ZipDecompression.StatMaxProgress += MaximumProgress;
            //

            try
            {
                foreach (DataRep zF in zipFiles)
                {
                    DpgCore(zF);
                }
                return true;
            }
            catch (Exception exc)
            {
                HeTrace.WriteLine(exc.Message);
                return false;
            }
        }

        internal bool MakeZipDpg(DataRep zipFile)
        {
            ZipDecompression.StatCurrentProgress += UpdateProgress;
            ZipDecompression.StatCurrentStatus += UpdateStatus;
            ZipDecompression.StatMaxProgress += MaximumProgress;
            ZipDecompression.StatToken = this.CancelToken;

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

        internal bool DpgCore(DataRep zF)
        {
            string gamePath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension( zF.Name));
            // Extraction des fichiers xml
            ZipDecompression.StatExtract(zF.ALinkToThePast, gamePath, "xml") ;
            return true;

        }

        public override void StopTask()
        {
            throw new NotImplementedException();
        }
    }
}
