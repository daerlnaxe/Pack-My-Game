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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoScreen));
            this.Log = new System.Windows.Forms.RichTextBox();
            this.btLeave = new System.Windows.Forms.Button();
            this.btAlive = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Log
            // 
            resources.ApplyResources(this.Log, "Log");
            this.Log.Name = "Log";
            this.Log.ReadOnly = true;
            this.Log.UseWaitCursor = true;
            // 
            // btLeave
            // 
            resources.ApplyResources(this.btLeave, "btLeave");
            this.btLeave.Name = "btLeave";
            this.btLeave.UseVisualStyleBackColor = true;
            this.btLeave.UseWaitCursor = true;
            this.btLeave.Click += new System.EventHandler(this.btLeave_Click);
            // 
            // btAlive
            // 
            resources.ApplyResources(this.btAlive, "btAlive");
            this.btAlive.Name = "btAlive";
            this.btAlive.UseVisualStyleBackColor = true;
            this.btAlive.UseWaitCursor = true;
            this.btAlive.Click += new System.EventHandler(this.btAlive_Click);
            // 
            // InfoScreen
            // 
            this.AcceptButton = this.btLeave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btAlive);
            this.Controls.Add(this.btLeave);
            this.Controls.Add(this.Log);
            this.Name = "InfoScreen";
            this.UseWaitCursor = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox Log;
        private System.Windows.Forms.Button btLeave;
        private System.Windows.Forms.Button btAlive;
    }
}