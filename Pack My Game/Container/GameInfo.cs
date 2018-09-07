using Pack_My_Game.BackupLB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
/*
    ShortGame contains ID, Title, Version
    */

namespace Pack_My_Game.Container
{
    public class GameInfo : ShortGame
    {
        #region infos
        [XmlElement(Order = 8)]
        public string Platform { get; set; }

        [XmlElement(Order = 9)]
        public string Series { get; set; }

        [XmlElement(Order = 10)]
        public string Genre { get; set; }

        [XmlElement(Order = 20)]
        public string Developer { get; set; }

        [XmlElement(Order = 21)]
        public string Publisher { get; set; }

        [XmlElement(Order = 23)]
        public string ReleaseDate { get; set; }

        [XmlElement(Order = 30)]
        public string Rating { get; set; }

        [XmlElement(Order = 31)]
        public string Version { get; set; }

        [XmlElement(Order = 100)]
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

        public static explicit operator GameInfo(Game zGame)
        {
            GameInfo tmpGInfo = new GameInfo()
            {
                Title = zGame.Title,
                ID = zGame.ID,
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
            };

            tmpGInfo.FileName = Path.GetFileName(zGame.ApplicationPath);
            tmpGInfo.ExploitableFileName = tmpGInfo.FileName.Split('.')[0];

            return tmpGInfo;
        }
    }
}
