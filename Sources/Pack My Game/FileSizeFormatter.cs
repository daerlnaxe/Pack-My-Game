using System;
using System.Collections.Generic;
using System.Text;

namespace Pack_My_Game
{
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
