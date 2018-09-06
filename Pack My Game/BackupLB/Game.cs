using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pack_My_Game.BackupLB
{

    ////     <Game{get; set;}
    ////    <ApplicationPath{get; set;}..\..\Games\Roms\Sega Mega Drive\Pending\3 Ninjas Kick Back(1994)(Sony Imagesoft) (US).zip</ApplicationPath{get; set;}
    ////    <CommandLine {get; set;}
    ////    <Completed{get; set;}false</Completed{get; set;}
    ////    <ConfigurationCommandLine {get; set;}
    ////    <ConfigurationPath {get; set;}
    ////    <DateAdded{get; set;}2018-09-05T07:16:32.4860113+02:00</DateAdded{get; set;}
    ////    <DateModified{get; set;}2018-09-05T08:37:23.3866486+02:00</DateModified{get; set;}
    ////    <Developer{get; set;}Malibu Interactive</Developer{get; set;}
    ////    <DosBoxConfigurationPath {get; set;}
    ////    <Emulator{get; set;}46980dd1-982d-4b30-ad3b-fda6139752c9</Emulator{get; set;}
    ////    <Favorite{get; set;}false</Favorite{get; set;}
    ////    <ID{get; set;}bedf7c05-999f-43fd-a788-9199c08e603e</ID{get; set;}
    ////    <LastPlayedDate{get; set;}2018-09-05T07:22:32.9731108+02:00</LastPlayedDate{get; set;}
    ////    <ManualPath{get; set;}..\..\Games\Manuels\Sega Mega Drive\3_Ninjas_Kick_Back_1994_Sony_Imagesoft_US.pdf</ManualPath{get; set;}
    ////    <MusicPath {get; set;}
    ////    <Notes{get; set;}3 Ninjas Kick Back follows 3 young ninja  brothers, Rocky, Colt and Tum-Tum as they assist an old Samurai in retrieving a prized dagger which has been stolen by his rival.The dagger, once given to the Samurai as a reward, will be passed along to younger generations once it is restored to its rightful owner.The boys learn ninjitsu and karate as they fight evil forces that are older, more powerful, and bigger than them.</Notes{get; set;}
    ////    <Platform{get; set;}Sega Mega Drive</Platform{get; set;}
    ////    <Publisher{get; set;}Sony Imagesoft</Publisher{get; set;}
    ////    <Rating{get; set;}T - Teen</Rating{get; set;}
    ////    <ReleaseDate{get; set;}1994-06-01T09:00:00+02:00</ReleaseDate{get; set;}
    ////    <RootFolder {get; set;}
    ////    <ScummVMAspectCorrection{get; set;}false</ScummVMAspectCorrection{get; set;}
    ////    <ScummVMFullscreen{get; set;}false</ScummVMFullscreen{get; set;}
    ////    <ScummVMGameDataFolderPath {get; set;}
    ////    <ScummVMGameType {get; set;}
    ////    <SortTitle {get; set;}
    ////    <Source {get; set;}
    ////    <StarRatingFloat{get; set;}0</StarRatingFloat{get; set;}
    ////    <StarRating{get; set;}0</StarRating{get; set;}
    ////    <CommunityStarRating{get; set;}0</CommunityStarRating{get; set;}
    ////    <CommunityStarRatingTotalVotes{get; set;}0</CommunityStarRatingTotalVotes{get; set;}
    ////    <Status {get; set;}
    ////    <DatabaseID{get; set;}3371</DatabaseID{get; set;}
    ////    <WikipediaURL{get; set;}https://en.wikipedia.org/wiki/3_Ninjas_Kick_Back_(video_game)</WikipediaURL{get; set;}
    ////    <Title{get; set;}3 Ninjas Kick Back</Title{get; set;}
    ////    <UseDosBox{get; set;}false</UseDosBox{get; set;}
    ////    <UseScummVM{get; set;}false</UseScummVM{get; set;}
    ////    <Version{get; set;}(1994)(Sony Imagesoft) (US)</Version{get; set;}
    ////    <Series {get; set;}
    ////    <PlayMode{get; set;}Coopération; Multijoueur</PlayMode{get; set;}
    ////    <Region{get; set;}United States</Region{get; set;}
    ////    <PlayCount{get; set;}2</PlayCount{get; set;}
    ////    <Portable{get; set;}false</Portable{get; set;}
    ////    <VideoPath {get; set;}
    ////    <Hide{get; set;}false</Hide{get; set;}
    ////    <Broken{get; set;}false</Broken{get; set;}
    ////    <Genre{get; set;}Action; Platform</Genre{get; set;}
    ////    <MissingVideo{get; set;}false</MissingVideo{get; set;}
    ////    <MissingBoxFrontImage{get; set;}false</MissingBoxFrontImage{get; set;}
    ////    <MissingScreenshotImage{get; set;}false</MissingScreenshotImage{get; set;}
    ////    <MissingClearLogoImage{get; set;}false</MissingClearLogoImage{get; set;}
    ////    <MissingBackgroundImage{get; set;}false</MissingBackgroundImage{get; set;}
    ////  </Game{get; set;}
    ///
    class Game:Common
    {
        public string CommandLine { get; set; }
        public bool Completed { get; set; }                     //false
        public string ConfigurationCommandLine { get; set; }    //
        public string ConfigurationPath { get; set; }           //
        public string DateAdded { get; set; }                   //2018-09-05T07:16:32.4860113+02:00
        public string DateModified { get; set; }                //2018-09-05T08:37:23.3866486+02:00
        public string Developer { get; set; }                   //Malibu Interactive
        public string DosBoxConfigurationPath { get; set; }     //
        public string Emulator { get; set; }                    //46980dd1-982d-4b30-ad3b-fda6139752c9
        public bool Favorite { get; set; }                      //false
        public string ID { get; set; }                          //bedf7c05-999f-43fd-a788-9199c08e603e
        public string LastPlayedDate { get; set; }              //2018-09-05T07:22:32.9731108+02:00
        public string ManualPath { get; set; }                  //..\..\Games\Manuels\Sega Mega Drive\3_Ninjas_Kick_Back_1994_Sony_Imagesoft_US.pdf
        public string MusicPath { get; set; }                   //
        //Synopsis par ex
        public string Notes { get; set; }                       //3 Ninjas Kick Back follows 3 young ninja  brothers, Rocky, Colt and Tum-Tum as they assist an old Samurai in retrieving a prized dagger which has been stolen by his rival.The dagger, once given to the Samurai as a reward, will be passed along to younger generations once it is restored to its rightful owner.The boys learn ninjitsu and karate as they fight evil forces that are older, more powerful, and bigger than them.
        public string Platform { get; set; }                    //Sega Mega Drive
        // Editeur
        public string Publisher { get; set; }                   //Sony Imagesoft
        public string Rating { get; set; }                      //T - Teen
        public string ReleaseDate { get; set; }                 //1994-06-01T09:00:00+02:00
        public string RootFolder { get; set; }                  //
        public bool ScummVMAspectCorrection { get; set; }       //false
        public bool ScummVMFullscreen { get; set; }             //false
        public string ScummVMGameDataFolderPath { get; set; }   //
        public string ScummVMGameType { get; set; }             //  
        public string SortTitle { get; set; }                   //
        public string Source { get; set; }                      //
        public int StarRatingFloat { get; set; }                //0
        public int StarRating { get; set; }                     //0
        public int CommunityStarRating { get; set; }            //0</CommunityStarRating{get; set;}
        public int CommunityStarRatingTotalVotes { get; set; }  //0</CommunityStarRatingTotalVotes{get; set;}
        public string Status { get; set; }                      //
        public int DatabaseID { get; set; }                     //3371
        public string WikipediaURL { get; set; }                //https://en.wikipedia.org/wiki/3_Ninjas_Kick_Back_(video_game)
        public string Title { get; set; }                       //3 Ninjas Kick Back
        public bool UseDosBox { get; set; }                     //false
        public bool UseScummVM { get; set; }                    //false
        public string Version { get; set; }                     //(1994)(Sony Imagesoft) (US)
        public string Series { get; set; }                      //
        public string PlayMode { get; set; }                    //Coopération; Multijoueur
        public string Region { get; set; }                      //United States
        public int PlayCount { get; set; }                      //2
        public bool Portable { get; set; }                       //false
        public string VideoPath { get; set; }                   //
        public bool Hide { get; set; }                          //false
        public bool Broken { get; set; }                        //false
        public string Genre { get; set; }                       //Action; Platform
        public bool MissingVideo { get; set; }                  //false
        public bool MissingBoxFrontImage { get; set; }          //false
        public bool MissingScreenshotImage { get; set; }        //false
        public bool MissingClearLogoImage { get; set; }         //false
        public bool MissingBackgroundImage { get; set; }        //false

        List<Tuple<string, string>> CustomFields { get; set; } = new List<Tuple<string, string>>();
        List<AdditionalApplication> AdditionalApplications { get; set; } = new List<AdditionalApplication>();
    }

}
