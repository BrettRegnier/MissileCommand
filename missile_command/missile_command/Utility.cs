using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class Size
    {
        int Width { get; set; }
        int Height { get; set; }

        public Size(int w, int h)
        {
            Width = w;
            Height = h;
        }
    }
}
