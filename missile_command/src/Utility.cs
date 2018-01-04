using System.Drawing;

namespace missile_command
{
	public enum PType
	{
		ENEMY = 0,
		PLAYER = 1
	};
	public enum ETag
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
	public static class Utils
	{
		//TODO hill sizes based on the screen
		// landmass + 100 + hill height
		public const int RETICLE_BOUNDS_OFFSET = LAND_MASS_HEIGHT + HILL_MASS_HEIGHT + 100;
		public const int LAND_MASS_HEIGHT = 50;
		public const int HILL_MASS_WIDTH = 100;
		public const int HILL_MASS_HEIGHT = 50;
		public const int SCREEN_OFFSET = 0;
		public const int CITY_SIZE = 80;

		public static System.Drawing.Size gameBounds = new System.Drawing.Size(
			System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - Utils.SCREEN_OFFSET,
			System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - Utils.SCREEN_OFFSET);
		public static int[] HILL_POSITIONS_X = { 0, (gameBounds.Width / 2) - (HILL_MASS_WIDTH / 2), gameBounds.Width - HILL_MASS_WIDTH };

		// TODO test this
		public static int[] CITY_POSITIONS_X = {
			HILL_MASS_WIDTH + CITY_SIZE /2,
			HILL_MASS_WIDTH + HILL_POSITIONS_X[1] - CITY_SIZE/2,
			HILL_POSITIONS_X[1] - CITY_SIZE/2,
			HILL_MASS_WIDTH + HILL_POSITIONS_X[1] + CITY_SIZE/2,
			HILL_POSITIONS_X[1] + gameBounds.Width + CITY_SIZE/2,
			HILL_POSITIONS_X[2] - CITY_SIZE
		};


		//HILL_MASS_WIDTH + HILL_POSITIONS_X[1] + gameBounds.Width - HILL_MASS_WIDTH + CITY_SIZE/2, original 5 index spot
	}
}
