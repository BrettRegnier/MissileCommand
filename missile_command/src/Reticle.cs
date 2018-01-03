using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace missile_command
{
	class Reticle : Entity
	{
		#region Constants
		private const int CURSOR_OFFSET = 5;
		private const int MOVE_VAL = 10;
		public const int CURSOR_DIMENSION = 9;
		#endregion

		#region Fields
		private PType player;
		private PictureBox sprite = new PictureBox();
		#endregion

		public Reticle(Point o, PType p, ETag t, int w = CURSOR_DIMENSION, int h = CURSOR_DIMENSION) : base(o, w, h,t)
		{
			UpdatePosition(position.X - CURSOR_OFFSET, position.Y);
			sprite.Left = o.X - CURSOR_OFFSET;
			sprite.Top = o.Y;
			sprite.Image = Properties.Resources.cursor_09;
		}
		public override void Draw(Graphics g)
		{
			g.DrawImage(sprite.Image, new Point(sprite.Left, sprite.Top));
		}
		public Point Move(Direction dir)
		{
			switch (dir)
			{
				//TODO maybe move into its own collision detection?
				case Direction.UP:
					if (sprite.Top - MOVE_VAL > 0)
						sprite.Top -= MOVE_VAL;
					else
						sprite.Top = 0;
					break;
				case Direction.RIGHT:
					if (sprite.Left + MOVE_VAL + CURSOR_DIMENSION < Utils.gameBounds.Width)
						sprite.Left += MOVE_VAL;
					else
						sprite.Left = Utils.gameBounds.Width - CURSOR_DIMENSION;
					break;
				case Direction.DOWN:
					if (sprite.Top + MOVE_VAL + CURSOR_DIMENSION < Utils.gameBounds.Height - Utils.RETICLE_BOUNDS_OFFSET)
						sprite.Top += MOVE_VAL;
					else
						sprite.Top = Utils.gameBounds.Height - (CURSOR_DIMENSION + Utils.RETICLE_BOUNDS_OFFSET);
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
		protected override void UpdatePosition(int x, int y)
		{
			sprite.Left = x;
			sprite.Top = y;
			base.UpdatePosition(x, y);
		}
	}
}
