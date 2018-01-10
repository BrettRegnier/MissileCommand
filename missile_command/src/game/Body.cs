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
		public Point TopLeft { get { return new Point(Left, Top); } }
		public Point TopCenter { get { return new Point(Center.X, Top); } }
		public Point TopRight { get { return new Point(Right, Top); } }
		public Point BottomLeft { get { return new Point(Left, Bottom); } }
		public Point BottomCenter { get { return new Point(Center.X, Bottom); } }
		public Point BottomRight { get { return new Point(Right, Bottom); } }
		public Point CenterLeft { get { return new Point(Left, Center.Y); } }
		public Point CenterRight { get { return new Point(Right, Center.Y); } }
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
		public void AdjustPosition(int x, int y)
		{
			position.X += x;
			position.Y += y;
		}
		public void AdjustX(int x)
		{
			position.X += x;
		}
		public void AdjustY(int y)
		{
			position.Y += y;
		}
	}
}
