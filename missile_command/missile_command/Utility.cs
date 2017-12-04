using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
    public enum PType
    {
        enemy = 0,
        player1 = 1,
        player2 = 2,
        player3 = 3,
        player4 = 4
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
