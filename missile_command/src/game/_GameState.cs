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

		private List<Player> lPlayer;
		private List<List<Entity>> lEntities;
		private Random rand = new Random();
		private GameMode mode;

		public GameState(int numPlayers, GameModes gamemode, Window g) : base(g)
		{
			// TODO switch to using a component list.
			lEntities = new List<List<Entity>>();
			lPlayer = new List<Player>();

			if (gamemode == GameModes.SURVIVAL)
				mode = new GameMode();
			else if (gamemode == GameModes.WAVE)
				mode = new GameMode();

			InitGame();
			InitPlayers(numPlayers);
		}
		private void InitGame()
		{
			//lEntities = new List<List<Entity>>();
			// 5 Types of entites based on accounts enum
			for (int i = 0; i < 5; i++)
				lEntities.Add(new List<Entity>());

			// Create landmasses
			Point baseLand = new Point(0, Consts.gameBounds.Height);
			Size baseSize = new Size(Consts.gameBounds.Width, Consts.LAND_MASS_HEIGHT);
			LandMass lm = new LandMass(baseLand.X, baseLand.Y, baseSize.Width, baseSize.Height);
			lEntities[(int)ETag.SYSTEM].Add(lm);

			for (int i = 0; i < 3; i++)
			{
				Point p = new Point(Consts.HILL_POSITIONS_X[i], lm.Body.Top);
				lEntities[(int)ETag.SYSTEM].Add(new LandMass(p.X, p.Y, Consts.HILL_MASS_WIDTH, Consts.HILL_MASS_HEIGHT));
			}

			// Build Cities
			for (int i = 0; i < 6; i++)
			{
				int w_h = Consts.CITY_SIZE;
				int x = Consts.CITY_POSITIONS_X[i];
				int y = Consts.gameBounds.Height - (Consts.LAND_MASS_HEIGHT + w_h);
				City c = new City(x, y, w_h, w_h);
				lEntities[(int)ETag.SYSTEM].Add(c);
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
				lPlayer.Add(p);
			}

			Player currentPlayer = lPlayer[0];
			for (int i = 0; i < 3; i++)
			{
				if (numPlayers == 2)
				{
					if (i == 1)
						currentPlayer = lPlayer[i];
				}
				else if (numPlayers == 3)
					currentPlayer = lPlayer[i];

				Size size = Config.Instance.TurretSize;
				Turret t = EntityFactory.MakeTurret(lEntities[(int)ETag.SYSTEM][i + 1].Body.TopCenter, size, PType.PLAYER, currentPlayer.GetTag());
				t.TurretShoot += P_TurretShoot;
				lEntities[(int)currentPlayer.GetTag()].Add(t);
				currentPlayer.AttachTurret(t);
			}

			KeypressHandler.Instance.Initialize(lPlayer);
		}
		private void P_TurretShoot(Point origin, Point destination, ETag a)
		{
			// use player upgrades, if I add them, to determine the size.
			Bomb bmb = EntityFactory.MakeBomb(origin, destination, PType.PLAYER, a);
			bmb.DestroyBomb += DestroyGameObject;
			lEntities[(int)bmb.Tag].Add(bmb);
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
				bmb.DestroyBomb += DestroyGameObject;
				lEntities[(int)bmb.Tag].Add(bmb);
			}
		}
		private void SpawnTest()
		{
			Point spawnPoint = new Point(rand.Next(0, Consts.gameBounds.Width), 0);
			//Point destination = new Point(Utils.gameBounds.Width / 2, Utils.gameBounds.Height);
			Point destination = new Point(spawnPoint.X, Consts.gameBounds.Height);
			Bomb bmb = EntityFactory.MakeBomb(spawnPoint, destination, PType.ENEMY, missile_command.ETag.ENEMY);
			bmb.DestroyBomb += DestroyGameObject;
			lEntities[(int)bmb.Tag].Add(bmb);
		}
		private void MassTest()
		{
			for (int i = 0; i < 100; i++)
				SpawnTest();
		}
		private void DestroyGameObject(Entity gameObject)
		{
			lEntities[(int)gameObject.Tag].Remove(gameObject);
		}
		public override void Draw(Graphics g)
		{
			// TODO uncomment
			//SpawnEnemies();

			for (int i = 0; i < lPlayer.Count; i++)
				lPlayer[i].Draw(g);

			for (int i = 0; i < lEntities.Count; i++)
				for (int j = 0; j < lEntities[i].Count; j++)
					lEntities[i][j].Draw(g);
		}
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
		public override void Update(long gameTime)
		{
			HotKeys();

			// :O almost n^3
			// minus 1 because I don't want to check enemy bombs twice.
			// TODO find a better solution
			for (int i = 0; i < lEntities.Count - 1; i++)
			{
				for (int j = 0; j < lEntities[(int)ETag.ENEMY].Count; j++)
				{
					for (int k = 0; k < lEntities[i].Count; k++)
					{
						lEntities[(int)ETag.ENEMY][j].Collider.CollisionDetection(lEntities[i][k].Collider);
					}
				}
			}

			for (int i = 0; i < lEntities.Count; i++)
			{
				for (int j = 0; j < lEntities[i].Count; j++)
				{
					lEntities[i][j].Update(gameTime);
				}
			}

			foreach (Player p in lPlayer)
				p.Update(gameTime);
		}
		public override void PostUpdate(long gameTime)
		{
			for (int i = 0; i < lEntities.Count; i++)
			{
				for (int j = 0; j < lEntities[i].Count; j++)
				{
					lEntities[i][j].PostUpdate(gameTime);
				}
			}

			foreach (Player p in lPlayer)
				p.PostUpdate(gameTime);
		}
	}
}
