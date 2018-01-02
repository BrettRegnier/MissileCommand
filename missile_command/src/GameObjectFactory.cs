using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace missile_command
{
	class GameObjectFactory
	{
		public static Bomb MakeBomb(Point ori, Point des, PType p, Account a)
		{
			return new Bomb(ori, des, p, a);
		}
		public static Turret MakeTurret(Point ori, PType p, Account a)
		{
			return new Turret(ori, p, a);
		}
	}
}
