using System.Drawing;

namespace missile_command
{
	// TODO rename to collider? Perhaps everything shouldn't inherit it, but is composed of it.
	abstract class Body
	{
		private Point position;
		private Size dimension;
		private ETag tag;

		public Body(Point o, Size d, ETag t)
		{
			Initialize(o, d, t);
		}
		public Body(Point o, int w, int h, ETag t)
		{
			Initialize(o, new Size(w, h), t);
		}
		public Body(int x, int y, Size d, ETag t)
		{
			Initialize(new Point(x, y), d, t);
		}
		public Body(int x, int y, int w, int h, ETag t)
		{
			Initialize(new Point(x, y), dimension = new Size(w, h), t);
		}
		public Body(ETag t)
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

		public Size Dimension { get { return dimension; } }
		public ETag Tag { get { return tag; } }

		public virtual int Left { get { return position.X; } }
		public virtual int Right { get { return position.X + dimension.Width; } }
		public virtual int Top { get { return position.Y; } }
		public virtual int Bottom { get { return position.Y + dimension.Height; } }
		public virtual int CenterX { get { return position.X + dimension.Width / 2; } }
		public virtual int CenterY { get { return position.Y + dimension.Height / 2; } }
		public virtual Point TopLeft { get { return new Point(Left, Top); } }
		public virtual Point TopCenter { get { return new Point(CenterX, Top); } }
		public virtual Point TopRight { get { return new Point(Right, Top); } }
		public virtual Point BottomLeft { get { return new Point(Left, Bottom); } }
		public virtual Point BottomCenter { get { return new Point(CenterX, Bottom); } }
		public virtual Point BottomRight { get { return new Point(Right, Bottom); } }
		public virtual Point CenterLeft { get { return new Point(Left, CenterY); } }
		public virtual Point CenterRight { get { return new Point(Right, CenterY); } }
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
		protected virtual void UpdateWidth(int w)
		{
			dimension.Width = w;
		}
		protected virtual void UpdateHeight(int h)
		{
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
