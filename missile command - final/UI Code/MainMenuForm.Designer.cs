namespace Missile_Command___Final
{
    partial class MainMenuForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnHighScores = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.pnlPlayer = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btn4Player = new System.Windows.Forms.Button();
            this.btn2Player = new System.Windows.Forms.Button();
            this.btn3Player = new System.Windows.Forms.Button();
            this.btn1Player = new System.Windows.Forms.Button();
            this.timAnimate = new System.Windows.Forms.Timer(this.components);
            this.pnlDifficulty = new System.Windows.Forms.Panel();
            this.btnDifficultyBack = new System.Windows.Forms.Button();
            this.btnNormal = new System.Windows.Forms.Button();
            this.btnHard = new System.Windows.Forms.Button();
            this.btnEasy = new System.Windows.Forms.Button();
            this.btnHowToPlay = new System.Windows.Forms.Button();
            this.pnlMain.SuspendLayout();
            this.pnlPlayer.SuspendLayout();
            this.pnlDifficulty.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lblTitle.Location = new System.Drawing.Point(145, 22);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(153, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Missile Command!";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.btnHowToPlay);
            this.pnlMain.Controls.Add(this.btnSettings);
            this.pnlMain.Controls.Add(this.btnHighScores);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Controls.Add(this.btnStart);
            this.pnlMain.Location = new System.Drawing.Point(172, 45);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(103, 147);
            this.pnlMain.TabIndex = 1;
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(3, 62);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(97, 23);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnHighScores
            // 
            this.btnHighScores.Location = new System.Drawing.Point(3, 33);
            this.btnHighScores.Name = "btnHighScores";
            this.btnHighScores.Size = new System.Drawing.Size(97, 23);
            this.btnHighScores.TabIndex = 2;
            this.btnHighScores.Text = "High Scores";
            this.btnHighScores.UseVisualStyleBackColor = true;
            this.btnHighScores.Click += new System.EventHandler(this.btnHighScores_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(3, 120);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(97, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(3, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(97, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start Game!";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // pnlPlayer
            // 
            this.pnlPlayer.Controls.Add(this.btnBack);
            this.pnlPlayer.Controls.Add(this.btn4Player);
            this.pnlPlayer.Controls.Add(this.btn2Player);
            this.pnlPlayer.Controls.Add(this.btn3Player);
            this.pnlPlayer.Controls.Add(this.btn1Player);
            this.pnlPlayer.Location = new System.Drawing.Point(50, 45);
            this.pnlPlayer.Name = "pnlPlayer";
            this.pnlPlayer.Size = new System.Drawing.Size(103, 149);
            this.pnlPlayer.TabIndex = 3;
            this.pnlPlayer.Visible = false;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(3, 120);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(97, 23);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btn4Player
            // 
            this.btn4Player.Location = new System.Drawing.Point(3, 91);
            this.btn4Player.Name = "btn4Player";
            this.btn4Player.Size = new System.Drawing.Size(97, 23);
            this.btn4Player.TabIndex = 3;
            this.btn4Player.Text = "4 Players";
            this.btn4Player.UseVisualStyleBackColor = true;
            this.btn4Player.Click += new System.EventHandler(this.PlayerSelect);
            // 
            // btn2Player
            // 
            this.btn2Player.Location = new System.Drawing.Point(3, 33);
            this.btn2Player.Name = "btn2Player";
            this.btn2Player.Size = new System.Drawing.Size(97, 23);
            this.btn2Player.TabIndex = 2;
            this.btn2Player.Text = "2 Players";
            this.btn2Player.UseVisualStyleBackColor = true;
            this.btn2Player.Click += new System.EventHandler(this.PlayerSelect);
            // 
            // btn3Player
            // 
            this.btn3Player.Location = new System.Drawing.Point(3, 62);
            this.btn3Player.Name = "btn3Player";
            this.btn3Player.Size = new System.Drawing.Size(97, 23);
            this.btn3Player.TabIndex = 1;
            this.btn3Player.Text = "3 Players";
            this.btn3Player.UseVisualStyleBackColor = true;
            this.btn3Player.Click += new System.EventHandler(this.PlayerSelect);
            // 
            // btn1Player
            // 
            this.btn1Player.Location = new System.Drawing.Point(3, 4);
            this.btn1Player.Name = "btn1Player";
            this.btn1Player.Size = new System.Drawing.Size(97, 23);
            this.btn1Player.TabIndex = 0;
            this.btn1Player.Text = "1 Player";
            this.btn1Player.UseVisualStyleBackColor = true;
            this.btn1Player.Click += new System.EventHandler(this.PlayerSelect);
            // 
            // timAnimate
            // 
            this.timAnimate.Enabled = true;
            this.timAnimate.Interval = 50;
            this.timAnimate.Tick += new System.EventHandler(this.timAnimate_Tick);
            // 
            // pnlDifficulty
            // 
            this.pnlDifficulty.Controls.Add(this.btnDifficultyBack);
            this.pnlDifficulty.Controls.Add(this.btnNormal);
            this.pnlDifficulty.Controls.Add(this.btnHard);
            this.pnlDifficulty.Controls.Add(this.btnEasy);
            this.pnlDifficulty.Location = new System.Drawing.Point(304, 45);
            this.pnlDifficulty.Name = "pnlDifficulty";
            this.pnlDifficulty.Size = new System.Drawing.Size(103, 149);
            this.pnlDifficulty.TabIndex = 5;
            this.pnlDifficulty.Visible = false;
            // 
            // btnDifficultyBack
            // 
            this.btnDifficultyBack.Location = new System.Drawing.Point(3, 120);
            this.btnDifficultyBack.Name = "btnDifficultyBack";
            this.btnDifficultyBack.Size = new System.Drawing.Size(97, 23);
            this.btnDifficultyBack.TabIndex = 4;
            this.btnDifficultyBack.Text = "Back";
            this.btnDifficultyBack.UseVisualStyleBackColor = true;
            this.btnDifficultyBack.Click += new System.EventHandler(this.btnDifficultyBack_Click);
            // 
            // btnNormal
            // 
            this.btnNormal.Location = new System.Drawing.Point(3, 33);
            this.btnNormal.Name = "btnNormal";
            this.btnNormal.Size = new System.Drawing.Size(97, 23);
            this.btnNormal.TabIndex = 2;
            this.btnNormal.Text = "Normal";
            this.btnNormal.UseVisualStyleBackColor = true;
            this.btnNormal.Click += new System.EventHandler(this.StartGame);
            // 
            // btnHard
            // 
            this.btnHard.Location = new System.Drawing.Point(3, 62);
            this.btnHard.Name = "btnHard";
            this.btnHard.Size = new System.Drawing.Size(97, 23);
            this.btnHard.TabIndex = 1;
            this.btnHard.Text = "Hard";
            this.btnHard.UseVisualStyleBackColor = true;
            this.btnHard.Click += new System.EventHandler(this.StartGame);
            // 
            // btnEasy
            // 
            this.btnEasy.Location = new System.Drawing.Point(3, 4);
            this.btnEasy.Name = "btnEasy";
            this.btnEasy.Size = new System.Drawing.Size(97, 23);
            this.btnEasy.TabIndex = 0;
            this.btnEasy.Text = "Easy";
            this.btnEasy.UseVisualStyleBackColor = true;
            this.btnEasy.Click += new System.EventHandler(this.StartGame);
            // 
            // btnHowToPlay
            // 
            this.btnHowToPlay.Location = new System.Drawing.Point(3, 91);
            this.btnHowToPlay.Name = "btnHowToPlay";
            this.btnHowToPlay.Size = new System.Drawing.Size(97, 23);
            this.btnHowToPlay.TabIndex = 4;
            this.btnHowToPlay.Text = "How To Play";
            this.btnHowToPlay.UseVisualStyleBackColor = true;
            this.btnHowToPlay.Click += new System.EventHandler(this.btnHowToPlay_Click);
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(468, 331);
            this.Controls.Add(this.pnlDifficulty);
            this.Controls.Add(this.pnlPlayer);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Missile Command";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainMenu_Paint);
            this.pnlMain.ResumeLayout(false);
            this.pnlPlayer.ResumeLayout(false);
            this.pnlDifficulty.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Button btnHighScores;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Panel pnlPlayer;
        private System.Windows.Forms.Button btn4Player;
        private System.Windows.Forms.Button btn2Player;
        private System.Windows.Forms.Button btn3Player;
        private System.Windows.Forms.Button btn1Player;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Timer timAnimate;
        private System.Windows.Forms.Panel pnlDifficulty;
        private System.Windows.Forms.Button btnDifficultyBack;
        private System.Windows.Forms.Button btnNormal;
        private System.Windows.Forms.Button btnHard;
        private System.Windows.Forms.Button btnEasy;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnHowToPlay;
    }
}