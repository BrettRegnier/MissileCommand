using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace missile_command
{
	class Player : GameObject
	{
		public delegate void Fire(Point origin, Point destination);
		public event Fire TurretShoot;

		private Reticle cursor;
		private int fireCount = 0;
		private int coolingDownCount = 0;
		private bool coolingDown = false;

		// TODO perhaps remove the number of players I dont know if I want to have more than one player
		private static int numPlayers = 0;
		private static int turretRadius = 100;
		private static Pen pen;
		private static Point bounds;
		private static List<Point> lTurretEnd = new List<Point>();
		private static List<Point> lOrigin = new List<Point>();
		private static List<Rectangle> towers = new List<Rectangle>();

		static Player()
		{
			int width = Screen.PrimaryScreen.Bounds.Width - Utils.SCREEN_OFFSET;
			int height = Screen.PrimaryScreen.Bounds.Height - Utils.SCREEN_OFFSET;
			bounds = new Point(width, height);
			Rectangle leftTower = new Rectangle(
				turretRadius / 2,
				bounds.Y - turretRadius / 2,
				turretRadius,
				turretRadius
				);
			Rectangle middleTower = new Rectangle(
				bounds.X / 2 - turretRadius / 2,
				bounds.Y - turretRadius / 2,
				turretRadius,
				turretRadius
				);
			Rectangle rightTower = new Rectangle(
				bounds.X - 150,
				bounds.Y - turretRadius / 2,
				turretRadius,
				turretRadius
				);
			towers.Add(leftTower);
			towers.Add(middleTower);
			towers.Add(rightTower);

			lOrigin.Add(new Point(turretRadius, bounds.Y));
			lOrigin.Add(new Point(bounds.X / 2, bounds.Y));
			lOrigin.Add(new Point(bounds.X - 100, bounds.Y));

			for (int i = 0; i < 3; i++)
				lTurretEnd.Add(new Point());

			pen = new Pen(Config.Instance().GetPlayerColor(PType.PLAYER1));
		}
		public Player(Point pos, PType p) : base(pos, p)
		{
			numPlayers++;
			cursor = new Reticle(new Point(lOrigin[1].X, 200), p, bounds);
			TurretCalculation();
		}
		public override void Draw(Graphics g)
		{
			// Need to calculate the circle size, and the line, with the angle based on the aiming reticle
			cursor.Draw(g);
		}
		public static void DrawBase(Graphics g)
		{
			for (int i = 0; i < towers.Count; i++)
			{
				g.DrawEllipse(pen, towers[i]);
				g.DrawLine(pen, lOrigin[i], lTurretEnd[i]);
			}
		}
		public override void Collided()
		{
			throw new NotImplementedException();
		}
		public void MoveReticle(Direction dir)
		{
			Point newPoint = cursor.Move(dir);
			TurretCalculation();
		}
		public void Shoot()
		{
			if (coolingDown == false)
			{
				if (fireCount > 2)
					fireCount = 0;

				TurretShoot(lTurretEnd[fireCount++], cursor.CenterPosition());
				coolingDown = true;
			}
			else
			{
				coolingDownCount++;
			}

			if (coolingDownCount == 10)
			{
				coolingDown = false;
				coolingDownCount = 0;
			}
		}
		public PType GetPType
		{
			get
			{
				return player;
			}
		}

		private void TurretCalculation()
		{
			for (int i = 0; i < lTurretEnd.Count; i++)
			{
				int cursorTowerDiffX = cursor.CenterPosition().X - lOrigin[i].X + 2;
				int cursorTowerDiffY = lOrigin[i].Y - cursor.CenterPosition().Y + 5;
				int turretDistance = turretRadius / 2;

				double turretAngle = Math.Atan((double)cursorTowerDiffY / (double)cursorTowerDiffX);
				int turretX;
				int turretY;

				if (turretAngle > 0)
				{
					turretX = (int)(((Math.Cos(turretAngle) * turretDistance)) + lOrigin[i].X);
					turretY = (int)(lOrigin[i].Y - (Math.Sin(turretAngle) * turretDistance));
				}
				else
				{
					turretX = (int)(((Math.Cos(turretAngle) * turretDistance) * -1) + lOrigin[i].X);
					turretY = (int)(lOrigin[i].Y - (Math.Sin(turretAngle) * turretDistance) * -1);
				}

				lTurretEnd[i] = new Point(turretX, turretY);
			}
		}
	}
}
