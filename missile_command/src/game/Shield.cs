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

		private const int STATUS_BAR_X_OFFSET = 5;
		private const int POSITION_Y_OFFSET = 21;

		private Status spBar;
		private Collider prevCollider;
		private bool isAlive;
		private bool animate;

		private int minHeight;
		private int maxHeight;

		public bool Alive { get { return spBar.Alive; } }
		public Collider PreviousCollider { get { return prevCollider; } }

		// Expects to get the BottomLeft for the hpbar, and the TopCenter for the shield
		public Shield(int statusBarX, int statusBarY, int x, int y, int w, int h, ETag t) : base(x, y, w, h, t)
		{
			// Set the hp bar to be below the city
			spBar = new Status(100, Color.Blue, (statusBarX - 20) + STATUS_BAR_X_OFFSET, statusBarY + 7, 40, 10);

			// Reposition the shield due to the fact that microsoft drawing has some weird dimension things going on.
			Body.AdjustX(-(Body.Width / 2 - 4));
			Body.AdjustY(-(POSITION_Y_OFFSET));

			isAlive = true;
			animate = false;

			minHeight = 10;
			maxHeight = Body.Height;
		}
		private void SpBar_Healed(object sender, EventArgs e)
		{
			if (!animate)
			{
				animate = true;
				Body.UpdateHeight(minHeight);
			}
		}
		protected override void Collided(Collider collider)
		{
			spBar.Damage(spBar.MaxValue);
			prevCollider = collider;
		}
		public override void Draw(Graphics g)
		{
			spBar.Draw(g);
			if (spBar.Alive)
				g.DrawArc(Pens.Blue, Body.Left, Body.Top, Body.Width, Body.Height, 180, 180);
		}
		public override void Update(long gameTime)
		{
			if (!spBar.Alive)
			{
				if (spBar.Heal((spBar.MaxValue / 120) / (Window.fps))) // Player upgrade rates? Heals in 2 minutes
				{
					animate = true;
					Body.UpdateHeight(minHeight);
				}
			}

			if (animate)
			{
				Body.UpdateHeight(Body.Height + 2);
				if (Body.Height >= maxHeight)
				{
					Body.UpdateHeight(maxHeight);
					animate = false;
				}
			}

			spBar.Update(gameTime);
		}
		public override void PostUpdate(long gameTime)
		{
			spBar.PostUpdate(gameTime);
			if (KeypressHandler.Instance.CurrentKey == System.Windows.Forms.Keys.H)
				spBar.Damage(spBar.MaxValue);
		}
	}
}
