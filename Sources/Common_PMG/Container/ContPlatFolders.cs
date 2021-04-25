using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common_PMG.Container
{
    public class ContPlatFolders
    {
        /// <summary>
        /// Nom du système
        /// </summary>
        public string Name { get; set;  }
        /// <summary>
        /// Chemin d'accès des roms du système
        /// </summary>
        public string FolderPath { get; set; }
     /*

        public string ManualPath { get; set; }

        public string MusicPath { get; set; }

        public string VideoPath { get; set; }

        public string ThemeVideoPath { get; set; }
     */

        /// <summary>
        /// Liste des dossiers de la plateforme, à part le dossier des roms
        /// </summary>
        public List<PlatformFolder> PlatformFolders { get; set; } = new List<PlatformFolder>();


    }
}
