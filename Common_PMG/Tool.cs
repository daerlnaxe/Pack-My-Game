using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Common_PMG
{
    public class Tool
    {


        /*
            '<>':   replaced
            ':' :   get first part only
            '"' :   replaced
            '/' :   replaced
            '\' :   replaced
            '|' :   replaced
            '?' :   removed
            '*' :   replaced
        */
        public static string WindowsConv_TitleToFileName(string title)
        {
            string tmp = title.Split(":")[0];                      

            tmp = new Regex(@"[<>""/\\|*]").Replace(tmp, " ");
            tmp = new Regex(@"[?]").Replace(tmp, "");

            return tmp;
        }

        /// <summary>
        /// Backup xml file
        /// </summary>
        /// <param name="file"></param>
        public static void BackupFile(string file, string destFolder)
        {
            string targetfP = Path.Combine(destFolder,
                                $"{Path.GetFileNameWithoutExtension(file)} - ");

            string extension = Path.GetExtension(file);

            for (int i = 00; i < 100; i++)
            {
                string tempo = targetfP + i.ToString("00") + extension;


                if (!File.Exists(tempo))
                {
                    targetfP = tempo;
                    break;
                }

                if (i == 99)
                    targetfP += "00.xml";
            }

            //File.Copy(machine,);
            File.Copy(file, targetfP, true);
        }

    }
}
