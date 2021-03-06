﻿using System;
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

		public Reticle(int x, int y, PType p, ETag t, int w = CURSOR_DIMENSION, int h = CURSOR_DIMENSION) : base(x, y, w, h)
		{
			Body.UpdatePosition(Body.Left - CURSOR_OFFSET, Body.Top);
			tag = t;
		}
		public override void Draw(Graphics g)
		{
			g.DrawImage(Config.Instance.GetPlayerCursor(tag), Body.TopLeft);
		}
		public void Move(Direction dir)
		{
			switch (dir)
			{
				case Direction.UP:
					if (Body.Top - MOVE_VAL > 0)
						Body.AdjustY(-MOVE_VAL);
					else
						Body.UpdatePositionY(0);
					break;
				case Direction.RIGHT:
					if (Body.Right + MOVE_VAL < Consts.gameBounds.Width)
						Body.AdjustX(MOVE_VAL);
					else
						Body.UpdatePositionX(Consts.gameBounds.Width - Body.Width);
					break;
				case Direction.DOWN:
					if (Body.Bottom + MOVE_VAL < Consts.gameBounds.Height - Consts.STAGE_BOUND_HEIGHT)
						Body.AdjustY(MOVE_VAL);
					else
						Body.UpdatePositionY(Consts.gameBounds.Height - (CURSOR_DIMENSION + Consts.STAGE_BOUND_HEIGHT));
					break;
				case Direction.LEFT:
					if (Body.Left - MOVE_VAL > 0)
						Body.AdjustX(-MOVE_VAL);
					else
						Body.UpdatePositionX(0);
					break;
			}
		}
		public override void Update(long gameTime)
		{
			if (Config.Instance.GetMouseCheck(tag))
			{
				// Mouse tracking enabled.
				Body.UpdatePosition(Cursor.Position.X, Cursor.Position.Y);
			}
			else
			{
				// Determine the keys pressed.
				KPress keysPressed = KeypressHandler.Instance.PlayerKeyState(tag);
				if ((keysPressed & KPress.UP) == KPress.UP)
					Move(Direction.UP);
				if ((keysPressed & KPress.RIGHT) == KPress.RIGHT)
					Move(Direction.RIGHT);
				if ((keysPressed & KPress.DOWN) == KPress.DOWN)
					Move(Direction.DOWN);
				if ((keysPressed & KPress.LEFT) == KPress.LEFT)
					Move(Direction.LEFT);
			}
		}
		public override void PostUpdate(long gameTime) { }
	}
}
