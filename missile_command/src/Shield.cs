using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class Shield : GameObject
	{
		private const int OUTLINE_OFFSET = 4;
		private const int POSITION_Y_OFFSET = 21;

		private Rectangle shield;
		private ShieldBar hpBar;

		public Shield(Point cityBottom, Point o, Size d, ETag t, PType p = PType.PLAYER) : base(o, d, p, t)
		{
			// TODO add utility setting for the size of the bar
			hpBar = new ShieldBar(cityBottom, new Size(40, 10), ETag.SYSTEM);


			// Reposition the shield due to the fact that microsoft drawing has some weird dimension things going on.
			MovePositionX(-(Utils.CITY_TRUE_SIZE + 12));
			MovePositionY(-POSITION_Y_OFFSET);
			shield = new Rectangle(TopLeft, Dimension);
		}
		public override void Collided()
		{
			hpBar.Damage();
		}
		public override void Draw(Graphics g)
		{
			// TODO Animate the shield, by uh growing? or by flashing a lighter blue.
			hpBar.Draw(g);
			if (hpBar.IsAlive())
			{
				g.DrawArc(Pens.Blue, shield, 180, 180);
			}
		}
		public bool Active()
		{
			return hpBar.IsAlive();
		}
	}
}
