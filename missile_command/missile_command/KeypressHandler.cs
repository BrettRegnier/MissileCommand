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
		enum KPress
		{
			NONE = 0,
			UP = 1,
			RIGHT = 2,
			DOWN = 4,
			LEFT = 8,
			SHOOT = 16
		};

		private KPress P1 = KPress.NONE;

		private bool isKeyDownP1 = false;
		private bool isKeyDownP2 = false;
		private bool isKeyDownP3 = false;
		private bool isKeyDownP4 = false;

		private void MoveCursor()
		{

		}

		public static void KeyDown(KeyEventArgs e)
		{

		}

		public static void KeyUp(KeyEventArgs e)
		{

		}
	}
}
