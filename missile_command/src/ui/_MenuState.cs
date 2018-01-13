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
		private int players = 0;
		private long elapsedTime = 0;
		List<Component> lComponents = new List<Component>();
		List<Component> lBombs = new List<Component>();

		public MenuState(Window g) : base(g)
		{
			int numButton = 0;
			int startX = Consts.gameBounds.Width / 2;
			int startY = 200;
			int btnWidth = 100;
			int btnHeight = 30;

			// declare buttons
			SlideRightButton newGameButton;
			GameButton highscoresButton;
			GameButton exitButton;

			newGameButton = new SlideRightButton("New Game", startX, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			newGameButton.Click += (sender, e) => { };
			numButton++;

			highscoresButton = new GameButton("Highscores", startX, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			highscoresButton.Click += HighScoresButton_Click;
			numButton++;

			exitButton = new GameButton("Exit", startX, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			exitButton.Click += (sender, e) => { game.Close(); };
			numButton++;


			// add buttons to components list
			lComponents.Add(newGameButton);
			lComponents.Add(highscoresButton);
			lComponents.Add(exitButton);
		}
		public override void Draw(Graphics g)
		{
			foreach (Component component in lComponents)
				component.Draw(g);
		}
		public override void Update(long gameTime)
		{
			foreach (Component component in lComponents)
				component.Update(gameTime);

			elapsedTime = gameTime;
		}
		public override void PostUpdate(long gameTime)
		{
			foreach (Component component in lComponents)
				component.PostUpdate(gameTime);
		}
		private void SurvivalMode_Click(object sender, EventArgs e)
		{
			game.NextState(new GameState(players, GameModes.SURVIVAL, game));
		}
		private void WaveMode_Click(object sender, EventArgs e)
		{
			game.NextState(new GameState(players, GameModes.WAVE, game));
		}
		private void HighScoresButton_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}
		private void ExitButton_Click(object sender, EventArgs e)
		{
		}
		private void SpawnBombs(long gameTime)
		{
			if (gameTime > elapsedTime + 1000)
			{
				Random rand = new Random();
				Point spawnPoint = new Point(rand.Next(0, Consts.gameBounds.Width), 0);
				// TODO make a list of guaranteed points
				Point destination = new Point(rand.Next(0, Consts.gameBounds.Width), Consts.gameBounds.Height);
				Bomb bmb = EntityFactory.MakeBomb(spawnPoint, destination, PType.ENEMY, missile_command.ETag.ENEMY);
			}
		}
	}
}
