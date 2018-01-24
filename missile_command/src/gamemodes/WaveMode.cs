using System;
using System.Collections.Generic;

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
			throw new NotImplementedException();
		}

		public override void Update(long gameTime)
		{
			throw new NotImplementedException();
		}
		public override void PostUpdate(long gameTime)
		{
			throw new NotImplementedException();
		}
	}
}
