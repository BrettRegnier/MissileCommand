using System;
using System.Collections.Generic;
using System.Drawing;

namespace missile_command
{
	class WaveMode : Mode
	{
		private int wave;
		private List<Bomb> spawnBombs;
		private double wait;
		private bool countingDown;
		private bool begin;
		private int countdown;
		private StringFormat sf;

		private float speedMod;
		private int spawnMod;
		private int spawnNum;
		private int spawnMultiplier;

		private bool spawning;
		private int spawnCount;
		private float spawnWait;

		private Random rand;

		public WaveMode(PlayState s) : base(s)
		{
			sf = new StringFormat
			{
				LineAlignment = StringAlignment.Center,
				Alignment = StringAlignment.Center
			};

			spawnBombs = new List<Bomb>();
			multiplier = 0.1;
			pendingPoints = 0;
			begin = false;
			countingDown = false;
			spawning = false;
			countdown = 3;

			spawnMod = 1;
			speedMod = 0.2f;
			spawnNum = 0;
			spawnMultiplier = 1;

			spawnCount = 0;
			spawning = false;
			spawnWait = 0;

			wait = 0;
			rand = new Random();

			GameModeTag = GameModes.WAVE;
		}
		public override void AddPoints(double points)
		{
			pendingPoints = Convert.ToInt32(points * multiplier * wave);
		}
		public override int ReceivePoints()
		{
			int points = pendingPoints;
			pendingPoints = 0;
			return points;
		}
		public override Bomb[] SpawnEnemies()
		{
			Bomb[] b = spawnBombs.ToArray();
			spawnBombs = new List<Bomb>();
			return b;
		}
		private void Spawn()
		{
			//Points of Attack
			List<Entity> POA = state.AliveEntities();

			if (POA.Count > 0)
			{
				Entity attackPoint = POA[rand.Next(0, POA.Count)];
				int attackX = attackPoint.Body.Center.X;
				int attackY = attackPoint.Body.Center.Y;
				int oX = rand.Next(0, Consts.gameBounds.Width / 2);
				int oY = 0;
				if (attackX > Consts.gameBounds.Width / 2)
					oX = rand.Next(Consts.gameBounds.Width / 2, Consts.gameBounds.Width);

				Bomb b = EntityFactory.MakeBomb(oX, oY, new System.Drawing.Point(attackX, attackY), PType.ENEMY, ETag.ENEMY);
				spawnBombs.Add(b);
			}
		}
		private void ModifyWave()
		{
			// Every 1 waves adds 1 bomb,
			// Every 5 waves increases speed by a small amount
			// Every 10 waves increases amount of bombs by 1
			if (wave % 2 == 0)
				speedMod += 0.2f;
			if (wave % 5 == 0)
				spawnMod += 1;
			if (wave % 10 == 0)
				spawnMultiplier += 1;

			spawnNum += spawnMod;
			spawning = true;
		}
		private void NextWave()
		{
			wave++;
			wait = 3.0;
			countingDown = true;
			begin = true;
			spawnWait = 1.0f;
			spawnCount = 0;
		}
		public override void Draw(Graphics g)
		{
			if (countingDown)
			{
				g.DrawString("Wave " + wave.ToString(), Config.Instance.TitleFont, new SolidBrush(Config.Instance.SystemColor), new Point(Consts.gameBounds.Width / 2, 200), sf);
				g.DrawString(countdown.ToString(), new Font(Config.Instance.Typeface, 16), new SolidBrush(Config.Instance.SystemColor), new Point(Consts.gameBounds.Width / 2, 300), sf);
			}
		}
		public override void Update(long gameTime)
		{
			if (state.RemainingBombs == 0 && begin == false && spawning == false)
			{
				NextWave();
			}

			if (wait > 0 && countingDown)
			{
				wait -= (1.0) / ((double)Window.fps);
				countdown = Convert.ToInt32(wait);
			}
			else if (countingDown)
			{
				wait = 0;
				countdown = Convert.ToInt32(wait);
				countingDown = false;
				begin = false;
				ModifyWave();
			}

			if (spawning)
			{
				spawnWait -= (1.0f) / ((float)Window.fps);
				if (spawnWait <= 0)
				{
					spawnWait = 1.0f;
					if (spawnCount < spawnNum)
					{
						for (int i = 0; i < spawnMultiplier; i++)
						{
							spawnCount++;
							Spawn();
						}
					}
					else
					{
						spawning = false;
					}
				}
			}
		}
		public override void PostUpdate(long gameTime)
		{

		}
	}
}
