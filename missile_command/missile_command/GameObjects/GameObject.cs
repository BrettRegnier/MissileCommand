using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace missile_command
{
	// TODO add needed public methods for the width, position, 
	interface IGameObject
	{
		void Draw(Graphics g);
		void Collided();
		Point GetPosition();
		Dimension GetDimension();
	}

	abstract class GameObject : IGameObject
	{
		protected PType pType;
		protected Point origin;
		protected Account account;

		public GameObject(Point pos, PType p, Account a)
		{
			origin = pos;
			pType = p;
			account = a;
		}
		
		public abstract void Draw(Graphics g);
		public abstract void Collided();
		public abstract Point GetPosition();
		public abstract Dimension GetDimension();
		public abstract PType GetPlayerType();
	}
}
