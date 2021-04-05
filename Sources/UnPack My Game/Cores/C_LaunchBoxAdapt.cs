using DxTBoxCore.Box_Progress;
using Hermes;
using Hermes.Cont;
using Hermes.Messengers;
using LaunchBox_XML.BackupLB;
using LaunchBox_XML.Container;
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
using UnPack_My_Game.Resources;
using PS = UnPack_My_Game.Properties.Settings;

namespace UnPack_My_Game.Cores
{
    /// <summary>
    /// Used to grant adaptation of archive to LaunchBox settings
    /// </summary>
    class C_LaunchBoxAdapt : C_LaunchBox
    {
        #region Progress
        public override event DoubleDel UpdateProgressT;
        public override event StringDel UpdateStatusT;
        public override event DoubleDel MaximumProgressT;

        public override event DoubleDel UpdateProgress;
        public override event StringDel UpdateStatus;
        public override event DoubleDel MaximumProgress;
        public override CancellationToken CancelToken { get; }

        #endregion
        public string PlatformName { get; }
        public List<FileObj> Games { get; }

        #region Informations
        protected Platform Machine { get; set; }

        protected string MachineXMLFile { get; set; }

        #endregion



        public C_LaunchBoxAdapt(List<Cont.FileObj> games, string platformName):base()
        {
            CancelToken = base.TokenSource.Token;
            PlatformName = platformName;
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


            // Récupération des infos de la plateforme
            UpdateStatus?.Invoke("Get infos from platform");
            XML_Functions xf = new XML_Functions();
            xf.ReadFile(Common.PlatformsFile);
            Machine = xf.ScrapPlatform(PlatformName);

            if (Machine.PlatformFolders.Count < 1)
            {
                UpdateStatus?.Invoke("Error: this machine has no path");
                return false;
            }


            // Backup datas
            MachineXMLFile = Path.Combine(PS.Default.LastLBpath, PS.Default.dPlatforms, $"{PlatformName}.xml");
            BackupPlatformFile(MachineXMLFile);
            UpdateStatus?.Invoke($"Backup of '{MachineXMLFile}'");

            // Initialisation des dossiers cible
            // Memo solution la plus simple, fixant des limites et normalement évolutive
            string root = Path.GetDirectoryName(Path.GetDirectoryName(Machine.FolderPath));
            TGamesP = Path.Combine(Machine.FolderPath);
            UpdateStatus?.Invoke($"Target Game path: {TGamesP}");

            TCheatsCodesP = Path.Combine(root, "Cheat Codes", PlatformName);
            UpdateStatus?.Invoke($"Target Cheats path: {TCheatsCodesP}");

            TImagesP = Path.GetDirectoryName(Machine.PlatformFolders.First((x) => x.MediaType.Contains("Box", StringComparison.OrdinalIgnoreCase)).FolderPath);
            UpdateStatus?.Invoke($"Target Images path: {TImagesP}");

            TManualsP = Machine.PlatformFolders.First((x) => x.MediaType == "Manual").FolderPath;
            UpdateStatus?.Invoke($"Target Manuals  path: {TManualsP}");

            TMusicsP = Machine.PlatformFolders.First((x) => x.MediaType == "Music").FolderPath;
            UpdateStatus?.Invoke($"Target Musics path: {TMusicsP}");

            TVideosP = Machine.PlatformFolders.First((x) => x.MediaType == "Video").FolderPath;
            UpdateStatus?.Invoke($"Target Videos path: {TVideosP}");

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

                // todo 7zip

                // Chargement des données du jeu
                LBGame lbGame = XML_Games.Scrap_GameLB(Path.Combine(tmpPath, "EBGame.xml"), "LaunchBox_Backup", PS.Default.wCustomFields);
                UpdateStatus?.Invoke($"Game info xml loaded: {lbGame.Title}");

                // Modification des chemins dans le jeu
                Modify_Paths(lbGame);

                // Modification de la platforme du jeu
                UpdateStatus?.Invoke($"Altération of platform {lbGame.Platform} => {PlatformName}");
                lbGame.Platform = PlatformName;

                // Copier
                Copy_LBManager(lbGame, tmpPath);

                /*// Platform modification
                if (PS.Default.ChangePlatform)
                    XMLBackup.Change_Platform(Path.Combine(destPath, "EBGame.xml"), machine);*/

                // Injection
                XML_Games.InjectGame(lbGame, MachineXMLFile, PS.Default.wCustomFields);
                UpdateStatus?.Invoke($"Injection in xml Launchbox's files");
                //XMLBackup.Copy_EBGame(gameName, Path.Combine(tmpPath, "EBGame.xml"), MachineXMLFile);


                // Effacer le dossier temporaire
                Delete(tmpPath);

                UpdateStatus?.Invoke("Game Finished");
                UpdateProgress?.Invoke(100);
                i++;
            }

