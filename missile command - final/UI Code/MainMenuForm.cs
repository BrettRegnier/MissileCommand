using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Missile_Command___Final
{
    public partial class MainMenuForm : Form
    {
        PlayerSettings PSettings;
        LinkerClass Link = new LinkerClass();

        private int NumPlayers;

        private Random rand = new Random();
        private int sendCounter = 0;
        private int sendInterval = 20;
        private bool isAnimating = true;
        private List<Bomb> bmbList = new List<Bomb>();
        private int bmbSpeed = 3;

        public MainMenuForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            lblTitle.Left = (this.ClientSize.Width / 2) - (lblTitle.Width / 2);
            pnlMain.Left = (this.ClientSize.Width / 2) - (pnlMain.Width / 2);
            pnlPlayer.Left = pnlMain.Left;
            pnlDifficulty.Left = pnlMain.Left;

            pnlPlayer.Top = pnlMain.Top;
            pnlDifficulty.Top = pnlMain.Top;

            try
            {
                PSettings = PlayerSettings.LoadSettings();
            }
            catch
            {
                MessageBox.Show("There was no player settings files found (PlayerSettings.XML), a default file will be created");
                PSettings = new PlayerSettings();

                PSettings.P1Color = Color.FromArgb(128, 255, 128).ToArgb();
                PSettings.P2Color = Color.FromArgb(30, 144, 255).ToArgb();
                PSettings.P3Color = Color.FromArgb(255, 255, 128).ToArgb();
                PSettings.P4Color = Color.FromArgb(246, 116, 116).ToArgb();

                PSettings.P1Index = 6;
                PSettings.P2Index = 8;
                PSettings.P3Index = 3;
                PSettings.P4Index = 0;

                PSettings.Player1Controller = false;
                PSettings.Player2Controller = false;
                PSettings.Player3Controller = false;
                PSettings.Player4Controller = false;

                PSettings.soundEnabled = true;

                PSettings.SaveSettings();
            }
        }

        #region Animate

        private void MainMenu_Paint(object sender, PaintEventArgs e)
        {
            for (int i = bmbList.Count -1; i >= 0; i--)
            {
                bmbList[i].Draw(e.Graphics);
            }
        }

        private void timAnimate_Tick(object sender, EventArgs e)
        {
            if (isAnimating)
            {
                // add method of making intervals faster based on how long/points of the player(s)
                if (SendBomb())
                {
                    Point originPoint;
                    Point destinationPoint;
                    if (rand.Next(0, 2) == 1)
                    {
                        destinationPoint = new Point(rand.Next(0, this.ClientSize.Width), this.ClientSize.Height);
                        originPoint = new Point(rand.Next(0, this.Width), 0);
                    }
                    else
                    {
                        destinationPoint = new Point(rand.Next(0, this.Width), 0);
                        originPoint = new Point(rand.Next(0, this.ClientSize.Width), this.ClientSize.Height);
                    }

                    EnemyBomb EBB = new EnemyBomb(originPoint, destinationPoint, this, bmbSpeed,Link, PSettings);
                    EBB.DestroyBomb += RemoveBomb;
                    bmbList.Add(EBB);
                }

                foreach (Bomb bmb in bmbList)
                {
                    bmb.Move();
                }

                this.Invalidate(false);
            }
        }

        private bool SendBomb()
        {
            // Set variable to decrease interval.
            bool sendBomb = false;
            if (sendCounter++ == sendInterval)
            {
                sendCounter = 0;
                sendBomb = true;
            }
            return sendBomb;
        }

        private void RemoveBomb(Bomb bmb)
        {
            bmbList.Remove(bmb);
        }

        #endregion

        #region Main Panel
        private void btnStart_Click(object sender, EventArgs e)
        {
            pnlPlayer.Visible = true;
            pnlMain.Visible = false;
        }

        private void btnHighScores_Click(object sender, EventArgs e)
        {
            this.Hide();
            StopAnimation();
            HighscoreForm highscoreForm = new HighscoreForm();
            highscoreForm.ShowDialog();
            isAnimating = true;
            this.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region PlayerSelect

        private void PlayerSelect(object sender, EventArgs e)
        {
            if (sender == btn1Player)
                NumPlayers = 1;
            else if (sender == btn2Player)
                NumPlayers = 2;
            else if (sender == btn3Player)
                NumPlayers = 3;
            else if (sender == btn4Player)
                NumPlayers = 4;
                        
            pnlPlayer.Visible = false;
            pnlDifficulty.Visible = true;
        }

        private void StartGame(object sender, EventArgs e)
        {
            int difficulty = 0;

            pnlMain.Visible = true;
            pnlDifficulty.Visible = false;
            StopAnimation();

            if (sender == btnEasy)
                difficulty = 0;
            else if (sender == btnNormal)
                difficulty = 1;
            else if (sender == btnHard)
                difficulty = 2;

            this.Hide();
            GameForm Game = new GameForm(NumPlayers, difficulty,PSettings);
            Game.ShowDialog();

            isAnimating = true;
            this.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            pnlMain.Visible = true;
            pnlPlayer.Visible = false;
        }

        #endregion

        #region Difficulty

        //172, 45
        private void btnDifficultyBack_Click(object sender, EventArgs e)
        {
            pnlDifficulty.Visible = false;
            pnlPlayer.Visible = true;
        }

        #endregion

        private void btnSettings_Click(object sender, EventArgs e)
        {
            StopAnimation();
            this.Hide();
            GameSettingsForm gsf = new GameSettingsForm(PSettings);
            gsf.ShowDialog();
            this.Show();
            isAnimating = true;
        }

        private void StopAnimation()
        {
            isAnimating = false;

            for (int i = bmbList.Count - 1; i >= 0; i--)
            {
                bmbList.Remove(bmbList[i]);
            }
        }

        private void btnHowToPlay_Click(object sender, EventArgs e)
        {
            HowToPlayForm HTPF = new HowToPlayForm();
            HTPF.ShowDialog();
        }
    }
}
