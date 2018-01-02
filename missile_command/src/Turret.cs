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
	class Turret : GameObject
	{
		public delegate void Fire(Point origin, Point destination, Account a);
		public event Fire TurretShoot;

		private const int GUN_END_LENGTH = 5;

		private Pen pen;
		private Rectangle tower;
		private Point turretEnd;
		
		public Turret(Point o, Size d, PType p, Account a) : base(o, d, p, a)
		{
			int tRadius = Config.Instance().TowerSize().Width;
			pen = new Pen(Config.Instance().GetPlayerColor(a));

			// Move tower to left to position it in the center of the given origin
			int nX = position.X - Config.Instance().TowerSize().Width / 2;
			int nY = position.Y - Config.Instance().TowerSize().Height / 2;
			UpdatePosition(nX, nY);
			tower = new Rectangle(position, dimension);
		}
		public override void Collided()
		{
			//throw new NotImplementedException();
		}
		public override void Draw(Graphics g)
		{
			g.DrawEllipse(pen, tower);
			g.DrawLine(pen, Center(), turretEnd);
		}
		public void TurretCalculation(Point aim)
		{
			int cursorTowerDiffX = aim.X - Center().X + 2;
			int cursorTowerDiffY = Center().Y - aim.Y + 5;
			int turretDistance = Config.Instance().TowerSize().Width / 2 + GUN_END_LENGTH;

			double turretAngle = Math.Atan((double)cursorTowerDiffY / (double)cursorTowerDiffX);
			int turretX;
			int turretY;

			if (turretAngle > 0)
			{
				turretX = (int)(((Math.Cos(turretAngle) * turretDistance)) + Center().X);
				turretY = (int)(Center().Y - (Math.Sin(turretAngle) * turretDistance));
			}
			else
			{
				turretX = (int)(((Math.Cos(turretAngle) * turretDistance) * -1) + Center().X);
				turretY = (int)(Center().Y - (Math.Sin(turretAngle) * turretDistance) * -1);
			}

			turretEnd = new Point(turretX, turretY);
		}
		public void ShootTurret(Point destination)
		{
			TurretShoot(turretEnd, destination, account);
		}

		public override PType GetPlayerType() { return pType; }
	}
}
