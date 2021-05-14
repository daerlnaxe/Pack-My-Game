using AsyncProgress;
using AsyncProgress.Tools;
using Common_PMG.Container;
using Common_PMG.Container.Game;
using DxLocalTransf;
using DxLocalTransf.Cont;
using DxTBoxCore.Box_Collec;
using DxTBoxCore.Box_Progress;
using DxTBoxCore.Common;
using DxTBoxCore.Box_MBox;
using Pack_My_Game.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using static Common_PMG.Tool;
using DxLocalTransf.Tools;
using Pack_My_Game.Language;

namespace Pack_My_Game.IHM
{
    /// <summary>
    /// S'occupe de gérer les fenêtres lors du processus de package
    /// </summary>
    class PackMe_IHM
    {


        /// <summary>
        /// Show a window to ask what to do when there is a file conflict
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="buttons"></param>
        /// <returns></returns>
        internal static E_Decision? Ask4_FileConflict2(object sender, EFileResult state, FileArgs fileSrc, FileArgs fileDest)
        {
            fileSrc.Length = new FileInfo(fileSrc.Path).Length;
            fileDest.Length = new FileInfo(fileDest.Path).Length;

            return Application.Current.Dispatcher?.Invoke
                (
                    () =>
                    {
                        DxTBoxCore.Box_Decisions.MBDecision boxDeciz = new DxTBoxCore.Box_Decisions.MBDecision()
                        {
                            Model = new DxTBoxCore.Box_Decisions.M_Decision()
                            {
                                Message = LanguageManager.Instance.Lang.File_Ex,
                                Source = fileSrc.Path,
                                Destination = fileDest.Path,

                                SourceInfo = FileSizeFormatter.Convert(fileSrc.Length),
                                DestInfo = FileSizeFormatter.Convert(fileDest.Length),
                            },
                            Buttons = E_DxConfB.OverWrite | E_DxConfB.Pass | E_DxConfB.Trash,
                        };

                        if (boxDeciz.ShowDialog() == true)
                        {
                            return boxDeciz.Model.Decision;
                        }

                        return E_Decision.None;
                    }
                );
        }





        /// <summary>
        /// Show a window to invite user to validate files found
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="location"></param>
        /// <param name="toSearch"></param>
        /// <returns></returns>
        internal static string[] Validate_FilesFound(List<string> files, string mediatype)
        {
            string[] ChosenFiles = null;
            Application.Current.Dispatcher?.Invoke(
                () =>
                {
                    BCElements mbL = new BCElements()
                    {
                        Model = new MCElements(files)
                        {
                            Message = $"Select {mediatype} matching.\rNote: There is an automatic preselection ",
                        }
                    };

                    mbL.ShowDialog();
                    ChosenFiles = mbL.Model.ChosenElements.ToArray();
                });


            return ChosenFiles;
        }

        internal static void LaunchBoxCore_Recap(string rootFolder, ContPlatFolders platform, GameDataCont gdC)
        {
            Application.Current.Dispatcher?.Invoke
                (
                () =>
                {
                    W_PackMeRes W_res = new W_PackMeRes()
                    {
                        Title = $"{LanguageManager.Instance.Lang.Word_Result} - {gdC.Title}",
                        Model = new M_PackMeRes(rootFolder, platform, gdC),

                    };
                    W_res.ShowDialog();
                }
                );
        }

        internal static string AskName(string exploitableFileName, string root)
        {
            string res = string.Empty;

            // Fenêtre pour le choix du nom
            Application.Current.Dispatcher?.Invoke(
                () =>
                {
                    W_ChooseName gnWindows = new W_ChooseName()
                    {
                        Title = "",
                        Model = new M_ChooseName(root)
                        {
                            GameName = exploitableFileName,
                            ArchiveName = exploitableFileName,
                            Message =LanguageManager.Instance.Lang.S_GiveArchName,
                        }
                    };

                    gnWindows.ShowDialog();

                    res = gnWindows.Model.ArchiveName;
                }
                );

            return res;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Objet"></param>
        /// <param name="method"></param>
        /// <param name="title">Titre de la tâche</param>
        /// <returns></returns>
        internal static bool? ZipCompressFolder(I_AsyncSigD Objet, Func<object> method, string title = null)
        {
            //DxDoubleProgress dp = null;
            return Application.Current.Dispatcher?.Invoke
                (
                    () =>
                    {// dp = new DxDoubleProgress(mawmaw)
                        EphemProgressD ephem = new EphemProgressD(Objet);

                        TaskLauncher launcher = new TaskLauncher()
                        {
                            AutoCloseWindow = true,
                            LoopDelay = 50,
                            ProgressIHM = new DxDoubleProgress(ephem),
                            MethodToRun = method

                        };
                        if (launcher.Launch(Objet) == true)
                        {
                            return true;
                        }

                        throw new OperationCanceledException("Interrupted by user");
                    }

                 );

        }

        internal static object LaunchOpProgress(I_AsyncSig Objet, Func<object> method, string title = null)
        {
            return Application.Current.Dispatcher?.Invoke
                 (
                     () =>
                     {
                         EphemProgress ephem = new EphemProgress(Objet);

                         TaskLauncher launcher = new TaskLauncher()
                         {
                             AutoCloseWindow = true,
                             LoopDelay = 50,
                             ProgressIHM = new DxProgressB1(ephem),
                             MethodToRun = method,
                         };

                         if (launcher.Launch(Objet) == true)
                         {
                             return true;
                         }

                         throw new OperationCanceledException("Interrupted by user");
                     }
                 );

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Objet"></param>
        /// <param name="method"></param>
        /// <param name="title">Titre de la tâche</param>
        /// <returns></returns>
        public static bool? DoubleProgress(I_AsyncSigD objet, string title, params Func<object, object>[] methods)
        {
            return Application.Current.Dispatcher?.Invoke
                (
                    () =>
                    {
                        EphemProgressD ephem = new EphemProgressD(objet)
                        {
                             TaskName = title,
                        };

                        TasksLauncher launcher = new TasksLauncher()
                        {
                            AutoCloseWindow = true,
                            ProgressIHM = new DxDoubleProgress(ephem),
                            MethodsToRun = methods,
                        };
                        return launcher.Launch(objet);
                    }
                ); ;

        }


        /*
DxAsProgress dadProgress = new DxAsProgress()
{
TaskName = title,
Model =  compressMethod,
M_Progress.Create(Objet, compressMethod),
};

if (dadProgress.ShowDialog() == true)
{
return true;
}

throw new OperationCanceledException("Interrupted by user");*/
        /*
      return Application.Current.Dispatcher?.Invoke
          (
              () =>
              {
                  MawEvo maw = new MawEvo(Objet);

                  DxAsDoubleProgress dadProgress = new DxAsDoubleProgress()
                  {
                      TaskName = title,
                      HideCloseButton = true,
                      Model = maw,
                      Launcher = BasicLauncher<MawEvo>.Create(maw, compressMethod),
                  };

                  if (dadProgress.ShowDialog() == true)
                  {
                      return true;
                  }

                  return false;
              }
          );*/

    }
}
