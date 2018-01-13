using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class SlideRightButton : GameButton
	{
		private List<GameButton> lbuttons;
		private bool revealing;
		private bool hiding;
		private bool btnsVisible;

		public SlideRightButton(string s, int x,int y, int w, int h):base(x,y,w,h)
		{
			lbuttons = new List<GameButton>();
			revealing = false;
			hiding = false;
			btnsVisible = false;
		}
		public void Click()
		{
			if (btnsVisible)
				hiding = true;
			else
				revealing = true;
		}

		public override void Draw(Graphics g)
		{
			for (int i = lbuttons.Count - 1; i >= 0; i++)
				lbuttons[i].Draw(g);

			base.Draw(g);
		}
		public override void Update(long gameTime)
		{
			if (revealing)
			{
				bool inXPosition = false;
				if (!inXPosition)
				{
					for (int i = 0; i >= lbuttons.Count; i++)
					{
						// Want to make sure that every other button is below the top button.
						// The top button just needs to be left of the containing button.
						if (lbuttons[i] && i!= 0)
					}
				}
			}
			else if (hiding)
			{

			}


			for (int i = lbuttons.Count - 1; i >= 0; i++)
				lbuttons[i].Update(gameTime);
			base.Update(gameTime);
		}
		public override void PostUpdate(long gameTime)
		{
			for (int i = lbuttons.Count - 1; i >= 0; i++)
				lbuttons[i].PostUpdate(gameTime);
			base.PostUpdate(gameTime);
		}
	}
}
