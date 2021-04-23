using Common_PMG.BackupLB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
/*
    ShortGame contains ID, Title, Version
    */

namespace Common_PMG.Container.Game.LaunchBox
{
    /// <summary>
    /// Récupère les informations les plus représentatives
    /// </summary>
    public class MedGame : BasicGame, I_MedGame
    {


        #region infos


        /// <summary>
        /// Saga
        /// </summary>
        /// <remarks>
        /// ex: Sonic
        /// </remarks>
        [XmlElement(Order = 9)]
        public string Series { get; set; }

        /// <summary>
        /// Genre
        /// </summary>
        /// <remarks>
        /// ex: Action; Platform
        /// </remarks>
        [XmlElement(Order = 10)]
        public string Genre { get; set; }

        /// <summary>
        /// Mode de jeu en multi
        /// </summary>
        /// <remarks>
        /// ex: Coopération; Multijoueur
        /// </remarks>
        [XmlElement(Order = 11)]
        public string PlayMode { get; set; }

        /// <summary>
        /// Nombre de joueurs maximum
        /// </summary>
        [XmlElement(Order =12), DefaultValue(1)]
        public int? MaxPlayers { get; set; }

        /// <summary>
        /// Développeur
        /// </summary> 
        /// <remarks>
        /// ex: Malibu Interactive
        /// </remarks>
        [XmlElement(Order = 20)]
        public string Developer { get; set; }

        /// <summary>
        /// Editeur
        /// </summary>
        /// <remarks>
        /// ex :Sony Imagesoft
        /// </remarks>
        [XmlElement(Order = 21)]
        public string Publisher { get; set; }

        /// <summary>
        /// Date de sortie
        /// </summary>
        /// <remarks>
        /// ex: 1994-06-01T09:00:00+02:00
        /// </remarks>
        [XmlElement(Order = 23)]
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Evaluation PEGI
        /// </summary>
        /// <remarks>
        /// ex: T - Teen
        /// </remarks>
        [XmlElement(Order = 29)]
        public string Rating { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        /// <remarks>
        /// ex: 3 Ninjas Kick Back follows 3 young ninja  brothers, Rocky, Colt and Tum-Tum as they assist an old Samurai in retrieving a prized dagger which has been stolen by his rival.The dagger, once given to the Samurai as a reward, will be passed along to younger generations once it is restored to its rightful owner.The boys learn ninjitsu and karate as they fight evil forces that are older, more powerful, and bigger than them.
        /// </remarks>
        [XmlElement(Order = 99)]
        public string Notes { get; set; }

        #endregion

        #region files
        //     [XmlIgnore]
        //     public string ApplicationPath { get; set; }

        //     [XmlIgnore]
        //      public string ManualPath { get; set; }

        //     [XmlIgnore]
        //      public string MusicPath { get; set; }

        //   [XmlIgnore]
        //   public string VideoPath { get; set; }
        #endregion


        /*
        public static explicit operator GameInfo(GameLB zGame)
        {
            GameInfo tmpGInfo = new GameInfo()
            {
                Title = zGame.Title,
                Id = zGame.Id,
                Region = zGame.Region,
                Platform = zGame.Platform,
                Series = zGame.Series,
                Genre = zGame.Genre,
                Developer = zGame.Developer,
                Publisher = zGame.Publisher,
                ReleaseDate = zGame.ReleaseDate,
                Rating = zGame.Rating,
                Version = zGame.Version,
                Notes = zGame.Notes,
                /*
                ApplicationPath = zGame.ApplicationPath,
                ManualPath = zGame.ManualPath,
                MusicPath= zGame.MusicPath,
                VideoPath=zGame.VideoPath*/

        /*
            };

            tmpGInfo.FileName = Path.GetFileName(zGame.ApplicationPath);
            // 2020 tmpGInfo.ExploitableFileName = tmpGInfo.FileName.Split('.')[0];
            tmpGInfo.ExploitableFileName = Path.GetFileNameWithoutExtension(tmpGInfo.FileName);


            return tmpGInfo;
        }*/
    }
}
