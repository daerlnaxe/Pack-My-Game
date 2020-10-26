using DxTrace;
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
        internal static string Choose_AName(string destFile)
        {
            string destFolder = Path.GetDirectoryName(destFile);

            SaveFileDialog sfd = new SaveFileDialog()
            {
                InitialDirectory = destFolder
            };

            while (true)
            {
                sfd.ShowDialog();

                if (!File.Exists(sfd.FileName))
                    break;
            }

            return sfd.FileName;
        }
    }

}