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

		public LandMass(Point o, Size d) : base(o, d)
		{
			// adjust the origin for the height, good ole microsoft windows and stuff
			UpdatePosition(position.X, position.Y - (dimension.Height - 5));

			land = new Rectangle(position, dimension);
			brush = new SolidBrush(Config.Instance().GetPlayerColor(Account.SYSTEM));
		}
		public void Draw(Graphics g)
		{
			g.FillRectangle(brush, land);
		}
	}
}
