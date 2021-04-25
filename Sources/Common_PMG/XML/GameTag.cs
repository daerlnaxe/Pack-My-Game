using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Common_PMG.XML
{
    public static class GameTag
    {
        public static readonly string ID = "ID";
        public static readonly string Title = "Title";
        public static readonly string Platform = "Platform";
        public static readonly string ManPath = "ManualPath";
        public static readonly string MusPath = "MusicPath";
        public static readonly string VidPath = "VideoPath";
        public static readonly string ThVidPath = "ThemeVideoPath";
        public static readonly string RootFolder = "RootFolder";
        public static readonly string ConfPath = "ConfigurationPath";
        public static readonly string DosBoxPath = "DosBoxConfigurationPath";
        internal static readonly string ScummVMGDPath = "ScummVMGameDataFolderPath";
    }
}
