namespace Pack_My_Game.IHM
{
    partial class ZipComp_Progress
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
            this.totalProgress = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.entryProgress = new System.Windows.Forms.ProgressBar();
            this.lbFiles = new System.Windows.Forms.Label();
            this.lbEntryP = new System.Windows.Forms.Label();
            this.lbTotalP = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelTask = new System.Windows.Forms.Label();
            this.lbInfos = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // totalProgress
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.totalProgress, 2);
            this.totalProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totalProgress.Location = new System.Drawing.Point(13, 84);
            this.totalProgress.Name = "totalProgress";
            this.totalProgress.Size = new System.Drawing.Size(418, 30);
            this.totalProgress.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Controls.Add(this.entryProgress, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.totalProgress, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbFiles, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbEntryP, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbTotalP, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 131);
            this.tableLayoutPanel1.TabIndex = 2;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // entryProgress
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.entryProgress, 2);
            this.entryProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.entryProgress.Location = new System.Drawing.Point(13, 48);
            this.entryProgress.Name = "entryProgress";
            this.entryProgress.Size = new System.Drawing.Size(418, 30);
            this.entryProgress.TabIndex = 1;
            // 
            // lbFiles
            // 
            this.lbFiles.AutoSize = true;
            this.lbFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFiles.Location = new System.Drawing.Point(437, 10);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.Size = new System.Drawing.Size(44, 35);
            this.lbFiles.TabIndex = 2;
            this.lbFiles.Text = "0/0";
            this.lbFiles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbEntryP
            // 
            this.lbEntryP.AutoSize = true;
            this.lbEntryP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbEntryP.Location = new System.Drawing.Point(437, 45);
            this.lbEntryP.Name = "lbEntryP";
            this.lbEntryP.Size = new System.Drawing.Size(44, 36);
            this.lbEntryP.TabIndex = 4;
            this.lbEntryP.Text = "100%";
            this.lbEntryP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbTotalP
            // 
            this.lbTotalP.AutoSize = true;
            this.lbTotalP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbTotalP.Location = new System.Drawing.Point(437, 81);
            this.lbTotalP.Name = "lbTotalP";
            this.lbTotalP.Size = new System.Drawing.Size(44, 36);
            this.lbTotalP.TabIndex = 5;
            this.lbTotalP.Text = "100%";
            this.lbTotalP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.labelTask);
            this.panel1.Controls.Add(this.lbInfos);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 29);
            this.panel1.TabIndex = 6;
            // 
            // labelTask
            // 
            this.labelTask.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTask.Location = new System.Drawing.Point(9, 4);
            this.labelTask.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.labelTask.Name = "labelTask";
            this.labelTask.Size = new System.Drawing.Size(35, 25);
            this.labelTask.TabIndex = 3;
            this.labelTask.Text = "Task:";
            this.labelTask.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbInfos
            // 
            this.lbInfos.AutoSize = true;
            this.lbInfos.Location = new System.Drawing.Point(44, 10);
            this.lbInfos.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.lbInfos.Name = "lbInfos";
            this.lbInfos.Size = new System.Drawing.Size(30, 13);
            this.lbInfos.TabIndex = 4;
            this.lbInfos.Text = "Infos";
            this.lbInfos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Compression_Progress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 131);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 170);
            this.Name = "Compression_Progress";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Compression_Progress";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar totalProgress;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ProgressBar entryProgress;
        private System.Windows.Forms.Label lbFiles;
        private System.Windows.Forms.Label labelTask;
        private System.Windows.Forms.Label lbEntryP;
        private System.Windows.Forms.Label lbTotalP;
        private System.Windows.Forms.Label lbInfos;
        private System.Windows.Forms.Panel panel1;
    }
}