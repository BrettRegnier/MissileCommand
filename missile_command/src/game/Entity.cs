using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	abstract class Entity : Component
	{
		public Collider Collider { get; protected set; }
		public ETag Tag { get; private set; }

		public Entity(int x, int y, int w, int h, ETag t) : base(x,y,w,h)
		{
			Collider = new Collider(Body);
			Collider.OnCollision += Collided;
			Tag = t;
		}
		protected abstract void Collided(Body body);
	}
}
