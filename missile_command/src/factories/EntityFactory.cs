using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace missile_command
{
	class EntityFactory
	{
		public static Bomb MakeBomb(int x, int y, Point des, PType p, ETag a)
		{
			int r;
			if (p == PType.ENEMY)
				r = Config.Instance.EnemyBombDiameter;
			else
				r = Config.Instance.PlayerBombDiameter;

			return new Bomb(x, y, r, r, des, p, a);
		}
		public static Turret MakeTurret(Point o, Size d, PType p, ETag a)
		{
			return new Turret(o.X, o.Y, d.Width, d.Height, p, a);
		}
	}
}
