using DxLocalTransf;
using DxTBoxCore.Box_Progress;
using DxTBoxCore.Common;
using DxTBoxCore.MBox;
using Hermes;
using Hermes.Cont;
using Hermes.Messengers;
using Common_PMG.BackupLB;
using Common_PMG.Container;
using Common_PMG.Container.AAPP;
using Common_PMG.Container.Game;
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
        public override event DoubleHandler UpdateProgressT;
        public override event MessageHandler UpdateStatusT;
        public override event DoubleHandler MaximumProgressT;

        public override event DoubleHandler UpdateProgress;
        public override event MessageHandler UpdateStatus;
        public override event DoubleHandler MaximumProgress;
        public override CancellationToken CancelToken { get; }

        #endregion
        public string PlatformName { get; }

        #region Informations
        protected Platform Machine { get; set; }

        protected string MachineXMLFile { get; set; }

        #endregion


        public C_LaunchBoxAdapt(List<Cont.FileObj> games, string platformName) : base("LaunchBoxAdapt", games)
        {
            CancelToken = base.TokenSource.Token;
            PlatformName = platformName;
            //Games = games;
        }

        public object Run(int timeSleep = 10)
        {
            try
            {
                /*

                log.AddCaller(this);
                HeTrace.AddLogger("LaunchBox", log);
                */

                //UpdateStatus += (x, y) => HeTrace.WriteLine(y, this);

                // Redirige les signaux
                //RedirectSignals();


                // Récupération des infos de la plateforme
                UpdateStatus?.Invoke(this, "Get infos from platform");
                /*
                 * Normalement on peut virer
                 * XML_Functions xf = new XML_Functions();
                xf.ReadFile(Common.PlatformsFile);

                Machine = xf.ScrapPlatform(PlatformName);*/

                Machine = XML_Platforms.GetPlatformPaths(Common.PlatformsFile, PlatformName);

                if (Machine.PlatformFolders.Count < 1)
                {
                    UpdateStatus?.Invoke(this, "Error: this machine has no path");
                    return false;
                }

                // Backup datas
                MachineXMLFile = Path.Combine(PS.Default.LastLBpath, PS.Default.dPlatforms, $"{PlatformName}.xml");
                BackupPlatformFile(MachineXMLFile);
                UpdateStatus?.Invoke(this, $"Backup of '{MachineXMLFile}'");

                // Initialisation des dossiers cible
                // Memo solution la plus simple, fixant des limites et normalement évolutive
                string root = Path.GetDirectoryName(Path.GetDirectoryName(Machine.FolderPath));
                TGamesP = Path.Combine(Machine.FolderPath);
                UpdateStatus?.Invoke(this, $"Target Game path: {TGamesP}");

                TCheatsCodesP = Path.Combine(root, "Cheat Codes", PlatformName);
                UpdateStatus?.Invoke(this, $"Target Cheats path: {TCheatsCodesP}");

                TImagesP = Path.GetDirectoryName(Machine.PlatformFolders.First((x) => x.MediaType.Contains("Box", StringComparison.OrdinalIgnoreCase)).FolderPath);
                UpdateStatus?.Invoke(this, $"Target Images path: {TImagesP}");

                TManualsP = Machine.PlatformFolders.First((x) => x.MediaType == "Manual").FolderPath;
                UpdateStatus?.Invoke(this, $"Target Manuals  path: {TManualsP}");

                TMusicsP = Machine.PlatformFolders.First((x) => x.MediaType == "Music").FolderPath;
                UpdateStatus?.Invoke(this, $"Target Musics path: {TMusicsP}");

                TVideosP = Machine.PlatformFolders.First((x) => x.MediaType == "Video").FolderPath;
                UpdateStatus?.Invoke(this, $"Target Videos path: {TVideosP}");

                //
                int i = 0;
                //MaximumProgressT?.Invoke(this, Games.Count());
                //MaximumProgress?.Invoke(this, 100);
                foreach (FileObj game in Games)
                {
                    UpdateProgressT?.Invoke(this, i);

                    UpdateStatus?.Invoke(this, $"Work on: {game.Nom}");

                    string gameName = Path.GetFileNameWithoutExtension(game.Nom);
                    string tmpPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(game.Nom));

                    // Décompresser
                    if (Path.GetExtension(game.Path).Equals(".zip", StringComparison.OrdinalIgnoreCase))
                        ZipDecompression.UnCompressArchive(game.Path, tmpPath, CancelToken);


                    if (CancelToken.IsCancellationRequested)
                    {
                        UpdateStatus?.Invoke(this, "Stopped by user");
                        return false;
                    }

                    // todo 7zip

                    // Chargement des données du jeu
                    string xmlFile = Path.Combine(tmpPath, "EBGame.xml");
                    LBGame lbGame = XML_Games.Scrap_LBGame(xmlFile);
                    List<AdditionalApplication> clones = XML_Games.ListAddApps(xmlFile);



                    //05/04/2021                LBGame lbGame = XML_Games.Scrap_GameLB(Path.Combine(tmpPath, "EBGame.xml"), "LaunchBox_Backup", PS.Default.wCustomFields);

                    UpdateStatus?.Invoke(this, $"Game info xml loaded: {lbGame.Title}");

                    // Modification des chemins dans le jeu
                    Modify_Paths(lbGame, clones);

                    // Modification de la platforme du jeu
                    UpdateStatus?.Invoke(this, $"Altération of platform {lbGame.Platform} => {PlatformName}");
                    lbGame.Platform = PlatformName;

                    // Copier
                    Copy_LBManager(lbGame, tmpPath);

                    /*// Platform modification
                    if (PS.Default.ChangePlatform)
                        XMLBackup.Change_Platform(Path.Combine(destPath, "EBGame.xml"), machine);*/


                    // Retrait du jeu si présence
                    bool? replace = false;
                    if (XML_Custom.TestPresence(MachineXMLFile, "Game", nameof(lbGame.Id).ToUpper(), lbGame.Id))
                        replace = AskDxMBox("Game is Already present", "Question", E_DxButtons.Yes | E_DxButtons.No, lbGame.Title);

                    if (replace == true)
                        XML_Games.Remove_Game(lbGame.Id, MachineXMLFile);


                    // Injection
                    XML_Games.InjectGame(lbGame, MachineXMLFile);
                    XML_Games.InjectAddApps(clones, MachineXMLFile);
                    if (PS.Default.wCustomFields)
                    {
                        //var r = XML_Games.ListCustomFields(xmlFile, "CustomField");
                        XML_Games.Trans_CustomF(xmlFile, MachineXMLFile);
                    }

                    UpdateStatus?.Invoke(this, $"Injection in xml Launchbox's files");
                    //XMLBackup.Copy_EBGame(gameName, Path.Combine(tmpPath, "EBGame.xml"), MachineXMLFile);


                    // Effacer le dossier temporaire
                    Delete(tmpPath);

                    UpdateStatus?.Invoke(this, "Game Finished");
                    UpdateProgress?.Invoke(this, 100);
                    i++;
                }

                UpdateStatus?.Invoke(this, "Task Finished");
                HeTrace.RemoveLogger("LaunchBoxAdapt");
                UpdateProgressT?.Invoke(this, 100);

                return true;
            }
            catch (Exception exc)
            {
                HeTrace.WriteLine(exc.Message);
                return false;
            }
        }



        private void Copy_LBManager(LBGame lbGame, string tempFolder)
        {
            // todo ajouter une fonctino pour grouper les jeux dans le sous dossier
            //string gameF = Path.Combine(Path.GetDirectoryName(lbGame.ApplicationPath), lbGame.Title);

            UpdateStatus?.Invoke(this, "Copy files");
            MaximumProgress?.Invoke(this, 6);
            UpdateProgress?.Invoke(this, 0);

            int i = 0;
            foreach (string d in Directory.GetDirectories(tempFolder))
            {
                UpdateProgress?.Invoke(this, i);

                string dirName = Path.GetFileName(d);

                // Games
                if (dirName == PS.Default.Games)
                {
                    string tmp = Path.GetDirectoryName(lbGame.ApplicationPath);
                    UpdateStatus?.Invoke(this, $"\t{Lang.I_Copy}: {Lang.Games} => '{tmp}'");
                    CopyContent(d, tmp);
                }

                // Cheat Codes
                else if (dirName == PS.Default.CheatCodes)
                {
                    UpdateStatus?.Invoke(this, $"\t{Lang.I_Copy}: {Lang.CheatCodes} => '{TCheatsCodesP}'");
                    CopyContent(d, TCheatsCodesP);
                }

                // Images
                else if (dirName == PS.Default.Images)
                {
                    UpdateStatus?.Invoke(this, $"\t{Lang.I_Copy}: {Lang.Images} => '{TImagesP}'");
                    CopyContent(d, TImagesP);
                }

                // Manuals
                else if (dirName == PS.Default.Manuals)
                {
                    string tmp = Path.GetDirectoryName(lbGame.ManualPath);
                    UpdateStatus?.Invoke(this, $"\t{Lang.I_Copy}: {Lang.Manuals} => ({ tmp }'");
                    CopyContent(d, tmp);
                }

                // Musics
                else if (dirName == PS.Default.Musics)
                {
                    string tmp = Path.GetDirectoryName(lbGame.MusicPath);
                    UpdateStatus?.Invoke(this, $"\t{Lang.I_Copy}: {Lang.Musics} => '{tmp}'");
                    CopyContent(d, tmp);
                }

                // Videos
                else if (dirName == PS.Default.Videos)
                {
                    string tmp = Path.GetDirectoryName(lbGame.VideoPath);
                    UpdateStatus?.Invoke(this, $"\t{Lang.I_Copy}: {Lang.Videos} => '{tmp}'");
                    CopyContent(d, tmp);
                }

                i++;
            }

            UpdateProgress?.Invoke(this, 6);
        }


        /// <summary>
        /// Open a window to ask for paths
        /// </summary>
        /// <param name="lbGame"></param>
        protected void Modify_Paths(LBGame lbGame, List<AdditionalApplication> clones)
        {
            Application.Current.Dispatcher?.Invoke(() =>
            {
                W_ModTargetPaths window = new W_ModTargetPaths()
                {
                    Model = new Models.M_ModTargetPaths()
                    {
                        Game = lbGame,
                        Clones = clones,
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
