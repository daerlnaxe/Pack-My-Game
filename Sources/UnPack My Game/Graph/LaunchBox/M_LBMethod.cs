using Common_PMG.Container;
using Common_PMG.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using UnPack_My_Game.Models;

namespace UnPack_My_Game.Graph.LaunchBox
{
    public class M_LBMethod : A_Method
    {
        public event BasicHandler Error;

        public ObservableCollection<ContPlatFolders> Machines { get; set; } = new ObservableCollection<ContPlatFolders>();

        private ContPlatFolders _SelectedPlatform;
        public ContPlatFolders SelectPlatform
        {
            get => _SelectedPlatform;
            set
            {
                _SelectedPlatform = value;
                Test_NullValue(value);
            }
        }

        public M_LBMethod()
        {
            Load_Platforms();
        }

        internal void Load_Platforms()
        {
            Remove_Error(nameof(SelectPlatform));
            try
            {
              //  XML_Platforms.ListShortPlatforms(Common.PlatformsFile, Machines);

                /*// Extracting XML datas
                XML_Functions xf = new XML_Functions();
                if (xf.ReadFile(Common.PlatformsFile))
                    xf.ListMachine();*/
            }
            catch
            {
                Debug.WriteLine("Problem on platform.xml opening");
            }

        }

        public override void CheckError()
        {
            Test_NullValue(SelectPlatform, nameof(SelectPlatform));
        }
    }
}
