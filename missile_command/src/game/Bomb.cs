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
		
		private bool atDestination;
		private SolidBrush brush;
		private Point destination;
		private int explosionSize;
		private bool flash;
		private Collider firstCollider;
		private PointF fNextPosition; // was newX, newY
		private int growthCount;
		private Point lOrigin; // traceOrigin
		private int oriRadius;
		private Pen pen;
		private float speed;
		private PointF velocity;

		public PType PlayerType { get; private set; }
		public bool GivePoints { get; private set; }

		public Bomb(int x, int y, int w, int h, Point des, PType p, ETag t) : base(x, y, w, h, t)
		{
			// Set fields
			atDestination = false;
			destination = des;
			explosionSize = Config.Instance.ExplosionDiameter;
			flash = false;
			fNextPosition = Body.TopLeft;
			growthCount = 0;
			lOrigin = Body.Center;
			oriRadius = Body.Dimension.Width;
			PlayerType = p;
			GivePoints = false;

			if (p == PType.ENEMY)
				speed = Config.Instance.EBombSpeed;
			else
				speed = Config.Instance.PBombSpeed;

			CalculateVelocity();
			SetColor();
		}
		private void CalculateVelocity()
		{
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
		protected override void Collided(Collider collider)
		{
			if (firstCollider == null)
				firstCollider = collider;
			atDestination = true;
		}
		public override void Draw(Graphics g)
		{
			if (flash)
				g.DrawEllipse(pen, Body.Left, Body.Top, Body.Width, Body.Height);
			else
				g.FillEllipse(brush, Body.Left, Body.Top, Body.Width, Body.Height);

			if (!atDestination)
				g.DrawLine(pen, lOrigin.X, lOrigin.Y, Body.Center.X, Body.Center.Y);
		}
		private void RepositionExplosion()
		{
			//Reposition the bomb's point for the explosion
			int explosionSize = Config.Instance.ExplosionDiameter;
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
					atDestination = true;
				}
			}
		}
		public override void PostUpdate(long gameTime)
		{
			if (atDestination)
			{
				if (Body.Width >= explosionSize)
				{
					if (firstCollider != null && firstCollider.Owner is Bomb)
						if (((Bomb)firstCollider.Owner).PlayerType == PType.PLAYER)
							GivePoints = true;
					DestroyBomb(this);
				}

				// Start growing the explosion
				// if false then grow the size of it.
				growthCount++;
				if (growthCount % 4 == 0)
				{
					// flash the graphics
					if (growthCount % 6 == 0)
						flash = !flash;

					int growth = explosionSize / 30;
					// if growth is not even, then make it even.
					if (growth % 2 != 0)
						growth++;

					if (Body.Width <= oriRadius)
						growth = 8;

					Body.UpdateDimension(Body.Width + growth, Body.Height + growth);
					Body.AdjustPosition(-growth / 2, -growth / 2);
				}
			}
		}
	}
}
