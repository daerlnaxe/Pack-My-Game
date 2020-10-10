using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        private bool _ConvertSpaces = true;

        public CheatForm()
        {
            InitializeComponent();

        }

        /// <summary>
        /// Ouvre un fichier Cheat
        /// </summary>
        /// <param name="CFilePath"></param>
        public static void Open(string CFilePath)
        {
            if (!File.Exists(CFilePath))
                return;

            CheatForm cf = new CheatForm();
            cf._CheatFilePath = CFilePath;

            try
            {
                cf.rtBOX1.Text = File.ReadAllText(CFilePath);
                cf.ShowDialog();

            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        public CheatForm(string gameName, string folderPath)
        {
            _GameName = gameName.Replace(":", " - ");
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

        /// <summary>
        /// Sauvegarde vers un fichier
        /// </summary>
        /// <returns></returns>
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
                    string _RootPath1 = _RootPath;
                    string testFile = $"{_RootPath1}-{version}.txt";

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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheatForm_Load(object sender, EventArgs e)
        {
            // Changement de l'espacement de tabulation
            rtBOX1.SelectionTabs = new int[] { 50, 100, 150, 200 };
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

            if (_ConvertSpaces)
                rtBOX1.Text = rtBOX1.Text.Replace("    ", "\t");

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

            int i = 1;
            // Examen de  chaque ligne            
            foreach (string line in lines)
            {
                // Si la ligne fait moins que la taille max
                if (line.Length <= maxLength)
                {
                    formatedString += line;// + '\n';
                }
                else
                {
                    formatedString += FormatLine(maxLength, line);// + '\n';
                }

                if (i < lines.Length)
                    formatedString += '\n';            

                i++;
            }

            rtBOX1.Text = formatedString;
            return;

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

        /// <summary>
        /// Formatte une ligne
        /// </summary>
        /// <param name="maxLength"></param>
        /// <param name="line"></param>
        /// <returns></returns>
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
                if (i + word.Length >= maxLength)
                {
                    formatedLine += '\n';
                    i = 0;
                }

                formatedLine += $"{word}";
                i += word.Length + 1;

                // On ajoute un espace sauf pour le dernier mot
                    formatedLine += " ";
                if (i < words.Length)
                {

                }
                else
                {
                    formatedLine += "|";
                    Debug.WriteLine($"Last Word {word}");
                }
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

        #region Menu

        /// <summary>
        /// Copier une partie dans le presse papier / Copy a part to Clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rtBOX1.SelectedText))
                Clipboard.SetText(rtBOX1.SelectedText);
        }

        /// <summary>
        /// Tout copier dans le presse papier / Copy All to ClipBoard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(rtBOX1.Text))
                Clipboard.SetText(rtBOX1.Text);
        }

        /// <summary>
        /// Copier dans la box / Copy to RichTextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tmp = Clipboard.GetText();
            if (_ConvertSpaces)
                tmp = tmp.Replace("    ", "\t");
            rtBOX1.Text = tmp;
        }

        /// <summary>
        /// Select All
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtBOX1.SelectAll();
        }
        #endregion

        /// <summary>
        /// Bouton abaissé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtBOX1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        contextMSText.Show(this, new Point(e.X, e.Y));//places the menu at the pointer position
                    }
                    break;
            }
        }

        /// <summary>
        /// Recherche sur Gamefaq
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameFaqToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Process.Start($"https://gamefaqs.gamespot.com/search?game={GetSearchName()}");
        }

        /// <summary>
        /// Recherche sur JeuxVideo.com
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jeuxVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Process.Start($"https://www.jeuxvideo.com/recherche.php?q={GetSearchName()}");
        }

        private string GetSearchName()
        {
            string toSearch = _GameName.Replace(' ', '+');
            toSearch = toSearch.Replace(":", "");
            return toSearch;
        }
    }
}

