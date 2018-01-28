namespace missile_command
{
	partial class HighscoresForm
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
			this.btnSave = new System.Windows.Forms.Button();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblScore = new System.Windows.Forms.Label();
			this.btnReturn = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(12, 63);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(100, 23);
			this.btnSave.TabIndex = 0;
			this.btnSave.Text = "Save";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(12, 37);
			this.txtName.MaxLength = 3;
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(100, 20);
			this.txtName.TabIndex = 1;
			this.txtName.Text = "AAA";
			this.txtName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// lblScore
			// 
			this.lblScore.AutoSize = true;
			this.lblScore.Location = new System.Drawing.Point(9, 21);
			this.lblScore.Name = "lblScore";
			this.lblScore.Size = new System.Drawing.Size(47, 13);
			this.lblScore.TabIndex = 2;
			this.lblScore.Text = "Score: 0";
			// 
			// btnReturn
			// 
			this.btnReturn.Location = new System.Drawing.Point(12, 93);
			this.btnReturn.Name = "btnReturn";
			this.btnReturn.Size = new System.Drawing.Size(100, 23);
			this.btnReturn.TabIndex = 3;
			this.btnReturn.Text = "Return";
			this.btnReturn.UseVisualStyleBackColor = true;
			this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.ForeColor = System.Drawing.Color.Firebrick;
			this.lblStatus.Location = new System.Drawing.Point(13, 123);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(0, 13);
			this.lblStatus.TabIndex = 4;
			// 
			// HighscoresForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(421, 365);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.btnReturn);
			this.Controls.Add(this.lblScore);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.btnSave);
			this.Name = "HighscoresForm";
			this.Text = "HighscoresForm";
			this.Load += new System.EventHandler(this.HighscoresForm_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.HighscoresForm_Paint);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblScore;
		private System.Windows.Forms.Button btnReturn;
		private System.Windows.Forms.Label lblStatus;
	}
}