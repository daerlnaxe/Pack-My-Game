using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Unbroken.LaunchBox.Plugins.Data;

namespace LaunchBox_XML.Container.Game
{
    /// <summary>
    /// Classe de base des jeux (reprise par Gameshort)
    /// </summary>
    public class BasicGame
    {
        /// <summary>
        /// ID
        /// </summary>
        /// <remarks>
        /// ex: bedf7c05-999f-43fd-a788-9199c08e603e
        /// </remarks>
        [XmlElement(Order = 1)]
        public string Id { get; set; }

        /// <summary>
        /// Titre
        /// </summary>
        /// <remarks>
        /// ex: 3 Ninjas Kick Back
        /// </remarks>
        [XmlElement(Order = 2)]
        public string Title { get; set; }

        /// <summary>
        /// Region
        /// </summary>
        /// <remarks>
        /// ex: United States
        /// </remarks>
        [XmlElement(Order = 3)]
        public string Region { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        /// <remarks>
        /// ex: (1994)(Sony Imagesoft) (US)
        /// </remarks>
        [XmlElement(Order = 4)]
        public string Version { get; set; }





    }
}
