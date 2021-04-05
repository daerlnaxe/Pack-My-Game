using DxTBoxCore.Box_Progress;
using Hermes;
using Hermes.Cont;
using Hermes.Messengers;
using LaunchBox_XML.BackupLB;
using LaunchBox_XML.Container.Game;
using LaunchBox_XML.XML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using UnPack_My_Game.Cont;
using UnPack_My_Game.Decompression;
using UnPack_My_Game.Graph;
using PS = UnPack_My_Game.Properties.Settings;

namespace UnPack_My_Game.Cores
{
    class C_LaunchBoxEB : C_LaunchBox
    {
        public override event DoubleDel UpdateProgressT;
        public override event StringDel UpdateStatusT;
        public override event DoubleDel MaximumProgressT;

        public override event DoubleDel UpdateProgress;
        public override event StringDel UpdateStatus;
        public override event DoubleDel MaximumProgress;

        public List<FileObj> Games { get; }

        public override CancellationToken CancelToken { get; }


        public C_LaunchBoxEB(List<FileObj> games) : base()
        {
            CancelToken = TokenSource.Token;

            Games = games;
        }


        public override object Run(int timeSleep = 10)
        {
            // Tracing
            MeSimpleLog log = new MeSimpleLog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.Logs, $"{DateTime.Now.ToFileTime()}.log"))
            {
                LogLevel = 1,
                FuncPrefix = EPrefix.Horodating,
            };
            log.AddCaller(this);
            HeTrace.AddLogger("LaunchBox", log);


            UpdateStatus += (x) => HeTrace.WriteLine(x, this);

            ZipDecompression.CurrentProgress += (x) => this.UpdateProgress?.Invoke(x);
            ZipDecompression.CurrentStatus += (x) => this.UpdateStatus?.Invoke(x);
            ZipDecompression.MaxProgress += (x) => this.MaximumProgress?.Invoke(x);


            //
            int i = 0;
            MaximumProgressT?.Invoke(Games.Count());
            MaximumProgress?.Invoke(100);
            foreach (FileObj game in Games)
            {
                UpdateProgressT?.Invoke(i);

                UpdateStatus?.Invoke($"Work on: {game.Nom}");


                string gameName = Path.GetFileNameWithoutExtension(game.Nom);
                string tmpPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(game.Nom));


                // Décompresser
                if (Path.GetExtension(game.Path).Equals(".zip", StringComparison.OrdinalIgnoreCase))
                    ZipDecompression.UncompressArchive(game.Path, tmpPath, CancelToken);

                if (CancelToken.IsCancellationRequested)
                {
                    UpdateStatus?.Invoke("Stopped by user");
                    return false;
                }

                // Chargement des données du jeu
                LBGame lbGame = XML_Games.Scrap_GameLB(Path.Combine(tmpPath, "EBGame.xml"), "LaunchBox_Backup", PS.Default.wCustomFields);
                UpdateStatus?.Invoke($"Game info xml loaded: {lbGame.Title}");

                // Vérification de la présence du fichier xml de la plateforme
                string machineXMLFile = Path.Combine(PS.Default.LastLBpath, PS.Default.dPlatforms, $"{lbGame.Platform}.xml");
                if (!File.Exists(machineXMLFile))
                {
                    UpdateStatus?.Invoke($"Creation of xml file: '{machineXMLFile}'");
                    XML_Games.NewPlatform(machineXMLFile);
                }

                // Retrait du jeu si présence                
                // Todo rajouter une boite ainsi qu'une option pour ne pas avoir cette boite et virer automatiquement
                XML_Games.Remove_Game(lbGame.Id, machineXMLFile);

                // Initialisation des dossiers

                TGamesP = Path.GetDirectoryName(lbGame.ApplicationPath);
                UpdateStatus?.Invoke($"Target Game path: {TGamesP}");

