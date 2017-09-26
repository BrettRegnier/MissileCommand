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

	public abstract class bomb
	{
		public abstract void move()
	}
}
