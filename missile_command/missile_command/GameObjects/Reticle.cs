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
		private const int MOVE_VAL = 10;
		#endregion

		#region Fields
		private PType player;
		private PictureBox sprite = new PictureBox();
		private Point bounds;
		#endregion

		public Reticle(Point ori, PType p, Point bound)
		{
			sprite.Left = ori.X;
			sprite.Top = ori.Y;
			player = p;
			sprite.Image = Properties.Resources.cursor_09;
			bounds = bound;
		}
		public void Draw(Graphics g)
		{
			g.DrawImage(sprite.Image, new Point(sprite.Left, sprite.Top));
		}
		public Point Move(Direction dir)
		{
			switch (dir)
			{
				case Direction.UP:
					if (sprite.Top - MOVE_VAL > 0)
						sprite.Top -= MOVE_VAL;
					else
						sprite.Top = 0;
					break;
				case Direction.RIGHT:
					if (sprite.Left + MOVE_VAL + CURSOR_DIMENSION < bounds.X)
						sprite.Left += MOVE_VAL;
					else
						sprite.Left = bounds.X - CURSOR_DIMENSION;
					break;
				case Direction.DOWN:
					if (sprite.Top + MOVE_VAL + CURSOR_DIMENSION < bounds.Y - Utils.GAME_BOUND_OFFSET)
						sprite.Top += MOVE_VAL;
					else
						sprite.Top = bounds.Y - (CURSOR_DIMENSION + Utils.GAME_BOUND_OFFSET);
					break;
				case Direction.LEFT:
					if (sprite.Left - MOVE_VAL > 0)
						sprite.Left -= MOVE_VAL;
					else
						sprite.Left = 0;
					break;
			}
			return CenterPosition();
		}
		public Point CenterPosition()
		{
			// Returns the center of the reticle
			int Left = sprite.Left + CURSOR_OFFSET;
			int Top = sprite.Top + CURSOR_OFFSET;
			Point center = new Point(Left, Top);
			return center;
		}
	}
}
