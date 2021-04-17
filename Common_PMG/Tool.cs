using System;
using System.Text.RegularExpressions;

namespace LaunchBox_XML
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

    }
}