                /*TCheatsCodesP = Path.Combine(root, "Cheat Codes", lbGame.Platform);
                UpdateStatus?.Invoke($"Target Cheats path: {TCheatsCodesP}");

                TImagesP = Path.Combine(root, "Images", lbGame.Platform);
                UpdateStatus?.Invoke($"Target Images path: {TImagesP}");*/

                TManualsP = Path.GetDirectoryName(lbGame.ManualPath);
                UpdateStatus?.Invoke($"Target Manuals  path: {TManualsP}");

                TMusicsP = Path.GetDirectoryName(lbGame.MusicPath);
                UpdateStatus?.Invoke($"Target Musics path: {TMusicsP}");

                TVideosP = Path.GetDirectoryName(lbGame.VideoPath);
                UpdateStatus?.Invoke($"Target Videos path: {TVideosP}");

                // Modification des chemins dans le jeu
                Modify_Paths(lbGame);

                // Copy des jeux
                Copy_Manager(lbGame, tmpPath);

                // Injection
                XML_Games.InjectGame(lbGame, machineXMLFile, PS.Default.wCustomFields);
                UpdateStatus?.Invoke($"Injection in xml Launchbox's files");
                //XMLBackup.Copy_EBGame(gameName, Path.Combine(tmpPath, "EBGame.xml"), MachineXMLFile);

                // Effacer le dossier temporaire
                Delete(tmpPath);
                
                //i++;
                UpdateProgress?.Invoke(100);
                UpdateStatus?.Invoke("Game Finished");
            }

            UpdateStatus?.Invoke("Task Finished");
            HeTrace.RemoveLogger("LaunchBox");
            UpdateProgressT?.Invoke(100);

            return true;
        }



        /// <summary>
        /// Copie les fichiers en fonction du dossier source
        /// </summary>
        /// <param name="destPath"></param>
        /// <param name="objMachine"></param>
        private void Copy_Manager(LBGame lbGame, string tempFolder)
        {
            foreach (string d in Directory.GetDirectories(tempFolder))
            {
                if (CancelToken.IsCancellationRequested)
                    throw new OperationCanceledException(CancelToken);

                string dirName = Path.GetFileName(d);

                // Games
                if (dirName == PS.Default.Games)
                    CopyContent(d, Path.GetDirectoryName(lbGame.ApplicationPath));

                // Cheats Codes
                else if (dirName == PS.Default.CheatCodes)
                    CopyContent(d, TCheatsCodesP);

                // Images
                else if (dirName == PS.Default.Images)
                    CopyContent(d, TImagesP);

                // Manuals
                else if (dirName == PS.Default.Manuals)
                    CopyContent(d, Path.GetDirectoryName(lbGame.ManualPath));

                // Musics
                else if (dirName == PS.Default.Musics)
                    CopyContent(d, Path.GetDirectoryName(lbGame.MusicPath));

                // Videos
                else if (dirName == PS.Default.Videos)
                    CopyContent(d, Path.GetDirectoryName(lbGame.VideoPath));
            }
        }

        private void Modify_Paths(LBGame lbGame)
        {
            string root = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(lbGame.ApplicationPath)));

            Application.Current.Dispatcher?.Invoke(() =>
            {
                W_DefinePaths window = new W_DefinePaths()
                {
                    Model = new Models.M_ModDefinePaths()
                    {
                        RootCheats = Path.Combine(root, "Cheat Codes", lbGame.Platform),
                        RootImg = Path.Combine(root, "Images", lbGame.Platform),
                    }
                };

                if (window.ShowDialog() == true)
                {
                    TImagesP = window.Model.RootImg;
                    TCheatsCodesP = window.Model.RootCheats;
                }
                else
                {
                    TokenSource.Cancel();
                }
            });
            /*
            Application.Current.Dispatcher?.Invoke(() =>
                new W_DefinePaths()
                {
                    Model = new Models.M_ModDefinePaths()
                    {
                        RootCheats = Path.Combine(root, "Cheat Codes", lbGame.Platform),
                        RootImg = Path.Combine(root, "Images", lbGame.Platform),
                    }
                }.ShowDialog());
            */
        }
    }
}
