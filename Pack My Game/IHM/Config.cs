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

        public Config()
        {
            InitializeComponent();

        }

        private void Config_Load(object sender, EventArgs e)
        {
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
            // listLang.SelectedValue = Properties.Settings.Default.Language;
            listLang.SelectedValue = Thread.CurrentThread.CurrentUICulture.TextInfo.CultureName;

            // General
            cbClone.Checked = Properties.Settings.Default.cloneActive;

            // Paths
            this.tbLaunchBoxPath.Text = Properties.Settings.Default.LBPath;
            this.tbCheatCodes.Text = Properties.Settings.Default.CCodesPath;

            //zip
            this.trackZipCompLvl.Position = Properties.Settings.Default.cZipCompLvl;
            this.cbZip.Checked = Properties.Settings.Default.cZipActive;

            // 7-Zip
            this.track7ZipCompLvl.Position = Properties.Settings.Default.c7zCompLvl;
            this.cb7Zip.Checked = Properties.Settings.Default.c7zActive;

        }

        #region Paths....
        // Todo replace by custom window
        private void btChoosePath_Click(object sender, EventArgs e)
        {
            tbLaunchBoxPath.ReadOnly = false;


            var cpWindow = new FolderBrowserDialog();
            cpWindow.Description = Lang.Choose_LBPath;
            cpWindow.ShowNewFolderButton = false;
            cpWindow.SelectedPath = Properties.Settings.Default.LastKPath;

            if (cpWindow.ShowDialog() == DialogResult.OK)
            {
                this.tbLaunchBoxPath.Text = cpWindow.SelectedPath;
                Properties.Settings.Default.LBPath = cpWindow.SelectedPath;
                Properties.Settings.Default.LastKPath = cpWindow.SelectedPath;
            }

        }

        private void btChooseCCodes_Click(object sender, EventArgs e)
        {
            tbCheatCodes.ReadOnly = false;


            var cpWindow = new FolderBrowserDialog();
            cpWindow.Description = Lang.Choose_CCodesPath;
            cpWindow.ShowNewFolderButton = false;
            cpWindow.SelectedPath = Properties.Settings.Default.LastKPath;

            if (cpWindow.ShowDialog() == DialogResult.OK)
            {
                this.tbCheatCodes.Text = cpWindow.SelectedPath;
                Properties.Settings.Default.CCodesPath = cpWindow.SelectedPath;
                Properties.Settings.Default.LastKPath = cpWindow.SelectedPath;
            }
        }
        #endregion


        private void btCancel_Click(object sender, EventArgs e)
        {
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

            string xmlLMachines = Path.Combine(this.tbLaunchBoxPath.Text, Properties.Settings.Default.fPlatforms);

            // Verification if there is the right file inside
            if (!File.Exists(xmlLMachines))
            {
                MessageBox.Show($"{Lang.Invalid_Path}:  'Platforms.xml'", Lang.Alert, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // General
            Properties.Settings.Default.cloneActive = cbClone.Checked;

            // Zip
            Properties.Settings.Default.cZipCompLvl = trackZipCompLvl.Position;
            Properties.Settings.Default.cZipActive = cbZip.Checked;

            // 7-Zip
            Properties.Settings.Default.c7zCompLvl = this.track7ZipCompLvl.Position;
            Properties.Settings.Default.c7zActive = this.cb7Zip.Checked;

            // Save
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void listLang_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listLang_SelectionChangeCommitted(object sender, EventArgs e)
        {
            // get selected Language
            KeyValuePair<string, string> selection = (KeyValuePair<string, string>)listLang.SelectedItem;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(selection.Key);

            Properties.Settings.Default.Language = selection.Key;

            this.Controls.Clear();
            this.InitializeComponent();
            LoadUI();
        }

        private void Path_Validating(object sender, CancelEventArgs e)
        {
            TextBox cuTextBox = (TextBox)sender;

            if (cuTextBox.Text == "")
            {
                MessageBox.Show(string.Format("Empty field {0 }", cuTextBox.Name.Substring(3)));
                e.Cancel = true;
            }
            else

            {
                e.Cancel = false;
            }


        }
    }
}