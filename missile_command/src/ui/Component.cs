using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	abstract class Component
	{
		public Body Body { get; private set; }
		public Component(int x, int y, int w, int h)
		{
			Body = new Body(x, y, w, h);
		}

		public abstract void Update(long gameTime);
		public abstract void PostUpdate(long gameTime);
		public abstract void Draw(System.Drawing.Graphics g);
	}
}
