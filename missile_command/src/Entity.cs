using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	abstract class Entity
	{
		public Body Body { get; private set; }
		public Collider Collider { get; private set; }
		public ETag Tag { get; private set; }

		public Entity(int x, int y, int w, int h, ETag t)
		{
			Body = new Body(x, y, w, h);
			Collider = new Collider(Body);
			Collider.OnCollision += Collided;
			Tag = t;
		}
		public Entity(ETag t)
		{
			//Might have to pass by reference
			Body = new Body();
			Collider = new Collider(Body);
			Tag = t;
		}

		public abstract void Draw(System.Drawing.Graphics g);
		protected abstract void Collided(Body body);
	}
}
