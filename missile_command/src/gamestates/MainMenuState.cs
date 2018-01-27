using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class MainMenuState : State
	{
		private List<Component> bombs;
		private List<Component> components;
		private long elapsedTime;
		private int players;
		private StringFormat sf;
		private int titleX;
		private int titleY;

		public MainMenuState(Window g) : base(g)
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

			newGameButton = new LadderButton("New Game", startX - btnWidth / 2, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			newGameButton.Click += (sender, e) => { ((LadderButton)sender).Toggle(); };

			onePlayer = new LadderButton("One Player", startX - btnWidth / 2, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			onePlayer.Click += (sender, e) => { players = 1; ((LadderButton)sender).Toggle(); };
			onePlayer.Enabled = false;
			onePlayer.IsVisible = false;

			twoPlayer = new LadderButton("Two Players", startX - btnWidth / 2, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			twoPlayer.Click += (sender, e) => { players = 2; ((LadderButton)sender).Toggle(); };
			twoPlayer.Enabled = false;
			twoPlayer.IsVisible = false;

			threePlayer = new LadderButton("Three Player", startX - btnWidth / 2, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			threePlayer.Click += (sender, e) => { players = 3; ((LadderButton)sender).Toggle(); };
			threePlayer.Enabled = false;
			threePlayer.IsVisible = false;

			//Attach the buttons.
			newGameButton.AddButton(onePlayer); newGameButton.AddButton(twoPlayer); newGameButton.AddButton(threePlayer);

			survival = new GameButton("Survival Mode", startX - btnWidth / 2, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			survival.Click += (sender, e) => { game.NextState(new PlayState(players, GameModes.SURVIVAL, game)); };
			survival.Enabled = false;
			survival.IsVisible = false;
			wave = new GameButton("Wave Mode", startX - btnWidth / 2, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			wave.Click += (sender, e) => { game.NextState(new PlayState(players, GameModes.WAVE, game)); };
			wave.Enabled = false;
			wave.IsVisible = false;


			onePlayer.AddButton(survival); onePlayer.AddButton(wave);
			twoPlayer.AddButton(survival); twoPlayer.AddButton(wave);
			threePlayer.AddButton(survival); threePlayer.AddButton(wave);

			numButton++;

			settingsButton = new GameButton("Settings", startX - btnWidth / 2, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			settingsButton.Click += (sender, e) => { new Settings().ShowDialog(); };
			numButton++;

			highscoresButton = new GameButton("Highscores", startX - btnWidth / 2, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
			highscoresButton.Click += (sender, e) => { new HighscoresForm().ShowDialog(); };
			numButton++;

			exitButton = new GameButton("Exit", startX - btnWidth / 2, startY + Consts.SEPERATION_VALUE * numButton, btnWidth, btnHeight);
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

			g.DrawString("Missile Command", new Font("Times New Roman", 30), new SolidBrush(Config.Instance.SystemColor), titleX, titleY, sf);
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
				elapsedTime = gameTime;
				Random rand = new Random();
				Point o = new Point(rand.Next(0, Consts.gameBounds.Width), 0);
				Point destination = new Point(rand.Next(Consts.gameBounds.Width/2, Consts.gameBounds.Width), Consts.gameBounds.Height);
				if (o.X < Consts.gameBounds.Width / 2)
					destination.X = rand.Next(0, Consts.gameBounds.Width / 2);
				Bomb bmb = EntityFactory.MakeBomb(o.X, o.Y, destination, PType.ENEMY, ETag.ENEMY);
				bmb.Speed = 10.0f;
				bmb.DestroyBomb += (Entity b) => { bombs.Remove(b); };
				bombs.Add(bmb);
			}
	}
}
