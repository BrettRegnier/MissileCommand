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

		private static int turretCount = 0;
		// TODO use a static constant position for all towers in the config
		private static List<Point> lTurretPos = new List<Point>();

		private Pen pen;
		private Rectangle tower;
		private Point turretEnd;

		static Turret()
		{
			int tRadius = Config.Instance().TowerSize().Width;
			Point middleTower = new Point(Utils.gameBounds.Width / 2 - tRadius / 2, Utils.gameBounds.Height - tRadius / 2);
			Point leftTower = new Point(tRadius / 2, Utils.gameBounds.Height - tRadius / 2);
			Point rightTower = new Point(Utils.gameBounds.Width - (tRadius + tRadius /2), Utils.gameBounds.Height - tRadius / 2);
			lTurretPos.Add(middleTower);
			lTurretPos.Add(leftTower);
			lTurretPos.Add(rightTower);
		}
		public Turret(Point pos, Size d, PType p, Account a) :  base(pos, d, p, a)
		{
			int tRadius = Config.Instance().TowerSize().Width;
			//TODO replace this logic with static positions from config
			tower = new Rectangle(
				lTurretPos[turretCount].X,
				lTurretPos[turretCount].Y,
				tRadius,
				tRadius
				);
			// TODO Gross
			UpdatePosition(lTurretPos[turretCount].X + tRadius / 2, lTurretPos[turretCount].Y + tRadius / 2);

			pen = new Pen(Config.Instance().GetPlayerColor(a));
			turretCount++;
		}
		public override void Collided()
		{
			//throw new NotImplementedException();
		}
		public override void Draw(Graphics g)
		{
			g.DrawEllipse(pen, tower);
			g.DrawLine(pen, position, turretEnd);
		}
		public void TurretCalculation(Point aim)
		{
			int cursorTowerDiffX = aim.X - position.X + 2;
			int cursorTowerDiffY = position.Y - aim.Y + 5;
			int turretDistance = Config.Instance().TowerSize().Width / 2 + GUN_END_LENGTH;

			double turretAngle = Math.Atan((double)cursorTowerDiffY / (double)cursorTowerDiffX);
			int turretX;
			int turretY;

			if (turretAngle > 0)
			{
				turretX = (int)(((Math.Cos(turretAngle) * turretDistance)) + position.X);
				turretY = (int)(position.Y - (Math.Sin(turretAngle) * turretDistance));
			}
			else
			{
				turretX = (int)(((Math.Cos(turretAngle) * turretDistance) * -1) + position.X);
				turretY = (int)(position.Y - (Math.Sin(turretAngle) * turretDistance) * -1);
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
