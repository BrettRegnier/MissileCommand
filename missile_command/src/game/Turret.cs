using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace missile_command
{
	// TODO add ammo, each turret has a certain amount of ammo to use.
	// TODO make cooldown and ready state for turret, on update(draw) game will update on the cooldown
	class Turret : Entity
	{
		public delegate void Fire(Point origin, Point destination, ETag a);
		public event Fire TurretShoot;

		private const int GUN_END_LENGTH = 5;
		
		private Body aim;
		private StatusBar hpBar;
		private Pen pen;
		private Collider prevCollider;
		private Point turretEnd;

		public Turret(int x, int y, int w, int h, PType p, ETag t) : base(x, y, w, h, t)
		{
			pen = new Pen(Config.Instance.GetPlayerColor(t));

			// Move tower to left to position it in the center of the given origin
			int nX = Body.Left - Config.Instance.TurretDiameter / 2;
			int nY = Body.Top - Config.Instance.TurretDiameter / 2;
			Body.UpdatePosition(nX, nY);

			hpBar = new HealthBar(Body.Center.X, Body.Center.Y, 50, 10);
		}
		public void AttachReticleBody(Body r)
		{
			aim = r;
		}
		protected override void Collided(Collider collider)
		{
			if (prevCollider != collider)
			{
				// Need to check to see if the last collider is the same as the one now.
				if (collider.Body.Top < Body.Center.Y)
				{
					if (hpBar.IsAlive)
						hpBar.Damage();
					else
						Alive = false;
				}
				prevCollider = collider;
			}
		}
		public override void Draw(Graphics g)
		{
			hpBar.Draw(g);
			g.DrawArc(pen, Body.Left, Body.Top, Body.Width, Body.Height, 180, 180);

			if (Alive)
				g.DrawLine(pen, Body.Center, turretEnd);
		}
		public void ShootTurret()
		{
			int bmbRadius = Config.Instance.PBombDiameter / 2;
			Point origin = new Point(turretEnd.X - bmbRadius, turretEnd.Y - bmbRadius); 
			TurretShoot(origin, aim.Center, Tag);
		}
		public override void Update(long gameTime)
		{
			hpBar.Update(gameTime);
			// Difference between where we are aiming and the center of the turret
			int cursorTowerDiffX = aim.Center.X - Body.Center.X;
			int cursorTowerDiffY = Body.Center.Y - aim.Center.Y;

			// This is the size of the line that is the gun
			int gunLength = Config.Instance.TurretDiameter / 2 + GUN_END_LENGTH;

			// Finds the adjecent tangent in radians.
			double turretAngle = Math.Atan((double)cursorTowerDiffY / (double)cursorTowerDiffX);

			// Find Hypotenuse, 
			int turretX = (int)(Math.Cos(turretAngle) * gunLength);
			int turretY = (int)(Math.Sin(turretAngle) * gunLength);
			if (turretAngle < 0)
			{
				turretX *= -1;
				turretY *= -1;
			}

			turretX = Body.Center.X + turretX;
			turretY = Body.Center.Y - turretY;

			turretEnd = new Point(turretX, turretY);
		}

		public override void PostUpdate(long gameTime)
		{
			hpBar.PostUpdate(gameTime);
			if (hpBar.IsAlive)
			{
				Alive = true;
			}
		}
	}
}
