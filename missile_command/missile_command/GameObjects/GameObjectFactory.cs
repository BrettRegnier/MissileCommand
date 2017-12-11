using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace missile_command
{
	class GameObjectFactory
	{
		public static Bomb MakeBomb(Point ori, Point des, Dimension dim, PType p)
		{
			return new Bomb(ori, des, dim, p);
		}
		public static Player MakePlayer(Point ori, PType p)
		{
			return new Player(ori, p);
		}
	}
}
