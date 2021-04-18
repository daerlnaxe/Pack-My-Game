using Common_PMG.Models;
using Pack_My_Game.Cont;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pack_My_Game.Models
{
    public class M_ChooseName : A_Err
    {
        public Language Lang => Common.ObjectLang;
        public string GameName { get; set; }
        public string Message { get; set; }

        private string _ArchiveName;
        public string ArchiveName
        {
            get => _ArchiveName;
            set
            {
                _ArchiveName = value;
                this.Test_NullValue(value);
                OnPropertyChanged();
            }
        }

        private string _Root;

        public M_ChooseName(string root)
        {
            _Root = root;
        }

        public bool Verif()
        {

            string archivePath = System.IO.Path.Combine(_Root, ArchiveName);
            if (!ArchiveName.Equals(GameName) && System.IO.Directory.Exists(archivePath))
            {
                Add_Error("Folder exists", nameof(ArchiveName));
                return false;
            }

            return true;
        }


    }
}
