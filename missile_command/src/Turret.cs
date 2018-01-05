﻿using System;
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
	class Turret : GameObject
	{
		public delegate void Fire(Point origin, Point destination, ETag a);
		public event Fire TurretShoot;

		private const int GUN_END_LENGTH = 5;

		private bool isDestroyed;
		private Pen pen;
		private Rectangle tower;
		private Point turretEnd;

		public Turret(Point o, Size d, PType p, ETag a) : base(o, d, p, a)
		{
			int tRadius = Config.Instance().TurretRadius();
			pen = new Pen(Config.Instance().GetPlayerColor(a));

			// Move tower to left to position it in the center of the given origin
			int nX = position.X - Config.Instance().TurretRadius() / 2;
			int nY = position.Y - Config.Instance().TurretRadius() / 2;
			UpdatePosition(nX, nY);
			tower = new Rectangle(position, dimension);
			isDestroyed = false;
		}
		public override void Collided()
		{
			// TODO for survival add hp?
			isDestroyed = true;
		}
		public override void Draw(Graphics g)
		{
			g.DrawEllipse(pen, tower);

			if (!isDestroyed)
				g.DrawLine(pen, Center(), turretEnd);
		}
		public void TurretCalculation(Point aim)
		{
			// Difference between where we are aiming and the center of the turret
			int cursorTowerDiffX = aim.X - Center().X;
			int cursorTowerDiffY = Center().Y - aim.Y;

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

			turretX = Center().X + turretX;
			turretY = Center().Y - turretY;

			turretEnd = new Point(turretX, turretY);
		}
		public void ShootTurret(Point destination)
		{
			TurretShoot(turretEnd, destination, tag);
		}

		public bool IsDestroyed { get { return isDestroyed; } }
	}
}
