using AsyncProgress;
using AsyncProgress.Basix;
using AsyncProgress.Cont;
using Common_PMG.Container;
using Common_PMG.Container.Game;
using Common_PMG.XML;
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
using PS = UnPack_My_Game.Properties.Settings;

namespace UnPack_My_Game.Cores
{
    /// <summary>
    /// Processus évolutif
    /// </summary>
    internal class DataPackGCore : C_LaunchBox
    {
        public DataPackGCore()
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




        #region
        public override bool InjectGames(ICollection<DataRep> games)
        {
            try
            {
                foreach (var game in games)
                {
                    //string tmpPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(game.Name));

                    InjectGame(game.ALinkToThePath);
                }
                return true;
            }
            catch (Exception exc)
            {
                return false;

            }
        }


        private void InjectGame(string path)
        {
            string dpgFile = Path.Combine(path, "DPGame.json");
            bool backupDone = false;

            // Check if DPG file exists
            if (!File.Exists(dpgFile))
            {
                DPGMakerCore dpgMaker = new DPGMakerCore();
                dpgMaker.MakeDPG_Folder(path);
            }

            GamePaths gpX = GamePaths.ReadFromJson<GamePaths>(dpgFile);

            string platformsFile = Path.Combine(PS.Default.LastLBpath, PS.Default.fPlatforms);
            bool CheckIfInjectionNeeded = !XML_Custom.TestPresence(platformsFile, Tag.Platform, Tag.Name, gpX.Platform);
            if (CheckIfInjectionNeeded)
            {
                HeTrace.WriteLine($"Backup of platforms file");
                // Backup du fichier de la plateforme;
                Common_PMG.Tool.BackupFile(platformsFile,
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.BackUp));

                string newPFile = IHMStatic.GetAFile(PS.Default.LastTargetPath, "Select the platform xml file", "xml");
                if (string.IsNullOrEmpty(newPFile))
                    throw new Exception("File for injection is not filled");

                HeTrace.WriteLine($"Injecting {gpX.Platform} in platforms file");
                InjectPlatform(gpX.Platform, newPFile);
            }


            // Copie des fichiers (on s'appuie sur les dossiers de la plateforme)















            /*// Vérification de la présence du fichier xml de la plateforme*/
            string machineXMLFile = Path.Combine(PS.Default.LastLBpath, PS.Default.dPlatforms, $"{gpX.Platform}.xml");
            // Backup platform file
            if (!backupDone)
            {
                //   BackupPlatformFile(machineXMLFile);
                HeTrace.WriteLine($"Backup of '{machineXMLFile}'");
                backupDone = true;
            }


        }




        #endregion




        public override void StopTask()
        {
            throw new NotImplementedException();
        }

        public override void Pause(int timeSleep)
        {
            throw new NotImplementedException();
        }
    }
}

