using System;
using System.Collections.Generic;
using System.Text;

namespace Common_PMG.Container.AAPP
{
    public interface I_Clone
    {
        /// <summary>
        /// ID du clone
        /// </summary>
        /// <remarks>
        /// ex: 22dfd169-f99d-4929-acc1-039e6df37129
        /// </remarks>
        public string Id { get; set; }

        /// <summary>
        /// ID du parent ?
        /// </summary>
        /// <remarks>
        /// ex :d99692c7-51c3-4e50-8592-7fab98295f5b
        /// </remarks>
        public string GameId { get; set; }

        /// <summary>
        /// Chemin vers l'application
        /// </summary>
        /// <remarks>
        /// ex: ..\..\Games\Roms\Sega Mega Drive\Pending\3 Ninjas Kick Back(1994)(Sony Imagesoft) (US).zip
        /// </remarks>
        public string ApplicationPath { get; set; }
    }
}
