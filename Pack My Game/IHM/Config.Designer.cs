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
            this.tabComp = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.trackZipCompLvl = new MyControls.Trackbar1();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.track7ZipCompLvl = new MyControls.Trackbar1();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btSave = new System.Windows.Forms.Button();
            this.tabConfig = new System.Windows.Forms.TabControl();
            this.btCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gbProcess = new System.Windows.Forms.GroupBox();
            this.cbZip = new System.Windows.Forms.CheckBox();
            this.cb7Zip = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cbClone = new System.Windows.Forms.CheckBox();
            this.gbCompress = new System.Windows.Forms.GroupBox();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.tabGen.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tabPaths.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gpPackMyGame.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tabComp.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.gbProcess.SuspendLayout();
            this.gbCompress.SuspendLayout();
            this.gbLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabGen
            // 
            this.tabGen.Controls.Add(this.tableLayoutPanel2);
            resources.ApplyResources(this.tabGen, "tabGen");
            this.tabGen.Name = "tabGen";
            this.tabGen.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            resources.ApplyResources(this.tableLayoutPanel2, "tableLayoutPanel2");
            this.tableLayoutPanel2.Controls.Add(this.lbLang, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.listLang, 1, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            // 
            // lbLang
            // 
            resources.ApplyResources(this.lbLang, "lbLang");
            this.lbLang.Name = "lbLang";
            // 
            // listLang
            // 
            this.listLang.FormattingEnabled = true;
            resources.ApplyResources(this.listLang, "listLang");
            this.listLang.Name = "listLang";
            this.listLang.SelectionChangeCommitted += new System.EventHandler(this.listLang_SelectionChangeCommitted);
            // 
            // tabPaths
            // 
            this.tabPaths.Controls.Add(this.flowLayoutPanel2);
            resources.ApplyResources(this.tabPaths, "tabPaths");
            this.tabPaths.Name = "tabPaths";
            this.tabPaths.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.groupBox2);
            this.flowLayoutPanel2.Controls.Add(this.gpPackMyGame);
            resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.groupBox2, "groupBox2");
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
            // 
            // btChooseLBPath
            // 
            resources.ApplyResources(this.btChooseLBPath, "btChooseLBPath");
            this.btChooseLBPath.Name = "btChooseLBPath";
            this.btChooseLBPath.UseVisualStyleBackColor = true;
            this.btChooseLBPath.Click += new System.EventHandler(this.btChooseLBPath_Click);
            this.btChooseLBPath.Validating += new System.ComponentModel.CancelEventHandler(this.LBPath_Validating);
            // 
            // tbLaunchBoxPath
            // 
            resources.ApplyResources(this.tbLaunchBoxPath, "tbLaunchBoxPath");
            this.tbLaunchBoxPath.Name = "tbLaunchBoxPath";
            this.tbLaunchBoxPath.Validating += new System.ComponentModel.CancelEventHandler(this.LBPath_Validating);
            // 
            // lbLBoxPath
            // 
            resources.ApplyResources(this.lbLBoxPath, "lbLBoxPath");
            this.lbLBoxPath.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbLBoxPath.Name = "lbLBoxPath";
            // 
            // gpPackMyGame
            // 
            this.gpPackMyGame.Controls.Add(this.richTextBox1);
            this.gpPackMyGame.Controls.Add(this.tableLayoutPanel7);
            resources.ApplyResources(this.gpPackMyGame, "gpPackMyGame");
            this.gpPackMyGame.Name = "gpPackMyGame";
            this.gpPackMyGame.TabStop = false;
            this.toolTip1.SetToolTip(this.gpPackMyGame, resources.GetString("gpPackMyGame.ToolTip"));
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.richTextBox1, "richTextBox1");
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            // 
            // tableLayoutPanel7
            // 
            resources.ApplyResources(this.tableLayoutPanel7, "tableLayoutPanel7");
            this.tableLayoutPanel7.Controls.Add(this.btChooseCCodes, 2, 1);
            this.tableLayoutPanel7.Controls.Add(this.tbCheatCodes, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.lbCheatCodes, 0, 1);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
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
            this.tbCheatCodes.Validating += new System.ComponentModel.CancelEventHandler(this.CCodes_Validating);
            // 
            // lbCheatCodes
            // 
            resources.ApplyResources(this.lbCheatCodes, "lbCheatCodes");
            this.lbCheatCodes.Cursor = System.Windows.Forms.Cursors.Default;
            this.lbCheatCodes.Name = "lbCheatCodes";
            this.toolTip1.SetToolTip(this.lbCheatCodes, resources.GetString("lbCheatCodes.ToolTip"));
            // 
            // tabComp
            // 
            this.tabComp.Controls.Add(this.flowLayoutPanel4);
            resources.ApplyResources(this.tabComp, "tabComp");
            this.tabComp.Name = "tabComp";
            this.tabComp.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.groupBox1);
            this.flowLayoutPanel4.Controls.Add(this.groupBox3);
            resources.ApplyResources(this.flowLayoutPanel4, "flowLayoutPanel4");
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel6);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // tableLayoutPanel6
            // 
            resources.ApplyResources(this.tableLayoutPanel6, "tableLayoutPanel6");
            this.tableLayoutPanel6.Controls.Add(this.trackZipCompLvl, 1, 1);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            // 
            // trackZipCompLvl
            // 
            this.trackZipCompLvl.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel6.SetColumnSpan(this.trackZipCompLvl, 3);
            resources.ApplyResources(this.trackZipCompLvl, "trackZipCompLvl");
            this.trackZipCompLvl.Maximum = 10;
            this.trackZipCompLvl.MidSpacer = new System.Windows.Forms.Padding(20, 0, 3, 0);
            this.trackZipCompLvl.Minimum = 0;
            this.trackZipCompLvl.Name = "trackZipCompLvl";
            this.trackZipCompLvl.Position = 10;
            this.trackZipCompLvl.TitleSize = 132;
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.flowLayoutPanel3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(this.flowLayoutPanel3, "flowLayoutPanel3");
            this.flowLayoutPanel3.Controls.Add(this.tableLayoutPanel8);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // tableLayoutPanel8
            // 
            resources.ApplyResources(this.tableLayoutPanel8, "tableLayoutPanel8");
            this.tableLayoutPanel8.Controls.Add(this.track7ZipCompLvl, 1, 1);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            // 
            // track7ZipCompLvl
            // 
            this.track7ZipCompLvl.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel8.SetColumnSpan(this.track7ZipCompLvl, 3);
            resources.ApplyResources(this.track7ZipCompLvl, "track7ZipCompLvl");
            this.track7ZipCompLvl.Maximum = 6;
            this.track7ZipCompLvl.MidSpacer = new System.Windows.Forms.Padding(20, 0, 3, 0);
            this.track7ZipCompLvl.Minimum = 0;
            this.track7ZipCompLvl.Name = "track7ZipCompLvl";
            this.track7ZipCompLvl.Position = 6;
            this.track7ZipCompLvl.TitleSize = 129;
            // 
            // tableLayoutPanel3
            // 
            resources.ApplyResources(this.tableLayoutPanel3, "tableLayoutPanel3");
            this.tableLayoutPanel3.Controls.Add(this.btSave, 2, 2);
            this.tableLayoutPanel3.Controls.Add(this.tabConfig, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btCancel, 1, 2);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            // 
            // btSave
            // 
            this.btSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btSave, "btSave");
            this.btSave.Name = "btSave";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // tabConfig
            // 
            this.tableLayoutPanel3.SetColumnSpan(this.tabConfig, 3);
            this.tabConfig.Controls.Add(this.tabGen);
            this.tabConfig.Controls.Add(this.tabPaths);
            this.tabConfig.Controls.Add(this.tabComp);
            this.tabConfig.Controls.Add(this.tabPage1);
            resources.ApplyResources(this.tabConfig, "tabConfig");
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.SelectedIndex = 0;
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btCancel, "btCancel");
            this.btCancel.Name = "btCancel";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gbLog);
            this.tabPage1.Controls.Add(this.gbCompress);
            this.tabPage1.Controls.Add(this.gbProcess);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gbProcess
            // 
            this.gbProcess.Controls.Add(this.checkBox4);
            this.gbProcess.Controls.Add(this.checkBox3);
            this.gbProcess.Controls.Add(this.checkBox2);
            this.gbProcess.Controls.Add(this.checkBox1);
            this.gbProcess.Controls.Add(this.cbClone);
            resources.ApplyResources(this.gbProcess, "gbProcess");
            this.gbProcess.Name = "gbProcess";
            this.gbProcess.TabStop = false;
            // 
            // cbZip
            // 
            resources.ApplyResources(this.cbZip, "cbZip");
            this.cbZip.Checked = true;
            this.cbZip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbZip.Name = "cbZip";
            this.cbZip.UseVisualStyleBackColor = true;
            // 
            // cb7Zip
            // 
            resources.ApplyResources(this.cb7Zip, "cb7Zip");
            this.cb7Zip.Checked = true;
            this.cb7Zip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb7Zip.Name = "cb7Zip";
            this.cb7Zip.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            resources.ApplyResources(this.checkBox4, "checkBox4");
            this.checkBox4.Name = "checkBox4";
            this.toolTip1.SetToolTip(this.checkBox4, resources.GetString("checkBox4.ToolTip"));
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            resources.ApplyResources(this.checkBox3, "checkBox3");
            this.checkBox3.Name = "checkBox3";
            this.toolTip1.SetToolTip(this.checkBox3, resources.GetString("checkBox3.ToolTip"));
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            resources.ApplyResources(this.checkBox2, "checkBox2");
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.toolTip1.SetToolTip(this.checkBox1, resources.GetString("checkBox1.ToolTip"));
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // cbClone
            // 
            resources.ApplyResources(this.cbClone, "cbClone");
            this.cbClone.Name = "cbClone";
            this.toolTip1.SetToolTip(this.cbClone, resources.GetString("cbClone.ToolTip"));
            this.cbClone.UseVisualStyleBackColor = true;
            // 
            // gbCompress
            // 
            this.gbCompress.Controls.Add(this.cbZip);
            this.gbCompress.Controls.Add(this.cb7Zip);
            resources.ApplyResources(this.gbCompress, "gbCompress");
            this.gbCompress.Name = "gbCompress";
            this.gbCompress.TabStop = false;
            // 
            // gbLog
            // 
            this.gbLog.Controls.Add(this.checkBox6);
            this.gbLog.Controls.Add(this.checkBox5);
            resources.ApplyResources(this.gbLog, "gbLog");
            this.gbLog.Name = "gbLog";
            this.gbLog.TabStop = false;
            // 
            // checkBox5
            // 
            resources.ApplyResources(this.checkBox5, "checkBox5");
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            resources.ApplyResources(this.checkBox6, "checkBox6");
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.UseVisualStyleBackColor = true;
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
            this.tabComp.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tabConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.gbProcess.ResumeLayout(false);
            this.gbProcess.PerformLayout();
            this.gbCompress.ResumeLayout(false);
            this.gbCompress.PerformLayout();
            this.gbLog.ResumeLayout(false);
            this.gbLog.PerformLayout();
            this.ResumeLayout(false);

        }



        #endregion
        private System.Windows.Forms.TabPage tabPaths;
        private System.Windows.Forms.TabPage tabComp;
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

        private System.Windows.Forms.TabControl tabConfig;
        private MyControls.Trackbar1 track7ZipCompLvl;
        private MyControls.Trackbar1 trackZipCompLvl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox gbProcess;
        private System.Windows.Forms.CheckBox cbZip;
        private System.Windows.Forms.CheckBox cb7Zip;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox cbClone;
        private System.Windows.Forms.GroupBox gbCompress;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox5;
    }
}