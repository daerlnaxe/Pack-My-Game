using System;
using System.ComponentModel;
using PS = Pack_My_Game.Properties.Settings;
using Pack_My_Game.Cont;
using System.Collections.ObjectModel;
using System.IO;
using Hermes;
using LaunchBox_XML.XML;
using LaunchBox_XML.Container;
using System.Collections.Generic;
using DxTBoxCore.MBox;
using DxTBoxCore.Common;
using Pack_My_Game.Core;
using DxTBoxCore.Box_Progress;
using LaunchBox_XML.Container.Game;
using DxTBoxCore.Box_Collec;
using System.Linq;

namespace Pack_My_Game.Models
{
    public class M_Main : A_Err, I_Lang
    {

        // private Language _Lang;
        public Language Lang => Common.ObjectLang;


        /// <summary>
        /// Dossier de LaunchBox
        /// </summary>
        public string LaunchBoxPath => PS.Default.LBPath;


        public string WorkingFolder => PS.Default.OutPPath;

        public ObservableCollection<string> Options { get; internal set; } = new ObservableCollection<string>();

        // ---

        public Platform SelectedPlatform { get; set; }
        public ObservableCollection<Platform> Platforms { get; private set; } = new ObservableCollection<Platform>();

        // ---

        private ObservableCollection<ShortGame> _AvailableGames;
        public ObservableCollection<ShortGame> AvailableGames
        {
            get => _AvailableGames;
            set
            {
                _AvailableGames = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<ShortGame> SelectedGames = new ObservableCollection<ShortGame>();

        // ---
        public bool NoGame { get; set; } = true;

        public M_Main()
        {
            LoadOptions();
            LoadPlatforms();
        }


        internal void Relocalize()
        {
            OnPropertyChanged(nameof(Lang));
        }

        internal void ReloadConfig()
        {
            OnPropertyChanged(nameof(LaunchBoxPath));
            OnPropertyChanged(nameof(WorkingFolder));
            LoadOptions();
            LoadPlatforms();
        }

        private void LoadOptions()
        {
            Options.Clear();

            if (PS.Default.opInfos)
                Options.Add(Lang.InfosGExp);

            if (PS.Default.opOBGame)
                Options.Add(Lang.OriXBExp);

            if (PS.Default.opEBGame)
                Options.Add(Lang.EnXBExp);

            if (PS.Default.opTreeV)
                Options.Add(Lang.TViewFExp);

            if (PS.Default.opClones)
                Options.Add(Lang.CCopyExp);

            if (PS.Default.opCheatCodes)
                Options.Add(Lang.CheatsCopyExp);

            if (PS.Default.opZip)
                Options.Add(Lang.CompZExp);

            if (PS.Default.op7_Zip)
                Options.Add(Lang.Comp7ZExp);
        }

        private void LoadPlatforms()
        {
            string xmlFilePlat = Path.Combine(LaunchBoxPath, PS.Default.fPlatforms);
            if (!File.Exists(xmlFilePlat))
            {
                HeTrace.WriteLine($"File doesn't exist '{xmlFilePlat}'");
                return;
            }

            Platforms.Clear();
            XML_Platforms.ListShortPlatforms(xmlFilePlat, Platforms);

        }

        internal void LoadGames()
        {
            SelectedGames.Clear();

            string xmlFile = Path.Combine(PS.Default.LBPath, PS.Default.dPlatforms, $"{SelectedPlatform.Name}.xml");
            AvailableGames = new ObservableCollection<ShortGame>(  XML_Games.ListShortGames(xmlFile));
            // throw new NotImplementedException();
        }

        internal void AddGame(ShortGame sgame)
        {
            if (!SelectedGames.Contains(sgame))
                SelectedGames.Add(sgame);
            Test_HasElement(SelectedGames, nameof(NoGame));
        }

        internal void RemoveGame(ShortGame sgame)
        {
            SelectedGames.Remove(sgame);
            Test_HasElement(SelectedGames, nameof(NoGame));
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Process()
        {
            Test_HasElement(SelectedGames, nameof(NoGame));

            if (HasErrors)
                return;

            LaunchBoxCore lbCore = new LaunchBoxCore(SelectedPlatform.Name);


            if (lbCore.Initialize(SelectedGames))
            {
                DxAsStateProgress progressW = new DxAsStateProgress()
                {
                    AutoClose = false,
                    Model = (M_ProgressCC)M_ProgressC.Create<LaunchBoxCore, M_ProgressCC>(lbCore, ()=> lbCore.Run()),
                    
                    /*new M_ProgressCC()
                    {
                        MaxProgressT = SelectedGames.Count,
                    },
                    TaskToRun = lbCore*/
                };
                progressW.ShowDialog();

                // On vérifie que la tâche est allée jusqu'au bout
                if(progressW.Model.TaskRunning.Status != System.Threading.Tasks.TaskStatus.RanToCompletion)
                {
                    HeTrace.WriteLine("Task not ran to completion");
                    return;
                    
                }

                if (DxMBox.ShowDial($"Remove game(s) from the list ?", "Remove game(s)", E_DxButtons.No | E_DxButtons.Yes) == true)
                {
                    HeTrace.WriteLine($"Remove game(s) from list");
                    foreach (ShortGame sgame in SelectedGames)
                        AvailableGames.Remove(sgame); 
                }
            }

            // traiter pour retirer ce qui est traité? voir etc etc
        }

  


    }
}
