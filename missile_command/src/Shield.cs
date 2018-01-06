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
		public delegate void Full_Health();
		public event Full_Health Replished;

		public delegate void Destroyed();
		public event Destroyed Lowered;

		private const int OUTLINE_OFFSET = 4;
		private const int POSITION_Y_OFFSET = 21;

		private Rectangle shield;
		private ShieldBar hpBar;
		private Body collidedBody;

		// TODO rethink the logic for shield's positioning it might fix the problems I am having with all the magic numbers
		// Expects to get the BottomLeft for the hpbar, and the TopCenter for the shield
		public Shield(int bottomX, int bottomY, int center, int top, int w, int h, ETag t) : base(center, top, w, h, t)
		{
			// TODO add utility setting for the size of the bar
			// Set the hp bar to be below the city
			hpBar = new ShieldBar(bottomX, bottomY, 40, 10);
			hpBar.Healed += HpBar_Healed;

			// Reposition the shield due to the fact that microsoft drawing has some weird dimension things going on.
			Body.MovePositionX(-(Utils.CITY_TRUE_SIZE + 12));
			Body.MovePositionY(-POSITION_Y_OFFSET);
			shield = new Rectangle(Body.TopLeft, Body.Dimension);
		}
		private void HpBar_Healed()
		{
			Replished();
		}
		protected override void Collided(Body collider)
		{
			hpBar.Damage();
			if (!hpBar.IsAlive())
			{
				Lowered();
			}
			collidedBody = collider;
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
		public Body CollidedBody()
		{
			return collidedBody;
		}
	}
}
