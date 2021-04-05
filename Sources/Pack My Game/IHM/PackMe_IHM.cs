﻿using DxLocalTransf;
using DxLocalTransf.Progress.ToImp;
using DxTBoxCore.Box_Collec;
using DxTBoxCore.Box_Progress;
using DxTBoxCore.Common;
using DxTBoxCore.MBox;
using LaunchBox_XML.Container;
using Pack_My_Game.Compression;
using Pack_My_Game.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace Pack_My_Game.IHM
{
    /// <summary>
    /// S'occupe de gérer les fenêtres lors du processus de package
    /// </summary>
    class PackMe_IHM
    {
        /// <summary>
        /// Show a message window, called by delegate (useful with async)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        internal static bool? Dispatch_Mbox(object sender, string message, string title, E_DxButtons buttons , string optMessage = null)
        {
            bool? res = false;
            Application.Current.Dispatcher?.Invoke(() => res = DxMBox.ShowDial(message, title, buttons, optMessage));
            return res;
        }



        /// <summary>
        /// Show a window to ask what to do when there is a file conflict
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="buttons"></param>
        /// <returns></returns>
        internal static E_Decision Ask4_FileConflict(string source, string destination, DxTBoxCore.Common.E_DxConfB buttons = DxTBoxCore.Common.E_DxConfB.All)
        {
            FileInfo srcFi = new FileInfo(source);
            FileInfo dstFi = new FileInfo(destination);

            E_Decision decision = E_Decision.None;

            Application.Current.Dispatcher?.Invoke(
                () =>
                {
                    DxTBoxCore.Box_Decisions.MBDecision boxDeciz = new DxTBoxCore.Box_Decisions.MBDecision()
                    {
                        Model = new DxTBoxCore.Box_Decisions.M_Decision()
                        {
                            Message = Common.ObjectLang.FileEExp,
                            Source = source,
                            Destination = destination,

                            SourceInfo = FileSizeFormatter.Convert(dstFi.Length),
                            DestInfo = FileSizeFormatter.Convert(dstFi.Length),
                        },
                        Buttons = buttons,
                    };

                    if (boxDeciz.ShowDialog() == true)
                    {
                        decision = boxDeciz.Model.Decision;
                    }


                }
                );
            return decision;
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

        internal static void LaunchBoxCore_Recap(string rootFolder, Platform platform, string gameName)
        {
            Application.Current.Dispatcher?.Invoke
                (
                () =>
                {
                    W_PackMeRes W_res = new W_PackMeRes()
                    {
                        Title = $"{Common.ObjectLang.ResultExp} - {gameName}",
                        Model = new M_PackMeRes(rootFolder, platform, gameName),

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
                        Title = "Choose a Name",
                        Model = new M_ChooseName(root)
                        {
                            GameName = exploitableFileName,
                            ArchiveName = exploitableFileName,
                            Message = "Select archive name",
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
        /// <param name="compressMethod"></param>
        /// <param name="title">Titre de la tâche</param>
        /// <returns></returns>
        internal static bool? ZipCompressFolder(I_ASBaseC Objet, Func<object> compressMethod, string title = null)
        {
            return Application.Current.Dispatcher?.Invoke
                (
                    () =>
                    {
                        DxAsDoubleProgress dadProgress = new DxAsDoubleProgress()
                        {
                            TaskName = title,
                            HideCloseButton = true,
                            Model = M_ProgressC.Create<I_ASBaseC, Merde>(Objet, compressMethod),
                        };

                        if (dadProgress.ShowDialog() == true)
                        {
                            return true;
                        }

                        return false;
                    }
                );
        }


        internal static bool? LaunchOpProgress(I_ASBase Objet, Func<object> compressMethod, string title = null)
        {
            return Application.Current.Dispatcher?.Invoke
                (
                    () =>
                    {
                        DxTBoxCore.Box_Progress_2.DxAsProgress dadProgress = new DxTBoxCore.Box_Progress_2.DxAsProgress()
                        {
                            TaskName = title,
                            Model = M_Progress.Create(Objet, compressMethod),
                        };

                        if (dadProgress.ShowDialog() == true)
                        {
                            return true;
                        }

                        throw new OperationCanceledException("Interrupted by user");
                    }
                );
        }
    }
}