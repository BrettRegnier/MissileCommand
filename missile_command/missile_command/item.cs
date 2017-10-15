using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	public abstract class item
	{
		public abstract void draw(Graphics g);
	}

	public class bomb : item
	{
		public bomb(Team t, Point origin, Point destination, )

		public override void draw(Graphics g)
		{

		}
		public void move()
		{

		}
		public void collide()
		{

		}
		public void calcVelocity()
		{

		}
		public void explosionCalc()
		{

		}
	}
}
