using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class Status : Component
	{
		private const int OUTLINE_OFFSET = 1;

		private Brush innerBrush;
		private Pen outlinePen;

		private Rectangle innerRectangle;
		protected Rectangle outlineRectangle;
		
		private double curValue;
		public double MaxValue { get; set; }

		public bool Alive { get; private set; }

		public Status(int max, Color c, int x, int y, int w, int h) : base(x, y, w, h)
		{
			outlinePen = new Pen(Color.Black);
			innerBrush = new SolidBrush(c);

			Body.AdjustY(Body.Dimension.Height + OUTLINE_OFFSET);
			Body.AdjustX(-Body.Dimension.Width / 2);
			innerRectangle = new Rectangle(Body.Left, Body.Top, Body.Width, Body.Height);

			// 2 pixels on all sides
			int nX = Body.Left - OUTLINE_OFFSET;
			int nY = Body.Top - OUTLINE_OFFSET;
			int nW = Body.Width + OUTLINE_OFFSET;
			int nH = Body.Height + OUTLINE_OFFSET;
			outlineRectangle = new Rectangle(nX, nY, nW, nH);

			curValue = max;
			MaxValue = max;
			UpdateStatus();

			Alive = true;
		}
		private void UpdateStatus()
		{
			int value = Convert.ToInt32((curValue / MaxValue) * Body.Width);
			innerRectangle.Width = value;
		}
		public bool Heal(double amount)
		{
			bool r = false;
			curValue += amount;
			if (curValue >= MaxValue)
			{
				curValue = MaxValue;
				Alive = true;
				r = true;
			}
			UpdateStatus();
			return r;
		}
		public bool Damage(double amount)
		{
			bool r = false;
			if (Alive)
			{
				curValue -= amount;
				if (curValue <= 0)
				{
					curValue = 0;
					Alive = false;
					r = true;
				}
			}
			UpdateStatus();
			return r;
		}
		public override void Draw(Graphics g)
		{
			g.DrawRectangle(outlinePen, outlineRectangle);
			g.FillRectangle(innerBrush, innerRectangle);
		}
		public override void PostUpdate(long gameTime)
		{

		}
		public override void Update(long gameTime)
		{

		}
	}
}
