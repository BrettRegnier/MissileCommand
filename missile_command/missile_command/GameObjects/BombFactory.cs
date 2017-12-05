using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
    class BombFactory
    {
        public static Bomb MakeBomb(Point ori, Point des, Dimensions dim, Graphics g, PType p)
        {
            return new Bomb(ori, des, dim, g, p);
        }
    }
}
