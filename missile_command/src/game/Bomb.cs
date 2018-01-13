using missile_command.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace missile_command
{
	class Bomb : Entity
	{
		private const int CURSOR_OFFSET = 5;

		public delegate void Deconstruct(Entity bomb);
		public event Deconstruct DestroyBomb;

		// TODO add some sort of config based settings
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

		public Bomb(Point o, Size d, Point des, PType p, ETag t) : base(o.X, o.Y, d.Width, d.Height, t)
		{
			destination = des;
			lOrigin = Body.TopLeft;

			// Adjust the bomb so the "origin" of it is the center 
			Body.AdjustX(-Body.Width / 2);
			explosionRadius = Config.Instance.DefaultExplosionSize / 2;

			// Set to the current position
			fNextPosition = Body.TopLeft;

			SetColor();
			CalculateVelocity();
		}
		protected override void Collided(Collider collider)
		{
			if (!atDestination)
				RepositionExplosion();
		}
		private void CalculateVelocity()
		{
			float speed = Config.Instance.DefaultBombSpeed;
			// Difference between the origin and where it will hit.
			double diffX = Body.Left - destination.X;
			double diffY = Body.Top - destination.Y;
			double tanAngle = 0; //Trajectory angle

			tanAngle = Math.Atan(diffY / diffX); //the Tangent Angle 

			velocity.X = speed * (float)Math.Cos(tanAngle);
			velocity.Y = speed * (float)Math.Sin(tanAngle);

			if (destination.X <= Body.Left)
			{
				velocity.X *= -1.0F;
				velocity.Y *= -1.0F;
			}
		}
		public override void Draw(Graphics g)
		{
			if (!atDestination)
			{
				g.FillEllipse(brush, Body.Left, Body.Top, Body.Width, Body.Height);
				g.DrawLine(pen, lOrigin.X, lOrigin.Y, Body.Center.X, Body.Center.Y);
			}
			else
			{
				if (explosionFlash)
					g.DrawEllipse(pen, Body.Left, Body.Top, Body.Width, Body.Height);
				else
					g.FillEllipse(brush, Body.Left, Body.Top, Body.Width, Body.Height);
			}
		}
		private void RepositionExplosion()
		{
			// Reposition the bomb's point for the explosion
			int explosionSize = Config.Instance.DefaultExplosionSize;
			int meanCoorindates = ((explosionSize / 2) - (Body.Width / 2));

			int nX = Body.Left - meanCoorindates;
			int nY = Body.Top - meanCoorindates;
			int nWidth = explosionSize;
			int nHeight = explosionSize;

			Body.UpdatePosition(nX, nY);
			Body.UpdateDimension(nWidth, nHeight);

			atDestination = true;
		}
		private void SetColor()
		{
			Color color = Config.Instance.GetPlayerColor(Tag);
			brush = new SolidBrush(color);
			pen = new Pen(color);
		}
		public override void Update(long gameTime)
		{
			if (!atDestination)
			{
				float remainingDiffX = Math.Abs(destination.X - Body.Left);
				float remainingDiffY = Math.Abs(destination.Y - Body.Top);

				if ((Body.Left != destination.X) || (Body.Top != destination.Y))
				{
					fNextPosition.X += velocity.X;
					fNextPosition.Y += velocity.Y;

					if (remainingDiffX < Math.Abs(velocity.X) && remainingDiffY < Math.Abs(velocity.Y))
					{
						fNextPosition.X = destination.X;
						fNextPosition.Y = destination.Y;
					}

					Body.UpdatePosition(Convert.ToInt32(fNextPosition.X), Convert.ToInt32(fNextPosition.Y));
				}
				else
				{
					RepositionExplosion();
				}
			}
		}
		public override void PostUpdate(long gameTime)
		{
			// TODO make explosion grow
			if (atDestination)
			{
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
