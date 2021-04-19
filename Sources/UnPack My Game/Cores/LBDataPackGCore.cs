using AsyncProgress;
using AsyncProgress.Basix;
using AsyncProgress.Cont;
using Common_PMG.Container;
using Hermes;
using Hermes.Cont;
using Hermes.Messengers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnPack_My_Game.Decompression;
using UnPack_My_Game.Graph;
using UnPack_My_Game.Graph.LaunchBox;

namespace UnPack_My_Game.Cores
{
    /// <summary>
    /// Processus évolutif
    /// </summary>
    internal class LBDataPackGCore : A_ProgressPersistD, I_ASBase
    {

        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();
        public CancellationToken CancelToken { get; }

        public bool CancelFlag { get; private set; }

        public bool IsPaused { get; set; }

        public bool IsInterrupted { get; private set; }



        public LBDataPackGCore()
        {
            // Tracing
            MeSimpleLog log = new MeSimpleLog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.Logs, $"{DateTime.Now.ToFileTime()}.log"))
            {
                LogLevel = 1,
                FuncPrefix = EPrefix.Horodating,
            };

            log.AddCaller(this);
            HeTrace.AddLogger("C_LBDPG", log);

            /*

            MeEmit mee = new MeEmit()
            {
                ByPass = true,
            };
            mee.SignalWrite += (x, y)=> SetStatus(x, (StateArg)y);
            HeTrace.AddMessenger("mee", mee);*/
        }

        /*
        public bool InjectSeveralGames(List<DataRep> games)
        {
            try
            {

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
        }*/

        #region Depacking

        internal bool Depacking(ObservableCollection<DataRep> games)
        {
            ProgressTotal = games.Count();
            try
            {
                foreach (DataRep game in games)
                {
                    string tmpPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(game.Name));

                    Depacking(game, tmpPath);

                    InjectGame(game, tmpPath);
                }
                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }


        public bool Depacking(DataRep g, string tmpPath)
        {
            string fileExt = Path.GetExtension(g.ALinkToThePath).TrimStart('.');


            // Décompression
            // Détection du mode
            if (fileExt.Equals("zip", StringComparison.OrdinalIgnoreCase))
                DepackZip(g.ALinkToThePath, tmpPath);
           else if (fileExt.Equals("7zip", StringComparison.OrdinalIgnoreCase) || fileExt.Equals("7z", StringComparison.OrdinalIgnoreCase))
                Depack7Zip(g.ALinkToThePath, tmpPath);
            else
                throw new Exception("File format not managed");

            return true;
        }

        private void DepackZip(string pathLink, string tmpPath)
        {
            // Extraction des fichiers
            ZipDecompression zippy = new ZipDecompression()
            {
                TokenSource = this.TokenSource,
                IsPaused = this.IsPaused,
            };

            IHMStatic.LaunchDouble(zippy, () => zippy.ExtractAll(pathLink, tmpPath), "Zip Extraction");
        }


        private void Depack7Zip(string pathLink, string tmpPath)
        {
            throw new NotImplementedException();
        }

        #endregion Depacking


        #region
        public void InjectGames(ICollection<DataRep> games)
        {
            try
            {
                foreach (var game in games)
                {
                    string tmpPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(game.Name));

                    InjectGame(game, tmpPath);
                }
            }
            catch (Exception exc)
            {

            }
        }


        private void InjectGame(DataRep game, string tmpPath)
        {
            string dpgFile = Path.Combine(tmpPath, "DPGame.json");

            // Check if DPG file exists
            if (!File.Exists(dpgFile))
            {
                DPGMakerCore dpgMaker = new DPGMakerCore();
                dpgMaker.MakeDPG_Folder(game);
            }
        }

        #endregion




        public void StopTask()
        {
            throw new NotImplementedException();
        }

        public void Pause(int timeSleep)
        {
            throw new NotImplementedException();
        }
    }
}

