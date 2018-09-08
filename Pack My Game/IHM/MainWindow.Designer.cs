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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.menuTop = new System.Windows.Forms.MenuStrip();
            this.configToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.menuTop.Location = new System.Drawing.Point(0, 0);
            this.menuTop.Name = "menuTop";
            this.menuTop.Size = new System.Drawing.Size(784, 24);
            this.menuTop.TabIndex = 3;
            this.menuTop.Text = "menuStrip1";
            // 
            // configToolStripMenuItem1
            // 
            this.configToolStripMenuItem1.Name = "configToolStripMenuItem1";
            this.configToolStripMenuItem1.Size = new System.Drawing.Size(55, 20);
            this.configToolStripMenuItem1.Text = "Config";
            this.configToolStripMenuItem1.Click += new System.EventHandler(this.settingsTSMI_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.testToolStripMenuItem});
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.configToolStripMenuItem.Text = "Options";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsTSMI_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.testToolStripMenuItem.Text = "test";
            this.testToolStripMenuItem.Visible = false;
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.creditsToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(131, 22);
            this.helpToolStripMenuItem1.Text = "Show Help";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // creditsToolStripMenuItem
            // 
            this.creditsToolStripMenuItem.Name = "creditsToolStripMenuItem";
            this.creditsToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.creditsToolStripMenuItem.Text = "Credits";
            this.creditsToolStripMenuItem.Click += new System.EventHandler(this.creditsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(131, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // lvGames
            // 
            this.lvGames.AutoArrange = false;
            this.lvGames.CheckBoxes = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lvGames, 5);
            this.lvGames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvGames.FullRowSelect = true;
            this.lvGames.Location = new System.Drawing.Point(6, 103);
            this.lvGames.MultiSelect = false;
            this.lvGames.Name = "lvGames";
            this.lvGames.Size = new System.Drawing.Size(765, 419);
            this.lvGames.TabIndex = 19;
            this.lvGames.UseCompatibleStateImageBehavior = false;
            this.lvGames.View = System.Windows.Forms.View.Details;
            this.lvGames.Visible = false;
            this.lvGames.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.GameColumn_Click);
            this.lvGames.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvGames_ItemCheck);
            this.lvGames.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvGames_MouseClick);
            this.lvGames.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvGames_MouseDoubleClick);
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 0;
            // 
            // Title
            // 
            this.Title.Text = "Title";
            this.Title.Width = 200;
            // 
            // Version
            // 
            this.Version.Text = "Version";
            this.Version.Width = 373;
            // 
            // lboxMachines
            // 
            this.lboxMachines.FormattingEnabled = true;
            this.lboxMachines.Location = new System.Drawing.Point(208, 36);
            this.lboxMachines.MinimumSize = new System.Drawing.Size(10, 0);
            this.lboxMachines.Name = "lboxMachines";
            this.lboxMachines.Size = new System.Drawing.Size(171, 21);
            this.lboxMachines.TabIndex = 12;
            this.lboxMachines.Visible = false;
            this.lboxMachines.SelectedIndexChanged += new System.EventHandler(this.lboxMachines_SelectedIndexChanged);
            // 
            // lbSystems
            // 
            this.lbSystems.AutoSize = true;
            this.lbSystems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbSystems.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbSystems.Location = new System.Drawing.Point(6, 36);
            this.lbSystems.Margin = new System.Windows.Forms.Padding(3);
            this.lbSystems.Name = "lbSystems";
            this.lbSystems.Size = new System.Drawing.Size(196, 13);
            this.lbSystems.TabIndex = 101;
            this.lbSystems.Text = "Choose a System (Only Consoles)";
            this.lbSystems.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbSystems.Visible = false;
            // 
            // lLaunchBoxPath
            // 
            this.lLaunchBoxPath.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lLaunchBoxPath.AutoSize = true;
            this.lLaunchBoxPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lLaunchBoxPath.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lLaunchBoxPath.Location = new System.Drawing.Point(116, 16);
            this.lLaunchBoxPath.Name = "lLaunchBoxPath";
            this.lLaunchBoxPath.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lLaunchBoxPath.Size = new System.Drawing.Size(182, 16);
            this.lLaunchBoxPath.TabIndex = 7;
            this.lLaunchBoxPath.Text = "Choose the path of LaunchBox";
            this.lLaunchBoxPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.label1.Size = new System.Drawing.Size(104, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "LaunchBox Path:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbGames
            // 
            this.lbGames.AutoSize = true;
            this.lbGames.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbGames.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbGames.Location = new System.Drawing.Point(6, 63);
            this.lbGames.Margin = new System.Windows.Forms.Padding(3);
            this.lbGames.Name = "lbGames";
            this.lbGames.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.lbGames.Size = new System.Drawing.Size(100, 33);
            this.lbGames.TabIndex = 102;
            this.lbGames.Text = "Choose a Game:";
            this.lbGames.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbGames.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.90393F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.09607F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.btSelectWFolder, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lvGames, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbOutPPath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbSystems, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lboxMachines, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbGames, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.butProceed, 4, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(778, 578);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // btSelectWFolder
            // 
            this.btSelectWFolder.AutoSize = true;
            this.btSelectWFolder.Location = new System.Drawing.Point(492, 6);
            this.btSelectWFolder.MinimumSize = new System.Drawing.Size(50, 10);
            this.btSelectWFolder.Name = "btSelectWFolder";
            this.btSelectWFolder.Size = new System.Drawing.Size(50, 24);
            this.btSelectWFolder.TabIndex = 18;
            this.btSelectWFolder.Text = "Go";
            this.btSelectWFolder.UseVisualStyleBackColor = true;
            this.btSelectWFolder.Click += new System.EventHandler(this.btChooseOutPut_Click);
            // 
            // label4
            // 
            this.label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(6, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(194, 24);
            this.label4.TabIndex = 100;
            this.label4.Text = "Choose a working folder:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbOutPPath
            // 
            this.tbOutPPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOutPPath.Location = new System.Drawing.Point(208, 8);
            this.tbOutPPath.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbOutPPath.Name = "tbOutPPath";
            this.tbOutPPath.ReadOnly = true;
            this.tbOutPPath.Size = new System.Drawing.Size(278, 20);
            this.tbOutPPath.TabIndex = 6;
            this.tbOutPPath.Text = "...";
            this.toolTip1.SetToolTip(this.tbOutPPath, "Indicate the folder to copy data & compress");
            // 
            // butProceed
            // 
            this.butProceed.Location = new System.Drawing.Point(696, 528);
            this.butProceed.Name = "butProceed";
            this.butProceed.Size = new System.Drawing.Size(75, 23);
            this.butProceed.TabIndex = 103;
            this.butProceed.Text = "Proceed";
            this.butProceed.UseVisualStyleBackColor = true;
            this.butProceed.Visible = false;
            this.butProceed.Click += new System.EventHandler(this.Proceed_Click);
            // 
            // Infos
            // 
            this.Infos.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Infos.Controls.Add(this.lLaunchBoxPath);
            this.Infos.Controls.Add(this.label1);
            this.Infos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Infos.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Infos.Location = new System.Drawing.Point(3, 3);
            this.Infos.Name = "Infos";
            this.Infos.Size = new System.Drawing.Size(778, 52);
            this.Infos.TabIndex = 4;
            this.Infos.TabStop = false;
            this.Infos.Text = "Infos";
            // 
            // groupOP
            // 
            this.groupOP.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupOP.Controls.Add(this.tableLayoutPanel1);
            this.groupOP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupOP.Location = new System.Drawing.Point(3, 61);
            this.groupOP.Name = "groupOP";
            this.groupOP.Padding = new System.Windows.Forms.Padding(0);
            this.groupOP.Size = new System.Drawing.Size(778, 591);
            this.groupOP.TabIndex = 103;
            this.groupOP.TabStop = false;
            this.groupOP.Text = "Options";
            this.groupOP.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.groupOP, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.Infos, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(784, 655);
            this.tableLayoutPanel2.TabIndex = 6;
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
            this.ctxLVGames.Size = new System.Drawing.Size(188, 136);
            this.toolTip1.SetToolTip(this.ctxLVGames, "Backup all informations from launchbox xml file");
            // 
            // miPackThis
            // 
            this.miPackThis.Name = "miPackThis";
            this.miPackThis.Size = new System.Drawing.Size(187, 22);
            this.miPackThis.Text = "PackThis ...";
            this.miPackThis.Click += new System.EventHandler(this.miPackThis_Click);
            // 
            // miRemoveIt
            // 
            this.miRemoveIt.Name = "miRemoveIt";
            this.miRemoveIt.Size = new System.Drawing.Size(187, 22);
            this.miRemoveIt.Text = "Remove from list";
            this.miRemoveIt.Click += new System.EventHandler(this.miRemoveIt_Click);
            // 
            // miInfoXml
            // 
            this.miInfoXml.Name = "miInfoXml";
            this.miInfoXml.Size = new System.Drawing.Size(187, 22);
            this.miInfoXml.Text = "Make Info.xml";
            this.miInfoXml.Click += new System.EventHandler(this.miInfoXml_Click);
            // 
            // imBackupXMLGame
            // 
            this.imBackupXMLGame.Name = "imBackupXMLGame";
            this.imBackupXMLGame.Size = new System.Drawing.Size(187, 22);
            this.imBackupXMLGame.Text = "Backup original datas";
            this.imBackupXMLGame.Click += new System.EventHandler(this.miBackupXMLGame_Click);
            // 
            // mi7ZipIt
            // 
            this.mi7ZipIt.Name = "mi7ZipIt";
            this.mi7ZipIt.Size = new System.Drawing.Size(187, 22);
            this.mi7ZipIt.Text = "Zip it...";
            this.mi7ZipIt.Click += new System.EventHandler(this.miZip_Click);
            // 
            // miZipIt
            // 
            this.miZipIt.Name = "miZipIt";
            this.miZipIt.Size = new System.Drawing.Size(187, 22);
            this.miZipIt.Text = "7-Zip it...";
            this.miZipIt.Click += new System.EventHandler(this.mi7ZipIt_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 679);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.menuTop);
            this.MainMenuStrip = this.menuTop;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainWindow";
            this.Text = "Pack My Game - Alpha";
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
    }
}

