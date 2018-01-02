using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace missile_command
{
	// TODO take player out of gameobjects, it isn't one, it controls gameobjects but isn't a gameobject
	class Player
	{
		// TODO make turret into its own class and add them to a player.
		// TODO move delegates to turret
		// TODO Add ammo

		private List<Turret> lTurrets = new List<Turret>();

		private Reticle cursor;
		private PType pType;
		private Account player;

		private int fireCount = 0;
		private int coolingDownCount = 0;
		private bool coolingDown = false;

		public Player(Point pos, PType p, Account ap)
		{
			cursor = new Reticle(new Point(Utils.gameBounds.Width/2, 200), p);
			pType = p;
			player = ap;
		}
		public void AttachTurret(Turret t)
		{
			lTurrets.Add(t);
			t.TurretCalculation(cursor.CenterPosition());
		}
		public void Draw(Graphics g)
		{
			// TODO maybe add statistics into drawing just like cursors.
			cursor.Draw(g);
		}
		public void MoveReticle(Direction dir)
		{
			Point newPoint = cursor.Move(dir);
			for (int i = 0; i < lTurrets.Count; i++)
				lTurrets[i].TurretCalculation(cursor.CenterPosition());
		}
		public void Shoot()
		{
			if (coolingDown == false)
			{
				// TODO add logic to shoot from a tower based on the position of the cursor, if its closer
				// it should fire first, if ammo is 0 then the next closest should fire.
				if (fireCount == lTurrets.Count)
					fireCount = 0;

				// TODO add logic for destroyed turrets
				lTurrets[fireCount++].ShootTurret(cursor.CenterPosition());
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
		public Account GetAccount() { return player; }
	}
}
