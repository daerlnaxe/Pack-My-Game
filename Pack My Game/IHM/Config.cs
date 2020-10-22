using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pack_My_Game.Properties;

namespace Pack_My_Game.IHM
{

    /*  Translation / Traduction: Add new culture here
     *      
     *  
     
         
    */
    public partial class Config : Form
    {
        //public static List<string> Languages { get; private set; } = new List<string>();
        public static Dictionary<string, string> Languages;
        public bool LanguageModified { get; private set; }
        public CultureInfo _PreviousCulture;

        public Config()
        {
            InitializeComponent();

        }

        private void Config_Load(object sender, EventArgs e)
        {
            _PreviousCulture = Thread.CurrentThread.CurrentUICulture;

            LoadUI();
        }

        private void LoadUI()
        {

            Languages = new Dictionary<string, string>();
            Languages.Add("fr-FR", "Français");
            Languages.Add("en-US", "English");

            //listLang.Enabled = false;
            listLang.DataSource = new BindingSource(Languages, null);
            listLang.DisplayMember = "Value";
            listLang.ValueMember = "Key";
            listLang.SelectedValue = Settings.Default.Language;
            //listLang.SelectedValue = _PreviousCulture.TextInfo.CultureName;
            // Paths
            this.tbLaunchBoxPath.Text = Settings.Default.LBPath;
            this.tbCheatCodes.Text = Settings.Default.CCodesPath;

            //zip
            this.trackZipCompLvl.Position = Settings.Default.cZipCompLvl;

            // 7-Zip
            this.track7ZipCompLvl.Position = Settings.Default.c7zCompLvl;

            // Progress
            cbInfos.Checked = Settings.Default.opInfos;
            cbOBGame.Checked = Settings.Default.opOBGame;
            cbEBGame.Checked = Settings.Default.opEBGame;
            cbTreeV.Checked = Settings.Default.opTreeV;

            cbClone.Checked = Settings.Default.opClones;
            cbCCC.Checked = Settings.Default.opCheatCodes;

            cbZip.Checked = Settings.Default.opZip;
            cb7_Zip.Checked = Settings.Default.op7_Zip;

            cbLogFile.Checked = Settings.Default.opLogFile;
            cbLogWindow.Checked = Settings.Default.opLogWindow;

        }

        #region Paths....
        // Todo replace by custom window
        // LaunchBox Path
        private void btChooseLBPath_Click(object sender, EventArgs e)
        {
            tbLaunchBoxPath.ReadOnly = false;

            var cpWindow = new FolderBrowserDialog();
            cpWindow.Description = Lang.Choose_LBPath;
            cpWindow.ShowNewFolderButton = false;
            cpWindow.SelectedPath = Settings.Default.LastKPath;

            if (cpWindow.ShowDialog() == DialogResult.OK && Verif_LaunchBoxPath(cpWindow.SelectedPath))
            {
                this.tbLaunchBoxPath.Text = cpWindow.SelectedPath;
                Verif_LaunchBoxPath(tbLaunchBoxPath.Text);

                Settings.Default.LastKSystem = cpWindow.SelectedPath;
            }

        }

        /// <summary>
        /// Perte de focus => validation sur la textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LBPath_Validating(object sender, CancelEventArgs e)
        {
            Verif_LaunchBoxPath(tbLaunchBoxPath.Text);
        }

        /// <summary>
        /// Vérification pour LaunchBox path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool Verif_LaunchBoxPath(string path)
        {
            string xmlLMachines = Path.Combine(path, Settings.Default.fPlatforms);

            // Verification if there is the right file inside
            if (xmlLMachines != "" && !File.Exists(xmlLMachines))
            {
                MessageBox.Show($"{Lang.Invalid_Path}: 'Platforms.xml'", Lang.Alert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                tbLaunchBoxPath.Text = Settings.Default.LBPath;
                return false;
            }
            return true;
        }

        //ccodes
        private void btChooseCCodes_Click(object sender, EventArgs e)
        {
            tbCheatCodes.ReadOnly = false;

            var cpWindow = new FolderBrowserDialog();
            cpWindow.Description = Lang.Choose_CCodesPath;
            cpWindow.ShowNewFolderButton = false;
            cpWindow.SelectedPath = Settings.Default.LastKPath;

            if (cpWindow.ShowDialog() == DialogResult.OK)
            {
                this.tbCheatCodes.Text = cpWindow.SelectedPath;

                Settings.Default.LastKPath = cpWindow.SelectedPath;
            }
        }

        private void CCodes_Validating(object sender, CancelEventArgs e)
        {
            Verif_Path(tbCheatCodes.Text, tbCheatCodes, Settings.Default.CCodesPath);
        }
        /// <summary>
        /// Generic function to verif folder
        /// </summary>
        /// <param name="path"></param>
        /// <param name="tb"></param>
        /// <param name="property"></param>
        private void Verif_Path(string path, TextBox tb, string property)
        {
            if (tb.Text == "" || !Directory.Exists(tb.Text))
            {
                MessageBox.Show($"{Lang.Invalid_Path} {tb.Name.Substring(2)}");
                tb.Text = property;
                //                
            }
        }

        #endregion


        private void btCancel_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentUICulture = _PreviousCulture;
            Settings.Default.Language = _PreviousCulture.TextInfo.CultureName;
            this.Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            // Verification empty string
            if (string.IsNullOrEmpty(tbLaunchBoxPath.Text))
            {
                MessageBox.Show($"{Lang.Invalid_Path}: {Lang.Empty}", Lang.Alert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string xmlLMachines = Path.Combine(this.tbLaunchBoxPath.Text, Settings.Default.fPlatforms);

            // Verification if there is the right file inside
            //if (!File.Exists(xmlLMachines))
            //{
            //    MessageBox.Show($"{Lang.Invalid_Path}:  'Platforms.xml'", Lang.Alert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    Console.WriteLine("bizarre " + Settings.Default.LBPath);
            //    return;
            //}

            // General            


            // Paths
            Settings.Default.LBPath = tbLaunchBoxPath.Text;
            Settings.Default.CCodesPath = tbCheatCodes.Text;

            // Zip
            Settings.Default.cZipCompLvl = trackZipCompLvl.Position;

            // 7-Zip
            Settings.Default.c7zCompLvl = this.track7ZipCompLvl.Position;

            // Progress
            Settings.Default.opInfos = cbInfos.Checked;
            Settings.Default.opOBGame = cbOBGame.Checked;
            Settings.Default.opEBGame = cbEBGame.Checked;
            Settings.Default.opTreeV = cbTreeV.Checked;
            Settings.Default.opClones = cbClone.Checked;
            Settings.Default.opCheatCodes = cbCCC.Checked;

            Settings.Default.opZip = cbZip.Checked;
            Settings.Default.op7_Zip = cb7_Zip.Checked;

            Settings.Default.opLogFile = cbLogFile.Checked;
            Settings.Default.opLogWindow = cbLogWindow.Checked;

            // Save
            Settings.Default.Save();
            this.Close();
        }

        private void listLang_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // get selected Language
            KeyValuePair<string, string> selection = (KeyValuePair<string, string>)listLang.SelectedItem;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(selection.Key);

            Settings.Default.Language = selection.Key;

            this.Controls.Clear();
            this.InitializeComponent();
            LoadUI();
        }


    }
}