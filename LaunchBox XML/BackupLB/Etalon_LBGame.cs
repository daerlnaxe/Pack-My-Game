using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins.Data;

namespace Pack_My_Game.BackupLB
{
    /// <summary>
    /// Utilisé juste pour vérifier qu'on a les versions les plus à jour (penser à le remettre en compilation régulièrement)
    /// </summary>
    class LBGame : IGame
    {
        #region Informations de base (Dans Shortgame)
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Titre
        /// </summary>
        public string Title { get; set; }
        
        /// <summary>
        /// Region
        /// </summary>
        public string Region { get; set; }
        
        
        #endregion

        // Path to Application (nom de la rom)
        public string ApplicationPath { get; set; }
        
        // Id du jeu

        #region infos jeu
        // Developer
        public string Developer { get; set; }
        // Editeur
        public string Publisher { get; set; }

        #endregion

        // ???
        public string CommandLine { get; set; }

        // ??? Quand on finit un jeu ?
        public bool Completed { get; set; }
        
        // ???
        public string ConfigurationCommandLine { get; set; }
        
        // ???
        public string ConfigurationPath { get; set; }

        #region stats Launchbox
        // Date d'ajout
        public DateTime DateAdded { get; set; }

        // Date de modification
        public DateTime DateModified { get; set; }
        #endregion

        #region stats joueur
        public bool Favorite { get; set; }

        #endregion

        #region Images Path
        // Fond
        public string BackgroundImagePath { get; set; }
        // image dos
        public string BackImagePath { get; set; }
        // box 3d
        public string Box3DImagePath { get; set; }
        // Cartouche 3d
        public string Cart3DImagePath{ get; set; }
        // image 2d
        public string FrontImagePath { get; set; }
        // image tranche
        public string MarqueeImagePath { get; set; }
        // Screenshot
        public string ScreenshotImagePath { get; set; }



        public string CartFrontImagePath { get; set; }
        public string CartBackImagePath { get; set; }
        #endregion
        public Image RatingImage => throw new NotImplementedException();



        // todo plein de balises en plus




        public string ClearLogoImagePath => throw new NotImplementedException();

        public string DetailsWithPlatform => throw new NotImplementedException();

        public string DetailsWithoutPlatform => throw new NotImplementedException();

        public string PlatformClearLogoImagePath => throw new NotImplementedException();

        
        public string DosBoxConfigurationPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string EmulatorId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }



        public DateTime? LastPlayedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ManualPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string MusicPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Notes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Platform { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Rating { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime? ReleaseDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? ReleaseYear { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string RootFolder { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool ScummVmAspectCorrection { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool ScummVmFullscreen { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ScummVmGameDataFolderPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ScummVmGameType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool ShowBack { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string SortTitle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Source { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int StarRating { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public float CommunityOrLocalStarRating => throw new NotImplementedException();

        public float StarRatingFloat { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float CommunityStarRating { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int CommunityStarRatingTotalVotes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Status { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? LaunchBoxDbId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? WikipediaId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string WikipediaUrl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool UseDosBox { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool UseScummVm { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Version { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Series { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string PlayMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int PlayCount { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Portable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string VideoPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ThemeVideoPath { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Hide { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Broken { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string CloneOf { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string GenresString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public BlockingCollection<string> Genres => throw new NotImplementedException();

        public string[] PlayModes => throw new NotImplementedException();

        public string[] Developers => throw new NotImplementedException();

        public string[] Publishers => throw new NotImplementedException();

        public string[] SeriesValues => throw new NotImplementedException();

        public string SortTitleOrTitle => throw new NotImplementedException();

        public bool OverrideDefaultStartupScreenSettings { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool UseStartupScreen { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool HideAllNonExclusiveFullscreenWindows { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int StartupLoadDelay { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool HideMouseCursorInGame { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DisableShutdownScreen { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool AggressiveWindowHiding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool? Installed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ReleaseType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int? MaxPlayers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string VideoUrl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IAdditionalApplication AddNewAdditionalApplication()
        {
            throw new NotImplementedException();
        }

        public IAlternateName AddNewAlternateName()
        {
            throw new NotImplementedException();
        }

        public ICustomField AddNewCustomField()
        {
            throw new NotImplementedException();
        }

        public IMount AddNewMount()
        {
            throw new NotImplementedException();
        }

        public string Configure()
        {
            throw new NotImplementedException();
        }

        public IAdditionalApplication[] GetAllAdditionalApplications()
        {
            throw new NotImplementedException();
        }

        public IAlternateName[] GetAllAlternateNames()
        {
            throw new NotImplementedException();
        }

        public ICustomField[] GetAllCustomFields()
        {
            throw new NotImplementedException();
        }

        public ImageDetails[] GetAllImagesWithDetails()
        {
            throw new NotImplementedException();
        }

        public ImageDetails[] GetAllImagesWithDetails(string imageType)
        {
            throw new NotImplementedException();
        }

        public IMount[] GetAllMounts()
        {
            throw new NotImplementedException();
        }

        public string GetBigBoxDetails(bool showPlatform)
        {
            throw new NotImplementedException();
        }

        public string GetManualPath()
        {
            throw new NotImplementedException();
        }

        public string GetMusicPath()
        {
            throw new NotImplementedException();
        }

        public string GetNewManualFilePath(string extension)
        {
            throw new NotImplementedException();
        }

        public string GetNewMusicFilePath(string extension)
        {
            throw new NotImplementedException();
        }

        public string GetNewThemeVideoFilePath(string extension)
        {
            throw new NotImplementedException();
        }

        public string GetNewVideoFilePath(string extension)
        {
            throw new NotImplementedException();
        }

        public string GetNextAvailableImageFilePath(string extension, string imageType, string region)
        {
            throw new NotImplementedException();
        }

        public string GetNextVideoFilePath(string videoType, string extension)
        {
            throw new NotImplementedException();
        }

        public string GetThemeVideoPath()
        {
            throw new NotImplementedException();
        }

        public string GetVideoPath(bool prioritizeThemeVideos = false)
        {
            throw new NotImplementedException();
        }

        public string GetVideoPath(string videoType)
        {
            throw new NotImplementedException();
        }

        public string OpenFolder()
        {
            throw new NotImplementedException();
        }

        public string OpenManual()
        {
            throw new NotImplementedException();
        }

        public string Play()
        {
            throw new NotImplementedException();
        }

        public bool TryRemoveAdditionalApplication(IAdditionalApplication additionalApplication)
        {
            throw new NotImplementedException();
        }

        public bool TryRemoveAlternateNames(IAlternateName alternateName)
        {
            throw new NotImplementedException();
        }

        public bool TryRemoveCustomField(ICustomField customField)
        {
            throw new NotImplementedException();
        }

        public bool TryRemoveMount(IMount mount)
        {
            throw new NotImplementedException();
        }
    }
}
