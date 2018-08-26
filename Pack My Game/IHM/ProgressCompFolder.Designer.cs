namespace Pack_My_Game.IHM
{
    partial class ProgressCompFolder
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
            this.dProgress = new MyControls.DoubleProgress();
            this.SuspendLayout();
            // 
            // dProgress
            // 
            this.dProgress.AutoSize = true;
            this.dProgress.CurrentInfo = "Infos";
            this.dProgress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dProgress.FilesDone = 0;
            this.dProgress.InfoName = "Info:";
            this.dProgress.InfoSup = "label1";
            this.dProgress.Location = new System.Drawing.Point(0, 0);
            this.dProgress.MinimumSize = new System.Drawing.Size(210, 140);
            this.dProgress.Name = "dProgress";
            this.dProgress.Size = new System.Drawing.Size(318, 151);
            this.dProgress.SupInfoName = "Main Info:";
            this.dProgress.TabIndex = 0;
            this.dProgress.TotalFiles = 0;
            // 
            // ProgressCompFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(318, 151);
            this.ControlBox = false;
            this.Controls.Add(this.dProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(320, 190);
            this.Name = "ProgressCompFolder";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "CompressFolder";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal MyControls.DoubleProgress dProgress;
    }
}