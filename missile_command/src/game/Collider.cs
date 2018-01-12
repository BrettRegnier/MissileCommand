using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class Collider
	{
		public delegate void Collision(Collider body);
		public event Collision OnCollision;

		public Body Body { get; set; }

		public Collider(Body b)
		{
			Body = b;
		}

		public void CollisionDetection(Collider collidee)
		{
			if (Colliding(collidee.Body))
			{
				collidee.OnCollision(this);
				OnCollision(collidee);
			}
		}

		private bool Colliding(Body collidee)
		{
			if (Body.Right < collidee.Left)
				return false;
			if (Body.Left >= collidee.Right)
				return false;
			if (Body.Bottom < collidee.Top)
				return false;
			if (Body.Top >= collidee.Bottom)
				return false;

			return true;
		}
	}
}
