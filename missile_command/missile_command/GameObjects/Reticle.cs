using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
    class Reticle
    {
        PType player;
        Graphics graphics;

        public Reticle(Graphics g, PType p)
        {
            graphics = g;
            player = p;
        }

        public void Draw()
        {

        }
    }
}
