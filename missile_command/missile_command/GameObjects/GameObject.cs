using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
    interface IGameObject
    {
        Point Position { get; set; }
        Dimensions Dimensions { get; set; }

        void Draw(Graphics g);
        void Collided();
    }

    abstract class GameObject : IGameObject
    {
        private PType player;

        public Point Position { get; set; }
        public Dimensions Dimensions { get; set; }

        public GameObject(Point pos, Dimensions dim, PType p)
        {
            Position = pos;
            Dimensions = dim;
            player = p;
        }

        public abstract void Draw(Graphics g);
        public abstract void Collided();
    }
}
