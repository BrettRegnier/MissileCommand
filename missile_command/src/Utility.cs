﻿namespace missile_command
{
	public enum GameModes
	{
		SURVIVAL,
		WAVE
	}
	public enum PType
	{
		ENEMY = 0,
		PLAYER = 1
	};
	public enum ETag
	{
		SYSTEM,
		PLAYER1,
		PLAYER2,
		PLAYER3,
		ENEMY
	}
	public enum Direction
	{
		NONE = 0,
		UP,
		RIGHT,
		DOWN,
		LEFT
	};
	public enum KPress
	{
		NONE = 0,
		UP = 1,
		RIGHT = 2,
		DOWN = 4,
		LEFT = 8,
		SHOOT = 16
	};
	public static class Consts
	{
		// landmass + 100 + hill height
		public const int STAGE_BOUND_HEIGHT = LAND_MASS_HEIGHT + HILL_MASS_HEIGHT + 100;
		public const int LAND_MASS_HEIGHT = 50;
		public const int HILL_MASS_WIDTH = 100;
		public const int HILL_MASS_HEIGHT = 70;
		public const int SCREEN_OFFSET = 0;
		public const int SEPERATION_VALUE = 34;
		public const int POINT_VALUE = 50;

		// the dimensions of this image is being drawn as 40x40
		public const int CITY_SIZE = 30;
		public const int CITY_TRUE_OFFSET = 10;
		// stupid windows drawing
		public const int CITY_TRUE_SIZE = 40;
		public const int DESTROYED_CITY_SIZE_OFFSET = 15;

		public static System.Drawing.Size gameBounds = new System.Drawing.Size(
			System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - Consts.SCREEN_OFFSET,
			System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - Consts.SCREEN_OFFSET);

		public static int[] HILL_POSITIONS_X = { 0, (gameBounds.Width / 2) - (HILL_MASS_WIDTH / 2), gameBounds.Width - HILL_MASS_WIDTH };

		public static int[] CITY_POSITIONS_X = {
			HILL_MASS_WIDTH + CITY_SIZE * 3,
			((HILL_MASS_WIDTH + CITY_SIZE * 3) + (HILL_POSITIONS_X[1] - CITY_SIZE * 4 - 7)) / 2,
			HILL_POSITIONS_X[1] - CITY_SIZE * 4 - 7,
			HILL_POSITIONS_X[1] + HILL_MASS_WIDTH + CITY_SIZE * 3,
			((HILL_POSITIONS_X[1] + HILL_MASS_WIDTH + CITY_SIZE * 3) + (HILL_POSITIONS_X[2] - CITY_SIZE * 4 - 7)) / 2,
			HILL_POSITIONS_X[2] - CITY_SIZE * 4 - 7,
		};
	}
}
