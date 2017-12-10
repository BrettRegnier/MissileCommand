using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Missile_Command___Final
{
    public partial class GameForm : Form
    {
        private PlayerSettings PSettings;
        private SoundPlayer GameMusic = new SoundPlayer(@"Music.wav");

        // Lists
        private List<DefenseTower> TowerList = new List<DefenseTower>();
        private List<Building> BuildingList = new List<Building>();
        private List<Bomb> BombList = new List<Bomb>();
        private List<Ship> ShipList = new List<Ship>();
        private List<Cursor> CursorList = new List<Cursor>();


        private InGameMenu myStartMenu;
        private bool isPlaying = true;
        private Random rand = new Random();
        private int numPlayers = 0;
        private int playerVar = 0;
        private int destroyedCount;
        private bool gameOver = false;

        private long Score = 0;

        private int numBuildings = 0;

        // Timer that will close the form
        private Timer closeTimer = new Timer();

        // Enemy Bombs
        private int speed = 3;
        private int sendCounter = 0;
        private int sendShipBombCount = 0;
        private int sendInterval = 90;
        private int shipSender = 0; // When this reaches a certain value it will send a ship.

        private int speedInterval = 0;
        private int sendModifier = 0;
        private int difficultyIncrease = 0;
        private int difficultyModifier = 1;
        private int difficultyInterval = 5;

        private Graphics g;
        private LinkerClass Link;
        private Rectangle groundRec;

        private const int BAR_SIZE = 40;

        /// <summary>
        /// Sets the difficulty when the form gets initalized.
        /// </summary>
        public GameForm(int NumPlayers, int difficulty, PlayerSettings playerSettings)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            PSettings = playerSettings;

            this.numPlayers = NumPlayers;

            difficultyModifier = difficulty * numPlayers;  // 0 Easy, 1 Normal, 2 Hard.

            // Dont laugh its for curses.
            if (difficultyModifier < difficultyModifier * 3)
                difficultyInterval = difficultyModifier * 3 + 4;
        }

        /// <summary>
        /// Sets up the form.
        /// </summary>
        private void CommandForm_Load(object sender, EventArgs e)
        {
            try
            {
                Link = new LinkerClass();

                // Maximize and hide everything else
                this.StartPosition = 0;
                int height = Screen.PrimaryScreen.Bounds.Height;
                int width = Screen.PrimaryScreen.Bounds.Width;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

                this.ClientSize = new Size(width, height);
                //this.ClientSize = new Size(1366, 768);

                //Bring them back in when it is all done.
                MakeMenus(); // Seperate File
                AddPlayerObjects(); // Seperate File
                AddBuildings(); // Seperate File

                numBuildings = BuildingList.Count + TowerList.Count;

                // Once everything is initialized, enable timer.
                GameTimer.Enabled = true;

                // Don't laugh it's for curses!
                groundRec = new Rectangle(-1, this.ClientSize.Height - BAR_SIZE, this.ClientSize.Width + 1, BAR_SIZE);

                if (PSettings.soundEnabled)
                    GameMusic.PlayLooping();


								Point originPoint = new Point(100, 0);
								EnemyBomb EBB = new EnemyBomb(originPoint, new Point(200, 1035), this, speed, Link, PSettings);
			}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// DRAWS EVERY FRICKEN THING TO THE DARN FORM
        /// </summary>
        private void CommandForm_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                g = e.Graphics;

                for (int i = TowerList.Count - 1; i >= 0; i--)
                {
                    TowerList[i].Draw(e.Graphics);
                    CursorList[i].Draw(e.Graphics);
                }

                // Had to draw it here, because it needs to be drawn after the tower but before the health bars.
                g.FillRectangle(Brushes.White, groundRec);

                // Had to seperate the health drawing because it would only draw the ground over top of the health bars.
                for (int i = TowerList.Count - 1; i >= 0; i--)
                {
                    TowerList[i].DrawHealth(e.Graphics);
                }

                // This needs to be drawn after the tower so that it sits overtop the extra part on the bottom.
                //g.FillRectangle(Brushes.White, groundRec);

                for (int i = BombList.Count - 1; i >= 0; i--)
                {
                    BombList[i].Draw(e.Graphics);
                }

                for (int i = BuildingList.Count - 1; i >= 0; i--)
                {
                    BuildingList[i].Draw(e.Graphics);
                }

                for (int i = ShipList.Count - 1; i >= 0; i--)
                {
                    ShipList[i].Draw(e.Graphics);
                }

                g.DrawString("Money: " + Link.currentMoney.ToString("C"), new Font("Arial", 14), Brushes.Red, 10, 10);
                g.DrawString("Score: " + Score.ToString(), new Font("Arial", 14), Brushes.Red, 10, 50);

                if (gameOver)
                {
                    g.DrawString("Game Over", new Font("Arial", 30), Brushes.Red, this.Width / 2 - 115, this.Height / 2 - 125);
                    //System.Threading.Thread.Sleep(5000); /// NONE OF THESE WORK. >:(
                    //Task.Delay(TimeSpan.FromSeconds(1));
                }
            }
            catch
            {
                isPlaying = false;
                MessageBox.Show("Game couldn't draw any grahpics, shutting down.");
                this.Close();
            }
        }

        /// <summary>
        /// Returns the point of a building to be attacked.
        /// </summary>
        private Point GetDestination()
        {
            Point destinationPoint;
            int attackVar = 0;
            int attackX = 0;

            // This makes the bombs guarentee to aim for a building.
            attackVar = rand.Next(0, numBuildings);

            if (attackVar < BuildingList.Count)
            {
                attackX = BuildingList[attackVar].PosX + rand.Next(-50, 51);
            }
            else
            {
                attackVar -= BuildingList.Count;
                attackX = TowerList[attackVar].PosX + rand.Next(-50, 51);
            }

            // The 5 is half the size of the bomb.............................
            return destinationPoint = new Point(attackX, this.ClientSize.Height - (BAR_SIZE + 5)); // Make the bombs track the location of the buildings by random.
            //destinationPoint = new Point(rand.Next(0, this.ClientSize.Width), this.ClientSize.Height - (BAR_SIZE + 5)); // Make the bombs track the location of the buildings by random.
        }

        /// <summary>
        /// Closes the form, and makes sure nothing gets repair any further.
        /// </summary>
        private void closeTimer_Tick(object sender, EventArgs e)
        {
            foreach (Building build in BuildingList)
                build.RebuildEnabled = false;

            foreach (DefenseTower tower in TowerList)
                tower.RebuildEnabled = false;

            closeTimer.Enabled = false;
            this.Close();
        }

        /// <summary>
        /// Does all the important things such as checking for game over. Sends bombs and ships and moves them all.
        /// Checks to see if the enemy bombs have collided with a builidng or a bomb.
        /// Checks to see if the player boms collided with ships too.
        /// Checks for keypresses as well.
        /// </summary>
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (isPlaying)
                {
                    if (destroyedCount == BuildingList.Count)
                    {
                        gameOver = true;
                        this.Invalidate(false);
                        ChangeGameState();

                        closeTimer.Interval = 5000;
                        closeTimer.Tick += closeTimer_Tick;
                        closeTimer.Enabled = true; // Maybe remove the timer and wait for player input of enter.
                    }

                    // Add method of making intervals faster based on how long/points of the player(s)
                    if (SendBomb())
                    {
                        Point originPoint = new Point(rand.Next(0, this.Width), 0);

                        EnemyBomb EBB = new EnemyBomb(originPoint, GetDestination(), this, speed, Link, PSettings);
                        EBB.DestroyBomb += RemoveBomb;
                        BombList.Add(EBB);
                    }

                    if (SendShip())
                    {
                        Ship shp = new Ship(this.Width, this.Height, Link);
                        shp.DestroyShip += Shp_DestroyShip;
                        ShipList.Add(shp);
                    }

                    for (int i = ShipList.Count - 1; i >= 0; i--)
                    {
                        ShipList[i].Move();

                        if (SendShipBomb())
                        {
                            Point originPoint = new Point(ShipList[i].PosX, ShipList[i].PosY + ShipList[i].Height);

                            EnemyBomb EBB = new EnemyBomb(originPoint, GetDestination(), this, speed, Link, PSettings);
                            EBB.DestroyBomb += RemoveBomb;
                            BombList.Add(EBB);
                        }
                    }

                    for (int i = BombList.Count - 1; i >= 0; i--)
                    {
                        BombList[i].Move();
                    }

                    for (int i = 0; i < BombList.Count; i++)
                    {
                        for (int j = 0; j < BombList.Count; j++)
                        {
                            if ((BombList[i] is EnemyBomb && BombList[j] is PlayerBomb) || (BombList[i] is PlayerBomb && BombList[j] is EnemyBomb))
                            {
                                if (BombCollision(i, j))
                                {
                                    if (BombList[i] is EnemyBomb)
                                    {
                                        if (((EnemyBomb)BombList[i]).MoneyRecieved == false)
                                        {
                                            Link.currentMoney += Link.bountryReward;
                                            Score += 25 + 10 * difficultyModifier;
                                        }
                                    }

                                    if (BombList[j] is EnemyBomb)
                                    {
                                        if (((EnemyBomb)BombList[j]).MoneyRecieved == false)
                                        {
                                            Link.currentMoney += Link.bountryReward;
                                            Score += 25;
                                        }
                                    }

                                    BombList[i].collided();
                                    BombList[j].collided();
                                }
                            }
                        }
                    }

                    for (int i = 0; i < BombList.Count; i++)
                    {
                        for (int j = 0; j < BuildingList.Count; j++)
                        {
                            if (BombList[i] is EnemyBomb)
                            {
                                if (BuildingList[j].Collidable && BuildingCollision(i, j))
                                {
                                    BombList[i].collided();
                                    BuildingList[j].Collided();

                                    if (BuildingList[j].Destroyed)
                                        destroyedCount++;
                                }
                            }
                        }
                    }

                    for (int i = 0; i < BombList.Count; i++)
                    {
                        for (int j = 0; j < TowerList.Count; j++)
                        {
                            if (BombList[i] is EnemyBomb)
                            {
                                if (TowerList[j].Collidable && TowerCollision(i, j))
                                {
                                    BombList[i].collided();
                                    TowerList[j].Collided();
                                }
                            }
                        }
                    }

                    for (int i = 0; i < BombList.Count; i++)
                    {
                        for (int j = 0; j < ShipList.Count; j++)
                        {
                            if (BombList[i] is PlayerBomb)
                            {
                                if (ShipCollision(i, j))
                                {
                                    Score += 100;
                                    BombList[i].collided();
                                    ShipList[j].Collided();
                                }
                            }
                        }
                    }

                    MoveCursors();
                }

                this.Invalidate(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something happened " + ex.Message);
            }
        }

        /// <summary>
        /// Checks for a collision between bombs.
        /// </summary>
        private bool BombCollision(int i, int j)
        {
            if (BombList[i].bombPositionX + BombList[i].bombWidth < BombList[j].bombPositionX)
                return false;
            if (BombList[j].bombPositionX + BombList[j].bombWidth < BombList[i].bombPositionX)
                return false;
            if (BombList[i].bombPositionY + BombList[i].bombHeight < BombList[j].bombPositionY)
                return false;
            if (BombList[j].bombPositionY + BombList[j].bombHeight < BombList[i].bombPositionY)
                return false;

            return true;

            // didnt work
            //bool collided = false;
            //int diffX = BombList[i].bombCenterPoint.X - BombList[j].bombCenterPoint.X;
            //int diffY = BombList[i].bombCenterPoint.Y - BombList[j].bombCenterPoint.Y;

            //double c = Math.Sqrt(Math.Pow(diffX, 2) + Math.Pow(diffY, 2));

            //if (c < BombList[i].BombRadius + BombList[j].BombRadius)
            //    collided = true;

            //return collided;
        }

        /// <summary>
        /// Checks to see if the enemy bombs have collided with a building
        /// </summary>
        private bool BuildingCollision(int i, int j)
        {
            if (BombList[i].bombPositionX + BombList[i].bombWidth < BuildingList[j].PosX)
                return false;
            if (BuildingList[j].PosX + BuildingList[j].Width < BombList[i].bombPositionX)
                return false;
            if (BombList[i].bombPositionY + BombList[i].bombHeight < BuildingList[j].PosY)
                return false;
            if (BuildingList[j].PosY + BuildingList[j].Height < BombList[i].bombPositionY)
                return false;

            return true;
        }

        /// <summary>
        /// Checks for a collision with a tower.
        /// </summary>
        private bool TowerCollision(int i, int j)
        {
            bool collided = false;
            int diffX = TowerList[j].Origin.X - BombList[i].bombCenterPoint.X;
            int diffY = TowerList[j].Origin.Y - BombList[i].bombCenterPoint.Y;

            double c = Math.Sqrt(Math.Pow(diffX, 2) + Math.Pow(diffY, 2));

            if (c < TowerList[j].TurretRadius + ((EnemyBomb)BombList[i]).BombRadius)
                collided = true;

            return collided;
        }

        /// <summary>
        /// Checks for a collision with a ship.
        /// </summary>
        private bool ShipCollision(int i, int j)
        {
            if (BombList[i].bombPositionX + BombList[i].bombWidth < ShipList[j].PosX)
                return false;
            if (ShipList[j].PosX + ShipList[j].Width < BombList[i].bombPositionX)
                return false;
            if (BombList[i].bombPositionY + BombList[i].bombHeight < ShipList[j].PosY)
                return false;
            if (ShipList[j].PosY + ShipList[j].Height < BombList[i].bombPositionY)
                return false;

            return true;
        }

        /// <summary>
        /// Remove ship from shipList
        /// </summary>
        private void Shp_DestroyShip(Ship shp)
        {
            ShipList.Remove(shp);
        }

        /// <summary>
        /// Remove a bomb from bomblist
        /// </summary>
        private void RemoveBomb(Bomb bmb)
        {
            BombList.Remove(bmb);
        }

        /// <summary>
        /// Does some calculation and what not to make the game harder and send a bomb on an interval.
        /// </summary>
        private bool SendBomb()
        {
            // Set variable to decrease interval.
            bool sendBomb = false;
            if (sendCounter++ == (sendInterval - sendModifier))
            {
                shipSender += 1;
                sendCounter = 0;
                sendBomb = true;

                // Every 10 bombs its gets harder.
                if (difficultyIncrease++ == (difficultyInterval - (difficultyModifier * 2)))
                {
                    difficultyIncrease = 0;

                    if (speedInterval++ == (5 - difficultyModifier))
                    {
                        if (speed <= 10)
                        {
                            speed++;
                        }

                        speedInterval = 0;
                    }

                    if (sendModifier < 70)
                    {
                        sendModifier += 1 + (difficultyModifier * 3);
                    }
                }

                // Impossible difficulty Wtf?
                //if (sendCounter++ == (sendInterval - sendInterval))
                //{
                //    sendCounter = 0;
                //    sendBomb = true;

                //    // Every 10 bombs its gets harder.
                //    if (difficultyIncrease++ == (difficultyInterval - (difficultyModifier * 3)))
                //    {
                //        difficultyIncrease = 0;

                //        if (speed <= 15)
                //        {
                //            speed++;
                //        }

                //        if (sendInterval < 70)
                //        {
                //            sendInterval++;
                //        }
                //    }
            }
            return sendBomb;
        }

        /// <summary>
        /// Sends a ship every 15 bombs.
        /// </summary>
        private bool SendShip()
        {
            bool send;
            if (shipSender == 15)
            {
                send = true;
                shipSender = 0;
            }
            else
                send = false;

            return send;
        }

        /// <summary>
        /// Sends a ship every few ticks.
        /// </summary>
        private bool SendShipBomb()
        {
            bool sendBomb = false;
            if (sendShipBombCount++ >= (sendInterval - sendModifier))
            {
                sendShipBombCount = 0;
                sendBomb = true;
            }

            return sendBomb;
        }

        /// <summary>
        /// Pauses the game or unpauses it depending whichever is setup.
        /// </summary>
        public void ChangeGameState()
        {
            if (engineerMenu.Visible)
                engineerMenu.Visible = false;
            if (incomeMenu.Visible)
                incomeMenu.Visible = false;
            if (researchMenu.Visible)
                researchMenu.Visible = false;

            isPlaying = !isPlaying;
            Link.isPlaying = isPlaying;
            this.Focus();
        }

        /// <summary>
        /// Shows the highscore form when game over.
        /// </summary>
        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            GameMusic.Stop();
            if (gameOver)
            {
                HighscoreForm highscore = new HighscoreForm(Score);
                highscore.ShowDialog();
            }
        }
    }
}
