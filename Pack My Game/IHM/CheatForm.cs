using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pack_My_Game.IHM
{
    public partial class CheatForm : Form
    {
        /// <summary>
        /// Chemin vers le fichier des Cheats
        /// </summary>
        string _CheatFilePath;

        /// <summary>
        /// Racine du chemin exploitable
        /// </summary>
        string _RootPath;

        /// <summary>
        /// Nom du jeu
        /// </summary>
        string _GameName;

        /// <summary>
        /// Repertoire des cheats
        /// </summary>
        string _CheatDirectory;

        public CheatForm()
        {
            InitializeComponent();

        }

        public CheatForm(string gameName, string folderPath)
        {
            _GameName = gameName;
            _CheatDirectory = folderPath;

            InitializeComponent();

            this.Text = _GameName;

            // Recherche d'un nom pour le nouveau fichier
            _RootPath = Path.Combine(folderPath, $"{gameName}");


            // Création du fichier
            //using (var f = File.Open(_CheatFilePath, FileMode.Create)) ;
        }

        /*
        public static void Showdialog(string title)
        {
            CheatForm cf = new CheatForm();
            cf.Text = title;
            cf.ShowDialog();

        }*/

        /// <summary>
        /// Save button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSave_Click(object sender, EventArgs e)
        {
            if (SaveToFile())
                MessageBox.Show("Saved", "State of Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    /*using(FileStream f = File.OpenWrite(_CheatFilePath))
            {
                f.
                    .Write(rtBOX1.Text);
            }*/
        }

        private bool SaveToFile()
        {
            //ushort limit = 100;

            // Si le chemin n'a pas encore été définit
                //string cheatpath;
            ushort version = 01;
            if (string.IsNullOrEmpty(_CheatFilePath))
            {
                while (true)
                {
                    string testFile = $"{_RootPath}-{version}.txt";

                    if (!File.Exists(testFile))
                    {
                        //cheatpath = testFile;
                        break;
                    }

                    version++;
                }

                // Proposition du chemin de sortie
                SaveFileDialog ofd = new SaveFileDialog()
                {
                    InitialDirectory = _CheatDirectory,
                    FileName = $"{_GameName}-{version}.txt"
                };

                // Assignation du chemin de sortie
                if (ofd.ShowDialog() == DialogResult.OK)
                    _CheatFilePath = Path.Combine(_CheatDirectory, ofd.FileName);

            }
            else
            {
                if (MessageBox.Show("Do you want to save ?", "Save cheat code ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return false;
            }


            // Sauvegarde
            try
            {
                rtBOX1.SaveFile(_CheatFilePath, RichTextBoxStreamType.PlainText);
                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            return false;
        }
    }
}
