using System;
using System.ComponentModel;

namespace Pack_My_Game.IHM
{
    partial class Config
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Config));
            this.tabGen = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbLang = new System.Windows.Forms.Label();
            this.listLang = new System.Windows.Forms.ComboBox();
            this.cbClone = new System.Windows.Forms.CheckBox();
            this.tabPaths = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btChooseLBPath = new System.Windows.Forms.Button();
            this.tbLaunchBoxPath = new System.Windows.Forms.TextBox();
            this.lbLBoxPath = new System.Windows.Forms.Label();
            this.gpPackMyGame = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.btChooseCCodes = new System.Windows.Forms.Button();
            this.tbCheatCodes = new System.Windows.Forms.TextBox();
            this.lbCheatCodes = new System.Windows.Forms.Label();
            this.tabPack = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.cbZip = new System.Windows.Forms.CheckBox();
            this.trackZipCompLvl = new MyControls.Trackbar1();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.cb7Zip = new System.Windows.Forms.CheckBox();
            this.track7ZipCompLvl = new MyControls.Trackbar1();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btSave = new System.Windows.Forms.Button();
            this.tabPanel1 = new System.Windows.Forms.TabControl();
            this.btCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tabGen.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPaths.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gpPackMyGame.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tabPack.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabGen
            // 
            resources.ApplyResources(this.tabGen, "tabGen");
            this.tabGen.Controls.Add(this.tableLayoutPanel2);
            this.tabGen.Name = "tabGen";
            this.toolTip1.SetToolTip(this.tabGen, resources.GetString("tabGen.ToolTip"));
            this.tabGen.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.lbLang, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.listLang, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbClone, 0, 1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.toolTip1.SetToolTip(this.tableLayoutPanel2, resources.GetString("tableLayoutPanel2.ToolTip"));
            // 
            // lbLang
            // 
            resources.ApplyResources(this.lbLang, "lbLang");
            this.lbLang.Name = "lbLang";
            this.toolTip1.SetToolTip(this.lbLang, resources.GetString("lbLang.ToolTip"));
            // 
            // listLang
            // 
            resources.ApplyResources(this.listLang, "listLang");
            this.listLang.FormattingEnabled = true;
            this.listLang.Name = "listLang";
            this.toolTip1.SetToolTip(this.listLang, resources.GetString("listLang.ToolTip"));
            this.listLang.SelectionChangeCommitted += new System.EventHandler(this.listLang_SelectionChangeCommitted);
            // 
            // cbClone
            // 
            resources.ApplyResources(this.cbClone, "cbClone");
            this.cbClone.Name = "cbClone";
            this.toolTip1.SetToolTip(this.cbClone, resources.GetString("cbClone.ToolTip"));
            this.cbClone.UseVisualStyleBackColor = true;
            // 
            // tabPaths
            // 
            resources.ApplyResources(this.tabPaths, "tabPaths");
            this.tabPaths.Controls.Add(this.flowLayoutPanel2);
            this.tabPaths.Name = "tabPaths";
            this.toolTip1.SetToolTip(this.tabPaths, resources.GetString("tabPaths.ToolTip"));
            this.tabPaths.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
            this.flowLayoutPanel2.Controls.Add(this.groupBox2);
            this.flowLayoutPanel2.Controls.Add(this.gpPackMyGame);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.toolTip1.SetToolTip(this.flowLayoutPanel2, resources.GetString("flowLayoutPanel2.ToolTip"));
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox2, resources.GetString("groupBox2.ToolTip"));
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.btChooseLBPath, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbLaunchBoxPath, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbLBoxPath, 0, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.toolTip1.SetToolTip(this.tableLayoutPanel1, resources.GetString("tableLayoutPanel1.ToolTip"));
            // 
            // btChooseLBPath
            // 
            resources.ApplyResources(this.btChooseLBPath, "btChooseLBPath");
            this.btChooseLBPath.Name = "btChooseLBPath";
            this.toolTip1.SetToolTip(this.btChooseLBPath, resources.GetString("btChooseLBPath.ToolTip"));
            this.btChooseLBPath.UseVisualStyleBackColor = true;
            this.btChooseLBPath.Click += new System.EventHandler(this.btChooseLBPath_Click);
            this.btChooseLBPath.Validating += new System.ComponentModel.CancelEventHandler(this.LBPath_Validating);
            // 
            // tbLaunchBoxPath
            // 
            resources.ApplyResources(this.tbLaunchBoxPath, "tbLaunchBoxPath");
            this.tbLaunchBoxPath.Name = "tbLaunchBoxPath";
            this.toolTip1.SetToolTip(this.tbLaunchBoxPath, resources.GetString("tbLaunchBoxPath.ToolTip"));
            this.tbLaunchBoxPath.Validating += new System.ComponentModel.CancelEventHandler(this.LBPath_Validating);
            // 
            // lbLBoxPath
            // 
            resources.ApplyResources(this.lbLBoxPath, "lbLBoxPath");
            this.lbLBoxPath.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbLBoxPath.Name = "lbLBoxPath";
            this.toolTip1.SetToolTip(this.lbLBoxPath, resources.GetString("lbLBoxPath.ToolTip"));
            // 
            // gpPackMyGame
            // 
            resources.ApplyResources(this.gpPackMyGame, "gpPackMyGame");
            this.gpPackMyGame.Controls.Add(this.richTextBox1);
            this.gpPackMyGame.Controls.Add(this.tableLayoutPanel7);
            this.gpPackMyGame.Name = "gpPackMyGame";
            this.gpPackMyGame.TabStop = false;
            this.toolTip1.SetToolTip(this.gpPackMyGame, resources.GetString("gpPackMyGame.ToolTip"));
            // 
            // richTextBox1
            // 
            resources.ApplyResources(this.richTextBox1, "richTextBox1");
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.toolTip1.SetToolTip(this.richTextBox1, resources.GetString("richTextBox1.ToolTip"));
            // 
            // tableLayoutPanel7
            // 
            resources.ApplyResources(this.tableLayoutPanel7, "tableLayoutPanel7");
            this.tableLayoutPanel7.Controls.Add(this.btChooseCCodes, 2, 1);
            this.tableLayoutPanel7.Controls.Add(this.tbCheatCodes, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.lbCheatCodes, 0, 1);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.toolTip1.SetToolTip(this.tableLayoutPanel7, resources.GetString("tableLayoutPanel7.ToolTip"));
            // 
            // btChooseCCodes
            // 
            resources.ApplyResources(this.btChooseCCodes, "btChooseCCodes");
            this.btChooseCCodes.Name = "btChooseCCodes";
            this.toolTip1.SetToolTip(this.btChooseCCodes, resources.GetString("btChooseCCodes.ToolTip"));
            this.btChooseCCodes.UseVisualStyleBackColor = true;
            this.btChooseCCodes.Click += new System.EventHandler(this.btChooseCCodes_Click);
            // 
            // tbCheatCodes
            // 
            resources.ApplyResources(this.tbCheatCodes, "tbCheatCodes");
            this.tbCheatCodes.Name = "tbCheatCodes";
            this.toolTip1.SetToolTip(this.tbCheatCodes, resources.GetString("tbCheatCodes.ToolTip"));
            this.tbCheatCodes.Validating += new System.ComponentModel.CancelEventHandler(this.CCodes_Validating);
            // 
            // lbCheatCodes
            // 
            resources.ApplyResources(this.lbCheatCodes, "lbCheatCodes");
            this.lbCheatCodes.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbCheatCodes.Name = "lbCheatCodes";
            this.toolTip1.SetToolTip(this.lbCheatCodes, resources.GetString("lbCheatCodes.ToolTip"));
            // 
            // tabPack
            // 
            resources.ApplyResources(this.tabPack, "tabPack");
            this.tabPack.Controls.Add(this.flowLayoutPanel4);
            this.tabPack.Name = "tabPack";
            this.toolTip1.SetToolTip(this.tabPack, resources.GetString("tabPack.ToolTip"));
            this.tabPack.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel4
            // 
            resources.ApplyResources(this.flowLayoutPanel4, "flowLayoutPanel4");
            this.flowLayoutPanel4.Controls.Add(this.groupBox1);
            this.flowLayoutPanel4.Controls.Add(this.groupBox3);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.toolTip1.SetToolTip(this.flowLayoutPanel4, resources.GetString("flowLayoutPanel4.ToolTip"));
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel6);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.toolTip1.SetToolTip(this.flowLayoutPanel1, resources.GetString("flowLayoutPanel1.ToolTip"));
            // 
            // tableLayoutPanel6
            // 
            resources.ApplyResources(this.tableLayoutPanel6, "tableLayoutPanel6");
            this.tableLayoutPanel6.Controls.Add(this.cbZip, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.trackZipCompLvl, 1, 1);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.toolTip1.SetToolTip(this.tableLayoutPanel6, resources.GetString("tableLayoutPanel6.ToolTip"));
            // 
            // cbZip
            // 
            resources.ApplyResources(this.cbZip, "cbZip");
            this.cbZip.Checked = true;
            this.cbZip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbZip.Name = "cbZip";
            this.toolTip1.SetToolTip(this.cbZip, resources.GetString("cbZip.ToolTip"));
            this.cbZip.UseVisualStyleBackColor = true;
            // 
            // trackZipCompLvl
            // 
            resources.ApplyResources(this.trackZipCompLvl, "trackZipCompLvl");
            this.trackZipCompLvl.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel6.SetColumnSpan(this.trackZipCompLvl, 3);
            this.trackZipCompLvl.Maximum = 10;
            this.trackZipCompLvl.MidSpacer = new System.Windows.Forms.Padding(20, 0, 3, 0);
            this.trackZipCompLvl.Minimum = 0;
            this.trackZipCompLvl.Name = "trackZipCompLvl";
            this.trackZipCompLvl.Position = 10;
            this.trackZipCompLvl.TitleSize = 132;
            this.toolTip1.SetToolTip(this.trackZipCompLvl, resources.GetString("trackZipCompLvl.ToolTip"));
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.flowLayoutPanel3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox3, resources.GetString("groupBox3.ToolTip"));
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(this.flowLayoutPanel3, "flowLayoutPanel3");
            this.flowLayoutPanel3.Controls.Add(this.tableLayoutPanel8);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.toolTip1.SetToolTip(this.flowLayoutPanel3, resources.GetString("flowLayoutPanel3.ToolTip"));
            // 
            // tableLayoutPanel8
            // 
            resources.ApplyResources(this.tableLayoutPanel8, "tableLayoutPanel8");
            this.tableLayoutPanel8.Controls.Add(this.cb7Zip, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.track7ZipCompLvl, 1, 1);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.toolTip1.SetToolTip(this.tableLayoutPanel8, resources.GetString("tableLayoutPanel8.ToolTip"));
            // 
            // cb7Zip
            // 
            resources.ApplyResources(this.cb7Zip, "cb7Zip");
            this.cb7Zip.Checked = true;
            this.cb7Zip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb7Zip.Name = "cb7Zip";
            this.toolTip1.SetToolTip(this.cb7Zip, resources.GetString("cb7Zip.ToolTip"));
            this.cb7Zip.UseVisualStyleBackColor = true;
            // 
            // track7ZipCompLvl
            // 
            resources.ApplyResources(this.track7ZipCompLvl, "track7ZipCompLvl");
            this.track7ZipCompLvl.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel8.SetColumnSpan(this.track7ZipCompLvl, 3);
            this.track7ZipCompLvl.Maximum = 6;
            this.track7ZipCompLvl.MidSpacer = new System.Windows.Forms.Padding(20, 0, 3, 0);
            this.track7ZipCompLvl.Minimum = 0;
            this.track7ZipCompLvl.Name = "track7ZipCompLvl";
            this.track7ZipCompLvl.Position = 6;
            this.track7ZipCompLvl.TitleSize = 129;
            this.toolTip1.SetToolTip(this.track7ZipCompLvl, resources.GetString("track7ZipCompLvl.ToolTip"));
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.btSave, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.tabPanel1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btCancel, 1, 2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.toolTip1.SetToolTip(this.tableLayoutPanel3, resources.GetString("tableLayoutPanel3.ToolTip"));
            // 
            // btSave
            // 
            resources.ApplyResources(this.btSave, "btSave");
            this.btSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btSave.Name = "btSave";
            this.toolTip1.SetToolTip(this.btSave, resources.GetString("btSave.ToolTip"));
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // tabPanel1
            // 
            resources.ApplyResources(this.tabPanel1, "tabPanel1");
            this.tableLayoutPanel3.SetColumnSpan(this.tabPanel1, 3);
            this.tabPanel1.Controls.Add(this.tabGen);
            this.tabPanel1.Controls.Add(this.tabPaths);
            this.tabPanel1.Controls.Add(this.tabPack);
            this.tabPanel1.Name = "tabPanel1";
            this.tabPanel1.SelectedIndex = 0;
            this.toolTip1.SetToolTip(this.tabPanel1, resources.GetString("tabPanel1.ToolTip"));
            // 
            // btCancel
            // 
            resources.ApplyResources(this.btCancel, "btCancel");
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Name = "btCancel";
            this.toolTip1.SetToolTip(this.btCancel, resources.GetString("btCancel.ToolTip"));
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // Config
            // 
            this.AcceptButton = this.btSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.Controls.Add(this.tableLayoutPanel3);
            this.MaximizeBox = false;
            this.Name = "Config";
            this.ShowInTaskbar = false;
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Config_Load);
            this.tabGen.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tabPaths.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.gpPackMyGame.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tabPack.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tabPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }



        #endregion
        private System.Windows.Forms.TabPage tabPaths;
        private System.Windows.Forms.TabPage tabPack;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbLBoxPath;
        private System.Windows.Forms.TextBox tbLaunchBoxPath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.TabPage tabGen;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lbLang;
        private System.Windows.Forms.ComboBox listLang;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.CheckBox cbZip;
        private System.Windows.Forms.GroupBox gpPackMyGame;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btChooseLBPath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Button btChooseCCodes;
        private System.Windows.Forms.TextBox tbCheatCodes;
        private System.Windows.Forms.Label lbCheatCodes;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.CheckBox cb7Zip;

        private System.Windows.Forms.TabControl tabPanel1;
        private MyControls.Trackbar1 track7ZipCompLvl;
        private MyControls.Trackbar1 trackZipCompLvl;
        private System.Windows.Forms.CheckBox cbClone;
    }
}