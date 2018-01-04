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
		private Rectangle land;
		private SolidBrush brush;
		private ETag account;

		public LandMass(Point o, Size d, ETag a = ETag.SYSTEM) : base(o, d, a)
		{
			// TODO maybe for hills chop it up so that it makes a hill ish? Perhaps make a new class for it.
			// adjust the origin for the height, good ole microsoft windows and stuff
			UpdatePositionY(position.Y - (dimension.Height - 5));

			land = new Rectangle(position, dimension);
			brush = new SolidBrush(Config.Instance().GetPlayerColor(ETag.SYSTEM));
			account = a;
		}
		public override void Draw(Graphics g)
		{
			g.FillRectangle(brush, land);
		}
	}
}
