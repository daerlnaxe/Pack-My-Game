using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Pack_My_Game.BackupLB
{
    // 1-29 les infos relatives au jeu
    // 30 - 49 complément info
    // 50 les chemins
    // 70 infos et params de launchbox
    // 100 les paramètres du jeu
    //
    // 200 stats
    class Game
    {
        #region info jeu
        
        [XmlElement(Order = 1)]
        public string ID { get; set; }                          //bedf7c05-999f-43fd-a788-9199c08e603e

        [XmlElement(Order = 2)]
        public string Title { get; set; }                       //3 Ninjas Kick Back

        [XmlElement(Order = 3)]
        public string Developer { get; set; }                   //Malibu Interactive

        // Editeur        
        [XmlElement(Order = 4)]
        public string Publisher { get; set; }                   //Sony Imagesoft

        [XmlElement(Order = 5)]
        public string ReleaseDate { get; set; }                 //1994-06-01T09:00:00+02:00

        [XmlElement(Order = 6)]
        public string Region { get; set; }                      //United States

        [XmlElement(Order = 7)]
        public string Platform { get; set; }                    //Sega Mega Drive

        [XmlElement(Order = 8)]
        public string Series { get; set; }                      //

        [XmlElement(Order = 9)]
        public string Genre { get; set; }                       //Action; Platform

        [XmlElement(Order = 10)]
        public string PlayMode { get; set; }                    //Coopération; Multijoueur

        [XmlElement(Order = 11)]
        public string Notes { get; set; }                       //3 Ninjas Kick Back follows 3 young ninja  brothers, Rocky, Colt and Tum-Tum as they assist an old Samurai in retrieving a prized dagger which has been stolen by his rival.The dagger, once given to the Samurai as a reward, will be passed along to younger generations once it is restored to its rightful owner.The boys learn ninjitsu and karate as they fight evil forces that are older, more powerful, and bigger than them.

        #endregion

        #region complément jeu (fini, status...)
        [XmlElement(Order = 30)]
        public string Status { get; set; }                      //

        [XmlElement(Order = 31)]
        public bool Completed { get; set; }                     //false

        [XmlElement(Order = 32)]
        public string DateAdded { get; set; }                   //2018-09-05T07:16:32.4860113+02:00

        [XmlElement(Order = 33)]
        public string DateModified { get; set; }                //2018-09-05T08:37:23.3866486+02:00

        [XmlElement(Order = 34)]
        public bool Favorite { get; set; }                      //false

        [XmlElement(Order = 35)]
        public string Rating { get; set; }                      //T - Teen

        [XmlElement(Order = 36)]
        public string WikipediaURL { get; set; }                //https://en.wikipedia.org/wiki/3_Ninjas_Kick_Back_(video_game)

        [XmlElement(Order = 37)]
        public string Version { get; set; }                     //(1994)(Sony Imagesoft) (US)
        #endregion

        #region chemins
        [XmlElement(Order = 50)]
        public string ApplicationPath { get; set; }             //..\..\Games\Roms\Sega Mega Drive\Pending\3 Ninjas Kick Back(1994)(Sony Imagesoft) (US).zip

        [XmlElement(Order = 51)]
        public string ManualPath { get; set; }                  //..\..\Games\Manuels\Sega Mega Drive\3_Ninjas_Kick_Back_1994_Sony_Imagesoft_US.pdf

        [XmlElement(Order = 52)]
        public string MusicPath { get; set; }                   //

        [XmlElement(Order = 53)]
        public string VideoPath { get; set; }                   //

        [XmlElement(Order = 54)]
        public string DosBoxConfigurationPath { get; set; }     //

        #endregion

        #region Params dans Launchbox
        [XmlElement(Order = 70)]
        public int DatabaseID { get; set; }                     //3371

        [XmlElement(Order = 71)]
        public string SortTitle { get; set; }                   //

        [XmlElement(Order = 72)]
        public bool Hide { get; set; }                          //false

        [XmlElement(Order = 73)]
        public bool Broken { get; set; }                        //false

        [XmlElement(Order = 74)]
        public bool Portable { get; set; }                       //false

        [XmlElement(Order = 75)]
        public bool ScummVMAspectCorrection { get; set; }       //false

        [XmlElement(Order = 76)]
        public bool ScummVMFullscreen { get; set; }             //false

        [XmlElement(Order = 77)]
        public string ScummVMGameDataFolderPath { get; set; }   //

        [XmlElement(Order = 78)]
        public string ScummVMGameType { get; set; }             //  

        [XmlElement(Order = 79)]
        public bool UseScummVM { get; set; }                    //false

        #endregion
               

        #region params de lancement
        [XmlElement(Order = 100)]
        public bool UseDosBox { get; set; }                     //false

        [XmlElement(Order = 101)]
        public string CommandLine { get; set; }

        [XmlElement(Order = 102)]
        public string ConfigurationCommandLine { get; set; }    //

        [XmlElement(Order = 103)]
        public string ConfigurationPath { get; set; }           //

        [XmlElement(Order = 104)]
        public string Emulator { get; set; }                    //46980dd1-982d-4b30-ad3b-fda6139752c9
        #endregion

        #region Stats
        [XmlElement(Order = 200)]
        public int PlayCount { get; set; }                      //2

        [XmlElement(Order = 201)]
        public string LastPlayedDate { get; set; }              //2018-09-05T07:22:32.9731108+02:00

        [XmlElement(Order = 202)]
        public int CommunityStarRating { get; set; }            //0</CommunityStarRating{get; set;}

        [XmlElement(Order = 203)]
        public int CommunityStarRatingTotalVotes { get; set; }  //0</CommunityStarRatingTotalVotes{get; set;}

        [XmlElement(Order = 204)]
        public int StarRatingFloat { get; set; }                //0

        [XmlElement(Order = 205)]
        public int StarRating { get; set; }                     //0
        #endregion

        public string RootFolder { get; set; }                  //

        public string Source { get; set; }                      //
                          
        public bool MissingVideo { get; set; }                  //false
        public bool MissingBoxFrontImage { get; set; }          //false
        public bool MissingScreenshotImage { get; set; }        //false
        public bool MissingClearLogoImage { get; set; }         //false
        public bool MissingBackgroundImage { get; set; }        //false

        public List<CustomField> CustomFields { get; set; }
        public List<AdditionalApplication> AdditionalApplications { get; set; }
    }

}
