using Common_PMG;
using Common_PMG.Container;
using Common_PMG.XML;
using DxTBoxCore.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnPack_My_Game.Graph;
using static UnPack_My_Game.Properties.Settings;

namespace UnPack_My_Game.Cores.LaunchBox
{
    internal class LBFunc
    {
        public static void InjectPlatform()
        {
            string backupFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.BackUp);
            string platformsFile = Path.Combine(Default.LaunchBoxPath, Default.fPlatforms);


            string newPFile = IHMStatic.GetAFile(Default.LastTargetPath, "Select the platform xml file", "xml");


            using (XML_Platforms srcPlatform = new XML_Platforms(newPFile))
            using (XML_Platforms lbPlatformes = new XML_Platforms(platformsFile))
            {
                string machineName = srcPlatform.Root.Element(Tag.Platform).Element(Tag.Name)?.Value;
                var samePlats = from platform in lbPlatformes.Root.Elements(Tag.Platform)
                                where ((string)platform.Element(Tag.Name).Value).Equals(machineName, StringComparison.OrdinalIgnoreCase)
                                select platform;

                bool? write = true;
                // On demande pour REMPLACER la machine si elle existe déjà
                if (samePlats.Count() > 0)
                    write &= IHMStatic.AskDxMBox(
                        "Platform is Already present, replace it ?", "Question", E_DxButtons.Yes | E_DxButtons.No, machineName);


                if (write == false)
                    return;

                // Backup du fichier de la plateforme;
                Tool.BackupFile(platformsFile,  backupFolder);

                // On efface si nécessaire.
                lbPlatformes.RemoveElemByChild( Tag.Platform, Tag.Name, machineName);
                lbPlatformes.RemoveElemByChild(Tag.PlatformFolder, Tag.Platform, machineName);

                lbPlatformes.Save(platformsFile);

                // Injection de la plateforme
                lbPlatformes.InjectPlatform(srcPlatform.Root.Element(Tag.Platform));

                // Injection des dossiers                
                lbPlatformes.InjectPlatFolders(srcPlatform.Root.Elements(Tag.PlatformFolder));

                // Sauvegarde
                lbPlatformes.Save(platformsFile);

            }
        }
    }

}
