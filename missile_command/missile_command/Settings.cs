using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace missile_command
{
	public static class PlayerSettings
	{
		public static Color LoadColor(Player p)
		{
			ColorConverter c = new ColorConverter();
			if (p == Player.player1)
				return (Color)c.ConvertFromString(ConfigurationManager.AppSettings["Player1C"]);
			else if (p == Player.player2)
				return (Color)c.ConvertFromString(ConfigurationManager.AppSettings["Player2C"]);
			else if (p == Player.player3)
				return (Color)c.ConvertFromString(ConfigurationManager.AppSettings["Player3C"]);
			else 
				return (Color)c.ConvertFromString(ConfigurationManager.AppSettings["Player4C"]);
		}
	}
}