            UpdateStatus?.Invoke("Task Finished");
            HeTrace.RemoveLogger("LaunchBox");
            UpdateProgressT?.Invoke(100);

            return true;
        }


        private void Copy_LBManager(LBGame lbGame, string tempFolder)
        {
            // todo ajouter une fonctino pour grouper les jeux dans le sous dossier
            //string gameF = Path.Combine(Path.GetDirectoryName(lbGame.ApplicationPath), lbGame.Title);

            UpdateStatus?.Invoke("Copy files");
            MaximumProgress?.Invoke(6);
            UpdateProgress?.Invoke(0);

            int i = 0;
            foreach (string d in Directory.GetDirectories(tempFolder))
            {
                UpdateProgress?.Invoke(i);

                string dirName = Path.GetFileName(d);

                // Games
                if (dirName == PS.Default.Games)
                {
                    string tmp = Path.GetDirectoryName(lbGame.ApplicationPath);
                    UpdateStatus?.Invoke($"\t{Lang.I_Copy}: {Lang.Games} => '{tmp}'");
                    CopyContent(d, tmp);
                }

                // Cheat Codes
                else if (dirName == PS.Default.CheatCodes)
                {
                    UpdateStatus?.Invoke($"\t{Lang.I_Copy}: {Lang.CheatCodes} => '{TCheatsCodesP}'");
                    CopyContent(d, TCheatsCodesP);
                }

                // Images
                else if (dirName == PS.Default.Images)
                {
                    UpdateStatus?.Invoke($"\t{Lang.I_Copy}: {Lang.Images} => '{TImagesP}'");
                    CopyContent(d, TImagesP);
                }

                // Manuals
                else if (dirName == PS.Default.Manuals)
                {
                    string tmp = Path.GetDirectoryName(lbGame.ManualPath);
                    UpdateStatus?.Invoke($"\t{Lang.I_Copy}: {Lang.Manuals} => ({ tmp }'");
                    CopyContent(d, tmp);
                }

                // Musics
                else if (dirName == PS.Default.Musics)
                {
                    string tmp = Path.GetDirectoryName(lbGame.MusicPath);
                    UpdateStatus?.Invoke($"\t{Lang.I_Copy}: {Lang.Musics} => '{tmp}'");
                    CopyContent(d, tmp);
                }

                // Videos
                else if (dirName == PS.Default.Videos)
                {
                    string tmp = Path.GetDirectoryName(lbGame.VideoPath);
                    UpdateStatus?.Invoke($"\t{Lang.I_Copy}: {Lang.Videos} => '{tmp}'");
                    CopyContent(d, tmp);
                }

                i++;
            }

            UpdateProgress?.Invoke(6);
        }


        protected void Modify_Paths(LBGame lbGame)
        {
            Application.Current.Dispatcher?.Invoke(() =>
            {
                W_ModTargetPaths window = new W_ModTargetPaths()
                {
                    Model = new Models.M_ModTargetPaths()
                    {
                        Game = lbGame,
                        TGamePath = TGamesP,
                        TImagesPath = TImagesP,
                        TManualPath = TManualsP,
                        TMusicsPath = TMusicsP,
                        TVideosPath = TVideosP,
                    }
                };
                if (window.ShowDialog() == true)
                {
                }
                else
                {
                    TokenSource.Cancel();

                }

            });

            /*
            new W_ModTargetPaths()
            {
                Model = new Models.M_ModTargetPaths()
                {
                    Game = lbGame,
                    TGamePath = TGamesP,
                    TImagesPath = TImagesP,
                    TManualPath = TManualsP,
                    TMusicsPath = TMusicsP,
                    TVideosPath = TVideosP,
                }
            }.ShowDialog());
            */

            //lbGame.ApplicationPath
        }
    }
}
