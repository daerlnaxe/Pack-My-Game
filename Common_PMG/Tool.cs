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


        /*
    yotta	Y	10^24
    zetta	Z	10^21
    exa	    E	10^18
    péta	P	10^15
    téra	T	10^12
    giga	G	10^9
    méga	M	10^6
    kilo	k	10^3
 */
        public static class FileSizeFormatter
        {
            // Sufixes
            static readonly string[] suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB" };

            public static string Convert(Int64 bytes)
            {
                int counter = 0;
                decimal number = (decimal)bytes;
                while (Math.Round(number / 1024) >= 1)
                {
                    number = number / 1024;
                    counter++;
                }
                return string.Format("{0:n1}{1}", number, suffixes[counter]);
            }
        }

    }
}
