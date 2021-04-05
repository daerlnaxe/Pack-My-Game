using DxTrace;
using LaunchBox_XML.Container;
using Microsoft.Win32;
using Pack_My_Game.Container;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Pack_My_Game.Files
{
    internal class FilesFunc
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool Check4Crash()
        {
            throw new NotImplementedException();
        }

        /*
        public static string[] CollecFiles()
        {

        }*/


        /// <summary>
        /// Fonction de copie avec affichage de la progression
        /// </summary>
        /// <param name="fileSrc">File source</param>
        /// <param name="destFile">File to destination</param>
        /// <param name="overWrite">Ecrasement du fichier de destination</param>
        internal static bool Copy(string fileSrc, string destFile, bool overWrite = false)
        {
            ITrace.BeginLine($"[CopyFiles] Copy of the file '{Path.GetFileName(fileSrc)}': ");


            try
            {
                File.Copy(fileSrc, destFile, overWrite);
                ITrace.EndlLine("Successful");
                return true;
            }
            catch (IOException e)
            {
                ITrace.EndlLine("Error");
                ITrace.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Recherche de clones
        /// </summary>
        /// <param name="clones"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        internal static List<Clone> DistinctClones(List<Clone> clones, string fileName)
        {
            List<Clone> clonestmp = new List<Clone>();

            foreach (Clone c in clones)
            {

                /*
                if (Path.GetFileName(c.ApplicationPath).Equals(fileName))
                    continue;*/

                // On cherche si c'est pas déjà présent dans clonestmp
                var kk = clonestmp.FirstOrDefault(x => c.ApplicationPath.Equals(x.ApplicationPath));
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
        internal static string Choose_AName(string destFile, string defFile = null)
        {
            string destFolder = Path.GetDirectoryName(destFile);

            SaveFileDialog sfd = new SaveFileDialog()
            {
                InitialDirectory = destFolder,
                FileName = Path.GetFileName($"{defFile}{Path.GetExtension(destFile)}")
            };

            string newDestFile = null;
            while (true)
            {
                //todo 10/03/2021 bool? res = sfd.ShowDialog();
                sfd.ShowDialog();

                /* Cas où l'on ferme la fenêtre res = false
                 * On ne peut pas ne rien rentrer comme chaine de caractère, ça ne valide pas.
                */
                /*if (sfd.FileName == null)
                    continue;*/
                newDestFile = Path.Combine(destFolder, sfd.FileName);

                if (!File.Exists(newDestFile))
                    break;


            }

            return newDestFile;
        }
    }

}