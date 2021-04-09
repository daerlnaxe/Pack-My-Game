using Common_PMG.BackupLB;
using Common_PMG.Container.AAPP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Unbroken.LaunchBox.Plugins.Data;

namespace Common_PMG.Container.Game
{
    // 1-29 les infos relatives au jeu
    // 30 - 49 complément info
    // 50 les chemins
    // 70 infos et params de launchbox
    // 100 les paramètres du jeu
    //
    // 200 stats
    //[XmlRoot("Game")]
    public class LBGame : MedGame, I_MedGame
    {

        #region complément jeu (fini, status...) 100-130

        [XmlElement(Order = 100)]
        public string SortTitle { get; set; }                   //

        [XmlElement(Order = 101)]
        public string Status { get; set; }                      //

        [XmlElement(Order = 102)]
        public bool Completed { get; set; }                     //false

        [XmlElement(Order = 103)]
        public DateTime DateAdded { get; set; }                   //2018-09-05T07:16:32.4860113+02:00

        [XmlElement(Order = 104)]
        public DateTime DateModified { get; set; }                //2018-09-05T08:37:23.3866486+02:00

        [XmlElement(Order = 105)]
        public bool Favorite { get; set; }                      //false
        #endregion


        #region Sources externes 150 - 169
        [XmlElement(Order = 150)]
        public string WikipediaUrl { get; set; }                //https://en.wikipedia.org/wiki/3_Ninjas_Kick_Back_(video_game)

        [XmlElement(Order = 151)]
        public int? WikipediaId { get; set; }

        [XmlElement(Order = 152)]
        public string VideoUrl { get; set; }

        [XmlElement(Order = 153)]
        //    public int DatabaseID { get; set; }                     //3371
        public int? LaunchBoxDbId { get; set; }

        #endregion

        #region chemins 200 - 229
        /// <summary>
        /// Chemin de l'application
        /// </summary>
        /// <remarks>
        /// ex: '..\..\Games\Roms\Sega Mega Drive\Pending\3 Ninjas Kick Back(1994)(Sony Imagesoft) (US).zip'
        /// </remarks>
        [XmlElement(Order = 200)]
        public string ApplicationPath
        {
            get;
            set;
        } = "";            

        [XmlElement(Order = 201)]
        public string ManualPath { get; set; } = "";               //..\..\Games\Manuels\Sega Mega Drive\3_Ninjas_Kick_Back_1994_Sony_Imagesoft_US.pdf

        [XmlElement(Order = 202)]
        public string MusicPath { get; set; } = "";                 //

        [XmlElement(Order = 203)]
        public string VideoPath { get; set; } = "";                 //

        [XmlElement(Order = 204)]
        public string ThemeVideoPath { get; set; } = "";

        [XmlElement(Order = 205)]
        public string DosBoxConfigurationPath { get; set; } = "";    //

        #endregion

        #region Images Path 230 - 299
        [XmlElement(Order = 230)]
        public string ScreenshotImagePath { get; }

        [XmlElement(Order = 231)]
        public string FrontImagePath { get; }

        [XmlElement(Order = 232)]
        public string MarqueeImagePath { get; }

        [XmlElement(Order = 233)]
        public string BackImagePath { get; }

        [XmlElement(Order = 234)]
        public string Box3DImagePath { get; }

        [XmlElement(Order = 235)]
        public string BackgroundImagePath { get; }

        [XmlElement(Order = 236)]
        public string Cart3DImagePath { get; }

        [XmlElement(Order = 237)]
        public string CartFrontImagePath { get; }

        [XmlElement(Order = 238)]
        public string CartBackImagePath { get; }

        [XmlElement(Order = 239)]
        public string ClearLogoImagePath { get; }

        [XmlElement(Order = 240)]
        public string PlatformClearLogoImagePath { get; }
        #endregion


        #region Stats 300 - 349
        [XmlElement(Order = 300)]
        public int PlayCount { get; set; }                      //2

        [XmlElement(Order = 301)]
        public DateTime? LastPlayedDate { get; set; }              //2018-09-05T07:22:32.9731108+02:00

