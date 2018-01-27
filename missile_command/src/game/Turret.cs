using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace missile_command
{
	class Turret : Entity
	{
		public delegate void Fire(Point origin, Point destination, ETag a);
		public event Fire TurretShoot;

		private const int GUN_END_LENGTH = 5;
		private const int COOLDOWN_SET = 10;

		private Body aim;
		private double ammo;
		private int maxAmmo;
		private Status hpBar;
		private Status reloadBar;
		private Pen bodyColor;
		private Collider prevCollider;
		private Point turretEnd;
		private long elapsedTime;
		private Rectangle indictatorLight;
		private Rectangle indictatorOutline;

		public bool FireIndicator { get; set; }
		public bool Armed { get { return reloadBar.Alive; } }
		public bool HasAmmo { get { return ((int)ammo % 1 == 0); } }

		public Turret(int x, int y, int w, int h, PType p, ETag t) : base(x, y, w, h, t)
		{
			// Move tower to left to position it in the center of the given origin
			int nX = Body.Left - Config.Instance.TurretDiameter / 2;
			int nY = Body.Top - Config.Instance.TurretDiameter / 2;
			Body.UpdatePosition(nX, nY);

			ammo = 10;
			maxAmmo = 10;
			FireIndicator = false;
			hpBar = new Status(100, Color.Red, Body.Center.X - 23, Body.Center.Y + 4, 50, 10);
			reloadBar = new Status(100, Color.DarkGray, hpBar.Body.Left, hpBar.Body.Bottom + 2, 36, 10);
			int lw = 8; int lh = 8;
			indictatorLight = new Rectangle(reloadBar.Body.Right + 5, reloadBar.Body.Top, lw, lh);
			indictatorOutline = new Rectangle(indictatorLight.Left, indictatorLight.Top, lw, lh);
			bodyColor = new Pen(Config.Instance.GetPlayerColor(t));
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
						Alive = hpBar.Damage(hpBar.MaxValue / 2); // Could make this into a damage value depending if I add upgrades.

				}
				prevCollider = collider;
			}
		}
		public override void Draw(Graphics g)
		{
			hpBar.Draw(g);
			reloadBar.Draw(g);

			g.DrawArc(bodyColor, Body.Left, Body.Top, Body.Width, Body.Height, 180, 180);
			g.DrawString("Ammo " + Convert.ToInt32(ammo).ToString(), new Font("Times New Roman", 12), new SolidBrush(Config.Instance.GetPlayerColor(Tag)), reloadBar.Body.Left - 10, reloadBar.Body.Top + 10);

			if (FireIndicator)
				g.FillEllipse(Brushes.Green, indictatorLight);
			else
				g.FillEllipse(Brushes.Red, indictatorLight);
			g.DrawEllipse(Pens.Black, indictatorOutline);

			if (Alive)
				g.DrawLine(bodyColor, Body.Center, turretEnd);
		}
		public void ShootTurret()
		{
			if (Armed)
			{
				int bmbRadius = Config.Instance.PBombDiameter / 2;
				Point origin = new Point(turretEnd.X - bmbRadius, turretEnd.Y - bmbRadius);
				TurretShoot(origin, aim.Center, Tag);
				reloadBar.Damage(reloadBar.MaxValue);
				ammo--;
			}
		}
		public override void Update(long gameTime)
		{
			CalculateGunEnd();
			if (bodyColor.Color != Config.Instance.GetPlayerColor(Tag))
				bodyColor = new Pen(Config.Instance.GetPlayerColor(Tag));

			reloadBar.Heal(reloadBar.MaxValue / Window.fps);

			if (hpBar.Alive == false)
			{
				double healValue = (hpBar.MaxValue / 50) / (Window.fps);
				Console.WriteLine(healValue);
				hpBar.Heal(healValue);
			}

			if (ammo < maxAmmo)
				ammo += 1.0 / (Window.fps * 3);

			if (KeypressHandler.Instance.Press(Keys.H))
				Alive = hpBar.Damage(hpBar.MaxValue);
			hpBar.Update(gameTime);
		}
		public override void PostUpdate(long gameTime)
		{
			hpBar.PostUpdate(gameTime);
		}
	}
}
