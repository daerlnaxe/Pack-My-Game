namespace Pack_My_Game.IHM
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
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.rtBOX1.Location = new System.Drawing.Point(27, 66);
            this.rtBOX1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rtBOX1.Name = "rtBOX1";
            this.rtBOX1.Size = new System.Drawing.Size(1038, 462);
            this.rtBOX1.TabIndex = 0;
            this.rtBOX1.Text = "";
            this.rtBOX1.WordWrap = false;
            this.rtBOX1.TextChanged += new System.EventHandler(this.rtBOX1_TextChanged);
            this.rtBOX1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rtBOX1_MouseDown);
            // 
            // btSave
            // 
            this.btSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btSave.Location = new System.Drawing.Point(27, 569);
            this.btSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(115, 31);
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
            this.btOK.Location = new System.Drawing.Point(978, 569);
            this.btOK.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(88, 31);
            this.btOK.TabIndex = 2;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            // 
            // btCancel
            // 
            this.btCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(892, 569);
            this.btCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(78, 31);
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter your cheats code";
            // 
            // tbCrop
            // 
            this.tbCrop.Location = new System.Drawing.Point(81, 3);
            this.tbCrop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tbCrop.MaxLength = 3;
            this.tbCrop.Name = "tbCrop";
            this.tbCrop.Size = new System.Drawing.Size(83, 23);
            this.tbCrop.TabIndex = 5;
            this.tbCrop.Text = "150";
            this.tbCrop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbCrop.Click += new System.EventHandler(this.tbCrop_Click);
            this.tbCrop.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCrop_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
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
            this.flowLayoutPanel1.Location = new System.Drawing.Point(572, 25);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(331, 33);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // btCrop
            // 
            this.btCrop.Enabled = false;
            this.btCrop.Location = new System.Drawing.Point(239, 3);
            this.btCrop.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btCrop.Name = "btCrop";
            this.btCrop.Size = new System.Drawing.Size(88, 27);
            this.btCrop.TabIndex = 7;
            this.btCrop.Text = "Crop";
            this.btCrop.UseVisualStyleBackColor = true;
            this.btCrop.Click += new System.EventHandler(this.btCrop_Click);
            // 
            // cbCropActive
            // 
            this.cbCropActive.AutoSize = true;
            this.cbCropActive.Checked = true;
            this.cbCropActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCropActive.Location = new System.Drawing.Point(172, 3);
            this.cbCropActive.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbCropActive.Name = "cbCropActive";
            this.cbCropActive.Size = new System.Drawing.Size(59, 19);
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(119, 6);
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchOnToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1093, 24);
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
            this.gameFaqToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.gameFaqToolStripMenuItem.Text = "GameFaq";
            this.gameFaqToolStripMenuItem.Click += new System.EventHandler(this.gameFaqToolStripMenuItem_Click);
            // 
            // jeuxVideoToolStripMenuItem
            // 
            this.jeuxVideoToolStripMenuItem.Name = "jeuxVideoToolStripMenuItem";
            this.jeuxVideoToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.jeuxVideoToolStripMenuItem.Text = "JeuxVideo";
            this.jeuxVideoToolStripMenuItem.Click += new System.EventHandler(this.jeuxVideoToolStripMenuItem_Click);
            // 
            // CheatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 616);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.btSave);
            this.Controls.Add(this.rtBOX1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
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