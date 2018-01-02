using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace missile_command
{
	abstract class GameObject : Entity
	{
		protected PType pType;
		protected Account account;

		public GameObject(Point o, Size d, PType p, Account a) : base(o, d)
		{
			pType = p;
			account = a;
		}
		
		public abstract void Draw(Graphics g);
		public abstract void Collided();
		public abstract PType GetPlayerType();
	}
}
