using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace missile_command
{
	class Reticle
	{
		#region Constants
		private const int CURSOR_DIMENSION = 9;
		private const int CURSOR_OFFSET = 5;
		private const int MOVE_VAL = 25;
		#endregion

		#region Fields
		private PType player;
		private PictureBox sprite = new PictureBox();
		#endregion

		public Reticle(Point ori, PType p)
		{
			sprite.Left = ori.X;
			sprite.Top = ori.Y;
			player = p;
			sprite.Image = Properties.Resources.cursor_09;
		}

		public void Draw(Graphics g)
		{
			g.DrawImage(sprite.Image, new Point(sprite.Left, sprite.Top));
		}
	}
}
