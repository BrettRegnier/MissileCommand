using missile_command.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace missile_command
{
	class Bomb : GameObject
	{
		private const int CURSOR_OFFSET = 5;

		#region Delegates
		public delegate void Deconstruct(ListType lt, GameObject bomb);
		public event Deconstruct DestroyBomb;
		#endregion

		// TODO add some sort of config based settings
		private Rectangle circle;
		private int explosionSize = 200;
		private int explosionRadius;
		private int radius = 5; // TODO REMOVE MAGIC NUMBER
		private float speed = 20f; // TODO REMOVE MAGIC NUMBER
		private PointF velocity;

		private bool atDestination = false;
		private Point destination;

		private SolidBrush brush;
		private Pen pen;
		private float newX, newY;
		private bool explosionFlash = false;
		private int flashCount = 0;
		private int destroyCount = 0;

		public Bomb(Point pos, Point des, Dimension dim, PType p) : base(pos, p)
		{
			destination = des;

			// TODO replace magic numbers
			circle = new Rectangle(pos.X, pos.Y, 10, 10);
			newX = pos.X;
			newY = pos.Y;

			SetColor();
			CalculateVelocity();

			explosionRadius = explosionSize / 2;
		}
		private void Move()
		{
			if (!atDestination)
			{
				float remainingDiffX = Math.Abs(destination.X - circle.X);
				float remainingDiffY = Math.Abs(destination.Y - circle.Y);

				if ((circle.Y != destination.Y) || (circle.X != destination.X))
				{
					newX += velocity.X;
					newY += velocity.Y;

					if (remainingDiffX < Math.Abs(velocity.X) && remainingDiffY < Math.Abs(velocity.Y))
					{
						newX = destination.X;
						newY = destination.Y;
					}

					circle.X = Convert.ToInt32(newX);
					circle.Y = Convert.ToInt32(newY);
				}
				else
				{
					PositionExplosion();
				}
			}
		}
		private void Collide()
		{
			throw new NotImplementedException();
		}
		private void ExplosionCalc()
		{
			throw new NotImplementedException();
		}
		private void PositionExplosion()
		{
			int meanCoorindates = ((explosionSize / 2) - Convert.ToInt32(CURSOR_OFFSET));
			circle.X = circle.X - meanCoorindates;
			circle.Y = circle.Y - meanCoorindates;
			atDestination = true;
		}
		private void CalculateVelocity()
		{
			// Difference between the origin and where it will hit.
			double diffX = Position.X - destination.X;
			double diffY = Position.Y - destination.Y;
			double tanAngle = 0; //Trajectory angle

			tanAngle = Math.Atan(diffY / diffX); //Gets the Tangent Angle 

			velocity.X = speed * (float)Math.Cos(tanAngle);
			velocity.Y = speed * (float)Math.Sin(tanAngle);

			if (destination.X < Position.X)
			{
				velocity.X *= -1.0F;
				velocity.Y *= -1.0F;
			}
		}
		private void SetColor()
		{
			Color color = Config.Instance().GetPlayerColor(player);
			brush = new SolidBrush(color);
			pen = new Pen(color);
		}
		public override void Draw(Graphics g)
		{
			Move();
			if (!atDestination)
			{
				g.FillEllipse(brush, circle); // Might draw into a line or a smaller circle.
			}
			else
			{
				circle.Width = explosionSize;
				circle.Height = explosionSize;

				if (explosionFlash)
					g.DrawEllipse(pen, circle);
				else
					g.FillEllipse(brush, circle);

				flashCount++;
				if (flashCount % 4 == 0)
				{
					explosionFlash = !explosionFlash;
					destroyCount++;
				}
				if (flashCount == 16)
				{
					ListType t = ListType.E_BOMB;
					if (player != 0)
						t = ListType.P_BOMB;
					DestroyBomb(t, this);
				}
			}
		}
		public override void Collided()
		{
			PositionExplosion();
		}

		public int GetWidth { get { return circle.Width; } }
		public int GetHeight { get { return circle.Height; } }
		public int GetX { get { return circle.X; } }
		public int GetY { get { return circle.Y; } }
		public Point GetCenter { get { return new Point(circle.X + (circle.Width / 2), circle.Y + (circle.Height / 2)); } }
		public int GetRadius { get { return radius; } }
	}
}
