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
		private const int HEAL_AMOUNT = 1;
		#endregion

		protected Brush innerBrush;
		protected Pen outlinePen;

		protected Rectangle outlineBar;

		protected int curHP;
		protected int maxHP;
		protected int replenishTick;
		// The replish require needs to reach a certain value before curHP can be increased
		protected int replenishValue;
		protected int replenishRequire; //TODO player upgrades

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
			Body.UpdateWidth(curHP);

			replenishTick = 0;
			replenishValue = 0;
			// Player upgrades can alter this value.
			replenishRequire = 40;

			isAlive = true;
			isDamaged = false;
			isRestored = false;
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
			// TODO might want to have different speeds for the Tower and the shield
			if (replenishTick >= REPLENISH_TICK_SETTING)
			{
				if (isDamaged)
				{
					replenishValue += REPLENISH_AMOUNT;
					// TODO animate it so its smoother when its > 1
					if (replenishValue >= replenishRequire)
					{
						replenishValue %= replenishRequire;
						curHP += HEAL_AMOUNT;
						Body.UpdateWidth(curHP);
					}
				}
				replenishTick %= REPLENISH_TICK_SETTING;
			}
			else
			{
				replenishTick += REPLENISH_RATE;
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
			Body.UpdateWidth(curHP);
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
			if (curHP >= maxHP / 2)
			{
				curHP -= maxHP / 2 + 5;
			}
			else
			{
				curHP = 0;
				isAlive = false;
				isRestored = false;
			}
			Body.UpdateWidth(curHP);
			isDamaged = true;
		}
	}
}
