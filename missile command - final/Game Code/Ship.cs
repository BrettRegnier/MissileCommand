using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Missile_Command___Final
{
    class Ship
    {
        #region Fields

        public delegate void Deconstuct(Ship shp);
        public event Deconstuct DestroyShip;

        private static List<PictureBox> shipImages = new List<PictureBox>();
        private PictureBox mainImage = new PictureBox();
        private int imgPrnt = 0;

        private Random rand = new Random();
        private bool movingRight = true;
        private int StartingX = 0;

        // Form Size
        private int formWidth;
        private int formHeight;

        // Bomb dropping variables
        private const int SEND_INTERVAL = 10;

        LinkerClass Link;

        private enum Direction
        {
            right = 0,
            left = 4
        };

        #endregion

        #region Constructors

        static Ship()
        {
            try
            {
                // Add Facing Right
                for (int i = 1; i < 5; i++)
                {
                    shipImages.Add(new PictureBox());
                    shipImages[shipImages.Count - 1].Image = new Bitmap("SpaceShip_R_" + i.ToString() + ".png");
                }

                // Add Facing left
                for (int i = 1; i < 5; i++)
                {
                    shipImages.Add(new PictureBox());
                    shipImages[shipImages.Count - 1].Image = new Bitmap("SpaceShip_L_" + i.ToString() + ".png");
                }
            }
            catch
            {
                MessageBox.Show("Missing spaceship images in the directory");
            }
        }

        /// <summary>
        /// Sets up the ship
        /// </summary>
        public Ship(int formWidth, int formHeight, LinkerClass link)
        {
            Link = link;
            this.formWidth = formWidth;
            this.formHeight = formHeight;
            if (rand.Next(0, 2) == 0)
            {
                // Facing right
                imgPrnt = (int)Direction.right;
                movingRight = true;
                StartingX = 0;
            }
            else
            {
                // Facing left
                imgPrnt = (int)Direction.left;
                movingRight = false;
                StartingX = formWidth;
            }

            mainImage.Image = shipImages[imgPrnt].Image;
            mainImage.SizeMode = PictureBoxSizeMode.AutoSize;

            if (movingRight)
            {
                mainImage.Left = 0;
            }
            else
            {
                mainImage.Left = StartingX - mainImage.Width;
            }

            mainImage.Top = formHeight / 4;
        }

        #endregion

        #region Properties

        public int PosX { get { return mainImage.Left; } }
        public int PosY { get { return mainImage.Top; } }
        public int Width { get { return mainImage.Width; } }
        public int Height { get { return mainImage.Height; } }

        #endregion

        #region Methods

        /// <summary>
        /// Draws the ship onto the form, and cycles the animations based on which direction it is facing
        /// </summary>
        public void Draw(Graphics g)
        {
            if (Link.isPlaying)
            {
                if (movingRight)
                {
                    if (++imgPrnt > shipImages.Count - 5)
                        imgPrnt = 0;
                }
                else
                {
                    if (++imgPrnt > shipImages.Count - 1)
                        imgPrnt = 4;
                }
            }

            mainImage.Image = shipImages[imgPrnt].Image;
            g.DrawImage(mainImage.Image, new Point(mainImage.Left, mainImage.Top));
        }

        /// <summary>
        /// Moves the ships based on which direction the ship is facing.
        /// </summary>
        public void Move()
        {
            int Distance = rand.Next(1, 11);

            if (movingRight)
            {
                if (mainImage.Left >= formWidth)
                {
                    DestroyShip(this);
                }
                else
                {
                    mainImage.Left += Distance;
                }
            }
            else
            {
                if (mainImage.Left <= 0 + mainImage.Width)
                {
                    DestroyShip(this);
                }
                else
                {
                    mainImage.Left -= Distance;
                }
            }
        }

        /// <summary>
        /// Destroys the object when collided with a bomb.
        /// </summary>
        public void Collided()
        {
            DestroyShip(this);
        }

        #endregion
    }
}