        [XmlElement(Order = 302)]
        // - Modifié le 18/07/2020 car la notation est en float dans le xml
        public float CommunityStarRating { get; set; }            //0</CommunityStarRating{get; set;}

        [XmlElement(Order = 303)]
        public int CommunityStarRatingTotalVotes { get; set; }  //0</CommunityStarRatingTotalVotes{get; set;}

        [XmlElement(Order = 304)]
        public float StarRatingFloat { get; set; }                //0

        [XmlElement(Order = 305)]
        public int StarRating { get; set; }                     //0

        [XmlIgnore]
        public float CommunityOrLocalStarRating { get; }
        #endregion

        // ---

        #region Params dans Launchbox 350 - 399

        [XmlElement(Order = 351)]
        public bool AggressiveWindowHiding { get; set; }

        [XmlElement(Order = 352)]
        public bool Hide { get; set; }                          //false

        [XmlElement(Order = 353)]
        public bool HideMouseCursorInGame { get; set; }

        [XmlElement(Order = 354)]
        public bool DisableShutdownScreen { get; set; }
        #endregion

        // ---

        #region params de lancement  400-499
        [XmlElement(Order = 400)]
        public bool UseDosBox { get; set; }                     //false

        [XmlElement(Order = 401)]
        public string CommandLine { get; set; }

        [XmlElement(Order = 402)]
        public string ConfigurationCommandLine { get; set; }    //

        [XmlElement(Order = 403)]
        public string ConfigurationPath { get; set; }           //

        [XmlElement(Order = 404)]
        //public string Emulator { get; set; }                    //46980dd1-982d-4b30-ad3b-fda6139752c9
        public string EmulatorId { get; set; }

        [XmlElement(Order = 405)]
        public bool UseScummVm { get; set; }                    //false

        [XmlElement(Order = 406)]
        public int StartupLoadDelay { get; set; }

        #endregion

        #region Scumm 700 - 703
        [XmlElement(Order = 700)]
        public bool ScummVmAspectCorrection { get; set; }       //false

        [XmlElement(Order = 701)]
        public bool ScummVmFullscreen { get; set; }             //false

        [XmlElement(Order = 702)]
        public string ScummVmGameDataFolderPath { get; set; }   //


        [XmlElement(Order = 703)]
        public string ScummVmGameType { get; set; }             //  


        #endregion


        [XmlElement(Order = 1000)]
        public string RootFolder { get; set; }                  //

        [XmlElement(Order = 1001)]
        public string Source { get; set; }                      //

        [XmlElement(Order = 1002)]
        public bool MissingVideo { get; set; }                  //false

        [XmlElement(Order = 1003)]
        public bool MissingBoxFrontImage { get; set; }          //false

        [XmlElement(Order = 1004)]
        public bool MissingScreenshotImage { get; set; }        //false

        [XmlElement(Order = 1005)]
        public bool MissingClearLogoImage { get; set; }         //false

        [XmlElement(Order = 1006)]
        public bool MissingBackgroundImage { get; set; }        //false


        [XmlElement(Order = 1007)]
        public bool Broken { get; set; }                        //false

        [XmlElement(Order = 1008)]
        public bool Portable { get; set; }                       //false
        [XmlElement(Order = 1009)]
        public bool UseStartupScreen { get; set; }


        [XmlElement(Order = 1010)]
        public bool OverrideDefaultStartupScreenSettings { get; set; }
        [XmlElement(Order = 1011)]
        public bool HideAllNonExclusiveFullscreenWindows { get; set; }

        [XmlElement(Order = 1012)]
        public string ReleaseType { get; set; }

        // ---

        [XmlIgnore]
        public List<CustomField> CustomFields { get; set; } = new List<CustomField>(); // 2020 initialization here

        //[XmlIgnore] 2021 on a dégagé ça pour une version ou les clones sont récupérés autrement
        //public List<AdditionalApplication> AdditionalApplications { get; set; } = new List<AdditionalApplication>(); //2020 initialization here


    }

}
