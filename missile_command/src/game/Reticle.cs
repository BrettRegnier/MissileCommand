using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace missile_command
{
	class Reticle : Component
	{
		private const int CURSOR_OFFSET = 6;
		private const int MOVE_VAL = 10;
		private const int CURSOR_DIMENSION = 9;

		private ETag tag;
		private Image sprite;

		public Reticle(int x, int y, PType p, ETag t, int w = CURSOR_DIMENSION, int h = CURSOR_DIMENSION) : base(x, y, w, h)
		{
			Body.UpdatePosition(Body.Left - CURSOR_OFFSET, Body.Top);
			// TODO load cursor by config
			sprite = Properties.Resources.cursor_09;
			tag = t;
		}
		public override void Draw(Graphics g)
		{
			g.DrawImage(sprite, Body.TopLeft);
		}
		public Point Move(Direction dir)
		{
			// TODO could just pass in x and y values of 1, -1
			switch (dir)
			{
				case Direction.UP:
					if (Body.Top - MOVE_VAL > 0)
						Body.MovePositionY(-MOVE_VAL);
					else
						Body.UpdatePositionY(0);
					break;
				case Direction.RIGHT:
					if (Body.Right + MOVE_VAL < Utils.gameBounds.Width)
						Body.MovePositionX(MOVE_VAL);
					else
						Body.UpdatePositionX(Utils.gameBounds.Width - Body.Width);
					break;
				case Direction.DOWN:
					if (Body.Bottom + MOVE_VAL < Utils.gameBounds.Height - Utils.STAGE_BOUND_HEIGHT)
						Body.MovePositionY(MOVE_VAL);
					else
						Body.UpdatePositionY(Utils.gameBounds.Height - (CURSOR_DIMENSION + Utils.STAGE_BOUND_HEIGHT));
					break;
				case Direction.LEFT:
					if (Body.Left - MOVE_VAL > 0)
						Body.MovePositionX(-MOVE_VAL);
					else
						Body.UpdatePositionX(0);
					break;
			}
			return Body.Center;
		}
		public override void Update(long gameTIme)
		{

		}
		public override void PostUpdate(long gameTime)
		{

		}
	}
}
