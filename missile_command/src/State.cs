using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	abstract class State
	{
		protected Window game;
		public State(Window g)
		{
			game = g;
		}
		public abstract void Update();
		public abstract void PostUpdate();
		public abstract void Draw(Graphics g);
	}
}
