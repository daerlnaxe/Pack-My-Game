namespace Pack_My_Game.IHM
{
    partial class InfoScreen
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
            this.Log = new System.Windows.Forms.RichTextBox();
            this.btLeave = new System.Windows.Forms.Button();
            this.btAlive = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Log
            // 
            this.Log.Location = new System.Drawing.Point(12, 47);
            this.Log.Name = "Log";
            this.Log.ReadOnly = true;
            this.Log.Size = new System.Drawing.Size(776, 250);
            this.Log.TabIndex = 0;
            this.Log.Text = "";
            this.Log.UseWaitCursor = true;
            // 
            // btLeave
            // 
            this.btLeave.Location = new System.Drawing.Point(676, 377);
            this.btLeave.Name = "btLeave";
            this.btLeave.Size = new System.Drawing.Size(95, 33);
            this.btLeave.TabIndex = 1;
            this.btLeave.Text = "Leave";
            this.btLeave.UseVisualStyleBackColor = true;
            this.btLeave.UseWaitCursor = true;
            this.btLeave.Visible = false;
            this.btLeave.Click += new System.EventHandler(this.btLeave_Click);
            // 
            // btAlive
            // 
            this.btAlive.Location = new System.Drawing.Point(595, 377);
            this.btAlive.Name = "btAlive";
            this.btAlive.Size = new System.Drawing.Size(75, 33);
            this.btAlive.TabIndex = 2;
            this.btAlive.Text = "KeepAlive";
            this.btAlive.UseVisualStyleBackColor = true;
            this.btAlive.UseWaitCursor = true;
            this.btAlive.Visible = false;
            this.btAlive.Click += new System.EventHandler(this.btAlive_Click);
            // 
            // InfoScreen
            // 
            this.AcceptButton = this.btLeave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btAlive);
            this.Controls.Add(this.btLeave);
            this.Controls.Add(this.Log);
            this.Name = "InfoScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "InfoScreen";
            this.UseWaitCursor = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox Log;
        private System.Windows.Forms.Button btLeave;
        private System.Windows.Forms.Button btAlive;
    }
}