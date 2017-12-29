using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace missile_command
{
	// Singleton class.
	public class Config
	{
		private struct KeyConfig
		{
			Keys up;
			Keys down;
			Keys left;
			Keys right;
			Keys shoot;
		};

		private static Config instance;

		private List<Color> lPColor = new List<Color>();
		private List<List<Keys>> lPKeys = new List<List<Keys>>();
		
		// TODO figure out how to select the image for a cursor, maybe a string that will load it?
		// TODO Pretty much this whole class.
		// TODO decide on how many players... leaning towards 3
		public Config()
		{
			LoadColorConfig();
			LoadPlayerKeys();
		}
		public static Config Instance()
		{
			if (instance == null)
				instance = new Config();

			return instance;
		}
		public Color GetPlayerColor(PType p)
		{
			Color color = Color.Black;
			switch (p)
			{
				case (PType.ENEMY):
					color = Color.White;
					break;
				case (PType.PLAYER1):
					color = Color.Green;
					break;
				case (PType.PLAYER2):
					color = Color.Blue;
					break;
				case (PType.PLAYER3):
					color = Color.Yellow;
					break;
				case (PType.PLAYER4):
					color = Color.Red;
					break;
			}
			return color;
		}
		public List<Keys> GetPlayerKeySet()
		{
			throw new NotImplementedException();
		}
		private void LoadColorConfig()
		{

		}
		private void LoadPlayerKeys()
		{

		}
	}
}

