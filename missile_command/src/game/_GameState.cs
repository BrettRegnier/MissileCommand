using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	// TODO refactor alllll of this, and use components instead.
	class GameState : State
	{
		private GameMode mode;

		private List<Player> players;
		private List<Entity> components;
		private List<Entity> eBombs;
		private List<Entity> pBombs;
		private Random rand = new Random();

		public GameState(int numPlayers, GameModes gamemode, Window g) : base(g)
		{
			players = new List<Player>();
			components = new List<Entity>();
			eBombs = new List<Entity>();
			pBombs = new List<Entity>();

			if (gamemode == GameModes.SURVIVAL)
				mode = new WaveMode();
			else if (gamemode == GameModes.WAVE)
				mode = new SurvivalMode();

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
		}
		private void InitPlayers(int numPlayers)
		{
			for (int i = 0; i < numPlayers; i++)
			{
				Point ori = new Point((Consts.gameBounds.Width / 3) * (i + 1), 200);
				PType pt = PType.PLAYER;
				ETag ap = ETag.P1;
				if (i == 1)
					ap = ETag.P2;
				else if (i == 2)
					ap = ETag.P3;

				Player p = new Player(pt, ap);
				players.Add(p);
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

				Size size = Config.Instance.TurretSize;
				Turret t = EntityFactory.MakeTurret(components[i + 1].Body.TopCenter, size, PType.PLAYER, currentPlayer.GetTag());
				t.TurretShoot += P_TurretShoot;
				components.Add(t);
				currentPlayer.AttachTurret(t);
			}

			KeypressHandler.Instance.Initialize(players);
		}
		private void P_TurretShoot(Point origin, Point destination, ETag a)
		{
			// use player upgrades, if I add them, to determine the size.
			Bomb bmb = EntityFactory.MakeBomb(origin, destination, PType.PLAYER, a);
			bmb.DestroyBomb += DestroyPBomb;
			pBombs.Add(bmb);
		}
		private void SpawnEnemies(long gameTime)
		{
			// TODO complex calculation based on ticks to determine when the next spawn is.
			if (Environment.TickCount >= gameTime + 1000)
			{
				Point spawnPoint = new Point(rand.Next(0, Consts.gameBounds.Width), 0);
				// TODO make a list of guaranteed points
				Point destination = new Point(rand.Next(0, Consts.gameBounds.Width), Consts.gameBounds.Height);
				Bomb bmb = EntityFactory.MakeBomb(spawnPoint, destination, PType.ENEMY, missile_command.ETag.ENEMY);
				bmb.DestroyBomb += DestroyEBomb;
				eBombs.Add(bmb);
			}
		}
		private void SpawnTest()
		{
		 Point spawnPoint = new Point(rand.Next(0, Consts.gameBounds.Width), 0);
			//Point destination = new Point(Utils.gameBounds.Width / 2, Utils.gameBounds.Height);
			Point destination = new Point(spawnPoint.X, Consts.gameBounds.Height);
			//destination.X -= 2;
			Bomb bmb = EntityFactory.MakeBomb(spawnPoint, destination, PType.ENEMY, missile_command.ETag.ENEMY);
			bmb.DestroyBomb += DestroyEBomb;
			eBombs.Add(bmb);
		}
		private void MassTest()
		{
			for (int i = 0; i < 100; i++)
				SpawnTest();
		}
		private void DestroyEBomb(Entity gameObject) { eBombs.Remove(gameObject); }
		private void DestroyPBomb(Entity gameObject) { pBombs.Remove(gameObject); }
		private void HotKeys()
		{
			System.Windows.Forms.Keys key = KeypressHandler.Instance.CurrentKey;
			if (key == System.Windows.Forms.Keys.H)
				SpawnTest();
			else if (key == System.Windows.Forms.Keys.J)
				MassTest();
			else if (key == System.Windows.Forms.Keys.Escape)
				game.Close();
		}

		public override void Draw(Graphics g)
		{
			// TODO uncomment
			//SpawnEnemies();
			
			for (int i = 0; i < components.Count; i++)
				components[i].Draw(g);
			for (int i = 0; i < eBombs.Count; i++)
				eBombs[i].Draw(g);
			for (int i = 0; i < pBombs.Count; i++)
				pBombs[i].Draw(g);
			for (int i = 0; i < players.Count; i++)
				players[i].Draw(g);
		}
		public override void Update(long gameTime)
		{
			HotKeys();

			for (int i = 0; i < eBombs.Count; i++)
			{
				for (int j = 0; j < components.Count; j++)
					components[j].Collider.CollisionDetection(eBombs[i].Collider);
				for (int j = 0; j < pBombs.Count; j++)
					pBombs[j].Collider.CollisionDetection(eBombs[i].Collider);
			}

			for (int i = 0; i < components.Count; i++)
				components[i].Update(gameTime);
			for (int i = 0; i < eBombs.Count; i++)
				eBombs[i].Update(gameTime);
			for (int i = 0; i < pBombs.Count; i++)
				pBombs[i].Update(gameTime);
			for (int i = 0; i < players.Count; i++)
				players[i].Update(gameTime);
		}
		public override void PostUpdate(long gameTime)
		{
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
