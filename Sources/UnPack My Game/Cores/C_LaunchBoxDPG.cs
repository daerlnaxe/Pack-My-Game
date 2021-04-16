using Common_PMG.Container;
using DxLocalTransf;
using DxLocalTransf.Progress;
using DxLocalTransf.Progress.ToImp;
using DxTBoxCore.Async_Box_Progress.Basix;
using Hermes;
using Hermes.Cont;
using Hermes.Messengers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using UnPack_My_Game.Decompression;
using UnPack_My_Game.Graph.LaunchBox;

namespace UnPack_My_Game.Cores
{
    /// <summary>
    /// Processus évolutif
    /// </summary>
    internal class C_LaunchBoxDPG : A_ProgressPersistD
    {

        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();
        public CancellationToken CancelToken { get; }

        public bool IsPaused { get; set; }
        /*
        public event DoubleHandler UpdateProgressT;
        public event MessageHandler UpdateStatusT;
        public event DoubleHandler MaximumProgressT;
        public event DoubleHandler UpdateProgress;
        public event DoubleHandler MaximumProgress;
        public event MessageHandler UpdateStatus;*/


        public C_LaunchBoxDPG()
        {
            // Tracing
            MeSimpleLog log = new MeSimpleLog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.Logs, $"{DateTime.Now.ToFileTime()}.log"))
            {
                LogLevel = 1,
                FuncPrefix = EPrefix.Horodating,
            };

            log.AddCaller(this);
            HeTrace.AddLogger("C_LBDPG", log);

            UpdateStatus += (x, y) => HeTrace.WriteLine(y, this);


        }

        public bool InjectSeveralGames(List<DataRep> games)
        {
            try
            {
                MaximumProgress?.Invoke(this, 100);
                MaximumProgressT?.Invoke(this, games.Count);

                foreach (var g in games)
                {

                    Depacking(g);


                }
                return true;

            }
            catch (Exception exc)
            {
                HeTrace.WriteLine(exc.Message, this);
                return false;
            }
        }

        public bool InjectGame()
        {
            // Détermination du mode de compression

            return true;
        }

        public bool Depacking(DataRep g)
        {
            string tmpPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(g.Name));

            // Décompression
            Decomp_Manager(g.ALinkToThePast, tmpPath);

            // Lecture du fichier DPGame
            DPG_Manager(tmpPath);

            return true;
        }


        /// <summary>
        /// Gestion de la décompression
        /// </summary>
        /// <param name="pathLink"></param>
        /// <param name="tmpPath"></param>
        /// <remarks>
        /// Ne gère que le zip pour le moment
        /// </remarks>
        private void Decomp_Manager(string pathLink, string tmpPath)
        {
            // Décompression au format zip
            if (Path.GetExtension(pathLink).Equals(".zip", StringComparison.OrdinalIgnoreCase))
            {
                ZipDecompression.UnCompressArchive(pathLink, tmpPath, CancelToken);
                return;
            }


            throw new Exception("File format not managed");

        }

        private void DPG_Manager(string tmpPath)
        {
            string dpgFile = Path.Combine(tmpPath, "DPGame.json");
            if(!File.Exists(dpgFile))
            {
                LaunchBoxCore_IHM.CreateDPG(tmpPath);

            }
            

        }


        public void StopTask()
        {
            throw new NotImplementedException();
        }
    }
}

