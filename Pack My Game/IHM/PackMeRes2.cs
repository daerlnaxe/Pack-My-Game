
using DlnxLocalTransfert;
using Pack_My_Game.Container;
using Pack_My_Game.Enum;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Pack_My_Game.IHM
{
    public partial class PackMeRes2 : Form
    {        
        #region Destination
        private string _Root
        {
            get; set;
        }


        /// <summary>
        /// Path of roms
        /// </summary>
        public string RomPath
        {
            get;
            set;
        }

        /// <summary>
        /// Path of manuals
        /// </summary>
        public string ManualPath
        {
            get;
            set;
        }

        /// <summary>
        /// Path of videos
        /// </summary>
        public string VideoPath
        {
            get;
            set;
        }

        /// <summary>
        /// Path of musics
        /// </summary>
        public string MusicPath
        {
            get;
            set;
        }

        /// <summary>
        /// Path of cheats
        /// </summary>
        public string CheatPath
        {
            get;
            set;
        }
        #endregion

        #region Source
        /// <summary>
        /// Source des cheats
        /// </summary>
        public string SourceCheatPath
        {
            get;
            set;
        }

        /// <summary>
        /// Source des manuels
        /// </summary>
        internal PlatformFolder SourceManuelPath
        {
            get;
            set;
        }

        /// <summary>
        /// Source des musiques
        /// </summary>
        internal PlatformFolder SourceMusicPath
        {
            get;
            set;
        }

        /// <summary>
        /// Source des roms
        /// </summary>
        public string SourceRomPath
        {
            get;
            set;
        }

        /// <summary>
        /// Source des musiques
        /// </summary>
        internal PlatformFolder SourceVideoPath
        {
            get;
            set;
        }
        #endregion

        #region Loads

        /// <summary>
        /// Charge les Applications
        /// </summary>
        private void LoadApplis()
        {
            lisbApp.Items.Clear();
            

            string[] applications = Directory.GetFiles(RomPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string p in applications)
            {
                lisbApp.Items.Add(Path.GetFileName(p));
            }
        }

        /// <summary>
        /// Charger les Cheats
        /// </summary>
        private void LoadCheats()
        {
            lisbCheats.Items.Clear();
            string[] cheats = Directory.GetFiles(CheatPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string c in cheats)
            {
                lisbCheats.Items.Add(Path.GetFileName(c));
            }
        }

        /// <summary>
        /// Charger les manuels
        /// </summary>
        /// <param name="manuals"></param>
        private void LoadManuals()
        {
            lisbManuels.Items.Clear();
            string[] manuals = Directory.GetFiles(ManualPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string m in manuals)
            {
                lisbManuels.Items.Add(Path.GetFileName(m));
            }
        }

        /// <summary>
        /// Load Musics
        /// </summary>
        public void LoadMusics()
        {
            lisbMusics.Items.Clear();
            string[] musics = Directory.GetFiles(MusicPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string v in musics)
            {
                lisbMusics.Items.Add(Path.GetFileName(v));
            }
        }

        /// <summary>
        /// Charger les Videos
        /// </summary>
        private void LoadVideos()
        {
            lisbVideos.Items.Clear();
            string[] videos = Directory.GetFiles(VideoPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string v in videos)
            {
                lisbVideos.Items.Add(Path.GetFileName(v));
            }
        }
        #endregion



        /// <summary>
        /// Nom du jeu
        /// </summary>
        public string GameName
        {
            get;
            internal set;
        }

        public void AddManual(string manual)
        {
            lisbManuels.Items.Add(manual);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        public PackMeRes2(string root)
        {
            _Root = root;
            CheatPath = Path.Combine(_Root, nameof(SubFolder.CheatCodes));
            ManualPath = Path.Combine(_Root, nameof(SubFolder.Manuals));
            MusicPath = Path.Combine(_Root, nameof(SubFolder.Musics));
            RomPath = Path.Combine(_Root, nameof(SubFolder.Roms));
            VideoPath = Path.Combine(_Root, nameof(SubFolder.Videos));


            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadDatas()
        {
            LoadApplis();
            LoadCheats();
            LoadManuals();
            LoadMusics();
            LoadVideos();
        }

        private void PackMeRes2_Load(object sender, EventArgs e)
        {

        }

        private void btReload_MouseClick(object sender, MouseEventArgs e)
        {
            LoadDatas();
        }


        #region Menus

        #endregion

        #region Cheat Menu Strip
        /// <summary>
        /// Mouson button pressed on ListBox Cheats
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void libCheats_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        cheatsMenuStrip.Show(Cursor.Position);//places the menu at the pointer position
                    }
                    break;
            }
        }

        /// <summary>
        /// MenuItem new Cheat File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newCheatFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            CheatForm cf = new CheatForm(GameName, CheatPath);
            cf.ShowDialog();
            this.Show();
            LoadCheats();
        }

        /// <summary>
        /// Open a file Cheat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openCheatFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheatForm.Open(Path.Combine(CheatPath, (string)lisbCheats.SelectedItem));
            //   cf._CheatFilePath = "dd";
        }

        /// <summary>
        /// MenuItem Copy Cheat File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyCheatFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyFile(SourceCheatPath, CheatPath);
            LoadCheats();
        }

        /// <summary>
        /// Delete Cheat File
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteCheatFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteFiles(lisbCheats.SelectedItems, CheatPath);
            LoadCheats();
        }

        /// <summary>
        /// Vérifier qu'un fichier est sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cheatsMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (lisbCheats.SelectedIndex == -1)
            {
                openCheatFileToolStripMenuItem.Enabled = false;
                deleteCheatFileToolStripMenuItem.Enabled = false;                
            }
            else
            {
                openCheatFileToolStripMenuItem.Enabled = true;
                deleteCheatFileToolStripMenuItem.Enabled = true;
            }
        }
        #endregion

        // ------------------------

        #region Manual Menu Strip
        /// <summary>
        /// Vérifier qu'un fichier est sélectionné
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>    
        private void manuelsMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (lisbManuels.SelectedIndex == -1)
            {
                openManuelToolStripMenuItem.Enabled = false;
                deleteManuelToolStripMenuItem.Enabled = false;

            }
            else
            {
                openManuelToolStripMenuItem.Enabled = true;
                deleteManuelToolStripMenuItem.Enabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lisbManuels_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        //places the menu at the pointer position
                        manuelsMenuStrip.Show(Cursor.Position);
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openManuelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(ManualPath, (string)lisbManuels.SelectedItems[0]);
            if (File.Exists(path))
            {
                Process.Start(path);
            }
        }

        /// <summary>
        /// Copie de manuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyManuelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string defaultPath = string.Empty;

            if (SourceManuelPath != null)
                defaultPath = SourceManuelPath.FolderPath;

            CopyFile(defaultPath, ManualPath);

            LoadManuals();
        }

        /// <summary>
        /// Effacer des manuels
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteManuelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteFiles(lisbManuels.SelectedItems, ManualPath);
            LoadManuals();
        }
        #endregion

        // ------------------------

        #region Music Menu Strip
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void musicsMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (lisbMusics.SelectedIndex == -1)
            {
                openMusicToolStripMenuItem.Enabled = false;
                deleteMusicToolStripMenuItem.Enabled = false;

            }
            else 
            { 
                openMusicToolStripMenuItem.Enabled = true;
                deleteMusicToolStripMenuItem.Enabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lisbMusics_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        //places the menu at the pointer position
                        musicsMenuStrip.Show(Cursor.Position);
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openMusicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(MusicPath, (string)lisbMusics.SelectedItems[0]);
            if (File.Exists(path))
            {
                Process.Start(path);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyMusicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string defaultPath = string.Empty;

            if (SourceMusicPath != null)
                defaultPath = SourceMusicPath.FolderPath;

            CopyFile(defaultPath, MusicPath);
            LoadMusics();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteMusicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteFiles(lisbMusics.SelectedItems, MusicPath);
            LoadMusics();
        }
        #endregion

        // ------------------------

        #region Roms Menu Strip     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lisbbApp_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        //places the menu at the pointer position
                        //romsMenuStrip.Show(this, new Point(e.X, e.Y));
                        romsMenuStrip.Show(Cursor.Position);
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void romsMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (lisbApp.Items.Count < 1)
            if (lisbApp.SelectedIndex == -1)
                deleteRomToolStripMenuItem.Enabled = false;
            else
                deleteRomToolStripMenuItem.Enabled = true;                
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyRomToolStripMenuItem_Click(object sender, EventArgs e)
        {   
            CopyFile(SourceRomPath, RomPath);
            LoadApplis();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteFiles(lisbApp.SelectedItems, RomPath);
            LoadApplis();
        }
        #endregion

        // ------------------------

        #region Videos Menu Strip

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void videosMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (lisbVideos.SelectedIndex == -1)
            {
                openVideoToolStripMenuItem.Enabled = false;
                deleteVideoToolStripMenuItem.Enabled = false;
            }
            else
            {
                openVideoToolStripMenuItem.Enabled = true;
                deleteVideoToolStripMenuItem.Enabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lisbVideos_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        //places the menu at the pointer position
                        videosMenuStrip.Show(Cursor.Position);
                    }
                    break;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(VideoPath, (string)lisbVideos.SelectedItems[0]);
            if (File.Exists(path))
            {
                Process.Start(path);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string defaultPath = string.Empty;

            if (SourceVideoPath != null)
                defaultPath = SourceVideoPath.FolderPath;

            CopyFile(defaultPath, VideoPath);
            LoadVideos();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteFiles(lisbVideos.SelectedItems, VideoPath);           
            LoadVideos();
        }

        #endregion


        #region Opération communes

        /// <summary>
        /// Fonction de copie des fichiers
        /// </summary>
        /// <param name="sourceDirectory">Répertoire source</param>
        /// <param name="targetDirectory">Répertoire cible</param>
        private void CopyFile(string sourceDirectory, string targetDirectory)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Multiselect = true
            };

            // Vérification que le dossier cible est définit
            if (!string.IsNullOrEmpty(sourceDirectory))
                ofd.InitialDirectory = sourceDirectory;

            string[] files;
            if (ofd.ShowDialog() == DialogResult.OK)
                files = ofd.FileNames;
            ofd.Dispose();

            foreach (var f in ofd.FileNames)
            {
                string fileName = Path.GetFileName(f);
                string target = Path.Combine(targetDirectory, fileName);

                // Cas où le fichier existe mais qu'on ne veut pas écrire par dessus
                if (File.Exists(target) &&
                    MessageBox.Show($"Target Exist: {fileName}.\r\nDo you want to overwrite ?", "Overwrite ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    continue;

                // Copie
                File.Copy(f, target, true);
                MessageBox.Show($"File copied: {fileName}", "Information");
            }
        }

        /// <summary>
        /// Fonction d'effacement des fichiers
        /// </summary>
        /// <param name="selectedItems"></param>
        /// <param name="basePath"></param>
        private void DeleteFiles(ListBox.SelectedObjectCollection selectedItems, string basePath)
        {
            foreach (string file in selectedItems)
            {
                if (OPFiles.SendTo_Recycle(file, Path.Combine(basePath, file)))
                    MessageBox.Show($"File: '{file}' deleted.");
                else
                    MessageBox.Show($"Error, File: '{file}' not deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        /// <summary>
        /// Ouvre le dossier du jeu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", _Root);
        }
    }
}
