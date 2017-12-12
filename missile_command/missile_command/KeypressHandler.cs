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
		private enum KPress
		{
			NONE = 0,
			UP = 1,
			RIGHT = 2,
			DOWN = 4,
			LEFT = 8,
			SHOOT = 16
		};

		private KPress pPress = KPress.NONE;
		private bool isKeyDown = false;
		private Player player;
		private PType type;

		public KeypressHandler(Player p)
		{
			player = p;

			// Load key configs
		}
		private void MoveCursor()
		{
			player.MoveReticle();
		}
		public void KeyDown(KeyEventArgs e)
		{

		}
		public void KeyUp(KeyEventArgs e)
		{

		}
	}
}
