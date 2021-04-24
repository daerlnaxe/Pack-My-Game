using DxTBoxCore.Common;
using DxTBoxCore.Box_MBox;
using System;
using System.Windows;
using System.Collections.Generic;
using DxTBoxCore.Box_Status;
using System.Windows.Media;
using AsyncProgress;
using AsyncProgress.Tools;
using DxTBoxCore.Box_Progress;

namespace Common_Graph
{
    public class SafeBoxes
    {
        /// <summary>
        /// Show a message window, called by delegate (useful with async)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public static bool? Dispatch_Mbox(object sender, string message, string title, E_DxButtons buttons = E_DxButtons.Ok, string optMessage = null)
        {
            bool? res = false;
            Application.Current.Dispatcher?.Invoke(() => res = DxMBox.ShowDial(message, title, buttons, optMessage));
            return res;
        }

        public static bool? ShowStatus(string message, string title, Dictionary<string, bool?> states, string trueC, string falseC, string nullC = "#FFFFFFFF", E_DxButtons buttons = E_DxButtons.Yes | E_DxButtons.No)
        {
            return Application.Current.Dispatcher?.Invoke
                (
                    () =>
                    {
                        return ColumnStatus.ShowDial(message, title, states, trueC, falseC, nullC, buttons);
                    }
                );
        }

        public static string SelectFolder(string root, string message)
        {
            return Application.Current.Dispatcher?.Invoke(
                () =>
                {

                    System.Windows.Forms.FolderBrowserDialog ofd = new System.Windows.Forms.FolderBrowserDialog()
                    {
                        Description = message,
                        ShowNewFolderButton = false,
                        SelectedPath = root
                    };
                    ofd.ShowDialog();
                    return ofd.SelectedPath;
                }
                );
        }

        public static bool? LaunchDouble(I_AsyncSigD objet, Func<object> method, string title)
        {
            return Application.Current.Dispatcher?.Invoke
                (
                    () =>
                    {
                        EphemProgressD mawmaw = new EphemProgressD(objet);
                        TaskLauncher launcher = new TaskLauncher()
                        {
                            AutoCloseWindow = true,
                            ProgressIHM = new DxDoubleProgress(mawmaw),
                            MethodToRun = method,
                        };
                        return launcher.Launch(objet);
                    }
                ); ;

        }

    }
}
