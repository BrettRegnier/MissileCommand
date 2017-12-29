using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Missile_Command___Final
{
    abstract class Building
    {
        #region Fields

        public delegate void Rebuilt();
        public event Rebuilt RebuiltHandler;

        protected int destroyImageNum;
        protected int curHealth;
        protected Point OriginPoint;
        protected static List<PictureBox> buildingImages = new List<PictureBox>();
        protected PictureBox mainImage = new PictureBox();

        protected bool isDestroyed = false;

        protected LinkerClass Link;
        protected int collidedCount = 0;
        protected bool isCollidable = true;

        protected const int SCREEN_OFFSET = 25;
        protected const int BAR_SIZE = 40;

        protected Timer rebuildTimer = new Timer();
        protected Timer repairTimer = new Timer();

        protected Rectangle baseBar;
        protected Rectangle HealthBar;

        protected enum eBuilding
        {
            Generic = 0,
            Income,
            Research,
            Engineering
        };

        #endregion

        #region Constructors

        /// <summary>
        /// Loads up the images for all of the buildings and their destroyed counterparts.
        /// </summary>
        static Building()
        {
            try
            {
                for (int i = 0; i < 4; i++)
                {
                    buildingImages.Add(new PictureBox());
                    buildingImages[buildingImages.Count - 1].Image = new Bitmap("Building_" + i.ToString() + ".png");
                }

                for (int i = 0; i < 4; i++)
                {
                    buildingImages.Add(new PictureBox());
                    buildingImages[buildingImages.Count - 1].Image = new Bitmap("destroyedBuilding_" + i.ToString() + ".png");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Sets up each building will their own origin rebuild timer and repair timer, along with their own health and link them.
        /// </summary>
        public Building(Point originPoint, LinkerClass link, int formWidth, int formHeight)
        {
            Link = link;
            curHealth = link.currentHealth;
            mainImage.SizeMode = PictureBoxSizeMode.AutoSize;
            //this.originPoint = originPoint;
            this.OriginPoint.X = originPoint.X;
            this.OriginPoint.Y = originPoint.Y - BAR_SIZE;

            rebuildTimer.Interval = 45000; // 30000
            rebuildTimer.Tick += rebuildTimer_Tick;
            rebuildTimer.Enabled = false;

            repairTimer.Interval = link.repairSpeed;
            repairTimer.Tick += repairTimer_Tick;
            repairTimer.Enabled = true;

            baseBar = new Rectangle(this.OriginPoint.X + 2, this.OriginPoint.Y + 14, 102, 17);
        }

        #endregion

        #region Properties

        public virtual int PosX { get { return mainImage.Left; } }
        public virtual int PosY { get { return mainImage.Top; } }
        public virtual int Width { get { return mainImage.Width; } }
        public virtual int Height { get { return mainImage.Height; } }
        public bool Destroyed { get { return isDestroyed; } }
        public bool Collidable { get { return isCollidable; } }
        public bool RebuildEnabled { set { rebuildTimer.Enabled = value; } }

        #endregion  

        #region Public Methods

        /// <summary>
        /// Draws each building and their health bar just slightly below it.
        /// </summary>
        public virtual void Draw(Graphics g)
        {
            g.DrawImage(mainImage.Image, new Point(mainImage.Left, mainImage.Top - BAR_SIZE));
            //g.DrawImage(mainImage.Image, new Point(mainImage.Left, mainImage.Top));
            g.FillRectangle(Brushes.Black, baseBar);

            int barSize = (int)(((double)curHealth / (double)Link.maxHealth) * 100);
            HealthBar = new Rectangle(OriginPoint.X + 3, OriginPoint.Y + 15, barSize, 15);
            g.FillRectangle(Brushes.Red, HealthBar);
        }

        /// <summary>
        /// Repairs the building only while damaged and while the game is playing
        /// </summary>
        public void repairTimer_Tick(object sender, EventArgs e)
        {
            if (Link.isPlaying)
            {
                if (curHealth != Link.maxHealth)
                {
                    if (curHealth + Link.repairAmt > Link.maxHealth)
                    {
                        curHealth = Link.maxHealth;
                    }
                    else
                    {
                        curHealth += Link.repairAmt;
                    }

                    //Link.currentHealth = curHealth; // I dont think I should have this here...
                }

                repairTimer.Interval = Link.repairSpeed;
            }
        }

        /// <summary>
        /// Rebuilds each building when they are destroyed.
        /// </summary>
        public virtual void rebuildTimer_Tick(object sender, EventArgs e)
        {
            if (Link.isPlaying)
            {
                isDestroyed = false;
                isCollidable = true;
                curHealth = 1;

                repairTimer.Enabled = true;
                rebuildTimer.Enabled = false;

                RebuiltHandler();
            }
        }

        /// <summary>
        /// when collided it checks if the health is greater than 0 if it is then it takes damage, else it gets destroyed and sets the building to 
        /// destroyed which will disable the upgrade windows. and stop the repair timer and enable the rebuild timer.
        /// </summary>
        public virtual void Collided()
        {
            if (curHealth > 0) //&& (curHealth - Link.explosionDamage) > 0)
            {
                isCollidable = true;

                if (collidedCount++ == 0)
                {
                    curHealth -= Link.explosionDamage; // Maybe just divide by 4 and let it hit 4 times, but could have bad results due to different values.
                }

                if (collidedCount == 4)
                {
                    collidedCount = 0;
                }
            }

            if (curHealth <= 0)
            {
                curHealth = 0;
                isCollidable = false;
                isDestroyed = true;
                mainImage.Image = buildingImages[destroyImageNum].Image;
                //mainImage.Top = originPoint.Y - (mainImage.Height); // Have to reposition because the height changes.

                rebuildTimer.Enabled = true;
                repairTimer.Enabled = false;
            }
        }

        #endregion
    }

    class DefenseTower : Building
    {
        #region Private Fields
            
        PlayerSettings PSettings;

        // New variables.
        private int cursorPX;
        private int cursorPY;

        private double turretAngle;
        private int turretRadius = 50;
        private int turretX;
        private int turretY;
        private Point gunEnd;
        private int playerNum;
        private Color playerColor;

        private int Ammo = 25;
        private int MaxAmmo = 25;

        Timer ReloadTimer = new Timer();

        enum Player
        {
            P1 = 0,
            P2,
            P3,
            P4
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Sets up the tower and their color.
        /// </summary>
        public DefenseTower(Point originPoint, int playerNum, LinkerClass link, int formWidth, int formHeight, PlayerSettings playerSettings)
            : base(originPoint, link, formWidth, formHeight)
        {
            PSettings = playerSettings;
            //this.originPoint = originPoint;
            gunEnd = new Point(this.OriginPoint.X, this.OriginPoint.Y - (turretRadius));
            this.playerNum = playerNum;
            

            // All player colors... Blue 30, 144, 255  Yellow 255, 255, 128  Red 246, 116, 116
            playerColor = Color.FromArgb(PSettings.P1Color);

            if (playerNum == (int)Player.P2)
                playerColor = Color.FromArgb(PSettings.P2Color);
            if (playerNum == (int)Player.P3)
                playerColor = Color.FromArgb(PSettings.P3Color);
            if (playerNum == (int)Player.P4)
                playerColor = Color.FromArgb(PSettings.P4Color);

            baseBar = new Rectangle(this.OriginPoint.X - turretRadius, this.OriginPoint.Y + 14, 102, 17);
            
            ReloadTimer.Interval = link.reloadAmmoInterval;
            ReloadTimer.Tick += ReloadTimer_Tick;
            ReloadTimer.Enabled = true;
        }

        #endregion

        #region Properties

        public Point GunPosition { get { return gunEnd; } }
        public int TurretRadius { get { return turretRadius; } }
        public Point Origin { get { return OriginPoint; } }
        public override int PosX { get { return OriginPoint.X; } }
        public override int PosY { get { return OriginPoint.Y; } }
        public override int Width { get { return turretRadius; } }
        public override int Height { get { return turretRadius; } }
        public int CurrentAmmo { get { return Ammo; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Calculates the turret's gun end based on where the cursor is pointed.
        /// </summary>
        public void TurretCalc(int CursorX, int CursorY)
        {
            // 5 offset from the origin not the top left point,
            cursorPX = CursorX - OriginPoint.X + 5;
            cursorPY = (int)OriginPoint.Y - CursorY + 5;

            turretAngle = (Math.Atan((double)cursorPY / (double)cursorPX));

            if (turretAngle > 0)
            {
                turretX = (int)(((Math.Cos(turretAngle) * turretRadius)) + OriginPoint.X);
                turretY = (int)(OriginPoint.Y - (Math.Sin(turretAngle) * turretRadius));
            }
            else
            {
                turretX = (int)(((Math.Cos(turretAngle) * turretRadius) * -1) + OriginPoint.X);
                turretY = (int)(OriginPoint.Y - (Math.Sin(turretAngle) * turretRadius) * -1);
            }

            gunEnd = new Point(turretX, turretY);
        }

        /// <summary>
        /// Draws the turret and gun when it is not destroyed, but when destroyed it doesnt draw the line.
        /// </summary>
        public override void Draw(Graphics g)
        {
            // Draws the angle numbers to the screen.
            //g.DrawString("Gun Angle: " + (turretAngle * (180 / Math.PI)).ToString(), new Font("Arial", 14), Brushes.Red, 10, 10);
            //g.DrawString("GunX: " + turretX.ToString(), new Font("Arial", 14), Brushes.Red, 10, 30);
            //g.DrawString("GunY: " + turretY.ToString(), new Font("Arial", 14), Brushes.Red, 10, 50);

            Pen myPen = new Pen(playerColor);
            SolidBrush myBrush = new SolidBrush(playerColor);
            g.DrawEllipse(myPen, new Rectangle(OriginPoint.X - turretRadius, OriginPoint.Y - (turretRadius), turretRadius * 2, turretRadius * 2));

            if (!isDestroyed)
            {
                g.DrawLine(myPen, new Point(OriginPoint.X, OriginPoint.Y), gunEnd);
            }
            
            g.DrawString("Ammo x" + Ammo.ToString(), new Font("Arial", 8f, FontStyle.Bold), myBrush, new Point(OriginPoint.X - (turretRadius /2), OriginPoint.Y- 12));
        }

        /// <summary>
        /// Draws the health which needs to be seperated because they need to be drawn at different times.
        /// </summary>
        public void DrawHealth(Graphics g)
        {

            g.FillRectangle(Brushes.Black, baseBar);

            int barSize = (int)(((double)curHealth / (double)Link.maxHealth) * 100);
            HealthBar = new Rectangle((OriginPoint.X - turretRadius) + 1, OriginPoint.Y + 15, barSize, 15);
            g.FillRectangle(Brushes.Red, HealthBar);
        }

        /// <summary>
        /// Rebuilds the building and disables the rebuildtimer and sets health to 1
        /// </summary>
        public override void rebuildTimer_Tick(object sender, EventArgs e)
        {
            isDestroyed = false;
            isCollidable = true;
            curHealth = 1;

            repairTimer.Enabled = true;
            rebuildTimer.Enabled = false;
        }

        /// <summary>
        /// Removes 1 ammo, if the reload timer is disabled then enable it.
        /// </summary>
        public void Shoot()
        {
            if (Ammo > 0)
            {
                Ammo--;
            }

            if (ReloadTimer.Enabled == false)
                ReloadTimer.Enabled = true;
        }

        /// <summary>
        /// Restores ammo to the turret
        /// </summary>
        private void ReloadTimer_Tick(object sender, EventArgs e)
        {
            if (Link.isPlaying)
            {
                if (Ammo < MaxAmmo)
                {
                    Ammo += 1;
                }
                else
                {
                    ReloadTimer.Enabled = false; // Do this to ensure that no ammo reload is missed.
                }

                ReloadTimer.Interval = Link.reloadAmmoInterval;
            }
        }

        #endregion
    }

    class GenericBuilding : Building
    {
        /// <summary>
        /// Sets the image of the building and its version of the destroyed one
        /// </summary>
        public GenericBuilding(Point originPoint, LinkerClass link, int formWidth, int formHeight)
            : base(originPoint, link, formWidth, formHeight)
        {
            destroyImageNum = 4 + (int)eBuilding.Generic;
            mainImage.Image = buildingImages[(int)eBuilding.Generic].Image;
            mainImage.Top = originPoint.Y - (mainImage.Height + SCREEN_OFFSET);
            mainImage.Left = originPoint.X;
        }

        /// <summary>
        /// Changes the current image to that of a rebuilt version of the building
        /// </summary>
        public override void rebuildTimer_Tick(object sender, EventArgs e)
        {
            base.rebuildTimer_Tick(sender, e);

            mainImage.Image = buildingImages[(int)eBuilding.Generic].Image;
        }
    }

    class IncomeBuilding : Building
    {
        /// <summary>
        /// Ditto.
        /// </summary>
        public IncomeBuilding(Point originPoint, LinkerClass link, int formWidth, int formHeight)
            : base(originPoint, link, formWidth, formHeight)
        {
            destroyImageNum = 4 + (int)eBuilding.Income;
            mainImage.Image = buildingImages[(int)eBuilding.Income].Image;
            mainImage.Top = originPoint.Y - (mainImage.Height + SCREEN_OFFSET);
            mainImage.Left = originPoint.X;
        }

        /// <summary>
        /// Ditto.
        /// </summary>
        public override void rebuildTimer_Tick(object sender, EventArgs e)
        {
            base.rebuildTimer_Tick(sender, e);

            Link.incomeDestroyed = false;
            mainImage.Image = buildingImages[(int)eBuilding.Income].Image;
        }

        /// <summary>
        /// If the income building gets destroyed, disable the continuous income, and disable the income menu upgrades.
        /// </summary>
        public override void Collided()
        {
            base.Collided();

            if (isDestroyed)
                Link.incomeDestroyed = isDestroyed;
        }
    }

    class ResearchBuilding : Building
    {
        /// <summary>
        /// Ditto.
        /// </summary>
        public ResearchBuilding(Point originPoint, LinkerClass link, int formWidth, int formHeight)
            : base(originPoint, link, formWidth, formHeight)
        {
            destroyImageNum = 4 + (int)eBuilding.Research;
            mainImage.Image = buildingImages[(int)eBuilding.Research].Image;
            mainImage.Top = originPoint.Y - (mainImage.Height + SCREEN_OFFSET);
            mainImage.Left = originPoint.X;
        }

        /// <summary>
        /// Ditto.
        /// </summary>
        public override void rebuildTimer_Tick(object sender, EventArgs e)
        {
            base.rebuildTimer_Tick(sender, e);

            Link.researchDestroyed = false;
            mainImage.Image = buildingImages[(int)eBuilding.Research].Image;
        }

        /// <summary>
        /// if research is destroyed disable the research the buildings upgrade menu
        /// </summary>
        public override void Collided()
        {
            base.Collided();

            if (isDestroyed)
                Link.researchDestroyed = isDestroyed;
        }
    }

    class EngineerBuilding : Building
    {
        /// <summary>
        /// Ditto.
        /// </summary>
        public EngineerBuilding(Point originPoint, LinkerClass link, int formWidth, int formHeight)
            : base(originPoint, link, formWidth, formHeight)
        {
            destroyImageNum = 4 + (int)eBuilding.Engineering;
            mainImage.Image = buildingImages[(int)eBuilding.Engineering].Image;
            mainImage.Top = originPoint.Y - (mainImage.Height + SCREEN_OFFSET);
            mainImage.Left = originPoint.X;
        }

        /// <summary>
        /// Ditto.
        /// </summary>
        public override void rebuildTimer_Tick(object sender, EventArgs e)
        {
            base.rebuildTimer_Tick(sender, e);

            Link.engineerDestroyed = false;
            mainImage.Image = buildingImages[(int)eBuilding.Engineering].Image;
        }

        /// <summary>
        /// If the Engineering building is destroyed it will disable to upgrades for it.
        /// </summary>
        public override void Collided()
        {
            base.Collided();

            if (isDestroyed)
                Link.engineerDestroyed = isDestroyed;
        }
    }
}
