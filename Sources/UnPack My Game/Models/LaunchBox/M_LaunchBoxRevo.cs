﻿using Common_PMG.Models;
using System;
using System.IO;
using System.Linq;
using UnPack_My_Game.Graph;
using UnPack_My_Game.Language;
using UnPack_My_Game.Resources;
using static UnPack_My_Game.Common;

namespace UnPack_My_Game.Models.LaunchBox
{
    class M_LaunchBoxRevo : A_Err
    {
        #region Pages

        private I_Source _ActiveSrcPage;
        public I_Source ActiveSrcPage
        {
            get => _ActiveSrcPage;
            set
            {
                if (value == null)
                    return;

                if (ActiveSrcPage != value && _ActiveSrcPage != null)
                    _ActiveSrcPage.Model.Signal -= this.GamesSelected;

                if (ActiveSrcPage != value)
                {
                    _ActiveSrcPage = value;
                    _ActiveSrcPage.Model.Signal += this.GamesSelected;

                    OnPropertyChanged();
                }


            }
        }

        /*
        private I_Method _ActiveMethodPage;
        public I_Method ActiveMethodPage
        {
            get => _ActiveMethodPage;
            set
            {
                _ActiveMethodPage = value;
                OnPropertyChanged();
            }
        }*/

        #endregion
        // --- 

        //private string _LaunchBoxPath;
        public string LaunchBoxPath
        {
            get => Config.HLaunchBoxPath;
            set
            {
                Config.HLaunchBoxPath = value;
                OnPropertyChanged();

                Test_NullValue(value);
            }
        }

        // --- 

        #region Folders
        //private string _Games;
        public string Games
        {
            get => Config.Games;
            set
            {
                Config.Games = value;
                OnPropertyChanged();

                Test_NullValue(value);
            }
        }

        //private string _CheatCodes;
        public string CheatCodes
        {
            get => Config.CheatCodes;
            set
            {
                Config.CheatCodes = value;
                OnPropertyChanged();

                Test_NullValue(value);
            }
        }

