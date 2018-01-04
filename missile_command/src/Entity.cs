using System.Drawing;

namespace missile_command
{
	// TODO rename to collider? Perhaps everything shouldn't inherit it, but is composed of it.
	abstract class Entity
	{
		protected Point position;
		protected Size dimension;
		protected ETag tag;

		public Entity(Point o, Size d, ETag t)
		{
			Initialize(o, d, t);
		}
		public Entity(Point o, int w, int h, ETag t)
		{
			Initialize(o, new Size(w, h), t);
		}
		public Entity(int x, int y, Size d, ETag t)
		{
			Initialize(new Point(x, y), d, t);
		}
		public Entity(int x, int y, int w, int h, ETag t)
		{
			Initialize(new Point(x, y), dimension = new Size(w, h), t);
		}
		public Entity(ETag t)
		{
			tag = t;
		}
		private void Initialize(Point o, Size d, ETag t)
		{
			position = o;
			dimension = d;
			tag = t;
		}

		public abstract void Draw(Graphics g);

		public Size Dimension() { return dimension; }
		public ETag GetTag() { return tag; }

		public virtual int Left() { return position.X; }
		public virtual int Right() { return position.X + dimension.Width; }
		public virtual int Top() { return position.Y; }
		public virtual int Bottom() { return position.Y + dimension.Height; }
		public virtual int CenterX() { return position.X + dimension.Width / 2; }
		public virtual int CenterY() { return position.Y + dimension.Height / 2; }
		public virtual Point TopLeft() { return new Point(Left(), Top()); }
		public virtual Point TopMiddle() { return new Point(CenterX(), Top()); }
		public virtual Point TopRight() { return new Point(Right(), Top()); }
		public virtual Point BottomLeft() { return new Point(Left(), Bottom()); }
		public virtual Point BottomMiddle() { return new Point(CenterX(), Bottom()); }
		public virtual Point BottomRight() { return new Point(Right(), Bottom()); }
		public virtual Point MiddleLeft() { return new Point(Left(), CenterY()); }
		public virtual Point MiddleRight() { return new Point(Right(), CenterY()); }
		public virtual Point Center()
		{
			int nX = position.X + dimension.Width / 2;
			int nY = position.Y + dimension.Height / 2;
			return new Point(nX, nY);
		}

		protected virtual void UpdateDimension(int w, int h)
		{
			dimension.Width = w;
			dimension.Height = h;
		}
		protected virtual void UpdatePosition(int x, int y)
		{
			position.X = x;
			position.Y = y;
		}
		protected virtual void UpdatePositionX(int x)
		{
			position.X = x;
		}
		protected virtual void UpdatePositionY(int y)
		{
			position.Y = y;
		}
		protected virtual void MovePosition(int x, int y)
		{
			position.X += x;
			position.Y += y;
		}
		protected virtual void MovePositionX(int x)
		{
			position.X += x;
		}
		protected virtual void MovePositionY(int y)
		{
			position.Y += y;
		}
	}
}
