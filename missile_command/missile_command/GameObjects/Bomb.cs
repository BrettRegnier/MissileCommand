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
		public delegate void Deconstruct(GameObject bomb);
		public event Deconstruct DestroyBomb;
		#endregion

		// TODO add some sort of config based settings
		private Rectangle circle;
		private int explosionSize = 100;
		private int explosionRadius;
		private int radius = 4; // TODO REMOVE MAGIC NUMBER
		private float speed = 5f; // TODO REMOVE MAGIC NUMBER
		private PointF velocity;

		private bool atDestination = false;
		private Point destination;

		private SolidBrush brush;
		private Pen pen;
		private float newX, newY;
		private bool explosionFlash = false;
		private int flashCount = 0;
		private int destroyCount = 0;

		public Bomb(Point pos, Point des, PType p, Account a) : base(pos, p, a)
		{
			// TODO replace magic numbers
			// TODO add line tracing
			destination = des;

			circle = new Rectangle(origin.X, origin.Y, radius, radius);
			explosionRadius = explosionSize / 2;

			newX = origin.X;
			newY = origin.Y;

			SetColor();
			CalculateVelocity();
		}
		public override void Collided()
		{
			if (!atDestination)
				PositionExplosion();
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
		private void PositionExplosion()
		{
			int meanCoorindates = ((explosionSize / 2) - Convert.ToInt32(CURSOR_OFFSET) / 2);
			circle.X = circle.X - meanCoorindates;
			circle.Y = circle.Y - meanCoorindates;
			circle.Width = explosionSize;
			circle.Height = explosionSize;

			atDestination = true;
		}
		private void CalculateVelocity()
		{
			// Difference between the origin and where it will hit.
			double diffX = circle.X - destination.X;
			double diffY = circle.Y - destination.Y;
			double tanAngle = 0; //Trajectory angle

			tanAngle = Math.Atan(diffY / diffX); //Gets the Tangent Angle 

			velocity.X = speed * (float)Math.Cos(tanAngle);
			velocity.Y = speed * (float)Math.Sin(tanAngle);

			if (destination.X <= circle.X)
			{
				velocity.X *= -1.0F;
				velocity.Y *= -1.0F;
			}
		}
		private void SetColor()
		{
			Color color = Config.Instance().GetPlayerColor(account);
			brush = new SolidBrush(color);
			pen = new Pen(color);
		}
		public override void Draw(Graphics g)
		{
			Move();
			if (!atDestination)
			{
				g.FillEllipse(brush, circle);
			}
			else
			{
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
					DestroyBomb(this);
				}
			}
			g.DrawLine(pen, origin.X, origin.Y, circle.X + circle.Width / 2, circle.Y + circle.Height / 2);
		}

		public override Point GetPosition() { return new Point(circle.X, circle.Y); }
		public override Dimension GetDimension() { return new Dimension(circle.Width, circle.Height); }
		public override PType GetPlayerType() { return pType; }
	}
}
