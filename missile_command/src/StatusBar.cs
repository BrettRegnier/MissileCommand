using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	abstract class StatusBar : Entity
	{
		private const int OUTLINE_OFFSET = 1;

		// TODO move this into an upgrades class
		#region NeedsMoving
		private const int REPLENISH_TICK_SETTING = 10;
		private const int REPLENISH_RATE = 2;
		private const int REPLENISH_AMOUNT = 1;
		#endregion

		protected Brush innerBrush;
		protected Pen outlinePen;

		protected Rectangle innerBar;
		protected Rectangle outlineBar;

		protected int curHP;
		protected int maxHP;
		protected int replishTick;
		protected bool isAlive;

		public StatusBar(Point o, Size d, ETag t) : base(o, d, t)
		{
			outlinePen = new Pen(Color.Black);

			MovePositionY(dimension.Height);

			// 2 pixels on all sides
			int nX = Left() - OUTLINE_OFFSET;
			int nY = Top() - OUTLINE_OFFSET;
			int nW = dimension.Width + OUTLINE_OFFSET;
			int nH = dimension.Height + OUTLINE_OFFSET;
			outlineBar = new Rectangle(nX, nY, nW, nH);

			maxHP = d.Width;
			curHP = maxHP;
			UpdateDimension(curHP, dimension.Height);

			replishTick = 0;
			isAlive = true;
		}
		public override void Draw(Graphics g)
		{
			g.DrawRectangle(outlinePen, outlineBar);
			g.FillRectangle(innerBrush, Left(), Top(), dimension.Width, dimension.Height);
			Replenish();
		}
		private void UpdateBarWidth()
		{
			innerBar.Width = curHP;
		}
		private void Replenish()
		{
			// TODO rethink how I want to implement this.
			// TODO use upgrade player values to determine the rate.
			if (replishTick >= REPLENISH_TICK_SETTING)
			{
				if (curHP >= maxHP)
				{
					curHP = maxHP;
					isAlive = true;
				}
				else
				{
					// TODO animate it so its smoother when its > 1
					curHP += REPLENISH_AMOUNT;
				}
				UpdateDimension(curHP, dimension.Height);
				replishTick = REPLENISH_TICK_SETTING % 10;
			}
			else
			{
				replishTick += REPLENISH_RATE;
			}
		}
		public bool IsAlive()
		{
			return isAlive;
		}
		public abstract void Damage();
	}
	class ShieldBar : StatusBar
	{
		public ShieldBar(Point o, Size d, ETag t) : base(o, d, t)
		{
			innerBrush = Brushes.Blue;
		}
		public override void Damage()
		{
			curHP = 0;
			isAlive = false;
		}
	}
	class HealthBar : StatusBar
	{
		public HealthBar(Point o, Size d, ETag t) : base(o, d, t)
		{
			innerBrush = Brushes.Red;
		}
		public override void Damage()
		{
			// TODO think of good way to damage health
			// TODO animate the drain of the health
			if (curHP > maxHP / 2)
			{
				curHP -= maxHP / 2;
			}
			else
			{
				curHP = 0;
				isAlive = false;
			}
		}
	}
}
