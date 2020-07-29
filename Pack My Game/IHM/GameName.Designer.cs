namespace Pack_My_Game.IHM
{
    partial class GameName
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
            this.lDescription = new System.Windows.Forms.Label();
            this.tbCurrentName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lGameName = new System.Windows.Forms.Label();
            this.lChoosenName = new System.Windows.Forms.Label();
            this.tbChosenName = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btChange = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lDescription
            // 
            this.lDescription.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.lDescription, 2);
            this.lDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lDescription.Location = new System.Drawing.Point(3, 0);
            this.lDescription.Name = "lDescription";
            this.lDescription.Size = new System.Drawing.Size(648, 13);
            this.lDescription.TabIndex = 0;
            this.lDescription.Text = "Description de l\'ihm";
            // 
            // tbCurrentName
            // 
            this.tbCurrentName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCurrentName.Enabled = false;
            this.tbCurrentName.Location = new System.Drawing.Point(73, 16);
            this.tbCurrentName.Name = "tbCurrentName";
            this.tbCurrentName.Size = new System.Drawing.Size(578, 20);
            this.tbCurrentName.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.lDescription, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbCurrentName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lGameName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lChoosenName, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbChosenName, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // lGameName
            // 
            this.lGameName.AutoSize = true;
            this.lGameName.Location = new System.Drawing.Point(3, 13);
            this.lGameName.Name = "lGameName";
            this.lGameName.Size = new System.Drawing.Size(64, 13);
            this.lGameName.TabIndex = 2;
            this.lGameName.Text = "Nom du Jeu";
            this.lGameName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lChoosenName
            // 
            this.lChoosenName.AutoSize = true;
            this.lChoosenName.Location = new System.Drawing.Point(3, 39);
            this.lChoosenName.Name = "lChoosenName";
            this.lChoosenName.Size = new System.Drawing.Size(60, 13);
            this.lChoosenName.TabIndex = 3;
            this.lChoosenName.Text = "Nom Choisi";
            // 
            // tbChosenName
            // 
            this.tbChosenName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbChosenName.Location = new System.Drawing.Point(73, 42);
            this.tbChosenName.Name = "tbChosenName";
            this.tbChosenName.Size = new System.Drawing.Size(578, 20);
            this.tbChosenName.TabIndex = 4;
            this.tbChosenName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbChosenName_KeyPress);
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 3);
            this.flowLayoutPanel1.Controls.Add(this.btChange);
            this.flowLayoutPanel1.Controls.Add(this.btCancel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 413);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(794, 34);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // btChange
            // 
            this.btChange.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btChange.Location = new System.Drawing.Point(716, 3);
            this.btChange.Name = "btChange";
            this.btChange.Size = new System.Drawing.Size(75, 23);
            this.btChange.TabIndex = 0;
            this.btChange.Text = "Validation";
            this.btChange.UseVisualStyleBackColor = true;
            this.btChange.Click += new System.EventHandler(this.btChange_Click);
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Location = new System.Drawing.Point(635, 3);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 1;
            this.btCancel.Text = "Annulation";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // GameName
            // 
            this.AcceptButton = this.btChange;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btCancel;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameName";
            this.Load += new System.EventHandler(this.GameName_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lDescription;
        private System.Windows.Forms.TextBox tbCurrentName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lGameName;
        private System.Windows.Forms.Label lChoosenName;
        private System.Windows.Forms.TextBox tbChosenName;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btChange;
        private System.Windows.Forms.Button btCancel;
    }
}