using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	abstract class UserInterface
	{
		public Body Body { get; private set; }
		public UserInterface(int x, int y, int w, int h)
		{
			Body = new Body(x, y, w, h);
		}

		public abstract void Draw(System.Drawing.Graphics g);
	}
}
