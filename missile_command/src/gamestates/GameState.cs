using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace missile_command
{
	class GameState : State
	{
		private Mode gameMode;

		private List<Entity> components;
		private List<Entity> eBombs;
		private long elapsedTime;
		private bool isPaused;
		private bool isGameOver;
		private List<Entity> pBombs;
		private List<Player> players;
		private Random rand;

		private int playTime;
		private int playTimeX;
		private int playTimeY;

		private int score;
		private int scoreX;
		private int scoreY;

		public GameState(int numPlayers, GameModes gamemode, Window g) : base(g)
		{
			components = new List<Entity>();
			eBombs = new List<Entity>();
			isPaused = false;
			isGameOver = false;
			pBombs = new List<Entity>();
			players = new List<Player>();
			rand = new Random();

			playTime = 0;
			playTimeX = Consts.gameBounds.Width - 150;
			playTimeY = 10;

			score = 0;
			scoreX = 10;
			scoreY = 10;

			if (gamemode == GameModes.SURVIVAL)
				gameMode = new SurvivalMode(this);
			else if (gamemode == GameModes.WAVE)
				gameMode = new WaveMode(this);

			InitGame();
			InitPlayers(numPlayers);
		}
		private void InitGame()
		{
			// Create landmasses
			Point baseLand = new Point(0, Consts.gameBounds.Height);
			Size baseSize = new Size(Consts.gameBounds.Width, Consts.LAND_MASS_HEIGHT);
			LandMass lm = new LandMass(baseLand.X, baseLand.Y, baseSize.Width, baseSize.Height);
			components.Add(lm);

			// Build hills.
			for (int i = 0; i < 3; i++)
			{
				Point p = new Point(Consts.HILL_POSITIONS_X[i], lm.Body.Top);
				components.Add(new LandMass(p.X, p.Y, Consts.HILL_MASS_WIDTH, Consts.HILL_MASS_HEIGHT));
			}

			// Build Cities
			for (int i = 0; i < 6; i++)
			{
				int w_h = Consts.CITY_SIZE;
				int x = Consts.CITY_POSITIONS_X[i];
				int y = Consts.gameBounds.Height - (Consts.LAND_MASS_HEIGHT + w_h);
				City c = new City(x, y, w_h, w_h);
				components.Add(c);
			}
			Cursor.Hide();
		}
		private void InitPlayers(int numPlayers)
		{
			for (int i = 0; i < numPlayers; i++)
			{
				Point ori = new Point((Consts.gameBounds.Width / 3) * (i + 1), 200);
				PType pt = PType.PLAYER;
				ETag ap = ETag.PLAYER1;
				if (i == 1)
					ap = ETag.PLAYER2;
				else if (i == 2)
					ap = ETag.PLAYER3;

				Player p = new Player(pt, ap);
				players.Add(p);

				// Load the current player's config
				Config.Instance.LoadPlayer(p.GetTag());
			}

			Player currentPlayer = players[0];
			for (int i = 0; i < 3; i++)
			{
				if (numPlayers == 2)
				{
					if (i == 1)
						currentPlayer = players[i];
				}
				else if (numPlayers == 3)
					currentPlayer = players[i];

				Size size = new Size(Config.Instance.TurretDiameter, Config.Instance.TurretDiameter);
				Turret t = EntityFactory.MakeTurret(components[i + 1].Body.TopCenter, size, PType.PLAYER, currentPlayer.GetTag());
				t.TurretShoot += P_TurretShoot;
				components.Add(t);
				currentPlayer.AttachTurret(t);
			}

			KeypressHandler.Instance.Initialize(players);
		}
		private string FormatTime()
		{
			// Format time into hrs, minutes, seconds.
			int playTimeSec = playTime % 60;
			int totalMinutes = playTime / 60;
			int playTimeMinutes = totalMinutes % 60;
			int playTimeHours = totalMinutes / 60;
			string seconds = playTimeSec.ToString();
			string minutes = playTimeMinutes.ToString();
			string hours = playTimeHours.ToString();
			if (playTimeSec < 10)
				seconds = "0" + playTimeSec.ToString();
			if (playTimeMinutes < 10)
				minutes = "0" + playTimeMinutes.ToString();
			if (playTimeHours < 10)
				hours = "0" + playTimeHours.ToString();
			return "Play Time: " + hours + ":" + minutes + ":" + seconds;
		}
		private void GameOver()
		{
			isGameOver = true;
			Cursor.Show();

			game.NextState(new GameOverState(game, this));
		}
		private void HotKeys()
		{
			//Keys key = KeypressHandler.Instance.CurrentKey;
			if (KeypressHandler.Instance.Press(Keys.Escape))
			{
				if (!isPaused)
					Pause();
			}
			else if (KeypressHandler.Instance.Press(Keys.Delete))
				GameOver();
		}
		private void P_TurretShoot(Point origin, Point destination, ETag a)
		{
			// use player upgrades, if I add them, to determine the size.
			Bomb bmb = EntityFactory.MakeBomb(origin.X, origin.Y, destination, PType.PLAYER, a);
			bmb.DestroyBomb += (gameObject) => { pBombs.Remove(gameObject); };
			pBombs.Add(bmb);
		}
		private void SpawnEnemies()
		{
			Bomb[] bombs = gameMode.SpawnEnemies();
			for (int i = 0; i < bombs.Length; i++)
			{
				bombs[i].DestroyBomb += (gameObject) =>
				{
					eBombs.Remove(gameObject);
					if (((Bomb)gameObject).GivePoints)
						gameMode.AddPoints(Consts.POINT_VALUE);
				};
				eBombs.Add(bombs[i]);
			}
		}
		// Finds the list of entities that are alive (Cities and Towers)
		public List<Entity> AliveEntities()
		{
			List<Entity> aliveList = new List<Entity>();
			for (int i = 4; i < components.Count; i++)
			{
				if (components[i].Alive)
					aliveList.Add(components[i]);
			}
			return aliveList;
		}
		public int GetScore()
		{
			return score;
		}
		public void Pause()
		{
			isPaused = true;
			game.NextState(new PauseState(game, this));
			Cursor.Show();
		}
		public void Resume()
		{
			isPaused = false;
			Cursor.Hide();
		}
		public void Restart()
		{
			game.NextState(new GameState(players.Count, gameMode.GameModeTag, game));
		}
		public override void Draw(Graphics g)
		{
			if (!isGameOver)
				gameMode.Draw(g);

			for (int i = 0; i < components.Count; i++)
				components[i].Draw(g);
			for (int i = 0; i < eBombs.Count; i++)
				eBombs[i].Draw(g);
			for (int i = 0; i < pBombs.Count; i++)
				pBombs[i].Draw(g);
			for (int i = 0; i < players.Count; i++)
				players[i].Draw(g);
			string scoreText = "Score: " + score.ToString();
			g.DrawString(scoreText, new Font("Times New Roman", 12), new SolidBrush(Config.Instance.SystemColor), scoreX, scoreY);
			g.DrawString(FormatTime(), new Font("Times New Roman", 12), new SolidBrush(Config.Instance.SystemColor), playTimeX, playTimeY);
		}
		public override void Update(long gameTime)
		{
			if (!isGameOver)
			{
				HotKeys();
				if (!isPaused)
				{
					gameMode.Update(gameTime);
					score += gameMode.ReceivePoints();
					SpawnEnemies();

					for (int i = 0; i < eBombs.Count; i++)
					{
						for (int j = 0; j < components.Count; j++)
							components[j].Collider.CollisionDetection(eBombs[i].Collider);
						for (int j = 0; j < pBombs.Count; j++)
							pBombs[j].Collider.CollisionDetection(eBombs[i].Collider);
					}

					int aliveCount = 0;
					for (int i = 0; i < components.Count; i++)
					{
						components[i].Update(gameTime);
						if (components[i] is City)
							if (components[i].Alive)
								aliveCount++;
					}
					for (int i = 0; i < eBombs.Count; i++)
						eBombs[i].Update(gameTime);
					for (int i = 0; i < pBombs.Count; i++)
						pBombs[i].Update(gameTime);
					for (int i = 0; i < players.Count; i++)
						players[i].Update(gameTime);

					if (gameTime >= elapsedTime + 1000)
					{
						// if here, then its been 1 second.
						elapsedTime = gameTime;
						playTime += 1;
					}
					if (aliveCount == 0)
						GameOver();
				}
			}
		}
		public override void PostUpdate(long gameTime)
		{
			if (!isGameOver)
			{
				if (!isPaused)
				{
					gameMode.PostUpdate(gameTime);
					for (int i = 0; i < components.Count; i++)
						components[i].PostUpdate(gameTime);
					for (int i = 0; i < eBombs.Count; i++)
						eBombs[i].PostUpdate(gameTime);
					for (int i = 0; i < pBombs.Count; i++)
						pBombs[i].PostUpdate(gameTime);
					for (int i = 0; i < players.Count; i++)
						players[i].PostUpdate(gameTime);
				}
			}
		}
	}
}
