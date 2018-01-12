using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class DropdownButton : GameButton
	{
		private const int DROP_SPEED = 1;

		public bool IsHiding { get; set; }
		public bool IsRevealing { get; set; }
		public bool IsRevealed { get; set; }
		private bool isFollowing;
		private int lastPosition;
		private int currPosition;

		public DropdownButton(string s, int x, int y, int w, int h) : base(s, x, y, w, h)
		{
			IsEnabled = false;
		}
		public override void Draw(Graphics g)
		{
			base.Draw(g);
		}
		public override void Update(long gameTime)
		{

			base.Update(gameTime);
		}
		public override void PostUpdate(long gameTime)
		{
			lastPosition = currPosition;
			currPosition = PrevButton.Body.Top;
			base.PostUpdate(gameTime);
		}
		protected override void CheckPosition()
		{
			if (IsHiding)
			{
				if (!isFollowing)
					Body.AdjustY(-DROP_SPEED);
				else
					Body.UpdatePositionY(PrevButton.Body.Top);
				if (currPosition == lastPosition && isFollowing == true)
				{
					IsRevealed = false;
					IsVisible = false;
					IsHiding = false;
					IsEnabled = true;
					Body.UpdatePositionY(PrevButton.Body.Top);
				}
				if (Body.Top <= PrevButton.Body.Top)
				{
					if (IsVisible)
					{
						isFollowing = true;
					}
					else
						isFollowing = false;
					if (PrevButton is DropdownButton && ((DropdownButton)PrevButton).IsVisible)
					{
						((DropdownButton)PrevButton).IsHiding = true;
					}
				}
			}
			if (IsRevealing)
			{
				IsVisible = true;
				if (Body.Top >= PrevButton.Body.Top + Utils.SEPERATION_VALUE)
				{
					Body.UpdatePositionY(PrevButton.Body.Top + Utils.SEPERATION_VALUE);
					IsRevealed = true;
					IsRevealing = false;
					IsEnabled = true;
				}
				Body.AdjustY(DROP_SPEED);
			}
		}
	}
}
