using AsyncProgress;
using DxTBoxCore.Box_Progress;
using System;
using System.Windows;

namespace UnPack_My_Game.Graph
{
    static class IHMStatic
    {
        public static bool? LaunchDouble(I_AsyncSigD objet, Func<object> method, string title)
        {
            return Application.Current.Dispatcher?.Invoke
                (
                    () =>
                    {
                        MawEvo mawmaw = new MawEvo(objet);
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

        internal static void ShowDPG(GamePathsExt gpX, string gamePath)
        {
            Application.Current.Dispatcher?.Invoke
                (
                    ()=>
                    {
                        W_DPGMaker winDPG = new W_DPGMaker()
                        {
                            Model = new M_DPGMaker(gpX, gamePath),
                        };
                        if (winDPG.ShowDialog() == true)
                        {
                            // ...
                        }
                    }
                );
        }
    }
}
