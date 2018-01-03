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
		private void Initialize(Point o, Size d, ETag t)
		{
			position = o;
			dimension = d;
			tag = t;
		}

		public abstract void Draw(Graphics g);

		public Point TopLeft()
		{
			return position;
		}
		public Point TopMiddle()
		{
			int nX = position.X + dimension.Width / 2;
			int nY = position.Y;
			return new Point(nX, nY);
		}
		public Point TopRight()
		{
			int nX = position.X + dimension.Width;
			int nY = position.Y;
			return new Point(nX, nY);
		}
		public Point BottomLeft()
		{
			int nX = position.X;
			int nY = position.Y + dimension.Height;
			return new Point(nX, nY);
		}
		public Point BottomMiddle()
		{
			int nX = position.X + dimension.Width / 2;
			int nY = position.Y + dimension.Height;
			return new Point(nX, nY);
		}
		public Point BottomRight()
		{
			int nX = position.X + dimension.Width;
			int nY = position.Y + dimension.Height;
			return new Point(nX, nY);
		}
		public Point LeftMiddle()
		{
			int nX = position.X;
			int nY = position.Y + dimension.Height / 2;
			return new Point(nX, nY);
		}
		public Point RightMiddle()
		{
			int nX = position.X + dimension.Width;
			int nY = position.Y + dimension.Height / 2;
			return new Point(nX, nY);
		}
		public Point Center()
		{
			int nX = position.X + dimension.Width / 2;
			int nY = position.Y + dimension.Height / 2;
			return new Point(nX, nY);
		}
		public Size Dimension()
		{
			return dimension;
		}
		public ETag GetTag()
		{
			return tag;
		}

		protected virtual void UpdatePosition(int x, int y)
		{
			position.X = x;
			position.Y = y;
		}
		protected virtual void UpdateDimension(int w, int h)
		{
			dimension.Width = w;
			dimension.Height = h;
		}
		protected virtual void UpdatePositionX(int x)
		{
			position.X = x;
		}
		protected virtual void UpdatePositionY(int y)
		{
			position.Y = y;
		}
	}
}
