using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class MouseHandler
	{
		[DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true)]
		public static extern int GetAsyncKeyState(int vKey);

		private static MouseHandler instance;

		public static MouseHandler Instance()
		{
			if (instance == null)
			{
				instance = new MouseHandler();
			}

			return instance;
		}
		public bool MouseState(MOUSE_BUTTONS button)
		{
			return GetAsyncKeyState((int)button) > 1;
		}
	}

	enum MOUSE_BUTTONS : int //Source: https://msdn.microsoft.com/nl-nl/library/windows/desktop/dd375731(v=vs.85).aspx
	{
		VK_LBUTTON = 0x01,
		VK_RBUTTON = 0x02,
		VK_MBUTTON = 0x04,
		VK_XBUTTON1 = 0x05,
		VK_XBUTTON2 = 0x06,
	}
}
