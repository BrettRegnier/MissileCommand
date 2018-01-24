using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class HighscoreState : State
	{
		private State prevState;

		public HighscoreState(Window g, State s) : base(g)
		{
			prevState = s;
		}
		public override void Draw(Graphics g)
		{
			throw new NotImplementedException();
		}
		public override void PostUpdate(long gameTime)
		{
			throw new NotImplementedException();
		}
		public override void Update(long gameTime)
		{
			throw new NotImplementedException();
		}
	}
}
