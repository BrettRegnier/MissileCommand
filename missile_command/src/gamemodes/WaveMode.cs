using System;
using System.Collections.Generic;
using System.Drawing;

namespace missile_command
{
	class WaveMode : Mode
	{
		private int wave;

		public WaveMode(GameState s) : base(s)
		{
			curBombs = new List<Bomb>();
			multiplier = 0.1;
			pendingPoints = 0;
			wave = 1;

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
		public override void Draw(Graphics g)
		{

		}
		public override void Update(long gameTime)
		{

		}
		public override void PostUpdate(long gameTime)
		{

		}
	}
}
