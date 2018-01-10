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
		//public delegate void Full_Health();
		//public event Full_Health Replished;

		//public delegate void Destroyed();
		//public event Destroyed Lowered;

		private const int OUTLINE_OFFSET = 4;
		private const int POSITION_Y_OFFSET = 21;

		private ShieldBar spBar;
		private Collider prevCollider;
		private bool isAlive;

		public bool Active { get { return spBar.IsAlive; } }
		public Collider PreviousCollider { get { return prevCollider; } }

		// TODO rethink the logic for shield's positioning it might fix the problems I am having with all the magic numbers
		// Expects to get the BottomLeft for the hpbar, and the TopCenter for the shield
		public Shield(int CenterX, int bottomY, int center, int top, int w, int h, ETag t) : base(center, top, w, h, t)
		{
			isAlive = true;

			// TODO I think the size of the bar should match the width of the city
			// Set the hp bar to be below the city
			spBar = new ShieldBar(CenterX, bottomY, 40, 10);

			// Reposition the shield due to the fact that microsoft drawing has some weird dimension things going on.
			Body.AdjustX(-((Body.Width / 2) - 4) );
			Body.AdjustY(-POSITION_Y_OFFSET);
		}
		protected override void Collided(Collider collider)
		{
			spBar.Damage();
			prevCollider = collider;
		}
		public override void Draw(Graphics g)
		{
			spBar.Draw(g);
			// TODO Animate the shield, by uh growing? or by flashing a lighter blue.
			if (isAlive)
				g.DrawArc(Pens.Blue, Body.Left, Body.Top, Body.Width, Body.Height, 180, 180);
		}
		public override void Update(long gameTime)
		{
			spBar.Update(gameTime);
			isAlive = spBar.IsAlive;
		}
		public override void PostUpdate(long gameTime)
		{
			spBar.PostUpdate(gameTime);
		}
	}
}
