using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace missile_command
{
	class Player : GameObject
	{
		private Reticle cursor;

		public Player(Point pos, Dimensions dim, Graphics g, PType p) : base(pos, dim, p)
		{
			cursor = new Reticle(g, p);
		}

		public override void Draw(Graphics g)
		{
			// Need to calculate the circle size, and the line, with the angle based on the aming reticle
			throw new NotImplementedException();
		}

		public override void Collided()
		{
			throw new NotImplementedException();
		}
	}
}
