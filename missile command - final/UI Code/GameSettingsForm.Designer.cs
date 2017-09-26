namespace Missile_Command___Final
{
    partial class GameSettingsForm
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
            this.lblControllers = new System.Windows.Forms.Label();
            this.lblPlayerColors = new System.Windows.Forms.Label();
            this.cmbP1 = new System.Windows.Forms.ComboBox();
            this.cmbP2 = new System.Windows.Forms.ComboBox();
            this.cmbP4 = new System.Windows.Forms.ComboBox();
            this.cmbP3 = new System.Windows.Forms.ComboBox();
            this.lblP1 = new System.Windows.Forms.Label();
            this.lblP2 = new System.Windows.Forms.Label();
            this.lblP3 = new System.Windows.Forms.Label();
            this.lblP4 = new System.Windows.Forms.Label();
            this.btnSave_Exit = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.chkSound = new System.Windows.Forms.CheckBox();
            this.chkPlayer1 = new System.Windows.Forms.CheckBox();
            this.chkPlayer3 = new System.Windows.Forms.CheckBox();
            this.chkPlayer2 = new System.Windows.Forms.CheckBox();
            this.chkPlayer4 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblControllers
            // 
            this.lblControllers.AutoSize = true;
            this.lblControllers.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblControllers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblControllers.Location = new System.Drawing.Point(41, 15);
            this.lblControllers.Name = "lblControllers";
            this.lblControllers.Size = new System.Drawing.Size(100, 24);
            this.lblControllers.TabIndex = 0;
            this.lblControllers.Text = "Controllers";
            // 
            // lblPlayerColors
            // 
            this.lblPlayerColors.AutoSize = true;
            this.lblPlayerColors.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerColors.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblPlayerColors.Location = new System.Drawing.Point(28, 103);
            this.lblPlayerColors.Name = "lblPlayerColors";
            this.lblPlayerColors.Size = new System.Drawing.Size(121, 24);
            this.lblPlayerColors.TabIndex = 3;
            this.lblPlayerColors.Text = "Player Colors";
            // 
            // cmbP1
            // 
            this.cmbP1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbP1.FormattingEnabled = true;
            this.cmbP1.Items.AddRange(new object[] {
            "Red",
            "Pink",
            "Orange",
            "Yellow",
            "Gold",
            "Silver",
            "Light Green",
            "Dark Green",
            "Light Blue",
            "Dark Blue",
            "Purple",
            "Violet"});
            this.cmbP1.Location = new System.Drawing.Point(60, 130);
            this.cmbP1.Name = "cmbP1";
            this.cmbP1.Size = new System.Drawing.Size(110, 21);
            this.cmbP1.TabIndex = 4;
            this.cmbP1.SelectedIndexChanged += new System.EventHandler(this.ColorSelector);
            // 
            // cmbP2
            // 
            this.cmbP2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbP2.FormattingEnabled = true;
            this.cmbP2.Items.AddRange(new object[] {
            "Red",
            "Pink",
            "Orange",
            "Yellow",
            "Gold",
            "Silver",
            "Light Green",
            "Dark Green",
            "Light Blue",
            "Dark Blue",
            "Purple",
            "Violet"});
            this.cmbP2.Location = new System.Drawing.Point(60, 157);
            this.cmbP2.Name = "cmbP2";
            this.cmbP2.Size = new System.Drawing.Size(110, 21);
            this.cmbP2.TabIndex = 5;
            this.cmbP2.SelectedIndexChanged += new System.EventHandler(this.ColorSelector);
            // 
            // cmbP4
            // 
            this.cmbP4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbP4.FormattingEnabled = true;
            this.cmbP4.Items.AddRange(new object[] {
            "Red",
            "Pink",
            "Orange",
            "Yellow",
            "Gold",
            "Silver",
            "Light Green",
            "Dark Green",
            "Light Blue",
            "Dark Blue",
            "Purple",
            "Violet"});
            this.cmbP4.Location = new System.Drawing.Point(60, 211);
            this.cmbP4.Name = "cmbP4";
            this.cmbP4.Size = new System.Drawing.Size(110, 21);
            this.cmbP4.TabIndex = 7;
            this.cmbP4.SelectedIndexChanged += new System.EventHandler(this.ColorSelector);
            // 
            // cmbP3
            // 
            this.cmbP3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbP3.FormattingEnabled = true;
            this.cmbP3.Items.AddRange(new object[] {
            "Red",
            "Pink",
            "Orange",
            "Yellow",
            "Gold",
            "Silver",
            "Light Green",
            "Dark Green",
            "Light Blue",
            "Dark Blue",
            "Purple",
            "Violet"});
            this.cmbP3.Location = new System.Drawing.Point(60, 184);
            this.cmbP3.Name = "cmbP3";
            this.cmbP3.Size = new System.Drawing.Size(110, 21);
            this.cmbP3.TabIndex = 6;
            this.cmbP3.SelectedIndexChanged += new System.EventHandler(this.ColorSelector);
            // 
            // lblP1
            // 
            this.lblP1.AutoSize = true;
            this.lblP1.Location = new System.Drawing.Point(9, 133);
            this.lblP1.Name = "lblP1";
            this.lblP1.Size = new System.Drawing.Size(45, 13);
            this.lblP1.TabIndex = 8;
            this.lblP1.Text = "Player 1";
            // 
            // lblP2
            // 
            this.lblP2.AutoSize = true;
            this.lblP2.Location = new System.Drawing.Point(9, 160);
            this.lblP2.Name = "lblP2";
            this.lblP2.Size = new System.Drawing.Size(45, 13);
            this.lblP2.TabIndex = 9;
            this.lblP2.Text = "Player 2";
            // 
            // lblP3
            // 
            this.lblP3.AutoSize = true;
            this.lblP3.Location = new System.Drawing.Point(9, 187);
            this.lblP3.Name = "lblP3";
            this.lblP3.Size = new System.Drawing.Size(45, 13);
            this.lblP3.TabIndex = 10;
            this.lblP3.Text = "Player 3";
            // 
            // lblP4
            // 
            this.lblP4.AutoSize = true;
            this.lblP4.Location = new System.Drawing.Point(9, 214);
            this.lblP4.Name = "lblP4";
            this.lblP4.Size = new System.Drawing.Size(45, 13);
            this.lblP4.TabIndex = 11;
            this.lblP4.Text = "Player 4";
            // 
            // btnSave_Exit
            // 
            this.btnSave_Exit.ForeColor = System.Drawing.Color.Black;
            this.btnSave_Exit.Location = new System.Drawing.Point(12, 261);
            this.btnSave_Exit.Name = "btnSave_Exit";
            this.btnSave_Exit.Size = new System.Drawing.Size(158, 23);
            this.btnSave_Exit.TabIndex = 12;
            this.btnSave_Exit.Text = "Save && Exit";
            this.btnSave_Exit.UseVisualStyleBackColor = true;
            this.btnSave_Exit.Click += new System.EventHandler(this.btnSave_Exit_Click);
            // 
            // btnExit
            // 
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Location = new System.Drawing.Point(12, 290);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(158, 23);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "Exit without saving";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // chkSound
            // 
            this.chkSound.AutoSize = true;
            this.chkSound.Checked = true;
            this.chkSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSound.Location = new System.Drawing.Point(12, 238);
            this.chkSound.Name = "chkSound";
            this.chkSound.Size = new System.Drawing.Size(57, 17);
            this.chkSound.TabIndex = 15;
            this.chkSound.Text = "Sound";
            this.chkSound.UseVisualStyleBackColor = true;
            // 
            // chkPlayer1
            // 
            this.chkPlayer1.AutoSize = true;
            this.chkPlayer1.Location = new System.Drawing.Point(12, 48);
            this.chkPlayer1.Name = "chkPlayer1";
            this.chkPlayer1.Size = new System.Drawing.Size(64, 17);
            this.chkPlayer1.TabIndex = 16;
            this.chkPlayer1.Text = "Player 1";
            this.chkPlayer1.UseVisualStyleBackColor = true;
            // 
            // chkPlayer3
            // 
            this.chkPlayer3.AutoSize = true;
            this.chkPlayer3.Location = new System.Drawing.Point(12, 71);
            this.chkPlayer3.Name = "chkPlayer3";
            this.chkPlayer3.Size = new System.Drawing.Size(64, 17);
            this.chkPlayer3.TabIndex = 17;
            this.chkPlayer3.Text = "Player 3";
            this.chkPlayer3.UseVisualStyleBackColor = true;
            // 
            // chkPlayer2
            // 
            this.chkPlayer2.AutoSize = true;
            this.chkPlayer2.Location = new System.Drawing.Point(106, 48);
            this.chkPlayer2.Name = "chkPlayer2";
            this.chkPlayer2.Size = new System.Drawing.Size(64, 17);
            this.chkPlayer2.TabIndex = 18;
            this.chkPlayer2.Text = "Player 2";
            this.chkPlayer2.UseVisualStyleBackColor = true;
            // 
            // chkPlayer4
            // 
            this.chkPlayer4.AutoSize = true;
            this.chkPlayer4.Location = new System.Drawing.Point(106, 71);
            this.chkPlayer4.Name = "chkPlayer4";
            this.chkPlayer4.Size = new System.Drawing.Size(64, 17);
            this.chkPlayer4.TabIndex = 19;
            this.chkPlayer4.Text = "Player 4";
            this.chkPlayer4.UseVisualStyleBackColor = true;
            // 
            // GameSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(181, 326);
            this.Controls.Add(this.chkPlayer4);
            this.Controls.Add(this.chkPlayer2);
            this.Controls.Add(this.chkPlayer3);
            this.Controls.Add(this.chkPlayer1);
            this.Controls.Add(this.chkSound);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave_Exit);
            this.Controls.Add(this.lblP4);
            this.Controls.Add(this.lblP3);
            this.Controls.Add(this.lblP2);
            this.Controls.Add(this.lblP1);
            this.Controls.Add(this.cmbP4);
            this.Controls.Add(this.cmbP3);
            this.Controls.Add(this.cmbP2);
            this.Controls.Add(this.cmbP1);
            this.Controls.Add(this.lblPlayerColors);
            this.Controls.Add(this.lblControllers);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblControllers;
        private System.Windows.Forms.Label lblPlayerColors;
        private System.Windows.Forms.ComboBox cmbP1;
        private System.Windows.Forms.ComboBox cmbP2;
        private System.Windows.Forms.ComboBox cmbP4;
        private System.Windows.Forms.ComboBox cmbP3;
        private System.Windows.Forms.Label lblP1;
        private System.Windows.Forms.Label lblP2;
        private System.Windows.Forms.Label lblP3;
        private System.Windows.Forms.Label lblP4;
        private System.Windows.Forms.Button btnSave_Exit;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.CheckBox chkSound;
        private System.Windows.Forms.CheckBox chkPlayer1;
        private System.Windows.Forms.CheckBox chkPlayer3;
        private System.Windows.Forms.CheckBox chkPlayer2;
        private System.Windows.Forms.CheckBox chkPlayer4;
    }
}