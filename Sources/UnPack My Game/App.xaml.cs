using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Hermes;
using Hermes.Cont;
using Hermes.Messengers;
using UnPack_My_Game.Graph;
using UnPack_My_Game.Graph.LaunchBox;
using UnPack_My_Game.Language;
using static UnPack_My_Game.Common;

namespace UnPack_My_Game
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
                if (Config.Version == null || !Config.Version.Equals(Common.ConfigVersion))
                {
                    //Config = Cont.Configuration.MakeDefault();
                    Config.Update();
                    Config.Save();
                }


                // --- Languages
                LanguageManager.Init();
                LanguageManager.Instance.ChangeLanguage(Config.Language);
            }
            catch(Exception exc)
            {
                DxTBoxCore.Box_MBox.DxMBox.ShowDial($"{exc.Message}", "Error", optMessage: exc.StackTrace);
            }
        }
    }
}
