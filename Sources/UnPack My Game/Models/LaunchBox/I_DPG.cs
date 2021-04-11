using Common_PMG.Container;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace UnPack_My_Game.Models.LaunchBox
{
    public interface I_DPG
    {


        public string Information{ get; }

        public string SelectSentence { get; }
        public bool HasErrors { get; }

        public ObservableCollection<DataRep> Elements { get; set; }

        public void SelectFunc();

        public bool Process();

        void Add();
        void RemoveElement(object element);
        void RemoveElements(IList<object> parameter);
    }
}
