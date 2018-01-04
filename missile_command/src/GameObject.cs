using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace missile_command
{
	abstract class GameObject : Entity
	{
		protected PType pType;

		public GameObject(Point o, Size d, PType p, ETag t) : base(o, d, t)
		{
			pType = p;
		}
		public GameObject(PType p, ETag t) : base(t)
		{
			pType = p;
		}

		public abstract void Collided();
		public PType GetPlayerType() { return pType; }
	}
}
