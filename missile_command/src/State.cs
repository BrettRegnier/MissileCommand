using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	public abstract class State
	{
		protected Window game;
		public State(Window g)
		{
			game = g;
		}
		public abstract void Draw(Graphics g);
		public abstract void Update(long gameTime);
		public abstract void PostUpdate(long gameTime);
	}
}
