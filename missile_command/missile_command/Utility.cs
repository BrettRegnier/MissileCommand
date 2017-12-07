using System;
using System.Collections.Generic;
using System.Text;

namespace missile_command
{
    public enum PType
    {
        ENEMY = 0,
        PLAYER1 = 1,
        PLAYER2 = 2,
        PLAYER3 = 3,
        PLAYER4 = 4,
        SYSTEM = 5
    }

    public class Dimensions
    {
        int Width { get; set; }
        int Height { get; set; }

        public Dimensions(int w, int h)
        {
            Width = w;
            Height = h;
        }
    }
}
