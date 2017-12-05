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
        private static Color p1Color;
        private static Color p2Color;
        private static Color p3Color;
        private static Color p4Color;

        public Config()
        {
            //load configs
        }

        public static Config GetInstance()
        {
            if (instance == null)
                instance = new Config();

            return instance;
        }

        public Color GetPlayerColor(PType p)
        {
            Color color = Color.Black;
            switch (p)
            {
                case (PType.ENEMY):
                    color = Color.White;
                    break;
                case (PType.PLAYER1):
                    color = Color.Green;
                    break;
                case (PType.PLAYER2):
                    color = Color.Blue;
                    break;
                case (PType.PLAYER3):
                    color = Color.Yellow;
                    break;
                case (PType.PLAYER4):
                    color = Color.Red;
                    break;
            }

            return color;
        }
    }
}

