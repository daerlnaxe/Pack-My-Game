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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
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
            this.button3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tbOutPPath = new System.Windows.Forms.TextBox();
            this.butProceed = new System.Windows.Forms.Button();
            this.Infos = new System.Windows.Forms.GroupBox();
            this.groupOP = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.Infos.SuspendLayout();
            this.groupOP.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configToolStripMenuItem,
            this.aboutToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
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
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
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
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
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
            this.lvGames.CheckBoxes = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lvGames, 5);
            resources.ApplyResources(this.lvGames, "lvGames");
            this.lvGames.FullRowSelect = true;
            this.lvGames.HoverSelection = true;
            this.lvGames.MultiSelect = false;
            this.lvGames.Name = "lvGames";
            this.toolTip1.SetToolTip(this.lvGames, resources.GetString("lvGames.ToolTip"));
            this.lvGames.UseCompatibleStateImageBehavior = false;
            this.lvGames.View = System.Windows.Forms.View.Details;
            this.lvGames.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.GameColumn_Click);
            this.lvGames.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvGames_ItemCheck);
            this.lvGames.DoubleClick += new System.EventHandler(this.lvGames_DoubleClick);
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
            resources.ApplyResources(this.lboxMachines, "lboxMachines");
            this.lboxMachines.FormattingEnabled = true;
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
            this.tableLayoutPanel1.Controls.Add(this.button3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lvGames, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbOutPPath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbSystems, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lboxMachines, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbGames, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.butProceed, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.btChooseOutPut_Click);
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
            // MainWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.Infos.ResumeLayout(false);
            this.Infos.PerformLayout();
            this.groupOP.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.MenuStrip menuStrip1;
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
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem creditsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.Button butProceed;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

