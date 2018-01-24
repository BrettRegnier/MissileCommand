using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace missile_command
{
	// Singleton class.
	[Serializable]
	public class Config
	{
		[XmlIgnore()] private static Config instance;
		[XmlIgnore()] private Dictionary<ETag, PlayerConfiguration> dicPConfigs;
		[XmlIgnore()] private static string dir = "./config";
		[XmlIgnore()] public Color SystemColor { get { return Color.FromArgb(Convert.ToInt32(SysColor)); } }

		public int EBombDiameter { get; set; }
		public int PBombDiameter { get; set; }
		public float EBombSpeed { get; set; }
		public float PBombSpeed { get; set; }
		public int ExplosionDiameter { get; set; }
		public int TurretDiameter { get; set; }
		public string SysColor { get; set; }

		public static Config Instance
		{
			get
			{
				if (instance == null)
					instance = Config.Load();

				return instance;
			}
		}
		public Config()
		{
			dicPConfigs = new Dictionary<ETag, PlayerConfiguration>();
		}
		public Color GetPlayerColor(ETag t)
		{
			if (t == ETag.ENEMY)
				return Color.White;
			else if (t == ETag.SYSTEM)
				return Color.White;

			return LoadPlayer(t).PColor;
		}
		public Dictionary<KPress, Keys> GetPlayerKeys(ETag t)
		{
			return LoadPlayer(t).PKeys;
		}
		public Bitmap GetPlayerCursor(ETag t)
		{
			return (Bitmap)Properties.Resources.ResourceManager.GetObject(LoadPlayer(t).PCursor);
		}
		public bool GetMouseCheck(ETag t)
		{
			return LoadPlayer(t).MouseEnabled;
		}

		public static Config Load()
		{
			Config c;
			string cdir = dir + "/configuration";

			if (File.Exists(cdir))
			{
				StreamReader r = new StreamReader(cdir);
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(Config));
				c = (Config)xmlSerializer.Deserialize(r);
				r.Close();
			}
			else
			{
				MessageBox.Show("Configurations not found, generationg defaults");
				Directory.CreateDirectory(dir);

				c = new Config
				{
					EBombDiameter = 4,
					EBombSpeed = 1f,
					PBombDiameter = 4,
					PBombSpeed = 15f,
					ExplosionDiameter = 100,
					TurretDiameter = 50,
					SysColor = "-8323200"
				};

				c.Save();
				MessageBox.Show("Configurations generated");
			}

			return c;
		}
		public void Save()
		{
			string cdir = dir + "/configuration";
			bool append = true;

			if (File.Exists(cdir))
				append = false;

			XmlSerializer xmlSerializer = new XmlSerializer(typeof(Config));
			StreamWriter myWriter = new StreamWriter(cdir, append);
			xmlSerializer.Serialize(myWriter, this);
			myWriter.Close();

			foreach (PlayerConfiguration pc in dicPConfigs.Values)
				pc.Save();
		}
		public PlayerConfiguration LoadPlayer(ETag t, bool force = false)
		{
			// If the pc was not loaded
			if (!dicPConfigs.ContainsKey(t))
				dicPConfigs.Add(t, PlayerConfiguration.Load(t));
			if (force)
				dicPConfigs[t] = PlayerConfiguration.Load(t);

			return dicPConfigs[t];
		}
	}
}

