namespace missile_command
{
	public enum PType
	{
		ENEMY = 0,
		PLAYER = 1
	};
	public enum Account
	{
		ENEMY,
		P1,
		P2,
		P3,
		SYSTEM
	}
	public enum Direction
	{
		UP = 0,
		RIGHT = 1,
		DOWN = 2,
		LEFT = 3
	};
	public class Dimension
	{
		public int Width { get; set; }
		public int Height { get; set; }

		public Dimension(int w, int h)
		{
			Width = w;
			Height = h;
		}
	}
	public static class Utils
	{
		// landmass + 100
		public const int RETICLE_BOUNDS_OFFSET = 150;
		public const int LAND_MASS_SIZE = 50;
		public const int SCREEN_OFFSET = 5;
		public static Dimension gameBounds = new Dimension(
			System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - Utils.SCREEN_OFFSET,
			System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - Utils.SCREEN_OFFSET);
	}
}
