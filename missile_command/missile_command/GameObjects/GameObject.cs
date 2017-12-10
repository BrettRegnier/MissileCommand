﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace missile_command
{
    interface IGameObject
    {
        Point Position { get; set; }
        Dimension Dimensions { get; set; }

        void Draw(Graphics g);
        void Collided();
    }

    abstract class GameObject : IGameObject
    {
        protected PType player;

        public Point Position { get; set; }
        public Dimension Dimensions { get; set; }

        public GameObject(Point pos, Dimension dim, PType p)
        {
            Position = pos;
            Dimensions = dim;
            player = p;
        }

        public abstract void Draw(Graphics g);
        public abstract void Collided();
    }
}
