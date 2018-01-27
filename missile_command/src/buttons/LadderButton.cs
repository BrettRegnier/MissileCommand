using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class LadderButton : GameButton
	{
		private List<GameButton> buttons;
		private bool revealing;
		private bool hiding;
		internal bool btnsVisible;

		private int xMoveSpeed = 5;
		private int yMoveSpeed = 5;

		public LadderButton(string s, int x, int y, int w, int h) : base(s, x, y, w, h)
		{
			buttons = new List<GameButton>();
			revealing = false;
			hiding = false;
			btnsVisible = false;
		}
		public void Toggle()
		{
			foreach (GameButton btn in buttons)
				btn.Enabled = false;

			if (btnsVisible)
			{
				hiding = true;
				foreach (GameButton btn in buttons)
				{
					if (btn is LadderButton)
					{
						if (((LadderButton)btn).btnsVisible)
							((LadderButton)btn).Toggle();
					}
				}
			}
			else
			{
				revealing = true;
			}
		}
		public void AddButton(GameButton btn)
		{
			buttons.Add(btn);
		}
		public override void Draw(Graphics g)
		{
			for (int i = buttons.Count - 1; i >= 0; i--)
				buttons[i].Draw(g);

			base.Draw(g);
		}
		internal void PositionHiddenButtons()
		{
			foreach (GameButton btn in buttons)
				btn.Body.UpdatePositionX(this.Body.Left);
		}
		public override void Update(long gameTime)
		{
			// Trying to make the buttons for one, two, and three player not move when I need it to stay after they are revealed.
			if (revealing)
			{
				foreach (GameButton btn in buttons)
					btn.IsVisible = true;

				bool inXPosition = false;
				bool inYPosition = false;
				if (!inXPosition)
				{
					inXPosition = true;
					for (int i = 0; i < buttons.Count; i++)
					{
						// Want to make sure that every other button is below the top button.
						// The top button just needs to be left of the containing button.
						if (buttons[i].Body.Left >= this.Body.Left + this.Body.Width + 4)
						{
							buttons[i].Body.UpdatePositionX(this.Body.Left + this.Body.Width + 4);
							if (buttons[i] is LadderButton)
							{
								((LadderButton)buttons[i]).PositionHiddenButtons();
							}
						}
						else
						{
							buttons[i].Body.AdjustX(xMoveSpeed);


							inXPosition = false;
						}
					}
				}
				if (inXPosition)
				{
					inYPosition = true;
					for (int i = 1; i < buttons.Count; i++)
					{
						// Want to move all of the buttons below their containing button.
						if (buttons[i].Body.Top >= buttons[i - 1].Body.Top + Consts.SEPERATION_VALUE)
						{
							buttons[i].Body.UpdatePositionY(buttons[i - 1].Body.Top + Consts.SEPERATION_VALUE);
						}
						else
						{
							buttons[i].Body.AdjustY(yMoveSpeed);
							inYPosition = false;
						}
					}
				}
				if (inYPosition)
				{
					foreach (GameButton btn in buttons)
						btn.Enabled = true;
					revealing = false;
					btnsVisible = true;
				}
			}
			else if (hiding)
			{
				bool inYPosition = false;
				bool inXPosition = false;
				if (!inYPosition)
				{
					inYPosition = true;
					for (int i = 1; i < buttons.Count; i++)
					{
						if (buttons[i].Body.Top - yMoveSpeed >= buttons[i - 1].Body.Top)
						{
							buttons[i].Body.AdjustY(-yMoveSpeed);
							inYPosition = false;
						}
						else
						{
							buttons[i].Body.UpdatePositionY(buttons[i - 1].Body.Top);
						}
					}
				}
				if (inYPosition)
				{
					inXPosition = true;
					for (int i = 0; i < buttons.Count; i++)
					{
						if (buttons[i].Body.Left - xMoveSpeed >= this.Body.Left)
						{
							buttons[i].Body.AdjustX(-xMoveSpeed);
							inXPosition = false;
						}
						else
						{
							buttons[i].Body.UpdatePositionX(this.Body.Left);
							buttons[i].IsVisible = false;
						}
					}
				}
				if (inXPosition)
				{
					hiding = false;
					btnsVisible = false;
				}
			}


			for (int i = buttons.Count - 1; i >= 0; i--)
			{
				buttons[i].Update(gameTime);
			}
			base.Update(gameTime);
		}
		public override void PostUpdate(long gameTime)
		{
			for (int i = buttons.Count - 1; i >= 0; i--)
				buttons[i].PostUpdate(gameTime);
			base.PostUpdate(gameTime);
		}
	}
}
