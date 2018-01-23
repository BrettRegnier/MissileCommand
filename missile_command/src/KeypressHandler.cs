using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace missile_command
{
	class KeypressHandler
	{
		private static KeypressHandler instance;
		
		private List<KPress> lPress = new List<KPress>();
		private Dictionary<ETag, Dictionary<KPress, Keys>> dlPKeys = new Dictionary<ETag, Dictionary<KPress, Keys>>();
		private Dictionary<ETag, KPress> dPKPress = new Dictionary<ETag, KPress>();
		private Keys currentKey;
		private Keys prevKey;

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
			// Load config of keypresses
			foreach (Player p in ps)
			{
				if (!dlPKeys.ContainsKey(p.GetTag()))
				{
					// Load here what their designated keypress is into a dictionary.
					dlPKeys.Add(p.GetTag(), Config.Instance.GetPlayerKeys(p.GetTag()));
					dPKPress.Add(p.GetTag(), KPress.NONE);
				}
			}
		}
		public void KeyDown(KeyEventArgs e)
		{
			currentKey = e.KeyData;
			foreach (KeyValuePair<ETag, Dictionary<KPress, Keys>> PDKPressKeys in dlPKeys)
				foreach (KeyValuePair<KPress, Keys> KPressKeys in PDKPressKeys.Value)
					if (e.KeyData == KPressKeys.Value)
						dPKPress[PDKPressKeys.Key] |= KPressKeys.Key;
		}
		public void KeyUp(KeyEventArgs e)
		{
			prevKey = currentKey;
			currentKey = Keys.None;
			foreach (KeyValuePair<ETag, Dictionary<KPress, Keys>> PDKPressKeys in dlPKeys)
				foreach (KeyValuePair<KPress, Keys> KPressKeys in PDKPressKeys.Value)
					if (e.KeyData == KPressKeys.Value)
						dPKPress[PDKPressKeys.Key] &= ~KPressKeys.Key;
		}
		public KPress PlayerKeyState(ETag t)
		{
			return dPKPress[t];
		}

		public Keys CurrentKey { get { return currentKey; } }
		public bool FullPress(Keys key)
		{
			if (currentKey == Keys.None && prevKey == key)
			{
				prevKey = Keys.None;
				return true;
			}
			else
				return false;
		}
	}
}
