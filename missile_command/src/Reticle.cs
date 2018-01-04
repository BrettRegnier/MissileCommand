using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace missile_command
{
	class Reticle : Entity
	{
		private const int CURSOR_OFFSET = 5;
		private const int MOVE_VAL = 10;
		private const int CURSOR_DIMENSION = 9;

		private Image sprite;

		public Reticle(Point o, PType p, ETag t, int w = CURSOR_DIMENSION, int h = CURSOR_DIMENSION) : base(o, w, h, t)
		{
			UpdatePosition(position.X - CURSOR_OFFSET, position.Y);
			// TODO load cursor by config
			sprite = Properties.Resources.cursor_09;
		}
		public override void Draw(Graphics g)
		{
			g.DrawImage(sprite, position);
		}
		public Point Move(Direction dir)
		{
			switch (dir)
			{
				//TODO maybe move into its own collision detection?
				case Direction.UP:
					if (Top() - MOVE_VAL > 0)
						MovePositionY(-MOVE_VAL);
					else
						UpdatePositionY(0);
					break;
				case Direction.RIGHT:
					if (Right() + MOVE_VAL < Utils.gameBounds.Width)
						MovePositionX(MOVE_VAL);
					else
						UpdatePositionX(Utils.gameBounds.Width - dimension.Width);
					break;
				case Direction.DOWN:
					if (Bottom() + MOVE_VAL < Utils.gameBounds.Height)
						MovePositionY(MOVE_VAL);
					else
						UpdatePositionY(Utils.gameBounds.Height - (CURSOR_DIMENSION + Utils.STAGE_BOUNDS));
					break;
				case Direction.LEFT:
					if (Left() - MOVE_VAL > 0)
						MovePositionX(-MOVE_VAL);
					else
						UpdatePositionX(0);
					break;
			}
			return Center();
		}
		public override Point Center()
		{
			// Honestly not sure why this works the way it does, but for some reason this centers the bombs.
			return new Point(position.X + 3, position.Y + 3);
		}
	}
}
