using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Common_PMG.Models;

namespace UnPack_My_Game.Models
{
    public class M_ModDefinePaths: A_Err
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        private string _RootCheats;
        /// <summary>
        /// Root for CheatCodes
        /// </summary>
        public string RootCheats 
        { 
            get => _RootCheats;
            set 
            {
                _RootCheats = value;
                Test_NullValue(value);
                OnPropertyChanged();
            }
        }


        private string _RootImg;
        /// <summary>
        /// Root for images
        /// </summary>
        public string RootImg 
        {
            get => _RootImg;
            set
            {
                _RootImg = value;
                Test_NullValue(value);
                OnPropertyChanged();
            }
        }

    }
}
