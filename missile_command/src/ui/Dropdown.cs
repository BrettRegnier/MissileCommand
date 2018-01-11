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
		private List<GameButton> buttons;
		private int btnIndex;
		private bool isRevealed;

		public Dropdown(string s, int x, int y, int w, int h) : base(s, x, y, w, h)
		{
			buttons = new List<GameButton>();
			btnIndex = 0;
			isRevealed = false;
		}
		public void AttachButton(GameButton btn)
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
				buttons[btnIndex].IsRevealing = true;
				buttons[btnIndex].IsVisible = true;
			}
		}
		public override void Update(long gameTime)
		{
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
