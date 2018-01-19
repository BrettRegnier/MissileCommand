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
		private List<Component> bombs;
		private List<Component> components;
		private long elapsedTime;
		private int players;
		private StringFormat sf;
		private int titleX;
		private int titleY;

		public MenuState(Window g) : base(g)
		{
			// Init fields
			bombs = new List<Component>();
			components = new List<Component>();
			elapsedTime = 0;
			players = 0;

			sf = new StringFormat
			{
				LineAlignment = StringAlignment.Center,
				Alignment = StringAlignment.Center
			};
			titleX = Consts.gameBounds.Width / 2;
			titleY = 150;

			int numButton = 0;
			int startX = Consts.gameBounds.Width / 2;
			int startY = 200;
			int btnWidth = 100;
			int btnHeight = 30;

			// declare buttons
			LadderButton newGameButton;

			//Player buttons
			LadderButton onePlayer;
			LadderButton twoPlayer;
			LadderButton threePlayer;

			// Mode buttons
			GameButton survival;
			GameButton wave;

			GameButton settingsButton;
			GameButton highscoresButton;
			GameButton exitButton;

			newGameButton = new LadderButton("New Game", startX, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			newGameButton.Click += (sender, e) => { ((LadderButton)sender).Toggle(); };

			onePlayer = new LadderButton("One Player", startX, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			onePlayer.Click += (sender, e) => { players = 1; ((LadderButton)sender).Toggle(); };
			onePlayer.IsEnabled = false;
			onePlayer.IsVisible = false;

			twoPlayer = new LadderButton("Two Players", startX, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			twoPlayer.Click += (sender, e) => { players = 2; ((LadderButton)sender).Toggle(); };
			twoPlayer.IsEnabled = false;
			twoPlayer.IsVisible = false;

			threePlayer = new LadderButton("Three Player", startX, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			threePlayer.Click += (sender, e) => { players = 3; ((LadderButton)sender).Toggle(); };
			threePlayer.IsEnabled = false;
			threePlayer.IsVisible = false;

			//Attach the buttons.
			newGameButton.AddButton(onePlayer); newGameButton.AddButton(twoPlayer); newGameButton.AddButton(threePlayer);

			survival = new GameButton("Survival Mode", startX, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			survival.Click += (sender, e) => { game.NextState(new GameState(players, GameModes.SURVIVAL, game)); };
			survival.IsEnabled = false;
			survival.IsVisible = false;
			wave = new GameButton("Wave Mode", startX, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			wave.Click += (sender, e) => { game.NextState(new GameState(players, GameModes.WAVE, game)); };
			wave.IsEnabled = false;
			wave.IsVisible = false;


			onePlayer.AddButton(survival); onePlayer.AddButton(wave);
			twoPlayer.AddButton(survival); twoPlayer.AddButton(wave);
			threePlayer.AddButton(survival); threePlayer.AddButton(wave);

			numButton++;

			settingsButton = new GameButton("Settings", startX, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			settingsButton.Click += (sender, e) => { new Settings().ShowDialog(); };
			numButton++;

			highscoresButton = new GameButton("Highscores", startX, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			highscoresButton.Click += (sender, e) => { };
			numButton++;

			exitButton = new GameButton("Exit", startX, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			exitButton.Click += (sender, e) => { game.Close(); };
			numButton++;


			// add buttons to components list
			components.Add(newGameButton);
			components.Add(settingsButton);
			components.Add(highscoresButton);
			components.Add(exitButton);
		}
		public override void Draw(Graphics g)
		{
			for (int i = 0; i < bombs.Count; i++)
				bombs[i].Draw(g);
			for (int i = 0; i < components.Count; i++)
				components[i].Draw(g);

			g.DrawString("Missile Command", new Font("Times New Roman", 30), new SolidBrush(Color.Green), titleX, titleY, sf);
		}
		public override void Update(long gameTime)
		{
			SpawnBombs(gameTime);

			for (int i = 0; i < bombs.Count; i++)
				bombs[i].Update(gameTime);
			for (int i = 0; i < components.Count; i++)
				components[i].Update(gameTime);
		}
		public override void PostUpdate(long gameTime)
		{
			for (int i = 0; i < bombs.Count; i++)
				bombs[i].PostUpdate(gameTime);
			for (int i = 0; i < components.Count; i++)
				components[i].PostUpdate(gameTime);
		}
		private void SpawnBombs(long gameTime)
		{
			if (gameTime > elapsedTime + 200)
			{
				elapsedTime = gameTime;
				Random rand = new Random();
				Point o = new Point(rand.Next(0, Consts.gameBounds.Width), 0);
				Point destination = new Point(rand.Next(o.X, Consts.gameBounds.Width), Consts.gameBounds.Height);
				if (o.X < Consts.gameBounds.Width / 2)
					destination.X = rand.Next(0, Consts.gameBounds.Width / 2);
				Bomb bmb = EntityFactory.MakeBomb(o.X, o.Y, destination, PType.ENEMY, ETag.ENEMY);
				bmb.DestroyBomb += (Entity b) => { bombs.Remove(b); };
				bombs.Add(bmb);

				o = new Point(rand.Next(0, Consts.gameBounds.Width), Consts.gameBounds.Height);
				destination = new Point(rand.Next(o.X, Consts.gameBounds.Width), 0);
				if (o.X < Consts.gameBounds.Width / 2)
					destination.X = rand.Next(0, Consts.gameBounds.Width / 2);
				Bomb bmb2 = EntityFactory.MakeBomb(o.X, o.Y, destination, PType.ENEMY, ETag.ENEMY);
				bmb2.DestroyBomb += (Entity b) => { bombs.Remove(b); };
				bombs.Add(bmb2);
			}
		}
	}
}
