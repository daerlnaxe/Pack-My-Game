﻿namespace Pack_My_Game.IHM
{
    partial class CheatForm
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
            this.rtBOX1 = new System.Windows.Forms.RichTextBox();
            this.btSave = new System.Windows.Forms.Button();
            this.btOK = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCrop = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btCrop = new System.Windows.Forms.Button();
            this.cbCropActive = new System.Windows.Forms.CheckBox();
            this.contextMSText = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.searchOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameFaqToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jeuxVideoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1.SuspendLayout();
            this.contextMSText.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtBOX1
            // 
            this.rtBOX1.AcceptsTab = true;
            this.rtBOX1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtBOX1.BulletIndent = 5;
            this.rtBOX1.Location = new System.Drawing.Point(23, 57);
            this.rtBOX1.Name = "rtBOX1";
            this.rtBOX1.Size = new System.Drawing.Size(753, 317);
            this.rtBOX1.TabIndex = 0;
            this.rtBOX1.Text = "";
            this.rtBOX1.WordWrap = false;
            this.rtBOX1.TextChanged += new System.EventHandler(this.rtBOX1_TextChanged);
            this.rtBOX1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rtBOX1_MouseDown);
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btSave.Location = new System.Drawing.Point(23, 409);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(99, 27);
            this.btSave.TabIndex = 1;
            this.btSave.Text = "Save";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOK.Enabled = false;
            this.btOK.Location = new System.Drawing.Point(701, 409);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(75, 27);
            this.btOK.TabIndex = 2;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(628, 409);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(67, 27);
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter your cheats code";
            // 
            // tbCrop
            // 
            this.tbCrop.Location = new System.Drawing.Point(68, 3);
            this.tbCrop.MaxLength = 3;
            this.tbCrop.Name = "tbCrop";
            this.tbCrop.Size = new System.Drawing.Size(72, 20);
            this.tbCrop.TabIndex = 5;
            this.tbCrop.Text = "150";
            this.tbCrop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbCrop.Click += new System.EventHandler(this.tbCrop_Click);
            this.tbCrop.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCrop_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Width Limit";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.btCrop);
            this.flowLayoutPanel1.Controls.Add(this.cbCropActive);
            this.flowLayoutPanel1.Controls.Add(this.tbCrop);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(490, 22);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(286, 29);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // btCrop
            // 
            this.btCrop.Enabled = false;
            this.btCrop.Location = new System.Drawing.Point(208, 3);
            this.btCrop.Name = "btCrop";
            this.btCrop.Size = new System.Drawing.Size(75, 23);
            this.btCrop.TabIndex = 7;
            this.btCrop.Text = "Crop";
            this.btCrop.UseVisualStyleBackColor = true;
            this.btCrop.Click += new System.EventHandler(this.btCrop_Click);
            // 
            // cbCropActive
            // 
            this.cbCropActive.AutoSize = true;
            this.cbCropActive.Location = new System.Drawing.Point(146, 3);
            this.cbCropActive.Name = "cbCropActive";
            this.cbCropActive.Size = new System.Drawing.Size(56, 17);
            this.cbCropActive.TabIndex = 8;
            this.cbCropActive.Text = "Active";
            this.cbCropActive.UseVisualStyleBackColor = true;
            this.cbCropActive.CheckedChanged += new System.EventHandler(this.cbCropActive_CheckedChanged);
            // 
            // contextMSText
            // 
            this.contextMSText.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.toolStripSeparator3,
            this.copyToolStripMenuItem,
            this.copyAllToolStripMenuItem,
            this.toolStripSeparator2,
            this.pasteToolStripMenuItem});
            this.contextMSText.Name = "contextMSText";
            this.contextMSText.Size = new System.Drawing.Size(123, 104);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.selectAllToolStripMenuItem.Text = "Select All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // copyAllToolStripMenuItem
            // 
            this.copyAllToolStripMenuItem.Name = "copyAllToolStripMenuItem";
            this.copyAllToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.copyAllToolStripMenuItem.Text = "Copy All";
            this.copyAllToolStripMenuItem.Click += new System.EventHandler(this.copyAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(119, 6);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(119, 6);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchOnToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // searchOnToolStripMenuItem
            // 
            this.searchOnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameFaqToolStripMenuItem,
            this.jeuxVideoToolStripMenuItem});
            this.searchOnToolStripMenuItem.Name = "searchOnToolStripMenuItem";
            this.searchOnToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.searchOnToolStripMenuItem.Text = "Search on";
            // 
            // gameFaqToolStripMenuItem
            // 
            this.gameFaqToolStripMenuItem.Name = "gameFaqToolStripMenuItem";
            this.gameFaqToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.gameFaqToolStripMenuItem.Text = "GameFaq";
            this.gameFaqToolStripMenuItem.Click += new System.EventHandler(this.gameFaqToolStripMenuItem_Click);
            // 
            // jeuxVideoToolStripMenuItem
            // 
            this.jeuxVideoToolStripMenuItem.Name = "jeuxVideoToolStripMenuItem";
            this.jeuxVideoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.jeuxVideoToolStripMenuItem.Text = "JeuxVideo";
            this.jeuxVideoToolStripMenuItem.Click += new System.EventHandler(this.jeuxVideoToolStripMenuItem_Click);
            // 
            // CheatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.rtBOX1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CheatForm";
            this.Text = "CheatForm";
            this.Load += new System.EventHandler(this.CheatForm_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.contextMSText.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtBOX1;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCrop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btCrop;
        private System.Windows.Forms.CheckBox cbCropActive;
        private System.Windows.Forms.ContextMenuStrip contextMSText;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem searchOnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameFaqToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jeuxVideoToolStripMenuItem;
    }
}