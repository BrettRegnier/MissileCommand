using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace missile_command
{
	class Player : GameObject
	{
		private Reticle cursor;

		public Player(Point pos, PType p) : base(pos, p)
		{
			Point reticleOrigin = new Point(200, 200);
			cursor = new Reticle(reticleOrigin, p);
		}
		public override void Draw(Graphics g)
		{
			// Need to calculate the circle size, and the line, with the angle based on the aming reticle
			cursor.Draw(g);
		}
		public override void Collided()
		{
			throw new NotImplementedException();
		}
	}
}
