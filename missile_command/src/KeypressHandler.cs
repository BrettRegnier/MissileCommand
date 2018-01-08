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
		private enum KPressBits
		{
			NONE = 0,
			UP = 1,
			RIGHT = 2,
			DOWN = 4,
			LEFT = 8,
			SHOOT = 16
		};

		private static KeypressHandler instance;

		//TODO maybe could make into a struct.
		private List<bool> lIsKeyDown = new List<bool>();
		private List<KPressBits> lPress = new List<KPressBits>();
		private List<List<Keys>> lPKeys = new List<List<Keys>>();
		private List<Player> lPlayers;

		public static KeypressHandler Instance()
		{
			if (instance == null)
				instance = new KeypressHandler();

			return instance;
		}
		public void Initialize(List<Player> ps)
		{
			lPlayers = ps;

			// TODO replace with config
			//Dummy information
			List<Keys> lk = new List<Keys>();
			lk.Add(Keys.W);
			lk.Add(Keys.D);
			lk.Add(Keys.S);
			lk.Add(Keys.A);
			lk.Add(Keys.Space);

			// Load config of keypresses
			for (int i = 0; i < lPlayers.Count; i++)
			{
				// Load here what their designated keypress is into a list.
				lPKeys.Add(lk);
				lPress.Add(KPressBits.NONE);
				lIsKeyDown.Add(false);
			}
		}
		public void KeyDown(KeyEventArgs e)
		{
			for (int i = 0; i < lPKeys.Count; i++)
			{
				for (int j = 0; j < lPKeys[i].Count; j++)
				{
					if (e.KeyData == lPKeys[i][j])
					{
						// J is the key type pressed
						switch (j)
						{
							case 0:
								// UP
								lPress[i] |= KPressBits.UP;
								break;
							case 1:
								// RIGHT
								lPress[i] |= KPressBits.RIGHT;
								break;
							case 2:
								// DOWN
								lPress[i] |= KPressBits.DOWN;
								break;
							case 3:
								//LEFT
								lPress[i] |= KPressBits.LEFT;
								break;
							case 4:
								// SHOOT
								lPress[i] |= KPressBits.SHOOT;
								break;
							default:
								break;
						}
					}
				}
			}
		}
		public void KeyUp(KeyEventArgs e)
		{
			for (int i = 0; i < lPKeys.Count; i++)
			{
				for (int j = 0; j < lPKeys[i].Count; j++)
				{
					if (e.KeyData == lPKeys[i][j])
					{
						// J is the key type pressed
						switch (j)
						{
							case 0:
								// UP
								lPress[i] &= ~KPressBits.UP;
								break;
							case 1:
								// RIGHT
								lPress[i] &= ~KPressBits.RIGHT;
								break;
							case 2:
								// DOWN
								lPress[i] &= ~KPressBits.DOWN;
								break;
							case 3:
								//LEFT
								lPress[i] &= ~KPressBits.LEFT;
								break;
							case 4:
								// SHOOT
								lPress[i] &= ~KPressBits.SHOOT;
								break;
							default:
								break;
						}
					}
				}
			}
		}
		public void MoveCursor()
		{
			for (int i = 0; i < lPlayers.Count; i++)
			{
				if ((lPress[i] & KPressBits.UP) == KPressBits.UP)
					lPlayers[i].MoveReticle(Direction.UP);
				if ((lPress[i] & KPressBits.RIGHT) == KPressBits.RIGHT)
					lPlayers[i].MoveReticle(Direction.RIGHT);
				if ((lPress[i] & KPressBits.DOWN) == KPressBits.DOWN)
					lPlayers[i].MoveReticle(Direction.DOWN);
				if ((lPress[i] & KPressBits.LEFT) == KPressBits.LEFT)
					lPlayers[i].MoveReticle(Direction.LEFT);
				if ((lPress[i] & KPressBits.SHOOT) == KPressBits.SHOOT)
					lPlayers[i].Shoot();
			}
		}
	}
}
