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
        public static Bomb makeBomb(Player player, Point origin, Point destination)
        {
            return new Bomb(player, origin, destination);
        }
    }
}
