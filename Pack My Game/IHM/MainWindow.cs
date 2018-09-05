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
using Microsoft.VisualBasic.FileIO;
using Pack_My_Game.Container;
using Pack_My_Game.IHM;
using Pack_My_Game.Pack;
using Pack_My_Game.Properties;
using Pack_My_Game.XML;

/*
 *
 *  Fonctions utiles:
 *      - ChooseSystem: Get the list of machines
 *      - 
 *  Fonctionne avec BackgroundWorker, pas besoin de plus pour le moment
 */

namespace Pack_My_Game
{
    public partial class MainWindow : Form
    {
        string _XmlFMachines;
        string _XmlFPlatform;
        string _lbPath;
        string _OutPPath;
        ResourceManager _RT;

        Dictionary<string, ShortGame> _GameList;
        int _GameSortColumn = -1;

        XML_Functions _xfGames;
        IHM.SplashScreen sScreen;


        public MainWindow()
        {
            // Change language according to user preference (first time: null)
            if (!string.IsNullOrEmpty(Settings.Default.Language))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language);
            }

            // Memo
            //_RT = new ResourceManager("Pack_My_Game.MainWindow", typeof(MainWindow).Assembly);            

            InitializeComponent();


            LoadUI();
        }

        /// <summary>
        /// Initialize on load or after a language change
        /// </summary>
        private void LoadUI()
        {

            /* Compression_Progress cp = new Compression_Progress();
             cp.Show();*/

            //.Except(CultureInfo.GetCultures(CultureTypes.SpecificCultures));
            _lbPath = Settings.Default.LBPath;
            _OutPPath = Settings.Default.OutPPath;

            // Peuplement des headers
            var properties = typeof(ShortGame).GetProperties();
            lvGames.Columns.Clear();

            foreach (var property in properties)
            {
                var col = new ColumnHeader();
                if (property.Name == "ID") continue;
                col.Text = property.Name;
                col.Name = property.Name;

                lvGames.Columns.Add(col);
            }

            //lvGames.Columns[0].DisplayIndex = lvGames.Columns.Count-1;

            // Chargement des paramètres - Load settings
            // Launchbox Path
            if (!string.IsNullOrEmpty(_lbPath))
            {
                this.lLaunchBoxPath.Text = _lbPath;
                this.lLaunchBoxPath.AutoSize = true;
                Console.WriteLine($"Init: LaunchBox folder = '{_lbPath}'");

                this.groupOP.Visible = true;
            }

            if (!string.IsNullOrEmpty(_OutPPath))
            {
                this.tbOutPPath.Text = _OutPPath;

                Console.WriteLine($"Init: Output folder = '{_OutPPath}'");
            }

            if (!string.IsNullOrEmpty(_OutPPath) && !string.IsNullOrEmpty(_lbPath))
            {
                FillListMachines();

            }
        }



        #region controls
        /// <summary>
        /// To Choose a folder where Launchbox is installed
        /// </summary>
        /// <remarks>Update User Settings</remarks>
        private void btChoosePath_Click(object sender, EventArgs e)
        {


        }

        /// <summary>
        /// To Choose a folder where Launchbox is installed
        /// </summary>
        /// <remarks>Update User Settings</remarks>
        private void btChooseOutPut_Click(object sender, EventArgs e)
        {
            var cpWindow = new FolderBrowserDialog();
            string tmpPath;

            cpWindow.Description = Lang.OutputBrowserDescription;
            cpWindow.SelectedPath = Properties.Settings.Default.LastKPath;

            if (cpWindow.ShowDialog() != DialogResult.OK) return;

            // Memorisation lastKnowPath
            Properties.Settings.Default.LastKPath = tmpPath = cpWindow.SelectedPath;

            // Test
            if (!Directory.Exists(tmpPath)) return;

            // Assignation & Update
            this.tbOutPPath.Text = _OutPPath = tmpPath;
            Properties.Settings.Default.OutPPath = tmpPath;
            Properties.Settings.Default.Save();

            FillListMachines();

        }

        /// <summary>
        /// Boite de selection des machines - ComboBox For Machines
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lboxMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lboxMachines.SelectedIndex == 0) return;

            string machine = lboxMachines.SelectedItem.ToString();
            Console.WriteLine($"\t Machine: {machine} selected");

            _XmlFPlatform = Path.Combine(_lbPath, Properties.Settings.Default.dPlatforms, $"{machine}.xml");

            Console.WriteLine($"\tTarget: {_XmlFPlatform}");

            FillListGames();

        }

        /// <summary>
        /// Boite de selection des jeux - Combobox for Games
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>



        #endregion

        #region menu
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IHM.Config cfg = new IHM.Config();
            var res = cfg.ShowDialog();


            if (res == DialogResult.OK)
            {
                this.lLaunchBoxPath.Text = _lbPath = Properties.Settings.Default.LBPath;

                // Change language
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
                this.Controls.Clear();
                this.InitializeComponent();
                LoadUI();
            }
        }


        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var helpWin = new IHM.Help();
            helpWin.ShowDialog();
        }
        #endregion

        #region Machines

        /// <summary>
        /// Récupère la liste des machines - Get the list of machines
        /// </summary>
        /// todo: put a possibility to get file manually
        private void FillListMachines()
        {
            // Show new controls
            lbSystems.Visible = lboxMachines.Visible = true;
            lbGames.Visible = lvGames.Visible = true;

            string xmlLMachines = Path.Combine(_lbPath, Properties.Settings.Default.fPlatforms);
            Console.WriteLine(xmlLMachines);
            XML_Functions xf = new XML_Functions();

            if (xf.ReadFile(xmlLMachines))
            {
                List<string> lm = new List<string>() { Lang.SelectASystem };
                xf.ListMachine(lm);

                lboxMachines.DataSource = lm;
            }
            else
            {
                Properties.Settings.Default.LBPath = null;
                Properties.Settings.Default.Save();
                _lbPath = null;
                // LaunchBoxPath.Text = null;
            }

        }
        #endregion

        #region Games
        /// <summary>
        /// Récupère la liste des jeux - Get the list of games
        /// </summary>
        private void FillListGames()
        {
            _xfGames = new XML_Functions();

            lvGames.Items.Clear();

            if (_xfGames.ReadFile(_XmlFPlatform))
            {
                _GameList = _xfGames.ListGames();

                // var lgSorted = lg.OrderBy(x => x.FileName).ToList();

                //foreach (ShortGame sg in lgSorted)
                //{
                //    ListViewItem lvi = new ListViewItem(sg.Title);

                //    lvi.Tag = sg.ID;
                //    lvi.SubItems.Add(sg.Region);
                //    lvi.SubItems.Add(sg.FileName);

                //    lvGames.Items.Add(lvi);
                //}

                foreach (var sGame in _GameList.Values.OrderBy(x => x.Title))
                {
                    ListViewItem lvi = new ListViewItem(sGame.Title);

                    lvi.Tag = sGame;
                    lvi.SubItems.Add(sGame.Region);
                    lvi.SubItems.Add(sGame.FileName);

                    lvGames.Items.Add(lvi);
                }
            }
            else
            {

            }

            lvGames.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            // lvGames.Columns[1].Width = 0;



        }

        private void lvGames_DoubleClick(object sender, EventArgs e)
        {

        }

        private void Proceed_Click(object sender, EventArgs e)
        {
            List<ListViewItem> gamesSelected = new List<ListViewItem>();
            for (int i = 0; i < lvGames.CheckedItems.Count; i++) gamesSelected.Add(lvGames.CheckedItems[i]);

            PackMeLauncher(gamesSelected);
        }

        /// <summary>
        /// 
        /// </summary>
        private void PackMeLauncher(List<ListViewItem> lGames)
        {
            /*
            // Work Async

            */
            //Get ID
            // 20-08-2018 PackMe.ID = lvGames.SelectedItems[0].Text;

            //   sScreen = new IHM.SplashScreen();

            //
            //
            //sScreen.ShowDialog();

            // remove from the litstview to help 
            // Todo Remove the clone too
            /*
             * res = MessageBox.Show("Remove this Game from the listView ? (Only Graphical)", "Remove this Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res != DialogResult.Yes) return;
            */
            // lvGames.
            string platform = lboxMachines.SelectedItem.ToString();

            foreach (ListViewItem item in lGames)
            {
                ShortGame zeGame = (ShortGame)item.Tag;
                var res = MessageBox.Show($"{Lang.MB_Pack_Question}: '{zeGame.Title}' ?", "Pack or Not Pack ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res != DialogResult.Yes) continue;

                Console.WriteLine($"[Main] PackMe for '{zeGame.Title}' | '{zeGame.ID}'");

                PackMe pm = new PackMe(zeGame.ID, platform);
                this.Hide();
                int state = pm.Initialize(_XmlFPlatform);


                if (state == 0 && pm.Run())
                {
                    if (MessageBox.Show( $"{Lang.Remove_Word} '{zeGame.Title}' {Lang.From_List} ?",Lang.Remove_Game, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Console.WriteLine($"Remove: '{zeGame.Title}' from list");
                        _GameList.Remove(zeGame.ID);
                        lvGames.Items.Remove(item);
                    }
                }
                pm = null;
                this.butProceed.Visible = false;
                this.Show();
            }


            #region poubelle
            ////////int indexFullname = -1;
            ////////for (int i = 0; i < lvGames.Columns.Count; i++)
            ////////{
            ////////    string colName = lvGames.Columns[i].Name;
            ////////    Console.WriteLine(colName);
            ////////    switch (colName)
            ////////    {
            ////////        case "FileName":
            ////////            indexFullname = i;
            ////////            break;

            ////////    }
            ////////}

            ////// string platform = lboxMachines.SelectedItem.ToString();

            ////// for (int i = 0; i < lvGames.SelectedItems.Count; i++)
            ////// {


            ////// string fullname = lvGames.SelectedItems[i].SubItems[indexFullname].Text.Split('.')[0]; //lève toute extension


            ////// var res = MessageBox.Show($"Pack This Game '{fullname}' ?", "Pack or Not Pack ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            ////// if (res != DialogResult.Yes) continue;



            //////// var objet = lvGames.SelectedItems[i];
            //////// string id = objet.Text;

            ////// Console.WriteLine($"[Main] PackMe for {id}");


            ////// PackMe pm = new PackMe(id, platform);
            ////// this.Hide();
            ////// int state = pm.Initialize(_XmlFPlatform);

            //////if (state == 0 && pm.Run())
            //////{

            //////    if (MessageBox.Show("Remove this Game from the listView ? (Only Graphical)", "Remove this Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //////    {
            //////        Console.WriteLine($"Remove: {fullname} from list");
            //////        lvGames.Items.Remove(objet);
            //////    }
            //////}
            //////pm = null;
            //////this.Show();
            ////////}
            #endregion poubelle
        }


        /// <summary>
        /// Reorder Game columns
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameColumn_Click(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != _GameSortColumn)
            {
                _GameSortColumn = e.Column;
                lvGames.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (lvGames.Sorting == SortOrder.Ascending) lvGames.Sorting = SortOrder.Descending;
                else lvGames.Sorting = SortOrder.Ascending;
            }

            lvGames.Sort();
            this.lvGames.ListViewItemSorter = new LvItemComparer(e.Column, lvGames.Sorting);
        }
        #endregion

        #region Async
        /*
        /// <summary>
        /// Fonction de lancement du pack en background
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BwWork(object sender, DoWorkEventArgs e)
        {
            PackMe.Initialize(_XmlFPlatform);
            Console.WriteLine("Phase2");
            PackMe.Run(PackMe.ID);
        }

        /// <summary>
        /// Stoppe le splashScreen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void BWRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //all background work has complete and we are going to close the waiting message
            sScreen.Close();
        }
        */
        #endregion



        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.Show();
        }

        private void creditsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Credits cr = new Credits();
            cr.Show();
        }

        private void lvGames_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            butProceed.Visible = true;

        }

        private void lvGames_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvGames_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) return;

            List<ListViewItem> gamesSelected = new List<ListViewItem>();
            for (int i = 0; i < lvGames.SelectedItems.Count; i++) gamesSelected.Add(lvGames.SelectedItems[i]);

            PackMeLauncher(gamesSelected);
        }



        #region Contextual menu
        /// <summary>
        /// Show contextual menu - Affiche le menu contextuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvGames_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) return;
            ctxLVGames.Show(lvGames, e.Location);

        }

        /// <summary>
        /// Remove  game from the list by menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miRemoveIt_Click(object sender, EventArgs e)
        {
            var item  =lvGames.SelectedItems[0];
            _GameList.Remove( ((ShortGame)item.Tag).ID );
            lvGames.Items.Remove(item);
        }


        #endregion
    }

}
