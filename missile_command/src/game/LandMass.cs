using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class LandMass : Entity
	{
		private SolidBrush brush;

		public LandMass(int x, int y, int w, int h, ETag t = ETag.SYSTEM) : base(x, y, w, h, t)
		{
			// TODO maybe for hills chop it up so that it makes a hill ish? Perhaps make a new class for it.
			// adjust the origin for the height, good ole microsoft windows and stuff
			Body.MovePositionY(-(Body.Dimension.Height - 5));
			
			brush = new SolidBrush(Config.Instance().GetPlayerColor(ETag.SYSTEM));
		}
		public override void Draw(Graphics g)
		{
			g.FillRectangle(brush, Body.Left, Body.Top, Body.Width, Body.Height);
		}

		protected override void Collided(Body body)
		{

		}
	}
}
