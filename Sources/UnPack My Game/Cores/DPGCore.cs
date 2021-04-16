using Common_PMG.Container;
using Common_PMG.Container.Game;
using DxLocalTransf;
using DxLocalTransf.Progress;
using DxLocalTransf.Progress.ToImp;
using DxTBoxCore.Async_Box_Progress.Basix;
using DxTBoxCore.Box_Progress.Basix;
using Hermes;
using LaunchBox_XML.XML;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using UnPack_My_Game.Decompression;
using UnPack_My_Game.Graph;
using PS = UnPack_My_Game.Properties.Settings;

namespace UnPack_My_Game.Cores
{
    /// <summary>
    /// Tout ce qui est en rapport avec le DPG
    /// </summary>
    internal class DPGCore : A_ProgressPersistD, I_ASBase
    {

        public Queue<DataRep> RejectedDatas { get; private set; } = new Queue<DataRep>();

        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        public CancellationToken CancelToken => TokenSource.Token;
        public bool CancelFlag { get; private set; }

        public bool IsPaused { get; set; }

        public bool IsInterrupted { get; private set; }


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
                    GamePathsExt gpX = null;

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

                    // --- 
#if DEBUG
                    Process.Start("explorer.exe", gamePath);

#endif

                    // Lecture des fichiers
                    if (File.Exists(Path.Combine(gamePath, "DPGame.json")))
                    {
                        gpX = GamePaths.ReadFromJson<GamePathsExt>(Path.Combine(gamePath, "DPGame.json"));
                    }
                    else if (File.Exists(Path.Combine(gamePath, "EBGame.xml")))
                    {
                        gpX = GetMainsInfo(gamePath, "EBGame.xml");
                    }
                    else if (File.Exists(Path.Combine(gamePath, "TBGame.xml")))
                    {
                        gpX = GetMainsInfo(gamePath, "TBGame.xml");
                    }

                    if (gpX == null)
                        return false;

                    // Complément des données
                    gpXCompletion(gpX, mode, zF.ALinkToThePast);

                    // Affichage
                    IHMStatic.ShowDPG(gpX, gamePath);
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

        #region Compression
        void DPGZipCore(DataRep archive, string gamePath)
        {
            // Lecture des préréquis ? <= pas utile je crois

            // Extraction des fichiers xml
            ZipDecompression zippy = new ZipDecompression()
            {
                TokenSource = this.TokenSource,
                IsPaused = this.IsPaused,
            };


            IHMStatic.LaunchDouble(zippy, () => zippy.ExtractSpecificFiles(archive.ALinkToThePast, gamePath,
                                                        "TBGame.xml", "EBGame.xml", "DPGame.json"),
                                                        "Zip Extraction");
            /*var res = PackMe_IHM.ZipCompressFolder(zippy, () => zippy.CompressFolder(
                                         gamePath, title, PS.Default.cZipCompLvl), "Compression Zip");*/

        }

        void DPG7ZipCore(DataRep zF, string gamePath)
        {
            // Extraction des fichiers xml
            SevenZipDecompression zippy = new SevenZipDecompression()
            {
                TokenSource = this.TokenSource,
                IsPaused = this.IsPaused,
            };

            IHMStatic.LaunchDouble(zippy, () => zippy.ExtractSpecificFiles(zF.ALinkToThePast, gamePath,
                                                        "TBGame.xml", "EBGame.xml", "DPGame.json"),
                                                        "SevenZip Extraction");
        }

        #endregion

        #region DPG



        private GamePathsExt GetMainsInfo(string gamePath, string file)
        {
            string xmlFile = Path.Combine(gamePath, file);
            GamePathsExt gpX = GamePathsExt.CreateBasic(XML_Games.Scrap_LBGame(xmlFile));

            return gpX;
        }

        private void gpXCompletion(GamePathsExt gpX, ArchiveMode mode, string archiveName)
        {
            if (mode == ArchiveMode.Zip)
            {
                gpX.CompApps = ZipDecompression.StaticGetFilesStartingBy(archiveName, $"{PS.Default.Games}/").ToList();
                //gpX.CompCheatCodes = ZipDecompression.StaticGetFilesStartingBy(archiveName, $"{PS.Default.CheatCodes}/").ToList();
                gpX.CompManuals = ZipDecompression.StaticGetFilesStartingBy(archiveName, $"{PS.Default.Manuals}/").ToList();
                gpX.CompMusics = ZipDecompression.StaticGetFilesStartingBy(archiveName, $"{PS.Default.Musics}/").ToList();
                //gpX.CompVideos= ZipDecompression.StaticGetFilesStartingBy(archiveName, $"{PS.Default.Videos}/").ToList();
            }
            else if (mode == ArchiveMode.SevenZip)
            {
                gpX.CompApps = SevenZipDecompression.StaticGetFilesStartingBy(archiveName, $"{PS.Default.Games}\\").ToList();
                gpX.CompManuals = SevenZipDecompression.StaticGetFilesStartingBy(archiveName, $"{PS.Default.Manuals}\\").ToList();
                gpX.CompMusics = SevenZipDecompression.StaticGetFilesStartingBy(archiveName, $"{PS.Default.Musics}\\").ToList();

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

        /*public  void StopTask()
        {
            throw new NotImplementedException();
        }*/
    }
}