        //public string _Images;
        public string Images
        {
            get => Config.Images;
            set
            {
                Config.Images = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }

        //private string _Manuals;
        public string Manuals
        {
            get => Config.Manuals;
            set
            {
                Config.Manuals = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }

        //private string _Musics;
        public string Musics
        {
            get => Config.Musics;
            set
            {
                Config.Musics = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }

        //private string _Videos;
        public string Videos
        {
            get => Config.Videos;
            set
            {
                Config.Videos = value;
                OnPropertyChanged();
                Test_NullValue(value);
            }
        }

        internal E_Method Mode { get; set; }
        #endregion

        // ---

        public M_LaunchBoxRevo()
        {
            // Vérifications
            var platformsFile = Path.Combine(Config.HLaunchBoxPath, Config.PlatformsFile);

            // Vérification du dossier Launchbox
            if (!File.Exists(platformsFile))
            {
                Add_Error(Lang.Err_PlatformsF, nameof(LaunchBoxPath));
            }

            Common.PlatformsFile = platformsFile;


            //Load_Platforms();
        }

 

        internal void GamesSelected(object sender)
        {
            Remove_Error(nameof(ActiveSrcPage));

            if (ActiveSrcPage.Model.Games.Count() < 1)
                Add_Error(Lang.No_Game, nameof(ActiveSrcPage));
        }

        // ---

        internal void Process()
        {
            //_Errors.Clear();

            // Vérification des données
            Test_NullValue(LaunchBoxPath, nameof(LaunchBoxPath));
            if (!Directory.Exists(this.LaunchBoxPath))
                Add_Error(LanguageManager.Instance.Lang.Err_LaunchBoxF, nameof(LaunchBoxPath));

            Test_NullValue(Games, nameof(Games));
            Test_NullValue(CheatCodes, nameof(CheatCodes));
            Test_NullValue(Images, nameof(Images));
            Test_NullValue(Manuals, nameof(Manuals));
            Test_NullValue(Musics, nameof(Musics));
            Test_NullValue(Videos, nameof(Videos));


            //Test_NullValue(SelectPlatform, nameof(SelectPlatform));

            // Test pour les jeux choisis
            // Queue<Cont.FileObj> games = new Queue<Cont.FileObj>();

            A_Source src = ActiveSrcPage.Model;
          //  A_Method method = ActiveMethodPage.Model;

            /* foreach (var game in src.Games)
             {
                 string ext = Path.GetExtension(game.Path);
                 if (ext.Equals(".zip", StringComparison.OrdinalIgnoreCase) || ext.Equals(".7zip", StringComparison.OrdinalIgnoreCase))
                     games.Enqueue(game);
             }*/

            //src.Games = games;

            src.CheckErrors();
    //        method.CheckError();

            /*if (games.Count() < 1)
                src.RaiseError(nameof(src.Games), Lang.NoElem);*/

            if (this.HasErrors || src.HasErrors )
                return;

            // Sauvegarde des folders
            Config.Save();

                /*15/04/2021
            try
            {
                C_LaunchBoxDPG clbDPG = new C_LaunchBoxDPG();

                DxAsStateProgress daspW = new DxAsStateProgress()
                {
                    AutoClose = false,
                    Model = M_ProgressD.Create<C_LaunchBoxDPG, M_ProgressDL>(clbDPG, ()=> clbDPG.InjectSeveralGames(src.Games)),
                };




                if (Mode == E_Method.LBMethod)
                {
               /*     M_LBMethod model = (M_LBMethod)ActiveMethodPage.Model;
                    C_LaunchBoxAdapt clbAdapt = new C_LaunchBoxAdapt(src.Games, model.SelectPlatform.Name);

                    daspW = new DxAsStateProgress()
                    {
                        AutoClose = false,
                        Model = M_ProgressC.Create<C_LaunchBoxAdapt, M_ProgressCC>(clbAdapt, () => clbAdapt.Run())


                        //            TaskToRun = clbAdapt,
                    };


                    */
                    //C_LaunchBox.Run_LBMethode(ref games, model.SelectPlatform);
                    /*15/04/2021
                }
                else if (Mode == E_Method.EBMethod)
                {
                    /*M_EBMethod model = (M_EBMethod)ActiveMethodPage.Model;
                    C_LaunchBoxEB clbEBAdapt = new C_LaunchBoxEB(src.Games);

                    daspW = new DxAsStateProgress()
                    {
                        AutoClose = false,
                        Model = M_ProgressC.Create<C_LaunchBoxEB, M_ProgressCC>(clbEBAdapt, () => clbEBAdapt.Run())


                        //TaskToRun = clbEBAdapt
                    };*/
            /*15042021
                }
                else if(Mode == E_Method.TBMethod)
                {
/*                    M_DPGMethod model = (M_DPGMethod)ActiveMethodPage.Model;

                    C_LaunchBoxTB clbTB = new C_LaunchBoxTB(src.Games, model.SelectedXML );

                    daspW = new DxAsStateProgress()
                    {
                        AutoClose = false,
                        Model = M_ProgressC.Create<C_LaunchBoxTB, M_ProgressCC>(clbTB, () => clbTB.Run(model.Plateforme))


                        //TaskToRun = clbEBAdapt
                    };
*/
            /*1504/2021
                }
                if (daspW != null)
                    daspW.ShowDialog();
            }
            catch (Exception exc)
            {

                Debug.WriteLine(exc.Message);
            }

            // Lancement du traitement
            /*if (DxMBox.ShowDial(Lang.Q_Continue, Lang.Question, E_DxButtons.No | E_DxButtons.Yes) == true)
            {
                C_LaunchBox lbCore = new C_LaunchBox();
                //lbCore.Run(LaunchBoxPath, SelectPlatform, ref games);
            }*/

            // Récupération des sources
        }
    }
}
