using Hermes;
using Common_PMG.Container;
using Common_PMG.Container.AAPP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pack_My_Game.Files
{
    internal class FilesFunc
    {



        /// <summary>
        /// Fonction de copie avec affichage de la progression
        /// </summary>
        /// <param name="fileSrc">File source</param>
        /// <param name="destFile">File to destination</param>
        /// <param name="overWrite">Ecrasement du fichier de destination</param>
        internal static bool Copy(string fileSrc, string destFile, bool overWrite = false)
        {
            HeTrace.Write($"[CopyFiles] Copy of the file '{Path.GetFileName(fileSrc)}': ");


            try
            {
                File.Copy(fileSrc, destFile, overWrite);
                HeTrace.EndLine("Successful");
                return true;
            }
            catch (IOException e)
            {
                HeTrace.EndLine("Error");
                HeTrace.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Recherche de clones
        /// </summary>
        /// <param name="clones"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        internal static List<Clone> DistinctClones(List<Clone> clones, string applicationPath, string launchboxPath)
        {
            string gamePath = Path.GetFullPath(applicationPath, launchboxPath);

            List <Clone> clonestmp = new List<Clone>();

            foreach (Clone c in clones)
            {
                // Ca peut y être par erreur
                if (Path.GetFullPath(c.ApplicationPath, launchboxPath).Equals(gamePath))
                    continue;

                // On cherche si c'est pas déjà présent dans clonestmp
               // var kk = clonestmp.FirstOrDefault(x => c.ApplicationPath.Equals(x.ApplicationPath));
                if (clonestmp.FirstOrDefault(x => c.ApplicationPath.Equals(x.ApplicationPath)) != null)
                    continue;

                clonestmp.Add(c);

            }

            return clonestmp;

        }


        /// <summary>
        /// Choisir un nom pour un fichier.
        /// </summary>
        /// <param name="destFile">Fichier de destination</param>
        /// <param name="defFile">Nom de Fichier proposé</param>
        /// <remarks>Ne permet pas de stopper si on clot la fenêtre</remarks>
        [Obsolete]
        internal static string Choose_AName(string destFile, string defFile = null)
        {
            string destFolder = Path.GetDirectoryName(destFile);
            /*2021
            SaveFileDialog sfd = new SaveFileDialog()
            {
                InitialDirectory = destFolder,
                FileName = Path.GetFileName($"{defFile}{Path.GetExtension(destFile)}")
            };*/

            /*2021
            string newDestFile = null;
            while (true)
            {2021*/
            //todo 10/03/2021 bool? res = sfd.ShowDialog();
            //2021sfd.ShowDialog();

            /* Cas où l'on ferme la fenêtre res = false
             * On ne peut pas ne rien rentrer comme chaine de caractère, ça ne valide pas.
            */
            /*if (sfd.FileName == null)
                continue;*/
            /*2021newDestFile = Path.Combine(destFolder, sfd.FileName);

            if (!File.Exists(newDestFile))
                break;


        }
            */
            return "";//2021 newDestFile;
        }
    }

}