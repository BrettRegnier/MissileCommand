using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class Shield : Entity
	{
		private const int OUTLINE_OFFSET = 4;		
		
		private Rectangle shield;
		private ShieldBar hpBar;

		public Shield(Point cityBottom, Point o, Size d, ETag t) : base(o, d, t)
		{
			// TODO add utility setting for the size of the bar
			hpBar = new ShieldBar(cityBottom, new Size(200, 200), ETag.SYSTEM);

			MovePositionX(-(Utils.CITY_TRUE_SIZE + 12));
			MovePositionY(-dimension.Height/3);
			shield = new Rectangle(position, dimension);
		}
		public override void Draw(Graphics g)
		{
			hpBar.Draw(g);
			// Use draw arc instead
			g.DrawEllipse(Pens.Blue, shield);
		}
	}
}
