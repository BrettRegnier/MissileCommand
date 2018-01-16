using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	abstract class GameMode
	{
		public abstract Bomb[] SpawnEnemies(long gameTime);
	}
	class WaveMode : GameMode
	{
		public override Bomb[] SpawnEnemies(long gameTime)
		{
			throw new NotImplementedException();
		}
	}
	class SurvivalMode : GameMode
	{
		public override Bomb[] SpawnEnemies(long gameTime)
		{
			throw new NotImplementedException();
		}
	}
}
