using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

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
