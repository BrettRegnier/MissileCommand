using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	abstract class StatusBar : Component
	{
		public event EventHandler Healed;

		private const int OUTLINE_OFFSET = 1;

		// TODO move this into an upgrades class
		#region NeedsMoving
		private const int REPLENISH_TICK_SETTING = 10;
		private const int REPLENISH_RATE = 2;
		private const int REPLENISH_AMOUNT = 1;
		#endregion

		protected Brush innerBrush;
		protected Pen outlinePen;

		protected Rectangle outlineBar;

		protected int curHP;
		protected int maxHP;
		protected int replishTick;

		protected bool isAlive;
		protected bool isDamaged;
		protected bool isRestored;

		public StatusBar(int x, int y, int w, int h) : base(x, y, w, h)
		{
			outlinePen = new Pen(Color.Black);

			Body.AdjustY(Body.Dimension.Height + OUTLINE_OFFSET);
			Body.AdjustX(-Body.Dimension.Width / 2);

			// 2 pixels on all sides
			int nX = Body.Left - OUTLINE_OFFSET;
			int nY = Body.Top - OUTLINE_OFFSET;
			int nW = Body.Width + OUTLINE_OFFSET;
			int nH = Body.Height + OUTLINE_OFFSET;
			outlineBar = new Rectangle(nX, nY, nW, nH);

			maxHP = Body.Width;
			curHP = maxHP;
			Body.UpdateDimension(curHP, Body.Height);

			replishTick = 0;
			isAlive = true;
			isDamaged = false;
		}
		public abstract void Damage();

		public override void Draw(Graphics g)
		{
			g.DrawRectangle(outlinePen, outlineBar);
			g.FillRectangle(innerBrush, Body.Left, Body.Top, Body.Width, Body.Height);
		}
		public override void Update(long gameTIme)
		{
			// TODO rethink how I want to implement this.
			// TODO use upgrade player values to determine the rate.
			if (replishTick >= REPLENISH_TICK_SETTING)
			{
				if (isDamaged)
				{
					// TODO animate it so its smoother when its > 1
					curHP += REPLENISH_AMOUNT;
				}
				Body.UpdateDimension(curHP, Body.Height);
				replishTick = REPLENISH_TICK_SETTING % 10;
			}
			else
			{
				replishTick += REPLENISH_RATE;
			}

		}
		public override void PostUpdate(long gameTime)
		{
			if (curHP >= maxHP)
			{
				curHP = maxHP;
				isAlive = true;
				if (!isRestored)
				{
					isRestored = true;
					Healed?.Invoke(this, new EventArgs());
				}
			}
		}
		public bool IsAlive
		{
			get
			{
				return isAlive;
			}
		}

	}
	class ShieldBar : StatusBar
	{
		public ShieldBar(int x, int y, int w, int h) : base(x, y, w, h)
		{
			innerBrush = Brushes.Blue;
		}
		public override void Damage()
		{
			curHP = 0;
			isAlive = false;
			isDamaged = true;
			isRestored = false;
		}
	}
	class HealthBar : StatusBar
	{
		public HealthBar(int x, int y, int w, int h) : base(x, y, w, h)
		{
			innerBrush = Brushes.Red;
		}
		public override void Damage()
		{
			// TODO think of good way to damage health
			// TODO animate the drain of the health
			if (curHP >= maxHP / 2)
			{
				curHP -= maxHP / 2;
			}
			else
			{
				curHP = 0;
				isAlive = false;
				isRestored = false;
			}
			isDamaged = true;
		}
	}
}
