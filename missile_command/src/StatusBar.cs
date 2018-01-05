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
		private const int OUTLINE_OFFSET = 4;

		protected Pen innerPen;
		protected Pen outlinePen;

		protected Rectangle innerBar;
		protected Rectangle outlineBar;

		protected int curHP;
		protected int maxHP;

		public StatusBar(Point o, Size d, ETag t) : base(o, d, t)
		{
			maxHP = d.Width;
			curHP = maxHP;
			outlinePen = new Pen(Color.Black);
			innerBar = new Rectangle(position, dimension);

			// 2 pixels on all sides
			int nX = CenterX() + OUTLINE_OFFSET / 2;
			int nY = CenterY() + OUTLINE_OFFSET / 2;
			int nW = dimension.Width + OUTLINE_OFFSET;
			int nH = dimension.Height + OUTLINE_OFFSET;
			outlineBar = new Rectangle(nX, nY, nW, nH);
		}
		public override void Draw(Graphics g)
		{
			g.DrawRectangle(outlinePen, outlineBar);
			g.DrawRectangle(innerPen, innerBar);
		}
		public void UpdateBarWidth()
		{
			innerBar.Width = curHP;
		}
		public void Replenish()
		{
			// TODO rethink how I want to implement this.
			if (curHP >= maxHP)
			{
				curHP = maxHP;
			}
			else
			{
				curHP++;
			}
		}
		public abstract void Damage();
	}
	class ShieldBar : StatusBar
	{
		public ShieldBar(Point o, Size d, ETag t) : base(o, d, t)
		{
			innerPen = new Pen(Color.Blue);
		}
		public override void Damage()
		{
			curHP = 0;
		}
	}
	class HealthBar : StatusBar
	{
		public HealthBar(Point o, Size d, ETag t) : base(o, d, t)
		{
			innerPen = new Pen(Color.Blue);
		}
		public override void Damage()
		{
			// TODO think of good way to damage health
			if (curHP > 0)
			{
				curHP -= maxHP / 2;
			}
		}
	}
}
