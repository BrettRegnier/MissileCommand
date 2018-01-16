using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	abstract class Mode
	{
		protected List<Bomb> curBombs;
		protected long elapsedTime;
		protected double multiplier;
		protected int pendingPoints;
		protected GameState state;
		public Mode(GameState s) { state = s; }
		public abstract void AddPoints(double points);
		public abstract int ReceivePoints();
		public abstract Bomb[] SpawnEnemies();
		public abstract void Update(long gameTime);
		public abstract void PostUpdate(long gameTime);
	}
}
