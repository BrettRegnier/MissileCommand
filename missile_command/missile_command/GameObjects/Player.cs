using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
    class Player : GameObject
    {
        private Reticle cursor;

        public Player(Point pos, Dimensions dim, Graphics g, PType p) : base(pos, dim, g, p)
        {
            cursor = new Reticle(g, p);
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void Collided()
        {
            throw new NotImplementedException();
        }
    }
}
