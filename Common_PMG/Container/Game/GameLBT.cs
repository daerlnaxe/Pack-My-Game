using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Unbroken.LaunchBox.Plugins.Data;

namespace Common_PMG.Container.Game
{
    /// <summary>
    /// réplique du modèle du plugin
    /// </summary>
    public class GameLBT : LBGame, I_FullGame
    {
        public GameLBT()
        {
        }


        public System.Drawing.Image RatingImage => throw new NotImplementedException();


        public string DetailsWithPlatform => throw new NotImplementedException();
        public string DetailsWithoutPlatform => throw new NotImplementedException();
        public int? ReleaseYear { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool ShowBack { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
      
        public bool? Installed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public string CloneOf { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string GenresString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public BlockingCollection<string> Genres => throw new NotImplementedException();


        public string[] PlayModes => throw new NotImplementedException();

        public string[] Developers => throw new NotImplementedException();

        public string[] Publishers => throw new NotImplementedException();

        public string[] SeriesValues => throw new NotImplementedException();

        public string SortTitleOrTitle => throw new NotImplementedException();



        #region Methods not implemented
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
        #endregion
    }

}
