using Hermes;
using Hermes.Cont;
using Hermes.Messengers;
using System;
using System.Windows;
using System.Windows.Controls;

namespace UnPack_My_Game.Graph
{
    /// <summary>
    /// Logique d'interaction pour W_Common.xaml
    /// </summary>
    public partial class W_Common : Window
    {

        /// <summary>
        /// Title Showed on Main Window
        /// </summary>
        public string Title
        {
            get
            {
                System.Reflection.Assembly asmbly = System.Reflection.Assembly.GetExecutingAssembly();
                return $"{asmbly.GetName().Name} - {asmbly.GetName().Version}";
            }
        }

        /// <summary>
        /// Page to Show
        /// </summary>
        public Page ActivePage { get; set; }

        public W_Common()
        {
#if DEBUG
            MeDebug md = new MeDebug
            {
                ByPass = true,

            };
            HeTrace.AddMessenger("Debug", md);

#endif

            // Tracing
            MeSimpleLog log = new MeSimpleLog(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Common.Logs, $"{DateTime.Now.ToFileTime()}.log"))
            {
                LogLevel = 1,
                FuncPrefix = EPrefix.Horodating,
                ByPass = true,
            };

            log.AddCaller(this);
            HeTrace.AddLogger("C_LBDPG", log);


            InitializeComponent();
            DataContext = this;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            HeTrace.RemoveMessenger("Debug");
        }
    }
}
