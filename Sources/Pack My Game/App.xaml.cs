using Hermes;
using Hermes.Cont;
using Hermes.Messengers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PS = Pack_My_Game.Properties.Settings;

namespace Pack_My_Game
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //if (!Directory.Exists(Common.LangFolder))
            Directory.CreateDirectory(Common.LangFolder);
            Directory.CreateDirectory(Common.Logs);

            // Tracing
            MeSimpleLog log = new MeSimpleLog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.Logs, $"{DateTime.Now.ToFileTime()}.log"))
            {
                LogLevel = 1,
                FuncPrefix = EPrefix.Horodating,
                ByPass = true,
            };
            log.AddCaller(this);
            HeTrace.AddLogger("PackMyGame", log);

            // Mise à jour des paramètres en cas de changement de version
            if (PS.Default.UpgradeRequired)
            {
                PS.Default.Upgrade();
                PS.Default.UpgradeRequired = false;
                PS.Default.Save();
            }

            string enLangFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.LangFolder, "Lang.en-EN.xml");
            // Création of languages files if needed
            if (!File.Exists(enLangFile))
                XML.XMLLang.Make_ENVersion();
                    
            if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.LangFolder, "Lang.fr-FR.xml")))
                XML.XMLLang.Make_FRVersion();


            // Change language according to user preference (first time: null)
            string langFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.LangFolder, $"Lang.{PS.Default.Language}.xml");
            if (!string.IsNullOrEmpty(PS.Default.Language) && File.Exists(langFile))
            {
                Common.ObjectLang = XML.XMLLang.Load(langFile);
            }

            // In case of file doesn't exist OR older version file
            if( !File.Exists(langFile) || !Common.ObjectLang.Version.Equals(Common.LangVersion))
            {
                XML.XMLLang.Make_ENVersion();
                Common.ObjectLang = XML.XMLLang.Load(enLangFile);
            }

        }

    }
}
