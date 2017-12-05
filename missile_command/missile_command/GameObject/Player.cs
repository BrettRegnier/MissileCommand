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
        public Player(Point pos, Size dim, Graphics g, PType p) : base(pos, dim, g, p)
        {

        }

        public override void Collided()
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
