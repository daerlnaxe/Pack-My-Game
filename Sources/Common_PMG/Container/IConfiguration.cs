﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common_PMG.Container
{
    public interface IConfiguration
    {
        /// <summary>
        /// Version du fichier de configuration
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Version du fichier de configuration
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Path for LaunchBox
        /// </summary>
        public string HLaunchBoxPath { get; set; }
        /// <summary>
        /// Relative path for LaunchBox
        /// </summary>
        public string RLaunchBoxPath { get; set; }

        /// <summary>
        /// Dossier temporaire de travail
        /// </summary>
        public string HWorkingFolder { get; set; }

        /// <summary>
        /// Dossier temporaire de travail (relatif)
        /// </summary>
        public string RWorkingFolder { get; set; }

        /// <summary>
        /// Dossier des cheats codes
        /// </summary>
        public string HCCodesPath { get; set; }

        /// <summary>
        /// Dossier des cheats codes (relatif)
        /// </summary>
        public string RCCodesPath { get; set; }

        /// <summary>
        /// Dernier dossier utilisé
        /// </summary>
        public string LastPath { get; set; }

        /// <summary>
        /// Tail to find "platforms.xml"
        /// </summary>
        public string PlatformsFile { get; set; }

        /// <summary>
        /// Tail to find 'Platforms' folder
        /// </summary>
        public string PlatformsFolder { get; set; }
    }
}
