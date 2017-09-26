using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Missile_Command___Final
{
    abstract class Bomb
    {
        #region Delegates

        public delegate void Deconstuct(Bomb bmb);
        public event Deconstuct DestroyBomb;

        public delegate void Explode(Bomb bmb);
        public event Explode Explosion;

        #endregion

        #region Constants

        protected const int CURSOR_OFFSET = 5;

        #endregion

        #region Private Fields

        PlayerSettings PSettings;

        // Graphics Fields
        protected SolidBrush bombBrush;
        protected Pen ExplodePen;
        protected bool explodeFlash = false;
        protected Form fp;
        protected Rectangle circle;
        protected int bombSize;
        protected int explosionSize;
        private Color playerColor;

        // Functionality Fields
        protected int originX = 0;
        protected int originY = 0;
        protected int destinationX = 0;
        protected int destinationY = 0;
        private int currentRadius = 0;

        // Movement fields
        private double vX = 0;
        private double vY = 0;
        protected static double DiagonalVelocity = 0;

        protected double newX = 0;
        protected double newY = 0;

        // Reached destination
        protected bool atDestination = false;
        protected int flashCount = 0;
        protected LinkerClass Link;


        #endregion

        #region Constructor

        public Bomb(Point originPoint, Point destinationPoint, Form fp, LinkerClass link, PlayerSettings playerSettings)
        {
            PSettings = playerSettings;
            // used for collision detection
            currentRadius = bombSize;

            // Linking class.
            Link = link;
            // Set bomb size to when the player upgrades it
            this.fp = fp;
            destinationX = destinationPoint.X;
            destinationY = destinationPoint.Y;
        }

        #endregion

        #region Properties

        public int FormWidth { get; set; }
        public int FormHeight { get; set; }
        public int bombWidth { get { return circle.Width; } }
        public int bombHeight { get { return circle.Height; } }
        public int bombPositionX { get { return circle.X; } }
        public int bombPositionY { get { return circle.Y; } }
        public Point bombCenterPoint {  get { return new Point(circle.X + (circle.Width / 2), circle.Y + (circle.Height / 2)); } }
        public int BombRadius { get { return currentRadius; } }


        #endregion

        #region Public/Protected Methods

        /// <summary>
        /// Method for drawing all of the bombs onto the screen. 
        /// when the bombs are exploding it will draw it as a bigger cirlce as positioned in the exact same spot, but bigger.
        /// when it hits the fourth flash it sends a delegate to destroy.
        /// </summary>
        public virtual void Draw(Graphics g)
        {
            if (atDestination)
            {
                circle.Width = explosionSize;
                circle.Height = explosionSize;

                if (!explodeFlash)
                {
                    g.FillEllipse(bombBrush, circle);
                }
                else if (explodeFlash)
                {
                    g.DrawEllipse(ExplodePen, circle);
                }

                if (Link.isPlaying)
                {
                    explodeFlash = !explodeFlash;
                    flashCount += 1;
                    if (flashCount == 4)
                    {
                        DestroyBomb(this);
                    }
                }
            }
            else
            {
                g.FillEllipse(bombBrush, circle);
            }
        }

        /// <summary>
        /// Method to move, uses some good ole physics to move the Vx and Vy if the bomb is within the range of its speeds distance
        ///  and 0 it will automatically jump to the destination ensuring it doesnt go flying past.
        /// </summary>
        public void Move()
        {
            double remainingDiffX = Math.Abs(destinationX - newX);
            double remainingDiffY = Math.Abs(destinationY - newY);

            if (((newY != destinationY) || (newX != destinationX)) && !atDestination)
            {
                newX += vX;
                newY += vY;

                if (remainingDiffX >= 0 && remainingDiffX < Math.Abs(vX) && remainingDiffY >= 0 && remainingDiffY < Math.Abs(vY))
                {
                    newX = destinationX;
                    newY = destinationY;
                }

                circle.X = Convert.ToInt32(newX);
                circle.Y = Convert.ToInt32(newY);
            }
            else if (!atDestination)
            {
                PositionNewCoordinates();
            }
        }

        /// <summary>
        /// Calculates the velocity of X and Y by taking the diagonal velocity and the bomb by using some triginomitry to determine
        /// the velocity of the X and Y. It checks the difference of of the origin and destination if the x is negative it will
        /// send it to the oppsoite direction.
        /// </summary>
        protected void CalcVelocity()
        {
            double diffX = originX - destinationX;
            double diffY = originY - destinationY;
            double trajAngle = 0;

            trajAngle = Math.Atan(diffY / diffX); // *(180 / Math.PI);

            if (destinationX > originX)
            {
                vX = DiagonalVelocity * Math.Cos(trajAngle);
                vY = DiagonalVelocity * Math.Sin(trajAngle);
            }
            else
            {
                vX = -DiagonalVelocity * Math.Cos(trajAngle);
                vY = -DiagonalVelocity * Math.Sin(trajAngle);
            }
        }

        /// <summary>
        /// If the bomb is collided it will check the position and determine it is at its destination.
        /// which puts its position to the exact center of the cursor now that it is much bigger.
        /// </summary>
        public virtual void collided()
        {
            if (!atDestination)
            {
                PositionNewCoordinates();
            }
        }

        /// <summary>
        /// Sets the position of the bomb to the same place when it gets recalculated since it is much bigger now
        /// </summary>
        public void PositionNewCoordinates()
        {
            circle.X = circle.X - ((explosionSize / 2) - Convert.ToInt32(CURSOR_OFFSET));
            circle.Y = circle.Y - ((explosionSize / 2) - Convert.ToInt32(CURSOR_OFFSET));
            currentRadius = explosionSize / 2;
            atDestination = true;
        }

        #endregion
    }

    class PlayerBomb : Bomb
    {
        PlayerSettings PSettings;

        /// <summary>
        /// Gives the Settings to this class for its available. Sets the bomb default size, and sets its explosion size to that of the link explosion
        /// which gets bigger when it gets upgraded. Sets Velocity which will be used to calculate the Vx and Vy.
        /// sets origin points and moves it to its correct position left and up a bit.
        /// sets the bomb color, circle and then the velocity.
        /// </summary>
        public PlayerBomb(Point originPoint, Point destinationPoint, Form fp, int playerNum, LinkerClass link, PlayerSettings playerSettings)
            : base(originPoint, destinationPoint, fp, link, playerSettings)
        {
            PSettings = playerSettings;

            // 200 is base Explosion. Need to make these all configurable speeds.
            bombSize = 10; // I didnt want these to be upgraded. (Could get really overpowered)
            explosionSize = link.explosionVar;
            // Changes overall speed
            DiagonalVelocity = link.bombSpeedVar;

            // Had to be moved into their own constructors because I need to get the bombsize first and if I left it
            // in the base constrcutor it would never get to it.
            originX = originPoint.X - bombSize / 2;
            originY = originPoint.Y - bombSize / 2;
            newX = originX;
            newY = originY;
            bombBrush = new SolidBrush(SetColor(playerNum));
            ExplodePen = new Pen(SetColor(playerNum));
            circle = new Rectangle(originX, originY, bombSize, bombSize);

            CalcVelocity();
        }

        /// <summary>
        /// Sets the player color to their setting color
        /// </summary> 
        public Color SetColor(int playerNum)
        {

            // All player colors.. P1 Green(128, 255, 128) P2 Blue(30, 144, 255) P3 Yellow(255, 255, 128) P4 Red(246, 116, 116)

            // Player 1
            Color playerColor = Color.FromArgb(PSettings.P1Color);

            // Player 2
            if (playerNum == 1)
            {
                playerColor = Color.FromArgb(PSettings.P2Color);
            }
            
            // Player 3
            if(playerNum == 2)
            {
                playerColor = Color.FromArgb(PSettings.P3Color);
            }

            // Player 4
            if (playerNum == 3)
            {
                playerColor = Color.FromArgb(PSettings.P4Color);
            }

            return playerColor;
        }
    }

    class EnemyBomb : Bomb
    {
        private bool moneyRecieved = false;

        public bool MoneyRecieved { get { return moneyRecieved; } }

        /// <summary>
        /// Ditto as above but for enemy bombs
        /// </summary>
        public EnemyBomb(Point originPoint, Point destinationPoint, Form fp, int speed, LinkerClass Link, PlayerSettings playerSettings)
            : base(originPoint, destinationPoint, fp, Link, playerSettings)
        {
            //Ditto
            bombSize = 10;
            explosionSize = 200;

            // Changes overall speed.
            DiagonalVelocity = speed; // original 3

            // Had to be moved into their own constructors because I need to get the bombsize first and if I left it
            // in the base constrcutor it would never get to it.
            originX = originPoint.X - bombSize / 2;
            originY = originPoint.Y - bombSize / 2;
            newX = originX;
            newY = originY;
            bombBrush = new SolidBrush(Color.White);
            ExplodePen = new Pen(Color.White);
            circle = new Rectangle(originX, originY, bombSize, bombSize);

            CalcVelocity();
        }

        /// <summary>
        /// Weird code, because it was done while I had a headache. but sets the moneyrecieved to true, if it has been collided once when shot down.
        /// </summary>
        public override void collided()
        {
            if (flashCount == 0)
            {
                moneyRecieved = true;
            }

            base.collided();
        }
    }
}
