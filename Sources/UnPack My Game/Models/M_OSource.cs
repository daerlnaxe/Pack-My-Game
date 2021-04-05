using System;
using System.Collections.Generic;
using System.Text;
using UnPack_My_Game.Cont;

namespace UnPack_My_Game.Models
{
    class M_OSource : A_Source
    {
        private string _SelectedGame;
        public string SelectedGame
        {
            get => _SelectedGame;
            set
            {
                _SelectedGame = value;
                OnPropertyChanged();
                Test_NullValue(_SelectedGame);
            }
        }


        //private IEnumerable<Game> _Games = new Game[0];
        public override List<FileObj> Games { get; set; } = new List<FileObj>();
        /*{
            get => _Games;
            set
            {
                _Games = value;
                //OnPropertyChanged();
                //Test_HasElement(_Games);

                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Games)));
            }
        }*/


        public override void Add_Game(string linkGame)
        {
            base.Add_Game(linkGame);
            SelectedGame = linkGame;

            Properties.Settings.Default.LastSpath = System.IO.Path.GetDirectoryName(linkGame);
            Properties.Settings.Default.Save();
        }


        public override void CheckErrors()
        {
            Test_HasElement(Games, nameof(SelectedGame));
        }



    }
}
