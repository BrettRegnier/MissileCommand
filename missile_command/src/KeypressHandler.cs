using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace missile_command
{
	// TODO switch to async states like mouse.
	class KeypressHandler
	{
		private static KeypressHandler instance;

		//TODO maybe could make into a struct.
		private List<KPress> lPress = new List<KPress>();
		private Dictionary<Player, Dictionary<KPress, Keys>> dlPKeys = new Dictionary<Player, Dictionary<KPress, Keys>>();
		private Dictionary<Player, KPress> dPKPress = new Dictionary<Player, KPress>();
		private Keys currentKey;

		public static KeypressHandler Instance
		{
			get
			{
				if (instance == null)
					instance = new KeypressHandler();
				return instance;
			}
		}
		public void Initialize(List<Player> ps)
		{
			// TODO replace with config
			//Dummy information
			Dictionary<KPress, Keys> lk = new Dictionary<KPress, Keys>();
			lk.Add(KPress.UP, Keys.W);
			lk.Add(KPress.RIGHT, Keys.D);
			lk.Add(KPress.DOWN, Keys.S);
			lk.Add(KPress.LEFT, Keys.A);
			lk.Add(KPress.SHOOT, Keys.Space);

			// Load config of keypresses
			foreach (Player p in ps)
			{
				// Load here what their designated keypress is into a dictionary.
				dlPKeys.Add(p, Config.Instance.GetPlayerKeys(p.GetTag()));
				dPKPress.Add(p, KPress.NONE);
			}
		}
		public void KeyDown(KeyEventArgs e)
		{
			currentKey = e.KeyData;
			foreach (KeyValuePair<Player, Dictionary<KPress, Keys>> PDKPressKeys in dlPKeys)
				foreach (KeyValuePair<KPress, Keys> KPressKeys in PDKPressKeys.Value)
					if (e.KeyData == KPressKeys.Value)
						dPKPress[PDKPressKeys.Key] |= KPressKeys.Key;
		}
		public void KeyUp(KeyEventArgs e)
		{
			currentKey = Keys.None;
			foreach (KeyValuePair<Player, Dictionary<KPress, Keys>> PDKPressKeys in dlPKeys)
				foreach (KeyValuePair<KPress, Keys> KPressKeys in PDKPressKeys.Value)
					if (e.KeyData == KPressKeys.Value)
						dPKPress[PDKPressKeys.Key] &= ~KPressKeys.Key;
		}
		public KPress PlayerKeyState(Player p)
		{
			return dPKPress[p];
		}

		public Keys CurrentKey { get { return currentKey; } }
	}
}
