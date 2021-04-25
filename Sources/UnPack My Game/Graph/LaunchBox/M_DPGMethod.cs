using Common_PMG.Container;
using Common_PMG.XML;
using System;
using System.Collections.Generic;
using System.Text;
using UnPack_My_Game.Models;

namespace UnPack_My_Game.Graph.LaunchBox
{
    class M_DPGMethod : A_Method
    {

        private string _SelectedXML;
        public string SelectedXML
        {
            get => _SelectedXML;
            set
            {
                _SelectedXML = value;
                OnPropertyChanged();
                Test_NullValue(value);

            }
        }

        private ContPlatFolders _Plateforme;
        public ContPlatFolders Plateforme
        {
            get => _Plateforme;
            set
            {
                _Plateforme = value;
                Test_NullValue(value);
                OnPropertyChanged();
            }
        }

        public override void CheckError()
        {
            Test_NullValue(SelectedXML, nameof(SelectedXML));
            Test_NullValue(Plateforme, nameof(Plateforme));
        }

        internal void AssignXML(string linkResult)
        {
            if (XML_Custom.CheckValidity(linkResult, "Platform"))
            {
                SelectedXML = linkResult;
                Plateforme = XML_Platforms.GetDirectPlatform(linkResult);

            }
        }
    }
}
