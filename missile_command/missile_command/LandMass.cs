using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class LandMass
	{
		private Rectangle land;
		private SolidBrush brush;

		public LandMass()
		{
			int x = 0;
			int y = Utils.gameBounds.Height - Utils.LAND_MASS_SIZE;
			Point position = new Point(x, y);
			int width = Utils.gameBounds.Width;
			int height = Utils.LAND_MASS_SIZE;
			Size size = new Size(width, height);

			land = new Rectangle(position, size);

			brush = new SolidBrush(Config.Instance().GetPlayerColor(Account.SYSTEM));
		}
		public void Draw(Graphics g)
		{
			g.FillRectangle(brush, land);
		}
	}
}
