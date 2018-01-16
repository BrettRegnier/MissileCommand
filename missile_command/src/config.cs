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

		private List<List<Keys>> lPKeys = new List<List<Keys>>();
		private List<Color> lAccountColors;
		// TODO Remove magic numbers
		private int eBombRadius = 4;
		private int pBombRadius = 4;
		private float eBombSpeed = 1f;
		private float pBombSpeed = 10f;
		private int explosionSize = 100;
		private int turretRadius = 50;

		public static Config Instance
		{
			get
			{
				if (instance == null)
					instance = new Config();

				return instance;
			}
		}

		// TODO figure out how to select the image for a cursor, maybe a string that will load it?
		// TODO Pretty much this whole class.
		// TODO decide on how many players... leaning towards 3
		public Config()
		{
			LoadBasicConfig();
			LoadColorConfig();
			LoadPlayerKeys();
		}
		public Color GetPlayerColor(ETag p)
		{
			int index = (int)p;
			return lAccountColors[index];
		}
		public List<Keys> GetPlayerKeySet()
		{
			throw new NotImplementedException();
		}
		
		public int EnemyBombDiameter { get { return eBombRadius; } }
		public int PlayerBombDiameter { get { return pBombRadius; } }
		public float EnemyBombSpeed { get { return eBombSpeed; } }
		public float PlayerBombSpeed { get { return pBombSpeed; } }
		public int DefaultExplosionSize { get { return explosionSize; } }
		public Size TurretSize { get { return new Size(turretRadius, turretRadius); } }
		public int TurretRadius { get { return turretRadius; } }

		private void LoadBasicConfig()
		{

		}
		private void LoadColorConfig()
		{
			// TODO load config
			lAccountColors = new List<Color>();
			lAccountColors.Add(Color.White);
			lAccountColors.Add(Color.LightGreen);
			lAccountColors.Add(Color.Blue);
			lAccountColors.Add(Color.Yellow);
			lAccountColors.Add(Color.White);
		}
		private void LoadPlayerKeys()
		{

		}
	}
}

