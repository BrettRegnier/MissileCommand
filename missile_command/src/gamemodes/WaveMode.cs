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

		public WaveMode(PlayState s) : base(s)
		{
			spawnBombs = new List<Bomb>();
			multiplier = 0.1;
			pendingPoints = 0;
			begin = false;
			countingDown = false;
			countdown = 3;

			wait = 0;

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
			return new Bomb[0];
		}
		private void BeginWave()
		{

		}
		private void NextWave()
		{
			wait = 4;
			countdown = 3;
			countingDown = true;
		}
		public override void Draw(Graphics g)
		{

		}
		public override void Update(long gameTime)
		{
			if (state.RemainingBombs == 0)
				NextWave();

			if (wait > 0 && countingDown)
			{
				wait -= (1 / 3) / (Window.fps);
				countdown = Convert.ToInt32(wait);
			}
			else if (countingDown)
			{
				wait = 0;
				countingDown = false;
				BeginWave();
			}
		}
		public override void PostUpdate(long gameTime)
		{

		}
	}
}
