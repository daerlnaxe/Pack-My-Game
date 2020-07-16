namespace Pack_My_Game
{
    partial class MainWindow
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.menuTop = new System.Windows.Forms.MenuStrip();
            this.configToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.creditsTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutTSMI = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.creditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lvGames = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Version = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lboxMachines = new System.Windows.Forms.ComboBox();
            this.lbSystems = new System.Windows.Forms.Label();
            this.lLaunchBoxPath = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbGames = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btSelectWFolder = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbOutPPath = new System.Windows.Forms.TextBox();
            this.butProceed = new System.Windows.Forms.Button();
            this.Infos = new System.Windows.Forms.GroupBox();
            this.groupOP = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ctxLVGames = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miPackThis = new System.Windows.Forms.ToolStripMenuItem();
            this.miRemoveIt = new System.Windows.Forms.ToolStripMenuItem();
            this.miInfoXml = new System.Windows.Forms.ToolStripMenuItem();
            this.imBackupXMLGame = new System.Windows.Forms.ToolStripMenuItem();
            this.mi7ZipIt = new System.Windows.Forms.ToolStripMenuItem();
            this.miZipIt = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuTop.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.Infos.SuspendLayout();
            this.groupOP.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.ctxLVGames.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuTop
            // 
            this.menuTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem1,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuTop, "menuTop");
            this.menuTop.Name = "menuTop";
            // 
            // configToolStripMenuItem1
            // 
            this.configToolStripMenuItem1.Name = "configToolStripMenuItem1";
            resources.ApplyResources(this.configToolStripMenuItem1, "configToolStripMenuItem1");
            this.configToolStripMenuItem1.Click += new System.EventHandler(this.settingsTSMI_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpTSMI,
            this.creditsTSMI,
            this.aboutTSMI});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // helpTSMI
            // 
            this.helpTSMI.Name = "helpTSMI";
            resources.ApplyResources(this.helpTSMI, "helpTSMI");
            this.helpTSMI.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // creditsTSMI
            // 
            this.creditsTSMI.Name = "creditsTSMI";
            resources.ApplyResources(this.creditsTSMI, "creditsTSMI");
            this.creditsTSMI.Click += new System.EventHandler(this.creditsToolStripMenuItem_Click);
            // 
            // aboutTSMI
            // 
            this.aboutTSMI.Name = "aboutTSMI";
            resources.ApplyResources(this.aboutTSMI, "aboutTSMI");
            this.aboutTSMI.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.testToolStripMenuItem});
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            resources.ApplyResources(this.configToolStripMenuItem, "configToolStripMenuItem");
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsTSMI_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            resources.ApplyResources(this.testToolStripMenuItem, "testToolStripMenuItem");
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.creditsToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            resources.ApplyResources(this.helpToolStripMenuItem1, "helpToolStripMenuItem1");
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // creditsToolStripMenuItem
            // 
            this.creditsToolStripMenuItem.Name = "creditsToolStripMenuItem";
            resources.ApplyResources(this.creditsToolStripMenuItem, "creditsToolStripMenuItem");
            this.creditsToolStripMenuItem.Click += new System.EventHandler(this.creditsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            resources.ApplyResources(this.aboutToolStripMenuItem1, "aboutToolStripMenuItem1");
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // lvGames
            // 
            this.lvGames.AutoArrange = false;
            this.lvGames.CheckBoxes = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lvGames, 5);
            resources.ApplyResources(this.lvGames, "lvGames");
            this.lvGames.FullRowSelect = true;
            this.lvGames.HideSelection = false;
            this.lvGames.MultiSelect = false;
            this.lvGames.Name = "lvGames";
            this.lvGames.UseCompatibleStateImageBehavior = false;
            this.lvGames.View = System.Windows.Forms.View.Details;
            this.lvGames.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.GameColumn_Click);
            this.lvGames.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvGames_ItemCheck);
            this.lvGames.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvGames_MouseClick);
            this.lvGames.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvGames_MouseDoubleClick);
            // 
            // ID
            // 
            resources.ApplyResources(this.ID, "ID");
            // 
            // Title
            // 
            resources.ApplyResources(this.Title, "Title");
            // 
            // Version
            // 
            resources.ApplyResources(this.Version, "Version");
            // 
            // lboxMachines
            // 
            this.lboxMachines.FormattingEnabled = true;
            resources.ApplyResources(this.lboxMachines, "lboxMachines");
            this.lboxMachines.Name = "lboxMachines";
            this.lboxMachines.SelectedIndexChanged += new System.EventHandler(this.lboxMachines_SelectedIndexChanged);
            // 
            // lbSystems
            // 
            resources.ApplyResources(this.lbSystems, "lbSystems");
            this.lbSystems.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbSystems.Name = "lbSystems";
            // 
            // lLaunchBoxPath
            // 
            resources.ApplyResources(this.lLaunchBoxPath, "lLaunchBoxPath");
            this.lLaunchBoxPath.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lLaunchBoxPath.Name = "lLaunchBoxPath";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Name = "label1";
            // 
            // lbGames
            // 
            resources.ApplyResources(this.lbGames, "lbGames");
            this.lbGames.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbGames.Name = "lbGames";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.btSelectWFolder, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lvGames, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbOutPPath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbSystems, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lboxMachines, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbGames, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.butProceed, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // btSelectWFolder
            // 
            resources.ApplyResources(this.btSelectWFolder, "btSelectWFolder");
            this.btSelectWFolder.Name = "btSelectWFolder";
            this.btSelectWFolder.UseVisualStyleBackColor = true;
            this.btSelectWFolder.Click += new System.EventHandler(this.btChooseOutPut_Click);
            // 
            // label4
            // 
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Name = "label4";
            // 
            // tbOutPPath
            // 
            resources.ApplyResources(this.tbOutPPath, "tbOutPPath");
            this.tbOutPPath.Name = "tbOutPPath";
            this.tbOutPPath.ReadOnly = true;
            this.toolTip1.SetToolTip(this.tbOutPPath, resources.GetString("tbOutPPath.ToolTip"));
            // 
            // butProceed
            // 
            resources.ApplyResources(this.butProceed, "butProceed");
            this.butProceed.Name = "butProceed";
            this.butProceed.UseVisualStyleBackColor = true;
            this.butProceed.Click += new System.EventHandler(this.Proceed_Click);
            // 
            // Infos
            // 
            this.Infos.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Infos.Controls.Add(this.lLaunchBoxPath);
            this.Infos.Controls.Add(this.label1);
            resources.ApplyResources(this.Infos, "Infos");
            this.Infos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Infos.Name = "Infos";
            this.Infos.TabStop = false;
            // 
            // groupOP
            // 
            this.groupOP.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupOP.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.groupOP, "groupOP");
            this.groupOP.Name = "groupOP";
            this.groupOP.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.groupOP, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.Infos, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // ctxLVGames
            // 
            this.ctxLVGames.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miPackThis,
            this.miRemoveIt,
            this.miInfoXml,
            this.imBackupXMLGame,
            this.mi7ZipIt,
            this.miZipIt});
            this.ctxLVGames.Name = "ctxLVGames";
            resources.ApplyResources(this.ctxLVGames, "ctxLVGames");
            this.toolTip1.SetToolTip(this.ctxLVGames, resources.GetString("ctxLVGames.ToolTip"));
            // 
            // miPackThis
            // 
            this.miPackThis.Name = "miPackThis";
            resources.ApplyResources(this.miPackThis, "miPackThis");
            this.miPackThis.Click += new System.EventHandler(this.miPackThis_Click);
            // 
            // miRemoveIt
            // 
            this.miRemoveIt.Name = "miRemoveIt";
            resources.ApplyResources(this.miRemoveIt, "miRemoveIt");
            this.miRemoveIt.Click += new System.EventHandler(this.miRemoveIt_Click);
            // 
            // miInfoXml
            // 
            this.miInfoXml.Name = "miInfoXml";
            resources.ApplyResources(this.miInfoXml, "miInfoXml");
            this.miInfoXml.Click += new System.EventHandler(this.miInfoXml_Click);
            // 
            // imBackupXMLGame
            // 
            this.imBackupXMLGame.Name = "imBackupXMLGame";
            resources.ApplyResources(this.imBackupXMLGame, "imBackupXMLGame");
            this.imBackupXMLGame.Click += new System.EventHandler(this.miBackupXMLGame_Click);
            // 
            // mi7ZipIt
            // 
            this.mi7ZipIt.Name = "mi7ZipIt";
            resources.ApplyResources(this.mi7ZipIt, "mi7ZipIt");
            this.mi7ZipIt.Click += new System.EventHandler(this.miZip_Click);
            // 
            // miZipIt
            // 
            this.miZipIt.Name = "miZipIt";
            resources.ApplyResources(this.miZipIt, "miZipIt");
            this.miZipIt.Click += new System.EventHandler(this.mi7ZipIt_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem,
            this.aboutToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // MainWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.menuTop);
            this.MainMenuStrip = this.menuTop;
            this.Name = "MainWindow";
            this.menuTop.ResumeLayout(false);
            this.menuTop.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.Infos.ResumeLayout(false);
            this.Infos.PerformLayout();
            this.groupOP.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ctxLVGames.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.MenuStrip menuTop;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ListView lvGames;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Title;
        private System.Windows.Forms.ColumnHeader Version;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbGames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lLaunchBoxPath;
        private System.Windows.Forms.Label lbSystems;
        private System.Windows.Forms.ComboBox lboxMachines;
        private System.Windows.Forms.GroupBox Infos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbOutPPath;
        private System.Windows.Forms.GroupBox groupOP;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btSelectWFolder;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem creditsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.Button butProceed;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip ctxLVGames;
        private System.Windows.Forms.ToolStripMenuItem miPackThis;
        private System.Windows.Forms.ToolStripMenuItem mi7ZipIt;
        private System.Windows.Forms.ToolStripMenuItem miZipIt;
        private System.Windows.Forms.ToolStripMenuItem miRemoveIt;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miInfoXml;
        private System.Windows.Forms.ToolStripMenuItem imBackupXMLGame;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpTSMI;
        private System.Windows.Forms.ToolStripMenuItem creditsTSMI;
        private System.Windows.Forms.ToolStripMenuItem aboutTSMI;
    }
}

