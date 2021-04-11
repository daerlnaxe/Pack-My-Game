using Common_PMG.Container;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UnPack_My_Game.Models
{
    class M_SSource : A_Source
    {
        //public override event BasicHandler Signal;
        private string _SelectedFolder;
        public string SelectedFolder
        {
            get => _SelectedFolder;
            set
            {
                _SelectedFolder = value;
                OnPropertyChanged();
                Test_NullValue(_SelectedFolder);
            }
        }

        public override List<DataRep> Games { get; set; } = new List<DataRep>();


        private Queue<DataRep> _AvailableGames;
        public Queue<DataRep> AvailableGames
        {
            get => _AvailableGames;
            set
            {
                _AvailableGames = value;
                OnPropertyChanged();
            }
        }

        public override void CheckErrors()
        {
            Test_HasElement(Games, nameof(AvailableGames));
        }

        public void Initialize_AvailableGames(string linkFolderGames)
        {
            if (string.IsNullOrEmpty(linkFolderGames))
                return;

            /*else
            {*/

            AvailableGames = new Queue<DataRep>(); //new List<FileObj>();
            string[] extensions = new string[] { "zip", "7zip" };

            foreach (string f in Directory.EnumerateFiles(linkFolderGames, "*.*", SearchOption.AllDirectories))
                if (extensions.Contains(Path.GetExtension(f).TrimStart('.').ToLowerInvariant()))
                    AvailableGames.Enqueue(new DataRep(f));

            Test_HasElement(AvailableGames, nameof(SelectedFolder));

            //};

            Properties.Settings.Default.LastSpath = linkFolderGames;
            Properties.Settings.Default.Save();

            

            //base.Signal?.Invoke(this);
        }

    }
}
