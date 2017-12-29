using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace missile_command
{
	class Player : GameObject
	{
		private const int SCREEN_OFFSET = 5;

		private Reticle cursor;
		private Rectangle turret;


		// TODO make origin into a list, and make gunends into a list
		private static Point turretEnd;
		private static Point origin;
		private static Point bounds;
		private static int turretRadius = 100;
		private static List<Rectangle> towers;
		private static Pen pen;


		static Player()
		{
			towers = new List<Rectangle>();
			int width = Screen.PrimaryScreen.Bounds.Width - SCREEN_OFFSET;
			int height = Screen.PrimaryScreen.Bounds.Height - SCREEN_OFFSET;
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
			origin = new Point(bounds.X / 2, bounds.Y);

			pen = new Pen(Config.Instance().GetPlayerColor(PType.PLAYER1));
		}
		public Player(Point pos, PType p) : base(pos, p)
		{
			Point reticleOrigin = new Point(200, 200);
			cursor = new Reticle(reticleOrigin, p, bounds);
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
				// TODO add the list of drawing gun ends
				g.DrawEllipse(pen, towers[i]);
			}
			g.DrawLine(pen, origin, turretEnd);
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
		public Bomb Shoot()
		{
			throw new NotImplementedException();
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
			int cursorTowerDiffX = cursor.CenterPosition().X - origin.X +2 ;
			int cursorTowerDiffY = origin.Y - cursor.CenterPosition().Y + 5;
			int turretDistance = turretRadius / 2;

			double turretAngle = Math.Atan((double)cursorTowerDiffY / (double)cursorTowerDiffX);
			int turretX;
			int turretY;

			if (turretAngle > 0)
			{
				turretX = (int)(((Math.Cos(turretAngle) * turretDistance)) + origin.X);
				turretY = (int)(origin.Y - (Math.Sin(turretAngle) * turretDistance));
			}
			else
			{
				turretX = (int)(((Math.Cos(turretAngle) * turretDistance) * -1) + origin.X);
				turretY = (int)(origin.Y - (Math.Sin(turretAngle) * turretDistance) * -1);
			}

			turretEnd = new Point(turretX, turretY);
		}
	}
}
