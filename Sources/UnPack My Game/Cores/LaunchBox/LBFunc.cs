using Common_PMG;
using Common_PMG.Container;
using Common_PMG.XML;
using DxTBoxCore.Common;
using Hermes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnPack_My_Game.Graph;
using static UnPack_My_Game.Common;

namespace UnPack_My_Game.Cores.LaunchBox
{
    internal class LBFunc
    {
        public static void InjectPlatform()
        {
            HeTrace.WriteLine("Platform injection...");
            string backupFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.BackUp);
            string platformsFile = Path.Combine(Config.LaunchBoxPath, Config.PlatformsFile);


            string newPFile = IHMStatic.GetAFile(Config.LastTargetPath, "Select the platform xml file", "xml");

            if (string.IsNullOrEmpty(newPFile))
            {
                HeTrace.WriteLine("Platform file is null !");
                return;
            }

            HeTrace.WriteLine($"Platform selected is '{newPFile}'");

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
                Tool.BackupFile(platformsFile, backupFolder);

                // On efface si nécessaire.
                lbPlatformes.RemoveElemByChild(Tag.Platform, Tag.Name, machineName);
                lbPlatformes.RemoveElemByChild(Tag.PlatformFolder, Tag.Platform, machineName);

                lbPlatformes.Save(platformsFile);

                // Injection de la plateforme
                lbPlatformes.InjectPlatform(srcPlatform.Root.Element(Tag.Platform));

                // Injection des dossiers                
                lbPlatformes.InjectPlatFolders(srcPlatform.Root.Elements(Tag.PlatformFolder));

                // Sauvegarde
                lbPlatformes.Save(platformsFile);

            }

            HeTrace.WriteLine("Platform injection, done");
        }
    }

}
