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

		private bool isDestroyed;
		private Pen pen;
		private Point turretEnd;
		private StatusBar hpBar;

		public Turret(int x, int y, int w, int h, PType p, ETag t) : base(x, y, w, h, t)
		{
			int tRadius = Config.Instance().TurretRadius();
			pen = new Pen(Config.Instance().GetPlayerColor(t));

			// Move tower to left to position it in the center of the given origin
			int nX = Body.Left - Config.Instance().TurretRadius() / 2;
			int nY = Body.Top - Config.Instance().TurretRadius() / 2;
			Body.UpdatePosition(nX, nY);
			isDestroyed = false;

			hpBar = new HealthBar(Body.CenterX, Body.CenterY, 50, 10);
			hpBar.Healed += HpBar_Healed;
		}
		private void HpBar_Healed()
		{
			isDestroyed = false;
		}
		protected override void Collided(Body collider)
		{
			if (hpBar.IsAlive())
				hpBar.Damage();
			else
				isDestroyed = true;
		}
		public override void Draw(Graphics g)
		{
			hpBar.Draw(g);
			g.DrawArc(pen, Body.Left, Body.Top, Body.Width, Body.Height, 180, 180);

			if (!isDestroyed)
				g.DrawLine(pen, Body.Center, turretEnd);
		}
		public void TurretCalculation(Point aim)
		{
			// Difference between where we are aiming and the center of the turret
			int cursorTowerDiffX = aim.X - Body.CenterX;
			int cursorTowerDiffY = Body.CenterY - aim.Y;

			// This is the size of the line that is the gun
			int gunLength = Config.Instance().TurretRadius() / 2 + GUN_END_LENGTH;

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

			turretX = Body.CenterX + turretX;
			turretY = Body.CenterY - turretY;

			turretEnd = new Point(turretX, turretY);
		}
		public void ShootTurret(Point destination)
		{
			TurretShoot(turretEnd, destination, Tag);
		}

		public bool IsDestroyed { get { return isDestroyed; } }
	}
}
