using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace missile_command
{
	[Serializable]
	public class PlayerConfiguration
	{
		// TODO maybe move to a data serialize.
		[XmlIgnore] private static string dir = "./config";
		[XmlIgnore] public bool MouseEnabled { get; set; }
		[XmlIgnore] public Color PColor { get; set; }
		[XmlIgnore] public string PCursor { get; set; }
		[XmlIgnore] public Dictionary<KPress, Keys> PKeys;
		[XmlIgnore] public ETag Tag { get; private set; }

		private XElement configurations;

		// Loads a playerconfiguration from storage
		public static PlayerConfiguration Load(ETag t)
		{
			PlayerConfiguration pc;
			string pdir = dir + "/" + t.ToString().ToLower();

			if (File.Exists(pdir))
			{
				pc = new PlayerConfiguration();
				pc.configurations = XElement.Load(pdir);
				pc.PCursor = pc.configurations.Element("Cursor").Value;
				pc.PColor = Color.FromArgb(Convert.ToInt32(pc.configurations.Element("Color").Value));

				pc.PKeys = new Dictionary<KPress, Keys>();
				foreach (XElement elm in pc.configurations.Element("PKeys").Elements())
				{
					Enum.TryParse(elm.Attribute("KPress").Value, out KPress kPress);
					Enum.TryParse(elm.Value, out Keys key);

					pc.PKeys.Add(kPress, key);
				}

				pc.MouseEnabled = (pc.configurations.Element("MouseEnabled").Value == "true");
				pc.Tag = (ETag)Convert.ToInt32(pc.configurations.Element("Tag").Value);
			}
			else
			{
				// Couldn't load. 
				MessageBox.Show("Could not load " + t.ToString().ToLower() + "'s configuration... generating to defaults, please change in config");
				Directory.CreateDirectory(dir);

				pc = new PlayerConfiguration
				{
					MouseEnabled = false,
					PColor = Color.FromArgb(128, 255, 128),
					PCursor = "cursor_09",
					Tag = t,
					PKeys = new Dictionary<KPress, Keys>
					{
						{ KPress.UP, Keys.W },
						{ KPress.RIGHT, Keys.D },
						{ KPress.DOWN, Keys.S },
						{ KPress.LEFT, Keys.A },
						{ KPress.SHOOT, Keys.Space }
					}
				};

				pc.Save();
			}

			return pc;
		}
		public void Save()
		{
			string pdir = dir + "/" + this.Tag.ToString().ToLower();
			bool append = true;
			if (File.Exists(pdir))
				append = false;
			configurations = new XElement("Configuration");

			// Save the player's cursor string and color
			configurations.Add(new XElement("Color", PColor.ToArgb().ToString()));
			configurations.Add(new XElement("Cursor", PCursor));

			// Save the player's keys
			XElement pElement = new XElement("PKeys");
			configurations.Add(pElement);
			foreach (var pair in PKeys)
			{
				XElement tElement = new XElement("Keys", pair.Value);
				tElement.SetAttributeValue("KPress", pair.Key);
				pElement.Add(tElement);
			}

			// Save configuration of using mouse or keyboard.
			configurations.Add(new XElement("MouseEnabled", MouseEnabled));
			configurations.Add(new XElement("Tag", (int)Tag));

			configurations.Save(pdir);
		}
	}
}
