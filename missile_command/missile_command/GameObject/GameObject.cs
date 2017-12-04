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
        Size Dimensions { get; set; }  

        void Draw();
        void Collided();
    }

    abstract class GameObject : IGameObject
    {
        public static Graphics graphics;

        public Point Position { get; set; }
        public Size Dimensions { get; set; }

        public GameObject(Point pos, Graphics g)
        {
            Position = pos;
            Dimensions = dim;
            graphics = g;
        }

        public abstract void Draw();
        public abstract void Collided();
    }
}
