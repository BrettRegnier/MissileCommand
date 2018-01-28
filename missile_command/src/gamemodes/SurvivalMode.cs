using System;
using System.Collections.Generic;
using System.Drawing;

namespace missile_command
{
	class SurvivalMode : Mode
	{
		private Random rand;
		private int spawnAmount;

		private float speedMod;
		private int spawnNum;

		private bool spawning;
		private float spawnWait;

		private float spawnTime;
		private float speedTime;
		private float unitTime;

		public SurvivalMode(PlayState s) : base(s)
		{
			elapsedTime = 0;
			multiplier = 1.0;
			pendingPoints = 0;
			rand = new Random();
			spawnAmount = 1;
			spawnTime = 0.0f;

			GameModeTag = GameModes.SURVIVAL;
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
			if (spawning)
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

						Bomb b = EntityFactory.MakeBomb(oX, oY, new System.Drawing.Point(attackX, attackY), PType.ENEMY, ETag.ENEMY);
						b.Speed = 1.2f;
						bombs[i] = b;
					}
					spawning = false;
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
			// every 20 seconds increase the speed of the bombs
			// every 60 seconds incease the number of bombs being spawned 
			spawnTime += 1.0f / Window.fps;
			speedTime += 1.0f / Window.fps;
			unitTime += 1.0f / Window.fps;

			if (spawnTime > 2.0f )
			{
				spawning = true;
				spawnTime = 0.0f;
			}
			if (speedTime > 20.0f)
			{
				speedMod += 0.2f;
				speedTime = 0.0f;
			}
			if (unitTime > 60.0f)
			{
				spawnAmount++;
				unitTime = 0.0f;
			}
		}
		public override void PostUpdate(long gameTime)
		{
		}
	}
}
