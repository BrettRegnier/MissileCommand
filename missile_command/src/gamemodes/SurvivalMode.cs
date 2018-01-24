using System;
using System.Collections.Generic;
using System.Drawing;

namespace missile_command
{
	class SurvivalMode : Mode
	{
		private Random rand;
		private long spawnTime;
		private int spawnCount;
		private int spawnAmount;
		public SurvivalMode(GameState s) : base(s)
		{
			elapsedTime = 0;
			curBombs = new List<Bomb>();
			multiplier = 1.0;
			pendingPoints = 0;
			rand = new Random();
			spawnAmount = 1;
			spawnCount = 0;
		}
		public override void AddPoints(double points)
		{
			pendingPoints = Convert.ToInt32(points * multiplier);
		}
		public override int ReceivePoints()
		{
			int points = pendingPoints;
			pendingPoints = 0;
			return points;
		}
		public override Bomb[] SpawnEnemies()
		{
			// TODO setup a certain amount of time passed that increases as playtime increases
			if (spawnTime > 1000)
			{
				Bomb[] bombs = new Bomb[spawnAmount];
				//Points of Attack
				List<Entity> POA = state.AliveEntities();

				if (POA.Count > 0)
				{
					for (int i = 0; i < spawnAmount; i++)
					{
						Entity attackPoint = POA[rand.Next(0, POA.Count)];
						int attackX = attackPoint.Body.Center.X;
						int attackY = attackPoint.Body.Center.Y;
						int oX = rand.Next(0, Consts.gameBounds.Width / 2);
						int oY = 0;
						if (attackX > Consts.gameBounds.Width / 2)
							oX = rand.Next(Consts.gameBounds.Width / 2, Consts.gameBounds.Width);

						bombs[i] = EntityFactory.MakeBomb(oX, oY, new System.Drawing.Point(attackX, attackY), PType.ENEMY, ETag.ENEMY);
						spawnCount++;
					}
					spawnTime %= 1000;
					return bombs;
				}
			}

			return new Bomb[0];
		}
		public override void Draw(Graphics g)
		{
			//throw new NotImplementedException();
		}
		public override void Update(long gameTime)
		{
			if (elapsedTime == 0)
				elapsedTime = gameTime;
			// Keep track fo the milliseconds that have elasped
			if (gameTime >= elapsedTime)
			{
				spawnTime += gameTime - elapsedTime;
				elapsedTime = gameTime;
			}
		}
		public override void PostUpdate(long gameTime)
		{
			//throw new NotImplementedException();
		}
	}
}
