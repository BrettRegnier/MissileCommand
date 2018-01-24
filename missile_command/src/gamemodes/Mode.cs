namespace missile_command
{
	abstract class Mode
	{
		protected System.Collections.Generic.List<Bomb> curBombs;
		protected long elapsedTime;
		protected double multiplier;
		protected int pendingPoints;
		protected GameState state;
		public GameModes GameModeTag { get; protected set; }
		public Mode(GameState s) { state = s; }
		public abstract void AddPoints(double points);
		public abstract int ReceivePoints();
		public abstract Bomb[] SpawnEnemies();
		public abstract void Draw(System.Drawing.Graphics g);
		public abstract void Update(long gameTime);
		public abstract void PostUpdate(long gameTime);
	}
}
