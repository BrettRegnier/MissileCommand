using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class PauseState : State
	{
		private List<Component> components;
		private PlayState prevState;

		public PauseState(Window g, PlayState s) : base(g)
		{
			components = new List<Component>();
			prevState = s;

			int startX = Consts.gameBounds.Width / 2;
			int startY = 200;
			int btnWidth = 100;
			int btnHeight = 30;
			int count = 0;

			GameButton resumeButton;
			GameButton settingButton;
			GameButton mainMenuButton;
			GameButton exitButton;

			settingButton = new GameButton("Settings", startX - btnWidth / 2, startY + Consts.SEPERATION_VALUE * ++count, btnWidth, btnHeight);
			settingButton.Click += (sender, e) => { new Settings().ShowDialog(); };

			mainMenuButton = new GameButton("Main Menu", startX - btnWidth / 2, startY + Consts.SEPERATION_VALUE * ++count, btnWidth, btnHeight);
			mainMenuButton.Click += (sender, e) => { g.NextState(new MainMenuState(g)); };

			exitButton = new GameButton("Exit Game", startX - btnWidth / 2, startY + Consts.SEPERATION_VALUE * ++count, btnWidth, btnHeight);
			exitButton.Click += (sender, e) => { g.Close(); };

			resumeButton = new GameButton("Resume", startX - btnWidth / 2, startY, btnWidth, btnHeight);
			resumeButton.Click += (sender, e) =>
			{
				settingButton.Enabled = false;
				mainMenuButton.Enabled = false;
				exitButton.Enabled = false;

				prevState.Resume();
				g.NextState(prevState);
			};

			components.Add(resumeButton);
			components.Add(settingButton);
			components.Add(mainMenuButton);
			components.Add(exitButton);
		}
		public override void Draw(Graphics g)
		{
			prevState.Draw(g);

			for (int i = 0; i < components.Count; i++)
				components[i].Draw(g);
		}
		public override void PostUpdate(long gameTime)
		{
			prevState.Update(gameTime);

			for (int i = 0; i < components.Count; i++)
				components[i].Update(gameTime);
		}
		public override void Update(long gameTime)
		{
			prevState.Update(gameTime);

			for (int i = 0; i < components.Count; i++)
				components[i].PostUpdate(gameTime);
		}
	}
}
