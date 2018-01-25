using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace missile_command
{
	class Player
	{
		private List<Turret> lTurrets;
		private Turret nextTurret;

		private Reticle cursor;
		private PType pType;
		private ETag tag;

		private int fireCount;
		private int coolingDownCount;
		private bool coolingDown;
		private bool noActiveTurrets;

		public ETag GetTag() { return tag; }
		public PType GetPType() { return pType; }

		public Player(PType p, ETag a)
		{
			cursor = new Reticle(Consts.gameBounds.Width / 2, 200, p, a);
			pType = p;
			tag = a;

			lTurrets = new List<Turret>();
			fireCount = 0;
			coolingDownCount = 0;
			coolingDown = false;
			noActiveTurrets = true;
		}
		public void AttachTurret(Turret t)
		{
			t.AttachReticleBody(cursor.Body);
			lTurrets.Add(t);
			noActiveTurrets = false;
		}
		public void Draw(Graphics g)
		{
			cursor.Draw(g);
		}
		private void Shoot()
		{
			if (nextTurret != null)
				nextTurret.ShootTurret();
		}
		public void Update(long gameTime)
		{
			List<Turret> availableTurrets = new List<Turret>();
			foreach (Turret t in lTurrets)
				if (t.Alive && t.HasAmmo && t.Armed)
					availableTurrets.Add(t);

			if (availableTurrets.Count > 0)
			{
				//Find smallest distance from turret and reticle
				int distance = int.MaxValue;
				foreach (Turret t in availableTurrets)
				{
					int x = Math.Abs(t.Body.Left - cursor.Body.Center.X);
					int y = Math.Abs(t.Body.Top - cursor.Body.Center.Y);

					int hypotenuse = Convert.ToInt32(Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)));
					if (hypotenuse < distance)
					{
						nextTurret = t;
						distance = hypotenuse;
					}
				}

				//nextTurret
			}
			else
			{
				nextTurret = null;
			}

			if (KeypressHandler.Instance.Press(Keys.Space))
				Shoot();

			cursor.Update(gameTime);
		}
		public void PostUpdate(long gameTime)
		{
			cursor.PostUpdate(gameTime);
		}
	}
}
