using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace missile_command
{
	class Player : GameObject
	{
		private Reticle cursor;
		private Rectangle turret;

		private static List<Rectangle> towers;
		private static Pen pen;

		static Player()
		{
			towers = new List<Rectangle>();
			int width = Screen.PrimaryScreen.Bounds.Width - 5;
			int height = Screen.PrimaryScreen.Bounds.Height - 5;
			Rectangle leftTower = new Rectangle(new Point(50, height - 50), new Size(50, 50));
			Rectangle middleTower = new Rectangle(new Point(width / 2, height - 50), new Size(50, 50));
			Rectangle rightTower = new Rectangle(new Point(width - 50, height - 50), new Size(50, 50));
			towers.Add(leftTower);
			towers.Add(middleTower);
			towers.Add(rightTower);

			pen = new Pen(Config.Instance().GetPlayerColor(PType.PLAYER1));
		}

		public Player(Point pos, PType p) : base(pos, p)
		{
			Point reticleOrigin = new Point(200, 200);
			cursor = new Reticle(reticleOrigin, p);
		}
		public override void Draw(Graphics g)
		{
			// Need to calculate the circle size, and the line, with the angle based on the aming reticle
			cursor.Draw(g);
		}
		public static void DrawTurrets(Graphics g)
		{
			for (int i = 0; i < towers.Count; i++)
				g.DrawEllipse(pen, towers[i]);
		}
		public override void Collided()
		{
			throw new NotImplementedException();
		}
		public void MoveReticle()
		{

		}

		public PType GetPType { get { return player; } }
	}
}
