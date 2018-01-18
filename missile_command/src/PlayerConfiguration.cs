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


// TODO implement IXmlSerializable myself.
// https://stackoverflow.com/questions/802711/serializing-private-member-data
// https://www.codeproject.com/Articles/43237/How-to-Implement-IXmlSerializable-Correctly
namespace missile_command
{
	[Serializable()]
	public class PlayerConfiguration
	{
		// TODO maybe move to a data serialize.
		[XmlIgnore] private static string dir = "./config";
		[XmlIgnore] public Dictionary<KPress, Keys> PKeys;
		[XmlIgnore] public ETag Tag { get; private set; }
		[XmlIgnore] public Color PColor { get; set; }
		[XmlIgnore] public Bitmap PCursor { get; set; }
		[XmlIgnore] public bool MouseEnabled { get; set; }

		public XElement el;

		// Loads a playerconfiguration from storage
		public static PlayerConfiguration Load(ETag t)
		{
			PlayerConfiguration pc;
			string pdir = dir + "/" + t.ToString().ToLower();

			if (File.Exists(pdir))
			{
				StreamReader r = new StreamReader(pdir);
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(PlayerConfiguration));
				pc = (PlayerConfiguration)xmlSerializer.Deserialize(r);
				pc.Tag = t;
				r.Close();

				foreach (XElement elm in pc.el.Elements())
				{
					int x = 1;
				}

			}
			else
			{
				// Couldn't load. 
				MessageBox.Show("Could not load " + t.ToString().ToLower() + "'s configuration... generating to defaults, please change in config");
				Directory.CreateDirectory(dir);

				pc = new PlayerConfiguration
				{
					MouseEnabled = true,
					PColor = Color.LimeGreen,
					PCursor = Properties.Resources.cursor_00,
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
			string pdir = dir + "/" + this.Tag.ToString();
			bool append = true;
			if (File.Exists(pdir))
				append = false;

			el = new XElement("PKeys");
			foreach (var pair in PKeys)
			{
				XElement tElement = new XElement("Keys", pair.Value);
				tElement.SetAttributeValue("KPress", pair.Key);
				el.Add(tElement);
			}

			XmlSerializer xmlSerializer = new XmlSerializer(typeof(PlayerConfiguration));
			StreamWriter myWriter = new StreamWriter(pdir, append);
			xmlSerializer.Serialize(myWriter, this);
			myWriter.Close();
		}
	}
}
