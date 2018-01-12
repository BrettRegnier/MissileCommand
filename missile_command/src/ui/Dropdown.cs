using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class Dropdown : GameButton
	{
		private List<DropdownButton> buttons;
		private int btnIndex;
		private bool isRevealed;

		public Dropdown(string s, int x, int y, int w, int h) : base(s, x, y, w, h)
		{
			buttons = new List<DropdownButton>();
			btnIndex = 0;
			isRevealed = false;
		}
		public void AttachButton(DropdownButton btn)
		{
			if (!buttons.Contains(btn))
				buttons.Add(btn);
		}
		public override void Draw(Graphics g)
		{
			for (int i = 2; i >= 0; i--)
				buttons[i].Draw(g);

			base.Draw(g);

		}
		public void DropDown()
		{
			if (!isRevealed)
			{
				foreach (DropdownButton btn in buttons)
				{
					btn.IsRevealing = true;
					btn.IsEnabled = false;
				}
			}
			else
			{
				buttons[buttons.Count - 1].IsHiding = true;
				foreach (DropdownButton btn in buttons)
					btn.IsEnabled = false;
			}
		}
		public override void Update(long gameTime)
		{
			isRevealed = true;
			for (int i = 2; i >= 0; i--)
			{
				buttons[i].Update(gameTime);
				isRevealed &= buttons[i].IsRevealed;
			}

			base.Update(gameTime);
		}
		public override void PostUpdate(long gameTime)
		{
			for (int i = 2; i >= 0; i--)
				buttons[i].PostUpdate(gameTime);

			base.PostUpdate(gameTime);
		}
	}
}
