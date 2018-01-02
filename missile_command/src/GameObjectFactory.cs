using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace missile_command
{
	class GameObjectFactory
	{
		public static Bomb MakeBomb(Point o, Size d, Point des, PType p, Account a)
		{
			return new Bomb(o, d, des, p, a);
		}
		public static Turret MakeTurret(Point o, Size d, PType p, Account a)
		{
			return new Turret(o, d, p, a);
		}
	}
}
