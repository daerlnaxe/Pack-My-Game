using Common_PMG.Container.Game.LaunchBox;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common_PMG.Container.Game
{
    public interface I_MedGame: I_BasicGame
    {

        #region infos


        /// <summary>
        /// Saga
        /// </summary>
        /// <remarks>
        /// ex: Sonic
        /// </remarks>
        public string Series { get; set; }

        /// <summary>
        /// Genre
        /// </summary>
        /// <remarks>
        /// ex: Action; Platform
        /// </remarks>
        public string Genre { get; set; }

        /// <summary>
        /// Mode de jeu en multi
        /// </summary>
        /// <remarks>
        /// ex: Coopération; Multijoueur
        /// </remarks>
        public string PlayMode { get; set; }

        /// <summary>
        /// Nombre de joueurs maximum
        /// </summary>
        public int? MaxPlayers { get; set; }

        /// <summary>
        /// Développeur
        /// </summary> 
        /// <remarks>
        /// ex: Malibu Interactive
        /// </remarks>
        public string Developer { get; set; }

        /// <summary>
        /// Editeur
        /// </summary>
        /// <remarks>
        /// ex :Sony Imagesoft
        /// </remarks>
        public string Publisher { get; set; }

        /// <summary>
        /// Date de sortie
        /// </summary>
        /// <remarks>
        /// ex: 1994-06-01T09:00:00+02:00
        /// </remarks>
        public DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// Evaluation PEGI
        /// </summary>
        /// <remarks>
        /// ex: T - Teen
        /// </remarks>
        public string Rating { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        /// <remarks>
        /// ex: 3 Ninjas Kick Back follows 3 young ninja  brothers, Rocky, Colt and Tum-Tum as they assist an old Samurai in retrieving a prized dagger which has been stolen by his rival.The dagger, once given to the Samurai as a reward, will be passed along to younger generations once it is restored to its rightful owner.The boys learn ninjitsu and karate as they fight evil forces that are older, more powerful, and bigger than them.
        /// </remarks>
        public string Notes { get; set; }

        #endregion
    }
}
