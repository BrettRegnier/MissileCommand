using System.Drawing;

namespace missile_command
{
	class Body
	{
		private Point position;
		private Size dimension;

		// TODO perhaps integer values that are offsets. For things like Reticle.

		public Body(int x, int y, int w, int h)
		{
			position = new Point(x, y);
			dimension = new Size(w, h);
		}
		public Body()
		{
			position = new Point();
			dimension = new Size();
		}

		public Size Dimension { get { return dimension; } }
		public int Left { get { return position.X; } }
		public int Right { get { return position.X + dimension.Width; } }
		public int Top { get { return position.Y; } }
		public int Bottom { get { return position.Y + dimension.Height; } }
		public int CenterX { get { return position.X + dimension.Width / 2; } }
		public int CenterY { get { return position.Y + dimension.Height / 2; } }
		public Point TopLeft { get { return new Point(Left, Top); } }
		public Point TopCenter { get { return new Point(CenterX, Top); } }
		public Point TopRight { get { return new Point(Right, Top); } }
		public Point BottomLeft { get { return new Point(Left, Bottom); } }
		public Point BottomCenter { get { return new Point(CenterX, Bottom); } }
		public Point BottomRight { get { return new Point(Right, Bottom); } }
		public Point CenterLeft { get { return new Point(Left, CenterY); } }
		public Point CenterRight { get { return new Point(Right, CenterY); } }
		public int Width {  get { return dimension.Width; } }
		public int Height {  get { return dimension.Height; } }
		public Point Center
		{
			get
			{

				int nX = position.X + dimension.Width / 2;
				int nY = position.Y + dimension.Height / 2;
				return new Point(nX, nY);
			}
		}

		public void UpdateDimension(int w, int h)
		{
			dimension.Width = w;
			dimension.Height = h;
		}
		public void UpdateWidth(int w)
		{
			dimension.Width = w;
		}
		public void UpdateHeight(int h)
		{
			dimension.Height = h;
		}
		public void UpdatePosition(int x, int y)
		{
			position.X = x;
			position.Y = y;
		}
		public void UpdatePositionX(int x)
		{
			position.X = x;
		}
		public void UpdatePositionY(int y)
		{
			position.Y = y;
		}
		public void MovePosition(int x, int y)
		{
			position.X += x;
			position.Y += y;
		}
		public void MovePositionX(int x)
		{
			position.X += x;
		}
		public void MovePositionY(int y)
		{
			position.Y += y;
		}
	}
}
