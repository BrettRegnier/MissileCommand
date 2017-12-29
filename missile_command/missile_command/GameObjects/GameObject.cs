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
		void DetectCollision(GameObject collider);
		Point GetPosition();
		Dimension GetDimension();
	}

	abstract class GameObject : IGameObject
	{
		protected PType bPlayer;
		protected Point bPosition;
		protected Dimension bDimension;

		public GameObject(Point pos, PType p)
		{
			bPosition = pos;
			bPlayer = p;
		}

		protected abstract void Collided();
		public abstract void Draw(Graphics g);
		public abstract void DetectCollision(GameObject collider);
		public abstract Point GetPosition();
		public abstract Dimension GetDimension();
	}
}
