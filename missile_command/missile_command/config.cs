using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
    // Singleton class.
    public class Config
    {
        private static Config instance;
        private static Color P1Color;
        private static Color P2Color;
        private static Color P3Color;
        private static Color P4Color;

        public Config()
        {
            //load configs
        }

        public static Config getInstance()
        {
            if (instance == null)
                instance = new Config();

            return instance;
        }

        public Color getPlayerColor(Player p)
        {
            Color color = Color.Black;
            switch (p)
            {
                case (Player.enemy):
                    color = Color.White;
                    break;
                case (Player.player1):
                    color = Color.Green;
                    break;
                case (Player.player2):
                    color = Color.Blue;
                    break;
                case (Player.player3):
                    color = Color.Yellow;
                    break;
                case (Player.player4):
                    color = Color.Red;
                    break;
            }

            return color;
        }
    }
}

