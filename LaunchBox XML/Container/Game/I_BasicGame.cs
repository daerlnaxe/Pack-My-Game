using System;
using System.Collections.Generic;
using System.Text;

namespace LaunchBox_XML.Container.Game
{
    public interface I_BasicGame
    {
        /// <summary>
        /// ID
        /// </summary>
        /// <remarks>
        /// ex: bedf7c05-999f-43fd-a788-9199c08e603e
        /// </remarks>
        public string Id { get; set; }

        /// <summary>
        /// Titre
        /// </summary>
        /// <remarks>
        /// ex: 3 Ninjas Kick Back
        /// </remarks>
        public string Title { get; set; }

        /// <summary>
        /// Region
        /// </summary>
        /// <remarks>
        /// ex: United States
        /// </remarks>
        public string Region { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        /// <remarks>
        /// ex: (1994)(Sony Imagesoft) (US)
        /// </remarks>
        public string Version { get; set; }
    }
}
