using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class GameOverState : State
	{
		private PlayState prevState;
		private List<Component> components;

		private StringFormat sf;

		public GameOverState(Window g, PlayState s) : base(g)
		{
			components = new List<Component>();
			prevState = s;
			sf = new StringFormat
			{
				LineAlignment = StringAlignment.Center,
				Alignment = StringAlignment.Center
			};

			int startX = Consts.gameBounds.Width / 2;
			int startY = Consts.gameBounds.Height / 2 - 50;
			int btnWidth = 100;
			int btnHeight = 30;

			GameButton restartButton;
			GameButton enterHighscorebutton;
			GameButton mainMenuButton;
			GameButton exitButton;

			enterHighscorebutton = new GameButton("Enter Highscore", startX - btnWidth - 5, startY, btnWidth, btnHeight);
			enterHighscorebutton.Click += (sender, e) =>
			{
				long score = prevState.GetScore();
				new HighscoresForm(score).ShowDialog();
				enterHighscorebutton.Enabled = false;
			};

			restartButton = new GameButton("Restart", enterHighscorebutton.Body.Left - btnWidth - 10, startY, btnWidth, btnHeight);
			restartButton.Click += (sender, e) => { prevState.Restart(); };

			mainMenuButton = new GameButton("Main Menu", startX + 5, startY, btnWidth, btnHeight);
			mainMenuButton.Click += (sender, e) => { g.NextState(new MainMenuState(g)); };

			exitButton = new GameButton("Exit Game", mainMenuButton.Body.Right + 10, startY, btnWidth, btnHeight);
			exitButton.Click += (sender, e) => { g.Close(); };

			components.Add(restartButton);
			components.Add(enterHighscorebutton);
			components.Add(mainMenuButton);
			components.Add(exitButton);
		}
		public override void Draw(Graphics g)
		{
			prevState.Draw(g);

			g.DrawString("Game Over", new Font("Times New Roman", 100), new SolidBrush(Config.Instance.SystemColor), Consts.gameBounds.Width / 2, Consts.gameBounds.Height / 2 - 100, sf);

			for (int i = 0; i < components.Count; i++)
				components[i].Draw(g);
		}
		public override void Update(long gameTime)
		{
			prevState.Update(gameTime);

			for (int i = 0; i < components.Count; i++)
				components[i].Update(gameTime);
		}
		public override void PostUpdate(long gameTime)
		{
			prevState.PostUpdate(gameTime);

			for (int i = 0; i < components.Count; i++)
				components[i].PostUpdate(gameTime);
		}
	}
}
