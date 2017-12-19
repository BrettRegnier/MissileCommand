using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Missile_Command___Final
{
    class Cursor
    {
        #region Constants

        private const int CURSOR_DIMENSION = 9;
        private const int CURSOR_OFFSET = 5;
        private const int MOVE_VAL = 25;

        #endregion

        #region Fields

        private static List<PictureBox> cursorList = new List<PictureBox>();
        private PictureBox mainImage = new PictureBox();
        private int playerNum = 0;
        private PlayerSettings pSettings;

        private enum KPress
        {
            NONE = 0,
            up = 1,
            right = 2,
            down = 4,
            left = 8
        }

        #endregion

        #region Constructors

        static Cursor()
        {
            try
            {
                for (int i = 0; i < 12; i++)
                {
                    cursorList.Add(new PictureBox());
                    cursorList[cursorList.Count - 1].Image = new Bitmap("cursor_" + i.ToString() + ".png");
                }
            }
            catch
            {
                MessageBox.Show("The pictures were not found in the same directory as the .exe.");
            }
        }

        /// <summary>
        /// Set the cursor to above the turret its associated with, and their color.
        /// </summary>
        public Cursor(int originX, int originY, int PlayerNum, PlayerSettings playerSettings)
        {
            pSettings = playerSettings;

            this.playerNum = PlayerNum;

            if (PlayerNum == 0)
                mainImage.Image = cursorList[pSettings.P1Index].Image;
            if (PlayerNum == 1)
                mainImage.Image = cursorList[pSettings.P2Index].Image;
            if (PlayerNum == 2)
                mainImage.Image = cursorList[pSettings.P3Index].Image;
            if (PlayerNum == 3)
                mainImage.Image = cursorList[pSettings.P4Index].Image;

            mainImage.SizeMode = PictureBoxSizeMode.AutoSize;

            // Center offset of 5
            mainImage.Left = originX - CURSOR_OFFSET;
            mainImage.Top = originY / 2 + CURSOR_OFFSET;
        }

        #endregion

        #region Properties

        public int FormWidth { get; set; }
        public int FormHeight { get; set; }
        public int PosX { get { return mainImage.Left; } }
        public int PosY { get { return mainImage.Top; } }

        #endregion

        #region Methods

        /// <summary>
        /// Simply draws the cursor.
        /// </summary>
        public void Draw(Graphics g)
        {
            g.DrawImage(mainImage.Image, new Point(mainImage.Left, mainImage.Top));
        }

        /// <summary>
        /// Moves it depended on the key input int, which is passed in from the keypress code.
        /// if the movement val plus the left image is too far for the restraints it doesnt move.
        /// </summary>
        public void move(int KeyData)
        {
            // Left
            if (mainImage.Left - MOVE_VAL > 0)
            {
                if (KeyData == (int)KPress.left)
                {
                    mainImage.Left -= MOVE_VAL;
                }
            }

            //Right
            if (mainImage.Left + mainImage.Width + CURSOR_DIMENSION < FormWidth)
            {
                if (KeyData == (int)KPress.right)
                {
                    mainImage.Left += MOVE_VAL;
                }
            }

            // Up
            if (mainImage.Top - MOVE_VAL > 0)
            {
                if (KeyData == (int)KPress.up)
                {
                    mainImage.Top -= MOVE_VAL;
                }
            }

            // Down
            if (mainImage.Top + mainImage.Height + CURSOR_DIMENSION < FormHeight - 160)
            {
                if (KeyData == (int)KPress.down)
                {
                    mainImage.Top += MOVE_VAL;
                }
            }
        }

        #endregion
    }
}
