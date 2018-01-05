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

		public delegate void Deconstruct(Body bomb);
		public event Deconstruct DestroyBomb;
		public delegate void RaisePoints(int points);
		public event RaisePoints AddScore;

		// TODO add some sort of config based settings
		private Rectangle circle;
		private int explosionRadius;
		private PointF velocity;

		private bool atDestination = false;
		private Point destination;

		private SolidBrush brush;
		private Pen pen;
		private PointF fNextPosition; // was newX, newY
		private bool explosionFlash = false;
		private int flashCount = 0;
		private int destroyCount = 0;
		private Point lOrigin; // traceOrigin

		public Bomb(Point o, Size d, Point des, PType p, ETag a) : base(o, d, p, a)
		{
			destination = des;
			lOrigin = TopLeft;

			// Adjust the bomb so the "origin" of it is the center 
			MovePositionX(-Dimension.Width / 2);
			circle = new Rectangle(TopLeft.X, TopLeft.Y, Dimension.Width, Dimension.Height);
			explosionRadius = Config.Instance().DefaultExplosionSize() / 2;

			// Set to the current position
			fNextPosition = TopLeft;

			SetColor();
			CalculateVelocity();
		}
		public override void Collided()
		{
			if (!atDestination)
			{
				RepositionExplosion();
				AddScore(10); // TODO add into config, plus a calculation based on survival waves.
			}
		}
		private void Move()
		{
			if (!atDestination)
			{
				float remainingDiffX = Math.Abs(destination.X - Left);
				float remainingDiffY = Math.Abs(destination.Y - Top);

				if ((Left != destination.X) || (Top != destination.Y))
				{
					fNextPosition.X += velocity.X;
					fNextPosition.Y += velocity.Y;

					if (remainingDiffX < Math.Abs(velocity.X) && remainingDiffY < Math.Abs(velocity.Y))
					{
						fNextPosition.X = destination.X;
						fNextPosition.Y = destination.Y;
					}

					UpdatePosition(Convert.ToInt32(fNextPosition.X), Convert.ToInt32(fNextPosition.Y));
				}
				else
				{
					RepositionExplosion();
				}
			}
		}
		private void RepositionExplosion()
		{
			// Reposition the bomb's point for the explosion
			int explosionSize = Config.Instance().DefaultExplosionSize();
			int meanCoorindates = ((explosionSize / 2) - (Dimension.Width / 2));

			int nX = Left - meanCoorindates;
			int nY = Top - meanCoorindates;
			int nWidth = explosionSize;
			int nHeight = explosionSize;

			UpdatePosition(nX, nY);
			UpdateDimension(nWidth, nHeight);

			atDestination = true;
		}
		private void CalculateVelocity()
		{
			float speed = Config.Instance().DefaultBombSpeed();
			// Difference between the origin and where it will hit.
			double diffX = Left - destination.X;
			double diffY = Top - destination.Y;
			double tanAngle = 0; //Trajectory angle

			tanAngle = Math.Atan(diffY / diffX); //the Tangent Angle 

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
			Color color = Config.Instance().GetPlayerColor(Tag);
			brush = new SolidBrush(color);
			pen = new Pen(color);
		}
		protected override void UpdatePosition(int x, int y)
		{
			circle.X = x;
			circle.Y = y;
			base.UpdatePosition(x, y);
		}
		protected override void UpdateDimension(int w, int h)
		{
			circle.Width = w;
			circle.Height = h;
			base.UpdateDimension(w, h);
		}
		public override void Draw(Graphics g)
		{
			Move();
			// TODO make the explosion "grow".
			if (!atDestination)
			{
				g.FillEllipse(brush, circle);
				g.DrawLine(pen, lOrigin.X, lOrigin.Y, CenterX, CenterY);
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
		}
	}
}
