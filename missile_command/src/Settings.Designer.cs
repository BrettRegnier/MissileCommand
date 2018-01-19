namespace missile_command
{
	partial class Settings
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
			this.cmbPlayer = new System.Windows.Forms.ComboBox();
			this.lblPlayer = new System.Windows.Forms.Label();
			this.btnApply = new System.Windows.Forms.Button();
			this.btnExit = new System.Windows.Forms.Button();
			this.lblAesethetic = new System.Windows.Forms.Label();
			this.cmbCursor = new System.Windows.Forms.ComboBox();
			this.pbCursor = new System.Windows.Forms.PictureBox();
			this.cmbColor = new System.Windows.Forms.ComboBox();
			this.lblColor = new System.Windows.Forms.Label();
			this.lblControls = new System.Windows.Forms.Label();
			this.chkMouse = new System.Windows.Forms.CheckBox();
			this.lblUp = new System.Windows.Forms.Label();
			this.cmbUp = new System.Windows.Forms.ComboBox();
			this.cmbDown = new System.Windows.Forms.ComboBox();
			this.lblDown = new System.Windows.Forms.Label();
			this.cmbLeft = new System.Windows.Forms.ComboBox();
			this.lblLeft = new System.Windows.Forms.Label();
			this.cmbRight = new System.Windows.Forms.ComboBox();
			this.lblRight = new System.Windows.Forms.Label();
			this.cmbShoot = new System.Windows.Forms.ComboBox();
			this.lblShoot = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pbCursor)).BeginInit();
			this.SuspendLayout();
			// 
			// cmbPlayer
			// 
			this.cmbPlayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbPlayer.FormattingEnabled = true;
			this.cmbPlayer.Items.AddRange(new object[] {
            "Player 1",
            "Player 2",
            "Player 3"});
			this.cmbPlayer.Location = new System.Drawing.Point(12, 37);
			this.cmbPlayer.Name = "cmbPlayer";
			this.cmbPlayer.Size = new System.Drawing.Size(121, 24);
			this.cmbPlayer.TabIndex = 0;
			this.cmbPlayer.SelectedIndexChanged += new System.EventHandler(this.cmbPlayer_SelectedIndexChanged);
			// 
			// lblPlayer
			// 
			this.lblPlayer.AutoSize = true;
			this.lblPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPlayer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.lblPlayer.Location = new System.Drawing.Point(33, 9);
			this.lblPlayer.Name = "lblPlayer";
			this.lblPlayer.Size = new System.Drawing.Size(67, 25);
			this.lblPlayer.TabIndex = 1;
			this.lblPlayer.Text = "Player";
			// 
			// btnApply
			// 
			this.btnApply.Location = new System.Drawing.Point(256, 344);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(75, 23);
			this.btnApply.TabIndex = 2;
			this.btnApply.Text = "Apply";
			this.btnApply.UseVisualStyleBackColor = true;
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// btnExit
			// 
			this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnExit.Location = new System.Drawing.Point(337, 344);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(75, 23);
			this.btnExit.TabIndex = 3;
			this.btnExit.Text = "Exit";
			this.btnExit.UseVisualStyleBackColor = true;
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// lblAesethetic
			// 
			this.lblAesethetic.AutoSize = true;
			this.lblAesethetic.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblAesethetic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.lblAesethetic.Location = new System.Drawing.Point(289, 9);
			this.lblAesethetic.Name = "lblAesethetic";
			this.lblAesethetic.Size = new System.Drawing.Size(93, 25);
			this.lblAesethetic.TabIndex = 4;
			this.lblAesethetic.Text = "Aesthetic";
			// 
			// cmbCursor
			// 
			this.cmbCursor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbCursor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbCursor.FormattingEnabled = true;
			this.cmbCursor.Location = new System.Drawing.Point(152, 37);
			this.cmbCursor.Name = "cmbCursor";
			this.cmbCursor.Size = new System.Drawing.Size(136, 24);
			this.cmbCursor.TabIndex = 5;
			this.cmbCursor.SelectedIndexChanged += new System.EventHandler(this.cmbCursor_SelectedIndexChanged);
			// 
			// pbCursor
			// 
			this.pbCursor.Location = new System.Drawing.Point(294, 45);
			this.pbCursor.Name = "pbCursor";
			this.pbCursor.Size = new System.Drawing.Size(21, 19);
			this.pbCursor.TabIndex = 6;
			this.pbCursor.TabStop = false;
			// 
			// cmbColor
			// 
			this.cmbColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbColor.FormattingEnabled = true;
			this.cmbColor.Location = new System.Drawing.Point(152, 67);
			this.cmbColor.Name = "cmbColor";
			this.cmbColor.Size = new System.Drawing.Size(136, 24);
			this.cmbColor.TabIndex = 7;
			this.cmbColor.SelectedIndexChanged += new System.EventHandler(this.cmbColor_SelectedIndexChanged);
			// 
			// lblColor
			// 
			this.lblColor.AutoSize = true;
			this.lblColor.Location = new System.Drawing.Point(294, 72);
			this.lblColor.Name = "lblColor";
			this.lblColor.Size = new System.Drawing.Size(18, 13);
			this.lblColor.TabIndex = 8;
			this.lblColor.Text = "W";
			// 
			// lblControls
			// 
			this.lblControls.AutoSize = true;
			this.lblControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblControls.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.lblControls.Location = new System.Drawing.Point(295, 127);
			this.lblControls.Name = "lblControls";
			this.lblControls.Size = new System.Drawing.Size(85, 25);
			this.lblControls.TabIndex = 9;
			this.lblControls.Text = "Controls";
			// 
			// chkMouse
			// 
			this.chkMouse.AutoSize = true;
			this.chkMouse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkMouse.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.chkMouse.Location = new System.Drawing.Point(300, 170);
			this.chkMouse.Name = "chkMouse";
			this.chkMouse.Size = new System.Drawing.Size(117, 21);
			this.chkMouse.TabIndex = 10;
			this.chkMouse.Text = "Enable Mouse";
			this.chkMouse.UseVisualStyleBackColor = true;
			// 
			// lblUp
			// 
			this.lblUp.AutoSize = true;
			this.lblUp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.lblUp.Location = new System.Drawing.Point(332, 204);
			this.lblUp.Name = "lblUp";
			this.lblUp.Size = new System.Drawing.Size(21, 13);
			this.lblUp.TabIndex = 11;
			this.lblUp.Text = "Up";
			// 
			// cmbUp
			// 
			this.cmbUp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbUp.FormattingEnabled = true;
			this.cmbUp.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D",
            "E",
            "F",
            "G",
            "H",
            "I",
            "J",
            "K",
            "M",
            "N",
            "O",
            "P",
            "Q",
            "R",
            "S",
            "T",
            "U",
            "V",
            "W",
            "X",
            "Y",
            "Z",
            "Space",
            "Enter",
            "Backspace",
            "Up",
            "Down",
            "Left",
            "Right",
            "LControl",
            "RControl",
            "Alt"});
			this.cmbUp.Location = new System.Drawing.Point(320, 220);
			this.cmbUp.Name = "cmbUp";
			this.cmbUp.Size = new System.Drawing.Size(60, 21);
			this.cmbUp.TabIndex = 12;
			// 
			// cmbDown
			// 
			this.cmbDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbDown.FormattingEnabled = true;
			this.cmbDown.Location = new System.Drawing.Point(347, 260);
			this.cmbDown.Name = "cmbDown";
			this.cmbDown.Size = new System.Drawing.Size(60, 21);
			this.cmbDown.TabIndex = 14;
			// 
			// lblDown
			// 
			this.lblDown.AutoSize = true;
			this.lblDown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.lblDown.Location = new System.Drawing.Point(357, 244);
			this.lblDown.Name = "lblDown";
			this.lblDown.Size = new System.Drawing.Size(35, 13);
			this.lblDown.TabIndex = 13;
			this.lblDown.Text = "Down";
			// 
			// cmbLeft
			// 
			this.cmbLeft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbLeft.FormattingEnabled = true;
			this.cmbLeft.Location = new System.Drawing.Point(255, 260);
			this.cmbLeft.Name = "cmbLeft";
			this.cmbLeft.Size = new System.Drawing.Size(60, 21);
			this.cmbLeft.TabIndex = 16;
			// 
			// lblLeft
			// 
			this.lblLeft.AutoSize = true;
			this.lblLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.lblLeft.Location = new System.Drawing.Point(265, 244);
			this.lblLeft.Name = "lblLeft";
			this.lblLeft.Size = new System.Drawing.Size(25, 13);
			this.lblLeft.TabIndex = 15;
			this.lblLeft.Text = "Left";
			// 
			// cmbRight
			// 
			this.cmbRight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbRight.FormattingEnabled = true;
			this.cmbRight.Location = new System.Drawing.Point(413, 260);
			this.cmbRight.Name = "cmbRight";
			this.cmbRight.Size = new System.Drawing.Size(60, 21);
			this.cmbRight.TabIndex = 18;
			// 
			// lblRight
			// 
			this.lblRight.AutoSize = true;
			this.lblRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.lblRight.Location = new System.Drawing.Point(423, 244);
			this.lblRight.Name = "lblRight";
			this.lblRight.Size = new System.Drawing.Size(32, 13);
			this.lblRight.TabIndex = 17;
			this.lblRight.Text = "Right";
			// 
			// cmbShoot
			// 
			this.cmbShoot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbShoot.FormattingEnabled = true;
			this.cmbShoot.Location = new System.Drawing.Point(294, 309);
			this.cmbShoot.Name = "cmbShoot";
			this.cmbShoot.Size = new System.Drawing.Size(60, 21);
			this.cmbShoot.TabIndex = 20;
			// 
			// lblShoot
			// 
			this.lblShoot.AutoSize = true;
			this.lblShoot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.lblShoot.Location = new System.Drawing.Point(304, 293);
			this.lblShoot.Name = "lblShoot";
			this.lblShoot.Size = new System.Drawing.Size(35, 13);
			this.lblShoot.TabIndex = 19;
			this.lblShoot.Text = "Shoot";
			// 
			// Settings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
			this.ClientSize = new System.Drawing.Size(661, 381);
			this.Controls.Add(this.cmbShoot);
			this.Controls.Add(this.lblShoot);
			this.Controls.Add(this.cmbRight);
			this.Controls.Add(this.lblRight);
			this.Controls.Add(this.cmbLeft);
			this.Controls.Add(this.lblLeft);
			this.Controls.Add(this.cmbDown);
			this.Controls.Add(this.lblDown);
			this.Controls.Add(this.cmbUp);
			this.Controls.Add(this.lblUp);
			this.Controls.Add(this.chkMouse);
			this.Controls.Add(this.lblControls);
			this.Controls.Add(this.lblColor);
			this.Controls.Add(this.cmbColor);
			this.Controls.Add(this.pbCursor);
			this.Controls.Add(this.cmbCursor);
			this.Controls.Add(this.lblAesethetic);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.lblPlayer);
			this.Controls.Add(this.cmbPlayer);
			this.Name = "Settings";
			this.Text = "Settings";
			((System.ComponentModel.ISupportInitialize)(this.pbCursor)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox cmbPlayer;
		private System.Windows.Forms.Label lblPlayer;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Label lblAesethetic;
		private System.Windows.Forms.ComboBox cmbCursor;
		private System.Windows.Forms.PictureBox pbCursor;
		private System.Windows.Forms.ComboBox cmbColor;
		private System.Windows.Forms.Label lblColor;
		private System.Windows.Forms.Label lblControls;
		private System.Windows.Forms.CheckBox chkMouse;
		private System.Windows.Forms.Label lblUp;
		private System.Windows.Forms.ComboBox cmbUp;
		private System.Windows.Forms.ComboBox cmbDown;
		private System.Windows.Forms.Label lblDown;
		private System.Windows.Forms.ComboBox cmbLeft;
		private System.Windows.Forms.Label lblLeft;
		private System.Windows.Forms.ComboBox cmbRight;
		private System.Windows.Forms.Label lblRight;
		private System.Windows.Forms.ComboBox cmbShoot;
		private System.Windows.Forms.Label lblShoot;
	}
}