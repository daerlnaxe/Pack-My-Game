using Common_PMG.Container.Game.LaunchBox;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Common_PMG.Container.Game
{
    public class GameInfo : MedGame
    {

        [XmlAttribute("GIVersion")]
        public string GameInfoVersion { get; set; } = "1.0.1.0";

        public static implicit operator GameInfo(LBGame zGame)
        {
            GameInfo tmpGInfo = new GameInfo()
            {
                Title = zGame.Title,
                Id = zGame.Id,
                Region = zGame.Region,
                Version = zGame.Version,
                //
                Platform = zGame.Platform,
                Series = zGame.Series,
                Genre = zGame.Genre,
                // 2021
                PlayMode = zGame.PlayMode,
                MaxPlayers =zGame.MaxPlayers,
                // 2021
                Developer = zGame.Developer,
                Publisher = zGame.Publisher,
                ReleaseDate = zGame.ReleaseDate,
                Rating = zGame.Rating,
                Notes = zGame.Notes,
                /*
                ApplicationPath = zGame.ApplicationPath,
                ManualPath = zGame.ManualPath,
                MusicPath= zGame.MusicPath,
                VideoPath=zGame.VideoPath*/


            };

            /*tmpGInfo.f = Path.GetFileName(zGame.ApplicationPath);
            // 2020 tmpGInfo.ExploitableFileName = tmpGInfo.FileName.Split('.')[0];
            tmpGInfo.ExploitableFileName = Path.GetFileNameWithoutExtension(tmpGInfo.FileName);*/


            return tmpGInfo;
        }
    }
}
