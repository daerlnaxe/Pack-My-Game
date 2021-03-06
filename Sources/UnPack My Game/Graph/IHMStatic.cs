﻿using AsyncProgress;
using AsyncProgress.Tools;
using Common_PMG.Container.Game;
using DxLocalTransf;
using DxLocalTransf.Cont;
using DxTBoxCore.Box_Progress;
using DxTBoxCore.BoxChoose;
using DxTBoxCore.Common;
using DxTBoxCore.Box_MBox;
using System;
using System.IO;
using System.Windows;
using DxLocalTransf.Tools;
using static Common_PMG.Tool;
using static UnPack_My_Game.Common;

namespace UnPack_My_Game.Graph
{
    static class IHMStatic
    {
        public static bool? AskDxMBox(string message, string title, E_DxButtons buttons, string optionnalMessage = null)
        {
            return Application.Current.Dispatcher?.Invoke
                (
                    () =>
                    {
                        return DxMBox.ShowDial(message, title, buttons, optionnalMessage);
                    }
                );
        }

   
        public static string GetAFile(string startingF, string info, params string[] extensions)
        {
            return Application.Current.Dispatcher?.Invoke
                (
                    () =>
                    {
                        TreeChoose tc = new TreeChoose()
                        {
                            Model = new M_ChooseRaw()
                            {
                                StartingFolder = startingF,
                                Mode = ChooseMode.File,
                                ShowFiles = true,
                                Info = info,
                                FilesExtension = extensions,
                            }
                        };
                        if (tc.ShowDialog() == true)
                        {
                            Config.LastTargetPath = Path.GetDirectoryName(tc.Model.LinkResult);
                            Config.Save();

                            return tc.Model.LinkResult;

                        }
                        return null;
                    }
                );
        }


 
        internal static void ShowDPG(GameDataCont gpC, GamePaths gpx, string gamePath)
        {
            Application.Current.Dispatcher?.Invoke
                (
                    () =>
                    {
                        W_DPGMaker winDPG = new W_DPGMaker()
                        {
                            Model = new M_DPGMaker(gpC, gpx, gamePath),
                        };
                        if (winDPG.ShowDialog() == true)
                        {
                            // ...
                        }
                    }
                );
        }

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
                                Message = "Overwrite ?",
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
    }
}
