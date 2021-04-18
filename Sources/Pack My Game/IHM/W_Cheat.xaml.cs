using DxTBoxCore.Common;
using DxTBoxCore.MBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Pack_My_Game.IHM
{
    /// <summary>
    /// Logique d'interaction pour W_Cheat.xaml
    /// </summary>
    public partial class W_Cheat : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static readonly RoutedUICommand SaveCmd = new RoutedUICommand("Save", "SaveCmd", typeof(W_Cheat));
        public static readonly RoutedUICommand OkCmd = new RoutedUICommand("Ok", "OkCmd", typeof(W_Cheat));

        private string _CheatCode;
        public string CheatsCode
        {
            get => _CheatCode;
            set
            {
                if (FormatActive)
                    _CheatCode = limit(value);
                else
                    _CheatCode = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CheatsCode)));
            }
        }

        private bool _FormatActive;
        public bool FormatActive
        {
            get => _FormatActive;
            set
            {
                _FormatActive = value;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(FormatActive)));
            }
        }

        public int MaxLength
        {
            get;
            set;
        } = 30;


        public bool _IsOk;

        /// <summary>
        /// Dossier de sauvegarde des cheat codes
        /// </summary>
        public string _CheatCodesPath;

        /// <summary>
        /// Chemin du fichier de cheats 
        /// </summary>
        public string _CheatFilePath;

        /// <summary>
        /// Nom du jeu
        /// </summary>
        private string _GameName;


        public W_Cheat(string cheatCodesPath, string gameName)
        {
            _CheatCodesPath = cheatCodesPath;
            _GameName = gameName;

            InitializeComponent();
            DataContext = this;
        }

        public W_Cheat(string cheatCodesPath, string gameName, string selectedCheatFile) : this(cheatCodesPath, gameName)
        {

            using (FileStream fs = new FileStream(_CheatFilePath, FileMode.Open, FileAccess.Read))
            using (StreamReader rs = new StreamReader(fs))
            {

                CheatsCode = rs.ReadToEnd();
            }
        }

        private void Can_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = CheatsCode != null && CheatsCode.Length > 0;
        }

        private void Exec_Save(object sender, ExecutedRoutedEventArgs e)
        {
            if (SaveToFile())
            {
                DxMBox.ShowDial("Saved", "State of Save", E_DxButtons.Ok);
                _IsOk = true;

            }

        }

        private bool SaveToFile()
        {

            // Si le chemin n'a pas encore été définit
            ushort version = 01;

            if (string.IsNullOrEmpty(_CheatFilePath))
            {
                string testFile = string.Empty;
                while (true)
                {
                    testFile = System.IO.Path.Combine(_CheatCodesPath, $"{_GameName}-{version.ToString("000")}.txt");

                    if (!File.Exists(testFile))
                    {
                        //cheatpath = testFile;
                        break;
                    }

                    version++;
                }
                /*
                     // Proposition du chemin de sortie
                     SaveFileDialog ofd = new SaveFileDialog()
                     {
                         InitialDirectory = _CheatDirectory,
                         FileName = $"{_GameName}-{version}.txt"
                     };

                     // Assignation du chemin de sortie
                     if (ofd.ShowDialog() == DialogResult.OK)
                         _CheatFilePath = Path.Combine(_CheatDirectory, ofd.FileName);*
                    */
                _CheatFilePath = testFile;
            }
            else
            {
                if (DxMBox.ShowDial("Do you want to save ?", "Save cheat code ?", E_DxButtons.No | E_DxButtons.Yes) != true)
                    return false;
            }


            // Sauvegarde
            try
            {
                using (FileStream fs = new FileStream(_CheatFilePath, FileMode.OpenOrCreate))
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.Write(CheatsCode);
                    sw.Flush();
                }
                //         rtBOX1.SaveFile(_CheatFilePath, RichTextBoxStreamType.PlainText);

                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return false;
        }

        // ---

        private void Can_Ok(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _IsOk;
        }

        private void Exec_Ok(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            _IsOk = false;
        }
        /*
        public string limit(string tmp)
        {
            // Quand on va à la ligne on ne laisse pas d'espace on remplace l'espace !!! (Basé sur les éditeurs)
            // Aucune remise en cause du saut de ligne, c'est à l'utilisateur de gérer ça (Basé sur les éditeurs)
            // Si un mot dépasse la longueur de ligne, soit on a un espace possible soit on coupe

            int position = tbMain.SelectionStart;

            string formatedText = string.Empty;

            //string[] lines = tmp.Split("\r\n");

            char c;

            int i = 0;
            string tmp2 = string.Empty;
            while (i < tmp.Length)
            {
                c = tmp[i];
                tmp2 += c;

                // Lignes
                if (tmp2.Contains("\r\n"))
                {
                    formatedText += FormatLine2(i, ref tmp2, ref position);
                    tmp2 = string.Empty;
                }
                else if(i >= tmp.Length -1 )
                {
                    formatedText += FormatLine2(i, ref tmp2, ref position);
                    tmp2 = string.Empty;
                }

                i++;
            }



            /*
        foreach (string line in lines)
        {
            if (line.Length <= MaxLength)
            {
                formatedText += line + "\r\n";
            }
            else
            {
                string[] words = line.Split(' ');
                foreach (string word in words)
                {
                    if (newline.Length + word.Length <= MaxLength)
                    {
                        newline += $"{word} ";
                    }
                    else
                    {
                        formatedText += $"{newline}\r\n{word.TrimStart()}";
                        newline = string.Empty;
                        position += 2;
                    }
                }

            }
        }
            */

        /*
            return formatedText;
            //e.Handled = true;
            tbMain.SelectionStart = position;


            //e.Handled = true;

        }


        private string FormatLine2(int previousPos, ref string tmp, ref int position)
        {
            string formatedText = string.Empty;
            string tmp2 = string.Empty;
            string newline = string.Empty;

            char c;

            int i = 0;
            // Mots
            while (i < tmp.Length)
            {
                c = tmp[i];
                /*if (c == '\r' || c == '\n')
                    return formatedText + newline + tmp2 + c;*/
        /*      if(tmp2.Contains("\r\n"))
                  return formatedText + newline + "\r\n";

              tmp2 += c;

              if (tmp2.Contains(' '))
              {
                  if (newline.Length + tmp2.Length > MaxLength)
                  {
                      formatedText += newline + "\r\n" + tmp2;
                      newline = tmp2 = string.Empty;
                      if (position >= previousPos)
                          position += 2;
                  }
                  else
                  {
                      newline += tmp2;
                      tmp2 = string.Empty;
                  }
              }


              i++;
          }

          if (!string.IsNullOrEmpty(tmp2))
              newline += tmp2;

          return formatedText + newline;
      }
        */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tmp"></param>
        /// <returns></returns>
        /// <remarks>
        /// Algorithme parfait, ne pas toucher !!! 
        /// </remarks>
        private string limit(string tmp)
        {
            string formatedText = string.Empty;
            int position = tbMain.SelectionStart;
            int lvl = 0;

            string[] lines = tmp.Split("\r\n");
            int charCount;  // Compte les caractères par ligne

            string line;
            for (int i = 0; i < lines.Length; i++)
            {
                string l = lines[i];

                lvl += l.Length;
                line = string.Empty;

                if (l.Length <= MaxLength)
                {
                    Debug.WriteLine($"On passe sur le formatage de ligne:{l}");

                    if (i != 0)
                        formatedText += "\r\n";

                    formatedText += l;

                    continue;
                }

                // Rajout d'un saut de ligne sauf pour la première.
                if (i != 0)
                    formatedText += "\r\n";

                Debug.WriteLine($"On va travailler sur la ligne: {l}");
                charCount = 0;
                foreach (string word in l.Split(' '))
                {
                    if (charCount + word.Length <= MaxLength)
                    {
                        if (charCount != 0)
                        {
                            line += " ";
                            charCount++;
                        }

                        line += word;
                        charCount += word.Length; // A cause de la partie rajoutée quand ça dépasse mieux vaut compter
                    }

                    else
                    {
                        if (word.Length >= MaxLength)
                            formatedText += CutWord($"{line} {word}", position);
                        else
                        {
                            formatedText += line + "\r\n" + word;
                            position += 2;
                            charCount = word.Length;
                        }

                        line = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(line))
                    formatedText += line;
            }

            tbMain.SelectionStart = position;
            return formatedText;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toCut"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        /// 
        /// <remarks>
        /// algorithme parfait, ne pas toucher !
        /// </remarks>
        private string CutWord(string toCut, int position)
        {
            string resultat = string.Empty;
            int comptChar = 0;

            string tmp = string.Empty;
            foreach (char c in toCut)
            {
                tmp += c;
                comptChar++;

                if (comptChar >= MaxLength)
                {
                    if (!string.IsNullOrEmpty(resultat))
                        resultat += "\r\n";

                    resultat += tmp;
                    comptChar = 0;

                    tmp = string.Empty;
                    position += 2;
                }
            }

            if (!string.IsNullOrEmpty(tmp))
                resultat += "\r\n" + tmp;

            return resultat;
        }

        private void Crop_Click(object sender, RoutedEventArgs e)
        {
            Crop();
        }

        private void Crop()
        {
            String[] lines = CheatsCode.Split("\r\n");
            string formatedString = null;

            int i = 1;
            // Examen de  chaque ligne            
            foreach (string line in lines)
            {
                // Si la ligne fait moins que la taille max
                if (line.Length <= MaxLength)
                {
                    formatedString += line;// + '\n';
                }
                // Si la ligne fait plus que la taille max
                else
                {
                    formatedString += FormatLine(MaxLength, line);// + '\n';
                }
                // Pour les lignes où il y a déjà un retour à la ligne
                if (i < lines.Length)
                    formatedString += "\r\n";

                i++;
            }

            CheatsCode = formatedString;
            return;
        }


        /// <summary>
        /// Formatte une ligne
        /// </summary>
        /// <param name="maxLength"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        private string FormatLine(int maxLength, string line)
        {
            // Calcul des fourchettes
            /*uint minSize = maxLength - maxLength * 10 / 100;
            uint maxSize = maxLength + maxLength * 10 / 100;*/
            line = line.Trim();
            string[] words = line.Split(' ');
            string formatedLine = string.Empty;
            uint lineSize = 0;
            //  int j = 1;

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
                if (lineSize + word.Length >= maxLength) // -1 à cause de l'espace nécessaire pour finir la phrase
                {
                    formatedLine += "\r\n";
                    lineSize = 0;
                }

                formatedLine += $"{word} ";

                /*   // On ajoute un espace sauf pour le dernier mot
                   if (j < words.Length)
                   {
                       formatedLine += " ";
                   }
                   else
                   {
                       // formatedLine += "----";
                       // Debug.WriteLine($"Last Word {word}");
                   }*/

                lineSize += (uint)word.Length;
                // j++;
                //i += word.Length + 1; // On calcule 
            }

            return formatedLine;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            FormatActive = false;
        }

        private void Active_Checked(object sender, RoutedEventArgs e)
        {
            FormatActive = true;
            _CheatCode = limit(_CheatCode);
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CheatsCode)));
        }
    }
}
