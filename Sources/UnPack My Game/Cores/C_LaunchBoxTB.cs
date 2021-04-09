using DxLocalTransf;
using DxTBoxCore.Common;
using Hermes;
using Hermes.Cont;
using Hermes.Messengers;
using Common_PMG.Container;
using Common_PMG.Container.Game;
using LaunchBox_XML.XML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using UnPack_My_Game.Decompression;
using PS = UnPack_My_Game.Properties.Settings;
using Cst = LaunchBox_XML.Common;
using UnPack_My_Game.Resources;

namespace UnPack_My_Game.Cores
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// - Les fichiers images, videos, manuals, musics sont copiés en fonction du fichier plateforme
    /// - Ca n'a strictement aucun intérêt de suivre les chemins du TBGame (exemple de l'importation)
    /// - On va réaffecter par contre:
    ///     * Video
    ///     * Thème Video
    ///     * Manual
    ///     * Music
    /// - Lorsqu'on réaffecte si c'est vide on prend le premier qui passe de la liste.
    /// - Si ce n'est pas vide on va chercher par rapport au nom du fichier (attention à la tail)
    /// </remarks>
    class C_LaunchBoxTB : C_LaunchBox
    {
        public override event DoubleHandler UpdateProgressT;
        public override event MessageHandler UpdateStatusT;
        public override event DoubleHandler MaximumProgressT;

        public override event DoubleHandler UpdateProgress;
        public override event MessageHandler UpdateStatus;
        public override event DoubleHandler MaximumProgress;

        public override CancellationToken CancelToken { get; }

        private string _SelectedPlatformXML;

        public C_LaunchBoxTB(List<Cont.FileObj> games, string selectedPlatformXml) : base("LaunchBoxTB", games)
        {
            CancelToken = base.TokenSource.Token;
            _SelectedPlatformXML = selectedPlatformXml;
        }

        public object Run(Platform machine)
        {
            try
            {
                // Lecture du fichier xml de la plateforme (verbatim)
                UpdateStatus?.Invoke(this, "Get infos from platform");


                //var srcPlatform = XML_Platforms.Read(_SelectedPlatformXML);
                //string platformName = XML_Platforms.GetByTag(srcPlatform.Element("Platform"), "Name") as string;

                // L'objet n'est utilisé que pour avoir des données, il ne sert pas pour l'écriture


                if (machine.PlatformFolders.Count < 1)
                {
                    UpdateStatus?.Invoke(this, "Error: this machine has no path");
                    return false;
                }

                // Travail sur le fichier des plateformes
                string platformsFile = Path.Combine(PS.Default.LastLBpath, PS.Default.fPlatforms);
                WorkOnPlatformFile(platformsFile, machine.Name);

                // Backup datas
                string xmlPlateforme = Path.Combine(PS.Default.LastLBpath, PS.Default.dPlatforms, $"{machine.Name}.xml");
                if (File.Exists(xmlPlateforme))
                {
                    BackupPlatformFile(xmlPlateforme);
                    UpdateStatus?.Invoke(this, $"Backup of '{xmlPlateforme}'");
                }


                // --- Initialisation des dossiers cible
                string root = Path.GetDirectoryName(Path.GetDirectoryName(machine.FolderPath));
                InitTargetPaths(machine, root);


                int done = 0;
                int i = 0;
                foreach (Cont.FileObj game in Games)
                {
                    UpdateProgressT?.Invoke(this, i);

                    UpdateStatus?.Invoke(this, $"Work on: {game.Nom}");

                    string gameName = Path.GetFileNameWithoutExtension(game.Nom);
                    string tmpPath = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(game.Nom));

                    // Décompression
                    if (Path.GetExtension(game.Path).Equals(".zip", StringComparison.OrdinalIgnoreCase))
                        ZipDecompression.UnCompressArchive(game.Path, tmpPath, CancelToken);

                    if (CancelToken.IsCancellationRequested)
                    {
                        UpdateStatus?.Invoke(this, "Stopped by user");
                        return false;
                    }


                    // Chargement des données du jeu
                    string xmlFile = Path.Combine(tmpPath, "TBGame.xml");
                    XElement verbatimGame = XML_Games.GetGameNode(xmlFile);

                    BasicGame baseGame = XML_Games.Scrap_BasicGame<BasicGame>(verbatimGame);
#if DEBUG
                    baseGame.Platform = "Sega Genesis";
#endif
                    /*
                    string gameTitle = XML_Games.GetByTag(verbatimGame, "Title");
                    UpdateStatus?.Invoke(this, $"Game Title: {gameTitle}");

                    string platformGame = XML_Games.GetByTag(verbatimGame, "Platform");
                    UpdateStatus?.Invoke(this, $"Game for {platformGame}");

                    string gameID = XML_Games.GetByTag(verbatimGame, "ID");
                    UpdateStatus?.Invoke(this, $"GameId: {gameID}");*/



                    // Test pour vérifier si ce sont les mêmes plateformes
                    if (!machine.Name.Equals(baseGame.Platform))
                    {
                        UpdateStatus?.Invoke(this, $"Aborded, wrong plateforme '{game.Nom}'");
                        i++;
                        UpdateProgressT?.Invoke(this, i);
                        continue;
                    }

                    // Copies 
                    //Copy_Images(Path.Combine(tmpPath, PS.Default.Images), TImagesP);


                    // Copie et modifie
                    ManageFiles(verbatimGame, tmpPath, Cst.ManualP);

                    //Copy_TBManager(tmpPath);

                    WorkOnGamesFile(xmlPlateforme, verbatimGame, xmlFile, baseGame);


                    // Effacer le dossier temporaire
                    Delete(tmpPath);

                    //i++;
                    UpdateProgress?.Invoke(this, 100);
                    UpdateStatus?.Invoke(this, "Game Finished");

                    done++;
                }

                UpdateStatus?.Invoke(this, $"Number of games processed: {done}/{Games.Count()}");

                return true;
            }
            catch (Exception exc)
            {
                return false;
            }
        }





        /// <summary>
        /// Travail sur le fichier xml contenant les plateformes
        /// </summary>
        /// <param name="platformsFile"></param>
        /// <param name="machineName"></param>
        private void WorkOnPlatformFile(string platformsFile, string machineName)
        {
            using (XML_Platforms xPlat = new XML_Platforms(platformsFile))
            {
                // Backup du fichier de la plateforme;
                BackupPlatformsFile(platformsFile);

                // On demande pour REMPLACER la machine si elle existe déjà
                if (xPlat.Exists("Name", machineName))
                {
                    bool? res = AskDxMBox("Platform is Already present, replace it ?", "Question", E_DxButtons.Yes | E_DxButtons.No, machineName);
                    if (res == true)
                    {
                        xPlat.RemoveElemByChild(Cst.Platform, Cst.Name, machineName);
                        xPlat.RemoveElemByChild(Cst.PlatformFolder, Cst.Platform, machineName);
                        UpdateStatus?.Invoke(this, "The has been removed");
                    }
                    else
                    {
                        UpdateStatus?.Invoke(this, "Injection aborted");
                        return;
                    }
                }

                // Injection en mode true verbatim
                XElement verbatimPlatform = XML_Platforms.GetPlatformNode(_SelectedPlatformXML);
                xPlat.InjectPlatform(verbatimPlatform);
                var verbatimPFolders = XML_Platforms.GetFoldersNodes(_SelectedPlatformXML);
                xPlat.InjectPlatFolders(verbatimPFolders);

                UpdateStatus?.Invoke(this, $"Platform {machineName} injected");

                xPlat.Save(platformsFile);
            }
        }


        /// <summary>
        /// Détermine les dossiers cibles pour la copie
        /// </summary>
        /// <param name="machine"></param>
        /// <param name="root"></param>
        /// <remarks>
        /// A la copie certains dossiers sont susceptibles de changer
        /// </remarks>
        private void InitTargetPaths(Platform machine, string root)
        {
            TGamesP = machine.FolderPath;
            UpdateStatus?.Invoke(this, $"Target Game path: {TGamesP}");

            TCheatsCodesP = Path.Combine(root, "Cheat Codes", machine.Name);
            UpdateStatus?.Invoke(this, $"Target Cheats path: {TCheatsCodesP}");

            TImagesP = Path.GetDirectoryName(machine.PlatformFolders.First((x) => x.MediaType.Contains("Box", StringComparison.OrdinalIgnoreCase)).FolderPath);
            UpdateStatus?.Invoke(this, $"Target Images path: {TImagesP}");

            TManualsP = machine.PlatformFolders.First((x) => x.MediaType == "Manual").FolderPath;
            UpdateStatus?.Invoke(this, $"Target Manuals  path: {TManualsP}");

            TMusicsP = machine.PlatformFolders.First((x) => x.MediaType == "Music").FolderPath;
            UpdateStatus?.Invoke(this, $"Target Musics path: {TMusicsP}");

            TVideosP = machine.PlatformFolders.First((x) => x.MediaType == "Video").FolderPath;
            UpdateStatus?.Invoke(this, $"Target Videos path: {TVideosP}");
        }


        /// <summary>
        /// Travaille sur le fichier de sortie pour injecter 
        /// </summary>
        /// <param name="xmlPlateforme"></param>
        /// <param name="baseGame"></param>
        private void WorkOnGamesFile(string xmlPlateforme, XElement verbatimGame, string xmlSrcFile, BasicGame baseGame)
        {
            

            // Travail sur le fichier de plateforme
            using (XML_Games xGame = new XML_Games(xmlPlateforme))
            {
                bool? replace = false;
                if (xGame.Exists("ID", baseGame.Id))
                    replace = AskDxMBox("Game is Already present, replace it (or duplicate) ?", "Question", E_DxButtons.Yes | E_DxButtons.No, baseGame.Title);

                if (replace == true)
                {
                    UpdateStatus?.Invoke(this, $"Removing game: {baseGame.Title}");
                    xGame.RemoveByChild(Cst.Game, Cst.Id, baseGame.Id);

                    UpdateStatus?.Invoke(this, $"Removing AdditionnalApps: {baseGame.Title}");
                    xGame.RemoveByChild(Cst.AddApp, Cst.GameId, baseGame.Id);

                    UpdateStatus?.Invoke(this, $"Removing CustomFields: {baseGame.Title}");
                    xGame.RemoveByChild(Cst.CustField, Cst.GameId, baseGame.Id);

                    UpdateStatus?.Invoke(this, $"Removing AlternateNames");
                    xGame.RemoveByChild(Cst.AltName, Cst.GameId, baseGame.Id);

                }

                // Injection
                UpdateStatus?.Invoke(this, $"Inject game");
                xGame.InjectGame(verbatimGame);
                UpdateStatus?.Invoke(this, $"Inject additionnal applications");
                xGame.InjectAddApps(XML_Games.GetNodes(xmlSrcFile, Cst.AddApp));
                UpdateStatus?.Invoke(this, $"Inject custom fields");
                xGame.InjectCustomF(XML_Games.GetNodes(xmlSrcFile, Cst.CustField));

                /*XML_Games.InjectAddApps(clones, machineXMLFile);
                if (PS.Default.wCustomFields)
                {
                    //var r = XML_Games.ListCustomFields(xmlFile, "CustomField");
                    XML_Games.Trans_CustomF(xmlFile, machineXMLFile);
                }*/

                //XML_Games.Remove_Game(lbGame.Id, machineXMLFile);
            }
        }


        /// <summary>
        /// Copie des fichiers roms en fonctions des additionnals applications et du fichier principal
        /// </summary>
        private void Copy_Roms()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tempFolder"></param>
        /// <param name="gameName"></param>
        /// <remarks>
        ///
        /// </remarks>
        private void Copy_TBManager(string tempFolder)
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
                    // On passe
                    /*
                    string tmp;
                    // prise en charge de la fonction pour placer dans un répertoire au nom du jeu
                    if (PS.Default.wGameNameFolder)
                        tmp = Path.Combine(TGamesP, Cst.WindowsConv_TitleToFileName(gameName));
                    else
                        tmp = TGamesP;

                    UpdateStatus?.Invoke(this, $"\t{Lang.I_Copy}: {Lang.Games} => '{tmp}'");
                    CopyContent(d, tmp);*/
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
                    // --- On passe
                }

                // Manuals
                else if (dirName == PS.Default.Manuals)
                {
                    UpdateStatus?.Invoke(this, $"\t{Lang.I_Copy}: {Lang.Manuals} => ({ TManualsP }'");
                    CopyContent(d, TManualsP);
                }

                // Musics
                else if (dirName == PS.Default.Musics)
                {                    
                    UpdateStatus?.Invoke(this, $"\t{Lang.I_Copy}: {Lang.Musics} => '{TMusicsP}'");
                    CopyContent(d, TMusicsP);
                }

                // Videos
                else if (dirName == PS.Default.Videos)
                {
                    UpdateStatus?.Invoke(this, $"\t{Lang.I_Copy}: {Lang.Videos} => '{TVideosP}'");
                    CopyContent(d, TVideosP);
                }

                i++;
            }

            UpdateProgress?.Invoke(this, 6);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="verbatimGame"></param>
        /// <param name="tmpPath"></param>
        /// <param name="tag"></param>
        private void ManageFiles(XElement verbatimGame , string tmpPath, string tag)
        {
            string d = Path.Combine(tmpPath, PS.Default.Manuals);
            string defFile = XML_Games.GetByTag(verbatimGame, tag);

            foreach (string f in Directory.EnumerateFiles(d ))
            {
                string tail = f.Replace(tmpPath, string.Empty);
                //tail = 

                if(defFile.Contains(tail))
                {

                }
            }

        }


    }
}
