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
            {
                MessageBox.Show("Saved", "State of Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btOK.Enabled = true;

            }
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



        private void CheatForm_Load(object sender, EventArgs e)
        {

        }

        private void tbCrop_Click(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// Limite les caractères aux digits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCrop_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }


        /// <summary>
        /// Enclenche le découpage du texte en fonction de la taille demandée dans la textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCrop_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbCrop.Text))
            {
                var whenToCrop = Convert.ToUInt16(tbCrop.Text);
                CropTextBox(whenToCrop);
            }
        }

        private void rtBOX1_TextChanged(object sender, EventArgs e)
        {
            int position = rtBOX1.SelectionStart;
            if (!string.IsNullOrEmpty(tbCrop.Text) && cbCropActive.Checked)
            {
                var whenToCrop = Convert.ToUInt16(tbCrop.Text);
                CropTextBox(whenToCrop);
            }
            else
            {
                cbCropActive.Checked = false;
            }
            rtBOX1.SelectionStart = position;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxLength"></param>
        private void CropTextBox(ushort maxLength)
        {
            String[] lines = rtBOX1.Text.Split('\n');
            string formatedString = null;
            // Examen de  chaque ligne
            foreach (string line in lines)
            {
                // Si la ligne fait moins que la taille max
                if (line.Length <= maxLength)
                    formatedString += line + '\n';
                else
                {
                    formatedString += FormatLine(maxLength, line) + '\n';
                }

            }
            rtBOX1.Text = formatedString;



            /* exemple avec les chars fonctionne mais pas top
            string chaine = rtBOX1.Text;
            string formattedString = null;
            uint i = 0;            

            foreach (char c in chaine)
            {
                // Cas d'une retour à la ligne
                if (c == '\n')
                {

                    i = 0;
                }

                // Si i dépasse la limite
                else if (i >= maxLength)
                {
                    formattedString += '\n';
                    i = 0;
                }

                formattedString += c;
                i++;
            }


            rtBOX1.Text = formattedString;
            */


            /*
            string[] lines = rtBOX1.Text.Split('\n');

            List<string> formatTxt = new List<string>();
            foreach (var line in lines)
            {



                ushort i = 0;

                //
                while (true)
                {
                    if (i + 200 < line.Length)
                    {
                        formatTxt.Add(line.Substring(i, 200));
                        i += 200;
                    }
                    else
                    {
                        formatTxt.Add(line.Substring(i) + "\n");
                        break;
                    }

                }
            }*/

            //  rtBOX1.Text = formatTxt.ToString();
        }

        private string FormatLine(ushort maxLength, string line)
        {
            // Calcul des fourchettes
            int minSize = maxLength - maxLength * 10 / 100;
            int maxSize = maxLength + maxLength * 10 / 100;

            string[] words = line.Split(' ');
            string formatedLine = string.Empty;
            int i = 0;

            //
            foreach (string word in words)
            {/*
                // En fonction de i, s'il a déjà dépassé la limite min on commence à évaluer
                if (i >= minSize)
                {
                    // Si en rajoutant le mot on dépasse la taille max
                    if (i + word.Length > maxSize)
                    {
                        formatedLine += '\n';
                        i = 0;
                    }
                }*/
                if(i +word.Length >= maxLength)
                {
                    formatedLine += '\n';
                    i = 0;

                }

                formatedLine += $"{word} ";
                i += word.Length + 1;


                //i += word.Length + 1; // On calcule 
            }

            return formatedLine;
        }

        private void cbCropActive_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbCropActive.Checked)
                return;

            if (!string.IsNullOrEmpty(tbCrop.Text))
            {
                var whenToCrop = Convert.ToUInt16(tbCrop.Text);
                CropTextBox(whenToCrop);
            }
            else
            {
                cbCropActive.Checked = false;
                MessageBox.Show("Width is null, enter a value", "Null value", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}

