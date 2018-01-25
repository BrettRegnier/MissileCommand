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
	// TODO make reload status, make ammo replenish method (depends on gamemode), make indicator light for which tower will fire, needs a bool. 
	// Green is fire, red is not
	class Turret : Entity
	{
		public delegate void Fire(Point origin, Point destination, ETag a);
		public event Fire TurretShoot;

		private const int GUN_END_LENGTH = 5;
		private const int COOLDOWN_SET = 10;

		private Body aim;
		private int ammo;
		private int cooldown; // TODO player upgrades
		private Status hpBar;
		private Pen pen;
		private Collider prevCollider;
		private Point turretEnd;
		private long elapsedTime;

		public bool Armed { get; private set; }
		public bool HasAmmo { get { return (ammo > 0); } }

		public Turret(int x, int y, int w, int h, PType p, ETag t) : base(x, y, w, h, t)
		{
			// Move tower to left to position it in the center of the given origin
			int nX = Body.Left - Config.Instance.TurretDiameter / 2;
			int nY = Body.Top - Config.Instance.TurretDiameter / 2;
			Body.UpdatePosition(nX, nY);

			ammo = 10;
			Armed = true;
			cooldown = 0;
			hpBar = new Status(100, Color.Red, Body.Center.X, Body.Center.Y, 50, 10);
			pen = new Pen(Config.Instance.GetPlayerColor(t));
		}
		public void AttachReticleBody(Body r)
		{
			aim = r;
		}
		private void CalculateGunEnd()
		{
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
		protected override void Collided(Collider collider)
		{
			if (prevCollider != collider)
			{
				// Need to check to see if the last collider is the same as the one now.
				if (collider.Body.Top < Body.Center.Y)
				{
					if (hpBar.Alive)
						hpBar.Damage(hpBar.MaxValue / 2); // Could make this into a damage value depending if I add upgrades.
				}
				prevCollider = collider;
			}
		}
		public override void Draw(Graphics g)
		{
			hpBar.Draw(g);

			g.DrawArc(pen, Body.Left, Body.Top, Body.Width, Body.Height, 180, 180);
			g.DrawString("Ammo " + ammo.ToString(), new Font("Times New Roman", 12), new SolidBrush(Config.Instance.GetPlayerColor(Tag)), hpBar.Body.Left - 10, hpBar.Body.Top + 10);

			if (hpBar.Alive)
				g.DrawLine(pen, Body.Center, turretEnd);
		}
		public void ShootTurret()
		{
			if (Armed)
			{
				int bmbRadius = Config.Instance.PBombDiameter / 2;
				Point origin = new Point(turretEnd.X - bmbRadius, turretEnd.Y - bmbRadius);
				TurretShoot(origin, aim.Center, Tag);
				Armed = false;
				cooldown = 60;
				ammo--;
			}
		}
		public override void Update(long gameTime)
		{
			hpBar.Update(gameTime);
			CalculateGunEnd();
			if (pen.Color != Config.Instance.GetPlayerColor(Tag))
				pen = new Pen(Config.Instance.GetPlayerColor(Tag));


			if (Armed == false)
			{
				if (cooldown > 0)
					cooldown--;
				else
					Armed = true;
			}

			if (gameTime > elapsedTime + 1000)
			{
				elapsedTime = gameTime;
				if (!hpBar.Alive)
					hpBar.Heal(10);
			}
		}
		public override void PostUpdate(long gameTime)
		{
			hpBar.PostUpdate(gameTime);
		}
	}
}
