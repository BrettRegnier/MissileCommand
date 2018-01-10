using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class MenuState : State
	{
		List<Component> lComponents = new List<Component>();
		public MenuState(Window g) : base(g)
		{
			Button newGameButton = new Button("New Game", Utils.gameBounds.Width / 2, 200, 100, 30);
			newGameButton.Click += NewGameButton_Click;
			lComponents.Add(newGameButton);

			Button highScoresButton = new Button("Highscores", Utils.gameBounds.Width / 2, 240, 100, 30);
			highScoresButton.Click += HighScoresButton_Click;
			lComponents.Add(highScoresButton);

			Button exitButton = new Button("Exit", Utils.gameBounds.Width / 2, 280, 100, 30);
			exitButton.Click += ExitButton_Click;
			lComponents.Add(exitButton);
		}
		public override void Update(long gameTime)
		{
			foreach (Component component in lComponents)
				component.Update(gameTime);
		}
		public override void PostUpdate(long gameTime)
		{
			foreach (Component component in lComponents)
				component.PostUpdate(gameTime);
		}
		public override void Draw(Graphics g)
		{
			foreach (Component component in lComponents)
				component.Draw(g);
		}

		private void NewGameButton_Click(object sender, EventArgs e)
		{
			game.NextState(new GameState(game));
		}
		private void HighScoresButton_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
		private void ExitButton_Click(object sender, EventArgs e)
		{
			game.Close();
		}
	}
}
