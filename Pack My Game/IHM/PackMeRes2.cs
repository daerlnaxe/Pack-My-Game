using Pack_My_Game.Container;
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
using System.Windows.Media;

namespace Pack_My_Game.IHM
{
    public partial class PackMeRes2 : Form
    {
        #region Destination
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



        /// <summary>
        /// Nom du jeu
        /// </summary>
        public string GameName
        {
            get;
            internal set;
        }




        /// <summary>
        /// Charge les Applications
        /// </summary>
        private void LoadApplis()
        {
            lbApp.Items.Clear();
            string[] applications = Directory.GetFiles(RomPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string p in applications)
            {
                lbApp.Items.Add(Path.GetFileName(p));
            }
        }

        /// <summary>
        /// Charger les Cheats
        /// </summary>
        private void LoadCheats()
        {
            lbCheats.Items.Clear();
            string[] cheats = Directory.GetFiles(CheatPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string c in cheats)
            {
                lbCheats.Items.Add(Path.GetFileName(c));
            }
        }

        /// <summary>
        /// Charger les manuels
        /// </summary>
        /// <param name="manuals"></param>
        private void LoadManuals()
        {
            lbManuels.Items.Clear();
            string[] manuals = Directory.GetFiles(ManualPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string m in manuals)
            {
                lbManuels.Items.Add(Path.GetFileName(m));
            }
        }

        /// <summary>
        /// Load Musics
        /// </summary>
        public void LoadMusics()
        {
            lbMusics.Items.Clear();
            string[] musics = Directory.GetFiles(MusicPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string v in musics)
            {
                lbMusics.Items.Add(Path.GetFileName(v));
            }
        }

        /// <summary>
        /// Charger les Videos
        /// </summary>
        private void LoadVideos()
        {
            lbVideos.Items.Clear();
            string[] videos = Directory.GetFiles(VideoPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string v in videos)
            {
                lbVideos.Items.Add(Path.GetFileName(v));
            }
        }



        public void AddManual(string manual)
        {
            lbManuels.Items.Add(manual);
        }




        public PackMeRes2()
        {
            InitializeComponent();
        }

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

        private void btCreateCheat_Click(object sender, EventArgs e)
        {
            //CheatForm.Showdialog(GameName);
            this.Hide();
            CheatForm cf = new CheatForm(GameName, CheatPath);
            cf.ShowDialog();
            this.Show();
            LoadCheats();
        }


        #region copies
        /// <summary>
        /// Copie de cheats
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCopCheat_Click(object sender, EventArgs e)
        {
            CopyFile(SourceCheatPath, CheatPath);
            LoadCheats();
        }

        /// <summary>
        /// Copie de roms
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCopRom_Click(object sender, EventArgs e)
        {
            CopyFile(SourceRomPath, RomPath);
            LoadApplis();
        }

        /// <summary>
        /// Copie de manuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCopMan_Click(object sender, EventArgs e)
        {
            string defaultPath = string.Empty;

            if (SourceManuelPath != null)
                defaultPath = SourceManuelPath.FolderPath;

            CopyFile(defaultPath, ManualPath);

            LoadManuals();
        }

        private void btCopMusik_Click(object sender, EventArgs e)
        {
            string defaultPath = string.Empty;

            if (SourceMusicPath != null)
                defaultPath = SourceMusicPath.FolderPath;

            CopyFile(defaultPath, MusicPath);
            LoadMusics();
        }


        /// <summary>
        /// Copie de video
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btCopVid_Click(object sender, EventArgs e)
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

        #endregion
    }
}
