using System;
using System.Collections.Generic;
using System.Text;

namespace missile_command
{
	public enum PType
	{
		ENEMY = 0,
		PLAYER1 = 1,
		PLAYER2 = 2,
		PLAYER3 = 3,
		PLAYER4 = 4,
		SYSTEM = 5
	};
	public enum ObjectType
	{
		BOMB = 0,
		PLAYER = 1,
		NPO
	};
	public enum ListType
	{
		E_BOMB,
		P_BOMB,
		PLAYER
	};
	public enum Direction
	{
		UP = 0,
		RIGHT = 1,
		DOWN = 2,
		LEFT = 3
	};
	public class Dimension
	{
		int Width { get; set; }
		int Height { get; set; }

		public Dimension(int w, int h)
		{
			Width = w;
			Height = h;
		}
	}
	public static class Utils
	{
		public const int SCREEN_OFFSET = 5;
		public const int GAME_BOUND_OFFSET = 100;
	}
}
