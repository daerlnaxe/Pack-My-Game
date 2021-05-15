using Hermes;
using Hermes.Cont;
using Hermes.Messengers;
using Pack_My_Game.Cont;
using Pack_My_Game.Language;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using static Pack_My_Game.Common;

namespace Pack_My_Game
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            try
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

                /*
                // Mise à jour des paramètres en cas de changement de version
                if (PS.Default.UpgradeRequired)
                {
                    PS.Default.Upgrade();
                    PS.Default.U-pgradeRequired = false;
                    PS.Default.Save();
                }*/

                // --- Configuration
                string configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.json");

                Config = null;
                if (!File.Exists(configFile))
                {
                    Config = Cont.Configuration.MakeDefault();
                    Config.Save();
                }
                else
                {
                    Config = Cont.Configuration.ReadConfig();
                }

                // Réécriture si le numéro de version diffère
                if (Config.Version == null || !Common.Config.Version.Equals(Common.ConfigVersion))
                {
                    Config.Update();
                    Config.Save();
                }

                // --- Languages
                LanguageManager.Instance.Init(Config.Language);
               

            }
            catch (Exception exc)
            {
                DxTBoxCore.Box_MBox.DxMBox.ShowDial($"{exc.Message}", "Error", optMessage: exc.StackTrace);
                HeTrace.WriteLine(exc.Message);
                HeTrace.WriteLine(exc.StackTrace);
            }
        }
    }
}
